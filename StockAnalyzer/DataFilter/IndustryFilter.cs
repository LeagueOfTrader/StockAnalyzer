using StockAnalyzer.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataFilter
{
    class IndustryFilter : StockFilter
    {
        private static List<string> ms_uninterestIndustries = new List<string>();
        private static bool ms_loaded = false;

        public IndustryFilter()
        {
            if (!ms_loaded)
            {
                loadUninterestedIndustries();
            }
        }

        private static void loadUninterestedIndustries()
        {
            ms_uninterestIndustries.Clear();
            StreamReader sr = new StreamReader("Data/UninterestedIndustryList.txt", Encoding.UTF8);
            string str;
            while ((str = sr.ReadLine()) != null)
            {
                ms_uninterestIndustries.Add(str);
            }

            ms_loaded = true;
        }

        public static bool isUninterestedIndustry(string stockID)
        {
            string industryName = StockPool.getInstance().getStockIndustry(stockID);
            if (industryName == null)
            {
                return false;
            }

            return ms_uninterestIndustries.Contains(industryName);
        }

        public override bool filterMethod(string stockID)
        {
            if (!isUninterestedIndustry(stockID)) {
                return true;
            }

            return false;
        }
    }
}
