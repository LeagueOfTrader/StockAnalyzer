using StockAnalyzer.DataAnalyze.Pattern;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class KLinePatternFilter : StockFilter
    {
        bool m_definiteDays = true;
        int m_maxDays = 20;
        int m_minDays = 2;
        KLinePattern m_pattern = null;

        public KLinePatternFilter(KLinePattern pattern, bool definite = true, int minDays = 2, int maxDays = 20)
        {
            m_pattern = pattern;
            m_definiteDays = definite;
            m_maxDays = maxDays;
            m_minDays = minDays;
        }

        public override bool filterMethod(string stockID)
        {
            if(m_pattern == null)
            {
                return false;
            }

            string str = StockDataCollector.queryKLineDataBaidu(stockID);
            List<StockKLineBaidu> arr = StockDataConvertor.parseKLineArrayBaiduAdvanced(str);
            if(arr == null)
            {
                return false;
            }

            if (m_definiteDays)
            {
                return m_pattern.isMatch(arr);
            }
            else
            {
                for(int i = m_minDays; i < m_maxDays; i++)
                {
                    int startIndex = arr.Count - i;
                    List<StockKLineBaidu> subArr = arr.GetRange(startIndex, arr.Count - startIndex);
                    if (m_pattern.isMatch(subArr))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
