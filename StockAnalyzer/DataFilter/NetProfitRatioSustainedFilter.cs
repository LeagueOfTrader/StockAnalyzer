using StockAnalyzer.DataSource.TushareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class NetProfitRatioSustainedFilter : NetProfitRatioFilter
    {
        protected int m_continuousYear = 3;

        public NetProfitRatioSustainedFilter(string targetYear, string targetSeason, double npr, int continuousYear = 3)
                : base(targetYear, targetSeason, npr)
        {
            m_continuousYear = continuousYear;
        }

        public override bool filterMethod(string stockID)
        {
            double npr = getStockNetProfitRatio(stockID, m_targetYear, m_targetSeason);
            if (npr < m_netProfitRatio)
            {
                return false;
            }

            int ty = int.Parse(m_targetYear);
            int n = m_continuousYear - 1;
            while (n > 0)
            {
                int year = ty - n;
                npr = getStockNetProfitRatio(stockID, year.ToString(), "4");
                if (npr < m_netProfitRatio)
                {
                    return false;
                }
                n--;
            }

            return true;
        }
        
    }
}
