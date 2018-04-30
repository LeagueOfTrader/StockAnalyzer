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
    abstract class StockFilter : IStockFilter
    {
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
    }
}
