using StockAnalyzer.Common;
using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    class StockDataCenter : Singleton<StockDataCenter>
    {
        public StockMarketData getMarketData(string stockID)
        {
            String mdStr = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

            return md;
        }

        public StockFinanceData getFinanceData(string stockID)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            return fd;
        }

        public List<StockKLine> getMonthKLine(string stockID)
        {
            string str = StockDataCollector.queryMonthlyKLineData(stockID);
            List<StockKLine> mk = StockDataConvertor.parseKLineArray(str);

            return mk;
        }

        public List<StockKLine> getMonthKLineBaidu(string stockID, bool exRights = false)
        {
            string monthKStr = StockDataCollector.queryMonthlyKLineDataBaidu(stockID, exRights);
            List<StockKLine> monthKData = StockDataConvertor.parseKLineArrayBaidu(monthKStr);

            return monthKData;
        }

        public List<StockKLineBaidu> getKLineBaidu(string stockID)
        {
            string str = StockDataCollector.queryKLineDataBaidu(stockID);
            List<StockKLineBaidu> arr = StockDataConvertor.parseKLineArrayBaiduAdvanced(str);

            return arr;
        }
    }
}
