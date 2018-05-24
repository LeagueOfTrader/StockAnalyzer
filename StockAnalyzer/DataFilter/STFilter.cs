using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class STFilter : StockFilter
    {
        public static bool isSTStock(string stockID)
        {
            string stockName = StockPool.getInstance().getStockName(stockID);
            if (stockName == null)
            {
                return false;
            }

            if (stockName.Contains("ST"))
            {
                return true;
            }

            return false;
        }

        public override bool filterMethod(string stockID)
        {
            if (!isSTStock(stockID))
            {
                return true;
            }

            return false;
        }
    }
}
