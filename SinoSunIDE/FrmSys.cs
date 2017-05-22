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
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmSys : DockContent
    {

        public FrmSys()
        {
            InitializeComponent();
            string mcuID = "0x0301";
            Loadxml(mcuID);
        }
        public void Loadxml(string mcuID)
        {


            string xmlPath = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            // string xxmlPath = "G:\\Emulator\\IDE\\SinoSunIDE\\XmlGenerator\\XmlGenerator\\XmlGenerator\\bin\\Debug\\OTP.cfg";
            //生成XML文件
            // xml_great(xxmlPath);
            

            propertyGridEx_sys.ShowCustomProperties = true;
            propertyGridEx_sys.PropertySort = PropertySort.NoSort;
            propertyGridEx_sys.Item.Clear();

            try
            {
                XElement rootNode = XElement.Load(xmlPath);
                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                                where (string)myTarget.Attribute("id").Value == mcuID
                                                select myTarget;

                IEnumerable<XElement> chipcfg = from srootNode in myvalue.Descendants("sys") select srootNode;
                foreach (XElement xnode in chipcfg)
                {
                    string sysName=xnode.Attribute("name").Value;
                    string sysDescribe=xnode.Attribute("describe").Value;
                    string ab = xnode.Attribute("value").Value;

                    propertyGridEx_sys.Item.Add(sysName, ab, false, "系统特殊寄存器", sysDescribe, true);
                    //dataGridView_reg.Rows[temp].Cells[0].Value = xnode.Attribute("name").Value;
                    //dataGridView_reg.Rows[temp].Cells[1].Value = xnode.Attribute("addr").Value;
                }
                propertyGridEx_sys.Refresh();
                // MessageBox.Show(romfaddr + "  " + romeaddr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void ApplyLanguage()
        {
            this.Text = ShowLanguage.Current.SpecialFunctionRegister;
            this.propertyGridEx_sys.ToolStrip.Items[0].ToolTipText = ShowLanguage.Current.Sorting;
            this.propertyGridEx_sys.ToolStrip.Items[1].ToolTipText = ShowLanguage.Current.Alphabetical;
            this.propertyGridEx_sys.ToolStrip.Items[4].ToolTipText = ShowLanguage.Current.PropertyPage;
        }
    }
}
