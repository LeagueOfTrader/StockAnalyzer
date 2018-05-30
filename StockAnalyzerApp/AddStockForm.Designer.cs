namespace StockAnalyzerApp
{
    partial class AddStockForm
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
            this.textBox_addstock_name = new System.Windows.Forms.TextBox();
            this.button_add_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_addstock_name
            // 
            this.textBox_addstock_name.Location = new System.Drawing.Point(29, 25);
            this.textBox_addstock_name.Name = "textBox_addstock_name";
            this.textBox_addstock_name.Size = new System.Drawing.Size(96, 21);
            this.textBox_addstock_name.TabIndex = 0;
            // 
            // button_add_ok
            // 
            this.button_add_ok.Location = new System.Drawing.Point(148, 23);
            this.button_add_ok.Name = "button_add_ok";
            this.button_add_ok.Size = new System.Drawing.Size(75, 23);
            this.button_add_ok.TabIndex = 1;
            this.button_add_ok.Text = "确定";
            this.button_add_ok.UseVisualStyleBackColor = true;
            this.button_add_ok.Click += new System.EventHandler(this.button_add_ok_Click);
            // 
            // AddStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 73);
            this.Controls.Add(this.button_add_ok);
            this.Controls.Add(this.textBox_addstock_name);
            this.Name = "AddStockForm";
            this.Text = "添加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_addstock_name;
        private System.Windows.Forms.Button button_add_ok;
    }
}