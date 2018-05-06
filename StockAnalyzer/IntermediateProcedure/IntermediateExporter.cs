using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using StockAnalyzer.SelectionStrategy;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.IntermediateProcedure
{
    class IntermediateExporter
    {
        public static void filterStocksByPriceScaleAndPE(double ratio, double pe, string filepath)
        {
            List<string> stocks = new List<string>();
            PEFilter peFilter = new PEFilter(pe);
            PriceScaleFilter priceFilter = new PriceScaleFilter(ratio);

            List<string> shStocks = peFilter.filter(priceFilter.filter(StockPool.getInstance().allSHStocks));
            List<string> szStocks = peFilter.filter(priceFilter.filter(StockPool.getInstance().allSZStocks));

            stocks.AddRange(shStocks);
            stocks.AddRange(szStocks);

            StockListFileUtil.writeStocksToFile(stocks, filepath);
        }

        public static void filterStocksByHighCostPerf()
        {
            HighCostPerfNotHighPosSelector s = new HighCostPerfNotHighPosSelector();
            List<string> stocks = s.screen();
            StockListFileUtil.writeStocksToFile(stocks, "Intermediate/cheap_stocks.txt");
        }
    }
}
