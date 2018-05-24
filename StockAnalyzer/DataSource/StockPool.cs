using StockAnalyzer.Common;
using StockAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    public class StockPool : Singleton<StockPool>
    {
        private List<string> m_allSZStocks = new List<string>();
        private List<string> m_allSHStocks = new List<string>();
        private Dictionary<string, string> m_stockNameMap = new Dictionary<string, string>();
        private Dictionary<string, string> m_stockIndustryMap = new Dictionary<string, string>();
        private Dictionary<string, List<string>> m_stocksInIndustry = new Dictionary<string, List<string>>();

        const string m_stockListFileSH = "Data/StockListSH.txt";
        const string m_stockListFileSZ = "Data/StockListSZ.txt";

        public List<String> allSZStocks
        {
            get { return m_allSZStocks; }
        }

        public List<String> allSHStocks
        {
            get { return m_allSHStocks; }
        }

        public void init() {
            m_stockNameMap.Clear();
            m_allSHStocks.Clear();
            m_allSZStocks.Clear();
            m_stockIndustryMap.Clear();

            loadStocks(m_stockListFileSH, m_allSHStocks, "sh");
            loadStocks(m_stockListFileSZ, m_allSZStocks, "sz");
            loadIndustry();
        }

        public List<string> getAllStocks()
        {
            List<string> stocks = new List<string>();

            stocks.AddRange(allSHStocks);
            stocks.AddRange(allSZStocks);

            return stocks;
        }

        private void loadStocks(string filepath, List<string> container, string prefix) {
            StreamReader sr = new StreamReader(filepath, Encoding.UTF8);
            string str;
            string content = "";
            while ((str = sr.ReadLine()) != null){
                content += str;
            }

            string[] arr = content.Split(')');
            //arr.Length;
            for (int i = 0; i < arr.Length; i++) {
                string[] items = arr[i].Split('(');
                
                if(items.Length >= 2){
                    string stockCode = prefix + items[1];
                    m_stockNameMap.Add(stockCode, items[0]);
                    container.Add(stockCode);
                }                
            }
        }

        private void loadIndustry()
        {
            List<List<string>> info = CSVFileUtil.readCSV("Data/StockIndustry.txt", '\t');
            for(int i = 0; i < info.Count; i++)
            {
                List<string> arr = info[i];
                if(arr.Count < 3)
                {
                    continue;
                }
                m_stockIndustryMap.Add(arr[0], arr[2]);

                if (!m_stocksInIndustry.ContainsKey(arr[2]))
                {
                    List<string> stockList = new List<string>();
                    m_stocksInIndustry.Add(arr[2], stockList);
                }
                m_stocksInIndustry[arr[2]].Add(arr[0]);
            }
        }

        public string getStockName(string stockCode)
        {
            string name = null;
            if(m_stockNameMap.TryGetValue(stockCode, out name))
            {
                return name;    
            }

            return null;
        }

        public string getStockIndustry(string stockCode)
        {
            string industryName = null;
            string code = StockIDUtil.getPureCode(stockCode);
            if(m_stockIndustryMap.TryGetValue(code, out industryName))
            {
                return industryName;
            }

            return null;
        }

        public List<string> getStocksInIndustry(string industryName)
        {
            if (m_stocksInIndustry.ContainsKey(industryName))
            {
                return m_stocksInIndustry[industryName];
            }

            return null;
        }
    }
}
