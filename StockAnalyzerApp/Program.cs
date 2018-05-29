using StockAnalyzer.DataSource;
using StockAnalyzer.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAnalyzerApp
{
    static class Program
    {
        static void initCore()
        {
            GlobalConfig.getInstance().init();
            StockPool.getInstance().init();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            initCore();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FilterForm());
            //Application.Run(new PriceComparisonForm());
        }
    }
}
