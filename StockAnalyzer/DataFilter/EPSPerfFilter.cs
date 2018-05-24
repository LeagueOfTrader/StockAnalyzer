using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class EPSPerfFilter : NumericStockFilter
    {
        private double m_epsChgLimit = 0.2;
        public EPSPerfFilter(double epsChgLimit)
        {
            m_epsChgLimit = epsChgLimit;
            m_filterDesc = "EPSPerf";
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
            double epsChg = 0.0;
            bool ret = getNumericValue(stockID, out epsChg);

            if (!ret)
            {
                return false;
            }

            if (epsChg > m_epsChgLimit)
            {
                return true;
            }

            return false;
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            val = 0.0;
            if(fd == null)
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

            val = (epsTTM - epsLastYear) / epsLastYear;

            return false;
        }

    }
}
