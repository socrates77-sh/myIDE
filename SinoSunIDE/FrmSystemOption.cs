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
using PropertyGridEx;
using System.IO;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmSystemOption : Form
    {
        public FrmSystemOption()
        {
            InitializeComponent();

            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.groupBox2.Text = ShowLanguage.Current.FontSize;
            this.groupBox1.Text = ShowLanguage.Current.TabSet;
            this.label3.Text = ShowLanguage.Current.TabSize;
            this.SpaceForTab.Text = ShowLanguage.Current.SpaceForTab;
            this.groupBox3.Text = ShowLanguage.Current.PageRange;
            this.label1.Text = ShowLanguage.Current.Language;
            this.btOk.Text = ShowLanguage.Current.Ok;
            this.btCancel.Text = ShowLanguage.Current.Cancel;
            this.treeView_SystemConfiguration.Nodes[0].Text = ShowLanguage.Current.Editor;
            this.treeView_SystemConfiguration.Nodes[0].Nodes[0].Text = ShowLanguage.Current.FontSet;
            this.treeView_SystemConfiguration.Nodes[0].Nodes[1].Text = ShowLanguage.Current.TabSetEditor;
            this.treeView_SystemConfiguration.Nodes[0].Nodes[2].Text = ShowLanguage.Current.PageRangeEditor;
        }

        private void FrmSystemOption_Load(object sender, EventArgs e)
        {
            //font
            string path = frmMAIN.APPLICATION_PATH + "global.ini";
            string font_size=null;// = "12";
            string languageSel = null;

            XElement rootNode = XElement.Load(path);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectFont")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                font_size = xnode.Attribute("Size").Value;
                if (font_size=="")
                {
                    font_size = "12";
                    xnode.Attribute("Size").Value = font_size;
                }

                FontSize.Text = font_size;
            }

            myvalue = from myTarget in rootNode.Descendants("ProjectTab")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                font_size = xnode.Attribute("TSize").Value;
                if (font_size=="")
                {
                    font_size = "8";
                    xnode.Attribute("TSize").Value = font_size;
                }

                TabSize.Text = font_size;
            }

            myvalue = from myTarget in rootNode.Descendants("ProjectLanguage")
                      select myTarget;
            foreach (XElement xnode in myvalue)
            {
                languageSel = xnode.Attribute("Value").Value;
            }

            SpaceForTab.Checked = true;
            SpaceForTab.Enabled = false;

            rootNode.Save(path);

            int curindex = 0;
            int.TryParse(languageSel, out curindex);
            this.LanguageSet.DataSource = ShowLanguage.GetLanguages();
            int count = this.LanguageSet.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (languageSel == "" || curindex >= count)
                {
                    IShowLanguage language = (IShowLanguage)this.LanguageSet.Items[i];
                    if(language.IsDefault)
                    {
                        this.LanguageSet.SelectedIndex = i;
                        break;
                    }
                }
                else if(i == curindex)
                {
                    this.LanguageSet.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            string path = frmMAIN.APPLICATION_PATH + "global.ini";
            //string font_size = null;// = "12"; 

            XElement rootNode = XElement.Load(path);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectFont")
                                            select myTarget;


            foreach (XElement xnode in myvalue)
            {
                if (FontSize.Text != "")
                {
                    xnode.Attribute("Size").Value = FontSize.Text;
                }
                else
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText29);

            }

            myvalue = from myTarget in rootNode.Descendants("ProjectTab")
                                            select myTarget;


            foreach (XElement xnode in myvalue)
            {
                if (TabSize.Text != "")
                {
                    xnode.Attribute("TSize").Value = TabSize.Text;
                }
                else
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText29);
            }

            myvalue = from myTarget in rootNode.Descendants("ProjectLanguage")
                      select myTarget;


            foreach (XElement xnode in myvalue)
            {
                if (LanguageSet.SelectedIndex >= 0)
                {
                    xnode.Attribute("Value").Value = LanguageSet.SelectedIndex.ToString();
                }
                else
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText30);
            }

            rootNode.Save(path);

            //this.Close();
        }

        private void LanguageSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LanguageSet.SelectedItem != null)
            {
                ShowLanguage.Current = (IShowLanguage)this.LanguageSet.SelectedItem;
            }
        }

    }
}
