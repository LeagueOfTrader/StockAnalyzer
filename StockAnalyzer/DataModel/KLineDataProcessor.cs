using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    class KLineDataProcessor
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
    }
}
