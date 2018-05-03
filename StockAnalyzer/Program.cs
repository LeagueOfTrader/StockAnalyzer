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

            // #1
            //LowLevelCheapPESelector s = new LowLevelCheapPESelector();
            //List<string> stocks = s.screen();

            // #2
            //List<string> src = new List<string>();
            //src.Add("sh600097");
            //PEFilter peFilter = new PEFilter(40);
            //IndustryFilter indFilter = new IndustryFilter();
            //EPSPerfFilter epsFilter = new EPSPerfFilter(0.2);

            //List<string> r0 = peFilter.filter(src);
            //List<string> r1 = indFilter.filter(r0);

            //List<string> stocks = epsFilter.filter(r1);

            //foreach (string stockCode in stocks)
            //{
            //    Logger.debugOutput(stockCode);
            //}

            // #3
            string curYear = "2018";
            string curSeason = "1";
            string targetYear = "2013";
            string targetSeason = "1";
            string stc1 = "sh600995";
            string stc2 = "sz000883";
            double curVal1 = CostPerfFilter.calcCostRefValue(stc1, curYear, curSeason);
            double curVal2 = CostPerfFilter.calcCostRefValue(stc2, curYear, curSeason);
            double targVal1 = CostPerfFilter.calcCostRefValue(stc1, targetYear, targetSeason);
            double targVal2 = CostPerfFilter.calcCostRefValue(stc2, targetYear, targetSeason);
            double param = curVal1 / targVal1 - curVal2 / targVal2;

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
