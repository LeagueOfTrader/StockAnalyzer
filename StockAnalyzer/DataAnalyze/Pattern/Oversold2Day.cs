using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.DataSource;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    class Oversold2Day : KLinePattern
    {
        const double m_oversoldChg = -0.05;
        const double m_oversoldChg2ndDay = -0.03;
        public override bool isMatch(List<StockKLineBaidu> kLineData)
        {
            if(kLineData.Count < 2)
            {
                return false;
            }

            int lastIndex = kLineData.Count - 1;
            if(kLineData[lastIndex - 1].getChange() > m_oversoldChg)
            {
                return false;
            }

            if(kLineData[lastIndex].getChange() > m_oversoldChg2ndDay)
            {
                return false;
            }

            double lastOpenPriceRatio = (kLineData[lastIndex].openPrice - kLineData[lastIndex - 1].latestPrice) / kLineData[lastIndex - 1].latestPrice;
            if(lastOpenPriceRatio > 0.2)
            {
                return false;
            }

            return true;
        }
    }
}
