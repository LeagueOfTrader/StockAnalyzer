using StockAnalyzer.Common;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.SelectionStrategy
{
    public class OptionalStocks : Singleton<OptionalStocks>
    {
        private bool m_inited = false;
        public List<string> optionalStockList = new List<string>();
        const string optionalStocksFilepath = "Intermediate/optional_stocks.txt";

        public void init()
        {
            load();
            m_inited = true;
        }

        public bool inited()
        {
            return m_inited;
        }

        public void load()
        {
            optionalStockList = StockListFileUtil.readStocksFromFile(optionalStocksFilepath);
        }

        public void save()
        {
            StockListFileUtil.writeStocksToFile(optionalStockList, optionalStocksFilepath);
        }

        public void addStockToOptionalList(string stockID)
        {
            if (optionalStockList.Contains(stockID))
            {
                return;
            }

            optionalStockList.Add(stockID);
        }

        public void addStocksToOptionalList(List<string> stocks)
        {
            foreach(string stockID in stocks)
            {
                addStockToOptionalList(stockID);
            }
        }

        public void removeStockFromOptionalList(string stockID)
        {
            optionalStockList.Remove(stockID);
        }

        public void clearOptionalList()
        {
            optionalStockList.Clear();
        }
    }
}
