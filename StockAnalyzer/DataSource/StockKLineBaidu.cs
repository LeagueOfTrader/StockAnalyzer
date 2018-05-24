using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    public class MAData
    {
        public double volume;
        public double avgPrice;
        public double ccl;
    }

    public class MACDData
    {
        public double diff;
        public double dea;
        public double macd;
    }

    public class KDJData
    {
        public double k;
        public double d;
        public double j;
    }

    public class RSIData
    {
        public double rsi1;
        public double rsi2;
        public double rsi3;
    }

    public class StockKLineBaidu : StockKLine
    {
        public MAData ma5 = new MAData();
        public MAData ma10 = new MAData();
        public MAData ma20 = new MAData();
        public MACDData macd = new MACDData();
        public KDJData kdj = new KDJData();
        public RSIData rsi = new RSIData();
    }
}
