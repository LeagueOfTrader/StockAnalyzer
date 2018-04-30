using StockAnalyzer.Assist;
using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.SelectionStrategy;
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

            //LowLevelCheapPESelector s = new LowLevelCheapPESelector();

            //List<string> stocks = s.screen();

            List<string> src = new List<string>();
            src.Add("sh600097");
            PEFilter peFilter = new PEFilter(40);
            IndustryFilter indFilter = new IndustryFilter();
            EPSPerfFilter epsFilter = new EPSPerfFilter(0.2);

            List<string> r0 = peFilter.filter(src);
            List<string> r1 = indFilter.filter(r0);

            List<string> stocks = epsFilter.filter(r1);

            foreach (string stockCode in stocks)
            {
                Logger.debugOutput(stockCode);
            }

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
