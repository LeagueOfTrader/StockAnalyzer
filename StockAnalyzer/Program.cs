using StockAnalyzer.Assist;
using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.Integration;
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
        static void Main(string[] args)
        {
            StockPool.getInstance().init();

            List<string> stocks = StockDataIntegration.filterStocksByEPSinLowPriceInstruments(0.2);

            foreach(string stockCode in stocks)
            {
                Logger.debugOutput(stockCode);
            }

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
