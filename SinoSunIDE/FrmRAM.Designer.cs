namespace SinoSunIDE
{
    partial class FrmRAM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRAM));
            this.toolStrip_RAMwindows = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.RAM_addr = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.myDataGridViewRAM = new SinoSunIDE.MyDataGridView();
            this.Addr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip_RAMwindows.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridViewRAM)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip_RAMwindows
            // 
            this.toolStrip_RAMwindows.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip_RAMwindows.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.RAM_addr});
            this.toolStrip_RAMwindows.Location = new System.Drawing.Point(3, 0);
            this.toolStrip_RAMwindows.Name = "toolStrip_RAMwindows";
            this.toolStrip_RAMwindows.Size = new System.Drawing.Size(171, 25);
            this.toolStrip_RAMwindows.TabIndex = 1;
            this.toolStrip_RAMwindows.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel1.Text = "地址：0x";
            // 
            // RAM_addr
            // 
            this.RAM_addr.Name = "RAM_addr";
            this.RAM_addr.Size = new System.Drawing.Size(100, 25);
            this.RAM_addr.Text = "00";
            this.RAM_addr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataFind_KeyDown_Clink);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.myDataGridViewRAM);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(551, 234);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(551, 259);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip_RAMwindows);
            // 
            // myDataGridViewRAM
            // 
            this.myDataGridViewRAM.AllowUserToAddRows = false;
            this.myDataGridViewRAM.AllowUserToResizeRows = false;
            this.myDataGridViewRAM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.myDataGridViewRAM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridViewRAM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Addr,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column1,
            this.Column15,
            this.Column16});
            this.myDataGridViewRAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataGridViewRAM.Location = new System.Drawing.Point(0, 0);
            this.myDataGridViewRAM.Name = "myDataGridViewRAM";
            this.myDataGridViewRAM.RowHeadersVisible = false;
            this.myDataGridViewRAM.RowHeadersWidth = 20;
            this.myDataGridViewRAM.RowTemplate.Height = 23;
            this.myDataGridViewRAM.Size = new System.Drawing.Size(551, 234);
            this.myDataGridViewRAM.TabIndex = 0;
            this.myDataGridViewRAM.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGridViewRAM_CellValueChanged);
            // 
            // Addr
            // 
            this.Addr.HeaderText = "Addr";
            this.Addr.Name = "Addr";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "00";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "01";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "02";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "03";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "04";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "05";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "06";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "07";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "08";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "09";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "0A";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "0B";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "0C";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "0D";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "0E";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "0F";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // FrmRAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 259);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRAM";
            this.Text = "RAM data Display";
            this.Load += new System.EventHandler(this.FrmRAM_Load);
            this.toolStrip_RAMwindows.ResumeLayout(false);
            this.toolStrip_RAMwindows.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridViewRAM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public  MyDataGridView myDataGridViewRAM;
        private System.Windows.Forms.ToolStrip toolStrip_RAMwindows;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox RAM_addr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Addr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
    }
}