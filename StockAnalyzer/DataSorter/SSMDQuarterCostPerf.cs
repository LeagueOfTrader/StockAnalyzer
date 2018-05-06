using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    class SSMDQuarterCostPerf : StockSortableMetadata
    {
        public SSMDQuarterCostPerf(string code) : base(code)
        {
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;

            double histData = QuarterCostPerfFilter.getMaxQuarterCostRefValueBefore(code, year, quarter);
            double curData = QuarterCostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
