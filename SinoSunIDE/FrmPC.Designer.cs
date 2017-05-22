namespace SinoSunIDE
{
    partial class FrmPC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPC));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lab_mcuName = new System.Windows.Forms.ToolStripLabel();
            this.bt_NoSort = new System.Windows.Forms.ToolStripButton();
            this.optionValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btSaveOpitonSet = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.propertyGridEx_PC = new PropertyGridEx.PropertyGridEx();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.optionValue0 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue1 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.optionValue3 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue4 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue5 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue6 = new System.Windows.Forms.ToolStripTextBox();
            this.optionValue7 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lab_mcuName,
            this.bt_NoSort,
            this.optionValue});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(123, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lab_mcuName
            // 
            this.lab_mcuName.Name = "lab_mcuName";
            this.lab_mcuName.Size = new System.Drawing.Size(36, 22);
            this.lab_mcuName.Text = "OTP ";
            // 
            // bt_NoSort
            // 
            this.bt_NoSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_NoSort.Image = global::SinoSunIDE.Properties.Resources._00_00_;
            this.bt_NoSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_NoSort.Name = "bt_NoSort";
            this.bt_NoSort.Size = new System.Drawing.Size(23, 22);
            this.bt_NoSort.ToolTipText = "按从低位到高位排序";
            this.bt_NoSort.Click += new System.EventHandler(this.bt_NoSort_Click);
            // 
            // optionValue
            // 
            this.optionValue.Name = "optionValue";
            this.optionValue.Size = new System.Drawing.Size(50, 25);
            this.optionValue.TextChanged += new System.EventHandler(this.option_value_change);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.propertyGridEx_PC);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(293, 283);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(293, 383);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip3);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip4);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSaveOpitonSet,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(114, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // btSaveOpitonSet
            // 
            this.btSaveOpitonSet.Name = "btSaveOpitonSet";
            this.btSaveOpitonSet.Size = new System.Drawing.Size(56, 22);
            this.btSaveOpitonSet.Text = "保存设置";
            this.btSaveOpitonSet.Click += new System.EventHandler(this.bt_OK);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::SinoSunIDE.Properties.Resources._01_00_;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "SaveOption";
            this.toolStripButton1.ToolTipText = "Save Option";
            this.toolStripButton1.Click += new System.EventHandler(this.bt_OK);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // propertyGridEx_PC
            // 
            // 
            // 
            // 
            this.propertyGridEx_PC.DocCommentDescription.AutoEllipsis = true;
            this.propertyGridEx_PC.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridEx_PC.DocCommentDescription.Location = new System.Drawing.Point(3, 19);
            this.propertyGridEx_PC.DocCommentDescription.Name = "";
            this.propertyGridEx_PC.DocCommentDescription.Size = new System.Drawing.Size(287, 36);
            this.propertyGridEx_PC.DocCommentDescription.TabIndex = 1;
            this.propertyGridEx_PC.DocCommentImage = null;
            // 
            // 
            // 
            this.propertyGridEx_PC.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridEx_PC.DocCommentTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.propertyGridEx_PC.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.propertyGridEx_PC.DocCommentTitle.Name = "";
            this.propertyGridEx_PC.DocCommentTitle.Size = new System.Drawing.Size(287, 16);
            this.propertyGridEx_PC.DocCommentTitle.TabIndex = 0;
            this.propertyGridEx_PC.DocCommentTitle.UseMnemonic = false;
            this.propertyGridEx_PC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridEx_PC.Location = new System.Drawing.Point(0, 0);
            this.propertyGridEx_PC.LVRE = true;
            this.propertyGridEx_PC.LVRS = true;
            this.propertyGridEx_PC.Name = "propertyGridEx_PC";
            this.propertyGridEx_PC.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGridEx_PC.RESE = true;
            this.propertyGridEx_PC.RSTE = true;
            this.propertyGridEx_PC.Size = new System.Drawing.Size(293, 283);
            this.propertyGridEx_PC.TabIndex = 0;
            // 
            // 
            // 
            this.propertyGridEx_PC.ToolStrip.AccessibleName = "工具栏";
            this.propertyGridEx_PC.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.propertyGridEx_PC.ToolStrip.AllowMerge = false;
            this.propertyGridEx_PC.ToolStrip.AutoSize = false;
            this.propertyGridEx_PC.ToolStrip.CanOverflow = false;
            this.propertyGridEx_PC.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.propertyGridEx_PC.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.propertyGridEx_PC.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.propertyGridEx_PC.ToolStrip.Name = "";
            this.propertyGridEx_PC.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.propertyGridEx_PC.ToolStrip.Size = new System.Drawing.Size(293, 25);
            this.propertyGridEx_PC.ToolStrip.TabIndex = 1;
            this.propertyGridEx_PC.ToolStrip.TabStop = true;
            this.propertyGridEx_PC.ToolStrip.Text = "PropertyGridToolBar";
            this.propertyGridEx_PC.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.Properties_PropertyValueChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionValue0,
            this.optionValue1,
            this.optionValue2});
            this.toolStrip3.Location = new System.Drawing.Point(3, 25);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(168, 25);
            this.toolStrip3.TabIndex = 2;
            // 
            // optionValue0
            // 
            this.optionValue0.Name = "optionValue0";
            this.optionValue0.Size = new System.Drawing.Size(50, 25);
            // 
            // optionValue1
            // 
            this.optionValue1.Name = "optionValue1";
            this.optionValue1.Size = new System.Drawing.Size(50, 25);
            // 
            // optionValue2
            // 
            this.optionValue2.Name = "optionValue2";
            this.optionValue2.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionValue3,
            this.optionValue4,
            this.optionValue5,
            this.optionValue6,
            this.optionValue7});
            this.toolStrip4.Location = new System.Drawing.Point(3, 50);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(168, 25);
            this.toolStrip4.TabIndex = 3;
            // 
            // optionValue3
            // 
            this.optionValue3.Name = "optionValue3";
            this.optionValue3.Size = new System.Drawing.Size(50, 25);
            // 
            // optionValue4
            // 
            this.optionValue4.Name = "optionValue4";
            this.optionValue4.Size = new System.Drawing.Size(50, 25);
            // 
            // optionValue5
            // 
            this.optionValue5.Name = "optionValue5";
            this.optionValue5.Size = new System.Drawing.Size(50, 25);
            // 
            // optionValue6
            // 
            this.optionValue6.Name = "optionValue6";
            this.optionValue6.Size = new System.Drawing.Size(50, 25);
            this.optionValue6.Visible = false;
            // 
            // optionValue7
            // 
            this.optionValue7.Name = "optionValue7";
            this.optionValue7.Size = new System.Drawing.Size(50, 25);
            this.optionValue7.Visible = false;
            // 
            // FrmPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 383);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPC";
            this.Text = "MCU OPTION值";
            this.Load += new System.EventHandler(this.FrmPC_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public PropertyGridEx.PropertyGridEx propertyGridEx_PC;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lab_mcuName;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripButton bt_NoSort;
        private System.Windows.Forms.ToolStripTextBox optionValue;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel btSaveOpitonSet;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip3;
        public System.Windows.Forms.ToolStripTextBox optionValue0;
        public System.Windows.Forms.ToolStripTextBox optionValue1;
        public System.Windows.Forms.ToolStripTextBox optionValue2;
        private System.Windows.Forms.ToolStrip toolStrip4;
        public System.Windows.Forms.ToolStripTextBox optionValue3;
        public System.Windows.Forms.ToolStripTextBox optionValue4;
        public System.Windows.Forms.ToolStripTextBox optionValue5;
        public System.Windows.Forms.ToolStripTextBox optionValue6;
        public System.Windows.Forms.ToolStripTextBox optionValue7;
    }
}