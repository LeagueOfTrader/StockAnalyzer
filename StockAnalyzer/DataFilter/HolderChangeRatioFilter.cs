using StockAnalyzer.DataSource.TushareData;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class HolderChangeRatioFilter : HolderCountTrendFilter
    {
        private double m_accumRatio = 0.0;
        public HolderChangeRatioFilter(string year, string quarter, double accumRatio, 
                                        int continousQuarter = 1, bool allowInvariant = true) 
                                        : base(year, quarter, continousQuarter, allowInvariant)
        {
            m_accumRatio = accumRatio;
        }

        public override bool filterMethod(string stockID)
        {
            string year = m_targetYear;
            string quarter = m_targetQuarter;
            for (int i = 0; i < m_continousQuarter; i++)
            {
                if (i != 0)
                {
                    string sy = year;
                    string sq = quarter;
                    DateUtil.getPrevQuarter(sy, sq, out year, out quarter);
                }
            }

            int curCount = 0, lastCount = 0;
            if(getStockHolderCount(stockID, m_targetYear, m_targetQuarter, out curCount)
                && getStockHolderCount(stockID, year, quarter, out lastCount))
            {
                double ratio = (double)(curCount - lastCount) / (double)(lastCount);
                if(ratio < m_accumRatio)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
