using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    abstract class NumericStockFilter : StockFilter
    {
        public abstract bool getNumericValue(string stockID, out double val);
    }
}
