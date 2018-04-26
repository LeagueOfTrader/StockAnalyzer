using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSource
{
    class StockPool : Singleton<StockPool>
    {
        private List<string> m_allSZStocks = new List<string>();
        private List<string> m_allSHStocks = new List<string>();
        private Dictionary<string, string> m_stockNameMap = new Dictionary<string, string>();

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

            loadStocks(m_stockListFileSH, m_allSHStocks);
            loadStocks(m_stockListFileSZ, m_allSZStocks);
        }

        private void loadStocks(string filepath, List<string> container) {
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
                    m_stockNameMap.Add(items[1], items[0]);
                    container.Add(items[1]);
                }                
            }
        }
    }
}
