using StockAnalyzer.Assist;
using StockAnalyzer.DataFilter.DataCache;
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
    public class CostPerfFilter : NumericStockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected double m_ratio = 0.0;  

        public CostPerfFilter(string targetYear, string targetSeason, double ratio)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_ratio = ratio;

            m_filterDesc = "DynamicCostPerf";
        }

        public override bool filterMethod(string stockID)
        {
            double chg = 0.0;
            bool ret = getNumericValue(stockID, out chg);
            if (!ret)
            {
                return false;
            }

            if(chg > m_ratio)
            {
                return true;
            }

            return false;
        }

        protected virtual double getSrcValue(string stockID)
        {
            //int maxYear = 0, maxQuarter = 0;
            //double srcVal = getMaxCostRefValueBefore(stockID, m_targetYear, m_targetSeason, out maxYear, out maxQuarter);
            double srcVal = StockDataCache.getInstance().getMaxCostRefValueBefore(stockID, m_targetYear, m_targetSeason);
            return srcVal;
        }

        public static double getMaxCostRefValueBefore(string stockID, string year, string season, out int maxYear, out int maxQuarter)
        {
            int endYear = int.Parse(year);
            maxYear = 0;// endYear;
            maxQuarter = 0;// int.Parse(season);
            double maxVal = 0.0;
            for(int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                for(int j = 1; j <= 4; j++)
                {
                    string qt = j.ToString();
                    double val = calcCostRefValue(stockID, yr, qt);
                    if(val > maxVal)
                    {
                        maxVal = val;
                        maxYear = i;
                        maxQuarter = j;
                    }
                }
            }

            int endQuarter = int.Parse(season);
            for(int i = 1; i < endQuarter; i++)
            {
                string qt = i.ToString();
                double val = calcCostRefValue(stockID, year, qt);
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = endYear;
                    maxQuarter = i;
                }
            }

            Logger.log("Best forecast annual cost for " + stockID + " before " + year + "Q" + season + ": " + maxYear + "Q" + maxQuarter);

            return maxVal;
        }

        public static double calcCostRefValue(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);

            List<StockKLine> mk = StockDataCenter.getInstance().getMonthKLine(stockID);
            string targetMonth = convertMonthBySeason(season);
            StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);
            int quarter = int.Parse(season);
            if(rd != null && kl != null)
            {
                costRefVal = (rd.eps / kl.latestPrice) / quarter * 4;
            }

            return costRefVal;
        }

        public static double calcCurCostRefValue(string stockID, string year, string season)
        {
            double costRefVal = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, season);

            StockMarketData md = StockDataCenter.getInstance().getMarketData(stockID);
            int quarter = int.Parse(season);
            if (rd != null && md != null)
            {
                costRefVal = (rd.eps / md.latestPrice) / quarter * 4;
            }

            return costRefVal;
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            double srcVal = getSrcValue(stockID);
            double targetVal = calcCurCostRefValue(stockID, m_targetYear, m_targetSeason);

            val = 0.0;
            if (srcVal < double.Epsilon)
            {
                return false;
            }

            val = (targetVal - srcVal) / srcVal;
            return true;
        }
    }
}
