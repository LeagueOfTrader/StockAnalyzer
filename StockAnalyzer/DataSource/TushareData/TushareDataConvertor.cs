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

            if (src.Count >= 11)
            {
                data = new StockReportData();
                data.code = src[0];
                data.name = src[1];
                data.eps = parseTsDoubleValue(src[2]);
                data.eps_yoy = parseTsDoubleValue(src[3]);
                data.bvps = parseTsDoubleValue(src[4]);
                data.roe = parseTsDoubleValue(src[5]);
                data.epcf = parseTsDoubleValue(src[6]);
                data.net_profits = parseTsDoubleValue(src[7]);
                data.profits_yoy = parseTsDoubleValue(src[8]);
                data.distrib = src[9];
                data.report_date = src[10];
            }

            return data;
        }

        public static StockCashflowData parseStockCashflowData(List<string> src)
        {
            StockCashflowData data = null;
            if (src.Count >= 7)
            {
                data = new StockCashflowData();
                data.code = src[0];
                data.name = src[1];
                data.cf_sales = parseTsDoubleValue(src[2]);
                data.rateOfReturn = parseTsDoubleValue(src[3]);
                data.cf_nm = parseTsDoubleValue(src[4]); ;
                data.cf_liabilities = parseTsDoubleValue(src[5]);
                data.cashflowRatio = parseTsDoubleValue(src[6]);
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
                data.mbrg = parseTsDoubleValue(src[2]);
                data.nprg = parseTsDoubleValue(src[3]);
                data.nav = parseTsDoubleValue(src[4]);
                data.targ = parseTsDoubleValue(src[5]);
                data.epsg = parseTsDoubleValue(src[6]);
                data.seg = parseTsDoubleValue(src[7]);
            }
            return data;
        }

        private static double parseTsDoubleValue(string src)
        {
            double target = 0.0;
            try
            {
                target = double.Parse(src);
            }
            catch(Exception e)
            {
                //nan
            }
            return target;
        }
    }
}
