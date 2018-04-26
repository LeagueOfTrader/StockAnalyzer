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

        //static string m_kLineXqUrl = "https://xueqiu.com/stock/forchartk/stocklist.json?symbol={0}&period={1}&type={2}";

        static string m_kLineUrlBaidu = "https://gupiao.baidu.com/api/stocks/stock{1}bar?from=pc&os_ver=1&cuid=xxx&vv=100&format=json&stock_code={0}&step=3&start=&count=320&fq_type={2}";

        private static String httpGet(string url)
        {
            return HttpUtilManager.getInstance().requestHttpGet(url, null);
        }

        private static String httpPost(string url, Dictionary<string, string> param) {
            return HttpUtilManager.getInstance().requestHttpPost(url, param);
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

        //public static String queryKLineDataXq(string stockID, bool exRights = false) //daily k line
        //{
        //    //return httpGet(String.Format(m_kLineUrl, stockID));
        //    string rightsSymbol = "before";
        //    if (exRights)
        //    {
        //        rightsSymbol = "after";
        //    }
        //    //string url = String.Format(m_kLineXqUrl, stockID, "1day", rightsSymbol);
        //    Dictionary<string, string> paras = new Dictionary<string, string>();
        //    paras.Add("symbol", stockID);
        //    paras.Add("period", "1day");
        //    paras.Add("type", rightsSymbol);

        //    return httpPost(m_kLineXqUrl, paras);
        //}

        //public static String queryWeeklyKLineDataXq(string stockID, bool exRights = false) //weekly k line
        //{
        //    //return httpGet(String.Format(m_weeklyKLineUrl, stockID));
        //    string rightsSymbol = "before";
        //    if (exRights)
        //    {
        //        rightsSymbol = "after";
        //    }
        //    //string url = String.Format(m_kLineXqUrl, stockID, "1week", rightsSymbol);

        //    Dictionary<string, string> paras = new Dictionary<string, string>();
        //    paras.Add("symbol", stockID);
        //    paras.Add("period", "1week");
        //    paras.Add("type", rightsSymbol);

        //    return httpPost(m_kLineXqUrl, paras);
        //}

        //public static String queryMonthlyKLineDataXq(string stockID, bool exRights = false) //monthly k line
        //{
        //    //return httpGet(String.Format(m_monthlyKLineUrl, stockID));
        //    string rightsSymbol = "before";
        //    if (exRights)
        //    {
        //        rightsSymbol = "after";
        //    }

        //    Dictionary<string, string> paras = new Dictionary<string, string>();
        //    paras.Add("symbol", stockID);
        //    paras.Add("period", "1month");
        //    paras.Add("type", rightsSymbol);

        //    //return httpPost(m_kLineXqUrl, paras);
        //    string url = String.Format(m_kLineXqUrl, stockID, "1month", rightsSymbol);

        //    return httpPost(url, null);
        //}

        public static String queryKLineDataBaidu(string stockID, bool exRights = false) //daily k line
        {
            string rightsSymbol = "front";
            if (exRights)
            {
                rightsSymbol = "no";
            }
            string url = String.Format(m_kLineUrlBaidu, stockID, "day", rightsSymbol);

            return httpPost(url, null);
        }

        public static String queryWeeklyKLineDataBaidu(string stockID, bool exRights = false) //weekly k line
        {
            string rightsSymbol = "front";
            if (exRights)
            {
                rightsSymbol = "no";
            }
            string url = String.Format(m_kLineUrlBaidu, stockID, "week", rightsSymbol);

            return httpPost(url, null);
        }

        public static String queryMonthlyKLineDataBaidu(string stockID, bool exRights = false) //monthly k line
        {
            string rightsSymbol = "front";
            if (exRights)
            {
                rightsSymbol = "no";
            }
            string url = String.Format(m_kLineUrlBaidu, stockID, "month", rightsSymbol);

            return httpPost(url, null);
        }
    }
}
