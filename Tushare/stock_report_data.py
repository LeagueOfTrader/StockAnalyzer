import pymysql as MySQLdb

from sqlalchemy import create_engine
import tushare as ts

useDB = False

def get_stock_report(year, season, engine):
	frame = ts.get_report_data(year, season)
	table_name = 'stock_report_' + str(year) + 's' + str(season)
	if useDB == True:
		frame.to_sql(table_name, engine)
	else:
		frame.to_csv(table_name + '.csv')
	
def get_stock_operation(year, season, engine):
	frame = ts.get_operation_data(year, season)
	table_name = 'stock_operation_' + str(year) + 's' + str(season)
	if useDB == True:
		frame.to_sql(table_name, engine)
	else:
		frame.to_csv(table_name + '.csv')
	
def get_stock_growth(year, season, engine):
	frame = ts.get_growth_data(year, season)
	table_name = 'stock_growth_' + str(year) + 's' + str(season)
	if useDB == True:
		frame.to_sql(table_name, engine)
	else:
		frame.to_csv(table_name + '.csv')
	
def get_stock_debtpaying(year, season, engine):
	frame = ts.get_debtpaying_data(year, season)
	table_name = 'stock_debtpaying_' + str(year) + 's' + str(season)
	if useDB == True:
		frame.to_sql(table_name, engine)
	else:
		frame.to_csv(table_name + '.csv')
	
def get_stock_cashflow(year, season, engine):
	frame = ts.get_cashflow_data(year, season)
	table_name = 'stock_cashflow_' + str(year) + 's' + str(season)
	if useDB == True:
		frame.to_sql(table_name, engine)
	else:
		frame.to_csv(table_name + '.csv')

startYear = 2012
startSeason = 1
curYear = 2018
curSeason = 2
engine = create_engine('mysql://root:123456@127.0.0.1/stock?charset=utf8')

for year in range(2012, 2019):
	for season in range(1, 5):
		if year == curYear and season >= curSeason:
			break
		elif year < startYear:
			continue
		elif year == startYear and season < startSeason:
			continue
		else:
			#frame = ts.get_report_data(year, season)			
			#table_name = 'stock_report_' + str(year) + 's' + str(season)
			#frame.to_sql(table_name, engine)
			get_stock_report(year, season, engine)
			'''
			get_stock_operation(year, season, engine)
			get_stock_growth(year, season, engine)
			get_stock_debtpaying(year, season, engine)
			get_stock_cashflow(year, season, engine)
			'''
			

