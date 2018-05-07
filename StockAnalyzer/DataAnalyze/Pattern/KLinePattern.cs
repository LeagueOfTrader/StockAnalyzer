using StockAnalyzer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    abstract class KLinePattern
    {
        public abstract bool isMatch(List<StockKLine> kLineData);

        protected int getLowestPositionIndex(List<StockKLine> arr)
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

        protected int getHighestPositionIndex(List<StockKLine> arr)
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
    }
}
