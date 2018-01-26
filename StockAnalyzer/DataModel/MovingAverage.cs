using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    class MovingAverage
    {
        private double m_high = 0.0;
        private double m_low = 0.0;

        private List<double> m_maData = new List<double>();
        public List<double> Data
        {
            get { return m_maData; }
        }

        private int m_days = 1;
        public int Days
        {
            get { return m_days; }
            set { m_days = value; }
        }

        public MovingAverage(List<KLine> kLineData, int days)
        {
            m_days = days;
            m_maData = KLineDataProcessor.calcMAData(kLineData, days);

            m_high = 0.0;
            m_low = double.MaxValue;
            for(int i = 0; i < m_maData.Count; i++)
            {
                if(m_maData[i] > m_high)
                {
                    m_high = m_maData[i];
                }

                if(m_maData[i] < m_low)
                {
                    m_low = m_maData[i];
                }
            }
        }

        public MovingAverage(List<StockKLine> kLineData, int days)
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

        public double Last
        {
            get { return m_maData.Last<double>(); }
        }

        public double NextToLast
        {
            get { return m_maData[m_maData.Count - 2]; }
        }

        public double getLastDiff()
        {
            //if(m_maData.Count < 2)
            //{
            //    return 0.0;
            //}

            int last = m_maData.Count - 1;
            return m_maData[last] - m_maData[last - 1];
        }

        public double getLastDiffRatio()
        {
            double diff = getLastDiff();
            double range = getRange();
            return diff / range;
        }

        public double getRange()
        {
            return m_high - m_low;
        }

        public double getValueByReversedIndex(int index)
        {
            int last = m_maData.Count - 1;
            if(last < index)
            {
                return 0.0;
            }

            return m_maData[last - index];
        }

        public double getDiffByReversedIndex(int index)
        {
            int last = m_maData.Count - 1;
            if (last < index + 1)
            {
                return 0.0;
            }

            return m_maData[last - index] - m_maData[last - index - 1];
        }
    }
}
