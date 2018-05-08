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
            //

            return false;
        }
    }
}
