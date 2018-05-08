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
        const double m_sidewayRangeCeilMA = 0.025;
        const double m_trendRangeFloorMA = 0.05;
        const double m_sidewayRangeCeilPrice = 0.05;
        const double m_trendRangeFloorPrice = 0.075;

        const double m_sidewayAmplitudeCeilMA = 0.025;
        const double m_trendAmplitudeFloorMA = 0.05;
        const double m_sidewayAmplitudeCeilPrice = 0.03;
        const double m_trendAmplitudeFloorPrice = 0.0;

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

        protected bool accordTrend(List<StockKLineBaidu> arr, TrendType trend) {
            bool ret = false;
            switch (trend)
            {
                case TrendType.TT_Down:
                    ret = isMAWithin(arr, -m_sidewayRange, -m_trendRange);
                    break;
                case TrendType.TT_NotUp:
                    ret = isMAWithin(arr, m_sidewayRange, -m_trendRange);
                    break;
                case TrendType.TT_Sideway:
                    ret = isMAWithin(arr, -m_trendRange, -m_trendRange);
                    break;
                case TrendType.TT_NotDown:
                    break;
                case TrendType.TT_Up:
                    break;
                default:
                    break;
            }

            return ret;
        }

        protected bool isMABeyond(List<StockKLineBaidu> arr, double diff, double diff5)
        {
            return false;
        }

        protected bool isMAWithin(List<StockKLineBaidu> arr, double diff, double diff5)
        {
            return false;
        }

        protected bool isMAInBox(List<StockKLineBaidu> arr, double diff)
        {
            return false;
        }
    }
}
