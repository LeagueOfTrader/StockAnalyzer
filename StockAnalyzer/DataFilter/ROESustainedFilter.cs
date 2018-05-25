using StockAnalyzer.DataSource.TushareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class ROESustainedFilter : ROEFilter
    {
        protected int m_continuousYear = 3;


        public ROESustainedFilter(string targetYear, string targetSeason, double roe, int continuousYear = 3) 
                : base(targetYear, targetSeason, roe)
        {            
            m_continuousYear = continuousYear;
        }

        public override bool filterMethod(string stockID)
        {
            double roe = getStockROE(stockID, m_targetYear, m_targetSeason);
            if(roe < m_roe)
            {
                return false;
            }

            int ty = int.Parse(m_targetYear);
            int n = m_continuousYear - 1;
            while(n > 0)
            {
                int year = ty - n;
                roe = getStockROE(stockID, year.ToString(), "4");
                if(roe < m_roe)
                {
                    return false;
                }
                n--;
            }

            return true;
        }
    }
}
