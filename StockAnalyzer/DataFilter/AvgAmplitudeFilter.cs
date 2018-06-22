using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class AvgAmplitudeFilter : StockFilter
    {
        protected int m_lastDays = 1;
        protected double m_avgVal = 0.01;
        public AvgAmplitudeFilter(double avgVal, int days = 1)
        {
            m_avgVal = avgVal;
            m_lastDays = days;
        }

        public override bool filterMethod(string stockID)
        {
            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);
            double avgAmp = KLineDataProcessor.calcKLineAverageAmplitude(arr, m_lastDays);
            if(avgAmp < m_avgVal)
            {
                return false;
            }

            return true;
        }
    }
}
