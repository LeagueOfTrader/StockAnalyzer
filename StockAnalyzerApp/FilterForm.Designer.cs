namespace StockAnalyzerApp
{
    partial class FilterForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox_src = new System.Windows.Forms.ListBox();
            this.button_import_src = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_src_count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_remove_src = new System.Windows.Forms.Button();
            this.button_add_src = new System.Windows.Forms.Button();
            this.button_export_src = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_screen_count = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_screen_sort = new System.Windows.Forms.Button();
            this.button_screen_remove = new System.Windows.Forms.Button();
            this.button_screen_add = new System.Windows.Forms.Button();
            this.button_export_screened = new System.Windows.Forms.Button();
            this.listBox_screened = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_select_count = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_select_sort = new System.Windows.Forms.Button();
            this.button_select_remove = new System.Windows.Forms.Button();
            this.button_select_add = new System.Windows.Forms.Button();
            this.button_save_select = new System.Windows.Forms.Button();
            this.button_load_select = new System.Windows.Forms.Button();
            this.listBox_selfSelect = new System.Windows.Forms.ListBox();
            this.button_screen = new System.Windows.Forms.Button();
            this.button_move_to_src = new System.Windows.Forms.Button();
            this.button_add_to_select = new System.Windows.Forms.Button();
            this.button_src_defaultAll = new System.Windows.Forms.Button();
            this.appStockListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appStockListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_src
            // 
            this.listBox_src.FormattingEnabled = true;
            this.listBox_src.ItemHeight = 12;
            this.listBox_src.Location = new System.Drawing.Point(24, 24);
            this.listBox_src.Name = "listBox_src";
            this.listBox_src.Size = new System.Drawing.Size(140, 280);
            this.listBox_src.TabIndex = 0;
            // 
            // button_import_src
            // 
            this.button_import_src.Location = new System.Drawing.Point(22, 318);
            this.button_import_src.Name = "button_import_src";
            this.button_import_src.Size = new System.Drawing.Size(75, 23);
            this.button_import_src.TabIndex = 1;
            this.button_import_src.Text = "导入";
            this.button_import_src.UseVisualStyleBackColor = true;
            this.button_import_src.Click += new System.EventHandler(this.button_import_src_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_src_defaultAll);
            this.groupBox1.Controls.Add(this.label_src_count);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_remove_src);
            this.groupBox1.Controls.Add(this.button_add_src);
            this.groupBox1.Controls.Add(this.button_export_src);
            this.groupBox1.Controls.Add(this.button_import_src);
            this.groupBox1.Controls.Add(this.listBox_src);
            this.groupBox1.Location = new System.Drawing.Point(31, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 359);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "候选列表";
            // 
            // label_src_count
            // 
            this.label_src_count.AutoSize = true;
            this.label_src_count.Location = new System.Drawing.Point(198, 265);
            this.label_src_count.Name = "label_src_count";
            this.label_src_count.Size = new System.Drawing.Size(11, 12);
            this.label_src_count.TabIndex = 6;
            this.label_src_count.Text = "0";
            this.label_src_count.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "数量：";
            // 
            // button_remove_src
            // 
            this.button_remove_src.Location = new System.Drawing.Point(170, 87);
            this.button_remove_src.Name = "button_remove_src";
            this.button_remove_src.Size = new System.Drawing.Size(50, 23);
            this.button_remove_src.TabIndex = 4;
            this.button_remove_src.Text = "删除";
            this.button_remove_src.UseVisualStyleBackColor = true;
            // 
            // button_add_src
            // 
            this.button_add_src.Location = new System.Drawing.Point(170, 42);
            this.button_add_src.Name = "button_add_src";
            this.button_add_src.Size = new System.Drawing.Size(50, 23);
            this.button_add_src.TabIndex = 3;
            this.button_add_src.Text = "添加";
            this.button_add_src.UseVisualStyleBackColor = true;
            // 
            // button_export_src
            // 
            this.button_export_src.Location = new System.Drawing.Point(110, 318);
            this.button_export_src.Name = "button_export_src";
            this.button_export_src.Size = new System.Drawing.Size(75, 23);
            this.button_export_src.TabIndex = 2;
            this.button_export_src.Text = "导出";
            this.button_export_src.UseVisualStyleBackColor = true;
            this.button_export_src.Click += new System.EventHandler(this.button_export_src_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_screen_count);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button_screen_sort);
            this.groupBox2.Controls.Add(this.button_screen_remove);
            this.groupBox2.Controls.Add(this.button_screen_add);
            this.groupBox2.Controls.Add(this.button_export_screened);
            this.groupBox2.Controls.Add(this.listBox_screened);
            this.groupBox2.Location = new System.Drawing.Point(365, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 359);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "筛选列表";
            // 
            // label_screen_count
            // 
            this.label_screen_count.AutoSize = true;
            this.label_screen_count.Location = new System.Drawing.Point(200, 265);
            this.label_screen_count.Name = "label_screen_count";
            this.label_screen_count.Size = new System.Drawing.Size(11, 12);
            this.label_screen_count.TabIndex = 9;
            this.label_screen_count.Text = "0";
            this.label_screen_count.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "数量：";
            // 
            // button_screen_sort
            // 
            this.button_screen_sort.Location = new System.Drawing.Point(170, 136);
            this.button_screen_sort.Name = "button_screen_sort";
            this.button_screen_sort.Size = new System.Drawing.Size(50, 23);
            this.button_screen_sort.TabIndex = 7;
            this.button_screen_sort.Text = "排序";
            this.button_screen_sort.UseVisualStyleBackColor = true;
            // 
            // button_screen_remove
            // 
            this.button_screen_remove.Location = new System.Drawing.Point(170, 87);
            this.button_screen_remove.Name = "button_screen_remove";
            this.button_screen_remove.Size = new System.Drawing.Size(50, 23);
            this.button_screen_remove.TabIndex = 6;
            this.button_screen_remove.Text = "删除";
            this.button_screen_remove.UseVisualStyleBackColor = true;
            // 
            // button_screen_add
            // 
            this.button_screen_add.Location = new System.Drawing.Point(170, 42);
            this.button_screen_add.Name = "button_screen_add";
            this.button_screen_add.Size = new System.Drawing.Size(50, 23);
            this.button_screen_add.TabIndex = 5;
            this.button_screen_add.Text = "添加";
            this.button_screen_add.UseVisualStyleBackColor = true;
            // 
            // button_export_screened
            // 
            this.button_export_screened.Location = new System.Drawing.Point(66, 318);
            this.button_export_screened.Name = "button_export_screened";
            this.button_export_screened.Size = new System.Drawing.Size(75, 23);
            this.button_export_screened.TabIndex = 2;
            this.button_export_screened.Text = "导出";
            this.button_export_screened.UseVisualStyleBackColor = true;
            this.button_export_screened.Click += new System.EventHandler(this.button_export_screened_Click);
            // 
            // listBox_screened
            // 
            this.listBox_screened.FormattingEnabled = true;
            this.listBox_screened.ItemHeight = 12;
            this.listBox_screened.Location = new System.Drawing.Point(24, 24);
            this.listBox_screened.Name = "listBox_screened";
            this.listBox_screened.Size = new System.Drawing.Size(140, 280);
            this.listBox_screened.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_select_count);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.button_select_sort);
            this.groupBox3.Controls.Add(this.button_select_remove);
            this.groupBox3.Controls.Add(this.button_select_add);
            this.groupBox3.Controls.Add(this.button_save_select);
            this.groupBox3.Controls.Add(this.button_load_select);
            this.groupBox3.Controls.Add(this.listBox_selfSelect);
            this.groupBox3.Location = new System.Drawing.Point(699, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 359);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "自选列表";
            // 
            // label_select_count
            // 
            this.label_select_count.AutoSize = true;
            this.label_select_count.Location = new System.Drawing.Point(200, 265);
            this.label_select_count.Name = "label_select_count";
            this.label_select_count.Size = new System.Drawing.Size(11, 12);
            this.label_select_count.TabIndex = 10;
            this.label_select_count.Text = "0";
            this.label_select_count.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "数量：";
            // 
            // button_select_sort
            // 
            this.button_select_sort.Location = new System.Drawing.Point(170, 136);
            this.button_select_sort.Name = "button_select_sort";
            this.button_select_sort.Size = new System.Drawing.Size(50, 23);
            this.button_select_sort.TabIndex = 8;
            this.button_select_sort.Text = "排序";
            this.button_select_sort.UseVisualStyleBackColor = true;
            // 
            // button_select_remove
            // 
            this.button_select_remove.Location = new System.Drawing.Point(170, 87);
            this.button_select_remove.Name = "button_select_remove";
            this.button_select_remove.Size = new System.Drawing.Size(50, 23);
            this.button_select_remove.TabIndex = 6;
            this.button_select_remove.Text = "删除";
            this.button_select_remove.UseVisualStyleBackColor = true;
            // 
            // button_select_add
            // 
            this.button_select_add.Location = new System.Drawing.Point(170, 42);
            this.button_select_add.Name = "button_select_add";
            this.button_select_add.Size = new System.Drawing.Size(50, 23);
            this.button_select_add.TabIndex = 5;
            this.button_select_add.Text = "添加";
            this.button_select_add.UseVisualStyleBackColor = true;
            // 
            // button_save_select
            // 
            this.button_save_select.Location = new System.Drawing.Point(110, 318);
            this.button_save_select.Name = "button_save_select";
            this.button_save_select.Size = new System.Drawing.Size(75, 23);
            this.button_save_select.TabIndex = 2;
            this.button_save_select.Text = "保存";
            this.button_save_select.UseVisualStyleBackColor = true;
            this.button_save_select.Click += new System.EventHandler(this.button_save_select_Click);
            // 
            // button_load_select
            // 
            this.button_load_select.Location = new System.Drawing.Point(22, 318);
            this.button_load_select.Name = "button_load_select";
            this.button_load_select.Size = new System.Drawing.Size(75, 23);
            this.button_load_select.TabIndex = 1;
            this.button_load_select.Text = "读取";
            this.button_load_select.UseVisualStyleBackColor = true;
            this.button_load_select.Click += new System.EventHandler(this.button_load_select_Click);
            // 
            // listBox_selfSelect
            // 
            this.listBox_selfSelect.FormattingEnabled = true;
            this.listBox_selfSelect.ItemHeight = 12;
            this.listBox_selfSelect.Location = new System.Drawing.Point(24, 24);
            this.listBox_selfSelect.Name = "listBox_selfSelect";
            this.listBox_selfSelect.Size = new System.Drawing.Size(140, 280);
            this.listBox_selfSelect.TabIndex = 0;
            // 
            // button_screen
            // 
            this.button_screen.Location = new System.Drawing.Point(274, 140);
            this.button_screen.Name = "button_screen";
            this.button_screen.Size = new System.Drawing.Size(75, 23);
            this.button_screen.TabIndex = 5;
            this.button_screen.Text = "筛选==>";
            this.button_screen.UseVisualStyleBackColor = true;
            this.button_screen.Click += new System.EventHandler(this.button_screen_Click);
            // 
            // button_move_to_src
            // 
            this.button_move_to_src.Location = new System.Drawing.Point(274, 212);
            this.button_move_to_src.Name = "button_move_to_src";
            this.button_move_to_src.Size = new System.Drawing.Size(75, 23);
            this.button_move_to_src.TabIndex = 6;
            this.button_move_to_src.Text = "<==转存";
            this.button_move_to_src.UseVisualStyleBackColor = true;
            this.button_move_to_src.Click += new System.EventHandler(this.button_move_to_src_Click);
            // 
            // button_add_to_select
            // 
            this.button_add_to_select.Location = new System.Drawing.Point(608, 139);
            this.button_add_to_select.Name = "button_add_to_select";
            this.button_add_to_select.Size = new System.Drawing.Size(75, 23);
            this.button_add_to_select.TabIndex = 7;
            this.button_add_to_select.Text = "加入==>";
            this.button_add_to_select.UseVisualStyleBackColor = true;
            this.button_add_to_select.Click += new System.EventHandler(this.button_add_to_select_Click);
            // 
            // button_src_defaultAll
            // 
            this.button_src_defaultAll.Location = new System.Drawing.Point(170, 136);
            this.button_src_defaultAll.Name = "button_src_defaultAll";
            this.button_src_defaultAll.Size = new System.Drawing.Size(50, 23);
            this.button_src_defaultAll.TabIndex = 7;
            this.button_src_defaultAll.Text = "所有";
            this.button_src_defaultAll.UseVisualStyleBackColor = true;
            this.button_src_defaultAll.Click += new System.EventHandler(this.button_src_defaultAll_Click);
            // 
            // appStockListBindingSource
            // 
            this.appStockListBindingSource.DataSource = typeof(StockAnalyzerApp.AppData.AppStockList);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 418);
            this.Controls.Add(this.button_add_to_select);
            this.Controls.Add(this.button_move_to_src);
            this.Controls.Add(this.button_screen);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FilterForm";
            this.Text = "股票筛选";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appStockListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_src;
        private System.Windows.Forms.Button button_import_src;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_export_src;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_export_screened;
        private System.Windows.Forms.ListBox listBox_screened;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_save_select;
        private System.Windows.Forms.Button button_load_select;
        private System.Windows.Forms.ListBox listBox_selfSelect;
        private System.Windows.Forms.Button button_remove_src;
        private System.Windows.Forms.Button button_add_src;
        private System.Windows.Forms.Button button_screen_sort;
        private System.Windows.Forms.Button button_screen_remove;
        private System.Windows.Forms.Button button_screen_add;
        private System.Windows.Forms.Button button_select_sort;
        private System.Windows.Forms.Button button_select_remove;
        private System.Windows.Forms.Button button_select_add;
        private System.Windows.Forms.Button button_screen;
        private System.Windows.Forms.Button button_move_to_src;
        private System.Windows.Forms.Button button_add_to_select;
        private System.Windows.Forms.Label label_src_count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_screen_count;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_select_count;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource appStockListBindingSource;
        private System.Windows.Forms.Button button_src_defaultAll;
    }
}

