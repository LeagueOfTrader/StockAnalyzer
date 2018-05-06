using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    class SSDMSpecCostPerf : StockSortableMetadata
    {
        string m_refYear;
        string m_refQuarter;

        public SSDMSpecCostPerf(string code, string year, string quarter) : base(code)
        {
            m_refYear = year;
            m_refQuarter = quarter;
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;

            double histData = CostPerfFilter.calcCostRefValue(code, m_refYear, m_refQuarter);
            double curData = CostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
