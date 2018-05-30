using StockAnalyzer.DataAnalyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    public class SSDMPriceScale : StockSortableMetadata
    {
        private string m_beginDate = "";
        
        public SSDMPriceScale(string code, string beginDate = "20180201") : base(code)
        {
            m_beginDate = beginDate;
        }

        protected override double calcRefData(string code)
        {
            double ratio = 0.0;
            if(!PriceAnalyzer.getPriceScaleFromDate(code, m_beginDate, out ratio))
            {
                return 0.0;
            }
            return ratio;
        }
    }
}
