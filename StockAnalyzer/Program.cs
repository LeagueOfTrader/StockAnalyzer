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

        static void Main(string[] args)
        {
            //string stockID = "sh600050";

            ////Console.WriteLine(md);
            ////dc.queryMarketDataAsync("sh600021", process);

            //string url = //"https://xueqiu.com/stock/forchartk/stocklist.json?symbol=SH600000&period=1month&type=before";
            //"https://gupiao.baidu.com/api/stocks/stockmonthbar?from=pc&os_ver=1&cuid=xxx&vv=100&format=json&stock_code=sh600000&step=3&start=&count=320&fq_type=front";
            //string ret = HttpUtilManager.getInstance().requestHttpPost(url, null);

            //String klStr = StockDataCollector.queryKLineData(stockID);
            //List<StockKLine> klData = StockDataConvertor.parseKLineArray(klStr);
            //String mdStr = StockDataCollector.queryMarketData(stockID);
            //StockMarketData md = StockDataConvertor.parseMarketData(mdStr);
            //Console.WriteLine(klStr);
            //Console.WriteLine(mdStr);

            StockPool.getInstance().init();

            List<string> shStocks = StockPool.getInstance().allSHStocks;

            filterStocksByPriceScaleAndPE(0.3, 40.0, "Intermediate/low_stocks.txt");

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
