using StockAnalyzer.DataSource.TushareData;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class HolderCountTrendFilter : StockFilter
    {
        protected int m_continousQuarter = 1;
        protected bool m_allowInvariant = true;
        protected string m_targetYear = "2018";
        protected string m_targetQuarter = "1";

        public HolderCountTrendFilter(string year, string quarter, int continousQuarter = 1, bool allowInvariant = true)
        {
            m_targetYear = year;
            m_targetQuarter = quarter;
            m_continousQuarter = continousQuarter;
            m_allowInvariant = allowInvariant;
        }

        public override bool filterMethod(string stockID)
        {
            string year = m_targetYear;
            string quarter = m_targetQuarter;
            for(int i = 0; i < m_continousQuarter; i++)
            {
                if(i != 0)
                {
                    string sy = year;
                    string sq = quarter;
                    DateUtil.getPrevQuarter(sy, sq, out year, out quarter);
                }

                double chg = 0.0;
                if(getStockHolderChange(stockID, year, quarter, out chg))
                {
                    if(chg < 0.0)
                    {
                        continue;
                    }

                    if(Math.Abs(chg) < double.Epsilon)
                    {
                        if (m_allowInvariant)
                        {
                            continue;
                        }
                    }

                    return false;
                }
            }

            int curCount = 0, lastCount = 0;
            if (getStockHolderCount(stockID, m_targetYear, m_targetQuarter, out curCount)
                && getStockHolderCount(stockID, year, quarter, out lastCount))
            {
                if(curCount < lastCount)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool getStockHolderChange(string stockID, string year, string quarter, out double chg)
        {
            chg = 0.0;
            StockHolderData hd = StockDBVisitor.getInstance().getStockHolderData(stockID, year, quarter);
            if(hd == null)
            {
                return false;
            }

            chg = hd.count_chg;
            return true;
        }

        public static bool getStockHolderCount(string stockID, string year, string quarter, out int count)
        {
            count = 0;
            StockHolderData hd = StockDBVisitor.getInstance().getStockHolderData(stockID, year, quarter);
            if (hd == null)
            {
                return false;
            }

            count = hd.holders_count;
            return true;
        }
    }
}
