using StockAnalyzer.Common;
using StockAnalyzerApp.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppGlobal
{
    class AppGlobalCache : Singleton<AppGlobalCache>
    {
        AppStockList m_targetList = null;

        public AppStockList getTargetList()
        {
            return m_targetList;
        }

        public void setTargetList(AppStockList target)
        {
            m_targetList = target;
        }
    }
}
