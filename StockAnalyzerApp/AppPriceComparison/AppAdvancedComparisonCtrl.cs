﻿using StockAnalyzer.DataCache;
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

        public override void init(string beginDate, string endDate)
        {
            base.init(beginDate, endDate);

            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            AvgValInIndustryFilter peAvgFlt = new AvgValInIndustryFilter(new PEFilter(0.0), 0.0);
            peAvgFlt.calcAvgValInIndustry(m_code, out m_peIndustry);
            AvgValInIndustryFilter roeAvgFlt = new AvgValInIndustryFilter(new ROEFilter(year, quarter, 0.0), 0.0);
            roeAvgFlt.calcAvgValInIndustry(m_code, out m_roeIndustry);

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
        
        private string m_beginDate = "20180201";
        private string m_endDate = "";

        public void refresh(List<string> stocks, string beginDate, string endDate)
        {
            m_compList.Clear();
            foreach (string stockID in stocks)
            {
                AppAdvancedCompareItem item = new AppAdvancedCompareItem(stockID);
                StockDataCenter.getInstance().subscribeMarketData(stockID);
                m_compList.Add(item);
            }
            m_beginDate = beginDate;
            m_endDate = endDate;
            reset();
        }

        protected void reset()
        {
            foreach (AppAdvancedCompareItem item in m_compList) 
            {
                item.init(m_beginDate, m_endDate);
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