import pymysql as MySQLdb

from sqlalchemy import create_engine
import tushare as ts

def get_stock_report_manual(year, season):
	frame = ts.get_report_data(year, season)
	table_name = 'stock_report_' + str(year) + 's' + str(season)
	db = MySQLdb.connect("localhost","root","123456",'stock_ts',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10), name varchar(16), eps text, eps_yoy text, bvps text, roe text, epcf text, net_profits text, profits_yoy text, distrib text, report_date text)'
	cursor.execute(createDBSql)
	for i in range(0, len(frame)):		
		prefix = 'insert into ' + table_name + '(code, name, eps, eps_yoy, bvps, roe, epcf, net_profits, profits_yoy, distrib, report_date) select \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\' from dual where not exists (select * from' + table_name + ' where code = \'%s\')'
		sql = prefix % (frame['code'][i], str(frame['name'][i]), float(frame['eps'][i]), float(frame['eps_yoy'][i]), float(frame['bvps'][i]), float(frame['roe'][i]), float(frame['epcf'][i]), float(frame['net_profits'][i]), float(frame['profits_yoy'][i]), frame['distrib'][i], str(frame['report_date'][i]), frame['code'][i])
		#print(sql)
		cursor.execute(sql)
		db.commit()
		print(frame['name'][i])
		
	db.close()
	#frame['code'][0]
	
def get_stock_growth_manual(year, season):
	frame = ts.get_growth_data(year, season)
	table_name = 'stock_growth_' + str(year) + 's' + str(season)
	db = MySQLdb.connect("localhost","root","123456",'stock_ts',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10), name varchar(16), mbrg text, nprg text, nav text, targ text, epsg text, seg text)'
	cursor.execute(createDBSql)
	for i in range(0, len(frame)):
		prefix = 'insert into ' + table_name + '(code, name, mbrg, nprg, nav, targ, epsg, seg) select \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\' from dual where not exists (select * from' + table_name + ' where code = \'%s\')'
		sql = prefix % (frame['code'][i], str(frame['name'][i]), str(frame['mbrg'][i]), str(frame['nprg'][i]), str(frame['nav'][i]), str(frame['targ'][i]), str(frame['epsg'][i]), str(frame['seg'][i]), frame['code'][i])
		#print(sql)
		cursor.execute(sql)
		db.commit()
		print(frame['name'][i])
		
	db.close()
	#frame.to_sql(table_name, engine)
	
def get_stock_cashflow_manual(year, season):
	frame = ts.get_cashflow_data(year, season)
	table_name = 'stock_cashflow_' + str(year) + 's' + str(season)
	#frame.to_sql(table_name, engine)
	db = MySQLdb.connect("localhost","root","123456",'stock_ts',charset='utf8')
	cursor = db.cursor()
	createDBSql = 'create table if not exists ' + table_name + '(code varchar(10), name varchar(16), cf_sales text, rateofreturn text, cf_nm text, cf_liabilities text, cashflowratio text)'
	cursor.execute(createDBSql)
	for i in range(0, len(frame)):		
		prefix = 'insert into ' + table_name + '(code, name, cf_sales, rateofreturn, cf_nm, cf_liabilities, cashflowratio) select \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\', \'%s\' from dual where not exists (select * from' + table_name + ' where code = \'%s\')'
		sql = prefix % (frame['code'][i], str(frame['name'][i]), str(frame['cf_sales'][i]), str(frame['rateofreturn'][i]), str(frame['cf_nm'][i]), str(frame['cf_liabilities'][i]), str(frame['cashflowratio'][i]), frame['code'][i])
		#print(sql)
		cursor.execute(sql)
		db.commit()
		print(frame['name'][i])
		
	db.close()

startYear = 2012
startSeason = 1
curYear = 2018
curSeason = 2

for year in range(2012, 2019):
	for season in range(1, 5):
		if year == curYear and season >= curSeason:
			break
		elif year < startYear:
			continue
		elif year == startYear and season < startSeason:
			continue
		else:
			get_stock_report_manual(year, season)
			#get_stock_operation(year, season)
			get_stock_growth_manual(year, season)
			#get_stock_debtpaying(year, season)
			get_stock_cashflow_manual(year, season)
			

