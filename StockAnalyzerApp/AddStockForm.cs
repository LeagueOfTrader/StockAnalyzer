using StockAnalyzer.Util;
using StockAnalyzerApp.AppGlobal;
using StockAnalyzerApp.AppUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAnalyzerApp
{
    public partial class AddStockForm : Form
    {
        public AddStockForm()
        {
            InitializeComponent();
        }

        private void button_add_ok_Click(object sender, EventArgs e)
        {
            string code = textBox_addstock_name.Text;
            if (!StockIDUtil.isValidCode(code))
            {
                if (!StockIDUtil.isValidPureCode(code))
                {
                    MessageBox.Show("无效的股票代码！");
                    this.Close();
                    return;
                }

                code = StockIDUtil.complementCode(code);
            }

            AppStockUtil.addItem(AppGlobalCache.getInstance().getTargetList(), code);
            this.Close();
        }
    }
}
