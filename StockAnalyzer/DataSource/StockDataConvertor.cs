using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    class StockDataConvertor
    {
        public static StockMarketData parseMarketData(String str)
        {
            if(str == null)
            {
                return null;
            }

            string[] arr = str.Split('~');

            if(arr.Length < 50)
            {
                return null;
            }
            try
            {
                StockMarketData md = new StockMarketData();
                md.stockName = arr[1];
                md.stockCode = arr[2];
                md.latestPrice = double.Parse(arr[3]);
                md.closePriceYesterday = double.Parse(arr[4]);
                md.openPrice = double.Parse(arr[5]);
                md.volume = long.Parse(arr[6]);

                int index = 9;
                for (int i = 0; i < 5; i++)
                {
                    md.bidList[i] = new BidOrderInfo();
                    md.bidList[i].price = double.Parse(arr[index++]);
                    md.bidList[i].amount = long.Parse(arr[index++]);
                }
                for (int i = 0; i < 5; i++)
                {
                    md.askList[i] = new BidOrderInfo();
                    md.askList[i].price = double.Parse(arr[index++]);
                    md.askList[i].amount = long.Parse(arr[index++]);
                }
                // transaction data ignored...
                md.date = arr[30];
                md.changing = double.Parse(arr[31]);
                md.chgPercent = double.Parse(arr[32]);
                md.highestPrice = double.Parse(arr[33]);
                md.lowestPrice = double.Parse(arr[34]);
                //
                md.turnoverVol = long.Parse(arr[36]);
                if (arr[38].Length > 0)
                {
                    md.exchangeRate = double.Parse(arr[38]);
                }
                else
                {
                    md.exchangeRate = 0;
                }

                md.PE = double.Parse(arr[39]);
                md.amplitude = double.Parse(arr[43]);
                md.circulateCapitalisation = double.Parse(arr[44]);
                md.totalCapitalisation = double.Parse(arr[45]);
                md.PB = double.Parse(arr[46]);
                md.volumeRatio = double.Parse(arr[49]);

                return md;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        private static StockKLine parseKLineData(String str)
        {
            string[] arr = str.Split(' ');
            if (arr.Length < 6)
            {
                return null;
            }

            StockKLine kl = new StockKLine();
            kl.date = arr[0];
            kl.openPrice = double.Parse(arr[1]);
            kl.latestPrice = double.Parse(arr[2]);
            kl.highestPrice = double.Parse(arr[3]);
            kl.lowestPrice = double.Parse(arr[4]);
            kl.volume = long.Parse((arr[5].Split('\\'))[0]);
            return kl;
        }

        public static List<StockKLine> parseKLineArray(String str)
        {
            if(str == null)
            {
                return null;
            }

            string[] wrapper = str.Split('\"');
            if(wrapper.Length < 2)
            {
                return null;
            }

            string[] arr = wrapper[1].Split('\n');
            if(arr.Length < 3)
            {
                return null;
            }

            List<StockKLine> kLines = new List<StockKLine>(); 
            for (int i = 2; i < arr.Length; i++)
            {
                StockKLine kl = parseKLineData(arr[i]);
                if(kl == null)
                {
                    continue;
                }
                kLines.Add(kl);
            }
            return kLines;
        }

        //public static List<StockKLine> parseKLineArrayXq(String str) {
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(str);
        //    bool ret = (jo["success"].Equals("true"));
        //    if (ret) {
        //        List<StockKLine> kLines = new List<StockKLine>();
        //        JArray arr = JArray.Parse(jo["chartlist"].ToString());
        //        for (int i = 0; i < arr.Count; i++) {
        //            StockKLine kl = parseKLineDataXq(arr[i].ToString());
        //            kLines.Add(kl);
        //        }

        //        return kLines;
        //    }

        //    return null;
        //}

        //private static StockKLine parseKLineDataXq(String str) {
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(str);
        //    if (jo != null) {
        //        StockKLine kLine = new StockKLine();
        //        kLine.highestPrice = double.Parse(jo["high"].ToString());
        //        kLine.lowestPrice = double.Parse(jo["low"].ToString());
        //        kLine.openPrice = double.Parse(jo["open"].ToString());
        //        kLine.latestPrice = double.Parse(jo["close"].ToString());
        //        kLine.volume = long.Parse(jo["volume"].ToString());
        //        kLine.date = jo["time"].ToString();

        //        return kLine;
        //    }

        //    return null;
        //}

        public static List<StockKLine> parseKLineArrayBaidu(String str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            bool ret = (jo["errorMsg"].ToString().Equals("SUCCESS"));
            if (ret)
            {
                List<StockKLine> kLines = new List<StockKLine>();
                if (jo.Property("mashData") != null)
                {
                    JArray arr = JArray.Parse(jo["mashData"].ToString());
                    for (int i = 0; i < arr.Count; i++)
                    {
                        StockKLine kl = parseKLineDataBaidu(arr[i].ToString());
                        kLines.Add(kl);
                    }
                }

                kLines.Reverse();

                return kLines;
            }

            return null;
        }

        private static StockKLine parseKLineDataBaidu(String str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                StockKLine kLine = new StockKLine();
                kLine.highestPrice = double.Parse(jo["kline"]["high"].ToString());
                kLine.lowestPrice = double.Parse(jo["kline"]["low"].ToString());
                kLine.openPrice = double.Parse(jo["kline"]["open"].ToString());
                kLine.latestPrice = double.Parse(jo["kline"]["close"].ToString());
                kLine.volume = long.Parse(jo["kline"]["volume"].ToString());
                kLine.date = jo["date"].ToString();

                return kLine;
            }

            return null;
        }

        public static List<StockKLineBaidu> parseKLineArrayBaiduAdvanced(String str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            bool ret = (jo["errorMsg"].ToString().Equals("SUCCESS"));
            if (ret)
            {
                List<StockKLineBaidu> kLines = new List<StockKLineBaidu>();
                if (jo.Property("mashData") != null)
                {
                    JArray arr = JArray.Parse(jo["mashData"].ToString());
                    for (int i = 0; i < arr.Count; i++)
                    {
                        StockKLineBaidu kl = parseKLineDataBaiduAdvanced(arr[i].ToString());
                        kLines.Add(kl);
                    }
                }

                kLines.Reverse();

                return kLines;
            }

            return null;
        }

        private static StockKLineBaidu parseKLineDataBaiduAdvanced(String str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                StockKLineBaidu kLine = new StockKLineBaidu();
                kLine.highestPrice = double.Parse(jo["kline"]["high"].ToString());
                kLine.lowestPrice = double.Parse(jo["kline"]["low"].ToString());
                kLine.openPrice = double.Parse(jo["kline"]["open"].ToString());
                kLine.latestPrice = double.Parse(jo["kline"]["close"].ToString());
                kLine.volume = long.Parse(jo["kline"]["volume"].ToString());
                kLine.date = jo["date"].ToString();

                kLine.ma5 = parseMABaidu(jo["ma5"].ToString());
                kLine.ma10 = parseMABaidu(jo["ma10"].ToString());
                kLine.ma20 = parseMABaidu(jo["ma20"].ToString());
                kLine.macd = parseMACDBaidu(jo["macd"].ToString());
                kLine.kdj = parseKDJBaidu(jo["kdj"].ToString());
                kLine.rsi = parseRSIBaidu(jo["rsi"].ToString());

                return kLine;
            }

            return null;
        }

        private static MAData parseMABaidu(string str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                MAData ma = new MAData();
                ma.volume = double.Parse(jo["volume"].ToString());
                ma.avgPrice = double.Parse(jo["avgPrice"].ToString());
                //ccl

                return ma;
            }

            return null;
        }

        private static MACDData parseMACDBaidu(string str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                MACDData macd = new MACDData();
                macd.diff = double.Parse(jo["diff"].ToString());
                macd.dea = double.Parse(jo["dea"].ToString());
                macd.macd = double.Parse(jo["macd"].ToString());
                return macd;
            }

            return null;
        }

        private static KDJData parseKDJBaidu(string str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                KDJData kdj = new KDJData();
                kdj.k = double.Parse(jo["k"].ToString());
                kdj.d = double.Parse(jo["d"].ToString());
                kdj.j = double.Parse(jo["j"].ToString());

                return kdj;
            }

            return null;
        }

        private static RSIData parseRSIBaidu(string str)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(str);
            if (jo != null)
            {
                RSIData rsi = new RSIData();
                rsi.rsi1 = double.Parse(jo["rsi1"].ToString());
                rsi.rsi2 = double.Parse(jo["rsi2"].ToString());
                rsi.rsi3 = double.Parse(jo["rsi3"].ToString());

                return rsi;
            }

            return null;
        }

        public static StockFinanceData parseFinanceDataSina(String str)
        {
            //string str0 = Encoding.Default.GetString(Encoding.GetEncoding("GBK").GetBytes(str));
            string[] arr0 = str.Split(';');
            Dictionary<string, string> contentMap = new Dictionary<string, string>();
            foreach (string element0 in arr0) {
                string element1 = element0.Replace("var ", "$");
                string[] arr1 = element1.Split('$');
                if(arr1.Length < 2)
                {
                    continue;
                }
                string element2 = arr1[1].Replace(" = ", "=");
                string[] arr2 = element2.Split('=');
                if(arr2.Length < 2)
                {
                    continue;
                }

                string keyStr = arr2[0];
                string valStr = arr2[1];
                //keyStr.Trim();
                //valStr.Trim();
                if(valStr[0] == '\'')
                {
                    valStr = valStr.Substring(1, valStr.Length - 2);
                }

                contentMap.Add(keyStr, valStr);
            }

            //// debug
            //foreach(KeyValuePair<string, string> keyVal in contentMap)
            //{
            //    System.Diagnostics.Debug.WriteLine(keyVal.Key + "," + keyVal.Value);
            //}

            StockFinanceData data = new StockFinanceData();
            try
            {
                //data.last5DayVolPerMinute = double.Parse(contentMap["lastfive"]);
                string val;
                if (contentMap.TryGetValue("lastfive", out val))
                {
                    data.last5DayVolPerMinute = double.Parse(val);
                }
                if (contentMap.TryGetValue("totalcapital", out val))
                {
                    data.totalCapital = double.Parse(val);
                }
                if (contentMap.TryGetValue("currcapital", out val))
                {
                    data.currCapital = double.Parse(val);
                }
                if (contentMap.TryGetValue("a_code", out val))
                {
                    data.stockCode = val;
                }
                if (contentMap.TryGetValue("fourQ_mgsy", out val))
                {
                    data.eps4Quarter = double.Parse(val);
                }
                if (contentMap.TryGetValue("lastyear_mgsy", out val))
                {
                    data.epsLastYear = double.Parse(val);
                }
                if (contentMap.TryGetValue("exchangerate", out val))
                {
                    data.exchangeRate = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_5_ago", out val))
                {
                    data.price5Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_10_ago", out val))
                {
                    data.price10Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_20_ago", out val))
                {
                    data.price20Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_60_ago", out val))
                {
                    data.price60Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_120_ago", out val))
                {
                    data.price120Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("price_250_ago", out val))
                {
                    data.price250Ago = double.Parse(val);
                }
                if (contentMap.TryGetValue("mgjzc", out val))
                {
                    data.naps = double.Parse(val);
                }
                if (contentMap.TryGetValue("profit", out val))
                {
                    data.profit = double.Parse(val);
                }
                if (contentMap.TryGetValue("profit_four", out val))
                {
                    data.profit4Quarter = double.Parse(val);
                }
                if (contentMap.TryGetValue("stockname", out val))
                {
                    data.stockName = val;
                }
            }
            catch (Exception e) {
                //
                return null;
            }

            return data;
        }
    }
}
