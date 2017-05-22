using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarsiLibrary.Win;
using WeifenLuo.WinFormsUI.Docking;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmFile : DockContent
    {

        public FrmFile()
        {
            InitializeComponent();
        }

        public void ApplyLanguage()
        {
            this.Text = ShowLanguage.Current.ProgramEdit;
        }
        
    }
}
