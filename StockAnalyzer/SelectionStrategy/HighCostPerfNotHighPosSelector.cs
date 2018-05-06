using StockAnalyzer.Assist;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.SelectionStrategy
{
    class HighCostPerfNotHighPosSelector : IStockSelector
    {
        public List<string> screen()
        {
            List<string> stocks = new List<string>();

            List<string> shStocks = StockPool.getInstance().allSHStocks;
            List<string> szStocks = StockPool.getInstance().allSZStocks;

            stocks.AddRange(shStocks);
            stocks.AddRange(szStocks);
            

            return filterStocksByCostPerf_PricePos_PE(stocks, "2018", "1", 0.05, 0.5, 40.0);
        }

        private List<string> filterStocksByCostPerf_PricePos_PE(List<string> src, string year, string quarter, double costDiffRatio, double priceRatio, double pe)
        {
            PEFilter peFilter = new PEFilter(pe);
            STFilter stFilter = new STFilter();
            IndustryFilter indFilter = new IndustryFilter();
            CostPerfFilter cpFilter = new CostPerfFilter(year, quarter, costDiffRatio);
            PriceScaleFilter psFilter = new PriceScaleFilter(priceRatio);

            Logger.log("Start filter ....");
            List<string> f1Result = indFilter.filter(stFilter.filter(src));
            Logger.log("Basic filter, count: " + f1Result.Count.ToString());
            List<string> f2Result = peFilter.filter(f1Result);
            Logger.log("PE filter, count: " + f2Result.Count.ToString());
            List<string> f3Result = cpFilter.filter(f2Result);
            Logger.log("Cost filter, count: " + f3Result.Count.ToString());
            List<string> f4Result = psFilter.filter(f3Result);
            Logger.log("Price Pos filter, count: " + f4Result.Count.ToString());
            return f4Result;
        }
    }
}
