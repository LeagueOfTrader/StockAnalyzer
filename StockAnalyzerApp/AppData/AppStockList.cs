using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzerApp.AppData
{
    class AppStockList
    {
        protected List<string> m_stocks = new List<string>();
        public List<string> stocks{
            get{ return m_stocks; }
            set { m_stocks = value;}
        }

        public void remove(string stockID)
        {
            m_stocks.Remove(stockID);
        }

        public void append(List<string> arr)
        {
            m_stocks.AddRange(arr);
            m_stocks = m_stocks.Distinct().ToList();
        }

        public void append(AppStockList other)
        {
            if(other != null)
            {
                append(other.stocks);
            }
        }

        public void add(string stockID)
        {
            if (!m_stocks.Contains(stockID))
            {
                m_stocks.Add(stockID);
            }
        }

        public void copy(List<string> arr)
        {
            if(arr == null)
            {
                return;
            }

            m_stocks.Clear();
            m_stocks.AddRange(arr);
        }

        public void copy(AppStockList other)
        {
            if(other != null)
            {
                copy(other.stocks);
            }
        }

        public void clear()
        {
            m_stocks.Clear();
        }

        public void load(string filepath)
        {
            m_stocks = StockListFileUtil.readStocksFromFile(filepath);
        }

        public void save(string filepath)
        {
            StockListFileUtil.writeStocksToFile(m_stocks, filepath);
        }
    }
}
