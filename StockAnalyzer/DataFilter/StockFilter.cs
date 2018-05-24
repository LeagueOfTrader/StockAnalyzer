using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.Assist;
using System.IO;

namespace StockAnalyzer.DataFilter
{
    public abstract class StockFilter : IStockFilter
    {
        protected const int m_startYear = 2007;

        public List<string> filter(List<string> src)
        {
            List<string> target = new List<string>();

            foreach (string stockID in src)
            {
                if (filterMethod(stockID))
                {
                    target.Add(stockID);
                }
            }

            return target;
        }

        public abstract bool filterMethod(string stockID);

        protected static string convertMonthBySeason(string season)
        {
            string month = "12";
            if (season == "1")
            {
                month = "3";
            }
            else if (season == "2")
            {
                month = "6";
            }
            else if (season == "3")
            {
                month = "9";
            }

            return month;
        }
    }
}
