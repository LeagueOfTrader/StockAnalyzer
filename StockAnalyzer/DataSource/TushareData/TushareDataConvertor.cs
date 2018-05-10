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

        public static StockDistribData parseStockDistribData(string src, string code = null)
        {
            StockDistribData dd = new StockDistribData();
            if(code != null)
            {
                dd.code = code;
            }
            string bonusStr = "派";
            string deliverStr = "送";
            string transferStr = "转";
            if(src != null && src.Length > 0)
            {
                string[] arr0 = src.Split(new String[] { bonusStr }, StringSplitOptions.None);                
                if(arr0.Length > 1)
                {
                    dd.bonus = double.Parse(arr0[1]);
                }
                string[] arr1 = arr0[0].Split(new String[] { deliverStr }, StringSplitOptions.None);
                if(arr1.Length > 1)
                {
                    dd.deliver = int.Parse(arr1[1]);
                }
                string[] arr2 = arr1[0].Split(new String[] { transferStr }, StringSplitOptions.None);
                if(arr2.Length > 1)
                {
                    dd.transfer = int.Parse(arr2[1]);
                }
            }

            return dd;
        }
    }
}
