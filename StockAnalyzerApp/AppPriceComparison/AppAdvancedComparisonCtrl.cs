using StockAnalyzer.DataCache;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppPriceComparison
{
    class AppAdvancedCompareItem : AppPriceCompareItem
    {
        public double m_dynamicPE = 0.0;
        public double m_peIndustry = 0.0;
         
        public double m_costRatio = 0.0;
         
        public double m_roe = 0.0;
        public double m_roeIndustry = 0.0;

        public AppAdvancedCompareItem(string stockID) : base(stockID)
        {
        }

        public override void init(string date)
        {
            base.init(date);

            string industryName = StockPool.getInstance().getStockIndustry(m_code);
            StockDataCache.getInstance().getAvgValInIndustry("PE", industryName, out m_peIndustry);
            StockDataCache.getInstance().getAvgValInIndustry("ROE", industryName, out m_roeIndustry);

            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            m_roe = ROEFilter.getStockROE(m_code, year, quarter);
        }

        public override void update()
        {
            //
        }
    }


    class AppAdvancedComparisonCtrl
    {
        public List<AppAdvancedCompareItem> m_compList = new List<AppAdvancedCompareItem>();
        
        private string m_date = "20180201";

        public void refresh(List<string> stocks, string date)
        {
            m_compList.Clear();
            foreach (string stockID in stocks)
            {
                AppAdvancedCompareItem item = new AppAdvancedCompareItem(stockID);
                StockDataCenter.getInstance().subscribeMarketData(stockID);
                m_compList.Add(item);
            }
            m_date = date;
            reset();
        }

        protected void reset()
        {
            foreach (AppAdvancedCompareItem item in m_compList) 
            {
                item.init(m_date);
                //
            }
        }

        public void update()
        {
            foreach (AppAdvancedCompareItem item in m_compList) 
            {
                item.update();
            }
        }
    }
}
