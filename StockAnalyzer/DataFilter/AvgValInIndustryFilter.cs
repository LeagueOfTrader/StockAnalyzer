using StockAnalyzer.DataCache;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class AvgValInIndustryFilter : StockFilter
    {
        private NumericStockFilter m_comparer = null;
        private double m_ratio = 0.0;

        public AvgValInIndustryFilter(NumericStockFilter comparer, double ratio)
        {
            m_comparer = comparer;
            m_ratio = ratio;
        }

        public override bool filterMethod(string stockID)
        {
            double curVal = 0.0;
            if(!calcTargetVal(stockID, out curVal))
            {
                return false;
            }

            double avgVal = 0.0;
            if(!calcAvgValInIndustry(stockID, out avgVal))
            {
                return false;
            }

            double ratio = (curVal - avgVal) / avgVal;
            if(m_comparer.compareValueRatio(ratio, m_ratio))
            {
                return true;
            }

            return false;
        }

        public bool calcTargetVal(string stockID, out double val)
        {
            val = 0.0;
            if(m_comparer == null)
            {
                return false;
            }

            if(m_comparer.getNumericValue(stockID, out val))
            {
                return true;
            }

            return false;
        }

        public bool calcAvgValInIndustry(string stockID, out double val)
        {
            val = 0.0;
            if(m_comparer == null)
            {
                return false;
            }
           
            string industryName = StockPool.getInstance().getStockIndustry(stockID);
            string comparerName = m_comparer.getFilterDesc();
            bool ret = true;

            if(!StockDataCache.getInstance().getAvgValInIndustry(comparerName, industryName, out val))
            {
                double accumVal = 0.0;
                int accumCount = 0;

                List<string> stocksInIndustry = StockPool.getInstance().getStocksInIndustry(industryName);
                foreach (string code in stocksInIndustry)
                {
                    double curVal = 0.0;
                    if (m_comparer.getNumericValue(code, out curVal))
                    {
                        if(m_comparer.needLimitValue() && 
                            !m_comparer.isValueValid(curVal))
                        {
                            curVal = m_comparer.getLimitValue();
                        }
                        accumVal += curVal;
                        accumCount++;
                    }
                }

                if (accumCount > 0)
                {
                    val = accumVal / accumCount;
                    StockDataCache.getInstance().setAvgValInIndustry(comparerName, industryName, val);                    
                }
                else
                {
                    ret = false;
                }
            }

            return ret;
        }
    }
}
