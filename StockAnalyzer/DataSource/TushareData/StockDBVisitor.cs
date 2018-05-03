﻿using StockAnalyzer.Common;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    class StockDBVisitor : Singleton<StockDBVisitor>
    {
        public StockReportData getStockReportData(string stockID, string year, string season)
        {
            string table = "stock_report_" + year + "s" + season;
            string sql = "select * from " + table + "where code=" + stockID;
            List<string> rs = MySqlDBReader.querySql(sql);
            if(rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockReportData(rs);
            }
            return null;
        }

        public StockGrowthData getStockGrowthData(string stockID, string year, string season)
        {
            string table = "stock_growth_" + year + "s" + season;
            string sql = "select * from " + table + "where code=" + stockID;
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockGrowthData(rs);
            }
            return null;
        }

        public StockCashflowData getStockCashflowData(string stockID, string year, string season)
        {
            string table = "stock_cashflow_" + year + "s" + season;
            string sql = "select * from " + table + "where code=" + stockID;
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockCashflowData(rs);
            }
            return null;
        }
    }
}
