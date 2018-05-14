using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class EPSPriceFilter : EPSPerfFilter
    {
        public EPSPriceFilter(double epsChgLimit) : base(epsChgLimit)
        {
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);
            str = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(str);

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
