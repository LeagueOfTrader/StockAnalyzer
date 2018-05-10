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
        const double m_sidewayRangeMA = 0.025;
        const double m_trendMinAmountMA = 0.05;
        const double m_sidewayRange = 0.05;
        const double m_trendMinAmount = 0.07;

        const double m_sidewayMaxAmplitudeMA = 0.02;
        const double m_trendAmplitudeMA = 0.01;
        const double m_sidewayMaxAmplitude = 0.02;
        const double m_trendAmplitude = 0.015;

        public delegate double extractDataMethod(StockKLineBaidu kl);

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
            bool rangeRet = false;
            bool diffRet = false;
            switch (trend)
            {
                case TrendType.TT_Down:
                    rangeRet = isRangeBeyond(arr, -m_trendMinAmount, getCenterPrice, false);
                    diffRet = isDiffBeyond(arr, -m_trendAmplitude, getCenterPrice, -m_trendAmplitudeMA, getMA5Value, false);
                    //ret = (rangeRet & diffRet);
                    break;
                case TrendType.TT_NotUp:
                    rangeRet = isRangeBeyond(arr, -m_trendMinAmount, getCenterPrice, false);
                    diffRet = isDiffBeyond(arr, m_sidewayMaxAmplitude, getCenterPrice, -m_trendAmplitudeMA, getMA5Value, false);
                    //ret = (rangeRet & diffRet);
                    break;
                case TrendType.TT_Sideway:
                    rangeRet = isRangeWithin(arr, -m_trendMinAmount, getCenterPrice);
                    diffRet = isDiffBeyond(arr, m_sidewayMaxAmplitude, getCenterPrice, -m_trendAmplitudeMA, getMA5Value, false);
                    break;
                case TrendType.TT_NotDown:
                    break;
                case TrendType.TT_Up:
                    break;
                default:
                    break;
            }

            ret = (rangeRet & diffRet);

            return ret;
        }

        protected bool isRangeBeyond(List<StockKLineBaidu> arr, double range, extractDataMethod method, bool positive = true)
        {
            if(arr == null || arr.Count < 2)
            {
                return false;
            }

            double startVal = method(arr[0]);
            double endVal = method(arr.Last());

            double val = (endVal - startVal) / startVal;
            if (positive)
            {
                return (val > range);
            }
            else
            {
                return (val < range);
            }
        }

        protected bool isRangeWithin(List<StockKLineBaidu> arr, double range, extractDataMethod method)
        {
            if (arr == null || arr.Count < 2)
            {
                return false;
            }

            double startVal = method(arr[0]);
            double endVal = method(arr.Last());

            double val = (endVal - startVal) / startVal;
            return (Math.Abs(val) <= range);
        }

        protected bool isDiffBeyond(List<StockKLineBaidu> arr, double diff, extractDataMethod method,
                            double subDiff, extractDataMethod subMethod, bool positive = true)
        {
            if (arr == null || method == null)
            {
                return false;
            }

            int i = 0;
            for (i = 0; i < arr.Count - 1; i++)
            {
                double curVal = method(arr[i]);
                double nextVal = method(arr[i + 1]);
                double curSubVal = subMethod(arr[i]);
                double nextSubVal = subMethod(arr[i + 1]);

                double val = (nextVal - curVal) / curVal;
                double subVal = (nextSubVal - curSubVal) / curSubVal;
                if (positive)
                {
                    if (val < diff)
                    {
                        if (subVal < subDiff)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (val > diff)
                    {
                        if (subVal > subDiff)
                        {
                            break;
                        }
                    }
                }
            }
            if (i < arr.Count - 1)
            {
                return false;
            }

            return true;
        }

        protected bool isDiffWithin(List<StockKLineBaidu> arr, double diff, extractDataMethod method, 
                                    double subDiff, extractDataMethod subMethod, bool positive = true)
        {
            if (arr == null || method == null)
            {
                return false;
            }

            int i = 0;
            for (i = 0; i < arr.Count - 1; i++)
            {
                double curVal = method(arr[i]);
                double nextVal = method(arr[i + 1]);
                double curSubVal = subMethod(arr[i]);
                double nextSubVal = subMethod(arr[i + 1]);

                double val = (nextVal - curVal) / curVal;
                double subVal = (nextSubVal - curSubVal) / curSubVal;
                if(Math.Abs(val) > diff)
                {
                    if(Math.Abs(subVal) > subDiff)
                    {
                        break;
                    }
                }
                
            }
            if (i < arr.Count - 1)
            {
                return false;
            }

            return true;
        }

        protected static double getCenterPrice(StockKLineBaidu kl)
        {
            return kl.getCenterPrice();
        }

        protected static double getMA5Value(StockKLineBaidu kl)
        {
            return kl.ma5.avgPrice;
        }
    }
}
