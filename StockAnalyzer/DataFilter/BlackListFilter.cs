using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class BlackListFilter : StockFilter
    {
        public override bool filterMethod(string stockID)
        {
            if (isInBlackList(stockID))
            {
                return false;
            }

            return true;
        }

        public static bool isInBlackList(string stockID)
        {
            return StockPool.getInstance().blackList.Contains(stockID);
        }
    }
}
