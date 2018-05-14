using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class AvgValInIndustryFilter : StockFilter
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
            if(ratio > m_ratio)
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

            double accumVal = 0.0;
            int accumCount = 0;
            string industryName = StockPool.getInstance().getStockIndustry(stockID);
            List<string> stocksInIndustry = StockPool.getInstance().getStocksInIndustry(industryName);
            foreach(string code in stocksInIndustry)
            {
                double curVal = 0.0;
                if(m_comparer.getNumericValue(code, out curVal))
                {
                    accumVal += curVal;
                    accumCount++;
                }
            }

            if(accumCount > 0)
            {
                val = accumVal / accumCount;
                return true;
            }

            return false;
        }
    }
}
