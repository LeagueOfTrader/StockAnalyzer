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
            //listView_pricecomp.Columns.Add("代码");
            //listView_pricecomp.Columns.Add("当前价");
            //listView_pricecomp.Columns.Add("上升幅度");
        }

        private void timer_pricecomparison_Tick(object sender, EventArgs e)
        {
            AppPriceCompareCtrl.getInstance().update();
        }

        private void button_pricecomp_refresh_Click(object sender, EventArgs e)
        {
            string date = textBox_pricecomp_date.Text;
            AppPriceCompareCtrl.getInstance().refresh(AppStockData.getInstance().m_selfSelectedList.stocks, date);
            AppPriceCompareCtrl.getInstance().update();
            refreshPriceCompListView();
        }

        private void refreshPriceCompListView()
        {
            //listBox_pricecomparison.Items.Clear();
            listView_pricecomp.Items.Clear();
            foreach (AppPriceCompareItem item in AppPriceCompareCtrl.getInstance().m_priceCompList)
            {
                ListViewItem lvi = new ListViewItem(item.m_code.ToString());
                lvi.SubItems.Add(item.m_curPrice.ToString());
                lvi.SubItems.Add(item.m_chgFromLowest.ToString());
                listView_pricecomp.Items.Add(lvi);
            }
        }
    }
}
