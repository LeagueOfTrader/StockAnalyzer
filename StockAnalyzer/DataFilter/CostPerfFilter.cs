using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.DataSource.TushareData;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class CostPerfFilter : StockFilter
    {
        private string m_srcYear = "2013";
        private string m_srcSeason = "1";
        private string m_targetYear = "2018";
        private string m_targetSeason = "1";
        private double m_ratio = 0.0;

        public CostPerfFilter(string srcYear, string srcSeason, string targetYear, string targetSeason, double ratio)
        {
            m_srcYear = srcYear;
            m_srcSeason = srcSeason;
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_ratio = ratio;
        }

        public override bool filterMethod(string stockID)
        {
            double srcVal = calcCostRefValue(stockID, m_srcYear, m_srcSeason);
            double targetVal = calcCostRefValue(stockID, m_targetYear, m_targetSeason);

            if(srcVal < double.Epsilon)
            {
                return false;
            }

            double chg = (targetVal - srcVal) / srcVal;
            if(chg > m_ratio)
            {
                return true;
            }

            return false;
        }

        public static double calcCostRefValue(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);
            
            string str = StockDataCollector.queryMonthlyKLineData(stockID);
            List<StockKLine> mk = StockDataConvertor.parseKLineArray(str);
            string targetMonth = convertMonthBySeason(season);
            StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);
            if(rd != null && kl != null)
            {
                costRefVal = rd.eps / kl.latestPrice;
            }

            return costRefVal;
        }

        private static string convertMonthBySeason(string season)
        {
            string month = "12";
            if(season == "1")
            {
                month = "3";
            }
            else if(season == "2")
            {
                month = "6";
            }
            else if(season == "3")
            {
                month = "9";
            }

            return month;
        }
    }
}
