using StockAnalyzer.Util;
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

        private string m_defaultBeginDate = "20180201";

        private bool m_advMode = true;

        public PriceComparisonForm()
        {
            InitializeComponent();

            timer_pricecomparison.Start();

            if (m_advMode)
            {
                listView_pricecomp.Visible = false;
                listView_advcomp.Visible = true;
            }
            else
            {
                listView_pricecomp.Visible = true;
                listView_advcomp.Visible = false;
            }
        }

        private void timer_pricecomparison_Tick(object sender, EventArgs e)
        {
            if (m_dirty)
            {
                return;
            }
            
            if (m_advMode)
            {
                AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().update();
                refreshAdvCompListView();
            }
            else
            {
                AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().update();
                refreshPriceCompListView();
            }
        }

        private void button_pricecomp_refresh_Click(object sender, EventArgs e)
        {
            string beginDate = textBox_pricecomp_begindate.Text;
            string endDate = textBox_pricecomp_enddate.Text;
            if (!DateUtil.isDateValid(beginDate))
            {
                beginDate = m_defaultBeginDate;
            }
            if (!DateUtil.isDateValid(endDate))
            {
                endDate = DateUtil.getTodayDate();
            }

            if (m_advMode)
            {
                AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().refresh(AppStockData.getInstance().m_selfSelectedList.stocks, beginDate, endDate);
                rebuildAdvCompListView();
            }
            else
            {
                AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().refresh(AppStockData.getInstance().m_selfSelectedList.stocks, beginDate, endDate);

                rebuildPriceCompListView();
            }
        }

        private void rebuildPriceCompListView()
        {
            m_dirty = true;
            listView_pricecomp.Items.Clear();
            for (int i = 0; i < AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().m_priceCompList.Count; i++)
            {
                AppPriceCompareItem item = AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().m_priceCompList[i];
                ListViewItem lvi = new ListViewItem(item.m_code.ToString());
                lvi.SubItems.Add(item.m_curPrice.ToString());
                lvi.SubItems.Add(item.m_chgFromLowest.ToString());
                listView_pricecomp.Items.Add(lvi);
            }
            m_dirty = false;
        }

        private void refreshPriceCompListView()
        {
            for (int i = 0; i < AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().m_priceCompList.Count; i++)
            {
                AppPriceCompareItem item = AppPriceCompareCtrl<AppPriceCompareItem>.getInstance().m_priceCompList[i];
                listView_pricecomp.Items[i].SubItems[1].Text = item.m_curPrice.ToString();
                listView_pricecomp.Items[i].SubItems[2].Text = item.m_chgFromLowest.ToString();
            }
        }

        private void rebuildAdvCompListView()
        {
            m_dirty = true;
            listView_pricecomp.Items.Clear();
            for (int i = 0; i < AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().m_priceCompList.Count; i++)
            {
                AppAdvancedCompareItem item = AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().m_priceCompList[i];
                ListViewItem lvi = new ListViewItem(item.m_code.ToString());
                lvi.SubItems.Add(item.m_curPrice.ToString());
                lvi.SubItems.Add(item.m_chgFromLowest.ToString());
                lvi.SubItems.Add(item.m_dynamicPE.ToString());
                lvi.SubItems.Add(item.m_peIndustry.ToString());
                lvi.SubItems.Add(item.m_roe.ToString());
                lvi.SubItems.Add(item.m_roeIndustry.ToString());
                lvi.SubItems.Add(item.m_netProfitRatio.ToString());
                lvi.SubItems.Add(item.m_nprIndustry.ToString());
                lvi.SubItems.Add(item.m_curCost.ToString());
                lvi.SubItems.Add(item.m_histCost.ToString());
                lvi.SubItems.Add(item.m_costRatio.ToString());
                listView_advcomp.Items.Add(lvi);
            }
            m_dirty = false;
        }

        private void refreshAdvCompListView()
        {
            for (int i = 0; i < AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().m_priceCompList.Count; i++)
            {
                AppAdvancedCompareItem item = AppPriceCompareCtrl<AppAdvancedCompareItem>.getInstance().m_priceCompList[i];
                listView_advcomp.Items[i].SubItems[1].Text = item.m_curPrice.ToString();
                listView_advcomp.Items[i].SubItems[2].Text = item.m_chgFromLowest.ToString();
                //listView_advcomp.Items[i].SubItems[3].Text = item.m_dynamicPE.ToString();
            }
        }
    }
}
