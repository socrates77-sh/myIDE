namespace XmlGenerator
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView_reg = new System.Windows.Forms.DataGridView();
            this.regName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Properties = new PropertyGridEx.PropertyGridEx();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_reg
            // 
            this.dataGridView_reg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_reg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regName,
            this.regAddr,
            this.regValue});
            this.dataGridView_reg.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_reg.Name = "dataGridView_reg";
            this.dataGridView_reg.RowHeadersVisible = false;
            this.dataGridView_reg.RowTemplate.Height = 23;
            this.dataGridView_reg.Size = new System.Drawing.Size(269, 357);
            this.dataGridView_reg.TabIndex = 0;
            // 
            // regName
            // 
            this.regName.HeaderText = "寄存器名称";
            this.regName.Name = "regName";
            // 
            // regAddr
            // 
            this.regAddr.HeaderText = "地址";
            this.regAddr.Name = "regAddr";
            // 
            // regValue
            // 
            this.regValue.HeaderText = "值";
            this.regValue.Name = "regValue";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // Properties
            // 
            // 
            // 
            // 
            this.Properties.DocCommentDescription.AutoEllipsis = true;
            this.Properties.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.Properties.DocCommentDescription.Location = new System.Drawing.Point(3, 19);
            this.Properties.DocCommentDescription.Name = "";
            this.Properties.DocCommentDescription.Size = new System.Drawing.Size(232, 36);
            this.Properties.DocCommentDescription.TabIndex = 1;
            this.Properties.DocCommentImage = null;
            // 
            // 
            // 
            this.Properties.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.Properties.DocCommentTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.Properties.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.Properties.DocCommentTitle.Name = "";
            this.Properties.DocCommentTitle.Size = new System.Drawing.Size(232, 16);
            this.Properties.DocCommentTitle.TabIndex = 0;
            this.Properties.DocCommentTitle.UseMnemonic = false;
            this.Properties.Location = new System.Drawing.Point(397, 12);
            this.Properties.Name = "Properties";
            this.Properties.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.Properties.Size = new System.Drawing.Size(238, 357);
            this.Properties.TabIndex = 1;
            // 
            // 
            // 
            this.Properties.ToolStrip.AccessibleName = "工具栏";
            this.Properties.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.Properties.ToolStrip.AllowMerge = false;
            this.Properties.ToolStrip.AutoSize = false;
            this.Properties.ToolStrip.CanOverflow = false;
            this.Properties.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.Properties.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Properties.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.Properties.ToolStrip.Name = "";
            this.Properties.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.Properties.ToolStrip.Size = new System.Drawing.Size(238, 25);
            this.Properties.ToolStrip.TabIndex = 1;
            this.Properties.ToolStrip.TabStop = true;
            this.Properties.ToolStrip.Text = "PropertyGridToolBar";
            this.Properties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.Properties_PropertyValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 377);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Properties);
            this.Controls.Add(this.dataGridView_reg);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_reg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn regName;
        private System.Windows.Forms.DataGridViewTextBoxColumn regAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn regValue;
        private PropertyGridEx.PropertyGridEx Properties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;

    }
}

