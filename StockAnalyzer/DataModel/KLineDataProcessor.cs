using StockAnalyzer.DataSource;
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

        public static double calcKLineAverageAmplitude(List<StockKLineBaidu> kLineData, int days)
        {
            if(kLineData == null || kLineData.Count == 0)
            {
                return 0.0;
            }

            days = Math.Min(days, kLineData.Count - 1);
            double accumVal = 0.0;
            for(int i = 0; i < days; i++)
            {
                int index = kLineData.Count - 1 - i;
                double amplitude = 0.0;
                if (kLineData[index].lowestPrice < kLineData[index - 1].latestPrice 
                    && kLineData[index].highestPrice > kLineData[index - 1].latestPrice)
                {
                    amplitude = kLineData[index].getRange() / kLineData[index - 1].latestPrice;
                }
                else if(kLineData[index].lowestPrice >= kLineData[index - 1].latestPrice)
                {
                    amplitude = kLineData[index].highestPrice / kLineData[index - 1].latestPrice - 1.0;
                }
                else
                {
                    amplitude = 1.0 - kLineData[index].lowestPrice / kLineData[index - 1].latestPrice;
                }

                accumVal += Math.Abs(amplitude);
            }

            double avgVal = accumVal / days;
            return avgVal;
        }
    }
}
