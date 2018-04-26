using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer
{
    class Program
    {
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

            foreach (string name in shStocks)
            {
                string stockID = "sh" + name;
                string monthKStr = StockDataCollector.queryMonthlyKLineDataBaidu(stockID);
                List<StockKLine> monthKData = StockDataConvertor.parseKLineArrayBaidu(monthKStr);
                String mdStr = StockDataCollector.queryMarketData(stockID);
                StockMarketData md = StockDataConvertor.parseMarketData(mdStr);
                double ratio = PriceAnalyzer.getPriceScale(monthKData, md.latestPrice, "20120101");
                if (ratio < 0.3 && ratio > 0.0 && md.latestPrice > 0.0)
                {
                    Console.WriteLine(stockID + " : " + ratio);
                }
            }

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
