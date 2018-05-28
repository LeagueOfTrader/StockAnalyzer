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

startYear = 2007
endYear = 2018
endQuarter = 1

isPy3 = (sys.version_info[0] >= 3)

items = ['date', 'holders_count', 'holders_chg', 'avg_stock', 'stock_chg']

def get_shareholder_data(code, year, quarter):
	#url = 'http://data.eastmoney.com/gdhs/detail/%s.html' % (code)
	#url = 'http://stock.jrj.com.cn/share,%s,gdhs.shtml' % (code)
	url = 'http://quotes.money.163.com/f10/gdfx_%s.html' % (code)
	tabClassName = 'table_bg001 border_box gudong_table'
	print(url)
	try:
		request = Request(url)
		text = urlopen(request, timeout=10).read()
		#text = text.decode('GB2312')
		#text = text.replace('--', '')
		#print(text)
		html = lxml.html.parse(StringIO(text))
		res = html.xpath("//table[@class=\"" + tabClassName + "\"]/tr")
		#print(res)
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
		#df = df.drop(13, axis=1)
		df.columns = items
		
		#nextPage = html.xpath('//div[@class=\"pages\"]/a[last()]/@onclick')		
		return df
	except Exception as e:
		print('error: ' + e.message)
		return None
		pass
		
frame = get_shareholder_data('601801', 2018, 1)
if frame is None:
	print('null')
else:
	print(frame);
