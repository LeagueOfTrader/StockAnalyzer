using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    class StockListFileUtil
    {
        public static List<string> readStocksFromFile(string filepath)
        {
            StreamReader sr = new StreamReader(filepath);
            List<string> stocks = new List<string>();
            string stockID;
            while ((stockID = sr.ReadLine()) != null)
            {
                stocks.Add(stockID);
            }

            return stocks;
        }

        public static void writeStocksToFile(List<string> stocks, string filepath)
        {
            if (stocks.Count == 0)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(filepath);
            foreach (string stock in stocks)
            {
                sw.WriteLine(stock);
            }

            sw.Close();
        }
    }
}
