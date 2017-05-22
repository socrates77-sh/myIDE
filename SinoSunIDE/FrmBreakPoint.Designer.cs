﻿namespace SinoSunIDE
{
    partial class FrmBreakPoint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBreakPoint));
            this.myDataGridViewBK = new SinoSunIDE.MyDataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Addr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Files = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridViewBK)).BeginInit();
            this.SuspendLayout();
            // 
            // myDataGridViewBK
            // 
            this.myDataGridViewBK.AllowUserToAddRows = false;
            this.myDataGridViewBK.AllowUserToDeleteRows = false;
            this.myDataGridViewBK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myDataGridViewBK.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.myDataGridViewBK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridViewBK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.Addr,
            this.Files});
            this.myDataGridViewBK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataGridViewBK.EnableHeadersVisualStyles = false;
            this.myDataGridViewBK.Location = new System.Drawing.Point(0, 0);
            this.myDataGridViewBK.Name = "myDataGridViewBK";
            this.myDataGridViewBK.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myDataGridViewBK.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.myDataGridViewBK.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.myDataGridViewBK.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.myDataGridViewBK.RowTemplate.Height = 23;
            this.myDataGridViewBK.Size = new System.Drawing.Size(243, 377);
            this.myDataGridViewBK.TabIndex = 0;
            this.myDataGridViewBK.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGridViewBK_CellValueChanged);
            this.myDataGridViewBK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyDataGridView_KeyPress);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Addr
            // 
            this.Addr.HeaderText = "Addr";
            this.Addr.Name = "Addr";
            this.Addr.ReadOnly = true;
            // 
            // Files
            // 
            this.Files.HeaderText = "Files";
            this.Files.Name = "Files";
            this.Files.ReadOnly = true;
            // 
            // FrmBreakPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 377);
            this.Controls.Add(this.myDataGridViewBK);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBreakPoint";
            this.Tag = "BreakPoint";
            this.Text = "BreakPoint List";
            this.Load += new System.EventHandler(this.Load_Click);
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridViewBK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public MyDataGridView myDataGridViewBK;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Addr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Files;

    }
}