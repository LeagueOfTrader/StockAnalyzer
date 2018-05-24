using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze
{
    public class PriceAnalyzer
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

        public static bool isPriceScaleSatisfied(string stockID, double curPrice, double ratio)
        {
            List<StockKLine> monthKData = StockDataCenter.getInstance().getMonthKLineBaidu(stockID);

            if (monthKData == null) {
                return false;
            }

            double sr = getPriceScale(monthKData, curPrice, "20120101");
            if (sr < ratio && sr > 0.0 && curPrice > 0.0)
            {
                return true;
            }

            return false;
        }
    }
}
