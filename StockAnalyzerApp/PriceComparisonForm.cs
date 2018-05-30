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
        private bool m_dirty = true;

        public PriceComparisonForm()
        {
            InitializeComponent();

            timer_pricecomparison.Start();
        }

        private void timer_pricecomparison_Tick(object sender, EventArgs e)
        {
            if (m_dirty)
            {
                return;
            }

            AppPriceCompareCtrl.getInstance().update();
            refreshPriceCompListView();
        }

        private void button_pricecomp_refresh_Click(object sender, EventArgs e)
        {
            string date = textBox_pricecomp_date.Text;
            AppPriceCompareCtrl.getInstance().refresh(AppStockData.getInstance().m_selfSelectedList.stocks, date);
            //AppPriceCompareCtrl.getInstance().update();
            //refreshPriceCompListView();
            rebuildPriceCompListView();
        }

        private void rebuildPriceCompListView()
        {
            m_dirty = true;
            listView_pricecomp.Items.Clear();
            foreach (AppPriceCompareItem item in AppPriceCompareCtrl.getInstance().m_priceCompList)
            {
                ListViewItem lvi = new ListViewItem(item.m_code.ToString());
                lvi.SubItems.Add(item.m_curPrice.ToString());
                lvi.SubItems.Add(item.m_chgFromLowest.ToString());
                listView_pricecomp.Items.Add(lvi);
            }
            m_dirty = false;
        }

        private void refreshPriceCompListView()
        {
            int i = 0;
            foreach (AppPriceCompareItem item in AppPriceCompareCtrl.getInstance().m_priceCompList)
            {
                listView_pricecomp.Items[i].SubItems[1].Text = item.m_curPrice.ToString();
                listView_pricecomp.Items[i].SubItems[2].Text = item.m_chgFromLowest.ToString();
                i++;
            }
        }
    }
}
