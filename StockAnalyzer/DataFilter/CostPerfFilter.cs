using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class CostPerfFilter : StockFilter
    {
        private string m_year = "2013";
        private string m_season = "1";

        public CostPerfFilter(string year, string season)
        {
            m_year = year;
            m_season = season;
        }

        public override bool filterMethod(string stockID)
        {
            throw new NotImplementedException();
        }
    }
}
