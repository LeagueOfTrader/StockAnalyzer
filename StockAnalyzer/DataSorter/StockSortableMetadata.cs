using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    public abstract class StockSortableMetadata : IComparable<StockSortableMetadata>
    {
        public string stockID;
        public double refData;

        public StockSortableMetadata(string code)
        {
            stockID = code;
            refData = calcRefData(code);
        }

        public int CompareTo(StockSortableMetadata other)
        {
            return refData.CompareTo(other.refData);
        }

        protected abstract double calcRefData(string code);
    }
}
