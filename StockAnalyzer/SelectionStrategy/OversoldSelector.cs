using StockAnalyzer.DataAnalyze.Pattern;
using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.SelectionStrategy
{
    class OversoldSelector : IStockSelector
    {
        public List<string> screen()
        {
            List<string> src = StockPool.getInstance().getAllStocks();

            STFilter stFlt = new STFilter();
            IndustryFilter indFlt = new IndustryFilter();
            KLinePatternFilter osFlt = new KLinePatternFilter(new Oversold2Day());
            PriceScaleFilter psFlt = new PriceScaleFilter(0.5);
            List<string> stocks = psFlt.filter(osFlt.filter(indFlt.filter(stFlt.filter(src))));
            return stocks;
        }
    }
}
