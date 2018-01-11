using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Common
{
    public class Singleton<T> where T : new()
    {
        private static T ms_instance;
        public static T getInstance()
        {
            if (ms_instance == null)
            {
                ms_instance = new T();
            }
            return ms_instance;
        }
    }
}
