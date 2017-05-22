namespace SinoSunIDE
{
    partial class FrmMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessage));
            this.fastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ROMMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ROMMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastColoredTextBox
            // 
            this.fastColoredTextBox.AllowDrop = true;
            this.fastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(29, 18);
            this.fastColoredTextBox.BackBrush = null;
            this.fastColoredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastColoredTextBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.fastColoredTextBox.Language = FastColoredTextBoxNS.Language.ASM;
            this.fastColoredTextBox.LeftBracket = '(';
            this.fastColoredTextBox.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox.Name = "fastColoredTextBox";
            this.fastColoredTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox.RightBracket = ')';
            this.fastColoredTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox.Size = new System.Drawing.Size(400, 191);
            this.fastColoredTextBox.TabIndex = 0;
            this.fastColoredTextBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseDouble_Click);
            this.fastColoredTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseRight);
            // 
            // ROMMenu
            // 
            this.ROMMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.PastToolStripMenuItem,
            this.DelectToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.ROMMenu.Name = "ROMMenu";
            this.ROMMenu.Size = new System.Drawing.Size(125, 92);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.copy);
            // 
            // PastToolStripMenuItem
            // 
            this.PastToolStripMenuItem.Name = "PastToolStripMenuItem";
            this.PastToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.PastToolStripMenuItem.Text = "粘贴";
            this.PastToolStripMenuItem.Click += new System.EventHandler(this.paste);
            // 
            // DelectToolStripMenuItem
            // 
            this.DelectToolStripMenuItem.Name = "DelectToolStripMenuItem";
            this.DelectToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DelectToolStripMenuItem.Text = "删除";
            this.DelectToolStripMenuItem.Click += new System.EventHandler(this.cut);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.selectAllToolStripMenuItem.Text = "SelectAll";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAll);
            // 
            // FrmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 191);
            this.Controls.Add(this.fastColoredTextBox);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMessage";
            this.TabText = "输出信息";
            this.Text = "输出信息";
            this.ROMMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox;
        private System.Windows.Forms.ContextMenuStrip ROMMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;


    }
}