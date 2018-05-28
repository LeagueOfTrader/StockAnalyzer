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
import conf as config

startYear = config.FROM_YEAR
endYear = config.REPORT_YEAR
endQuarter = config.REPORT_YEAR

items = ['code', 'name', 'inst_count', 'count_chg', 'hold_ratio', 'hold_chg', 'prop_of_circulation', 'poc_chg']
isPy3 = (sys.version_info[0] >= 3)

def get_inst_pos_data(code, year, quarter):
	url = 'http://vip.stock.finance.sina.com.cn/q/go.php/vComStockHold/kind/jgcg/index.phtml?symbol=%s&reportdate=%s&quarter=%s' % (code, year, quarter)
	try:
		request = Request(url)
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
		df = df.drop(8, axis=1)
		df.columns = items
		#arr = arr.append(df, ignore_index=True)
		nextPage = html.xpath('//div[@class=\"pages\"]/a[last()]/@onclick')		
		return df
	except Exception as e:
		print('error: ' + e.message)
		return None
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
		
def save_inst_pos_data(code, name, inst_count, count_chg, hold_ratio, hold_chg, prop_of_circulation, poc_chg, year, season):
	if season == '0':
		return
	table_name = 'stock_institution_positions_' + str(year) + 'q' + str(season)
	db = MySQLdb.connect(host='localhost',port=3306,user='root',passwd='123456',db='stock',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10) not null primary key, name varchar(16), inst_count text, count_chg text, hold_ratio text, hold_chg text, prop_of_circulation text, poc_chg text)'
	cursor.execute(createDBSql)			
	prefix = 'insert into ' + table_name + '(code, name, inst_count, count_chg, hold_ratio, hold_chg, prop_of_circulation, poc_chg) values(\'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\')'
	sql = prefix % (code, name, inst_count, count_chg, hold_ratio, hold_chg, prop_of_circulation, poc_chg)
	#print(sql)
	cursor.execute(sql)
	db.commit()
	print(name + ' ' + year + 'Q' + season)
		
	db.close()
	
def achieve_inst_pos_data(code, year, quarter):
	df = get_inst_pos_data(code, year, quarter)
	if df is None:
		return
	for i in range(0, len(df)):		
		try:
			save_inst_pos_data(code, df['name'][i], df['inst_count'][i], df['count_chg'][i], df['hold_ratio'][i], df['hold_chg'][i], df['prop_of_circulation'][i], df['poc_chg'][i], year, quarter)
		except Exception as e:
			pass

def achieve_all_inst_pos_data():
	f = open("stock_codes.txt")
	for line in f:
		code = line.strip()
		for y in range(startYear, endYear):
			for q in range(1, 5):
				achieve_inst_pos_data(code, y, q)
		for cq in range(1, endQuarter + 1):
			achieve_inst_pos_data(code, endYear, cq)
			
	print('Finish achieve all inst hold.')
		
if isPy3 == False:
	reload(sys)  
	sys.setdefaultencoding('utf-8') 
achieve_all_inst_pos_data()
#achieve_inst_pos_data('sz300498', 2015, 1)
print('Finish!')
	
