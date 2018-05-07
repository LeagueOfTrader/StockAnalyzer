using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    class KLineMA : MovingAverage
    {
        public KLineMA(List<KLine> kLineData, int days)
        {
            m_days = days;
            m_maData = KLineDataProcessor.calcMAData(kLineData, days);

            m_high = 0.0;
            m_low = double.MaxValue;
            for (int i = 0; i < m_maData.Count; i++)
            {
                if (m_maData[i] > m_high)
                {
                    m_high = m_maData[i];
                }

                if (m_maData[i] < m_low)
                {
                    m_low = m_maData[i];
                }
            }
        }

        public KLineMA(List<StockKLine> kLineData, int days)
        {
            m_days = days;
            m_maData = KLineDataProcessor.calcMAData(kLineData, days);

            m_high = 0.0;
            m_low = double.MaxValue;
            for (int i = 0; i < m_maData.Count; i++)
            {
                if (m_maData[i] > m_high)
                {
                    m_high = m_maData[i];
                }

                if (m_maData[i] < m_low)
                {
                    m_low = m_maData[i];
                }
            }
        }
    }
}
