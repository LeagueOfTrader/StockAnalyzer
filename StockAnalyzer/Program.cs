using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer
{
    class Program
    {
        static void debugOutput(String info)
        {
            Console.WriteLine(info);
            System.Diagnostics.Debug.WriteLine(info);
        }

        static void filterStocksByPriceScaleAndPE(double ratio, double pe, string filepath) {
            List<string> stocks = filterStocksByPriceScaleAndPE(ratio, pe);

            if(stocks.Count == 0)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(filepath);
            foreach(string stock in stocks)
            {
                sw.WriteLine(stock);
            }

            sw.Close();
        }         

        static List<string> filterStocksByPriceScaleAndPE(double ratio, double pe) {
            List<string> stocks = new List<string>();

            List<string> shStocks = StockPool.getInstance().allSHStocks;
            foreach (string name in shStocks)
            {
                string stockID = "sh" + name;
                String mdStr = StockDataCollector.queryMarketData(stockID);
                StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

                if (md != null &&
                    md.PE <= pe &&
                    md.PE > 0 &&
                    PriceAnalyzer.isPriceScaleSatisfied(stockID, md.latestPrice, ratio))
                {
                    stocks.Add(stockID);
                    debugOutput(stockID);
                }
            }

            List<string> szStocks = StockPool.getInstance().allSZStocks;
            foreach (string name in szStocks)
            {
                string stockID = "sz" + name;
                String mdStr = StockDataCollector.queryMarketData(stockID);
                StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

                if (md != null &&
                    md.PE <= pe &&
                    md.PE > 0 &&
                    PriceAnalyzer.isPriceScaleSatisfied(stockID, md.latestPrice, ratio))
                {
                    stocks.Add(stockID);
                    debugOutput(stockID);
                }
            }

            return stocks;
        }

        List<string> readLowPriceStocks()
        {
            string lowPriceStocksFile = "Intermediate/low_stocks.txt";
            StreamReader sr = new StreamReader(lowPriceStocksFile);
            List<string> stocks = new List<string>();
            string stockID;
            while((stockID = sr.ReadLine()) != null){
                stocks.Add(stockID);
            }

            return stocks;
        }

        static void Main(string[] args)
        {
            //string url = "http://finance.sina.com.cn/realstock/company/sz000876/jsvar.js";

            //string ret = HttpUtilManager.getInstance().requestHttpGet(url, null);
            String str = StockDataCollector.queryFinanceDataSina("sz000876");
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            StockPool.getInstance().init();

            //List<string> shStocks = StockPool.getInstance().allSHStocks;

            //filterStocksByPriceScaleAndPE(0.3, 40.0, "Intermediate/low_stocks.txt");

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
