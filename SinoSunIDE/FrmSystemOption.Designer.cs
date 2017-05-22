namespace SinoSunIDE
{
    partial class FrmSystemOption
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("字体设置");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("制表符设置");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("区域设置");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("编辑器", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView_SystemConfiguration = new System.Windows.Forms.TreeView();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.FontSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SpaceForTab = new System.Windows.Forms.CheckBox();
            this.TabSize = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LanguageSet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_SystemConfiguration
            // 
            this.treeView_SystemConfiguration.Location = new System.Drawing.Point(-2, 0);
            this.treeView_SystemConfiguration.Name = "treeView_SystemConfiguration";
            treeNode1.Name = "Editor_font";
            treeNode1.Text = "字体设置";
            treeNode2.Name = "Editor_Tab";
            treeNode2.Text = "制表符设置";
            treeNode3.Name = "Editor_Language";
            treeNode3.Text = "区域设置";
            treeNode4.Name = "Editor";
            treeNode4.Text = "编辑器";
            this.treeView_SystemConfiguration.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView_SystemConfiguration.Size = new System.Drawing.Size(184, 274);
            this.treeView_SystemConfiguration.TabIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(384, 274);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(284, 274);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // FontSize
            // 
            this.FontSize.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FontSize.FormattingEnabled = true;
            this.FontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.FontSize.Location = new System.Drawing.Point(73, 20);
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(121, 24);
            this.FontSize.TabIndex = 5;
            this.FontSize.Text = "12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(17, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "制表符大小：";
            // 
            // SpaceForTab
            // 
            this.SpaceForTab.AutoSize = true;
            this.SpaceForTab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SpaceForTab.Location = new System.Drawing.Point(62, 67);
            this.SpaceForTab.Name = "SpaceForTab";
            this.SpaceForTab.Size = new System.Drawing.Size(91, 20);
            this.SpaceForTab.TabIndex = 8;
            this.SpaceForTab.Tag = "insert space for tab";
            this.SpaceForTab.Text = "插入空格";
            this.SpaceForTab.UseVisualStyleBackColor = true;
            // 
            // TabSize
            // 
            this.TabSize.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabSize.Location = new System.Drawing.Point(127, 30);
            this.TabSize.Name = "TabSize";
            this.TabSize.Size = new System.Drawing.Size(48, 26);
            this.TabSize.TabIndex = 9;
            this.TabSize.Text = "4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SpaceForTab);
            this.groupBox1.Controls.Add(this.TabSize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(211, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 104);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "制表符设置：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FontSize);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(211, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 62);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字体大小设置：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LanguageSet);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(211, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 67);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "区域设置：";
            // 
            // LanguageSet
            // 
            this.LanguageSet.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LanguageSet.FormattingEnabled = true;
            this.LanguageSet.Location = new System.Drawing.Point(127, 27);
            this.LanguageSet.Name = "LanguageSet";
            this.LanguageSet.Size = new System.Drawing.Size(140, 20);
            this.LanguageSet.TabIndex = 11;
            this.LanguageSet.SelectedIndexChanged += new System.EventHandler(this.LanguageSet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "语言";
            // 
            // FrmSystemOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 309);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.treeView_SystemConfiguration);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemOption";
            this.Text = "FrmSystemOption";
            this.Load += new System.EventHandler(this.FrmSystemOption_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_SystemConfiguration;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.ComboBox FontSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox SpaceForTab;
        private System.Windows.Forms.MaskedTextBox TabSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LanguageSet;

    }
}