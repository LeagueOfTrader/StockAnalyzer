using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class PEFilter : StockFilter
    {
        private double m_limitPE = 40.0;

        public PEFilter(double limitPE)
        {
            m_limitPE = limitPE;
        }

        public override bool filterMethod(string stockID)
        {
            String mdStr = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

            if (md != null &&
                md.PE <= m_limitPE &&
                md.PE > 0)
            {
                return true;
            }

            return false;
        }
    }
}
