using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    class Oversold2Day : KLinePattern
    {
        public override bool isMatch(List<StockKLineBaidu> kLineData)
        {
            if(kLineData.Count < 2)
            {
                return false;
            }

            int lastIndex = kLineData.Count - 1;

            double refPrice2Days = kLineData[lastIndex - 1].openPrice;
            if (kLineData.Count > 2)
            {
                refPrice2Days = kLineData[lastIndex - 2].latestPrice;
            }

            double chg2Days = (kLineData[lastIndex].latestPrice - refPrice2Days) / refPrice2Days;
            if(chg2Days > -KLineConstant.STRG_LARGE)
            {
                return false;
            }
            
            if(kLineData[lastIndex - 1].getChange() > -KLineConstant.STRG_IMPACT)
            {
                return false;
            }

            if(kLineData[lastIndex].getYinYangType() == KLineType.LT_Yang)
            {
                if (!kLineData[lastIndex].isCrossShape())
                {
                    return false;
                }

                if (kLineData[lastIndex].latestPrice > kLineData[lastIndex - 1].latestPrice)
                {
                    return false;
                }
            }
            else
            {
                double lastOpenPriceRatio = (kLineData[lastIndex].openPrice - kLineData[lastIndex - 1].latestPrice) / (kLineData[lastIndex - 1].openPrice - kLineData[lastIndex - 1].latestPrice);
                if(lastOpenPriceRatio > 0.2)
                {
                    return false;
                }
            }

            double volChg = (double)(kLineData[lastIndex].volume - kLineData[lastIndex - 1].volume) / (double)kLineData[lastIndex - 1].volume;

            if (volChg > 0.5) {
                return false;
            }

            return true;
        }
    }
}
