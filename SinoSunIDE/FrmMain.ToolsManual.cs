using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using FarsiLibrary.Win;
using System.IO.Ports;
using System.Timers;
using WeifenLuo.WinFormsUI.Docking;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using CmdSNLink;
using COD;

namespace SinoSunIDE
{
    partial class frmMAIN
    {

        private void processS19_Click(object sender, EventArgs e)
        {
            FrmProcessS19 frmProcessS19 = new FrmProcessS19();
            frmProcessS19.Show();
        }


        private void COMSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCOMSeting frmCOMSeting = new FrmCOMSeting();
            frmCOMSeting.Show();
        }

    }


}