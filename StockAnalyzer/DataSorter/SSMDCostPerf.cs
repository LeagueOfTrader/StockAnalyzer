using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    class SSMDCostPerf : StockSortableMetadata
    {
        public SSMDCostPerf(string code) : base(code)
        {
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;

            double histData = CostPerfFilter.getMaxCostRefValueBefore(code, year, quarter);
            double curData = CostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
