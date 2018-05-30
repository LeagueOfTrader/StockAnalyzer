using StockAnalyzer.DataSorter;
using StockAnalyzer.Util;
using StockAnalyzerApp.AppData;
using StockAnalyzerApp.AppGlobal;
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
    public partial class SortForm : Form
    {
        enum AppStockSortType
        {
            SST_AnnualCost,
            SST_DynamicCost,
            SST_QuarterCost,
            SST_SpecCost,
            SST_YoyCost,
            SST_PriceScale
        }

        AppStockSortType m_sortType;

        const string m_defaultRefDate = "20180201";

        public SortForm()
        {
            InitializeComponent();
        }

        private void button_sort_ok_Click(object sender, EventArgs e)
        {
            if (radioButton_sort_cost_annual.Checked)
            {
                m_sortType = AppStockSortType.SST_AnnualCost;
            }
            else if (radioButton_sort_cost_dynamic.Checked)
            {
                m_sortType = AppStockSortType.SST_DynamicCost;
            }
            else if (radioButton_sort_cost_quarter.Checked)
            {
                m_sortType = AppStockSortType.SST_QuarterCost;
            }
            else if (radioButton_sort_cost_spec.Checked)
            {
                m_sortType = AppStockSortType.SST_SpecCost;
            }
            else if (radioButton_sort_cost_yoy.Checked)
            {
                m_sortType = AppStockSortType.SST_YoyCost;
            }
            else if (radioButton_sort_pricescale.Checked)
            {
                m_sortType = AppStockSortType.SST_PriceScale;
            }

            runSort();
            this.Close();
        }

        private void runSort()
        {
            AppStockList targetList = AppGlobalCache.getInstance().getTargetList();
            List<string> src = new List<string>();
            src.AddRange(targetList.stocks);

            List<StockSortableMetadata> target = new List<StockSortableMetadata>();
            bool reverse = true;
            foreach(string stockID in src){
                StockSortableMetadata sd = null;
                switch (m_sortType)
                {
                    case AppStockSortType.SST_AnnualCost:
                        sd = new SSMDAnnualCostPerf(stockID);
                        break;
                    case AppStockSortType.SST_DynamicCost:
                        sd = new SSMDCostPerf(stockID);
                        break;
                    case AppStockSortType.SST_PriceScale:
                        string refDate = textBox_pricescale_refdate.Text;
                        if (!DateUtil.isDateValid(refDate))
                        {
                            refDate = m_defaultRefDate;
                        }
                        sd = new SSDMPriceScale(stockID, refDate);
                        break;
                    case AppStockSortType.SST_QuarterCost:
                        sd = new SSMDQuarterCostPerf(stockID);
                        break;
                    case AppStockSortType.SST_SpecCost:
                        string y = textBox_spec_year.Text;
                        string q = textBox_spec_quarter.Text;
                        sd = new SSMDSpecCostPerf(stockID, y, q);
                        break;
                    case AppStockSortType.SST_YoyCost:
                        sd = new SSMDYoyCostPerf(stockID);
                        break;
                    default:
                        break;
                }

                if(sd != null)
                {
                    target.Add(sd);
                }
            }

            target.Sort();
            if (reverse)
            {
                target.Reverse();
            }

            List<string> result = new List<string>();
            for(int i = 0; i < target.Count; i++)
            {
                result.Add(target[i].stockID);
            }

            targetList.copy(result);
        }
    }
}
