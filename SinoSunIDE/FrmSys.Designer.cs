namespace SinoSunIDE
{
    partial class FrmSys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSys));
            this.propertyGridEx_sys = new PropertyGridEx.PropertyGridEx();
            this.SuspendLayout();
            // 
            // propertyGridEx_sys
            // 
            // 
            // 
            // 
            this.propertyGridEx_sys.DocCommentDescription.AutoEllipsis = true;
            this.propertyGridEx_sys.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridEx_sys.DocCommentDescription.Location = new System.Drawing.Point(3, 19);
            this.propertyGridEx_sys.DocCommentDescription.Name = "";
            this.propertyGridEx_sys.DocCommentDescription.Size = new System.Drawing.Size(278, 36);
            this.propertyGridEx_sys.DocCommentDescription.TabIndex = 1;
            this.propertyGridEx_sys.DocCommentImage = null;
            // 
            // 
            // 
            this.propertyGridEx_sys.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridEx_sys.DocCommentTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.propertyGridEx_sys.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.propertyGridEx_sys.DocCommentTitle.Name = "";
            this.propertyGridEx_sys.DocCommentTitle.Size = new System.Drawing.Size(278, 16);
            this.propertyGridEx_sys.DocCommentTitle.TabIndex = 0;
            this.propertyGridEx_sys.DocCommentTitle.UseMnemonic = false;
            this.propertyGridEx_sys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridEx_sys.Location = new System.Drawing.Point(0, 0);
            this.propertyGridEx_sys.Name = "propertyGridEx_sys";
            this.propertyGridEx_sys.Size = new System.Drawing.Size(284, 300);
            this.propertyGridEx_sys.TabIndex = 0;
            // 
            // 
            // 
            this.propertyGridEx_sys.ToolStrip.AccessibleName = "工具栏";
            this.propertyGridEx_sys.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.propertyGridEx_sys.ToolStrip.AllowMerge = false;
            this.propertyGridEx_sys.ToolStrip.AutoSize = false;
            this.propertyGridEx_sys.ToolStrip.CanOverflow = false;
            this.propertyGridEx_sys.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.propertyGridEx_sys.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.propertyGridEx_sys.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.propertyGridEx_sys.ToolStrip.Name = "";
            this.propertyGridEx_sys.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.propertyGridEx_sys.ToolStrip.Size = new System.Drawing.Size(284, 25);
            this.propertyGridEx_sys.ToolStrip.TabIndex = 1;
            this.propertyGridEx_sys.ToolStrip.TabStop = true;
            this.propertyGridEx_sys.ToolStrip.Text = "PropertyGridToolBar";
            // 
            // FrmSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 300);
            this.Controls.Add(this.propertyGridEx_sys);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSys";
            this.Text = "特殊功能寄存器";
            this.ResumeLayout(false);

        }

        #endregion

        public PropertyGridEx.PropertyGridEx propertyGridEx_sys;
    }
}