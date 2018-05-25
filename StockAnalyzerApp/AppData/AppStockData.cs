using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppData
{
    class AppStockData : Singleton<AppStockData>
    {
        public AppStockList m_srcList = new AppStockList();
        public AppStockList m_screenedList = new AppStockList();
        public AppStockList m_selfSelectedList = new AppStockList();
    }
}
