using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class PEFilter : NumericStockFilter
    {
        private double m_limitPE = 40.0;

        public PEFilter(double limitPE)
        {
            m_limitPE = limitPE;

            m_filterDesc = "StaticPE";
        }

        public override bool filterMethod(string stockID)
        {
            double pe = 0.0;
            bool ret = getNumericValue(stockID, out pe);
            if (!ret)
            {
                return false;
            }

            if (pe <= m_limitPE &&
                pe > 0)
            {
                return true;
            }

            return false;
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            StockMarketData md = StockDataCenter.getInstance().getMarketData(stockID);

            val = 0.0;
            if (md != null)
            {
                val = md.PE;
                return true;
            }

            return false;
        }
    }
}
