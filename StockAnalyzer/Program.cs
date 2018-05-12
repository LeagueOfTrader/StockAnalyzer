using StockAnalyzer.Assist;
using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataAnalyze.Pattern;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSorter;
using StockAnalyzer.DataSource;
using StockAnalyzer.Global;
using StockAnalyzer.IntermediateProcedure;
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
        static void outputSortData(List<StockSortableMetadata> stockDataList)
        {
            for(int i = 0; i < stockDataList.Count; i++)
            {
                Logger.log(stockDataList[i].stockID);
            }
        }

        static void Main(string[] args)
        {
            GlobalConfig.getInstance().init();
            StockPool.getInstance().init();
            OptionalStocks.getInstance().init();

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
            //string curYear = "2018";
            //string curSeason = "1";
            ////string targetYear = "2013";
            ////string targetSeason = "1";
            //string stc1 = "sh600995";
            //string stc2 = "sz000883";
            //double curVal1 = CostPerfFilter.calcCostRefValue(stc1, curYear, curSeason);
            //double curVal2 = CostPerfFilter.calcCostRefValue(stc2, curYear, curSeason);
            ////double targVal1 = CostPerfFilter.calcCostRefValue(stc1, targetYear, targetSeason);
            ////double targVal2 = CostPerfFilter.calcCostRefValue(stc2, targetYear, targetSeason);
            ////double param = curVal1 / targVal1 - curVal2 / targVal2;
            //double histVal1 = CostPerfFilter.getMaxCostRefValueBefore(stc1, curYear, curSeason);
            //double histVal2 = CostPerfFilter.getMaxCostRefValueBefore(stc2, curYear, curSeason);

            //double param = curVal1 / histVal1 - curVal2 / histVal2;

            // #4
            //HighCostPerfNotHighPosSelector s = new HighCostPerfNotHighPosSelector();            
            //List<string> stocks = s.screen();

            // #5
            //List<string> src = IntermediateImporter.readCheapStocks();
            //List<StockSortableMetadata> target = new List<StockSortableMetadata>();
            //foreach(string stockID in src)
            //{
            //    if (IndustryFilter.isStockInIndustry(stockID, "银行"))
            //    {
            //        continue;
            //    }
            //    StockSortableMetadata sd = new SSMDCostPerf(stockID);
            //    target.Add(sd);
            //}
            //target.Sort();

            //List<string> stocks = new List<string>();
            //for(int i = target.Count - 1; i >= 0; i--)
            //{
            //    stocks.Add(target[i].stockID);
            //}

            //foreach (string stockCode in stocks)
            //{
            //    Logger.debugOutput(stockCode);
            //}

            // #6
            //string stockID = "sh600352";
            //double curVal = CostPerfFilter.calcCurCostRefValue(stockID, "2018", "1");
            //double refValByReport = CostPerfFilter.getMaxCostRefValueBefore(stockID, "2018", "1");
            //double refValByAnnual = AnnualCostPerfFilter.getMaxAnnualCostRefValueBefore(stockID, "2018");
            //double refVal = Math.Max(refValByReport, refValByAnnual);
            //double ratio = curVal / refVal;

            // #7
            //List<string> selfSelectedList = OptionalStocks.getInstance().optionalStockList;
            //List<StockSortableMetadata> selfSelectSortByQuarter = new List<StockSortableMetadata>();
            //List<StockSortableMetadata> selfSelectSortByYoy = new List<StockSortableMetadata>();
            //List<StockSortableMetadata> selfSelectSortByNormal = new List<StockSortableMetadata>();
            //List<StockSortableMetadata> selfSelectSortByAnnual = new List<StockSortableMetadata>();
            //foreach (string stockCode in selfSelectedList)
            //{
            //    StockSortableMetadata sdn = new SSMDCostPerf(stockCode);
            //    StockSortableMetadata sdq = new SSMDQuarterCostPerf(stockCode);
            //    StockSortableMetadata sdy = new SSMDCostPerf(stockCode);
            //    StockSortableMetadata sda = new SSMDAnnualCostPerf(stockCode);
            //    selfSelectSortByQuarter.Add(sdq);
            //    selfSelectSortByNormal.Add(sdn);
            //    selfSelectSortByYoy.Add(sdy);
            //    selfSelectSortByAnnual.Add(sda);
            //}

            //selfSelectSortByQuarter.Sort();
            //selfSelectSortByQuarter.Reverse();
            //selfSelectSortByNormal.Sort();
            //selfSelectSortByNormal.Reverse();
            //selfSelectSortByYoy.Sort();
            //selfSelectSortByYoy.Reverse();
            //selfSelectSortByAnnual.Sort();
            //selfSelectSortByAnnual.Reverse();

            //Logger.log("Normal sort: ");
            //outputSortData(selfSelectSortByNormal);
            //Logger.log("Quarter sort: ");
            //outputSortData(selfSelectSortByQuarter);
            //Logger.log("Yoy sort: ");
            //outputSortData(selfSelectSortByYoy);
            //Logger.log("Annual sort: ");
            //outputSortData(selfSelectSortByAnnual);

            // #8
            //List<string> src = IntermediateImporter.readMidRepGrowthStocks();
            //AnnualCostPerfFilter acpFilter = new AnnualCostPerfFilter("2018", "1", 0.0);
            //PriceScaleFilter pcFilter = new PriceScaleFilter(0.5);
            //List<string> stocks = pcFilter.filter(acpFilter.filter(src));
            //List<StockSortableMetadata> arr = new List<StockSortableMetadata>();
            //foreach(string code in stocks)
            //{
            //    StockSortableMetadata sda = new SSMDAnnualCostPerf(code);
            //    arr.Add(sda);
            //}
            //arr.Sort();
            //outputSortData(arr);

            // #9
            //OversoldSelector s = new OversoldSelector();
            //List<string> stocks = s.screen();
            //List<StockSortableMetadata> stocksSortByAnnual = new List<StockSortableMetadata>();
            //foreach (string stockCode in stocks)
            //{
            //    StockSortableMetadata sda = new SSMDAnnualCostPerf(stockCode);
            //    stocksSortByAnnual.Add(sda);
            //}
            //stocksSortByAnnual.Sort();
            //outputSortData(stocksSortByAnnual);
            string stockID = "sh600703";
            string str = StockDataCollector.queryKLineDataBaidu(stockID);
            List<StockKLineBaidu> arr = StockDataConvertor.parseKLineArrayBaiduAdvanced(str);
            int endIndex = StockDataUtil.getIndexByDate(arr, "20180426");
            List<StockKLineBaidu> subArr = arr.GetRange(0, endIndex + 1);
            Oversold2Day osp = new Oversold2Day();
            bool ret = osp.isMatch(subArr);

            while (true) {
                Thread.Sleep(1000);
            }
        }

    }
}
