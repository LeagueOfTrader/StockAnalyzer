using StockAnalyzer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Assist
{
    class Logger
    {
        public static void debugOutput(string info)
        {
            Console.WriteLine(info);
            System.Diagnostics.Debug.WriteLine(info);
        }
    }
}
