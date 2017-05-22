using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmProjOption : Form
    {

        public FrmProjOption()
        {
            InitializeComponent();

            ApplyLanguage();
        }

        public void ApplyLanguage()
        {
            this.Text = ShowLanguage.Current.ProjOption;
            this.btOK.Text = ShowLanguage.Current.Ok;
            this.btCancel.Text = ShowLanguage.Current.Cancel;
        }
    }
}
