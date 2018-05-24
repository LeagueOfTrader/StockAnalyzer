using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public abstract class NumericStockFilter : StockFilter
    {
        protected string m_filterDesc = "";

        public string getFilterDesc()
        {
            return m_filterDesc;
        }

        public abstract bool getNumericValue(string stockID, out double val);
    }
}
