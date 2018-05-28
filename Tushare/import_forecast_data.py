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

from sqlalchemy import create_engine
import conf as config

forcastYear = config.FORECAST_YEAR
forecastMonth = config.FORECAST_MONTH
forecastQuarter = config.FORECAST_QUARTER

isPy3 = (sys.version_info[0] >= 3)
	
items = ['code', 'name', 'type', 'pub_date', 'report_date', 'summary', 'eps_last', 'forecast_chg'];
	
def get_date_year(date):
	if len(date) < 4:
		return 0
	year = date[:4]
	return year
	
def get_date_month(date):
	if len(date) < 7:
		return 0
	month = date[5:7]
	return month
	
def get_forecast_data(code):
	rpurl = 'http://vip.stock.finance.sina.com.cn/q/go.php/vFinanceAnalyze/kind/performance/index.phtml?symbol=%s' % (code)
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
		df = df.drop(8, axis=1)
		df.columns = items
		#arr = arr.append(df, ignore_index=True)
		nextPage = html.xpath('//div[@class=\"pages\"]/a[last()]/@onclick')		
		return df
	except Exception as e:
		print('error')
		pass

def save_forecast_data(code, name, type, report_date, summary, eps_last, forecast_chg):
	table_name = 'stock_forecast_' + str(forcastYear) + 'q' + str(forecastQuarter)
	db = MySQLdb.connect(host='localhost',port=3306,user='root',passwd='123456',db='stock',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10) not null primary key, name varchar(16), type text, report_date text, summary text, eps_last text, forecast_chg text)'
	cursor.execute(createDBSql)			
	prefix = 'insert into ' + table_name + '(code, name, type, report_date, summary, eps_last, forecast_chg) values(\'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\')'
	sql = prefix % (code, name, type, report_date, summary, eps_last, forecast_chg)
	#print(sql)
	cursor.execute(sql)
	db.commit()
	print(name + ' ' + year + 'Q' + season)
		
	db.close()
	
def achieve_forecast_data(code):
	df = get_forecast_data(code)
	if df is None:
		return
	for i in range(0, len(df)):		
		date = str(df['report_date'][i])
		if date is None:
			continue
		if date == 'nan':
			continue
		year = int(get_date_year(date))
		if year < forcastYear:
			continue
		month = int(get_date_month(date))
		if month < forecastMonth:
			continue
		try:
			save_forecast_data(code, df['name'][i], df['type'][i], df['report_date'][i], df['summary'][i], df['eps_last'][i], df['forecast_chg'][i])
		except Exception as e:
			pass

def achieve_all_forecast_data():
	f = open("stock_codes.txt")
	for line in f:
		code = line.strip()
		achieve_forecast_data(code)
	print('Finish achieve all forecast.')
		
if isPy3 == False:
	reload(sys)  
	sys.setdefaultencoding('utf-8') 
achieve_all_forecast_data()

#frame = get_forecast_data('600995')
#frame.to_csv('./test_forecast.txt')
	
