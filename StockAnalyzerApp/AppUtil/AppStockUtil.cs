using StockAnalyzerApp.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppUtil
{
    class AppStockUtil
    {
        public static void addItem(AppStockList targetList, string item)
        {
            if(targetList != null && targetList.stocks != null)
            {
                targetList.stocks.Add(item);
            }
        }

        public static void removeItem(AppStockList targetList, string item)
        {
            if (targetList != null && targetList.stocks != null)
            {
                targetList.stocks.Remove(item);
            }
        }
    }
}
