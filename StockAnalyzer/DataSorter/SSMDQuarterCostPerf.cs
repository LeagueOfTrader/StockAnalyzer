using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    public class SSMDQuarterCostPerf : StockSortableMetadata
    {
        public SSMDQuarterCostPerf(string code) : base(code)
        {
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;

            int maxYear = 0, maxQuarter = 0;
            double histData = QuarterCostPerfFilter.getMaxQuarterCostRefValueBefore(code, year, quarter, out maxYear, out maxQuarter);
            double curData = QuarterCostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
