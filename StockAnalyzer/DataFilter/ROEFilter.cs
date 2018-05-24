using StockAnalyzer.DataSource.TushareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class ROEFilter : NumericStockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected double m_roe = 20.0;

        public ROEFilter(string targetYear, string targetSeason, double roe)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_roe = roe;

            m_filterDesc = "ROE";
        }

        public override bool filterMethod(string stockID)
        {
            double roe = 0.0;
            if (getNumericValue(stockID, out roe))
            {
                if (roe >= m_roe)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            double roe = getStockROE(stockID, m_targetYear, m_targetSeason);
            if(roe > -double.MaxValue)
            {
                val = roe;
                return true;
            }

            val = 0.0;
            return false;
        }

        public static double getStockROE(string stockID, string year, string quarter)
        {
            StockReportData rd = StockDBVisitor.getInstance().getStockReportData(stockID, year, quarter);
            if (rd == null)
            {
                return -double.MaxValue;
            }

            return rd.roe;
        }
    }
}
