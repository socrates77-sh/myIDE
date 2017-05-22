using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmROM : DockContent
    //public partial class FrmROM : Form
    {
        public FrmROM()
        {
            InitializeComponent();

        }

        private void MouseRight(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                ROMMenu.Show(ROMDataDisplay,new Point(e.X,e.Y));
            }
        }

        private void copy(object sender, EventArgs e)
        {
            if (ROMDataDisplay.Text == null)
            {
                return;
            }
            ROMDataDisplay.Copy();
        }

        private void paste(object sender, EventArgs e)
        {
            if (ROMDataDisplay.Text == null)
            {
                return;
            }
            ROMDataDisplay.Paste();
        }

        private void cut(object sender, EventArgs e)
        {
            if (ROMDataDisplay.Text == null)
            {
                return;
            }
            ROMDataDisplay.Cut();
        }

        private void selectAll(object sender, EventArgs e)
        {
            if (ROMDataDisplay == null)
            {
                return;
            }
            ROMDataDisplay.Selection.SelectAll();
        }

        public void ApplyLanguage()
        {
            this.CopyToolStripMenuItem.Text = ShowLanguage.Current.Copy;
            this.PastToolStripMenuItem.Text = ShowLanguage.Current.Paste;
            this.DelectToolStripMenuItem.Text = ShowLanguage.Current.Delete;
            this.selectAllToolStripMenuItem.Text = ShowLanguage.Current.SelectAll;
        }

        
    }
}
