using StockAnalyzer.Common;
using StockAnalyzer.DataModel;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    public class StockDataCenter : Singleton<StockDataCenter>
    {
        Dictionary<string, StockRealTimeData> m_realTimeData = new Dictionary<string, StockRealTimeData>();
        Dictionary<string, StockMarketDataUpdater> m_dataUpdaters = new Dictionary<string, StockMarketDataUpdater>();

        public StockMarketData getMarketData(string stockID)
        {
            String mdStr = StockDataCollector.queryMarketData(stockID);
            StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

            return md;
        }

        public StockRealTimeData getMarketRealTimeData(string stockID)
        {
            if (m_realTimeData.ContainsKey(stockID))
            {
                return m_realTimeData[stockID];
            }

            return null;
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
            if(str == null || str.Length == 0)
            {
                return null;
            }

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

        public StockRealTimeData queryRealTimeData(string stockID)
        {
            String str = StockDataCollector.queryMarketRealTimeDataSina(stockID);
            StockRealTimeData rd = StockDataConvertor.parseRealTimeData(str);
            if (rd != null)
            {
                rd.stockCode = StockIDUtil.getPureCode(stockID);
            }
            return rd;
        }

        public void assignRealTimeData(string stockID, StockRealTimeData data)
        {
            m_realTimeData[stockID] = data;
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
