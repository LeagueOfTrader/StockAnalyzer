using StockAnalyzer.Common;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    public class StockDBVisitor : Singleton<StockDBVisitor>
    {
        public StockReportData getStockReportData(string stockID, string year, string season)
        {
            string table = "stock_report_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select * from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if(rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockReportData(rs);
            }
            return null;
        }

        public StockGrowthData getStockGrowthData(string stockID, string year, string season)
        {
            string table = "stock_growth_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select * from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockGrowthData(rs);
            }
            return null;
        }

        public StockCashflowData getStockCashflowData(string stockID, string year, string season)
        {
            string table = "stock_cashflow_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select * from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockCashflowData(rs);
            }
            return null;
        }

        public StockDistribData getDistribData(string stockID, string year, string season)
        {
            string table = "stock_report_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select distrib from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockDistribData(rs[0], code);
            }
            return null;
        }

        public StockProfitData getStockProfitData(string stockID, string year, string season)
        {
            string table = "stock_profit_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select * from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockProfitData(rs);
            }
            return null;
        }

        public StockForecastData getStockForecastData(string stockID, string year, string season)
        {
            string table = "stock_forecast_" + year + "q" + season;
            string code = StockIDUtil.getPureCode(stockID);
            string sql = "select * from " + table + " where code='" + code + "'";
            List<string> rs = MySqlDBReader.querySql(sql);
            if (rs != null && rs.Count > 0)
            {
                return TushareDataConvertor.parseStockForecastData(rs);
            }
            return null;
        }
    }
}
