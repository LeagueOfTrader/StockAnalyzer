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

            StockMarketData md = new StockMarketData();
            md.stockName = arr[1];
            md.stockCode = arr[2];
            md.latestPrice = double.Parse(arr[3]);
            md.closePriceYesterday = double.Parse(arr[4]);
            md.openPrice = double.Parse(arr[5]);
            md.volume = long.Parse(arr[6]);

            int index = 9;
            for(int i = 0; i < 5; i++)
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
            md.exchangeRate = double.Parse(arr[38]);
            md.PE = double.Parse(arr[39]);
            md.amplitude = double.Parse(arr[43]);
            md.circulateCapitalisation = double.Parse(arr[44]);
            md.totalCapitalisation = double.Parse(arr[45]);
            md.PB = double.Parse(arr[46]);
            md.volumeRatio = double.Parse(arr[49]);

            return md;
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
    }
}
