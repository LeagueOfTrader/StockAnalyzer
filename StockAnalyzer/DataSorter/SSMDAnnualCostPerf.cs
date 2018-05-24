using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    public class SSMDAnnualCostPerf : StockSortableMetadata
    {
        public SSMDAnnualCostPerf(string code) : base(code)
        {
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;

            int maxYear = 0;
            double histData = AnnualCostPerfFilter.getMaxAnnualCostRefValueBefore(code, year, out maxYear);
            double curData = AnnualCostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
