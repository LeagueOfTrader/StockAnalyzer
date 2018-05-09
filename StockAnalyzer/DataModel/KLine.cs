using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    class KLine
    {
        public double openPrice;
        public double latestPrice; // == closePrice
        public double highestPrice;
        public double lowestPrice;

        public static KLine merge(List<KLine> kLines)
        {            
            if(kLines.Count == 0)
            {
                return null;
            }

            KLine kl = new KLine();
            kl.openPrice = kLines[0].openPrice;
            kl.latestPrice = kLines[kLines.Count - 1].latestPrice;

            kl.highestPrice = kLines[0].highestPrice;
            kl.lowestPrice = kLines[0].lowestPrice;

            for(int i = 0; i < kLines.Count; i++)
            {
                if(kLines[i].highestPrice > kl.highestPrice)
                {
                    kl.highestPrice = kLines[i].highestPrice;
                }

                if(kLines[i].lowestPrice < kl.lowestPrice)
                {
                    kl.lowestPrice = kLines[i].lowestPrice;
                }
            }

            return kl;
        }

        public KLineType getYinYangType()
        {
            if (latestPrice > openPrice)
            {
                return KLineType.LT_Yang;
            }
            else if (latestPrice < openPrice)
            {
                return KLineType.LT_Yin;
            }

            return KLineType.LT_None;
        }

        public KLineStrength getKLineStrength()
        {
            var val = Math.Abs(latestPrice - openPrice);
            var ratio = val / openPrice;
            if (ratio < KLineConstant.FLUCTUATION_LV1)
            {
                return KLineStrength.LS_Micro;
            }
            else if (ratio < KLineConstant.FLUCTUATION_LV2)
            {
                return KLineStrength.LS_Small;
            }
            else if (ratio < KLineConstant.FLUCTUATION_LV3)
            {
                return KLineStrength.LS_Medium;
            }
            else
            {
                return KLineStrength.LS_Large;
            }
        }

        public double getSolidLength()
        {
            return Math.Abs(openPrice - latestPrice);
        }

        public double getTotalLength()
        {
            return highestPrice - lowestPrice;
        }

        public double getUpperShadowLength()
        {
            double solidCeil = Math.Max(openPrice, latestPrice);
            return highestPrice - solidCeil;
        }

        public double getLowerShadowLength()
        {
            double solidFloor = Math.Min(openPrice, latestPrice);
            return solidFloor - lowestPrice;
        }

        public bool isCrossShape()
        {
            return getSolidLength() < KLineConstant.LINE_LENGTH_THRESHOLD; 
        }

        public bool isShadowDominant()
        {
            var longerShadow = Math.Max(getUpperShadowLength(), getLowerShadowLength());
            var solidLen = getSolidLength();

            if (longerShadow > solidLen * KLineConstant.SHADOW_SOLID_RATIO_THRESHOLD)
            {
                return true;
            }

            return false;
        }

        public bool hasUpperShadow()
        {
            if (getUpperShadowLength() > double.Epsilon)
            {
                return true;
            }
            return false;
        }

        public bool hasLowerShadow()
        {           
            if (getLowerShadowLength() > double.Epsilon)
            {
                return true;
            }
            return false;
        }

        public bool hasLongUpperShadow()
        {
            double upperShadowLen = getUpperShadowLength();
            double threshold = openPrice * KLineConstant.SHADOW_PRICE_RATIO_THRESHOLD;
            if (upperShadowLen > threshold)
            {
                return true;
            }
            return false;
        }

        public bool hasLongLowerShadow()
        {
            double lowerShadowLen = getLowerShadowLength();
            double threshold = openPrice * KLineConstant.SHADOW_PRICE_RATIO_THRESHOLD;
            if (lowerShadowLen > threshold)
            {
                return true;
            }
            return false;
        }

        public double getChange()
        {
            return latestPrice - openPrice;
        }

        public double getChangePercent()
        {
            double chgPct = getChange() / openPrice;
            return chgPct;
        }

        public double getCenterPrice()
        {
            double centerPrice = (openPrice + latestPrice) / 2.0;
            return centerPrice;
        }
    }

    class StockKLine : KLine
    {
        public string date;
        public long volume;
    }
}
