namespace SinoSunIDE
{
    partial class FrmROM
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
            this.ROMDataDisplay = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ROMMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ROMMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ROMDataDisplay
            // 
            this.ROMDataDisplay.AllowDrop = true;
            this.ROMDataDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ROMDataDisplay.AutoScrollMinSize = new System.Drawing.Size(99, 14);
            this.ROMDataDisplay.BackBrush = null;
            this.ROMDataDisplay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ROMDataDisplay.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ROMDataDisplay.Location = new System.Drawing.Point(18, 12);
            this.ROMDataDisplay.Name = "ROMDataDisplay";
            this.ROMDataDisplay.Paddings = new System.Windows.Forms.Padding(0);
            this.ROMDataDisplay.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ROMDataDisplay.Size = new System.Drawing.Size(674, 388);
            this.ROMDataDisplay.TabIndex = 0;
            this.ROMDataDisplay.Text = "data.....";
            this.ROMDataDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseRight);
            // 
            // ROMMenu
            // 
            this.ROMMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.PastToolStripMenuItem,
            this.DelectToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.ROMMenu.Name = "ROMMenu";
            this.ROMMenu.Size = new System.Drawing.Size(120, 92);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.copy);
            // 
            // PastToolStripMenuItem
            // 
            this.PastToolStripMenuItem.Name = "PastToolStripMenuItem";
            this.PastToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.PastToolStripMenuItem.Text = "粘贴";
            this.PastToolStripMenuItem.Click += new System.EventHandler(this.paste);
            // 
            // DelectToolStripMenuItem
            // 
            this.DelectToolStripMenuItem.Name = "DelectToolStripMenuItem";
            this.DelectToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.DelectToolStripMenuItem.Text = "删除";
            this.DelectToolStripMenuItem.Click += new System.EventHandler(this.cut);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.selectAllToolStripMenuItem.Text = "SelectAll";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAll);
            // 
            // FrmROM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 412);
            this.Controls.Add(this.ROMDataDisplay);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmROM";
            this.Text = "FrmROM";
            this.ROMMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FastColoredTextBoxNS.FastColoredTextBox ROMDataDisplay;
        private System.Windows.Forms.ContextMenuStrip ROMMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}