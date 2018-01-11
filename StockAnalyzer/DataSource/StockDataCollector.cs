using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockAnalyzer.Util.HttpAsyncReq;

namespace StockAnalyzer.DataSource
{
    class StockDataCollector
    {
        static string m_marketDataUrl = "http://qt.gtimg.cn/q=";
        static string m_depthDataUrl = "http://qt.gtimg.cn/q=s_pk";
        static string m_fundFlowUrl = "http://qt.gtimg.cn/q=ff_";

        static string m_timeLineUrl = "http://data.gtimg.cn/flashdata/hushen/minute/{0}.js";
        static string m_kLineUrl = "http://data.gtimg.cn/flashdata/hushen/latest/daily/{0}.js";
        static string m_weeklyKLineUrl = "http://data.gtimg.cn/flashdata/hushen/latest/weekly/{0}.js";
        static string m_monthlyKLineUrl = "http://data.gtimg.cn/flashdata/hushen/monthly/{0}.js";

        private static String httpGet(string url)
        {
            return HttpUtilManager.getInstance().requestHttpGet(url, null);
        }

        // info

        public static String queryMarketData(string stockID)
        {
            return httpGet(m_marketDataUrl + stockID);
        }

        public static String queryDepthData(string stockID)
        {
            return httpGet(m_depthDataUrl + stockID);
        } 

        public static String queryFundFlowData(string stockID)
        {
            return httpGet(m_fundFlowUrl + stockID);
        }

        // transaction data
        public static String queryTimeLineData(string stockID)
        {
            return httpGet(String.Format(m_timeLineUrl, stockID));
        }

        public static String queryKLineData(string stockID) //daily k line
        {
            return httpGet(String.Format(m_kLineUrl, stockID));
        }

        public static String queryWeeklyKLineData(string stockID) //weekly k line
        {
            return httpGet(String.Format(m_weeklyKLineUrl, stockID));
        }

        public static String queryMonthlyKLineData(string stockID) //monthly k line
        {
            return httpGet(String.Format(m_monthlyKLineUrl, stockID));
        }
    }
}
