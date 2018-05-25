using StockAnalyzer.DataSource.TushareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class NetProfitRatioFilter : NumericStockFilter
    {
        protected string m_targetYear = "2018";
        protected string m_targetSeason = "1";
        protected double m_netProfitRatio = 0.0;

        public NetProfitRatioFilter(string targetYear, string targetSeason, double npr)
        {
            m_targetYear = targetYear;
            m_targetSeason = targetSeason;
            m_netProfitRatio = npr;

            m_filterDesc = "NetProfitRatio";
        }

        public override bool filterMethod(string stockID)
        {
            double npr = 0.0;
            if(getNumericValue(stockID, out npr))
            {
                if(npr >= m_netProfitRatio)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool getNumericValue(string stockID, out double val)
        {
            double npr = getStockNetProfitRatio(stockID, m_targetYear, m_targetSeason);
            if(npr > -double.MaxValue)
            {
                val = npr;
                return true;
            }

            val = 0.0;
            return false;
        }

        public static double getStockNetProfitRatio(string stockID, string year, string quarter)
        {
            StockProfitData pd = StockDBVisitor.getInstance().getStockProfitData(stockID, year, quarter);
            if (pd == null)
            {
                return -double.MaxValue;
            }

            return pd.net_profit_ratio;
        }
    }
}
