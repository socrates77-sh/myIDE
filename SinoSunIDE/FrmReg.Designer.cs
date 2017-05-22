namespace SinoSunIDE
{
    partial class FrmREG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmREG));
            this.dataGridView_reg = new SinoSunIDE.MyDataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_reg
            // 
            this.dataGridView_reg.AllowUserToAddRows = false;
            this.dataGridView_reg.AllowUserToResizeRows = false;
            this.dataGridView_reg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_reg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_reg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.regAddr,
            this.regValue});
            this.dataGridView_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_reg.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowHeadersVisible = false;
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(193, 414);
            this.dataGridView_reg.TabIndex = 0;
            this.dataGridView_reg.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGridViewRAM_CellValueChanged);
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // regAddr
            // 
            this.regAddr.HeaderText = "地址";
            this.regAddr.Name = "regAddr";
            this.regAddr.ReadOnly = true;
            // 
            // regValue
            // 
            this.regValue.HeaderText = "值";
            this.regValue.Name = "regValue";
            this.regValue.ReadOnly = true;
            // 
            // FrmREG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 414);
            this.Controls.Add(this.dataGridView_reg);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmREG";
            this.TabText = "Reg display";
            this.Text = "Reg display";
            this.Load += new System.EventHandler(this.FrmREG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public MyDataGridView dataGridView_reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn regAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn regValue;

    }
}