using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class IncreaseDaysFilter : StockFilter
    {
        protected int m_daysCount = 1;
        protected double m_increaseAmount = 0.099;
        protected int m_lastDays = 20;

        public IncreaseDaysFilter(double increase = 0.99, int daysCount = 1, int totalDays = 20)
        {
            m_daysCount = daysCount;
            m_increaseAmount = increase;
            m_lastDays = totalDays;
        }

        public override bool filterMethod(string stockID)
        {
            List<StockKLineBaidu> arr = StockDataCenter.getInstance().getKLineBaidu(stockID);
            if(arr == null || arr.Count == 0)
            {
                return false;
            }
            int days = Math.Min(m_lastDays, arr.Count - 1);
            int count = 0;
            for (int i = 0; i < days; i++) {
                int index = arr.Count - 1 - i;
                double chg = (arr[index].latestPrice - arr[index - 1].latestPrice) / arr[index - 1].latestPrice;
                if(chg >= m_increaseAmount)
                {
                    count++;
                }
            }

            if (count < m_daysCount)
            {
                return false;
            }

            return true;
        }
    }
}
