using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter.DataCache
{
    class StockDataCache : Singleton<StockDataCache>
    {
        Dictionary<string, double> m_annualCostPerfCache = new Dictionary<string, double>();
        Dictionary<string, string> m_bestAnnualCostPerfYear = new Dictionary<string, string>();

        Dictionary<string, double> m_dynamicCostPerfCache = new Dictionary<string, double>();
        Dictionary<string, string> m_bestDynamicCostPerfQuarter = new Dictionary<string, string>();

        Dictionary<string, double> m_quarterCostPerfCache = new Dictionary<string, double>();
        Dictionary<string, string> m_bestQuarterCostPerfQuarter = new Dictionary<string, string>();

        Dictionary<string, double> m_yoyCostPerfCache = new Dictionary<string, double>();
        Dictionary<string, string> m_bestYoyCostPerfQuarter = new Dictionary<string, string>();

        Dictionary<string, Dictionary<string, double>> m_avgValInIndustry = new Dictionary<string, Dictionary<string, double>>();

        public double getMaxAnnualCostRefValueBefore(string stockID, string year)
        {
            string cacheID = makeUpCacheID(stockID, year);
            if (!m_annualCostPerfCache.ContainsKey(cacheID))
            {
                int maxYear = 0;
                double maxAnnualCostRefVal = AnnualCostPerfFilter.getMaxAnnualCostRefValueBefore(stockID, year, out maxYear);
                m_annualCostPerfCache.Add(cacheID, maxAnnualCostRefVal);
                m_bestAnnualCostPerfYear.Add(stockID, maxYear.ToString());
            }

            return m_annualCostPerfCache[cacheID];
        }

        public string getBestAnnualCostYear(string stockID, string year)
        {
            string cacheID = makeUpCacheID(stockID, year);
            if (m_bestAnnualCostPerfYear.ContainsKey(cacheID))
            {
                return m_bestAnnualCostPerfYear[cacheID];
            }

            return "NA";
        }

        public double getMaxCostRefValueBefore(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (!m_dynamicCostPerfCache.ContainsKey(cacheID))
            {
                int maxYear = 0, maxQuarter = 0;
                double maxCostRefVal = CostPerfFilter.getMaxCostRefValueBefore(stockID, year, quarter, out maxYear, out maxQuarter);
                m_dynamicCostPerfCache.Add(cacheID, maxCostRefVal);
                m_bestDynamicCostPerfQuarter.Add(stockID, maxYear.ToString() + "q" + maxQuarter.ToString());
            }

            return m_dynamicCostPerfCache[cacheID];
        }

        public string getBestDynamicCostQuarter(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (m_bestDynamicCostPerfQuarter.ContainsKey(cacheID))
            {
                return m_bestDynamicCostPerfQuarter[cacheID];
            }

            return "NA";
        }

        public double getMaxQuarterCostRefValueBefore(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (!m_quarterCostPerfCache.ContainsKey(cacheID))
            {
                int maxYear = 0, maxQuarter = 0;
                double maxCostRefVal = QuarterCostPerfFilter.getMaxQuarterCostRefValueBefore(stockID, year, quarter, out maxYear, out maxQuarter);
                m_quarterCostPerfCache.Add(cacheID, maxCostRefVal);
                m_bestQuarterCostPerfQuarter.Add(stockID, maxYear.ToString() + "q" + maxQuarter.ToString());
            }

            return m_quarterCostPerfCache[cacheID];
        }

        public string getBestQuarterCostQuarter(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (m_bestQuarterCostPerfQuarter.ContainsKey(cacheID))
            {
                return m_bestQuarterCostPerfQuarter[cacheID];
            }

            return "NA";
        }

        public double getMaxYoyCostRefValueBefore(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (!m_yoyCostPerfCache.ContainsKey(cacheID))
            {
                int maxYear = 0;
                double maxCostRefVal = CostPerfYoyFilter.getMaxCostYoyRefValueBefore(stockID, year, quarter, out maxYear);
                m_yoyCostPerfCache.Add(cacheID, maxCostRefVal);
                m_bestYoyCostPerfQuarter.Add(stockID, maxYear.ToString() + "q" + quarter.ToString());
            }

            return m_yoyCostPerfCache[cacheID];
        }

        public string getBestYoyCostQuarter(string stockID, string year, string quarter)
        {
            string cacheID = makeUpCacheID(stockID, year, quarter);
            if (m_bestYoyCostPerfQuarter.ContainsKey(cacheID))
            {
                return m_bestYoyCostPerfQuarter[cacheID];
            }

            return "NA";
        }

        private string makeUpCacheID(string stockID, string year, string quarter = "")
        {
            string cacheID = stockID + year;
            if(quarter.Length > 0)
            {
                cacheID += "q" + quarter;
            }
            return cacheID;
        }

        public void setAvgValInIndustry(string comparerName, string industryName, double avgVal)
        {
            if (!m_avgValInIndustry.ContainsKey(comparerName))
            {
                Dictionary<string, double> avgValMap = new Dictionary<string, double>();
                m_avgValInIndustry.Add(comparerName, avgValMap);
            }

            if (!m_avgValInIndustry[comparerName].ContainsKey(industryName))
            {
                m_avgValInIndustry[comparerName].Add(industryName, avgVal);
            }
            else
            {
                m_avgValInIndustry[comparerName][industryName] = avgVal;
            }
        }

        public bool getAvgValInIndustry(string comparerName, string industryName, out double avgVal)
        {
            bool ret = false;
            avgVal = 0.0;

            if (m_avgValInIndustry.ContainsKey(comparerName))
            {
                if (m_avgValInIndustry[comparerName].ContainsKey(industryName))
                {
                    avgVal = m_avgValInIndustry[comparerName][industryName];
                }
            }

            return ret;
        }
    }
}
