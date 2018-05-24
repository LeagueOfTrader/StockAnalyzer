using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    public class KLineAnalyzer
    {
        readonly int[] m_maDays = {5, 10 , 20, 21, 30, 60, 68};
        double m_lowPrice = 0.0;
        double m_highPrice = 0.0;

        List<StockKLine> m_kLineData = null;
        Dictionary<int, MovingAverage> m_maDataMap = new Dictionary<int, MovingAverage>();
        StockMarketData m_curMarketData = null;

        public KLineAnalyzer(List<StockKLine> data, StockMarketData md)
        {
            m_kLineData = data;
            m_curMarketData = md;

            init();
        }

        protected void init()
        {
            KLineDataProcessor.calcKLineExtremum(m_kLineData, out m_highPrice, out m_lowPrice);

            for(int i = 0; i < m_maDays.Length; i++)
            {
                int days = m_maDays[i];
                m_maDataMap.Add(days, new KLineMA(m_kLineData, days));
            }
        }

        public MovingAverage getMAData(int days)
        {
            if (m_maDataMap.ContainsKey(days))
            {
                return m_maDataMap[days];
            }

            return null;
        }

        public double getCurPricePos()
        {
            double range = m_highPrice - m_lowPrice;
            double waterLevel = (m_curMarketData.latestPrice - m_lowPrice) / range;
            return waterLevel;
        }

        

        // moving average analyze
        public bool isLongStableUptrendByMA()
        {
            MovingAverage ma10 = getMAData(10);
            MovingAverage ma21 = getMAData(21);
            MovingAverage ma68 = getMAData(68);

            if(ma10.getLastDiff() > 0.0 && ma21.getLastDiff() > 0.0 && ma68.getLastDiff() > 0.0)
            {
                return true;
            }

            return false;
        }

        // price volume analyze
        // 量价齐升
        public bool isVolPrcAllUp()
        {
            if(m_curMarketData.volumeRatio < KLinePriceVolumeParam.VOL_RATIO_UPTREND)
            {
                return false;
            }

            int last = m_kLineData.Count - 1;
            for(int i = 0; i < 3; i++)
            {
                if(m_kLineData[last - i].getChangePercent() < KLinePriceVolumeParam.PRICE_CHG_PCT)
                {
                    return false;
                }
            }

            return true;
        }

        // 量价背离 价创新高 量未创新高
        public bool isVolPrcDeviate()
        {
            if(Math.Abs(m_highPrice - m_curMarketData.highestPrice) > double.Epsilon)
            {
                return false;
            }

            int last = m_kLineData.Count - 1;
            long lastHighestVolume = 0;
            for(int i = 1; i < KLinePriceVolumeParam.REFERENCE_DAYS; i++)
            {
                if(m_kLineData[last - i].volume > lastHighestVolume)
                {
                    lastHighestVolume = m_kLineData[last - i].volume;
                }
            }

            if (m_curMarketData.volume - lastHighestVolume < double.Epsilon)
            {
                return false;
            }

            return true;
        }

        // 价升量减
        public bool isPrcUpVolDown()
        {
            if(m_curMarketData.getChangePercent() < KLinePriceVolumeParam.PRICE_CHG_PCT)
            {
                return false;
            }

            if(m_curMarketData.volumeRatio > KLinePriceVolumeParam.VOL_RATIO_DOWNTREND)
            {
                return false;
            }

            return true;
        }

        // 量价井喷
        public bool isVolPrcExplode()
        {
            if(m_curMarketData.volumeRatio < KLinePriceVolumeParam.VOL_RATIO_EXPLOSION)
            {
                return false;
            }

            if(m_curMarketData.getChangePercent() < KLinePriceVolumeParam.PRICE_CHG_PCT_EXPLOSION)
            {
                return false;
            }

            return true;
        }

        // 放量滞涨
        public bool isVolUpPrcStagflation()
        {
            if(m_curMarketData.volumeRatio < KLinePriceVolumeParam.VOL_RATIO_UPTREND)
            {
                return false;
            }

            if(m_curMarketData.getChangePercent() > KLinePriceVolumeParam.PRICE_CHG_PCT)
            {
                return false;
            }

            MovingAverage ma10 = getMAData(10);
            if(ma10.getLastDiff() < 0.0)
            {
                return false;
            }

            return true;
        }

        // 二次探底缩量
        public bool isPrcTouchBottomTwiceVolShrink()
        {
            if(m_curMarketData.volumeRatio > KLinePriceVolumeParam.VOL_RATIO_SHRINK)
            {
                return false;
            }

            if (m_curMarketData.lowestPrice > m_lowPrice) {
                if (Math.Abs(m_curMarketData.lowestPrice - m_lowPrice) > KLinePriceVolumeParam.PRICE_DIFF_THRESHOLD)
                {
                    return false;
                }
            }

            MovingAverage ma5 = getMAData(5);
            if(ma5.getLastDiff() > 0)
            {
                return false;
            }

            bool negative = true;
            int last = m_kLineData.Count - 1;
            int i = 1;
            for(; i < KLinePriceVolumeParam.REFERENCE_DAYS; i++)
            {
                double diff = ma5.getDiffByReversedIndex(i);
                if (negative)
                {
                    if(diff < 0.0)
                    {
                        continue;
                    }
                    else
                    {
                        negative = false;
                    }
                }
                else
                {
                    if(diff > 0.0)
                    {
                        continue;
                    }
                    else
                    {
                        if (m_kLineData[last - i].lowestPrice > m_lowPrice)
                        {
                            if (Math.Abs(m_kLineData[last - i].lowestPrice - m_lowPrice) > KLinePriceVolumeParam.PRICE_DIFF_THRESHOLD)
                            {
                                negative = true;
                                continue;
                            }
                        }
                        break;
                    }
                }
            }

            if(i == KLinePriceVolumeParam.REFERENCE_DAYS)
            {
                return false;
            }

            return true;
        }

        // 放量下探 
        public bool isVolUpPrcDown()
        {
            if(m_curMarketData.volumeRatio < KLinePriceVolumeParam.VOL_RATIO_UPTREND)
            {
                return false;
            }

            MovingAverage ma5 = getMAData(5);

            if (m_curMarketData.latestPrice > ma5.Last)
            {
                return false;
            }

            double prcDownPct = (ma5.Last - m_curMarketData.latestPrice) / ma5.Last;
            if(prcDownPct < KLinePriceVolumeParam.PRICE_CHG_PCT_EXPLOSION)
            {
                return false;
            }

            return true;
        }

        // 高位放量破均线
        public bool isVolUpPrcBreakMAInHighPos()
        {
            double pos = getCurPricePos();
            if(pos < 1.0 - KLinePriceVolumeParam.PRICE_RELATIVE_POS)
            {
                return false;
            }

            MovingAverage ma5 = getMAData(5);
            MovingAverage ma10 = getMAData(10);
            MovingAverage ma30 = getMAData(30);

            if(m_curMarketData.highestPrice > ma5.Last &&
               m_curMarketData.highestPrice > ma10.Last &&
               m_curMarketData.highestPrice > ma30.Last)
            {
                return true;
            }

            return false;
        }
    }
}
