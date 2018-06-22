using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class QuarterNetProfitFilter : AnnualNetProfitFilter
    {
        public QuarterNetProfitFilter(string targetYear, string targetQuarter, int yearsCount = 1, double netProfitThreshold = 0) 
            : base(targetYear, targetQuarter, yearsCount, netProfitThreshold)
        {
        }

        public override bool filterMethod(string stockID)
        {
            int i = 1;
            int targetYear = int.Parse(m_targetYear);
            int targetQuarter = int.Parse(m_targetQuarter);
            for (int q = 1; q <= targetQuarter; q++) {
                string strQuarter = q.ToString();
                if (getStockNetProfit(stockID, m_targetYear, strQuarter) <= m_threshold)
                {
                    return false;
                }
           }

            while (i < m_yearsCount)
            {
                targetYear--;
                string strYear = targetYear.ToString();
                for (int q = 1; q <= 4; q++)
                {
                    string strQuarter = q.ToString();
                    double netProfit = getStockNetProfit(stockID, strYear, strQuarter);
                    if (netProfit <= m_threshold)
                    {
                        return false;
                    }
                }

                i++;
            }

            return true;
        }
    }
}
