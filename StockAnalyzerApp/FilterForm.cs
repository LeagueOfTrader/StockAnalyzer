using StockAnalyzer.DataSource;
using StockAnalyzerApp.AppData;
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

        private void refreshSrcList()
        {
            refreshListBox(listBox_src, AppStockData.getInstance().m_srcList.stocks);
            refreshListCount(label_src_count, AppStockData.getInstance().m_srcList.stocks);
        }

        private void refreshScreenedList()
        {
            refreshListBox(listBox_screened, AppStockData.getInstance().m_screenedList.stocks);
            refreshListCount(label_screen_count, AppStockData.getInstance().m_screenedList.stocks);
        }

        private void refreshSelfSelectList()
        {
            refreshListBox(listBox_selfSelect, AppStockData.getInstance().m_selfSelectedList.stocks);
            refreshListCount(label_select_count, AppStockData.getInstance().m_selfSelectedList.stocks);
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
            refreshScreenedList();
        }

        private void button_move_to_src_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_srcList.copy(AppStockData.getInstance().m_screenedList);
            refreshSrcList();
        }

        private void button_add_to_select_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.append(AppStockData.getInstance().m_screenedList);
            refreshSelfSelectList();
        }

        private void button_load_select_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.load("Intermediate/optional_stocks.txt");
            refreshSelfSelectList();
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

                refreshSrcList();
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
            refreshSrcList();
        }        

        private void button_select_remove_Click(object sender, EventArgs e)
        {
            string val = (string)listBox_selfSelect.SelectedItem;
            AppStockUtil.removeItem(AppStockData.getInstance().m_selfSelectedList, val);
            refreshSelfSelectList();
        }

        private void button_screen_remove_Click(object sender, EventArgs e)
        {
            string val = (string)listBox_screened.SelectedItem;
            AppStockUtil.removeItem(AppStockData.getInstance().m_screenedList, val);
            refreshScreenedList();
        }

        private void button_add_src_Click(object sender, EventArgs e)
        {
            AppGlobalCache.getInstance().setTargetList(AppStockData.getInstance().m_srcList);
            AddStockForm addForm = new AddStockForm();
            addForm.ShowDialog();
            AppGlobalCache.getInstance().setTargetList(null);
            refreshSrcList();
        }

        private void button_screen_add_Click(object sender, EventArgs e)
        {
            AppGlobalCache.getInstance().setTargetList(AppStockData.getInstance().m_screenedList);
            AddStockForm addForm = new AddStockForm();
            addForm.ShowDialog();
            AppGlobalCache.getInstance().setTargetList(null);
            refreshScreenedList();
        }

        private void button_select_add_Click(object sender, EventArgs e)
        {
            AppGlobalCache.getInstance().setTargetList(AppStockData.getInstance().m_selfSelectedList);
            AddStockForm addForm = new AddStockForm();
            addForm.ShowDialog();
            AppGlobalCache.getInstance().setTargetList(null);
            refreshSelfSelectList();
        }

        private void button_remove_src_Click(object sender, EventArgs e)
        {
            string val = (string)listBox_src.SelectedItem;
            AppStockUtil.removeItem(AppStockData.getInstance().m_srcList, val);
            refreshSrcList();
        }

        private void button_src_clear_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_srcList.clear();
            refreshSrcList();
        }

        private void button_select_clear_Click(object sender, EventArgs e)
        {
            AppStockData.getInstance().m_selfSelectedList.clear();
            refreshSelfSelectList();
        }

        private void button_select_sort_Click(object sender, EventArgs e)
        {
            AppGlobalCache.getInstance().setTargetList(AppStockData.getInstance().m_selfSelectedList);
            SortForm form = new SortForm();
            form.ShowDialog();
            AppGlobalCache.getInstance().setTargetList(null);
            refreshSelfSelectList();
        }

        private void button_screen_sort_Click(object sender, EventArgs e)
        {
            AppGlobalCache.getInstance().setTargetList(AppStockData.getInstance().m_screenedList);
            SortForm form = new SortForm();
            form.ShowDialog();
            AppGlobalCache.getInstance().setTargetList(null);
            refreshScreenedList();
        }
    }
}
