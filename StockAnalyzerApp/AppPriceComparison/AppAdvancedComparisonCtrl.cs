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
        public double m_curCost = 0.0;
        public double m_histCost = 0.0;
         
        public double m_roe = 0.0;
        public double m_roeIndustry = 0.0;

        public double m_netProfitRatio = 0.0;
        public double m_nprIndustry = 0.0;

        private bool m_basedOnAnnual = true;

        public override void init(string stockID, string beginDate, string endDate)
        {
            base.init(stockID, beginDate, endDate);

            string year = GlobalConfig.getInstance().curYear;
            string curYear = year;
            string quarter = GlobalConfig.getInstance().curQuarter;
            string curQuarter = quarter;
            if (m_basedOnAnnual)
            {
                if(int.Parse(quarter) < 4)
                {
                    year = (int.Parse(year) - 1).ToString();
                    quarter = "4";
                }
            }

            AvgValInIndustryFilter peAvgFlt = new AvgValInIndustryFilter(new PEFilter(0.0), 0.0);
            peAvgFlt.calcAvgValInIndustry(m_code, out m_peIndustry);
            AvgValInIndustryFilter roeAvgFlt = new AvgValInIndustryFilter(new ROEFilter(year, quarter, 0.0), 0.0);
            roeAvgFlt.calcAvgValInIndustry(m_code, out m_roeIndustry);
            AvgValInIndustryFilter nprAvgFlt = new AvgValInIndustryFilter(new NetProfitRatioFilter(year, quarter, 0.0), 0.0);
            nprAvgFlt.calcAvgValInIndustry(m_code, out m_nprIndustry);

            m_roe = ROEFilter.getStockROE(m_code, year, quarter);
            m_netProfitRatio = NetProfitRatioFilter.getStockNetProfitRatio(m_code, year, quarter);
            
            m_histCost = StockDataCache.getInstance().getMaxAnnualCostRefValueBefore(m_code, curYear);
            m_curCost = CostPerfFilter.calcCurCostRefValue(m_code, curYear, curQuarter);
            m_costRatio = m_curCost / m_histCost;

            DynamicPEFilter.calcDynamicPE(m_code, out m_dynamicPE);
        }
    }
}
