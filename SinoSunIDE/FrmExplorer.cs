using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using System.IO;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{

    //1.定义委托，指定返回类型和形参列表。与类定义一样，同在命名空间下  
    public delegate void MyDelegate(string str);
    
    public partial class FrmExplorer : DockContent
    {
        
        //定义事件Openfile_MouseDouble_Clik
        public event MyDelegate Openfile_MouseDouble;

        private TreeNode dragNode = null;

        // Temporary drop node for selection
        private TreeNode tempDropNode = null;

        // Timer for scrolling
        private Timer timer = new Timer();

        private string localFilePath=null;
        private string fileNameExt=null;

        public FrmExplorer()
        {
            
            InitializeComponent();
            this.treeView1.ExpandAll();
            treeView1.MouseClick += new MouseEventHandler(treeView1_MouseClick);
           // timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            string srt = frmMAIN.APPLICATION_PATH + "global.ini";
            XElement rootNode = XElement.Load(srt);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                            select myTarget;
            foreach(XElement xnode in myvalue)
            {
                srt = xnode.Attribute("Ppath").Value;
            }

            if (File.Exists(srt) ==true)
            { 
                ProjectTreeBuild(srt);
            }
            else
            {
                srt = frmMAIN.APPLICATION_PATH + "Sample\\aaaa\\aaaa.Proj";
                ProjectTreeBuild(srt);
            }
           
          
        }

        #region Explorer_Click
        void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            treeView1.ContextMenuStrip = null;
            TreeNode selectNode = treeView1.GetNodeAt(e.X, e.Y);
            if (selectNode.Level == 0)
            {
                Addchild_ToolStripMenuItem.Enabled = false;
                AddRootToolStripMenuItem.Enabled = true;
                treeView1.ContextMenuStrip = contextMenuStrip_1;//contextMenuStrip_1
                
            }
            else if (selectNode.Level == 1)
            {
                AddRootToolStripMenuItem.Enabled = false;
                Addchild_ToolStripMenuItem.Enabled = true;
                treeView1.ContextMenuStrip = contextMenuStrip_1;
            }
            else if (selectNode.Level == 2)
            {
                AddRootToolStripMenuItem.Enabled = false;
                Addchild_ToolStripMenuItem.Enabled =false;
                treeView1.ContextMenuStrip = contextMenuStrip_1;
            }
        }

        public void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode.Level==2) 
            {
                string str = (string)this.treeView1.SelectedNode.Tag;
                //调用事件，引发事件委托
                Openfile_MouseDouble(str);
               // frmMAIN.CreateTab(str);
                
            }
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                //this.treeView1.Nodes.Add("Explorer");
                return;
            }

            mySaveFileDialog();

            if (localFilePath!=null && fileNameExt!=null)
            {
                //在文件夹中建立项目 OUTPUT,DEBUG,SYSTEM文件件夹
                if (Directory.Exists(localFilePath) == false)
                {
                    Directory.CreateDirectory(localFilePath);
                }

                TreeNode node = CreateNewNode(fileNameExt);
                node.Tag = localFilePath;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                this.treeView1.SelectedNode.Nodes.Add(node);
            }
 
        }

        private TreeNode CreateNewNode(string nodeName)
        {
            TreeNode node = new TreeNode(nodeName);
            //node.Tag = this.txtContent.Text.Clone();
            return node;
        }

        private void btnAddChild_Click(object sender, EventArgs e) // add a new file to project
        {

            if (this.treeView1.SelectedNode == null)
            {
                //this.treeView1.Nodes.Add("Explorer");
                return;
            }

            mySaveFileDialog();
            if (localFilePath != null && fileNameExt != null)
            {
                //在文件夹中建立项目 OUTPUT,DEBUG,SYSTEM文件件夹
                if (File.Exists(localFilePath) == false)
                {
                    FileStream fs = File.Create(localFilePath);
                    fs.Close();
                }
                TreeNode node = CreateNewNode(fileNameExt);
                node.Tag = localFilePath;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                this.treeView1.SelectedNode.Nodes.Add(node);
                this.treeView1.SelectedNode.Expand();
            }
            
        }

        private void AddOldFileToolStripMenuItem_Click(object sender, EventArgs e) //add files to project
        {
            //myOpenFileDialog();
            String projPath = frmMAIN.GetProjectPath();

            projPath = projPath.Substring(0, projPath.LastIndexOf("\\"));

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Reset();
            openFileDialog.InitialDirectory = projPath;

            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int aa = 0; aa < openFileDialog.FileNames.Length; aa++)
                {
                    localFilePath = openFileDialog.FileNames[aa];

                    fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);

                    if (localFilePath != null && fileNameExt != null)
                    {
                        //在文件夹中建立项目 OUTPUT,DEBUG,SYSTEM文件件夹
                        if (File.Exists(localFilePath) == false)
                        {
                            FileStream fs = File.Create(localFilePath);
                            fs.Close();
                        }
                        TreeNode node = CreateNewNode(fileNameExt);
                        node.Tag = localFilePath;
                        node.ImageIndex = 2;
                        node.SelectedImageIndex = 2;
                        this.treeView1.SelectedNode.Nodes.Add(node);
                        this.treeView1.SelectedNode.Expand();
                    }

                }
            }
            else
            {
                localFilePath = null;
                fileNameExt = null;
            }


        }

        private void btnDelSel_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);
        }

        //string Path,string fileName
        public void ProjectTreeBuild(string str)
        {
            //string str = @"G:\Emulator\IDE\SinoSunIDE\SinoSunIDE\bin\Debug\Sample\aaaa.Proj";
            if (File.Exists(str) == false)
                return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str);

            String projPath = str.Substring(0, str.LastIndexOf(@"\")); //获取项目路径
            XmlNodeList xmlNodes = xmlDoc.SelectSingleNode("/Project/ProWindows").ChildNodes;///SouceCode
            // XmlNodeList xmlNodes = xmlDoc.DocumentElement.ChildNodes;

            this.treeView1.Nodes.Clear();
            XmlNode2TreeNode(xmlNodes, this.treeView1.Nodes, 0,projPath);
            this.treeView1.ImageList = this.imageList_Explorer;
            this.treeView1.ExpandAll();

        }

        private void XmlNode2TreeNode(XmlNodeList xmlNode, TreeNodeCollection treeNode, UInt16 ImagIndex,string str)
        {
            foreach (XmlNode var in xmlNode)
            {
                if (var.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                TreeNode newTreeNode = new TreeNode(var.Attributes["fName"].Value, ImagIndex, ImagIndex);
                if (var.Attributes.Count >= 2)
                {
                    newTreeNode.Tag =str + var.Attributes.Item(1).Value;
                }
                //newTreeNode.Text = var.Attributes["Title"].Value;
                //newTreeNode.ImageIndex = ImagIndex;

                if (var.HasChildNodes)
                {
                    if (var.Attributes.Count>=2)
                    {
                        newTreeNode.Tag = var.Attributes.Item(1).Value;
                    }
                    ImagIndex += 1;
                    XmlNode2TreeNode(var.ChildNodes, newTreeNode.Nodes, ImagIndex,str); //递归调用
                    ImagIndex -= 1;
                }
                treeNode.Add(newTreeNode);
                //this.treeView1.Nodes.Add(newTreeNode);
                //this.treeView1.Refresh();
            }
        }
        #endregion


        /// <summary>
        /// treeView Drog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#region treeView Drag

        private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            // Get drag node and select it
            this.dragNode = (TreeNode)e.Item;
            this.treeView1.SelectedNode = this.dragNode;

            // Reset image list used for drag image
            this.imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize = new Size(this.dragNode.Bounds.Size.Width + this.treeView1.Indent, this.dragNode.Bounds.Height);

            // Create new bitmap
            // This bitmap will contain the tree node image to be dragged
            Bitmap bmp = new Bitmap(this.dragNode.Bounds.Width + this.treeView1.Indent, this.dragNode.Bounds.Height);

            // Get graphics from bitmap
            Graphics gfx = Graphics.FromImage(bmp);

            // Draw node icon into the bitmap
            gfx.DrawImage(this.imageList_Explorer.Images[0], 0, 0);

            // Draw node label into bitmap
            gfx.DrawString(this.dragNode.Text,
                this.treeView1.Font,
                new SolidBrush(this.treeView1.ForeColor),
                (float)this.treeView1.Indent, 1.0f);

            // Add bitmap to imagelist
            this.imageListDrag.Images.Add(bmp);

            // Get mouse position in client coordinates
            Point p = this.treeView1.PointToClient(Control.MousePosition);

            // Compute delta between mouse position and node bounds
            int dx = p.X + this.treeView1.Indent - this.dragNode.Bounds.Left;
            int dy = p.Y - this.dragNode.Bounds.Top;

            // Begin dragging image
            if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
            {
                // Begin dragging
                this.treeView1.DoDragDrop(bmp, DragDropEffects.Move);
                // End dragging image
                DragHelper.ImageList_EndDrag();
            }

        }

        private void treeView1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Compute drag position and move image
            Point formP = this.PointToClient(new Point(e.X, e.Y));
            DragHelper.ImageList_DragMove(formP.X - this.treeView1.Left, formP.Y - this.treeView1.Top);

            // Get actual drop node
            TreeNode dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));
            if (dropNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;

            // if mouse is on a new node select it
            if (this.tempDropNode != dropNode)
            {
                DragHelper.ImageList_DragShowNolock(false);
                this.treeView1.SelectedNode = dropNode;
                DragHelper.ImageList_DragShowNolock(true);
                tempDropNode = dropNode;
            }

            // Avoid that drop node is child of drag node 
            TreeNode tmpNode = dropNode;
            while (tmpNode.Parent != null)
            {
                if (tmpNode.Parent == this.dragNode) e.Effect = DragDropEffects.None;
                tmpNode = tmpNode.Parent;
            }
        }

        private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Unlock updates
            DragHelper.ImageList_DragLeave(this.treeView1.Handle);

            // Get drop node
            TreeNode dropNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));

            // If drop node isn't equal to drag node, add drag node as child of drop node
            if (this.dragNode != dropNode)
            {
                // Remove drag node from parent
                if (this.dragNode.Parent == null)
                {
                    this.treeView1.Nodes.Remove(this.dragNode);
                }
                else
                {
                    this.dragNode.Parent.Nodes.Remove(this.dragNode);
                }

                // Add drag node to drop node
                dropNode.Nodes.Add(this.dragNode);
                dropNode.ExpandAll();

                // Set drag node to null
                this.dragNode = null;

                // Disable scroll timer
                this.timer.Enabled = false;
            }
        }

        private void treeView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            DragHelper.ImageList_DragEnter(this.treeView1.Handle, e.X - this.treeView1.Left,
                e.Y - this.treeView1.Top);

            // Enable timer for scrolling dragged item
            this.timer.Enabled = true;
        }

        private void treeView1_DragLeave(object sender, System.EventArgs e)
        {
            DragHelper.ImageList_DragLeave(this.treeView1.Handle);

            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
        }

        private void treeView1_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                e.UseDefaultCursors = false;
                this.treeView1.Cursor = Cursors.Default;
            }
            else e.UseDefaultCursors = true;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // get node at mouse position
            Point pt = PointToClient(Control.MousePosition);
            TreeNode node = this.treeView1.GetNodeAt(pt);

            if (node == null) return;

            // if mouse is near to the top, scroll up
            if (pt.Y < 30)
            {
                // set actual node to the upper one
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    // hide drag image
                    DragHelper.ImageList_DragShowNolock(false);
                    // scroll and refresh
                    node.EnsureVisible();
                    this.treeView1.Refresh();
                    // show drag image
                    DragHelper.ImageList_DragShowNolock(true);

                }
            }
            // if mouse is near to the bottom, scroll down
            else if (pt.Y > this.treeView1.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.treeView1.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

#endregion

  


        private void mySaveFileDialog()
        {
            

            String projPath=frmMAIN.GetProjectPath();

            projPath = projPath.Substring(0, projPath.LastIndexOf("\\"));
            
            SaveFileDialog saveFileDialog=new SaveFileDialog();

            //fill file type
            saveFileDialog.Reset();
            saveFileDialog.InitialDirectory=projPath;
            saveFileDialog.Filter="txt files(*.txt)|*.txt|asm files(*.asm)|*.asm|C files(*.c)|*.c|All files(*.*)|*.*";
            saveFileDialog.FilterIndex=2;
          

            //save the old directory
            saveFileDialog.RestoreDirectory=true;

            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                localFilePath=saveFileDialog.FileName.ToString();

                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);

               
            }
            else
            {
                localFilePath = null;
                fileNameExt = null;
            }
            
        }

        private void myOpenFileDialog()
        {
            String projPath = frmMAIN.GetProjectPath();

            projPath = projPath.Substring(0, projPath.LastIndexOf("\\"));

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Reset();
            openFileDialog.InitialDirectory = projPath;

            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                localFilePath = openFileDialog.FileName.ToString();

                int  aa =openFileDialog.SafeFileNames.Length ;
                
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            else
            {
                localFilePath = null;
                fileNameExt = null;
            }
        }

        public void ApplyLanguage()
        {
            this.ConfigToolStripMenuItem.Text = ShowLanguage.Current.Configuration;
            this.CompileToolStripMenuItem.Text = ShowLanguage.Current.Compile;
            this.RegeneratedToolStripMenuItem.Text = ShowLanguage.Current.Regenerated;
            this.AddRootToolStripMenuItem.Text = ShowLanguage.Current.NewProject;
            this.Addchild_ToolStripMenuItem.Text = ShowLanguage.Current.NewFile;
            this.AddafileToolStripMenuItem.Text = ShowLanguage.Current.Addafile;
            this.btnDelSelToolStripMenuItem.Text = ShowLanguage.Current.DelSel;
        }

    }
}
