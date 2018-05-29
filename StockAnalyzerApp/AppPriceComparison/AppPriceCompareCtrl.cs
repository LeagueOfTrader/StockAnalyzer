using StockAnalyzer.Common;
using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
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

        public AppPriceCompareItem(string stockID)
        {
            m_code = stockID;
        }

        public void calcChg()
        {
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

    class AppPriceCompareCtrl : Singleton<AppPriceCompareCtrl>
    {
        public List<AppPriceCompareItem> m_priceCompList = new List<AppPriceCompareItem>();
        private bool m_dirty = true;
        private string m_date = "20180201";

        public void refresh(List<string> stocks, string date)
        {
            m_priceCompList.Clear();
            foreach(string stockID in stocks)
            {
                AppPriceCompareItem item = new AppPriceCompareItem(stockID);
                m_priceCompList.Add(item);
            }
            m_date = date;
            m_dirty = true;
        }

        public void update()
        {
            bool completed = true;
            foreach(AppPriceCompareItem item in m_priceCompList)
            {
                if(m_dirty == true)
                {
                    if(item.m_lowestPrice <= 0.0 &&
                        !PriceAnalyzer.getLowestPriceFromDate(item.m_code, m_date, out item.m_lowestPrice))
                    {
                        completed = false;
                    }
                }
            }

            if (m_dirty)
            {
                if (completed)
                {
                    m_dirty = false;
                }
            }

            foreach(AppPriceCompareItem item in m_priceCompList)
            {
                StockMarketData md = StockDataCenter.getInstance().getMarketData(item.m_code);
                if (md != null)
                {
                    item.m_curPrice = md.latestPrice;
                }

                if (item.m_lowestPrice > 0.0)
                {
                        item.calcChg();
                }
            }
        }
    }
}
