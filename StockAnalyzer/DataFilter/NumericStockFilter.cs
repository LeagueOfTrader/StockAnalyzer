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
        protected bool m_needLimitValue = false;
        protected double m_limitValue = 0.0;

        public string getFilterDesc()
        {
            return m_filterDesc;
        }

        public abstract bool getNumericValue(string stockID, out double val);

        public double getLimitValue()
        {
            return m_limitValue;
        }

        public bool needLimitValue()
        {
            return m_needLimitValue;
        }

        public virtual bool isValueValid(double val)
        {
            return true;
        }

        public virtual bool compareValueRatio(double srcVal, double targetVal)
        {
            return srcVal > targetVal;
        }
    }
}
