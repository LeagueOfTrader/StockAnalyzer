using StockAnalyzerApp.AppData;
using StockAnalyzerApp.AppPriceComparison;
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
    public partial class PriceComparisonForm : Form
    {
        public PriceComparisonForm()
        {
            InitializeComponent();

            //listBox_pricecomparison.DataSource = AppPriceCompareCtrl.getInstance().m_priceCompList;
            //listBox_pricecomparison.DisplayMember = 
            //listView_pricecomp.Data;
            //listBox_pricecomparison.MultiColumn
        }

        private void timer_pricecomparison_Tick(object sender, EventArgs e)
        {
            AppPriceCompareCtrl.getInstance().update();
        }

        private void button_pricecomp_refresh_Click(object sender, EventArgs e)
        {
            string date = textBox_pricecomp_date.Text;
            AppPriceCompareCtrl.getInstance().refresh(AppStockData.getInstance().m_selfSelectedList.stocks, date);
        }

        private void refreshPriceCompListBox()
        {
            //
        }
    }
}
