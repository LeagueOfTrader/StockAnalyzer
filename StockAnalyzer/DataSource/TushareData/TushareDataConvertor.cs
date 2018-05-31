using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource.TushareData
{
    public class TushareDataConvertor
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

        public static StockProfitData parseStockProfitData(List<string> src)
        {
            StockProfitData data = null;
            if(src != null && src.Count >= 9)
            {
                try
                {
                    data = new StockProfitData();
                    data.code = src[0];
                    data.name = src[1];
                    data.roe = double.Parse(src[2]);
                    data.net_profit_ratio = double.Parse(src[3]);
                    data.gross_profit_ratio = double.Parse(src[4]);
                    data.net_profit = double.Parse(src[5]);
                    data.eps = double.Parse(src[6]);
                    data.income = double.Parse(src[7]);
                    data.ips = double.Parse(src[8]);
                }
                catch (Exception e)
                {
                    //
                }
            }

            return data;
        }

        protected static StockPerformanceForecastType parseForecastType(string str)
        {
            StockPerformanceForecastType type = StockPerformanceForecastType.PFT_Alert;
            if(str == "预增")
            {
                type = StockPerformanceForecastType.PFT_Increase;
            }
            else if(str == "预升")
            {
                type = StockPerformanceForecastType.PFT_Advance;
            }
            else if (str == "预盈")
            {
                type = StockPerformanceForecastType.PFT_Profit;
            }
            else if (str == "预亏")
            {
                type = StockPerformanceForecastType.PFT_Loss;
            }
            else if (str == "预减")
            {
                type = StockPerformanceForecastType.PFT_Recede;
            }
            else if (str == "预降")
            {
                type = StockPerformanceForecastType.PFT_Decrease;
            }
            else if (str == "预警")
            {
                type = StockPerformanceForecastType.PFT_Alert;
            }

            return type;
        }

        protected static void parseForecastRange(string str, out double floor, out double ceil)
        {
            floor = 0.0;
            ceil = 0.0;
            if(str == null)
            {
                return;
            }

            if(str == "nan")
            {
                return;
            }

            string[] strArr = str.Split('~');
            string strFloor = null;
            string strCeil = null;
            if(strArr.Length == 1)
            {
                strFloor = strArr[0];
                strCeil = strArr[0];
            }
            else if(strArr.Length == 2)
            {
                strFloor = strArr[0];
                strCeil = strArr[1];
            }

            if(strFloor.Last() == '%')
            {
                strFloor = strFloor.Substring(0, strFloor.Length - 1);
            }
            floor = double.Parse(strFloor);
            floor *= 0.01;

            if(strCeil.Last() == '%')
            {
                strCeil = strCeil.Substring(0, strCeil.Length - 1);
            }
            ceil = double.Parse(strCeil);
            ceil *= 0.01;
        }

        public static StockForecastData parseStockForecastData(List<string> src)
        {
            StockForecastData data = null;
            try
            {
                data = new StockForecastData();
                data.code = src[0];
                data.name = src[1];
                data.type = parseForecastType(src[2]);
                data.report_date = src[3];
                data.summary = src[4];
                data.previous_eps = double.Parse(src[5]);
                parseForecastRange(src[6], out data.forecast_chg_floor, out data.forecast_chg_ceil);
            }
            catch(Exception e)
            {
                //
            }

            return data;
        }

        public static StockHolderData parseStockHolderData(List<string> src)
        {
            StockHolderData data = null;
            try
            {
                data = new StockHolderData();
                data.code = src[0];
                data.holders_count = int.Parse(src[1]);
                if (src[2].Equals("不变"))
                {
                    data.count_chg = 0;
                }
                else
                {
                    string strChg = src[2].Substring(0, src[2].Length - 1);
                    data.count_chg = double.Parse(strChg);                    
                }
                string str10ThousandStocks = "万股";
                string strStocks = src[3].Substring(0, src[3].Length - str10ThousandStocks.Length);
                data.avg_stocks = int.Parse(strStocks);
                if (src[4].Equals("不变"))
                {
                    data.avg_stocks_chg = 0;
                }
                else
                {
                    string strChg = src[4].Substring(0, src[4].Length - 1);
                    data.avg_stocks_chg = double.Parse(strChg);
                }
            }
            catch(Exception e)
            {
                //
            }

            return data;
        }
    }
}
