namespace COD
{
    partial class FrmProcessS19
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
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.cancelbt = new System.Windows.Forms.Button();
            this.OKbt = new System.Windows.Forms.Button();
            this.Selectbt = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Namelab = new System.Windows.Forms.Label();
            this.S19openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(293, 25);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(100, 22);
            this.richTextBox.TabIndex = 10;
            this.richTextBox.Text = "";
            this.richTextBox.Visible = false;
            // 
            // cancelbt
            // 
            this.cancelbt.Location = new System.Drawing.Point(231, 95);
            this.cancelbt.Name = "cancelbt";
            this.cancelbt.Size = new System.Drawing.Size(93, 32);
            this.cancelbt.TabIndex = 8;
            this.cancelbt.Text = "取消";
            this.cancelbt.UseVisualStyleBackColor = true;
            this.cancelbt.Click += new System.EventHandler(this.cancelbt_Click);
            // 
            // OKbt
            // 
            this.OKbt.Location = new System.Drawing.Point(110, 95);
            this.OKbt.Name = "OKbt";
            this.OKbt.Size = new System.Drawing.Size(93, 32);
            this.OKbt.TabIndex = 9;
            this.OKbt.Text = "合并";
            this.OKbt.UseVisualStyleBackColor = true;
            this.OKbt.Click += new System.EventHandler(this.OKbt_Click);
            // 
            // Selectbt
            // 
            this.Selectbt.Location = new System.Drawing.Point(483, 56);
            this.Selectbt.Name = "Selectbt";
            this.Selectbt.Size = new System.Drawing.Size(65, 20);
            this.Selectbt.TabIndex = 7;
            this.Selectbt.Text = "浏览...";
            this.Selectbt.UseVisualStyleBackColor = true;
            this.Selectbt.Click += new System.EventHandler(this.Selectbt_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(467, 21);
            this.textBox1.TabIndex = 6;
            // 
            // Namelab
            // 
            this.Namelab.AutoSize = true;
            this.Namelab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Namelab.Location = new System.Drawing.Point(7, 31);
            this.Namelab.Name = "Namelab";
            this.Namelab.Size = new System.Drawing.Size(144, 16);
            this.Namelab.TabIndex = 5;
            this.Namelab.Text = "指定主目标文件：";
            // 
            // S19openFileDialog
            // 
            this.S19openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmProcessS19
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 175);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.cancelbt);
            this.Controls.Add(this.OKbt);
            this.Controls.Add(this.Selectbt);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Namelab);
            this.Name = "FrmProcessS19";
            this.Text = "合并S19";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button cancelbt;
        private System.Windows.Forms.Button OKbt;
        private System.Windows.Forms.Button Selectbt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Namelab;
        private System.Windows.Forms.OpenFileDialog S19openFileDialog;
    }
}