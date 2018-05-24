using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    public class SSMDYoyCostPerf : StockSortableMetadata
    {
        public SSMDYoyCostPerf(string code) : base(code)
        {
        }

        protected override double calcRefData(string code)
        {
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            string refYear = GlobalConfig.getInstance().defaultRefYear;

            double histData = CostPerfFilter.calcCostRefValue(code, refYear, quarter);
            double curData = CostPerfFilter.calcCurCostRefValue(code, year, quarter);

            return curData / histData;
        }
    }
}
