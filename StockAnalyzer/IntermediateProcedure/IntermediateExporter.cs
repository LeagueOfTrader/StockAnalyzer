using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
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

            if (stocks.Count == 0)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(filepath);
            foreach (string stock in stocks)
            {
                sw.WriteLine(stock);
            }

            sw.Close();
        }
    }
}
