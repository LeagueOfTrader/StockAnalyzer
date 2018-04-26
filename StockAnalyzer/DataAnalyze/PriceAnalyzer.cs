using StockAnalyzer.DataModel;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze
{
    class PriceAnalyzer
    {
        public static double getPriceScale(List<StockKLine> data, double curPrice, string limitDate = "20000101") {

            if (data.Count == 0) {
                return 1.0;
            }

            double ceil = 0.0;
            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++) {
                if (DateUtil.compareDate(data[i].date, limitDate) < 0) {
                    continue;
                }
                if (data[i].highestPrice > ceil) {
                    ceil = data[i].highestPrice;
                   
                }
                if (floor > data[i].lowestPrice){
                    floor = data[i].lowestPrice;
                }
            }

            double div = ceil - floor;
            double ret = double.MinValue;
            if (div > double.Epsilon) {
                ret = (curPrice - floor) / div;
            }

            return ret;
        }
    }
}
