using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class EPSPriceFilter : EPSPerfFilter
    {
        public EPSPriceFilter(double epsChgLimit) : base(epsChgLimit)
        {
            m_filterDesc = "EPSPrice";
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            StockFinanceData fd = StockDataCenter.getInstance().getFinanceData(stockID);
            StockMarketData md = StockDataCenter.getInstance().getMarketData(stockID);

            val = 0.0;
            if(fd == null || md == null)
            {
                return false;
            }

            double epsTTM = fd.eps4Quarter;
            double epsLastYear = fd.epsLastYear;

            if (epsTTM < 0.0)
            {
                return false;
            }

            if (epsLastYear < 0.0 && epsTTM > double.Epsilon)
            {
                return true;
            }

            val = (epsTTM - epsLastYear) / md.latestPrice;

            return false;
        }
    }
}
