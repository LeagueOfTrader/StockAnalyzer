using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    public class KLineDataProcessor
    {
        public static double calcMAValue(List<KLine> kLineData)
        {
            double maPrice = 0.0;
            if (kLineData.Count > 0)
            {
                for (int i = 0; i < kLineData.Count; i++)
                {
                    maPrice += kLineData[i].latestPrice;
                }
                maPrice /= kLineData.Count;
            }
            return maPrice;
        }

        public static double calcMAValue(List<KLine> kLineData, int count, int start = 0)
        {
            double maPrice = 0.0;
            if (start + count > kLineData.Count)
            {
                count = kLineData.Count - start;
            }

            for (int i = 0; i < count; i++)
            {
                maPrice += kLineData[i + start].latestPrice;
            }
            maPrice /= count;

            return maPrice;
        }

        public static List<double> calcMAData(List<KLine> kLineData, int days)
        {
            List<double> maArr = new List<double>();

            if(kLineData.Count >= days)
            {
                for(int i = days - 1; i < kLineData.Count; i++)
                {
                    double accumVal = 0.0;
                    for(int j = 0; j < days; j++)
                    {
                        accumVal += kLineData[i - j].latestPrice;
                    }
                    double maVal = accumVal / days;
                    maArr.Add(maVal);
                }
            }

            return maArr;
        }

        public static List<double> calcMAData(List<StockKLine> kLineData, int days)
        {
            List<double> maArr = new List<double>();

            if (kLineData.Count >= days)
            {
                for (int i = days - 1; i < kLineData.Count; i++)
                {
                    double accumVal = 0.0;
                    for (int j = 0; j < days; j++)
                    {
                        accumVal += kLineData[i - j].latestPrice;
                    }
                    double maVal = accumVal / days;
                    maArr.Add(maVal);
                }
            }

            return maArr;
        }

        public static void calcKLineExtremum(List<KLine> kLineData, out double maxPrice, out double minPrice)
        {
            maxPrice = 0.0;
            minPrice = double.MaxValue;

            for(int i = 0; i < kLineData.Count; i++)
            {
                if(maxPrice < kLineData[i].highestPrice)
                {
                    maxPrice = kLineData[i].highestPrice;
                }

                if(minPrice > kLineData[i].lowestPrice)
                {
                    minPrice = kLineData[i].lowestPrice;
                }
            }
        }

        public static void calcKLineExtremum(List<StockKLine> kLineData, out double maxPrice, out double minPrice)
        {
            maxPrice = 0.0;
            minPrice = double.MaxValue;

            for (int i = 0; i < kLineData.Count; i++)
            {
                if (maxPrice < kLineData[i].highestPrice)
                {
                    maxPrice = kLineData[i].highestPrice;
                }

                if (minPrice > kLineData[i].lowestPrice)
                {
                    minPrice = kLineData[i].lowestPrice;
                }
            }
        }
    }
}
