using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    class MAData
    {
        public double volume;
        public double avgPrice;
        public double ccl;
    } 

    class MACDData
    {
        public double diff;
        public double dea;
        public double macd;
    }

    class KDJData
    {
        public double k;
        public double d;
        public double j;
    }

    class RSIData
    {
        public double rsi1;
        public double rsi2;
        public double rsi3;
    }

    class StockKLineBaidu : StockKLine
    {
        public MAData ma5 = new MAData();
        public MAData ma10 = new MAData();
        public MAData ma20 = new MAData();
        public MACDData macd = new MACDData();
        public KDJData kdj = new KDJData();
        public RSIData rsi = new RSIData();
    }
}
