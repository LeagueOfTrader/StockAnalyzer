using StockAnalyzer.DataSource.TushareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class AnnualNetProfitFilter : StockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetQuarter = "4";
        protected int m_yearsCount = 1;
        protected double m_threshold = 0.0;

        public AnnualNetProfitFilter(string targetYear, string targetQuarter, int yearsCount = 1, double netProfitThreshold = 0.0)
        {
            m_targetYear = targetYear;
            m_targetQuarter = targetQuarter;
            m_yearsCount = yearsCount;
            m_threshold = netProfitThreshold;
        }

        public override bool filterMethod(string stockID)
        {
            int i = 1;
            int targetYear = int.Parse(m_targetYear);
            if(getStockNetProfit(stockID, m_targetYear, m_targetQuarter) <= m_threshold)
            {
                return false;
            }

            while(i < m_yearsCount)
            {
                targetYear--;
                string strYear = targetYear.ToString();
                double netProfit = getStockNetProfit(stockID, strYear, "4");
                if(netProfit <= m_threshold)
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        public static double getStockNetProfit(string stockID, string year, string quarter)
        {
            StockProfitData pd = StockDBVisitor.getInstance().getStockProfitData(stockID, year, quarter);
            if(pd != null)
            {
                return pd.net_profit;
            }

            return 0.0;
        }
    }
}
