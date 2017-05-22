using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Text.RegularExpressions;
using SinoSunIDE.Languages;


namespace SinoSunIDE
{
    //1.定义委托，指定返回类型和形参列表。与类定义一样，同在命名空间下  
    public delegate void FindErroLine(int iLine);

    public partial class FrmMessage : DockContent
    {

        //定义事件Openfile_MouseDouble_Clik
        public event FindErroLine gotoLine_Click;

        public FrmMessage()
        {
            InitializeComponent();
        }

        private void MouseDouble_Click(object sender, MouseEventArgs e)
        {
            int currentLine = fastColoredTextBox.Selection.Start.iLine;
            string strEER = fastColoredTextBox.GetLineText(currentLine);
            string temp = null;

            if (frmMAIN.MCU_TYPE=="RISC")
            {
                string pattern = @":Error";
                //regex
                Match matches = Regex.Match(strEER, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
                    | RegexOptions.RightToLeft);

                int strEndIndex = matches.Index;

                if (strEndIndex==0)
                {
                    return;
                }

                pattern = @"asm:";

                matches = Regex.Match(strEER, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
                    | RegexOptions.RightToLeft);
                string tempmatches = matches.Value;
                if(tempmatches != "")
                {
                    int strFirstIndex = matches.Index + 4;
                    int strLength = strEndIndex - strFirstIndex;

                    temp = strEER.Substring(strFirstIndex, strLength);
                }
                else
                {
                    pattern = @"lst:";

                    matches = Regex.Match(strEER, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
                        | RegexOptions.RightToLeft);
                    tempmatches = matches.Value;
                    if (tempmatches != "")
                    {
                        int strFirstIndex = matches.Index + 4;
                        int strLength = strEndIndex - strFirstIndex;

                        temp = strEER.Substring(strFirstIndex, strLength);
                    }
                }

                gotoLine_Click(Convert.ToInt32(temp));
            }
            //#error cp6805 F:\AAAAA\ABC.c:17(12) missing ;
            else if(frmMAIN.MCU_TYPE=="CHC05")
            {
                string pattern = @"\.(c|h)\:[0-9]+(\(|\s)";
                //regex
                Match matches = Regex.Match(strEER, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
                    | RegexOptions.RightToLeft);
                if (matches.Value!="")
                {
                    temp = matches.Value.Substring(3, matches.Value.Length - 4);
                    gotoLine_Click(Convert.ToInt32(temp));
                }
          
            }
            else if (frmMAIN.MCU_TYPE=="CRISC")// add by wj
            {
                string pattern = @"\.(c|h)\:[0-9]+\:";
                //regex
                Match matches = Regex.Match(strEER, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
                    | RegexOptions.RightToLeft);
                if (matches.Value != "")
                {
                    temp = matches.Value.Substring(3, matches.Value.Length - 4);
                    gotoLine_Click(Convert.ToInt32(temp));
                }
            }
            else
            {
                if (strEER.Length < 9)
                {
                    return;
                }
                 temp = strEER.Substring(0, 10);


                string ErrorLine = "";
                if (temp == (@"error:line"))
                {
                    ErrorLine = strEER.Replace("error:line", " ");

                    //调用事件，引发事件委托
                    gotoLine_Click(Convert.ToInt32(ErrorLine));
                }
                else
                {
                    MessageBox.Show("Mouse Double Click the \"error:line xxx\"\n"
                       + " will jump to the Error line");
                }
            }
        }

        private void MouseRight(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ROMMenu.Show(fastColoredTextBox, new Point(e.X, e.Y));
            }
        }

        private void copy(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Text == null)
            {
                return;
            }
            fastColoredTextBox.Copy();
        }

        private void paste(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Text == null)
            {
                return;
            }
            fastColoredTextBox.Paste();
        }

        private void cut(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Text == null)
            {
                return;
            }
            fastColoredTextBox.Cut();
        }

        private void selectAll(object sender, EventArgs e)
        {
            if (fastColoredTextBox == null)
            {
                return;
            }
            fastColoredTextBox.Selection.SelectAll();
        }

        public void ApplyLanguage()
        {
            this.TabText = ShowLanguage.Current.MessagesOutput;
            this.CopyToolStripMenuItem.Text = ShowLanguage.Current.Copy;
            this.PastToolStripMenuItem.Text = ShowLanguage.Current.Paste;
            this.DelectToolStripMenuItem.Text = ShowLanguage.Current.Delete;
            this.selectAllToolStripMenuItem.Text = ShowLanguage.Current.SelectAll;
        }
    }
}
