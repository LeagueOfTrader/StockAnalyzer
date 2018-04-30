using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.IntermediateProcedure
{
    class IntermediateImporter
    {
        public static List<string> readLowPriceStocks()
        {
            string lowPriceStocksFile = "Intermediate/low_stocks.txt";
            StreamReader sr = new StreamReader(lowPriceStocksFile);
            List<string> stocks = new List<string>();
            string stockID;
            while ((stockID = sr.ReadLine()) != null)
            {
                stocks.Add(stockID);
            }

            return stocks;
        }
    }
}
