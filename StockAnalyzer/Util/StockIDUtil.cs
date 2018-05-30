using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Util
{
    public class StockIDUtil
    {
        public static string getPureCode(string stockID)
        {
            if(stockID.Length < 8)
            {
                return "";
            }

            return stockID.Substring(2, 6);
        }

        public static bool isValidCode(string stockID)
        {
            if(stockID.Length != 8)
            {
                return false;
            }

            string prefix = stockID.Substring(0, 2);
            if(prefix != "sh" || prefix != "sz")
            {
                return false;
            }

            for(int i = 2; i < 8; i++)
            {
                char num = stockID[i];
                if(num > '9' || num < '0')
                {
                    return false;
                }
            }

            return true;
        }

        public static bool isValidPureCode(string stockCode)
        {
            if(stockCode.Length != 6)
            {
                return false;
            }

            for (int i = 0; i < 6; i++)
            {
                char num = stockCode[i];
                if (num > '9' || num < '0')
                {
                    return false;
                }
            }

            return true;
        }

        public static string complementCode(string pureCode)
        {
            if(pureCode == null || pureCode.Length == 0)
            {
                return null;
            }

            char first = pureCode[0];
            string prefix = "";
            if(first == '6')
            {
                prefix = "sh";
            }
            else if(first == '3' || first == '0')
            {
                prefix = "sz";
            }
            else
            {
                return null;
            }

            return prefix + pureCode;
        }
    }
}
