using StockAnalyzer.Assist;
using StockAnalyzer.DataCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class CostPerfYoyFilter : CostPerfFilter
    {
        public CostPerfYoyFilter(string targetYear, string targetSeason, double ratio) : base(targetYear, targetSeason, ratio)
        {
            m_filterDesc = "YoyCostPerf";
        }

        protected override double getSrcValue(string stockID)
        {
            //int maxYear = 0;
            //double srcVal = getMaxCostYoyRefValueBefore(stockID, m_targetYear, m_targetSeason, out maxYear);
            double srcVal = StockDataCache.getInstance().getMaxYoyCostRefValueBefore(stockID, m_targetYear, m_targetSeason);
            return srcVal;
        }

        public static double getMaxCostYoyRefValueBefore(string stockID, string year, string quarter, out int maxYear)
        {
            int endYear = int.Parse(year);
            maxYear = 0;// endYear;
            double maxVal = 0.0;
            for (int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                double val = calcCostRefValue(stockID, yr, quarter);
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = i;
                }
            }


            Logger.log("Best yoy cost for " + stockID + " before " + year + "Q" + quarter + ": " + maxYear + "Q" + quarter);
            return maxVal;
        }
    }
}
