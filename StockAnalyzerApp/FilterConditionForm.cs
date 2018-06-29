using StockAnalyzerApp.AppFilter;
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
    public partial class FilterConditionForm : Form
    {
        FilterForm m_mainForm = null;

        public FilterConditionForm(FilterForm mainForm)
        {
            InitializeComponent();
            m_mainForm = mainForm;
        }

        private void button_filter_ok_Click(object sender, EventArgs e)
        {
            AppStockFilter.getInstance().start();

            if(AppStockFilter.getInstance().getSrcStocks() == null ||
                AppStockFilter.getInstance().getSrcStocks().Count == 0)
            {
                MessageBox.Show("No stocks to filter! ");
                this.Close();
                return;
            }

            try
            {
                collectConditions();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Condition Error: " + ex.Message);
                return;
            }

            AppStockFilter.getInstance().doFilter();
            AppStockFilter.getInstance().end();

            notifyMainForm();

            this.Close();
        }

        private void notifyMainForm()
        {
            if(m_mainForm != null)
            {
                m_mainForm.onFilterFinish();
            }
        }

        private void collectConditions()
        {
            AppFilterItem item = null;
            int subType = 0;
            double param1 = 0.0;
            string param2 = "";

            if (checkBox_cost.Checked)
            {
                if (radioButton_cost_annaul.Checked)
                {
                    subType = 0;
                }
                else if (radioButton_cost_dynamic.Checked)
                {
                    subType = 1;
                }
                else if (radioButton_cost_quarter.Checked)
                {
                    subType = 2;
                }
                else if (radioButton_cost_yoy.Checked)
                {
                    subType = 3;
                }
                else if (radioButton_cost_spec.Checked)
                {
                    subType = 4;
                }

                param1 = double.Parse(textBox_cost_ratio.Text);
                item = new AppFilterItem(AppFilterType.FLTT_Cost, subType, param1);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_pe.Checked)
            {
                if (radioButton_pe_dynamic.Checked)
                {
                    subType = 1;
                }
                else if (radioButton_pe_static.Checked)
                {
                    subType = 0;
                }

                param1 = double.Parse(textBox_pe.Text);
                item = new AppFilterItem(AppFilterType.FLTT_PE, subType, param1);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_roe.Checked)
            {
                param1 = double.Parse(textBox_roe.Text);
                param2 = textBox_roe_yrs.Text;

                item = new AppFilterItem(AppFilterType.FLTT_ROE, 0, param1, param2);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_netProfitRatio.Checked)
            {
                param1 = double.Parse(textBox_netProfitRatio.Text);
                param2 = textBox_netProfitRatio_yrs.Text;

                item = new AppFilterItem(AppFilterType.FLTT_NetProfitRatio, 0, param1, param2);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_inIndustry.Checked)
            {
                if (radioButton_inInd_annual.Checked)
                {
                    subType = 0;
                }
                else if (radioButton_inInd_cost.Checked)
                {
                    subType = 1;
                }
                else if (radioButton_inInd_PE.Checked)
                {
                    subType = 2;
                }
                else if (radioButton_inInd_PEDyn.Checked)
                {
                    subType = 3;
                }
                else if (radioButton_inInd_roe.Checked)
                {
                    subType = 4;
                }
                else if (radioButton_inInd_netProfitRatio.Checked)
                {
                    subType = 5;
                }

                param1 = double.Parse(textBox_industry_ratio.Text);
                item = new AppFilterItem(AppFilterType.FLTT_InIndustry, subType, param1);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_price.Checked)
            {
                param1 = double.Parse(textBox_price.Text);
                item = new AppFilterItem(AppFilterType.FLTT_PriceScale, 0, param1);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_distrib.Checked)
            {
                param1 = double.Parse(textBox_distrib.Text);
                item = new AppFilterItem(AppFilterType.FLTT_Distrib, 0, param1);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_excludeIndustry.Checked)
            {
                item = new AppFilterItem(AppFilterType.FLTT_ExcludeIndustry);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_includeIndustry.Checked)
            {
                param2 = textBox_includeInd_names.Text;
                if (param2.Length > 0)
                {
                    item = new AppFilterItem(AppFilterType.FLTT_IncludeIndustry, 0, 0.0, param2);
                    AppStockFilter.getInstance().addCondition(item);
                }
            }

            if (checkBox_st.Checked)
            {
                item = new AppFilterItem(AppFilterType.FLTT_STStocks);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_holdercount.Checked)
            {
                if (radioButton_holdercount_decrease.Checked)
                {
                    subType = 0;
                    if (checkBox_holdercount_allowinvariant.Checked)
                    {
                        param2 = true.ToString();
                    }
                    else
                    {
                        param2 = false.ToString();
                    }
                }
                else if (radioButton_holdercount_accumratio.Checked)
                {
                    subType = 1;
                    param2 = textBox_holdercount_ratio.Text;
                }
                param1 = double.Parse(textBox_holdercount_quarters.Text);
                item = new AppFilterItem(AppFilterType.FLTT_HolderCount, subType, param1, param2);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_stableprofit.Checked)
            {
                param2 = textBox_stable_years.Text;
                item = new AppFilterItem(AppFilterType.FLTT_StableProfit, 0, 0, param2);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_amplitude.Checked)
            {
                param1 = double.Parse(textBox_amplitude.Text);
                param2 = textBox_amp_days.Text;
                item = new AppFilterItem(AppFilterType.FLTT_Amplitude, 0, param1, param2);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_subnew.Checked)
            {
                item = new AppFilterItem(AppFilterType.FLTT_SubNew);
                AppStockFilter.getInstance().addCondition(item);
            }

            if (checkBox_pbcost.Checked)
            {
                param1 = double.Parse(textBox_pbcost_ratio.Text);
                item = new AppFilterItem(AppFilterType.FLTT_PBCost, 0, param1);
                AppStockFilter.getInstance().addCondition(item);
            }
        }

        private void button_filter_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
