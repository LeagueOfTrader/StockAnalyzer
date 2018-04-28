using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Integration
{
    class StockDataIntegration
    {
        public static void filterStocksByPriceScaleAndPE(double ratio, double pe, string filepath)
        {
            List<string> stocks = new List<string>();
            List<string> shStocks = StockFilter.filterStocksByPriceScaleAndPE(StockPool.getInstance().allSHStocks, ratio, pe);
            List<string> szStocks = StockFilter.filterStocksByPriceScaleAndPE(StockPool.getInstance().allSZStocks, ratio, pe);

            stocks.AddRange(shStocks);
            stocks.AddRange(szStocks);

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

        public static List<string> getLowPriceStocksWithoutST()
        {
            List<string> lowStocks = readLowPriceStocks();
            List<string> lowStocksWithoutST = new List<string>();
            foreach(string stockID in lowStocks)
            {
                if (StockFilter.isSTStock(stockID))
                {
                    continue;
                }

                lowStocksWithoutST.Add(stockID);
            }

            return lowStocksWithoutST;
        }

        public static List<string> filterStocksByEPSinLowPriceInstruments(double epsChgLimit)
        {
            List<string> lowStocks = getLowPriceStocksWithoutST();

            return StockFilter.filterStocksByEPSPeformance(lowStocks, epsChgLimit);
        }
    }
}
