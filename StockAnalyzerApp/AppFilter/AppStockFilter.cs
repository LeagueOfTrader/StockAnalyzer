using StockAnalyzer.Common;
using StockAnalyzerApp.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppFilter
{
    class AppStockFilter : Singleton<AppStockFilter>
    {
        AppFilterCondition m_condition = null;
        List<string> m_srcList = null;
        List<string> m_targetList = null;

        public void start()
        {
            m_condition = new AppFilterCondition();
            m_srcList = AppStockData.getInstance().m_srcList.stocks;
        }

        public void end()
        {
            AppStockData.getInstance().m_screenedList.copy(m_targetList);
        }

        public void addCondition(AppFilterItem item)
        {
            if(m_condition != null)
            {
                m_condition.addFilterCondition(item);
            }
        }

        public void doFilter()
        {
            if(m_condition != null)
            {
                m_targetList = m_condition.doFilter(m_srcList);
            }
        }

        public void clear()
        {
            m_condition = null;
            m_srcList = null;
            m_targetList = null;
        }

        public List<string> getScreenedStocks()
        {
            return m_targetList;
        }

        public List<string> getSrcStocks()
        {
            return m_srcList;
        }
    }
}
