using StockAnalyzer.Assist;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSorter;
using StockAnalyzer.DataSource;
using StockAnalyzer.Global;
using StockAnalyzer.SelectionStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzerConsole
{
    class Program
    {
        static void outputSortData(List<StockSortableMetadata> stockDataList)
        {
            for (int i = 0; i < stockDataList.Count; i++)
            {
                Logger.log(stockDataList[i].stockID);
            }
        }

        static void showLowPositionCheapStocks()
        {
            LowLevelCheapPESelector s = new LowLevelCheapPESelector();
            List<string> stocks = s.screen();

            foreach (string stockCode in stocks)
            {
                Logger.log(stockCode);
            }
        }

        static void showHighCostPerfStocks()
        {
            HighCostPerfNotHighPosSelector s = new HighCostPerfNotHighPosSelector();
            List<string> stocks = s.screen();

            foreach (string stockCode in stocks)
            {
                Logger.log(stockCode);
            }
        }

        static double calcCostPerfInHistory(string stockID)
        {
            double curVal = CostPerfFilter.calcCurCostRefValue(stockID, "2018", "1");
            int maxYear = 0, maxQuarter = 0, maxYearAnl = 0;
            double refValByReport = CostPerfFilter.getMaxCostRefValueBefore(stockID, "2018", "1", out maxYear, out maxQuarter);
            double refValByAnnual = AnnualCostPerfFilter.getMaxAnnualCostRefValueBefore(stockID, "2018", out maxYearAnl);
            double refVal = Math.Max(refValByReport, refValByAnnual);
            double ratio = curVal / refVal;

            return ratio;
        }

        static void Main(string[] args)
        {
            GlobalConfig.getInstance().init();
            StockPool.getInstance().init();
            OptionalStocks.getInstance().init();

            //double ratio = calcCostPerfInHistory("sh603067");
            //double r2 = calcCostPerfInHistory("sh600995");

            //StockRealTimeData rd = StockDataCenter.getInstance().queryRealTimeData("sz000662");
            double pe = 0;
            DynamicPEFilter.calcDynamicPE("sz002551", out pe);

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

    }
}
