using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial  class FrmEditor : DockContent
    {
        public FrmEditor()
        {
            InitializeComponent();
            // TabText = @"GuaTJ.s";
            //ftext.Text.to
            propertyGrid.SelectedObject = ftext;
           
        }

        private void FrmEditor_Load(object sender, EventArgs e)
        {

        }

        public void ApplyLanguage()
        {
            
        }
    }
}
