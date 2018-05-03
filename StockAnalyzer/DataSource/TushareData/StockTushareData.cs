using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    class StockReportData
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

    class StockGrowthData
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

    class StockCashflowData
    {
        public string code;
        public string name;
        public double cf_sales;
        public double rateOfReturn;
        public double cf_nm;
        public double cf_liabilities;
        public double cashflowRatio;
    }
}
