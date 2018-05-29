using StockAnalyzer.Common;
using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    public class StockDataCenter : Singleton<StockDataCenter>
    {
        Dictionary<string, StockMarketData> m_marketData = new Dictionary<string, StockMarketData>();
        Dictionary<string, StockMarketDataUpdater> m_dataUpdaters = new Dictionary<string, StockMarketDataUpdater>();

        public StockMarketData getMarketData(string stockID)
        {
            if (m_marketData.ContainsKey(stockID))
            {
                return m_marketData[stockID];
            }

            return queryMarketData(stockID);
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

        public StockMarketData queryMarketData(string stockID)
        {
            String mdStr = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

            return md;
        }

        public void assignMarketData(string stockID, StockMarketData data)
        {
            m_marketData[stockID] = data;
        }

        public void subscribeMarketData(string stockID)
        {
            if (!m_dataUpdaters.ContainsKey(stockID))
            {
                StockMarketDataUpdater updater = new StockMarketDataUpdater(stockID);
                m_dataUpdaters.Add(stockID, updater);
                updater.start();
            }
        }

        public void unsubscribeMarketData(string stockID)
        {
            if (m_dataUpdaters.ContainsKey(stockID))
            {
                m_dataUpdaters[stockID].stop();
                m_dataUpdaters.Remove(stockID);
            }
        }
    }
}
