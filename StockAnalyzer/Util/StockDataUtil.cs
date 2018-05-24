using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    public class StockDataUtil
    {
        public static StockKLine getMonthKLineByYearMonth(List<StockKLine> klArr, string year, string month)
        {
            StockKLine kl = null;
            if(klArr != null && klArr.Count > 0)
            {
                for(int i = 0; i < klArr.Count; i++)
                {
                    if (DateUtil.matchYearMonth(klArr[i].date, year, month))
                    {
                        kl = klArr[i];
                        break;
                    }
                }
            }

            return kl;
        }

        public static int getIndexByDate(List<StockKLineBaidu> klArr, string date)
        {
            int index = -1;
            if (klArr != null && klArr.Count > 0)
            {
                for (int i = klArr.Count - 1; i >= 0;  i--)
                {
                    if (DateUtil.matchDate(klArr[i].date, date))
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }

        public static void getNextQuarter(string srcYear, string srcQuarter, out string targetYear, out string targetQuarter)
        {
            if(srcQuarter == "4")
            {
                int ty = int.Parse(srcYear) + 1;
                targetYear = ty.ToString();
                targetQuarter = "1";
                return;
            }

            int tq = int.Parse(srcQuarter) + 1;
            targetYear = srcYear;
            targetQuarter = tq.ToString();
        }
    }
}
