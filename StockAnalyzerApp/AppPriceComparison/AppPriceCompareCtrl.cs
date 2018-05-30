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
    class AppPriceCompareItem //: INotifyPropertyChanged
    {
        public string m_code;
        public double m_curPrice = 0.0;
        public double m_lowestPrice = 0.0;
        public double m_chgFromLowest = 0.0;

        public AppPriceCompareItem(string stockID)
        {
            m_code = stockID;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public double curPrice
        //{
        //    set
        //    {
        //        m_curPrice = value;
        //        if(PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("CurPrice"));
        //        }
        //    }
        //    get
        //    {
        //        return m_curPrice;
        //    }
        //}

        //public double chgFromLow
        //{
        //    set
        //    {
        //        m_chgFromLowest = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("ChgFromLow"));
        //        }
        //    }
        //    get
        //    {
        //        return m_chgFromLowest;
        //    }
        //}

        public virtual void init(string beginDate, string endDate)
        {
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

    class AppPriceCompareCtrl : Singleton<AppPriceCompareCtrl>
    {
        public List<AppPriceCompareItem> m_priceCompList = new List<AppPriceCompareItem>();
        public ObservableCollection<AppPriceCompareItem> m_priceCompCollection = new ObservableCollection<AppPriceCompareItem>();
        //private bool m_dirty = true;
        private string m_beginDate = "20180201";
        private string m_endDate = "";

        public void refresh(List<string> stocks, string beginDate, string endDate)
        {
            m_priceCompList.Clear();
            //m_priceCompCollection.Clear();
            foreach (string stockID in stocks)
            {
                AppPriceCompareItem item = new AppPriceCompareItem(stockID);
                StockDataCenter.getInstance().subscribeMarketData(stockID);
                m_priceCompList.Add(item);
                //m_priceCompCollection.Add(item);
            }
            m_beginDate = beginDate;
            m_endDate = endDate;
            //m_dirty = true;
            reset();
        }

        protected void reset()
        {
            foreach (AppPriceCompareItem item in m_priceCompList) //m_priceCompCollection)
            {
                //item.m_lowestPrice = StockDataCache.getInstance().getLowestPriceFromDate(item.m_code, m_date);
                item.init(m_beginDate, m_endDate);
            }
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
