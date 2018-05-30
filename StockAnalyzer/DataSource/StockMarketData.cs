using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    public class BidOrderInfo
    {
        public double price;
        public long amount;
    }    

    public class StockRealTimeData : StockKLine
    {
        public string stockCode;
        public string stockName;
        public BidOrderInfo[] bidList = new BidOrderInfo[5];
        public BidOrderInfo[] askList = new BidOrderInfo[5];
        public double closePriceYesterday;
        public double bidPrice;
        public double askPrice;
        //public long turnoverVol; //成交量
        public double turnoverAmount; //成交额
        public string time;
    }

    public class StockMarketData : StockRealTimeData
    {        
        public double exchangeRate;
        //public double bidPrice; // buy
        //public double askPrice; // sell
        //public BidOrderInfo[] bidList = new BidOrderInfo[5];
        //public BidOrderInfo[] askList = new BidOrderInfo[5];
        public double volumeRatio;
        public double changing;
        public double chgPercent;
        
        public double PB;
        public double PE;
        public double amplitude;
        public double circulateCapitalisation = 0;
        public double totalCapitalisation = 0;
    }
}
