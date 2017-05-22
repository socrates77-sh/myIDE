using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    partial class FrmAbout : Form
    {

        public FrmAbout()
        {
            InitializeComponent();

            const string OptionName = "CustomInfo.xml";

            string CurPath = Application.StartupPath; //获取当前EXE路径
            string xmlFileName = CurPath + "\\" + OptionName;

            this.Text = OperateIniFile.ReadIniData("Information", "SoftName", "WinScope IDE", xmlFileName);
            this.labelProductName.Text = OperateIniFile.ReadIniData("Information", "ProductName", "WinScope IDE", xmlFileName) ;
            this.labelVersion.Text = OperateIniFile.ReadIniData("Information", "Version", "V1.00 2013-03-26", xmlFileName);
            this.labelCopyright.Text = OperateIniFile.ReadIniData("Information", "Copyright", "shanghai dzmi", xmlFileName);
            this.labelCompanyName.Text = OperateIniFile.ReadIniData("Information", "CompanyName", "dzmi", xmlFileName);
            this.textBoxDescription.Text = OperateIniFile.ReadIniData("Information", "Description", "这是一个全新的IDE。", xmlFileName);

            ApplyLanguage();
            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyLanguage()
        {
            this.okButton.Text = ShowLanguage.Current.Ok;
        }
    }
}
