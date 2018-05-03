using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    class TushareDataConvertor
    {
        public static StockReportData parseStockReportData(List<string> src)
        {
            StockReportData data = null;

            if(src.Count >= 11)
            {
                data = new StockReportData();
                data.code = src[0];
                data.name = src[1];
                data.eps = double.Parse(src[2]);
                data.eps_yoy = double.Parse(src[3]);
                data.bvps = double.Parse(src[4]);
                data.roe = double.Parse(src[5]);
                data.epcf = double.Parse(src[6]);
                data.net_profits = double.Parse(src[7]);
                data.profits_yoy = double.Parse(src[8]);
                data.distrib = src[9];
                data.report_date = src[10];
            }

            return data;
        }

        public static StockCashflowData parseStockCashflowData(List<string> src)
        {
            StockCashflowData data = null;
            if(src.Count >= 7)
            {
                data = new StockCashflowData();
                data.code = src[0];
                data.name = src[1];
                data.cf_sales = double.Parse(src[2]);
                data.rateOfReturn = double.Parse(src[3]);
                data.cf_nm = double.Parse(src[4]); ;
                data.cf_liabilities = double.Parse(src[5]); 
                data.cashflowRatio = double.Parse(src[6]); 
            }
            return data;
        }

        public static StockGrowthData parseStockGrowthData(List<string> src)
        {
            StockGrowthData data = null;
            if (src.Count >= 8)
            {
                data = new StockGrowthData();
                data.code = src[0];
                data.name = src[1];
                data.mbrg = double.Parse(src[2]);
                data.nprg = double.Parse(src[3]);
                data.nav = double.Parse(src[4]);
                data.targ = double.Parse(src[5]);
                data.epsg = double.Parse(src[6]);
                data.seg = double.Parse(src[7]);
            }
            return data;
        }
    }
}
