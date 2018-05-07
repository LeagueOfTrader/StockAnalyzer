using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.DataModel;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    class PoDiFanPattern : KLinePattern
    {
        MovingAverage m_ma5 = null;
        MovingAverage m_ma20 = null;

        public override bool isMatch(List<StockKLine> kLineData)
        {
            int lowIndex = getLowestPositionIndex(kLineData);
            if(lowIndex < 6)
            {
                return false;
            }

            m_ma5 = new KLineMA(kLineData, 5);
            m_ma20 = new KLineMA(kLineData, 20);

            List<StockKLine> prevKLineData = kLineData.GetRange(0, lowIndex + 1);
            int prevLowIndex = getLowestPositionIndex(prevKLineData);
            List<StockKLine> midKLineData = prevKLineData.GetRange(prevLowIndex, prevKLineData.Count - prevLowIndex);
            int recoverHighIndex = prevLowIndex + getHighestPositionIndex(midKLineData);
            //

            return false;
        }
    }
}
