using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class EPSPerfFilter : StockFilter
    {
        private double m_epsChgLimit = 0.2;
        public EPSPerfFilter(double epsChgLimit)
        {
            m_epsChgLimit = epsChgLimit;
        }

        public static double getEPSChanging(string stockID)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            double epsTTM = fd.eps4Quarter;
            double epsLastYear = fd.epsLastYear;

            //if(Math.Abs(epsLastYear) < double.Epsilon)
            //{
            //    return -1.0;
            //}

            return (epsTTM - epsLastYear) / epsLastYear;
        }

        public override bool filterMethod(string stockID)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            double epsTTM = fd.eps4Quarter;
            double epsLastYear = fd.epsLastYear;

            if(epsTTM < 0.0)
            {
                return false;
            }

            if (epsLastYear < 0.0 && epsTTM > double.Epsilon) {
                return true;
            }

            double epsChg = (epsTTM - epsLastYear) / epsLastYear;

            if (epsChg > m_epsChgLimit)
            {
                return true;
            }

            return false;
        }
    }
}
