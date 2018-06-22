using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    public class IndustryIncludeFilter : StockFilter
    {
        List<string> m_specifiedIndustries = new List<string>();
        public IndustryIncludeFilter(List<string> arr)
        {
            m_specifiedIndustries.Clear();
            m_specifiedIndustries.AddRange(arr);
        }

        public override bool filterMethod(string stockID)
        {
            foreach(string industryName in m_specifiedIndustries)
            {
                if(IndustryExcludeFilter.isStockInIndustry(stockID, industryName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
