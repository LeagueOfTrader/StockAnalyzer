import pymysql as MySQLdb
import tushare as ts
import pandas as pd
import lxml.html
from lxml import etree
import re
import time
from pandas.compat import StringIO
try:
    from urllib.request import urlopen, Request
except ImportError:
    from urllib2 import urlopen, Request
import sys
import math

limitYear = 2007
items = ['name', 'report_date', 'eps', 'eps_yoy', 'bvps', 'roe', 'epcf', 'net_profits', 'profits_yoy', 'distrib', 'pub_date']
isPy3 = (sys.version_info[0] >= 3)

def get_report_data(code):
	rpurl = 'http://vip.stock.finance.sina.com.cn/q/go.php/vFinanceAnalyze/kind/mainindex/index.phtml?symbol=%s' % (code)
	try:
		request = Request(rpurl)
		text = urlopen(request, timeout=10).read()
		text = text.decode('GBK')
		text = text.replace('--', '')
		html = lxml.html.parse(StringIO(text))
		res = html.xpath("//table[@class=\"list_table\"]/tr")		
		if isPy3:
			sarr = [etree.tostring(node).decode('utf-8') for node in res]
		else:
			sarr = [etree.tostring(node) for node in res]
		sarr = ''.join(sarr)
		if len(sarr) == 0:
			return None
		sarr = '<table>%s</table>'%sarr
		df = pd.read_html(sarr)[0]
		df = df.drop(11, axis=1)
		df.columns = items
		#arr = arr.append(df, ignore_index=True)
		nextPage = html.xpath('//div[@class=\"pages\"]/a[last()]/@onclick')		
		return df
	except Exception as e:
		print('error: ' + e.message)
		pass
    #raise IOError('error 2311')
	
def get_date_year(date):
	year = date[:4]
	return year
	
def get_date_month(date):
	month = date[5:7]
	return month

def get_date_quarter(date):
	month = get_date_month(date)
	if month == '03':
		return '1'
	elif month == '06':
		return '2'
	elif month == '09':
		return '3'
	elif month == '12':
		return '4'
	else:
		return '0'
		
def save_report_data(code, name, eps, eps_yoy, bvps, roe, epcf, net_profits, profits_yoy, distrib, report_date):
	year = get_date_year(report_date)
	season = get_date_quarter(report_date)
	if season == '0':
		return
	table_name = 'stock_report_' + str(year) + 'q' + str(season)
	db = MySQLdb.connect(host='localhost',port=3306,user='root',passwd='123456',db='stock',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10) not null primary key, name varchar(16), eps text, eps_yoy text, bvps text, roe text, epcf text, net_profits text, profits_yoy text, distrib text, report_date text)'
	cursor.execute(createDBSql)			
	prefix = 'insert into ' + table_name + '(code, name, eps, eps_yoy, bvps, roe, epcf, net_profits, profits_yoy, distrib, report_date) values(\'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\')'
	sql = prefix % (code, name, eps, eps_yoy, bvps, roe, epcf, net_profits, profits_yoy, distrib, report_date)
	#print(sql)
	cursor.execute(sql)
	db.commit()
	print(name + ' ' + year + 'Q' + season)
		
	db.close()
	
def achieve_report_data(code):
	df = get_report_data(code)
	if df is None:
		return
	for i in range(0, len(df)):		
		date = str(df['report_date'][i])
		if date == 'nan':
			continue
		year = int(get_date_year(date))
		if year < limitYear:
			continue
		distrib = str(df['distrib'][i])
		if distrib == 'nan':
			distrib = ''
		try:
			save_report_data(code, df['name'][i], df['eps'][i], df['eps_yoy'][i], df['bvps'][i], df['roe'][i], df['epcf'][i], df['net_profits'][i], df['profits_yoy'][i], distrib, date)
		except Exception as e:
			pass

def achieve_all_report_data():
	f = open("stock_codes.txt")
	for line in f:
		code = line.strip()
		achieve_report_data(code)
	print('Finish achieve all report.')
		
if isPy3 == False:
	reload(sys)  
	sys.setdefaultencoding('utf-8') 
achieve_all_report_data()
	
