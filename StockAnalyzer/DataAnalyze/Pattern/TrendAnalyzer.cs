using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    class TrendAnalyzer
    {
        const double m_inflectionLimit = 0.005;

        const int m_sidewayDaysLimitInTrend = 3;
        const double m_retracementLimit = 0.03;
        const double m_trendEstablishedAmount = 0.05;
        const double m_trendDayAmplitude = 0.015;
        const double m_sidewayAmplitudeLimit = 0.02;

        public delegate double extractDataMethod(StockKLineBaidu kl);

        public static int getLowestPositionIndex(List<StockKLineBaidu> arr)
        {
            if (arr == null)
            {
                return -1;
            }

            int index = -1;
            double lowestPrice = double.MaxValue;

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].lowestPrice < lowestPrice)
                {
                    index = i;
                    lowestPrice = arr[i].lowestPrice;
                }
            }

            return index;
        }

        public static int getHighestPositionIndex(List<StockKLineBaidu> arr)
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

        public static double getMADiffRatio(List<StockKLineBaidu> arr, int days, int index)
        {
            if (arr == null || arr.Count < index)
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

        public static int getInflectionPoint(List<StockKLineBaidu> arr, int startIndex, TrendType trend, bool reversed = false)
        {
            int index = -1;
            int i = 0;
            double diff = m_inflectionLimit;
            double subDiff = m_inflectionLimit;
            if (!reversed)
            {
                index = arr.Count;
                for (i = startIndex; i < arr.Count - 1; i++)
                {
                    double curVal = getCenterPrice(arr[i]);
                    double nextVal = getCenterPrice(arr[i + 1]);
                    double curSubVal = getMA5Value(arr[i]);
                    double nextSubVal = getMA5Value(arr[i + 1]);

                    double val = (nextVal - curVal) / curVal;
                    double subVal = (nextSubVal - curSubVal) / curSubVal;
                    if (trend == TrendType.TT_Up)
                    {
                        if (val < diff && subVal < subDiff)
                        {
                            index = i + 1;
                            break;
                        }
                    }
                    else if (trend == TrendType.TT_Down)
                    {
                        if (val > -diff && subVal < -subDiff)
                        {
                            index = i + 1;
                            break;
                        }
                    }
                }
            }
            else
            {
                index = 0;
                for (i = startIndex; i > 0; i--)
                {
                    double curVal = getCenterPrice(arr[i]);
                    double prevVal = getCenterPrice(arr[i - 1]);
                    double curSubVal = getMA5Value(arr[i]);
                    double prevSubVal = getMA5Value(arr[i - 1]);

                    double val = (curVal - prevVal) / prevVal;
                    double subVal = (curSubVal - prevSubVal) / prevSubVal;
                    if (trend == TrendType.TT_Up)
                    {
                        if (val < diff && subVal < subDiff)
                        {
                            index = i - 1;
                            break;
                        }
                    }
                    else if (trend == TrendType.TT_Down)
                    {
                        if (val > -diff && subVal < -subDiff)
                        {
                            index = i - 1;
                            break;
                        }
                    }
                }
            }

            return index;
        }

        public static bool accordTrend(List<StockKLineBaidu> arr, TrendType trend)
        {
            bool ret = false;
            switch (trend)
            {
                case TrendType.TT_Down:
                    ret = accordDownTrend(arr);                  
                    break;
                case TrendType.TT_NotUp:
                    ret = accordNotUpTrend(arr);                 
                    break;
                case TrendType.TT_Sideway:
                    ret = accordSideway(arr);
                    break;
                case TrendType.TT_NotDown:
                    ret = accordNotDownTrend(arr);
                    break;
                case TrendType.TT_Up:
                    ret = accordUpTrend(arr);
                    break;
                default:
                    break;
            }

            return ret;
        }

        //public bool isRangeBeyond(List<StockKLineBaidu> arr, double range, extractDataMethod method, bool positive = true)
        //{
        //    if (arr == null || arr.Count < 2)
        //    {
        //        return false;
        //    }

        //    double startVal = method(arr[0]);
        //    double endVal = method(arr.Last());

        //    double val = (endVal - startVal) / startVal;
        //    if (positive)
        //    {
        //        return (val > range);
        //    }
        //    else
        //    {
        //        return (val < range);
        //    }
        //}

        //public bool isRangeWithin(List<StockKLineBaidu> arr, double range, extractDataMethod method)
        //{
        //    if (arr == null || arr.Count < 2)
        //    {
        //        return false;
        //    }

        //    double startVal = method(arr[0]);
        //    double endVal = method(arr.Last());

        //    double val = (endVal - startVal) / startVal;
        //    return (Math.Abs(val) <= range);
        //}

        //public bool isDiffBeyond(List<StockKLineBaidu> arr, double diff, extractDataMethod method,
        //                    double subDiff, extractDataMethod subMethod, bool positive = true)
        //{
        //    if (arr == null || method == null)
        //    {
        //        return false;
        //    }

        //    int i = 0;
        //    for (i = 0; i < arr.Count - 1; i++)
        //    {
        //        double curVal = method(arr[i]);
        //        double nextVal = method(arr[i + 1]);
        //        double curSubVal = subMethod(arr[i]);
        //        double nextSubVal = subMethod(arr[i + 1]);

        //        double val = (nextVal - curVal) / curVal;
        //        double subVal = (nextSubVal - curSubVal) / curSubVal;
        //        if (positive)
        //        {
        //            if (val < diff)
        //            {
        //                if (subVal < subDiff)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (val > diff)
        //            {
        //                if (subVal > subDiff)
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    if (i < arr.Count - 1)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public bool isDiffWithin(List<StockKLineBaidu> arr, double diff, extractDataMethod method,
        //                            double subDiff, extractDataMethod subMethod)
        //{
        //    if (arr == null || method == null)
        //    {
        //        return false;
        //    }

        //    int i = 0;
        //    for (i = 0; i < arr.Count - 1; i++)
        //    {
        //        double curVal = method(arr[i]);
        //        double nextVal = method(arr[i + 1]);
        //        double curSubVal = subMethod(arr[i]);
        //        double nextSubVal = subMethod(arr[i + 1]);

        //        double val = (nextVal - curVal) / curVal;
        //        double subVal = (nextSubVal - curSubVal) / curSubVal;
        //        if (Math.Abs(val) > diff)
        //        {
        //            if (Math.Abs(subVal) > subDiff)
        //            {
        //                break;
        //            }
        //        }

        //    }
        //    if (i < arr.Count - 1)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public static double getCenterPrice(StockKLineBaidu kl)
        {
            return kl.getCenterPrice();
        }

        public static double getMA5Value(StockKLineBaidu kl)
        {
            return kl.ma5.avgPrice;
        }

        protected static bool accordUpTrend(List<StockKLineBaidu> arr)
        {
            if(arr == null || arr.Count < 2)
            {
                return false;
            }

            double endCenterVal = getCenterPrice(arr.Last());
            double startCenterVal = getCenterPrice(arr.First());

            double phaseAmount = (endCenterVal - startCenterVal) / startCenterVal;
            if(phaseAmount < m_trendEstablishedAmount)
            {
                return false;
            }

            int sidewayDays = 0;
            int retracementIndex = -1;
            int i = 0;
            for(i = 0; i < arr.Count - 1; i++)
            {
                double curCenterVal = getCenterPrice(arr[i]);
                double nextCenterVal = getCenterPrice(arr[i + 1]);

                double curAmplitude = (nextCenterVal - curCenterVal) / curCenterVal;
                if(curAmplitude > m_trendDayAmplitude)
                {
                    if(retracementIndex != -1)
                    {
                        //if(arr[i].latestPrice > arr[retracementIndex].latestPrice)
                        double chgFromRtr = (arr[i].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                        if(chgFromRtr > m_sidewayAmplitudeLimit)
                        {
                            retracementIndex = -1;
                            sidewayDays = 0;
                        }
                    }
                    continue;
                }

                if(curAmplitude < -m_sidewayAmplitudeLimit)
                {
                    // 下跌幅度过大且下破5日均线
                    if(arr[i].latestPrice > arr[i].ma5.avgPrice && 
                        arr[i + 1].latestPrice < arr[i + 1].ma5.avgPrice)
                    {
                        break;
                    }                                        
                }

                sidewayDays++;
                if(retracementIndex == -1)
                {
                    retracementIndex = i;
                }

                double chg = (arr[i + 1].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                if(chg < -m_retracementLimit)
                {
                    break;
                }                
                
                if (sidewayDays >= m_sidewayDaysLimitInTrend)
                {
                    break;
                }                
            }
            
            if(i == arr.Count)
            {
                return true;
            }
            return false;
        }

        protected static bool accordDownTrend(List<StockKLineBaidu> arr)
        {
            if (arr == null || arr.Count < 2)
            {
                return false;
            }

            double endCenterVal = getCenterPrice(arr.Last());
            double startCenterVal = getCenterPrice(arr.First());

            double phaseAmount = (endCenterVal - startCenterVal) / startCenterVal;
            if (phaseAmount > -m_trendEstablishedAmount)
            {
                return false;
            }

            int sidewayDays = 0;
            int retracementIndex = -1;
            int i = 0;
            for (i = 0; i < arr.Count - 1; i++)
            {
                double curCenterVal = getCenterPrice(arr[i]);
                double nextCenterVal = getCenterPrice(arr[i + 1]);

                double curAmplitude = (nextCenterVal - curCenterVal) / curCenterVal;
                if (curAmplitude < -m_trendDayAmplitude)
                {
                    if (retracementIndex != -1)
                    {
                        double chgFromRtr = (arr[i].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                        if (chgFromRtr < -m_sidewayAmplitudeLimit)
                        {
                            retracementIndex = -1;
                            sidewayDays = 0;
                        }
                    }
                    continue;
                }

                if (curAmplitude > m_sidewayAmplitudeLimit)
                {
                    // 上涨幅度过大且上破5日均线
                    if (arr[i].latestPrice < arr[i].ma5.avgPrice &&
                        arr[i + 1].latestPrice > arr[i + 1].ma5.avgPrice)
                    {
                        break;
                    }
                }

                sidewayDays++;
                if (retracementIndex == -1)
                {
                    retracementIndex = i;
                }

                double chg = (arr[i + 1].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                if (chg > m_retracementLimit)
                {
                    break;
                }

                if (sidewayDays >= m_sidewayDaysLimitInTrend)
                {
                    break;
                }
            }

            if (i == arr.Count)
            {
                return true;
            }
            return false;
        }

        protected static bool accordNotDownTrend(List<StockKLineBaidu> arr)
        {
            if (arr == null || arr.Count < 2)
            {
                return false;
            }

            int retracementIndex = -1;
            int i = 0;
            for (i = 0; i < arr.Count - 1; i++)
            {
                double curCenterVal = getCenterPrice(arr[i]);
                double nextCenterVal = getCenterPrice(arr[i + 1]);
                double curAmplitude = (nextCenterVal - curCenterVal) / curCenterVal;
                if (curAmplitude > m_trendDayAmplitude)
                {
                    if (retracementIndex != -1)
                    {
                        double chgFromRtr = (arr[i].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                        if (chgFromRtr > m_sidewayAmplitudeLimit)
                        {
                            retracementIndex = -1;
                        }
                    }
                    continue;
                }
                if (curAmplitude < -m_sidewayAmplitudeLimit)
                {
                    if (arr[i].latestPrice < arr[i].ma5.avgPrice &&
                        arr[i + 1].latestPrice > arr[i + 1].ma5.avgPrice)
                    {
                        break;
                    }
                }
                if (retracementIndex == -1)
                {
                    retracementIndex = i;
                }

                double chg = (arr[i + 1].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                if (chg < -m_retracementLimit)
                {
                    break;
                }
            }

            if (i == arr.Count)
            {
                return true;
            }
            return false;
        }

        protected static bool accordNotUpTrend(List<StockKLineBaidu> arr)
        {
            if (arr == null || arr.Count < 2)
            {
                return false;
            }

            int retracementIndex = -1;
            int i = 0;
            for (i = 0; i < arr.Count - 1; i++)
            {
                double curCenterVal = getCenterPrice(arr[i]);
                double nextCenterVal = getCenterPrice(arr[i + 1]);
                double curAmplitude = (nextCenterVal - curCenterVal) / curCenterVal;
                if (curAmplitude < -m_trendDayAmplitude)
                {
                    if (retracementIndex != -1)
                    {
                        double chgFromRtr = (arr[i].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                        if (chgFromRtr < -m_sidewayAmplitudeLimit)
                        {
                            retracementIndex = -1;
                        }
                    }
                    continue;
                }
                if (curAmplitude > m_sidewayAmplitudeLimit)
                {
                    if (arr[i].latestPrice < arr[i].ma5.avgPrice &&
                        arr[i + 1].latestPrice > arr[i + 1].ma5.avgPrice)
                    {
                        break;
                    }
                }
                if (retracementIndex == -1)
                {
                    retracementIndex = i;
                }

                double chg = (arr[i + 1].latestPrice - arr[retracementIndex].latestPrice) / arr[retracementIndex].latestPrice;
                if (chg > m_retracementLimit)
                {
                    break;
                }
            }

            if (i == arr.Count)
            {
                return true;
            }
            return false;
        }

        protected static bool accordSideway(List<StockKLineBaidu> arr)
        {
            if (arr == null || arr.Count < 2)
            {
                return false;
            }

            int i = 0;
            for(i = 0; i < arr.Count; i++)
            {
                double curCenterVal = getCenterPrice(arr[i]);
                double nextCenterVal = getCenterPrice(arr[i + 1]);
                double curAmplitude = (nextCenterVal - curCenterVal) / curCenterVal;
                if(Math.Abs(curAmplitude) > m_sidewayAmplitudeLimit)
                {
                    break;
                }
            }

            if (i == arr.Count)
            {
                return true;
            }
            return false;
        }

        public static double getAvgVolume(List<StockKLineBaidu> arr, int index, int days = 5)
        {
            int count = Math.Min(index, days);
            int startIndex = index - count;
            long accumVol = 0;
            for(int i = startIndex; i < count + startIndex; i++)
            {
                accumVol += arr[i].volume;
            }

            if(count > 0)
            {
                return accumVol / count;
            }
            return 0.0;
        }
    }
}
