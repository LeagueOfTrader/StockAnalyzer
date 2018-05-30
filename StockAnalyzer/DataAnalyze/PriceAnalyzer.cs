using StockAnalyzer.DataCache;
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
        public static double getPriceScale(List<StockKLine> data, double curPrice, string beginDate = "20000101") {

            if (data.Count == 0) {
                return 1.0;
            }

            double ceil = 0.0;
            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++) {
                if (DateUtil.compareDate(data[i].date, beginDate) < 0) {
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

        public static double getPriceScale(List<StockKLineBaidu> data, double curPrice, string beginDate = "20000101")
        {
            if (data.Count == 0)
            {
                return 1.0;
            }

            double ceil = 0.0;
            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++)
            {
                if (DateUtil.compareDate(data[i].date, beginDate) < 0)
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

        public static bool getPriceScaleFromDate(string stockID, string beginDate, out double ratio)
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

            ratio = getPriceScale(arr, curPrice, beginDate);
            return true;
        }

        public static bool getLowestPriceFromDate(string stockID, string beginDate, out double lowestPrice)
        {
            if (StockDataCache.getInstance().getLowestPriceFromDate(stockID, beginDate, out lowestPrice))
            {
                return true;
            }

            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);
            lowestPrice = 0.0;
            if (arr == null || arr.Count == 0)
            {
                return false;
            }

            lowestPrice = getLowestPrice(arr, beginDate);
            StockDataCache.getInstance().setLowestPriceFromDate(stockID, beginDate, lowestPrice);
            return true;
        }

        public static bool getLowestPriceBetweenDate(string stockID, string beginDate, string endDate, out double lowestPrice)
        {
            if (StockDataCache.getInstance().getLowestPriceBetweenDate(stockID, beginDate, endDate, out lowestPrice))
            {
                return true;
            }

            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);
            lowestPrice = 0.0;
            if (arr == null || arr.Count == 0)
            {
                return false;
            }

            lowestPrice = getLowestPrice(arr, beginDate, endDate);
            StockDataCache.getInstance().setLowestPriceBetweenDate(stockID, beginDate, endDate, lowestPrice);
            return true;
        }

        protected static double getLowestPrice(List<StockKLineBaidu> data, string beginDate = "20000101", string endDate = "")
        {
            if (data.Count == 0)
            {
                return 0.0;
            }

            if(endDate == "")
            {
                endDate = DateUtil.getTodayDate();
            }

            double floor = double.MaxValue;
            for (int i = 0; i < data.Count; i++)
            {
                if (DateUtil.compareDate(data[i].date, beginDate) < 0 ||
                    DateUtil.compareDate(data[i].date, endDate) > 0 )
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
