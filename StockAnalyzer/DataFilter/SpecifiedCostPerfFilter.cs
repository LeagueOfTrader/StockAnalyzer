using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class SpecifiedCostPerfFilter : CostPerfFilter
    {
        private string m_srcYear = "2013";
        private string m_srcSeason = "1";

        public SpecifiedCostPerfFilter(string srcYear, string srcSeason, string targetYear, string targetSeason, double ratio) : base(targetYear, targetSeason, ratio)
        {
            m_srcYear = srcYear;
            m_srcSeason = srcSeason;
        }

        protected override double getSrcValue(string stockID)
        {
            double srcVal = calcCostRefValue(stockID, m_srcYear, m_srcSeason);
            return srcVal;
        }
    }
}
