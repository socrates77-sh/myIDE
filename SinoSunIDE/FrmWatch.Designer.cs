namespace SinoSunIDE
{
    partial class FrmWatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWatch));
            this.DataGridView_Watch = new SinoSunIDE.MyDataGridView();
            this.NameW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddrW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueDEC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Watch)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_Watch
            // 
            this.DataGridView_Watch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Watch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Watch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameW,
            this.AddrW,
            this.ValueW,
            this.Column1,
            this.Column2,
            this.ValueDEC,
            this.Column3,
            this.Column4});
            this.DataGridView_Watch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Watch.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_Watch.Name = "DataGridView_Watch";
            this.DataGridView_Watch.RowHeadersVisible = false;
            this.DataGridView_Watch.RowTemplate.Height = 23;
            this.DataGridView_Watch.Size = new System.Drawing.Size(601, 185);
            this.DataGridView_Watch.TabIndex = 0;
            this.DataGridView_Watch.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGridViewWatch_CellValueChanged);
            this.DataGridView_Watch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeletekeyDown);
            // 
            // NameW
            // 
            this.NameW.HeaderText = "名称";
            this.NameW.Name = "NameW";
            // 
            // AddrW
            // 
            this.AddrW.HeaderText = "地址";
            this.AddrW.Name = "AddrW";
            this.AddrW.ReadOnly = true;
            // 
            // ValueW
            // 
            this.ValueW.HeaderText = "值(十六进制)";
            this.ValueW.Name = "ValueW";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // ValueDEC
            // 
            this.ValueDEC.HeaderText = "值(十进制)";
            this.ValueDEC.Name = "ValueDEC";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // FrmWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 185);
            this.Controls.Add(this.DataGridView_Watch);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWatch";
            this.Text = "观察窗";
            this.Load += new System.EventHandler(this.FrmWatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Watch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public MyDataGridView DataGridView_Watch;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameW;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddrW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueW;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueDEC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        //private System.Windows.Forms.DataGridViewTextBoxColumn wName;
        //private System.Windows.Forms.DataGridViewTextBoxColumn wAddr;
        //private System.Windows.Forms.DataGridViewTextBoxColumn wValue;
    }
}