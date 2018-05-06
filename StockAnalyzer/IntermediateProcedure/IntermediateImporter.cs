using StockAnalyzer.Util;
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
            return StockListFileUtil.readStocksFromFile(lowPriceStocksFile);
        }

        public static List<string> readCheapStocks()
        {
            string cheapStocksFile = "Intermediate/cheap_stocks.txt";
            return StockListFileUtil.readStocksFromFile(cheapStocksFile);
        }
    }
}
