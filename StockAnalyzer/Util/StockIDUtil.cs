using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    public class StockIDUtil
    {
        public static string getPureCode(string stockID)
        {
            if(stockID.Length < 8)
            {
                return "";
            }

            return stockID.Substring(2, 6);
        }
    }
}
