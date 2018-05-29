namespace StockAnalyzerApp
{
    partial class SortForm
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
            this.groupBox_sort_type = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_spec_quarter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_spec_year = new System.Windows.Forms.TextBox();
            this.radioButton_sort_cost_spec = new System.Windows.Forms.RadioButton();
            this.radioButton_sort_cost_quarter = new System.Windows.Forms.RadioButton();
            this.radioButton_sort_cost_yoy = new System.Windows.Forms.RadioButton();
            this.radioButton_sort_cost_dynamic = new System.Windows.Forms.RadioButton();
            this.radioButton_sort_cost_annual = new System.Windows.Forms.RadioButton();
            this.button_sort_ok = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_pricescale_refdate = new System.Windows.Forms.TextBox();
            this.radioButton_sort_pricescale = new System.Windows.Forms.RadioButton();
            this.groupBox_sort_type.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_sort_type
            // 
            this.groupBox_sort_type.Controls.Add(this.panel1);
            this.groupBox_sort_type.Location = new System.Drawing.Point(12, 12);
            this.groupBox_sort_type.Name = "groupBox_sort_type";
            this.groupBox_sort_type.Size = new System.Drawing.Size(473, 282);
            this.groupBox_sort_type.TabIndex = 0;
            this.groupBox_sort_type.TabStop = false;
            this.groupBox_sort_type.Text = "排序条件";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_pricescale_refdate);
            this.panel1.Controls.Add(this.radioButton_sort_pricescale);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_spec_quarter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_spec_year);
            this.panel1.Controls.Add(this.radioButton_sort_cost_spec);
            this.panel1.Controls.Add(this.radioButton_sort_cost_quarter);
            this.panel1.Controls.Add(this.radioButton_sort_cost_yoy);
            this.panel1.Controls.Add(this.radioButton_sort_cost_dynamic);
            this.panel1.Controls.Add(this.radioButton_sort_cost_annual);
            this.panel1.Location = new System.Drawing.Point(8, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 255);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "季度：";
            // 
            // textBox_spec_quarter
            // 
            this.textBox_spec_quarter.Location = new System.Drawing.Point(361, 142);
            this.textBox_spec_quarter.Name = "textBox_spec_quarter";
            this.textBox_spec_quarter.Size = new System.Drawing.Size(47, 21);
            this.textBox_spec_quarter.TabIndex = 7;
            this.textBox_spec_quarter.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "年度：";
            // 
            // textBox_spec_year
            // 
            this.textBox_spec_year.Location = new System.Drawing.Point(232, 143);
            this.textBox_spec_year.Name = "textBox_spec_year";
            this.textBox_spec_year.Size = new System.Drawing.Size(47, 21);
            this.textBox_spec_year.TabIndex = 5;
            this.textBox_spec_year.Text = "2013";
            // 
            // radioButton_sort_cost_spec
            // 
            this.radioButton_sort_cost_spec.AutoSize = true;
            this.radioButton_sort_cost_spec.Location = new System.Drawing.Point(23, 148);
            this.radioButton_sort_cost_spec.Name = "radioButton_sort_cost_spec";
            this.radioButton_sort_cost_spec.Size = new System.Drawing.Size(119, 16);
            this.radioButton_sort_cost_spec.TabIndex = 4;
            this.radioButton_sort_cost_spec.TabStop = true;
            this.radioButton_sort_cost_spec.Text = "按指定季度性价比";
            this.radioButton_sort_cost_spec.UseVisualStyleBackColor = true;
            // 
            // radioButton_sort_cost_quarter
            // 
            this.radioButton_sort_cost_quarter.AutoSize = true;
            this.radioButton_sort_cost_quarter.Location = new System.Drawing.Point(23, 116);
            this.radioButton_sort_cost_quarter.Name = "radioButton_sort_cost_quarter";
            this.radioButton_sort_cost_quarter.Size = new System.Drawing.Size(95, 16);
            this.radioButton_sort_cost_quarter.TabIndex = 3;
            this.radioButton_sort_cost_quarter.TabStop = true;
            this.radioButton_sort_cost_quarter.Text = "按季度性价比";
            this.radioButton_sort_cost_quarter.UseVisualStyleBackColor = true;
            // 
            // radioButton_sort_cost_yoy
            // 
            this.radioButton_sort_cost_yoy.AutoSize = true;
            this.radioButton_sort_cost_yoy.Location = new System.Drawing.Point(23, 85);
            this.radioButton_sort_cost_yoy.Name = "radioButton_sort_cost_yoy";
            this.radioButton_sort_cost_yoy.Size = new System.Drawing.Size(95, 16);
            this.radioButton_sort_cost_yoy.TabIndex = 2;
            this.radioButton_sort_cost_yoy.TabStop = true;
            this.radioButton_sort_cost_yoy.Text = "按同比性价比";
            this.radioButton_sort_cost_yoy.UseVisualStyleBackColor = true;
            // 
            // radioButton_sort_cost_dynamic
            // 
            this.radioButton_sort_cost_dynamic.AutoSize = true;
            this.radioButton_sort_cost_dynamic.Location = new System.Drawing.Point(23, 52);
            this.radioButton_sort_cost_dynamic.Name = "radioButton_sort_cost_dynamic";
            this.radioButton_sort_cost_dynamic.Size = new System.Drawing.Size(95, 16);
            this.radioButton_sort_cost_dynamic.TabIndex = 1;
            this.radioButton_sort_cost_dynamic.TabStop = true;
            this.radioButton_sort_cost_dynamic.Text = "按动态性价比";
            this.radioButton_sort_cost_dynamic.UseVisualStyleBackColor = true;
            // 
            // radioButton_sort_cost_annual
            // 
            this.radioButton_sort_cost_annual.AutoSize = true;
            this.radioButton_sort_cost_annual.Location = new System.Drawing.Point(23, 19);
            this.radioButton_sort_cost_annual.Name = "radioButton_sort_cost_annual";
            this.radioButton_sort_cost_annual.Size = new System.Drawing.Size(95, 16);
            this.radioButton_sort_cost_annual.TabIndex = 0;
            this.radioButton_sort_cost_annual.TabStop = true;
            this.radioButton_sort_cost_annual.Text = "按年度性价比";
            this.radioButton_sort_cost_annual.UseVisualStyleBackColor = true;
            // 
            // button_sort_ok
            // 
            this.button_sort_ok.Location = new System.Drawing.Point(404, 318);
            this.button_sort_ok.Name = "button_sort_ok";
            this.button_sort_ok.Size = new System.Drawing.Size(75, 23);
            this.button_sort_ok.TabIndex = 1;
            this.button_sort_ok.Text = "确定";
            this.button_sort_ok.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "日期：";
            // 
            // textBox_pricescale_refdate
            // 
            this.textBox_pricescale_refdate.Location = new System.Drawing.Point(232, 174);
            this.textBox_pricescale_refdate.Name = "textBox_pricescale_refdate";
            this.textBox_pricescale_refdate.Size = new System.Drawing.Size(102, 21);
            this.textBox_pricescale_refdate.TabIndex = 10;
            this.textBox_pricescale_refdate.Text = "20180201";
            // 
            // radioButton_sort_pricescale
            // 
            this.radioButton_sort_pricescale.AutoSize = true;
            this.radioButton_sort_pricescale.Location = new System.Drawing.Point(23, 179);
            this.radioButton_sort_pricescale.Name = "radioButton_sort_pricescale";
            this.radioButton_sort_pricescale.Size = new System.Drawing.Size(83, 16);
            this.radioButton_sort_pricescale.TabIndex = 9;
            this.radioButton_sort_pricescale.TabStop = true;
            this.radioButton_sort_pricescale.Text = "按相对价位";
            this.radioButton_sort_pricescale.UseVisualStyleBackColor = true;
            // 
            // SortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 367);
            this.Controls.Add(this.button_sort_ok);
            this.Controls.Add(this.groupBox_sort_type);
            this.Name = "SortForm";
            this.Text = "排序";
            this.groupBox_sort_type.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_sort_type;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_spec_quarter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_spec_year;
        private System.Windows.Forms.RadioButton radioButton_sort_cost_spec;
        private System.Windows.Forms.RadioButton radioButton_sort_cost_quarter;
        private System.Windows.Forms.RadioButton radioButton_sort_cost_yoy;
        private System.Windows.Forms.RadioButton radioButton_sort_cost_dynamic;
        private System.Windows.Forms.RadioButton radioButton_sort_cost_annual;
        private System.Windows.Forms.Button button_sort_ok;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_pricescale_refdate;
        private System.Windows.Forms.RadioButton radioButton_sort_pricescale;
    }
}