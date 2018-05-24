using StockAnalyzer.Assist;
using StockAnalyzer.DataFilter.DataCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class AnnualCostPerfFilter : CostPerfFilter
    {
        public AnnualCostPerfFilter(string targetYear, string targetSeason, double ratio) : base(targetYear, targetSeason, ratio)
        {
            m_filterDesc = "AnnualCostPerf";
        }

        protected override double getSrcValue(string stockID)
        {
            //int maxYear = 0;
            //double srcVal = getMaxAnnualCostRefValueBefore(stockID, m_targetYear, out maxYear);
            double srcVal = StockDataCache.getInstance().getMaxAnnualCostRefValueBefore(stockID, m_targetYear);
            return srcVal;
        }

        public static double getMaxAnnualCostRefValueBefore(string stockID, string year, out int maxYear)
        {
            int endYear = int.Parse(year);
            maxYear = 0;// endYear;
            double maxVal = 0.0;
            for (int i = m_startYear; i < endYear; i++)
            {
                string yr = i.ToString();
                double val = calcCostRefValue(stockID, yr, "4");
                if (val > maxVal)
                {
                    maxVal = val;
                    maxYear = i;
                }                
            }
           

            Logger.log("Best annual cost for " + stockID + " before " + year + ": " + maxYear);
            return maxVal;
        }
    }
}
