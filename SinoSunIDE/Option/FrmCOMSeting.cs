using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace SinoSunIDE
{
    public partial class FrmCOMSeting : Form
    {
        public FrmCOMSeting()
        {
            InitializeComponent();
        }

        private void FrmCOMSeting_Load(object sender, EventArgs e)
        {
            string[] portsname = SerialPort.GetPortNames();

            int i = portsname.Length;

            for ( ; i > 0; i--)
            {
                COMNameList.Items.Add(portsname[i-1]);
            }
           

        }
    }
}
