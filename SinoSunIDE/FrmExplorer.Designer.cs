namespace SinoSunIDE
{
    partial class FrmExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
       // private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExplorer));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList_Explorer = new System.Windows.Forms.ImageList(this.components);
            this.imageListDrag = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip_1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegeneratedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Addchild_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelSelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.ImageList = this.imageList_Explorer;
            this.treeView1.LabelEdit = true;
            this.treeView1.Name = "treeView1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeView1.Nodes")))});
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
            this.treeView1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.treeView1_GiveFeedback);
            this.treeView1.DragLeave += new System.EventHandler(this.treeView1_DragLeave);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
            // 
            // imageList_Explorer
            // 
            this.imageList_Explorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Explorer.ImageStream")));
            this.imageList_Explorer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Explorer.Images.SetKeyName(0, "(01,38).png");
            this.imageList_Explorer.Images.SetKeyName(1, "(02,29).png");
            this.imageList_Explorer.Images.SetKeyName(2, "File.ico");
            this.imageList_Explorer.Images.SetKeyName(3, "(01,34).png");
            this.imageList_Explorer.Images.SetKeyName(4, "(01,22).png");
            // 
            // imageListDrag
            // 
            this.imageListDrag.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListDrag, "imageListDrag");
            this.imageListDrag.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // contextMenuStrip_1
            // 
            this.contextMenuStrip_1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem,
            this.CompileToolStripMenuItem,
            this.RegeneratedToolStripMenuItem,
            this.AddRootToolStripMenuItem,
            this.Addchild_ToolStripMenuItem,
            this.AddafileToolStripMenuItem,
            this.btnDelSelToolStripMenuItem});
            this.contextMenuStrip_1.Name = "contextMenuStrip_1";
            resources.ApplyResources(this.contextMenuStrip_1, "contextMenuStrip_1");
            // 
            // ConfigToolStripMenuItem
            // 
            resources.ApplyResources(this.ConfigToolStripMenuItem, "ConfigToolStripMenuItem");
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            // 
            // CompileToolStripMenuItem
            // 
            resources.ApplyResources(this.CompileToolStripMenuItem, "CompileToolStripMenuItem");
            this.CompileToolStripMenuItem.Name = "CompileToolStripMenuItem";
            // 
            // RegeneratedToolStripMenuItem
            // 
            resources.ApplyResources(this.RegeneratedToolStripMenuItem, "RegeneratedToolStripMenuItem");
            this.RegeneratedToolStripMenuItem.Name = "RegeneratedToolStripMenuItem";
            // 
            // AddRootToolStripMenuItem
            // 
            this.AddRootToolStripMenuItem.Image = global::SinoSunIDE.Properties.Resources._02_29_;
            this.AddRootToolStripMenuItem.Name = "AddRootToolStripMenuItem";
            resources.ApplyResources(this.AddRootToolStripMenuItem, "AddRootToolStripMenuItem");
            this.AddRootToolStripMenuItem.Click += new System.EventHandler(this.btnAddRoot_Click);
            // 
            // Addchild_ToolStripMenuItem
            // 
            this.Addchild_ToolStripMenuItem.Image = global::SinoSunIDE.Properties.Resources._16_00_;
            this.Addchild_ToolStripMenuItem.Name = "Addchild_ToolStripMenuItem";
            resources.ApplyResources(this.Addchild_ToolStripMenuItem, "Addchild_ToolStripMenuItem");
            this.Addchild_ToolStripMenuItem.Click += new System.EventHandler(this.btnAddChild_Click);
            // 
            // AddafileToolStripMenuItem
            // 
            this.AddafileToolStripMenuItem.Image = global::SinoSunIDE.Properties.Resources._12_43_;
            this.AddafileToolStripMenuItem.Name = "AddafileToolStripMenuItem";
            resources.ApplyResources(this.AddafileToolStripMenuItem, "AddafileToolStripMenuItem");
            this.AddafileToolStripMenuItem.Click += new System.EventHandler(this.AddOldFileToolStripMenuItem_Click);
            // 
            // btnDelSelToolStripMenuItem
            // 
            this.btnDelSelToolStripMenuItem.Image = global::SinoSunIDE.Properties.Resources._00_04_;
            this.btnDelSelToolStripMenuItem.Name = "btnDelSelToolStripMenuItem";
            resources.ApplyResources(this.btnDelSelToolStripMenuItem, "btnDelSelToolStripMenuItem");
            this.btnDelSelToolStripMenuItem.Click += new System.EventHandler(this.btnDelSel_Click);
            // 
            // FrmExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView1);
            this.Name = "FrmExplorer";
            this.TabText = "Solution Explorer";
            this.contextMenuStrip_1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.ImageList imageList_Explorer;
        private System.Windows.Forms.ImageList imageListDrag;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_1;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CompileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegeneratedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Addchild_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnDelSelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddafileToolStripMenuItem;
    }
}