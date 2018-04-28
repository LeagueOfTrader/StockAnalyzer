using StockAnalyzer.DataAnalyze;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockAnalyzer.Assist;
using System.IO;

namespace StockAnalyzer.DataFilter
{
    class StockFilter
    {
        public static List<string> filterStocksByPriceScaleAndPE(List<string> targetStocks, double ratio, double pe)
        {
            List<string> stocks = new List<string>();

            foreach (string stockID in targetStocks)
            {
                String mdStr = StockDataCollector.queryMarketData(stockID);
                StockMarketData md = StockDataConvertor.parseMarketData(mdStr);

                if (md != null &&
                    md.PE <= pe &&
                    md.PE > 0 &&
                    PriceAnalyzer.isPriceScaleSatisfied(stockID, md.latestPrice, ratio))
                {
                    stocks.Add(stockID);
                    Logger.debugOutput(stockID);
                }
            }

            return stocks;
        }

        public static bool isSTStock(string stockID)
        {
            string stockName = StockPool.getInstance().getStockName(stockID);
            if(stockName == null)
            {
                return false;
            }

            if (stockName.Contains("ST"))
            {
                return true;
            }

            return false;
        }

        private static double getEPSChanging(string stockID)
        {
            String str = StockDataCollector.queryFinanceDataSina(stockID);
            StockFinanceData fd = StockDataConvertor.parseFinanceDataSina(str);

            double epsTTM = fd.eps4Quarter;
            double epsLastYear = fd.epsLastYear;

            //if(Math.Abs(epsLastYear) < double.Epsilon)
            //{
            //    return -1.0;
            //}

            return (epsTTM - epsLastYear) / epsLastYear;
        }

        public static List<string> filterStocksByEPSPeformance(List<string> targetStocks, double epsChgLimit)
        {
            List<string> stocks = new List<string>();
            foreach (string stockID in targetStocks) {
                double epsChg = getEPSChanging(stockID);
                if(epsChg > epsChgLimit)
                {
                    stocks.Add(stockID);
                }
            }

            return stocks;
        }
    }
}
