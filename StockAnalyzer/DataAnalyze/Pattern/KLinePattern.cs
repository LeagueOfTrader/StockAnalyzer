using StockAnalyzer.DataModel;
using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataAnalyze.Pattern
{
    public abstract class KLinePattern
    {
        public abstract bool isMatch(List<StockKLineBaidu> kLineData);
    }
}
