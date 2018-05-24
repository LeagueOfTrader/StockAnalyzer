using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Assist
{
    public class Logger
    {
        public static void log(string info)
        {
            Console.WriteLine(info);
            System.Diagnostics.Debug.WriteLine(info);
        }
    }
}
