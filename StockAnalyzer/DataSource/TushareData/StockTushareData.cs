using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    public class StockReportData
    {
        public string code;
        public string name;
        public double eps;
        public double eps_yoy;
        public double bvps;
        public double roe;
        public double epcf;
        public double net_profits;
        public double profits_yoy;
        public string distrib;
        public string report_date;
    }

    public class StockGrowthData
    {
        public string code;
        public string name;
        public double mbrg;
        public double nprg;
        public double nav;
        public double targ;
        public double epsg;
        public double seg;
    }

    public class StockCashflowData
    {
        public string code;
        public string name;
        public double cf_sales;
        public double rateOfReturn;
        public double cf_nm;
        public double cf_liabilities;
        public double cashflowRatio;
    }

    public class StockDistribData
    {
        public string code;
        public string rawData;
        public double bonus = 0.0; //现金
        public int deliver = 0; //送股
        public int transfer = 0;  //转增
    }

    public class StockProfitData
    {
        public string code;
        public string name;
        public double roe;
        public double net_profit_ratio;
        public double gross_profit_ratio;
        public double net_profit;
        public double eps;
        public double income;
        public double ips;
    }

    public enum StockPerformanceForecastType
    {
        PFT_Increase = 3,
        PFT_Advance = 2,        
        PFT_Profit = 1,
        PFT_Alert = 0,
        PFT_Loss = -1,
        PFT_Recede = -2,
        PFT_Decrease = -3
    }

    public class StockForecastData
    {
        public string code;
        public string name;
        public StockPerformanceForecastType type;
        public string report_date;
        public string summary;
        public double previous_eps;
        public double forecast_chg_floor;
        public double forecast_chg_ceil;
    }
}
