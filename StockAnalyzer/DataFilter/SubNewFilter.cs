using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using StockAnalyzer.Global;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class SubNewFilter : StockFilter
    {
        const int ms_subNewYearLimit = 5;
        public override bool filterMethod(string stockID)
        {
            string curYear = GlobalConfig.getInstance().curYear;
            int targetYear = int.Parse(curYear);
            int srcYear = getEarliestYearInKLineInfo(stockID);
            if(srcYear == 0)
            {
                return false;
            }

            if(targetYear - srcYear <= ms_subNewYearLimit)
            {
                return false;
            }

            return true;
        }

        public int getEarliestYearInKLineInfo(string stockID)
        {
            List<StockKLine> arr = StockDataCenter.getInstance().getMonthKLine(stockID);
            if(arr == null || arr.Count == 0)
            {
                return 0;
            }

            int year = DateUtil.getShortDateYear(arr[0].date);
            return year;
        }
    }
}
