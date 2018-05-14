using StockAnalyzer.DataSorter;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class RankMetadata : StockSortableMetadata
    {
        public RankMetadata(string code, double data) : base(code)
        {
            stockID = code;
            refData = data;
        }

        public void setRefData(double data)
        {
            refData = data;
        }

        protected override double calcRefData(string code)
        {
            return 0.0;
        }

    }

    class RankInIndustryFilter : StockFilter
    {
        private int m_rank = 0;
        private NumericStockFilter m_comparer = null;

        public RankInIndustryFilter(NumericStockFilter comparer, int rank = 10)
        {
            m_rank = rank;
        }

        public override bool filterMethod(string stockID)
        {
            int rank = calcRankInIndustry(stockID);

            if(rank < 0)
            {
                return false;
            }

            if(rank >= m_rank)
            {
                return false;
            }

            return true;
        }

        public int calcRankInIndustry(string stockID)
        {
            if (m_comparer == null)
            {
                return -1;
            }

            string industryName = StockPool.getInstance().getStockIndustry(stockID);
            List<string> stocksInIndustry = StockPool.getInstance().getStocksInIndustry(industryName);
            List<RankMetadata> sortableArr = new List<RankMetadata>();
            foreach (string code in stocksInIndustry)
            {
                double val = 0.0;
                if (!m_comparer.getNumericValue(code, out val))
                {
                    continue;
                }
                RankMetadata md = new RankMetadata(code, val);
                sortableArr.Add(md);
            }

            sortableArr.Sort();
            int i = 0;
            for (i = 0; i < sortableArr.Count; i++)
            {
                if (sortableArr[i].stockID.Equals(stockID))
                {
                    break;
                }
            }

            if(i == sortableArr.Count)
            {
                return -1;
            }

            return i + 1;
        }
    }
}
