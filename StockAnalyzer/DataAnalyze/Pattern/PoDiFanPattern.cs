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
            int lowIndex = getLowestPositionIndex(kLineData);
            if(lowIndex < 6)
            {
                return false;
            }

            List<StockKLineBaidu> prevKLineData = kLineData.GetRange(0, lowIndex + 1);
            int prevLowIndex = getLowestPositionIndex(prevKLineData);
            List<StockKLineBaidu> midKLineData = prevKLineData.GetRange(prevLowIndex, prevKLineData.Count - prevLowIndex);
            int recoverHighIndex = prevLowIndex + getHighestPositionIndex(midKLineData);

            int startIndex = getInflectionPoint(prevKLineData, prevKLineData.Count - 1, TrendType.TT_Down, true);
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
            if (!accordTrend(recoverPhase, TrendType.TT_NotDown))
            {
                return false;
            }

            List<StockKLineBaidu> againDownPhase = kLineData.GetRange(recoverHighIndex, lowIndex);
            if(!accordTrend(againDownPhase, TrendType.TT_Down))
            {
                return false;
            }

            //

            return false;
        }
    }
}
