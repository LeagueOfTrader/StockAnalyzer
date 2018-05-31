namespace StockAnalyzerApp
{
    partial class PriceComparisonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox_pricecomp_begindate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_pricecomp_refresh = new System.Windows.Forms.Button();
            this.timer_pricecomparison = new System.Windows.Forms.Timer(this.components);
            this.listView_pricecomp = new System.Windows.Forms.ListView();
            this.columnHeader_lv_pc_code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_lv_pc_curprice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_lv_pc_chg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_pricecomp_enddate = new System.Windows.Forms.TextBox();
            this.listView_advcomp = new System.Windows.Forms.ListView();
            this.colHeader_advcomp_code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_chg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_roe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_pe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_ind_roe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_adv_ind_pe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_npr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_advcomp_ind_npr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHead_cost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_cost_hist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_cost_ratio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textBox_pricecomp_begindate
            // 
            this.textBox_pricecomp_begindate.Location = new System.Drawing.Point(83, 12);
            this.textBox_pricecomp_begindate.Name = "textBox_pricecomp_begindate";
            this.textBox_pricecomp_begindate.Size = new System.Drawing.Size(100, 21);
            this.textBox_pricecomp_begindate.TabIndex = 1;
            this.textBox_pricecomp_begindate.Text = "20180201";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始日期：";
            // 
            // button_pricecomp_refresh
            // 
            this.button_pricecomp_refresh.Location = new System.Drawing.Point(200, 12);
            this.button_pricecomp_refresh.Name = "button_pricecomp_refresh";
            this.button_pricecomp_refresh.Size = new System.Drawing.Size(75, 23);
            this.button_pricecomp_refresh.TabIndex = 3;
            this.button_pricecomp_refresh.Text = "刷新";
            this.button_pricecomp_refresh.UseVisualStyleBackColor = true;
            this.button_pricecomp_refresh.Click += new System.EventHandler(this.button_pricecomp_refresh_Click);
            // 
            // timer_pricecomparison
            // 
            this.timer_pricecomparison.Interval = 1000;
            this.timer_pricecomparison.Tick += new System.EventHandler(this.timer_pricecomparison_Tick);
            // 
            // listView_pricecomp
            // 
            this.listView_pricecomp.AllowColumnReorder = true;
            this.listView_pricecomp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_lv_pc_code,
            this.columnHeader_lv_pc_curprice,
            this.columnHeader_lv_pc_chg});
            this.listView_pricecomp.FullRowSelect = true;
            this.listView_pricecomp.GridLines = true;
            this.listView_pricecomp.Location = new System.Drawing.Point(12, 70);
            this.listView_pricecomp.MultiSelect = false;
            this.listView_pricecomp.Name = "listView_pricecomp";
            this.listView_pricecomp.Size = new System.Drawing.Size(361, 421);
            this.listView_pricecomp.TabIndex = 4;
            this.listView_pricecomp.UseCompatibleStateImageBehavior = false;
            this.listView_pricecomp.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_lv_pc_code
            // 
            this.columnHeader_lv_pc_code.Text = "代码";
            // 
            // columnHeader_lv_pc_curprice
            // 
            this.columnHeader_lv_pc_curprice.Text = "当前价";
            // 
            // columnHeader_lv_pc_chg
            // 
            this.columnHeader_lv_pc_chg.Text = "涨幅";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "截止日期：";
            // 
            // textBox_pricecomp_enddate
            // 
            this.textBox_pricecomp_enddate.Location = new System.Drawing.Point(83, 43);
            this.textBox_pricecomp_enddate.Name = "textBox_pricecomp_enddate";
            this.textBox_pricecomp_enddate.Size = new System.Drawing.Size(100, 21);
            this.textBox_pricecomp_enddate.TabIndex = 5;
            // 
            // listView_advcomp
            // 
            this.listView_advcomp.AllowColumnReorder = true;
            this.listView_advcomp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeader_advcomp_code,
            this.colHeader_advcomp_price,
            this.colHeader_advcomp_chg,
            this.colHeader_advcomp_pe,
            this.colHeader_adv_ind_pe,
            this.colHeader_advcomp_roe,
            this.colHeader_advcomp_ind_roe,
            this.colHeader_advcomp_npr,
            this.colHeader_advcomp_ind_npr,
            this.colHead_cost,
            this.colHeader_cost_hist,
            this.colHeader_cost_ratio});
            this.listView_advcomp.FullRowSelect = true;
            this.listView_advcomp.GridLines = true;
            this.listView_advcomp.Location = new System.Drawing.Point(12, 69);
            this.listView_advcomp.MultiSelect = false;
            this.listView_advcomp.Name = "listView_advcomp";
            this.listView_advcomp.Size = new System.Drawing.Size(728, 421);
            this.listView_advcomp.TabIndex = 7;
            this.listView_advcomp.UseCompatibleStateImageBehavior = false;
            this.listView_advcomp.View = System.Windows.Forms.View.Details;
            // 
            // colHeader_advcomp_code
            // 
            this.colHeader_advcomp_code.Text = "代码";
            // 
            // colHeader_advcomp_price
            // 
            this.colHeader_advcomp_price.Text = "当前价";
            // 
            // colHeader_advcomp_chg
            // 
            this.colHeader_advcomp_chg.Text = "涨幅";
            // 
            // colHeader_advcomp_roe
            // 
            this.colHeader_advcomp_roe.Text = "ROE";
            // 
            // colHeader_advcomp_pe
            // 
            this.colHeader_advcomp_pe.Text = "PE";
            // 
            // colHeader_advcomp_ind_roe
            // 
            this.colHeader_advcomp_ind_roe.Text = "行业平均ROE";
            // 
            // colHeader_adv_ind_pe
            // 
            this.colHeader_adv_ind_pe.Text = "行业平均PE";
            // 
            // colHeader_advcomp_npr
            // 
            this.colHeader_advcomp_npr.Text = "净利润率";
            // 
            // colHeader_advcomp_ind_npr
            // 
            this.colHeader_advcomp_ind_npr.Text = "行业平均净利润率";
            // 
            // colHead_cost
            // 
            this.colHead_cost.Text = "性价比参数";
            // 
            // colHeader_cost_hist
            // 
            this.colHeader_cost_hist.Text = "历史性价比";
            // 
            // colHeader_cost_ratio
            // 
            this.colHeader_cost_ratio.Text = "性价比比值";
            // 
            // PriceComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 507);
            this.Controls.Add(this.listView_advcomp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_pricecomp_enddate);
            this.Controls.Add(this.listView_pricecomp);
            this.Controls.Add(this.button_pricecomp_refresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_pricecomp_begindate);
            this.Name = "PriceComparisonForm";
            this.Text = "价位对比";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_pricecomp_begindate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_pricecomp_refresh;
        private System.Windows.Forms.Timer timer_pricecomparison;
        private System.Windows.Forms.ListView listView_pricecomp;
        private System.Windows.Forms.ColumnHeader columnHeader_lv_pc_code;
        private System.Windows.Forms.ColumnHeader columnHeader_lv_pc_curprice;
        private System.Windows.Forms.ColumnHeader columnHeader_lv_pc_chg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_pricecomp_enddate;
        private System.Windows.Forms.ListView listView_advcomp;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_code;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_price;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_chg;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_roe;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_ind_roe;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_pe;
        private System.Windows.Forms.ColumnHeader colHeader_adv_ind_pe;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_npr;
        private System.Windows.Forms.ColumnHeader colHeader_advcomp_ind_npr;
        private System.Windows.Forms.ColumnHeader colHead_cost;
        private System.Windows.Forms.ColumnHeader colHeader_cost_hist;
        private System.Windows.Forms.ColumnHeader colHeader_cost_ratio;
    }
}