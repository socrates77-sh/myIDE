using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class NewProject_From : Form
    {
        private string chipID = null;
        private string mcuType = null;
        private string OptionValue = null;
        private string OptionValue0 = null;
        private string OptionValue1 = null;
        private string OptionValue2 = null;
        private string OptionValue3 = null;
        private string OptionValue4 = null;
        private string OptionValue5 = null;
        private string OptionValue6 = null;
        private string OptionValue7 = null;

        public NewProject_From()
        {
            InitializeComponent();

            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            this.Text = ShowLanguage.Current.Wizard;
            this.MCUSelect_label.Text = ShowLanguage.Current.ChipModel;
            this.labelProjectName.Text = ShowLanguage.Current.ProjectName;
            this.labelProjectLoadPath.Text = ShowLanguage.Current.OutputPath;
            this.button1.Text = ShowLanguage.Current.Browse;
            this.checkBoxCreateDirectory.Text = ShowLanguage.Current.CreateDirectory;
            this.rbASM.Text = ShowLanguage.Current.ASM;
            this.rbC.Text = ShowLanguage.Current.C;
            this.labelCompiler.Text = ShowLanguage.Current.SelectCompiler;
            this.bt_backStep.Text = ShowLanguage.Current.PreStep;
            this.btNextStep.Text = ShowLanguage.Current.NextStep;
            this.btCancle.Text = ShowLanguage.Current.Cancel;
        }

        private void NewProject_From_Load(object sender, EventArgs e)
        {
            ProjectAbstractepanel.Visible = false;
            panel_step1.Visible = true;

            labelCompiler.Enabled = false;
            CompilerSelect.Enabled = false;
            CompilerSelect.Text = "\\tools\\gpasm.exe";
            rbASM.Checked = true;
            rbC.Checked = false;
            rbC.Enabled = false;

            //MCUSelect_comboBox.Items[0] = "mc20p24b";
            string xmlPath = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            XElement rootNode = XElement.Load(xmlPath);
            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                            //where (string)myTarget.Attribute("id").Value == mcuID
                                            select myTarget;
            //int temp = 0;
            foreach (XElement xnode in myvalue)
            {
                MCUSelect_comboBox.Items.Add(xnode.Attribute("name").Value);
                chipID = xnode.Attribute("id").Value;
                //temp = temp + 1;
            }
            MCUSelect_comboBox.SelectedIndex = 0;

            //create directory for project checked
            checkBoxCreateDirectory.Checked = true;

            xmlPath = frmMAIN.GetProjectPath();
            xmlPath = xmlPath.Substring(0, xmlPath.LastIndexOf("\\"));
            xmlPath = xmlPath.Substring(0, xmlPath.LastIndexOf("\\"));
            ProjectPath_comboBox.Text = xmlPath;
        }

        private void MCUSelectItemsChange(object sender, EventArgs e)
        {
            string xmlPath = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            XElement rootNode = XElement.Load(xmlPath);
            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                            where (string)myTarget.Attribute("name").Value == MCUSelect_comboBox.Text
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                chipID = xnode.Attribute("id").Value;
                mcuType = xnode.Attribute("type").Value;
            }

            if (chipID == "0x8132")
            {
                MessageBox.Show("仿真器不监控x7与xF两列的ram数据，固定显示为00，详见IDE说明书附录五!");
            }
            //if (chipID=="0x0301" || chipID=="0x0314"||chipID=="0x0311"||chipID=="0x3221") //add mcu
            if (mcuType == "RISC14" || mcuType == "RISC16")
            {
                CompilerSelect.Text = "gpasm.exe";
                mcuType = "RISC";
                rbASM.Checked = true;
                rbC.Checked = false;
                rbC.Enabled = true;

            }
            else
            {
                CompilerSelect.Text = "Assembly.exe/C ";
                mcuType = "AHC05";
                rbASM.Checked = true;
                rbC.Checked = false;
                rbC.Enabled = true;
            }
        }

        private void bt_backStep_Click(object sender, EventArgs e)
        {

        }

        private void btNextStep_Click(object sender, EventArgs e)
        {
            DialogResult OpitonResult = DialogResult.Cancel;


            string xmlPath = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            XElement rootNode = XElement.Load(xmlPath);
            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                            where (string)myTarget.Attribute("name").Value == MCUSelect_comboBox.Text
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                chipID = xnode.Attribute("id").Value;
            }

            if (btNextStep.Tag.ToString() == "1")
            {
                btNextStep.Text = ShowLanguage.Current.SecondStep;
                btNextStep.Tag = "2";
                //FrmMC20P24B frmMC20P24B = new FrmMC20P24B();
                //frmMC20P24B.Show();
                FrmPC frmPC = new FrmPC();
                frmPC.mcuID = chipID;
                frmPC.loadState = 1;
                OpitonResult = frmPC.ShowDialog();
                if (OpitonResult == DialogResult.OK)
                {
                    if (!frmMAIN.OPTIONNullFlag)
                        OptionValue = frmMAIN.OPTION.ToString("X");
                    else
                        OptionValue = "";
                    if (!frmMAIN.OPTION0NullFlag)
                        OptionValue0 = frmMAIN.OPTION0.ToString("X");
                    else
                        OptionValue0 = "";
                    if (!frmMAIN.OPTION1NullFlag)
                        OptionValue1 = frmMAIN.OPTION1.ToString("X");
                    else
                        OptionValue1 = "";
                    if (!frmMAIN.OPTION2NullFlag)
                        OptionValue2 = frmMAIN.OPTION2.ToString("X");
                    else
                        OptionValue2 = "";
                    if (!frmMAIN.OPTION3NullFlag)
                        OptionValue3 = frmMAIN.OPTION3.ToString("X");
                    else
                        OptionValue3 = "";
                    if (!frmMAIN.OPTION4NullFlag)
                        OptionValue4 = frmMAIN.OPTION4.ToString("X");
                    else
                        OptionValue4 = "";
                    if (!frmMAIN.OPTION5NullFlag)
                        OptionValue5 = frmMAIN.OPTION5.ToString("X");
                    else
                        OptionValue5 = "";
                    if (!frmMAIN.OPTION6NullFlag)
                        OptionValue6 = frmMAIN.OPTION6.ToString("X");
                    else
                        OptionValue6 = "";
                    if (!frmMAIN.OPTION7NullFlag)
                        OptionValue7 = frmMAIN.OPTION7.ToString("X");
                    else
                        OptionValue7 = "";
                }

            }
            else if (btNextStep.Tag.ToString() == "2")
            {
                btNextStep.Text = ShowLanguage.Current.Finish;
                btNextStep.Tag = "3";
                panel_step1.Visible = false;
                ProjectAbstractepanel.Visible = true;
                ProjectAbstractRichTextBox.AppendText("ProjectName : ");
                ProjectAbstractRichTextBox.AppendText(ProjectName_textBox.Text + "\n");
                ProjectAbstractRichTextBox.AppendText("ProjectPath : ");
                ProjectAbstractRichTextBox.AppendText(ProjectPath_comboBox.Text + "\n");
                ProjectAbstractRichTextBox.AppendText("\n");
                ProjectAbstractRichTextBox.AppendText("Compiler:" + CompilerSelect.Text);
                ProjectAbstractRichTextBox.AppendText("\n");
                if (rbASM.Checked == true)
                {
                    ProjectAbstractRichTextBox.AppendText("Language:" + "ASM");
                    ProjectAbstractRichTextBox.AppendText("\n");
                }
                else
                {
                    if (mcuType == "AHC05")
                        mcuType = "CHC05";
                    else
                        mcuType = "CRISC";
                    ProjectAbstractRichTextBox.AppendText("Language:" + "C");
                    ProjectAbstractRichTextBox.AppendText("\n");
                }
                ProjectAbstractRichTextBox.AppendText("\n");
                ProjectAbstractRichTextBox.AppendText("MCU Name : " + MCUSelect_comboBox.Text + "\n");
                ProjectAbstractRichTextBox.AppendText("Option Set : " + "\n");
                ProjectAbstractRichTextBox.AppendText("Value=" + OptionValue);

            }
            else if (btNextStep.Tag.ToString() == "3")
            {
                btNextStep.Text = ShowLanguage.Current.NextStep;
                btNextStep.Tag = "1";
                NewProjectFile(ProjectPath_comboBox.Text, ProjectName_textBox.Text);
                // string str = Mathfuntion.GetProjectPath();
                btNextStep.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
        }
        /// <summary>
        /// NewProjectFile 
        /// </summary>
        public void NewProjectFile(string ProjectPath, string ProjectName)
        {
            string str = ProjectPath + "\\" + ProjectName;

            if (Directory.Exists(str) == false)
            {
                Directory.CreateDirectory(str); //如果项目文件夹不存在，则新建一个
            }
            else
            {
                MessageBox.Show(ProjectName + ShowLanguage.Current.MessageBoxText19 + "\n" + ShowLanguage.Current.MessageBoxText20);
            }
            //在文件夹中建立项目 OUTPUT,DEBUG,SYSTEM文件件夹
            if (Directory.Exists(str + "\\Debug") == false)
            {
                Directory.CreateDirectory(str + "\\Debug");
            }
            //Output
            if (Directory.Exists(str + "\\Output") == false)
            {
                Directory.CreateDirectory(str + "\\Output");
            }
            //System
            if (Directory.Exists(str + "\\System") == false)
            {
                Directory.CreateDirectory(str + "\\System");
            }

            //创建project文件
            if (File.Exists(str + "\\" + ProjectName + ".Proj") == false)
            {
                //File.Create(str + ".Proj");
                //create xml file as proj
                //XmlTextWriter profile = new XmlTextWriter(str + ".Proj", null);
                string xmlPath = str + "\\" + ProjectName + ".Proj";
                string lang = null;
                string strFile = null;
                string strfileNameExt = null;
                if (rbASM.Checked == true)
                {
                    lang = "ASM";
                    strfileNameExt = ProjectName + ".asm";
                    strFile = str + "\\" + ProjectName + ".asm";
                    FileStream fs = File.Create(strFile);
                    fs.Close();
                }
                else
                {
                    lang = "C";
                    strfileNameExt = ProjectName + ".c";
                    strFile = str + "\\" + ProjectName + ".c";
                    FileStream fs = File.Create(strFile);
                    fs.Close();
                }


                try
                {//生成Project 文件
                    XDocument myXDoc = new XDocument(
                        new XElement("Project", new XAttribute("MCU", MCUSelect_comboBox.Text), new XAttribute("ID", chipID), new XAttribute("MCU_Type", mcuType),
                        /*new XAttribute("Author","Mike.Mo"),*/new XAttribute("Date", "2012-08-28"), new XAttribute("VS", "1.01"),
                            new XElement("ProWindows",
                                new XElement("Explorer", new XAttribute("fName", ProjectName_textBox.Text),
                                    new XElement("SourceCode", new XAttribute("fName", "SourceCode"), new XElement("asmFile", new XAttribute("fName", strfileNameExt), new XAttribute("fPath", @"\" + strfileNameExt))),              
                                    new XElement("Output", new XAttribute("fName", "Output")),
                                    new XElement("Debug", new XAttribute("fName", "Debug"))
                                    )
                                ),
                            new XElement("Option", new XAttribute("Value", OptionValue), new XAttribute("Value0", OptionValue0), new XAttribute("Value1", OptionValue1),
                                new XAttribute("Value2", OptionValue2), new XAttribute("Value3", OptionValue3), new XAttribute("Value4", OptionValue4), new XAttribute("Value5", OptionValue5)),
                            new XElement("Compiler", new XAttribute("Name", CompilerSelect.Text)),
                            new XElement("Language", new XAttribute("Name", lang))
                            )
                        );


                    myXDoc.Save(xmlPath);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

            //create global.ini

            if (File.Exists(frmMAIN.APPLICATION_PATH + "global.ini") == false)
            {
                string xmlPath = frmMAIN.APPLICATION_PATH + "global.ini";
                try
                {//生成Project 文件
                    XDocument myXDoc = new XDocument(
                        new XComment("global config "),
                        new XElement("GLOBAL",/*new XAttribute("Web","http://www.dzmi.com"),*/new XAttribute("Vesion", "1.00"),
                        //new XAttribute("Author","Mike.Mo"),
                          new XElement("ProjectPath", new XAttribute("Ppath", str + ".Proj")),
                          new XElement("ProjectFont", new XAttribute("Size", "16"))
                            )
                        );

                    myXDoc.Save(xmlPath);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(frmMAIN.APPLICATION_PATH + "global.ini");
                XmlNodeList xmlNodes = xmlDoc.SelectSingleNode("/GLOBAL").ChildNodes;///SouceCode

                foreach (XmlNode xn1 in xmlNodes)
                {
                    XmlElement xe2 = (XmlElement)xn1;
                    if (xe2.Name == "ProjectPath")
                    {
                        xe2.Attributes["Ppath"].Value = str + "\\" + ProjectName + ".Proj";
                        break;//找到退出来就可以了   
                    }
                }
                xmlDoc.Save(frmMAIN.APPLICATION_PATH + "global.ini");
            }
        }

        private void btCancle_Click(object sender, EventArgs e)
        {
            btCancle.DialogResult = DialogResult.Cancel;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialogNewProject = new FolderBrowserDialog();

            folderBrowserDialogNewProject.Description = "请选择项目存放目录，不自动创建新的文件夹！";

            folderBrowserDialogNewProject.SelectedPath = ProjectPath_comboBox.Text;

            if (folderBrowserDialogNewProject.ShowDialog() == DialogResult.OK)
            {
                string ProjectPath = folderBrowserDialogNewProject.SelectedPath.ToString();
                //if (checkBoxCreateDirectory.Checked==true)
                //{
                //    ProjectPath_comboBox.Text = ProjectPath + ProjectName_textBox.Text;
                //} 
                //else
                //{
                ProjectPath_comboBox.Text = ProjectPath;
                // }

            }
            else
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText21);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
