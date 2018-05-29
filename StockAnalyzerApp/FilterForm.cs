using StockAnalyzer.DataSource;
using StockAnalyzerApp.AppData;
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
    public partial class FilterForm : Form
    {
        // method:
        protected void refreshListBox(ListBox lb, List<string> arr)
        {
            lb.Items.Clear();
            foreach (string data in arr)
            {
                lb.Items.Add(data);
            }
            lb.Update();
        }

        protected void refreshListCount(Label l, List<string> arr)
        {
            l.Text = arr.Count.ToString();
        }

        public FilterForm()
        {
            InitializeComponent();

            PriceComparisonForm pcForm = new PriceComparisonForm();
            pcForm.Show();
            //this.listBox_src.DataSource = AppStockData.getInstance().m_srcList.stocks;
            //bindList_src = new BindingList<string>(AppStockData.getInstance().m_srcList.stocks);
            //this.listBox_src.DataSource = bindList_src;
        }

        public void onFilterFinish()
        {
            refreshListBox(listBox_screened, AppStockData.getInstance().m_screenedList.stocks);
            refreshListCount(label_screen_count, AppStockData.getInstance().m_screenedList.stocks);
        }

        private void button_move_to_src_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_srcList.copy(AppStockData.getInstance().m_screenedList);
            refreshListBox(listBox_src, AppStockData.getInstance().m_srcList.stocks);
            refreshListCount(label_src_count, AppStockData.getInstance().m_srcList.stocks);
        }

        private void button_add_to_select_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.append(AppStockData.getInstance().m_screenedList);
            refreshListBox(listBox_selfSelect, AppStockData.getInstance().m_selfSelectedList.stocks);
            refreshListCount(label_select_count, AppStockData.getInstance().m_selfSelectedList.stocks);
        }

        private void button_load_select_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.load("Intermediate/optional_stocks.txt");
            refreshListBox(listBox_selfSelect, AppStockData.getInstance().m_selfSelectedList.stocks);
            refreshListCount(label_select_count, AppStockData.getInstance().m_selfSelectedList.stocks);
        }

        private void button_save_select_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.save("Intermediate/optional_stocks.txt");
        }

        private void button_import_src_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filepath = dlg.FileName;
                AppStockData.getInstance().m_srcList.load(filepath);
                //foreach(string stockID in AppStockData.getInstance().m_srcList.stocks)
                //{
                //    this.listBox_src.Items.Add(stockID);
                //}

                refreshListBox(listBox_src, AppStockData.getInstance().m_srcList.stocks);
                refreshListCount(label_src_count, AppStockData.getInstance().m_srcList.stocks);
            }
        }

        private void button_export_src_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filepath = dlg.FileName;
                AppStockData.getInstance().m_srcList.save(filepath);
            }
        }

        private void button_export_screened_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filepath = dlg.FileName;
                AppStockData.getInstance().m_screenedList.save(filepath);
            }
        }

        private void button_screen_Click(object sender, EventArgs e)
        {
            //AppStockData.getInstance().m_srcList.copy(AppStockData.getInstance().m_screenedList);
            //refreshListBox(listBox_src, AppStockData.getInstance().m_srcList.stocks);
            FilterConditionForm conditionForm = new FilterConditionForm(this);
            conditionForm.ShowDialog();
        }

        private void button_src_defaultAll_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_srcList.copy(StockPool.getInstance().getAllStocks());
            refreshListBox(listBox_src, AppStockData.getInstance().m_srcList.stocks);
            refreshListCount(label_src_count, AppStockData.getInstance().m_srcList.stocks);
        }
    }
}
