using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SinoSunIDE
{
    public partial class FrmStopMess : Form
    {
        public FrmStopMess()
        {
            InitializeComponent();
        }

        private void FrmStopMess_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMAIN.Stop_stat = 0;
        }
    }
}
