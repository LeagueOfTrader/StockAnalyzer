using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    class PoDiFanPattern : KLinePattern
    {
        public override bool isMatch(List<StockKLineBaidu> kLineData)
        {
            int lowIndex = TrendAnalyzer.getLowestPositionIndex(kLineData);
            if(lowIndex < 6)
            {
                return false;
            }

            List<StockKLineBaidu> prevKLineData = kLineData.GetRange(0, lowIndex + 1);
            int prevLowIndex = TrendAnalyzer.getLowestPositionIndex(prevKLineData);
            List<StockKLineBaidu> midKLineData = prevKLineData.GetRange(prevLowIndex, prevKLineData.Count - prevLowIndex);
            int recoverHighIndex = prevLowIndex + TrendAnalyzer.getHighestPositionIndex(midKLineData);

            int startIndex = TrendAnalyzer.getInflectionPoint(prevKLineData, prevKLineData.Count - 1, TrendType.TT_Down, true);
            if(kLineData[startIndex].getCenterPrice() < kLineData[recoverHighIndex].getCenterPrice() ||
                kLineData[startIndex].lowestPrice < kLineData[recoverHighIndex].lowestPrice ||
                kLineData[startIndex].latestPrice < kLineData[recoverHighIndex].latestPrice)
            {
                return false;
            }

            //double centerVal = (kLineData[prevLowIndex].getCenterPrice() - kLineData[lowIndex].getCenterPrice()) / kLineData[lowIndex].getCenterPrice();
            double lowVal = (kLineData[prevLowIndex].lowestPrice - kLineData[lowIndex].lowestPrice) / kLineData[lowIndex].lowestPrice;
            if(lowVal > 0.1)
            {
                return false;
            }

            List<StockKLineBaidu> recoverPhase = kLineData.GetRange(prevLowIndex, recoverHighIndex - prevLowIndex + 1);
            if (!TrendAnalyzer.accordTrend(recoverPhase, TrendType.TT_Up))
            {
                return false;
            }

            List<StockKLineBaidu> againDownPhase = kLineData.GetRange(recoverHighIndex, lowIndex);
            if(!TrendAnalyzer.accordTrend(againDownPhase, TrendType.TT_Down))
            {
                return false;
            }

            List<StockKLineBaidu> lastPhase = kLineData.GetRange(lowIndex, kLineData.Count - lowIndex);
            int passRecoverHighPosIndex = TrendAnalyzer.getInflectionPoint(kLineData, lowIndex, TrendType.TT_Up);//getHighestPositionIndex(lastPhase);

            List<StockKLineBaidu> reversalPhase = kLineData.GetRange(lowIndex, passRecoverHighPosIndex - lowIndex + 1);
            if(!TrendAnalyzer.accordTrend(reversalPhase, TrendType.TT_NotDown))
            {
                return false;
            }

            if(kLineData[lowIndex].latestPrice >= kLineData[passRecoverHighPosIndex].latestPrice)
            {
                return false;
            }

            //if(kLineData[passRecoverHighPosIndex].volume)

            return true;
        }
    }
}
