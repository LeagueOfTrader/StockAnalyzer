using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string stockID = "sh600050";

            //Console.WriteLine(md);
            //dc.queryMarketDataAsync("sh600021", process);



            String klStr = StockDataCollector.queryKLineData(stockID);
            List<StockKLine> klData = StockDataConvertor.parseKLineArray(klStr);
            String mdStr = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(mdStr);
            Console.WriteLine(klStr);
            Console.WriteLine(mdStr);
        }


    }
}
