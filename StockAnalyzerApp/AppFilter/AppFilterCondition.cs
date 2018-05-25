using StockAnalyzer.Assist;
using StockAnalyzer.DataFilter;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppFilter
{
    enum AppFilterType
    {
        FLTT_Cost,
        FLTT_PE,
        FLTT_ROE,
        FLTT_NetProfitRatio,
        FLTT_InIndustry,
        FLTT_PriceScale,
        FLTT_Distrib,
        FLTT_ExcludeIndustry,
        FLTT_IncludeIndustry,
        FLTT_STStocks
    }

    enum AppCostFilterSubType
    {
        CFST_Annual,
        CFST_Dynamic,
        CFST_Quarter,
        CFST_Yoy,
        CFST_Spec
    }

    enum AppPEFilterSubType
    {
        PEFST_Static,
        PEFST_Dynamic
    }

    enum AppInIndustryFilterSubType
    {
        IIFST_CostAnnual,
        IIFST_CostDynamic,
        IIFST_PEStatic,
        IIFST_PEDynamic,
        IIFST_ROE,
        IIFST_NetProfitRatio
    }

    class AppFilterItem
    {
        public AppFilterType m_type = AppFilterType.FLTT_PE;
        public int m_subType = 0;
        public double m_param1 = 0.0;
        public string m_param2 = "";

        public AppFilterItem(AppFilterType type, int subType = 0, double param1 = 0.0, string param2 = "")
        {
            m_type = type;
            m_subType = subType;
            m_param1 = param1;
            m_param2 = param2;
        }

        public IStockFilter generateFilter()
        {
            IStockFilter filter = null;
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            switch (m_type)
            {
                case AppFilterType.FLTT_Cost:
                    filter = generateCostFilter();
                    break;
                case AppFilterType.FLTT_PE:
                    filter = generatePEFilter();
                    break;
                case AppFilterType.FLTT_InIndustry:
                    filter = generateInIndustryFilter();
                    break;
                case AppFilterType.FLTT_PriceScale:
                    filter = new PriceScaleFilter(m_param1);
                    break;
                case AppFilterType.FLTT_Distrib:                    
                    filter = new DistribBonusFilter(year, quarter, m_param1);
                    break;
                case AppFilterType.FLTT_ExcludeIndustry:
                    filter = new IndustryExcludeFilter();
                    break;
                case AppFilterType.FLTT_STStocks:
                    filter = new STFilter();
                    break;
                case AppFilterType.FLTT_ROE:
                    filter = new ROESustainedFilter(year, quarter, m_param1, int.Parse(m_param2));
                    break;
                case AppFilterType.FLTT_NetProfitRatio:
                    filter = new NetProfitRatioSustainedFilter(year, quarter, m_param1, int.Parse(m_param2));
                    break;
                case AppFilterType.FLTT_IncludeIndustry:
                    string[] arr = m_param2.Split(';');
                    List<string> industries = new List<string>();
                    industries.AddRange(arr);
                    filter = new IndustryIncludeFilter(industries);
                    break;
                default:
                    break;
            }

            return filter;
        }

        protected IStockFilter generateCostFilter()
        {
            IStockFilter filter = null;
            AppCostFilterSubType cfType = (AppCostFilterSubType)m_subType;
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            switch (cfType)
            {
                case AppCostFilterSubType.CFST_Annual:
                    filter = new AnnualCostPerfFilter(year, quarter, m_param1);
                    break;
                case AppCostFilterSubType.CFST_Dynamic:
                    filter = new CostPerfFilter(year, quarter, m_param1);
                    break;
                case AppCostFilterSubType.CFST_Quarter:
                    filter = new QuarterCostPerfFilter(year, quarter, m_param1);
                    break;
                case AppCostFilterSubType.CFST_Spec:
                    string refYear = GlobalConfig.getInstance().defaultRefYear;
                    filter = new SpecifiedCostPerfFilter(refYear, quarter, year, quarter, m_param1);
                    break;
                case AppCostFilterSubType.CFST_Yoy:
                    filter = new CostPerfYoyFilter(year, quarter, m_param1);
                    break;
                default:
                    break;
            }

            return filter;
        }

        protected IStockFilter generatePEFilter()
        {
            IStockFilter filter = null;
            AppPEFilterSubType peType = (AppPEFilterSubType)m_subType;
            switch (peType)
            {
                case AppPEFilterSubType.PEFST_Dynamic:
                    filter = new PEFilter(m_param1);
                    break;
                case AppPEFilterSubType.PEFST_Static:
                    filter = new DynamicPEFilter(m_param1);
                    break;
                default:
                    break;
            }

            return filter;
        }

        protected IStockFilter generateInIndustryFilter()
        {
            IStockFilter filter = null;
            NumericStockFilter comparer = null;
            AppInIndustryFilterSubType indType = (AppInIndustryFilterSubType)m_subType;
            string year = GlobalConfig.getInstance().curYear;
            string quarter = GlobalConfig.getInstance().curQuarter;
            switch (indType)
            {
                case AppInIndustryFilterSubType.IIFST_CostAnnual:
                    comparer = new AnnualCostPerfFilter(year, quarter, 0.0);
                    break;
                case AppInIndustryFilterSubType.IIFST_CostDynamic:
                    comparer = new CostPerfFilter(year, quarter, 0.0);
                    break;
                case AppInIndustryFilterSubType.IIFST_PEDynamic:
                    comparer = new DynamicPEFilter(0.0);
                    break;
                case AppInIndustryFilterSubType.IIFST_PEStatic:
                    comparer = new PEFilter(0.0);
                    break;
                case AppInIndustryFilterSubType.IIFST_ROE:
                    comparer = new ROEFilter(year, quarter, 0.0);
                    break;
                case AppInIndustryFilterSubType.IIFST_NetProfitRatio:
                    comparer = new NetProfitRatioFilter(year, quarter, 0.0);
                    break;
                default:
                    break;
            }

            if (comparer != null)
            {
                filter = new AvgValInIndustryFilter(comparer, m_param1);
            }

            return filter;
        }
    }

    
    class AppFilterCondition
    {
        public static AppFilterType[] ms_filterSequence = {
                                                        AppFilterType.FLTT_STStocks,
                                                        AppFilterType.FLTT_ExcludeIndustry,
                                                        AppFilterType.FLTT_IncludeIndustry,
                                                        AppFilterType.FLTT_PE,
                                                        AppFilterType.FLTT_ROE,
                                                        AppFilterType.FLTT_NetProfitRatio,
                                                        AppFilterType.FLTT_Cost,
                                                        AppFilterType.FLTT_PriceScale,
                                                        AppFilterType.FLTT_Distrib,
                                                        AppFilterType.FLTT_InIndustry
                                                       };

        private Dictionary<AppFilterType, AppFilterItem> m_conditionMap = new Dictionary<AppFilterType, AppFilterItem>();

        public void addFilterCondition(AppFilterItem item)
        {
            if(item != null)
            {
                m_conditionMap.Add(item.m_type, item);
            }
        }

        public List<string> doFilter(List<string> stocks)
        {
            List<string> src = new List<string>();
            src.AddRange(stocks);
            List<string> target = new List<string>();
            target.AddRange(stocks);
            for(int i = 0; i < ms_filterSequence.Length; i++)
            {
                AppFilterType type = ms_filterSequence[i];
                if (m_conditionMap.ContainsKey(type))
                {
                    AppFilterItem item = m_conditionMap[type];
                    IStockFilter flt = item.generateFilter();
                    if(flt != null)
                    {
                        target = flt.filter(src);
                        outputInfo(item, src.Count, target.Count);
                        src.Clear();
                        src.AddRange(target);
                    }
                }
            }

            return target;
        }

        protected void outputInfo(AppFilterItem item, int srcCount, int targetCount)
        {
            if(item != null)
            {
                string msg = "Filter type: " + item.m_type.ToString() +
                                ", sub type: " + item.m_subType.ToString() +
                                ", before filter: " + srcCount.ToString() +
                                ", after filter: " + targetCount.ToString() + ".";
                Logger.log(msg);
            }
        }
    }
}
