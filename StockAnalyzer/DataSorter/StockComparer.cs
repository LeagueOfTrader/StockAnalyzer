using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataSorter
{
    class StockComparer : IComparer<StockSortableMetadata>
    {
        public int Compare(StockSortableMetadata x, StockSortableMetadata y)
        {
            return x.CompareTo(y);
        }
    }
}
