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
	
def get_profit_data(year, season):
	frame = ts.get_profit_data(year, season)
	table_name = 'stock_profit_' + str(year) + 'q' + str(season)
	#frame.to_sql(table_name, engine)
	db = MySQLdb.connect("localhost","root","123456",'stock',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10), name varchar(16), roe text, net_profit_ratio text, gross_profit_rate text, net_profits text, eps text, business_income text, bips text)'
	cursor.execute(createDBSql)
	for i in range(0, len(frame)):		
		prefix = 'insert into ' + table_name + '(code, name, roe, net_profit_ratio, gross_profit_rate, net_profits, eps, business_income, bips) select \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\' from dual where not exists (select * from ' + table_name + ' where code = \'%s\')'
		sql = prefix % (frame['code'][i], str(frame['name'][i]), str(frame['roe'][i]), str(frame['net_profit_ratio'][i]), str(frame['gross_profit_rate'][i]), str(frame['net_profits'][i]), str(frame['eps'][i]), str(frame['business_income'][i]), str(frame['bips'][i]), frame['code'][i])
		cursor.execute(sql)
		db.commit()
		print(frame['name'][i])
		
	db.close()

startYear = config.FROM_YEAR2
startSeason = config.FROM_QUARTER
curYear = config.REPORT_YEAR
curSeason = config.REPORT_QUARTER + 1

def get_all_profits_data():
	for year in range(2012, 2019):
		for season in range(1, 5):
			if year == curYear and season >= curSeason:
				break
			elif year < startYear:
				continue
			elif year == startYear and season < startSeason:
				continue
			else:
				get_profit_data(year, season)
			
#get_profit_data(2018, 1)
get_all_profits_data()
	
