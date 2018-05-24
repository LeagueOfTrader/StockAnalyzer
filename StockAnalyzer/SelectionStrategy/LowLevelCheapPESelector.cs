using StockAnalyzer.DataFilter;
using StockAnalyzer.DataSource;
using StockAnalyzer.IntermediateProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.SelectionStrategy
{
    public class LowLevelCheapPESelector : IStockSelector
    {
        private bool m_useIntermediate = true;

        public List<string> screen()
        {
            List<string> stocks = null;
            if (m_useIntermediate)
            {
                stocks = IntermediateImporter.readLowPriceStocks();
            }
            else
            {
                stocks = StockPool.getInstance().getAllStocks();
            }

            return filterStocksByPriceScale_PE_Eps(stocks, 0.3, 40.0, 0.2);
        }

        private List<string> filterStocksByPriceScale_PE_Eps(List<string> src, double ratio, double pe, double epsChg)
        {
            List<string> lowStocks = null;
            if (!m_useIntermediate)
            {
                PriceScaleFilter psFilter = new PriceScaleFilter(ratio);
                lowStocks = psFilter.filter(src);
            }
            else
            {
                lowStocks = src;
            }
            PEFilter peFilter = new PEFilter(pe);
            STFilter stFilter = new STFilter();
            IndustryExcludeFilter indFilter = new IndustryExcludeFilter();
            EPSPerfFilter epsFilter = new EPSPerfFilter(epsChg);

            return epsFilter.filter(peFilter.filter(indFilter.filter(stFilter.filter(lowStocks))));
        }
    }
}
