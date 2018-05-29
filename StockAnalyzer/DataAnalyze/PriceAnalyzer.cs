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

        public static double getPriceScale(List<StockKLineBaidu> data, double curPrice, string limitDate = "20000101")
        {
            if (data.Count == 0)
            {
                return 1.0;
            }

            double ceil = 0.0;
            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++)
            {
                if (DateUtil.compareDate(data[i].date, limitDate) < 0)
                {
                    continue;
                }
                if (data[i].highestPrice > ceil)
                {
                    ceil = data[i].highestPrice;

                }
                if (floor > data[i].lowestPrice)
                {
                    floor = data[i].lowestPrice;
                }
            }

            double div = ceil - floor;
            double ret = double.MinValue;
            if (div > double.Epsilon)
            {
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

        public static bool getPriceScaleFromDate(string stockID, string limitDate, out double ratio)
        {
            ratio = 0.0;
            StockMarketData md = StockDataCenter.getInstance().getMarketData(stockID);
            if(md == null)
            {
                return false;
            }

            double curPrice = md.latestPrice;
            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);

            if(arr == null || arr.Count == 0)
            {
                return false;
            }

            ratio = getPriceScale(arr, curPrice, limitDate);
            return true;
        }

        public static bool getLowestPriceFromDate(string stockID, string limitDate, out double lowestPrice)
        {
            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);
            lowestPrice = 0.0;
            if (arr == null || arr.Count == 0)
            {
                return false;
            }

            lowestPrice = getLowestPrice(arr, limitDate);
            return true;
        }

        public static double getLowestPrice(List<StockKLineBaidu> data, string limitDate = "20000101")
        {
            if (data.Count == 0)
            {
                return 0.0;
            }

            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++)
            {
                if (DateUtil.compareDate(data[i].date, limitDate) < 0)
                {
                    continue;
                }
                if (floor > data[i].lowestPrice)
                {
                    floor = data[i].lowestPrice;
                }
            }

            return floor;
        }
    }
}
