using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    // sina finance data
    public class StockFinanceData
    {
        public string stockCode = "";
        public double totalCapital = 0;
        public double currCapital = 0;
        public double last5DayVolPerMinute = 0;
        public double eps4Quarter = 0;
        public double epsLastYear = 0;
        public double exchangeRate = 0;
        public double price5Ago = 0;
        public double price10Ago = 0;
        public double price20Ago = 0;
        public double price60Ago = 0;
        public double price120Ago = 0;
        public double price250Ago = 0;
        public double naps = 0; // net asset per share
        public string stockName = "";
        public double profit = 0;
        public double profit4Quarter = 0;
    }
}
