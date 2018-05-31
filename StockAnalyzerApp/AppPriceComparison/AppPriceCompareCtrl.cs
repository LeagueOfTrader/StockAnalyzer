using StockAnalyzer.Common;
using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataCache;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppPriceComparison
{
    class AppPriceCompareItem
    {
        public string m_code;
        public double m_curPrice = 0.0;
        public double m_lowestPrice = 0.0;
        public double m_chgFromLowest = 0.0;

        public virtual void init(string stockID, string beginDate, string endDate)
        {
            m_code = stockID;
            PriceAnalyzer.getLowestPriceBetweenDate(m_code, beginDate, endDate, out m_lowestPrice);
        }

        public virtual void update()
        {
            StockRealTimeData rd = StockDataCenter.getInstance().getMarketRealTimeData(m_code);
            if (rd != null)
            {
                m_curPrice = rd.latestPrice;
            }

            if (m_lowestPrice > 0.0)
            {
                m_chgFromLowest = (m_curPrice - m_lowestPrice) / m_lowestPrice;
            }
            else
            {
                m_chgFromLowest = 0.0;
            }
        }
    }

    class AppPriceCompareCtrl<T>  : Singleton<AppPriceCompareCtrl<T>> where T : AppPriceCompareItem, new()
    {
        public List<T> m_priceCompList = new List<T>();
        private string m_beginDate = "20180201";
        private string m_endDate = "";

        public void refresh(List<string> stocks, string beginDate, string endDate)
        {
            m_priceCompList.Clear();
            foreach (string stockID in stocks)
            {
                T item = new T();
                item.init(stockID, m_beginDate, m_endDate);
                StockDataCenter.getInstance().subscribeMarketData(stockID);
                m_priceCompList.Add(item);
            }
            m_beginDate = beginDate;
            m_endDate = endDate;
        }

        public void update()
        {
            foreach (AppPriceCompareItem item in m_priceCompList) //m_priceCompCollection)
            {
                item.update();
            }
        }
    }
}
