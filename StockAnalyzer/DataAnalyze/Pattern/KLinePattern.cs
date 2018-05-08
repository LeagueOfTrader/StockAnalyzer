using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    abstract class KLinePattern
    {
        public abstract bool isMatch(List<StockKLineBaidu> kLineData);

        protected int getLowestPositionIndex(List<StockKLineBaidu> arr)
        {
            if(arr == null)
            {
                return -1;
            }

            int index = -1;
            double lowestPrice = double.MaxValue;

            for(int i = 0; i < arr.Count; i++)
            {
                if(arr[i].lowestPrice < lowestPrice)
                {
                    index = i;
                    lowestPrice = arr[i].lowestPrice;
                }
            }

            return index;
        }

        protected int getHighestPositionIndex(List<StockKLineBaidu> arr)
        {
            if (arr == null)
            {
                return -1;
            }

            int index = -1;
            double highestPrice = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].highestPrice > highestPrice)
                {
                    index = i;
                    highestPrice = arr[i].highestPrice;
                }
            }

            return index;
        }

        protected double getMADiffRatio(List<StockKLineBaidu> arr, int days, int index)
        {
            if(arr == null || arr.Count < index)
            {
                return 0.0;
            }

            double curMA = 0.0;
            double nextMA = 0.0;
            switch (days)
            {
                case 5:
                    curMA = arr[index].ma5.avgPrice;
                    nextMA = arr[index + 1].ma5.avgPrice;
                    break;
                case 10:
                    curMA = arr[index].ma10.avgPrice;
                    nextMA = arr[index + 1].ma10.avgPrice;
                    break;
                case 20:
                    curMA = arr[index].ma20.avgPrice;
                    nextMA = arr[index + 1].ma20.avgPrice;
                    break;
                default:
                    curMA = arr[index].ma5.avgPrice;
                    nextMA = arr[index + 1].ma5.avgPrice;
                    break;
            }

            double val = (nextMA - curMA) / curMA;
            return val;
        }

        //protected bool isTrend
    }
}
