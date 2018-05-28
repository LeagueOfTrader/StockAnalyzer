'''
import sys
reload(sys)
sys.setdefaultencoding('utf-8')
ecd = sys.getdefaultencoding()
print(ecd)
'''

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

startYear = config.REPORT_YEAR - 1
endYear = config.REPORT_YEAR
curQuarter = config.REPORT_QUARTER

isPy3 = (sys.version_info[0] >= 3)

items = ['date', 'holders_count', 'holders_chg', 'avg_stock', 'avg_stock_chg']

def get_stockholder_data(code):
	url = 'http://quotes.money.163.com/f10/gdfx_%s.html' % (code)
	tabClassName = 'table_bg001 border_box gudong_table'
	#print(url)
	try:
		request = Request(url)
		text = urlopen(request, timeout=10).read()
		html = lxml.html.parse(StringIO(text))
		res = html.xpath("//table[@class=\"" + tabClassName + "\"]/tr")
		if isPy3:
			sarr = [etree.tostring(node).decode('utf-8') for node in res]
		else:
			sarr = [etree.tostring(node) for node in res]
		sarr = ''.join(sarr)
		#print(sarr)
		if len(sarr) == 0:
			return None
		sarr = '<table>%s</table>'%sarr
		df = pd.read_html(sarr)[0]
		df.columns = items
		return df
	except Exception as e:
		print('error: ' + e.message)
		return None
		pass
		
		
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
		
		
def save_stockholder_data(code, date, holders_count, holders_chg, avg_stocks, avg_stocks_chg):
	year = get_date_year(date)
	season = get_date_quarter(date)
	if season == '0':
		return
	table_name = 'stock_stockholder_' + str(year) + 'q' + str(season)
	db = MySQLdb.connect(host='localhost',port=3306,user='root',passwd='123456',db='stock',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10) not null primary key, date text, holders_count text, holders_chg text, avg_stocks text, avg_stocks_chg text)'
	cursor.execute(createDBSql)			
	prefix = 'insert into ' + table_name + '(code, date, holders_count, holders_chg, avg_stocks, avg_stocks_chg) values(\'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\')'
	sql = prefix % (code, date, holders_count, holders_chg, avg_stocks, avg_stocks_chg)
	cursor.execute(sql)
	db.commit()
	print(name + ' ' + year + 'Q' + season)		
	db.close()
		
		
def achieve_stockholder_data(code):
	df = get_stockholder_data(code)
	if df is None:
		return
	for i in range(0, len(df)):		
		date = str(df['date'][i])
		if date == 'nan':
			continue
		year = int(get_date_year(date))
		quarter = int(get_date_quarter(date))
		if year < startYear:
			continue
		if year == startYear and quarter <= curQuarter:
			continue
		try:
			save_stockholder_data(code, df['date'][i], df['holders_count'][i], df['holders_chg'][i], df['avg_stocks'][i], df['avg_stocks_chg'][i])
		except Exception as e:
			pass
		
def achieve_all_stockholder_data():
	f = open("stock_codes.txt")
	for line in f:
		code = line.strip()
		achieve_stockholder_data(code)
	print('Finish achieve all stockholder.')
	
achieve_all_stockholder_data()
