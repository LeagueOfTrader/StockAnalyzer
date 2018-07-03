using StockAnalyzer.Assist;
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
    public class AnnualPBCostFilter : NumericStockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected double m_ratio = 0.0;

        public AnnualPBCostFilter(string targetYear, string targetSeason, double ratio)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_ratio = ratio;

            m_filterDesc = "PBCostPerf";
        }

        public override bool filterMethod(string stockID)
        {
            double curVal = calcPBCostValue(stockID, m_targetYear, m_targetSeason);
            int maxYear = 0;
            double histVal = getMinAnnualPBCostValueBefore(stockID, m_targetYear, out maxYear);
            if(histVal < double.Epsilon)
            {
                return false;
            }

            double ratio = (histVal - curVal) / histVal;
            if(ratio < m_ratio)
            {
                return false;
            }

            return true;
        }

        public static double calcPBCostValue(string stockID, string year, string quarter)
        {
            double costRefVal = 0.0;
            double pe = 0.0;
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, quarter);
            if (rd == null)
            {
                return double.MaxValue;
            }

            List<StockKLine> mk = StockDataCenter.getInstance().getMonthKLine(stockID);
            if (mk == null)
            {
                return double.MaxValue;
            }

            string targetMonth = convertMonthBySeason(quarter);
            StockKLine kl = StockDataUtil.getMonthKLineByYearMonth(mk, year, targetMonth);
            if(kl == null)
            {
                return double.MaxValue;
            }
            pe = kl.latestPrice / rd.eps;

            StockProfitData pd = StockDBVisitor.getInstance().getStockProfitData(stockID, year, quarter);
            if(pd == null)
            {
                return double.MaxValue;
            }

            costRefVal = pe * pd.roe;

            return costRefVal;
        }

        public static double getMinAnnualPBCostValueBefore(string stockID, string year, out int minYear)
        {
            int endYear = int.Parse(year);
            minYear = 0;// endYear;
            double minVal = double.MaxValue;
            int startYear = 2013;
            for (int i = startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                double val = calcPBCostValue(stockID, yr, "4");
                if (val < minVal)
                {
                    minVal = val;
                    minYear = i;
                }
            }


            Logger.log("Best annual pb cost for " + stockID + " before " + year + ": " + minYear);
            return minVal;
        }

        public override bool getNumericValue(string stockID, out double val)
        {            
            int maxYear = 0;
            double srcVal = getMinAnnualPBCostValueBefore(stockID, m_targetYear, out maxYear);
            double targetVal = calcPBCostValue(stockID, m_targetYear, m_targetSeason);

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
