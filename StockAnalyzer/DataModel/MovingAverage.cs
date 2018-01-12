using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    class MovingAverage
    {
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
        }

        //
    }
}
