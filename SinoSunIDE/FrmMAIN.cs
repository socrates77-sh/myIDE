﻿#define  DEBUG_MODEL
//#define  Editor_debug

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
using SinoSunIDE.Languages;



namespace SinoSunIDE
{

    //public  int Stop_stat = 0;
    public partial class frmMAIN : Form
    {
        FrmStopMess obj;
        public static int Stop_stat = 0;
        public static int RomSpace_stat = 0;
        public frmMAIN(string strProName)
        {
            InitializeComponent();

            if (strProName != null)// wj add for open IDE through the pro file
            {
                string srt = frmMAIN.APPLICATION_PATH + "global.ini";
                XElement rootProNode = XElement.Load(srt);

                IEnumerable<XElement> myprovalue = from myTarget in rootProNode.Descendants("ProjectPath")
                                                   select myTarget;
                foreach (XElement xnode in myprovalue)
                {
                    xnode.Attribute("Ppath").Value = strProName;
                }

                rootProNode.Save(srt);
            }

            //wj add 20141209
            if (!this.frmfile.IsDisposed)
            {
                this.frmfile.tsFiles.TabStripItemSelectionChanged += new FarsiLibrary.Win.TabStripItemChangedHandler(this.ItemSelectionChanged);
            }

            string path = frmMAIN.APPLICATION_PATH + "global.ini";
            string languageSel = null;

            XElement rootNode = XElement.Load(path);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectLanguage")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                languageSel = xnode.Attribute("Value").Value;
            }

            int curindex = 0;
            int.TryParse(languageSel, out curindex);
            int count = ShowLanguage.GetLanguages().Count;
            for (int i = 0; i < count; i++)
            {
                IShowLanguage language = (IShowLanguage)ShowLanguage.GetLanguages()[i];
                if (languageSel == "" || curindex >= count)
                {
                    if (language.IsDefault)
                    {
                        ShowLanguage.Current = language;
                        break;
                    }
                }
                else if (i == curindex)
                {
                    ShowLanguage.Current = language;
                    break;
                }
            }
            ApplyLanguage();

            //添加委托事件
            frmexplorer.Openfile_MouseDouble += new MyDelegate(OpenFile_MouseDouble_Click);
            frmmessage.gotoLine_Click += new FindErroLine(gotoLine_Click);

            m_deserializeDockContent += new DeserializeDockContent(GetContentFromPersistString);
            //frmmessage.
            recentFileHandler.RecentFileToolStripItem = RecentFilesToolStripMenuItem;
            recentProjectHandler.RecentFileToolStripItem = RecentProjectToolStripMenuItem;
            //initial button enable/disable
            DebugtoolStripButton.Checked = false;
            DebugButtonDisplayContral();

            busy_Timer.Enabled = false;

            const string OptionName = "CustomInfo.xml";

            string CurPath = Application.StartupPath; //获取当前EXE路径
            string xmlFileName = CurPath + "\\" + OptionName;

            toolStripStatusLabel2.Text = OperateIniFile.ReadIniData("Information", "Website", "WinScope IDE", xmlFileName);
            //frm = OperateIniFile.ReadIniData("Information", "SoftName", "WinScope IDE", xmlFileName);
            this.Text += OperateIniFile.ReadIniData("Information", "Version", "V0.04bt", xmlFileName);

        }

        private void frmMAIN_Load(object sender, EventArgs e)
        {

            string uiFile = Path.Combine(Application.StartupPath, "CustomUI.xml");
            if (File.Exists(uiFile))
                this.dockPanelMain.LoadFromXml(uiFile, m_deserializeDockContent); //UI display

            this.toolStripStatusLabel3.Text = ShowLanguage.Current.LoginTime + "：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            InitialProjectWindown();

            //check update 
            //Thread th_updateCheck = new Thread(update_check);//hide by wj 20150415
            //update_check();
            //th_updateCheck.Start();//hide by wj 20150415

#if Editor_debug
                fs.Show();
#endif


        }



        #region 加载布局
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FrmMessage).ToString())
                return frmmessage;
            else if (persistString == typeof(FrmExplorer).ToString())
                return frmexplorer;
            else if (persistString == typeof(FrmREG).ToString())
                return frmREG;
            else if (persistString == typeof(FrmFile).ToString())
                return frmfile;
            else if (persistString == typeof(FrmBreakPoint).ToString())
                return frmBreakPoint;
            else if (persistString == typeof(FrmRAM).ToString())
                return frmRAM;
            else if (persistString == typeof(FrmROM).ToString())
                return frmROM;
            else if (persistString == typeof(FrmPC).ToString())
                return frmPC;
            else if (persistString == typeof(FrmSys).ToString())
                return frmSys;
            else if (persistString == typeof(FrmWatch).ToString())
                return frmWatch;
            else if (persistString == typeof(FrmMTPRAM).ToString())
                return frmMTPRAM;
            //else if (persistString == typeof(FrmEditor).ToString())
            //    return frme;
            else
            {

                frmexplorer.Show(dockPanelMain);
                frmexplorer.DockTo(dockPanelMain, DockStyle.Left);
                frmmessage.Show(dockPanelMain);
                frmmessage.DockTo(dockPanelMain, DockStyle.Bottom);
                frmREG.Show(dockPanelMain);
                frmREG.DockTo(dockPanelMain, DockStyle.Right);
                frmfile.Show(dockPanelMain);
                frmfile.DockTo(dockPanelMain, DockStyle.Fill);
                frmBreakPoint.DockTo(dockPanelMain, DockStyle.Right);
                frmBreakPoint.Show(dockPanelMain);
                frmRAM.DockTo(dockPanelMain, DockStyle.Bottom);
                frmRAM.Show(dockPanelMain);
                frmMTPRAM.DockTo(dockPanelMain, DockStyle.Bottom);
                frmMTPRAM.Show(dockPanelMain);
                frmROM.DockTo(dockPanelMain, DockStyle.Bottom);
                frmROM.Show(dockPanelMain);
                frmPC.DockTo(dockPanelMain, DockStyle.Left);
                frmPC.Show(dockPanelMain);
                frmSys.DockTo(dockPanelMain, DockStyle.Left);
                frmSys.Show(dockPanelMain);
                frmWatch.DockTo(dockPanelMain, DockStyle.Bottom);
                frmWatch.Show(dockPanelMain);
                //fs.Show(dockPanelMain);
                //fs.DockTo(dockPanelMain, DockStyle.Top);
                return frmfile;
            }
        }
        #endregion

        private void frmDisplayControl_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (System.Windows.Forms.ToolStripMenuItem)sender;
            if (tm.Tag == null)
                return;
            switch (tm.Tag.ToString())
            {

                case "ProjectWindow":
                    if (frmexplorer.IsDisposed)
                    {
                        frmexplorer = new FrmExplorer();
                        frmexplorer.Openfile_MouseDouble += new MyDelegate(OpenFile_MouseDouble_Click);
                    }
                    frmexplorer.Show(dockPanelMain);
                    frmexplorer.DockTo(dockPanelMain, DockStyle.Left);
                    ProjectWindowSH_ToolStripMenuItem.Checked = true;
                    break;

                case "EditorWindow":
                    if (frmfile.IsDisposed)
                    {
                        frmfile = new FrmFile();
                        frmfile.tsFiles.TabStripItemSelectionChanged += new FarsiLibrary.Win.TabStripItemChangedHandler(this.ItemSelectionChanged);
                    }
                    frmfile.Show(dockPanelMain);
                    frmfile.DockTo(dockPanelMain, DockStyle.Fill);
                    EditorToolStripMenuItem.Checked = true;
                    break;
                case "REGWindow":
                    if (frmREG.IsDisposed)
                    {
                        frmREG = new FrmREG();
                    }
                    frmREG.Show(dockPanelMain);
                    frmREG.DockTo(dockPanelMain, DockStyle.Right);
                    REGWindowSH_ToolStripMenuItem.Checked = true;
                    break;

                case "BuildWindow":
                    if (frmmessage.IsDisposed)
                    {
                        frmmessage = new FrmMessage();
                        frmmessage.gotoLine_Click += new FindErroLine(gotoLine_Click);
                    }
                    frmmessage.Show(dockPanelMain);
                    frmmessage.DockTo(dockPanelMain, DockStyle.Bottom);
                    BuildWindowSH_ToolStripMenuItem.Checked = true;
                    break;

                case "OptionWindow":
                    if (frmPC.IsDisposed)
                    {
                        frmPC = new FrmPC();
                    }
                    frmPC.Show(dockPanelMain);
                    frmPC.DockTo(dockPanelMain, DockStyle.Left);
                    OptionWindowToolStripMenuItem.Checked = true;
                    break;

                case "SFREGWindow":
                    //fs.Show(dockPanelMain);
                    if (frmSys.IsDisposed)
                    {
                        frmSys = new FrmSys();
                    }
                    frmSys.Show(dockPanelMain);
                    frmSys.DockTo(dockPanelMain, DockStyle.Right);
                    SFREGWindowSH_ToolStripMenuItem.Checked = true;
                    break;
                case "BreakpointWindow":
                    if (frmBreakPoint.IsDisposed)
                    {
                        frmBreakPoint = new FrmBreakPoint();
                    }
                    frmBreakPoint.Show(dockPanelMain);
                    frmBreakPoint.DockTo(dockPanelMain, DockStyle.Right);
                    BreakpointWindowSH_ToolStripMenuItem.Checked = true;
                    break;
                case "RAMWindow":
                    if (frmRAM.IsDisposed)
                    {
                        frmRAM = new FrmRAM();
                    }
                    frmRAM.Show(dockPanelMain);
                    frmRAM.DockTo(dockPanelMain, DockStyle.Bottom);
                    RAMWindowSH_ToolStripMenuItem.Checked = true;
                    break;
                case "MTPWindow":
                    if (frmMTPRAM.IsDisposed)
                    {
                        frmMTPRAM = new FrmMTPRAM();
                    }
                    frmMTPRAM.Show(dockPanelMain);
                    frmMTPRAM.DockTo(dockPanelMain, DockStyle.Bottom);
                    MTPWindowSH_ToolStripMenuItem.Checked = true;
                    break;
                case "ROMWindow":
                    if (frmROM.IsDisposed)
                    {
                        frmROM = new FrmROM();
                        //frmROM.Openfile_MouseDouble += new MyDelegate(OpenFile_MouseDouble_Click);
                    }
                    frmROM.Show(dockPanelMain);
                    frmROM.DockTo(dockPanelMain, DockStyle.Bottom);
                    ROMWindowSH_ToolStripMenuItem.Checked = true;

                    break;
                case "WatchWindow":
                    if (frmWatch.IsDisposed)
                    {
                        frmWatch = new FrmWatch();
                    }
                    frmWatch.Show(dockPanelMain);
                    frmWatch.DockTo(dockPanelMain, DockStyle.Bottom);
                    WatchWindowSH_ToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;

            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void OpenFile_ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreateTab(ofdMain.FileName);
            }
        }

        //
        //frmExplorer.openfile_mouseDouble event
        //

        public void OpenFile_MouseDouble_Click(string str)
        {
            if (str != null)
            {
                CreateTab(str);

            }
            else
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText);
            }
        }

        private void NewFile_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }


        private void CloseToolStrip_Click(object sender, EventArgs e)
        {
            if (frmfile.tsFiles.SelectedItem != null)
            {
                frmfile.tsFiles.Items.Remove(frmfile.tsFiles.SelectedItem);
                if (frmfile.tsFiles.Items.Count > 0)
                {
                    frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[0];
                }
            }
        }

        private void CloseProjWindown_Click(object sender, EventArgs e)
        {
            if (frmexplorer.treeView1.Nodes.Count <= 0)//modify by wj 20150415
            {
                return;
            }
            DialogResult BResult = MessageBox.Show(ShowLanguage.Current.MessageBoxText1, ShowLanguage.Current.MessageBoxCaption1, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (BResult == DialogResult.OK)
            {
                saveAllToolStripMenuItem_Click(null, null);
            }
            /*else
            {
                return;
            }modify by wj 20150415*/

            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;
                //get UniqueId of current line
                // int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;
                while (info.EnableBreakpoints.Count > 0)
                {
                    int id = info.EnableBreakpoints[info.EnableBreakpoints.Count - 1];
                    //remove BK
                    info.EnableBreakpoints.Remove(id);
                    info.EnableBreakpointsLineId.Remove(id);
                }

                while (info.DisableBreakpoints.Count > 0)
                {
                    int id = info.DisableBreakpoints[info.DisableBreakpoints.Count - 1];
                    //remove BK
                    info.DisableBreakpoints.Remove(id);
                    //remove BK
                    info.DisableBreakpointsLineId.Remove(id);

                }
                //repaint
                tb.Invalidate();
            }
            BreakPointCheck();

            int aaa = frmfile.tsFiles.Items.Count;
            if (aaa > 0)
            {
                for (int i = aaa - 1; i >= 0; i--)
                {
                    frmfile.tsFiles.Items.Remove(frmfile.tsFiles.Items[i]);
                }
            }

            if (frmexplorer.IsDisposed == false)
            {
                frmexplorer.treeView1.Nodes.Clear();
            }
            DebugtoolStripButton.Checked = false;
            DebugFlag = false;
            SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
            DebugButtonDisplayContral();
            if (COMPORT.port1 != null)
            {
                if (COMPORT.port1.IsOpen == true)
                {
                    COMPORT.ClosePort();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmfile.tsFiles.SelectedItem != null)
                Save(frmfile.tsFiles.SelectedItem);
        }
        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (frmfile.tsFiles.SelectedItem != null)
            //    Save(frmfile.tsFiles.SelectedItem);
            int aaa = frmfile.tsFiles.Items.Count;
            if (aaa > 0)
            {
                for (int i = 0; i < aaa; i++)
                {
                    frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[i];
                    Save(frmfile.tsFiles.SelectedItem);
                }
            }

            //save explorer treeviw
            ProjectSaveXML();


        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmfile.tsFiles.SelectedItem != null)
            {
                string oldFile = frmfile.tsFiles.SelectedItem.Tag as string;
                frmfile.tsFiles.SelectedItem.Tag = null;
                if (!Save(frmfile.tsFiles.SelectedItem))
                    if (oldFile != null)
                    {
                        frmfile.tsFiles.SelectedItem.Tag = oldFile;
                        frmfile.tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                    }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                CurrentTB.Print(new PrintDialogSettings());
            }
        }

        private void PrintSetToolStripButton_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

        }

        void recentFiles_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RecentFileHandler.FileMenuItem fmi = (RecentFileHandler.FileMenuItem)e.ClickedItem;
            // this.op(fmi.FileName);
            OpenFile_MouseDouble_Click(fmi.FileName);

        }

        void recentProject_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RecentFileHandler.FileMenuItem fmi = (RecentFileHandler.FileMenuItem)e.ClickedItem;
            // this.op(fmi.FileName);
            OpenProject(fmi.FileName);

        }

        /// <summary>
        /// Copy selected text into Clipboard
        /// </summary>
        public void Copy()
        {
            //if (Selection.End != Selection.Start)
            //{
            //    ExportToHTML exp = new ExportToHTML();
            //    exp.UseBr = false;
            //    exp.UseNbsp = false;
            //    exp.UseStyleTag = true;
            //    string html = "<pre>" + exp.GetHtml(Selection) + "</pre>";
            //    var data = new DataObject();
            //    data.SetData(DataFormats.UnicodeText, true, Selection.Text);
            //    data.SetData(DataFormats.Html, PrepareHtmlForClipboard(html));
            //    Clipboard.SetDataObject(data, true);
            //}
        }

        private bool Save(FATabStripItem tab)
        {

            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
            }

            if (tb.IsChanged == true)// wj add 20150318
            {
                try
                {
                    File.WriteAllText(tab.Tag as string, tb.Text, Encoding.Default);
                    tb.IsChanged = false;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        return Save(tab);
                    else
                        return false;
                }
            }

            tb.Invalidate();

            return true;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.Paste();
        }

        private void deletesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.ClearSelected();
        }
        private void deletesCurrentLine_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.ClearCurrentLine();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.Selection.SelectAll();
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            CurrentTB.ShowFindDialog();

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            CurrentTB.ShowReplaceDialog();
        }

        private void commentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CurrentTB.InsertLinePrefix(";");
            if (MCU_TYPE == "CHC05" || MCU_TYPE == "CRISC")
                CurrentTB.InsertLinePrefix("//");
            else
                CurrentTB.InsertLinePrefix(";");
        }

        private void uncommentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CurrentTB.RemoveLinePrefix(";");
            if (MCU_TYPE == "CHC05" || MCU_TYPE == "CRISC")
                CurrentTB.RemoveLinePrefix("//");
            else
                CurrentTB.RemoveLinePrefix(";");
        }
        private void IndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.IncreaseIndent();
        }
        private void UnindentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.DecreaseIndent();
        }

        private void goForwardCtrlShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.NavigateForward();
        }

        private void goBackwardCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            CurrentTB.NavigateBackward();
        }

        //test jump to line
        private void gotoLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // uinASM_compile();
        }

        //test jump to line
        private void gotoLine_Click(int iLine)
        {
            //CurrentTB.CollapseBlock(10, 15); 块缩进
            // CurrentTB.GetLine(10);
            // string aaa=CurrentTB.GetLineText(10);

            // TempCounter = TempCounter + 1;
            if (CurrentTB == null) return;
            if (iLine < CurrentTB.LinesCount)
            {
                CurrentTB.Selection.Start = new Place(0, iLine - 1);
                CurrentTB.DoSelectionVisible();
                CurrentTB.Invalidate();
            }

        }
        #region Initial ProjectWindown


        private void InitialProjectWindown()
        {
            string str = frmMAIN.APPLICATION_PATH + "global.ini";
            string strPath = null;

            XElement rootNode = XElement.Load(str);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                strPath = xnode.Attribute("Ppath").Value;

                if (File.Exists(strPath) == false)
                {
                    strPath = frmMAIN.APPLICATION_PATH + "Sample\\aaaa\\aaaa.Proj";
                    xnode.Attribute("Ppath").Value = strPath;
                    //return;
                }
            }
            rootNode.Save(str);

            ProjectPath = strPath;
            frmexplorer.ProjectTreeBuild(strPath);

            XElement rootNode0 = XElement.Load(strPath);

            MCU_ID = rootNode0.Attribute("ID").Value;
            MCU_TYPE = rootNode0.Attribute("MCU_Type").Value;

            InitalDeviceConfig(MCU_ID);

            if (frmPC.IsDisposed == false)
            {
                frmPC.OptionReflash();
            }

            if (frmSys.IsDisposed == false)
            {
                frmSys.Loadxml(MCU_ID);
            }
            if (frmREG.IsDisposed == false)
            {
                frmREG.GetXmlNodeInformation(MCU_ID);
            }
            if (frmRAM.IsDisposed == false)
            {
                frmRAM.RamInitial(DeviceConfigXX.MCU_RAMSize);
            }
            if (frmMTPRAM.IsDisposed == false)
            {
                frmMTPRAM.RamInitial(0x400);
            }

            DebugInitial();

        }

        private void InitalDeviceConfig(string mcu_ID)
        {
            string str = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            //string xmlPath = 
            XElement rootNode = XElement.Load(str);
            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                            where (string)myTarget.Attribute("id").Value == mcu_ID
                                            select myTarget;


            IEnumerable<XElement> chipcfg = from srootNode in myvalue.Descendants("chip_cfg") select srootNode;
            foreach (XElement xnode in chipcfg)
            {
                string temp = xnode.Attribute("chip_romFirstAddr").Value;
                DeviceConfigXX.MCU_ROMAddrFirst = Convert.ToUInt16(temp, 16);

                temp = xnode.Attribute("chip_romEndAddr").Value;
                DeviceConfigXX.MCU_ROMAddrEnd = Convert.ToUInt16(temp, 16);

                temp = xnode.Attribute("chip_ramFirstAddr").Value;
                DeviceConfigXX.MCU_RAMAddrFirst = Convert.ToUInt16(temp, 16);

                temp = xnode.Attribute("chip_ramEndAddr").Value;
                DeviceConfigXX.MCU_RAMAddrEnd = Convert.ToUInt16(temp, 16);

                temp = xnode.Attribute("chip_ramSize").Value;
                DeviceConfigXX.MCU_RAMSize = Convert.ToUInt16(temp, 16);

                temp = xnode.Attribute("chip_romSize").Value;
                DeviceConfigXX.MCU_ROMSize = Convert.ToUInt16(temp, 16);

            }

            IEnumerable<XElement> devcfg = from dc in myvalue.Descendants("ev_cfg") select dc;
            foreach (XElement xnode2 in devcfg)
            {
                string temp = xnode2.Attribute("ev_romFirstAddr").Value;
                DeviceConfigXX.DEV_ROMAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_romEndAddr").Value;
                DeviceConfigXX.DEV_ROMAddrEnd = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_ramFirstAddr").Value;
                DeviceConfigXX.DEV_RAMAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_ramEndAddr").Value;
                DeviceConfigXX.DEV_RAMAddrEnd = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_bkFirstAddr").Value;
                DeviceConfigXX.DEV_BKAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_bkEndAddr").Value;
                DeviceConfigXX.DEV_BKAddrEnd = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_pcFirstAddr").Value;
                DeviceConfigXX.DEV_PCAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_pcEndAddr").Value;
                DeviceConfigXX.DEV_PCAddrEnd = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_optionAddr").Value;
                DeviceConfigXX.DEV_OPTIONAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_optionCnt").Value;
                DeviceConfigXX.DEV_OPTIONAddrEnd = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_traceFirstAddr").Value;
                DeviceConfigXX.DEV_TRACEAddrFirst = Convert.ToUInt32(temp, 16);

                temp = xnode2.Attribute("ev_traceEndAddr").Value;
                DeviceConfigXX.DEV_TRACEAddrEnd = Convert.ToUInt32(temp, 16);

            }


        }
        #endregion

        private void OpenProjecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            // CreateTab(ofdMain.FileName);
            {
                if (File.Exists(opdMain.FileName) == true)
                {
                    CloseProjWindown_Click(null, null);
                }

                OpenProject(opdMain.FileName);
            }
        }
        private void OpenProject(string path)
        {
            ProjectPath = path;
            frmexplorer.ProjectTreeBuild(path);

            //reflash option display
            string srt = frmMAIN.APPLICATION_PATH + "global.ini";
            XElement rootNode = XElement.Load(srt);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                xnode.Attribute("Ppath").Value = ProjectPath;
            }

            rootNode.Save(srt);

            //recent project path
            recentProjectHandler.AddFile(ProjectPath);
            //search mcu id
            XElement rootNode0 = XElement.Load(ProjectPath);

            MCU_ID = rootNode0.Attribute("ID").Value;
            MCU_TYPE = rootNode0.Attribute("MCU_Type").Value;

            myvalue = from myTarget in rootNode0.Descendants("Option")
                      select myTarget;
            foreach (XElement xnode in myvalue)
            {
                if (xnode.Attribute("Value") != null)
                {
                    if (xnode.Attribute("Value").Value != "")
                    {
                        frmMAIN.OPTION = Convert.ToUInt32(xnode.Attribute("Value").Value, 16);
                        frmMAIN.OPTION0NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION = 0x00;
                        frmMAIN.OPTIONNullFlag = true;
                    }
                }

                if (xnode.Attribute("Value0") != null)
                {
                    if (xnode.Attribute("Value0").Value != "")
                    {
                        frmMAIN.OPTION0 = Convert.ToUInt32(xnode.Attribute("Value0").Value, 16);
                        frmMAIN.OPTION0NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION0 = 0x00;
                        frmMAIN.OPTION0NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value1") != null)
                {
                    if (xnode.Attribute("Value1").Value != "")
                    {
                        frmMAIN.OPTION1 = Convert.ToUInt32(xnode.Attribute("Value1").Value, 16);
                        frmMAIN.OPTION1NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION1 = 0x00;
                        frmMAIN.OPTION1NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value2") != null)
                {
                    if (xnode.Attribute("Value2").Value != "")
                    {
                        frmMAIN.OPTION2 = Convert.ToUInt32(xnode.Attribute("Value2").Value, 16);
                        frmMAIN.OPTION2NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION2 = 0x00;
                        frmMAIN.OPTION2NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value3") != null)
                {
                    if (xnode.Attribute("Value3").Value != "")
                    {
                        frmMAIN.OPTION3 = Convert.ToUInt32(xnode.Attribute("Value3").Value, 16);
                        frmMAIN.OPTION3NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION3 = 0x00;
                        frmMAIN.OPTION3NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value4") != null)
                {
                    if (xnode.Attribute("Value4").Value != "")
                    {
                        frmMAIN.OPTION4 = Convert.ToUInt32(xnode.Attribute("Value4").Value, 16);
                        frmMAIN.OPTION4NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION4 = 0x00;
                        frmMAIN.OPTION4NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value5") != null)
                {
                    if (xnode.Attribute("Value5").Value != "")
                    {
                        frmMAIN.OPTION5 = Convert.ToUInt32(xnode.Attribute("Value5").Value, 16);
                        frmMAIN.OPTION5NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION5 = 0x00;
                        frmMAIN.OPTION5NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value6") != null)
                {
                    if (xnode.Attribute("Value6").Value != "")
                    {
                        frmMAIN.OPTION6 = Convert.ToUInt32(xnode.Attribute("Value6").Value, 16);
                        frmMAIN.OPTION6NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION6 = 0x00;
                        frmMAIN.OPTION6NullFlag = true;
                    }
                }

                if (xnode.Attribute("Value7") != null)
                {
                    if (xnode.Attribute("Value7").Value != "")
                    {
                        frmMAIN.OPTION7 = Convert.ToUInt32(xnode.Attribute("Value7").Value, 16);
                        frmMAIN.OPTION7NullFlag = false;
                    }
                    else
                    {
                        frmMAIN.OPTION7 = 0x00;
                        frmMAIN.OPTION7NullFlag = true;
                    }
                }

                if (MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                {
                    if (xnode.Attribute("FOSC_7041")!=null)
                    {
                        if (xnode.Attribute("FOSC_7041").Value != "")
                        {
                            frmPC.propertyGridEx_PC.Item[4].Value = xnode.Attribute("FOSC_7041").Value;
                        }
                        else
                        {
                            frmPC.propertyGridEx_PC.Item[4].Value = xnode.Attribute("FOSC_7041").Value;
                        }

                    }
                    
                }
                
            }


            if (frmPC.IsDisposed == false)
            {
                frmPC.OptionReflash();
            }

            if (frmSys.IsDisposed == false)
            {
                frmSys.Loadxml(MCU_ID);
            }
            if (frmREG.IsDisposed == false)
            {
                frmREG.GetXmlNodeInformation(MCU_ID);
            }

            InitalDeviceConfig(MCU_ID);
            if (frmRAM.IsDisposed == false)
            {
                frmRAM.RamInitial(DeviceConfigXX.MCU_RAMSize);
            }
        }

        bool tbFindChanged = false;

        private void tbFindNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                Range r = tbFindChanged ? CurrentTB.Range.Clone() : CurrentTB.Selection.Clone();
                tbFindChanged = false;
                r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                //var pattern = Regex.Replace(tbFind.Text, FastColoredTextBoxNS.FindForm.RegexSpecSymbolsPattern, "\\$0");
                var pattern = Regex.Escape(tbFind.Text);
                foreach (var found in r.GetRanges(pattern))
                {
                    found.Inverse();
                    CurrentTB.Selection = found;
                    CurrentTB.DoSelectionVisible();
                    return;
                }
                MessageBox.Show(ShowLanguage.Current.MessageBoxText2);
            }
            else
                tbFindChanged = true;
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && CurrentTB != null)
            {
                //Range r = tbFindChanged ? CurrentTB.Range.Clone() : CurrentTB.Selection.Clone();
                //tbFindChanged = false;
                //r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                //var pattern = Regex.Replace(tbFind.Text, FastColoredTextBoxNS.FindForm.RegexSpecSymbolsPattern, "\\$0");

                Range r = tbFindChanged ? CurrentTB.Range.Clone() : CurrentTB.Selection.Clone();
                tbFindChanged = false;
                r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                var pattern = Regex.Escape(tbFind.Text);
                foreach (var found in r.GetRanges(pattern))
                {
                    found.Inverse();
                    CurrentTB.Selection = found;
                    CurrentTB.DoSelectionVisible();
                    return;
                }
                MessageBox.Show(ShowLanguage.Current.MessageBoxText2);
            }
            else
                tbFindChanged = true;
        }
        private void cloneLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
        }

        private void cloneLinesAndCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //start autoUndo block
            CurrentTB.BeginAutoUndo();
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //comment lines
            CurrentTB.InsertLinePrefix(";");
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
            //end of autoUndo block
            CurrentTB.EndAutoUndo();
        }

        #region CompileClick

        private void compile_currentfile(object sender, EventArgs e)    //编译文件
        {
            /*if (MCU_ID == "0x7323")
            {
                if (frmPC.optionValue0.Text != "")
                {
                    UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                    byte[] Value0=new byte[3];
                    //Value0[8] = (byte)(opt / 0x100);
                    Value0[0] = (byte)(opt % 0x100);
                }
            }*/

            saveToolStripMenuItem_Click(null, null);

            if (DebugtoolStripButton.Checked == true)
            {
                DebugCommand("Stop");
                //DebugCommand("Reset");
                DebugtoolStripButton_Click(null, null);
            }

            if (MCU_TYPE == "RISC")
            {
                RISC_compile_currentfile();                         //汇编文件编译
            }
            else if (MCU_TYPE == "CRISC")
            {
                CRISC_compile_currentfile();                        //C文件编译
            }
            else if (MCU_TYPE == "AHC05")
            {
                HC05_compilerASM();
                //HC05_compilerC();
            }
            else //MCU_TYPE=="CHC05"
            {
                HC05_compilerC();
            }

        }

        //         [DllImport("IAES.dll", EntryPoint = "EncryptFile", CallingConvention = CallingConvention.StdCall)]
        //         public static extern void EncryptFile(string path);
        // 
        //         [DllImport("IAES.dll", EntryPoint = "DecryptFile", CallingConvention = CallingConvention.StdCall)]
        //         public static extern void DecryptFile(string path);
        // 
        //         [DllImport("IAES.dll", EntryPoint = "IsTextFile", CallingConvention = CallingConvention.StdCall)]
        //         public static extern string IsTextFile(string path);

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)     //生成S代码
        {
            //             if (IsTextFile(@"D:\2891k.WRT") == "true")
            //                 EncryptFile(@"D:\2891k.WRT");
            //             else
            //                 DecryptFile(@"D:\2891k.WRT");
            if (frmexplorer.treeView1.Nodes.Count <= 0)
            {
                MessageBox.Show("The Project is Null!");
                return;
            }
            //save all asm files
            saveAllToolStripMenuItem_Click(null, null);
            if (DebugtoolStripButton.Checked == true)
            {
                DebugCommand("Stop");
                //System.Threading.Thread.Sleep(100);
                // DebugCommand("Reset");
                DebugtoolStripButton_Click(null, null);
            }

            if (MCU_TYPE == "RISC")
            {
                RISC_compiler();                                    //汇编
                if (MCU_ID == "0x7011" || MCU_ID == "0x7031")       //detele ||MCU_ID=="0x7041" by LYL 20170216
                {
                    checks19error();
                }
            }
            else if (MCU_TYPE == "CRISC")
            {
                if (MCU_ID == "0x7011" || MCU_ID == "0x7031" || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                    CRISC_compiler_2711();                          //C外部编译器
                else
                    CRISC_compiler();                               //C内部编译器
            }
            else if (MCU_TYPE == "AHC05")
            {
                HC05_compilerASM();
                //HC05_compilerC();
            }
            else //MCU_TYPE=="CHC05"
            {
                HC05_compilerC();
            }

            // update watch
            int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1;
            for (int i = 0; i < datagrid_len; i++)
            {
                string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[0].Value.ToString();
                frmWatch.Watch_add_symbol(addr_temp, i);
            }

        }

        private void checks19error()
        {
            string filename = null;
            string fpath = null;
            filename = ProjectPath;
            filename = filename.Substring(0, filename.LastIndexOf("\\")) + "\\Output";
            fpath = filename;
            filename = bsGetFiles.GetFiles(new DirectoryInfo(filename), "*.s19");
            fpath = bsGetFiles.GetFiles(new DirectoryInfo(fpath), "*.lst");
            string objectfilename = Path.GetFileNameWithoutExtension(fpath) + ".lst";
            if (File.Exists(filename) == false || File.Exists(fpath) == false)
            {
                return;
            }
            StreamReader FileLoad = new StreamReader(filename);
            string s = FileLoad.ReadToEnd();
            string[] Line = s.Split(new char[] { '\n' }, StringSplitOptions.None);
            FileLoad.Close();

            StreamReader FileLoad1 = new StreamReader(fpath);
            string s1 = FileLoad1.ReadToEnd();
            string[] Line1 = s1.Split(new char[] { '\n' }, StringSplitOptions.None);
            FileLoad1.Close();

            int i, j, k;
            int Addr = 0;
            byte dataH, dataL;
            bool haveErrorCode7011 = false;
            string strErrorCode = null;
            string strErrorMsg = null;
            for (i = 0; i < Line.Length; i++)
            {
                byte t1, t2, t3, t4;

                if (Line[i].Length < 1)
                {
                    continue;
                }
                if (Line[i][1] != '1')
                {
                    continue;
                }

                // Length
                t1 = Line[i][2] > 0x39 ? System.Convert.ToByte((int)Line[i][2] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][2] - 0x30);
                t2 = Line[i][3] > 0x39 ? System.Convert.ToByte((int)Line[i][3] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][3] - 0x30);

                // Addr
                t1 = Line[i][4] > 0x39 ? System.Convert.ToByte((int)Line[i][4] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][4] - 0x30);
                t2 = Line[i][5] > 0x39 ? System.Convert.ToByte((int)Line[i][5] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][5] - 0x30);
                t3 = Line[i][6] > 0x39 ? System.Convert.ToByte((int)Line[i][6] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][6] - 0x30);
                t4 = Line[i][7] > 0x39 ? System.Convert.ToByte((int)Line[i][7] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][7] - 0x30);
                Addr = t1 * 4096 + t2 * 256 + t3 * 16 + t4;

                for (j = 8; j < Line[i].Length - 6; j += 4)
                {
                    t1 = Line[i][j] > 0x39 ? System.Convert.ToByte((int)Line[i][j] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j] - 0x30);
                    t2 = Line[i][j + 1] > 0x39 ? System.Convert.ToByte((int)Line[i][j + 1] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j + 1] - 0x30);
                    t3 = Line[i][j + 2] > 0x39 ? System.Convert.ToByte((int)Line[i][j + 2] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j + 2] - 0x30);
                    t4 = Line[i][j + 3] > 0x39 ? System.Convert.ToByte((int)Line[i][j + 3] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j + 3] - 0x30);
                    dataL = (byte)(t1 * 16 + t2);
                    dataH = (byte)(t3 * 16 + t4);
                    if (((dataH == 0x50 || dataH == 0x51 || dataH == 0x52 || dataH == 0x53 || dataH == 0x54 ||
                          dataH == 0x58 || dataH == 0x59 || dataH == 0x5a || dataH == 0x5b || dataH == 0x5c ||
                          dataH == 0x70 || dataH == 0x71 || dataH == 0x72 || dataH == 0x73 || dataH == 0x74 ||
                          dataH == 0x78 || dataH == 0x79 || dataH == 0x7a || dataH == 0x7b || dataH == 0x7c) &&
                          dataL == 0xd0) ||
                        ((dataH == 0x50 || dataH == 0x51 || dataH == 0x52 || dataH == 0x53 || dataH == 0x54 ||
                          dataH == 0x55 || dataH == 0x58 || dataH == 0x59 || dataH == 0x5a || dataH == 0x5b ||
                          dataH == 0x5c || dataH == 0x5d || dataH == 0x70 || dataH == 0x71 || dataH == 0x72 ||
                          dataH == 0x73 || dataH == 0x74 || dataH == 0x75 || dataH == 0x78 || dataH == 0x79 ||
                          dataH == 0x7a || dataH == 0x7b || dataH == 0x7c || dataH == 0x7d) &&
                          dataL == 0xd4) ||
                        ((dataH == 0x52 || dataH == 0x53 || dataH == 0x54 || dataH == 0x5a || dataH == 0x5b ||
                          dataH == 0x5c || dataH == 0x72 || dataH == 0x73 || dataH == 0x74 || dataH == 0x7a ||
                          dataH == 0x7b || dataH == 0x7c) &&
                          dataL == 0xd5) ||
                        (((byte)(dataH & 0xf0) == 0x50 || (byte)(dataH & 0xf0) == 0x70) &&
                         (dataL == 0xdb || dataL == 0xdd || dataL == 0xed)) ||
                        ((dataH == 0x07 || dataH == 0x15 || dataH == 0x16 || dataH == 0x25 || dataH == 0x26) &&
                         (dataL == 0xd0 || dataL == 0xd4 || dataL == 0xd5 || dataL == 0xdb || dataL == 0xdd ||
                          dataL == 0xed)))
                    {
                        haveErrorCode7011 = true;
                        strErrorCode = (Addr / 2 + (j - 8) / 4).ToString("X6");
                        for (k = 0; k < Line1.Length; k++)
                        {
                            if(Line1[k].ToUpper().Contains(strErrorCode))
                            {
                                strErrorMsg = "\n" + objectfilename + ":" + (k + 1).ToString() + ":" + "Error[200]   Have Error Code,请参考应用笔记修改";
                                frmmessage.fastColoredTextBox.InsertText(strErrorMsg);
                            }
                        }
                    }
                }
            }
            if (haveErrorCode7011)
            {
                compile_result = false;
            }
        }

        private void compile_allfiles_download(object sender, EventArgs e)          //生成S代码及下载
        {
            if (frmexplorer.treeView1.Nodes.Count <= 0)
            {
                MessageBox.Show("The Project is Null!");
                return;
            }
            compileToolStripMenuItem_Click(null, null);                             //生成S代码
            if (!compile_result)
            {
                return;
            }
            DebugtoolStripButton_Click(null, null);
            if (MCU_ID == "0x7333" || MCU_ID == "0x7122" || MCU_ID == "0x9903" || MCU_ID == "0x9904")
            {
                BreakPointCheck();      //add by LYL for 7333,but others may need this tooo
            }
        }

        #endregion


        /// <summary>
        /// breakpoints click
        /// </summary>

        #region breakpoints click

        private void breakpointPlusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            TbInfo info = CurrentTB.Tag as TbInfo;
            //get UniqueId of current line
            int id_display = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;  //file line

            if (info.EnableBreakpointsLineId.Contains(id_display))
            {
                info.EnableBreakpoints.Remove(id_display); //EnableBreakpoints
                info.EnableBreakpointsLineId.Remove(id_display);
            }
            else
            {
                //add bookmark
                info.EnableBreakpoints.Add(id_display);
                info.EnableBreakpointsLineId.Add(id_display);
            }
            //repaint
            CurrentTB.Invalidate();

            BreakPointCheck();

        }

        private void btRemoveBreakpoints_Click(object sender, EventArgs e)
        {

            if (CurrentTB == null)
                return;
            TbInfo info = CurrentTB.Tag as TbInfo;
            //get UniqueId of current line
            int id_display = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;  //file line

            if (!info.EnableBreakpoints.Contains(id_display))
                return;
            //remove bookmark
            info.EnableBreakpoints.Remove(id_display);
            info.EnableBreakpointsLineId.Remove(id_display);


            info.DisableBreakpoints.Add(id_display);
            info.DisableBreakpointsLineId.Add(id_display);


            //repaint
            CurrentTB.Invalidate();
        }

        private void btRemoveAllBreakpoints_Click(object sender, EventArgs e) //disable all BK
        {

            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;
                //get UniqueId of current line
                // int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;
                while (info.EnableBreakpoints.Count > 0)
                {
                    int id = info.EnableBreakpoints[info.EnableBreakpoints.Count - 1];
                    //remove BK
                    info.EnableBreakpoints.Remove(id);
                    info.DisableBreakpoints.Add(id);
                    info.EnableBreakpointsLineId.Remove(id);
                    info.DisableBreakpointsLineId.Add(id);

                }

                //repaint
                tb.Invalidate();
            }
        }

        private void btClearAllBreakpoints_Click(object sender, EventArgs e)
        {

            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;
                //get UniqueId of current line
                // int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;
                while (info.EnableBreakpoints.Count > 0)
                {
                    int id = info.EnableBreakpoints[info.EnableBreakpoints.Count - 1];
                    //remove BK
                    info.EnableBreakpoints.Remove(id);
                    info.EnableBreakpointsLineId.Remove(id);
                }

                while (info.DisableBreakpoints.Count > 0)
                {
                    int id = info.DisableBreakpoints[info.DisableBreakpoints.Count - 1];
                    //remove BK
                    info.DisableBreakpoints.Remove(id);
                    //remove BK
                    info.DisableBreakpointsLineId.Remove(id);

                }
                //repaint
                tb.Invalidate();
            }
            BreakPointCheck();
        }


        private void BKButton_DropDownOpening(object sender, EventArgs e)
        {
            BKListtoolStripSplitButton.DropDownItems.Clear();

            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;
                // int a = info.EnableBreakpoints[0];
                for (int i = 0; i < info.EnableBreakpoints.Count; i++)
                {
                    var item = BKListtoolStripSplitButton.DropDownItems.Add("BK" + BKListtoolStripSplitButton.DropDownItems.Count + " [" + Path.GetFileNameWithoutExtension(tab.Tag as String) + "]");
                    //item.Tag = new () { tb = tb, iBookmark = i };
                    //item.Click += (o, a) => GoTo((BookmarkInfo)(o as ToolStripItem).Tag);
                }
            }
        }


        #endregion

        #region bookmark Click

        private void bookmarkPlusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            TbInfo info = CurrentTB.Tag as TbInfo;

            //get UniqueId of current line
            int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;
            if (info.bookmarksLineId.Contains(id))
            {
                info.bookmarks.Remove(id);
                info.bookmarksLineId.Remove(id);
                //repaint
                CurrentTB.Invalidate();
                return;
            }
            //add bookmark
            info.bookmarks.Add(id);
            info.bookmarksLineId.Add(id);
            //repaint
            CurrentTB.Invalidate();
        }
        private void bookmarkMinusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            TbInfo info = CurrentTB.Tag as TbInfo;
            //get UniqueId of current line
            int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;
            if (!info.bookmarksLineId.Contains(id))
                return;
            //remove bookmark
            info.bookmarks.Remove(id);
            info.bookmarksLineId.Remove(id);
            //repaint
            CurrentTB.Invalidate();
        }

        private void bookmarkMinusAllButton_Click(object sender, EventArgs e)
        {
            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;

                int bookmarkcouter = info.bookmarks.Count - 1;

                while (info.bookmarks.Count > 0)
                {
                    //get UniqueId of current line
                    int id = info.bookmarks[info.bookmarks.Count - 1];
                    info.bookmarks.Remove(id);
                    info.bookmarksLineId.Remove(id);
                    //repaint
                    tb.Invalidate();
                }
            }

        }

        private void gotoButton_DropDownOpening(object sender, EventArgs e)
        {
            gotoButton.DropDownItems.Clear();
            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;
                for (int i = 0; i < info.bookmarks.Count; i++)
                {
                    var item = gotoButton.DropDownItems.Add("Bookmark " + gotoButton.DropDownItems.Count + " [" + Path.GetFileNameWithoutExtension(tab.Tag as String) + "]");
                    item.Tag = new BookmarkInfo() { tb = tb, iBookmark = i };
                    item.Click += (o, a) => GoTo((BookmarkInfo)(o as ToolStripItem).Tag);
                }
            }
        }

        public class BookmarkInfo
        {
            public int iBookmark;
            public FastColoredTextBox tb;
        }

        private void GoTo(BookmarkInfo bookmark)
        {
            TbInfo info = bookmark.tb.Tag as TbInfo;

            try
            {
                CurrentTB = bookmark.tb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (bookmark.iBookmark < 0 || bookmark.iBookmark >= info.bookmarks.Count)
                return;

            int id = info.bookmarks[bookmark.iBookmark];
            BookmarkPointer = bookmark.iBookmark;
            for (int i = 0; i < CurrentTB.LinesCount; i++)
                if (CurrentTB[i].UniqueId == id)
                {
                    CurrentTB.Selection.Start = new Place(0, i);
                    CurrentTB.DoSelectionVisible();
                    CurrentTB.Invalidate();
                    break;
                }
        }

        private void GotoNextdownBookmark_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            TbInfo info = CurrentTB.Tag as TbInfo;
            if (info.bookmarks.Count == 0)
                return;
            if (BookmarkPointer < info.bookmarks.Count - 1)
            {
                BookmarkPointer = BookmarkPointer + 1;
            }
            else
            {
                BookmarkPointer = 0;
            }

            int id = info.bookmarks[BookmarkPointer];
            for (int i = 0; i < CurrentTB.LinesCount; i++)
            {
                if (CurrentTB[i].UniqueId == id)
                {
                    CurrentTB.Selection.Start = new Place(0, i);
                    CurrentTB.DoSelectionVisible();
                    CurrentTB.Invalidate();
                    break;
                }
            }

        }

        private void GotoNextUpBookmark_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
            {
                return;
            }
            TbInfo info = CurrentTB.Tag as TbInfo;
            if (info.bookmarks.Count == 0)
                return;
            BookmarkPointer = BookmarkPointer - 1;

            if (BookmarkPointer < 0)
            {
                BookmarkPointer = info.bookmarks.Count - 1;
            }
            int id = info.bookmarks[BookmarkPointer];
            for (int i = 0; i < CurrentTB.LinesCount; i++)
            {
                if (CurrentTB[i].UniqueId == id)
                {
                    CurrentTB.Selection.Start = new Place(0, i);
                    CurrentTB.DoSelectionVisible();
                    CurrentTB.Invalidate();
                    break;
                }
            }

        }



        #endregion

        private void ToLowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String aa = CurrentTB.Selection.Text.ToLower();
            // CurrentTB.Selection.Text.Remove()
            CurrentTB.InsertText(aa);
        }
        private void ToUpperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String aa = CurrentTB.Selection.Text.ToUpper();
            // CurrentTB.Selection.Text.Remove()
            CurrentTB.InsertText(aa);
        }

        public void CreateTab(string fileName)
        {
            try
            {
                var tb = new FastColoredTextBox();
                tb.AcceptsTab = true;
                // fs.propertyGrid.SelectedObject =tb;

                tb.ContextMenuStrip = FileEditorcontextMenuStrip;
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.LeftPadding = 17;


                if (MCU_TYPE == "CHC05" || MCU_TYPE == "CRISC")
                {
                    tb.Language = Language.CSharp;

                    //tb.TabLength = 4;

                }
                else if (MCU_TYPE == "RISC")
                {
                    tb.Language = Language.RISC;

                    // tb.TabLength = 8;
                }
                else
                {
                    tb.Language = Language.ASM;
                    //tb.TabLength = 8;
                }

                string path = frmMAIN.APPLICATION_PATH + "global.ini";
                string font_size = null;// = "12"; 

                XElement rootNode = XElement.Load(path);

                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectFont")
                                                select myTarget;
                foreach (XElement xnode in myvalue)
                {
                    font_size = xnode.Attribute("Size").Value;
                    if (font_size == "")
                    {
                        font_size = "12";
                        xnode.Attribute("Size").Value = font_size;
                    }
                    tb.Font = new Font("Courier New", Convert.ToInt16(font_size));
                    //tb.Font =Convert.ToInt16(font_size);
                }

                myvalue = from myTarget in rootNode.Descendants("ProjectTab")
                          select myTarget;
                foreach (XElement xnode in myvalue)
                {
                    font_size = xnode.Attribute("TSize").Value;
                    if (font_size == "")
                    {
                        font_size = "8";
                        xnode.Attribute("TSize").Value = font_size;
                    }

                    tb.TabLength = Convert.ToInt16(font_size);
                }

                tb.Cursor = System.Windows.Forms.Cursors.IBeam; //鼠标的型状设置
                tb.ImeMode = ImeMode.On; //允许中文输入

                //tb.Font = fs.ftext.Font;
                tb.LeftPadding = 30;
                tb.AddStyle(new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray))));//same words style
                var tab = new FATabStripItem(fileName != null ? Path.GetFileName(fileName) : "[new]", tb);
                tab.Tag = fileName;

                //control open only one file
                if (fileName != null)
                {
                    int tips = frmfile.tsFiles.Items.Count;
                    string tabName = tab.ToString();

                    for (int i = 0; i < tips; i++)
                    {
                        if (tabName == frmfile.tsFiles.Items[i].ToString())
                        {
                            return;
                        }
                    }
                }


                if (fileName != null)
                {
                    FileStream textLoad = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    if (textLoad.CanRead)
                    {
                        Encoding encoding = Encoding.Unicode;
                        encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fileName);
                        tb.Text = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);

                        textLoad.Close();

                        //tb.Text = File.ReadAllText(fileName);
                        recentFileHandler.Clear();
                        recentFileHandler.AddFile(fileName);

                        ASM_watcher.Path = Path.GetDirectoryName(fileName);
                        ASM_watcher.Filter = "*.asm"; //Path.GetFileName(fileName);
                        ASM_watcher.NotifyFilter = NotifyFilters.LastWrite;
                        ASM_watcher.Changed += new FileSystemEventHandler(ASM_onChanged);
                        //ASM_watcher.EnableRaisingEvents = true;
                    }
                    else
                    {
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText3);
                    }
                }

                tb.ClearUndo();
                tb.Tag = new TbInfo();
                tb.IsChanged = false;
                frmfile.tsFiles.AddTab(tab);
                frmfile.tsFiles.SelectedItem = tab;
                tb.Focus();
                tb.SelectionStart = 0;
                tb.DoCaretVisible();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 1000;
                tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.MouseLeave += new EventHandler(tb_MouseLeave);
                tb.TextChanged += new EventHandler<TextChangedEventArgs>(tb_TextChanged);
                tb.PaintLine += new EventHandler<PaintLineEventArgs>(tb_PaintLine);

                tb.MouseClick += new MouseEventHandler(tb_MouseClick);

                tb.ChangedLineColor = changedLineColor;
                //if (btHighlightCurrentLine.Checked)
                tb.CurrentLineColor = currentLineColor;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;

                //create autocomplete popup menu
                AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                popupMenu.Items.ImageList = ilAutocomplete;
                popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(popupMenu);
                (tb.Tag as TbInfo).popupMenu = popupMenu;

            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
        }



        private void frmMAIN_DeActivated(object sender, EventArgs e) //DEactivated
        {


            //int a = 0;
            //a = 1 + Convert.ToInt32(toolStripLabel1.Text);
            //toolStripLabel1.Text += "b";// a.ToString();
            try
            {
                if (ASM_watcher.Path.Length > 2)
                    ASM_watcher.EnableRaisingEvents = true;

                OldEditeFileList.Clear();
                for (int i = 0; i < frmfile.tsFiles.Items.Count; i++)
                {
                    if (frmfile.tsFiles.Items[i].Tag == null)
                    {
                        return;
                    }
                    string edt_pth = frmfile.tsFiles.Items[i].Tag.ToString();
                    FileInfo fi = new FileInfo(edt_pth);
                    string at = fi.LastWriteTime.ToString();
                    OldEditeFileList.Add(edt_pth + at);

                    //frmmessage.fastColoredTextBox.AppendText(at+"\n\r");  

                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText4 + ex.ToString(), ShowLanguage.Current.MessageBoxCaption4);
                return;
            }
        }
        private void LastWriterTimeCompare()
        {
            for (int i = 0; i < frmfile.tsFiles.Items.Count; i++)
            {
                string edt_pth = frmfile.tsFiles.Items[i].Tag.ToString();

                if (File.Exists(edt_pth) != true)
                {
                    return;
                }
                frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[i];

                var tb = (frmfile.tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);

                FileStream textLoad = new FileStream(edt_pth, FileMode.Open, FileAccess.Read);
                if (textLoad.CanRead)
                {
                    Encoding encoding = Encoding.Unicode;
                    encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(edt_pth);
                    tb.Text = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                    tb.IsChanged = false;
                    textLoad.Close();
                    //saveToolStripMenuItem_Click(null, null);
                    //tb.Text = File.ReadAllText(fileName);
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText3);
                }

            }


        }
        private void frmMAIN_Activated(object sender, EventArgs e) //DEactivated
        {

            //------compare with old time---------------
            try
            {
                NewEditeFileList.Clear();
                for (int i = 0; i < frmfile.tsFiles.Items.Count; i++)
                {
                    if (frmfile.tsFiles.Items[i].Tag == null)
                    {
                        return;
                    }
                    string edt_pth = frmfile.tsFiles.Items[i].Tag.ToString();
                    FileInfo fi = new FileInfo(edt_pth);
                    File.GetLastWriteTime(edt_pth).ToString();
                    string at = fi.LastWriteTime.ToString();
                    at = File.GetLastWriteTime(edt_pth).ToString();
                    NewEditeFileList.Add(edt_pth + at);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText4 + ex.ToString(), ShowLanguage.Current.MessageBoxCaption4);
                return;
            }

            for (int i = 0; i < OldEditeFileList.Count; i++)
            {
                //frmmessage.fastColoredTextBox.AppendText(OldEditeFileList[i] + "  " + NewEditeFileList[i]+"\n\r"); 

                if (OldEditeFileList[i] != NewEditeFileList[i])
                {
                    DialogResult dr = MessageBox.Show(ShowLanguage.Current.MessageBoxText5, ShowLanguage.Current.MessageBoxCaption5, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        LastWriterTimeCompare();
                    }

                    //OldEditeFileList.Clear();
                    //for (int j = 0; j < NewEditeFileList.Count;j++ )
                    //{
                    //    OldEditeFileList.Add(NewEditeFileList[j]);
                    //}

                    break;
                }
            }

            //--------------------------------
            //if (fileChangedFlag)
            //{
            //    fileChangedFlag = false;

            //    DialogResult dr=MessageBox.Show("当前文件已经被更改，是否加载新文件？","Attention",MessageBoxButtons.OKCancel,
            //        MessageBoxIcon.Question);
            //    if (dr==DialogResult.OK)
            //    {
            //        LastWriterTimeCompare();
            //    }
            //}
        }
        private void ASM_onChanged(object source, FileSystemEventArgs e)
        {
            //MessageBox.Show("当前文件已经被更改，是否加载新文件？\n"+e.ChangeType.ToString());
            //fileChangedFlag = true;
            ASM_watcher.EnableRaisingEvents = false;
        }

        void popupMenu_Opening(object sender, CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.GreenStyle);
            if (iGreenStyle >= 0)
                if (CurrentTB.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            //foreach (var item in snippets)
            //    items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            //foreach (var item in declarationSnippets)
            //    items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            //foreach (var item in methods)
            //    items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            //foreach (var item in keywords)
            //    items.Add(new AutocompleteItem(item) { ImageIndex=3});

            //items.Add(new InsertSpaceSnippet());
            // items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            popupMenu.Items.SetAutocompleteItems(new DynamicCollection(CurrentTB));
            //set as autocomplete source
            // popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
        }

        DateTime lastNavigatedDateTime = DateTime.Now;

        private bool NavigateBackward()
        {
            DateTime max = new DateTime();
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < frmfile.tsFiles.Items.Count; iTab++)
            {
                var t = (frmfile.tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit < lastNavigatedDateTime && t[i].LastVisit > max)
                    {
                        max = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                frmfile.tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                tb.Focus();
                return true;
            }
            else
                return false;
        }

        private bool NavigateForward()
        {
            DateTime min = DateTime.Now;
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < frmfile.tsFiles.Items.Count; iTab++)
            {
                var t = (frmfile.tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                    {
                        min = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                frmfile.tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                tb.Focus();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                //var pattern = Regex.Replace(fragmentText, FastColoredTextBoxNS.FindForm.RegexSpecSymbolsPattern, "\\$0");
                //if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                //    return CompareResult.Visible;
                //return CompareResult.Hidden;
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d*)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null)
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }

        #region tool tip
        void tb_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastMouseCoord != e.Location && DebugtoolStripButton.Checked)
            {
                //restart tooltip timer
                CancelToolTip();
                tm.Start();
            }
            lastMouseCoord = e.Location;
        }

        void CancelToolTip()
        {
            tm.Stop();
            tt.Hide(this);
        }

        private void tb_MouseLeave(object sender, EventArgs e)
        {
            CancelToolTip();
        }
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            CancelToolTip();
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            tm.Stop();
            //get place under mouse
            var place = CurrentTB.PointToPlace(lastMouseCoord);

            //check distance
            var p = CurrentTB.PlaceToPoint(place);
            if (Math.Abs(p.X - lastMouseCoord.X) > CurrentTB.CharWidth * 2 ||
                Math.Abs(p.Y - lastMouseCoord.Y) > CurrentTB.CharHeight * 2)
                return;
            //get word under mouse
            var r = new Range(CurrentTB, place, place);
            string hoverWord = r.GetFragment("[a-zA-Z0-9_]").Text;

            if (hoverWord == "")
                return;

            //load variable file when it's null
            if (gValiableFile == null)
            {
                string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
                string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);
                string fpath = project_path + "\\System\\" + project_name + ".var";
                if (File.Exists(fpath) != true)
                {
                    return;
                }
                FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
                gValiableFile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();

            }

            byte flag_bit = 0xff;
            bool equ_flag = false;
            string keyword_display = null;
            //find if keyword is a bit flag
            //@"#define+\s+[\w|(\[[^\]\])]+\s+((0x[A-Fa-f0-9]+\b)|([0-9]+\b)|([\w]+[\.]bitn[\.]bit[0-9]))"

            //if bit flag
            //Regex bitflag_key = new Regex(@"define+\s+" + hoverWord + @"\s+([\w]+[\.]bitn[\.]bit[0-9])", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Regex bitflag_key = new Regex(@"define+\s+" + hoverWord + @"\s+([\w]+[\,][0-9])", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Match bitflag_str = bitflag_key.Match(gValiableFile, 0);

            if (bitflag_str.Value == "")
            {
                bitflag_key = new Regex(@"define+\s+" + hoverWord + @"\s+((0x[A-Fa-f0-9]+\b)|([0-9]+\b))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                bitflag_str = bitflag_key.Match(gValiableFile, 0);
                if (bitflag_str.Value != "")
                {
                    equ_flag = true;
                    tt.ToolTipTitle = hoverWord;
                    keyword_display = bitflag_str.Value;
                }
            }
            else
            {
                tt.ToolTipTitle = hoverWord;
                keyword_display = hoverWord;
                string bit_name_str = bitflag_str.Value;

                //bitflag_key = new Regex(@"[\w]+[\.]bitn[\.]bit[0-9]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                bitflag_key = new Regex(@"[\w]+[\,][0-9]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                bitflag_str = bitflag_key.Match(bit_name_str, 0);

                //hoverWord = bitflag_str.Value.Substring(0, bitflag_str.Value.IndexOf("."));
                hoverWord = bitflag_str.Value.Substring(0, bitflag_str.Value.IndexOf(","));

                string bit_value = bitflag_str.Value.Substring(bitflag_str.Value.Length - 1, 1);

                flag_bit = Convert.ToByte(bit_value);

            }

            // globle & temp variable
            string buffer_value = null;
            string gaddr_str = null;
            if (equ_flag == false)
            {

                //temp variable: R_KEY1+\s+at+\s+0x[a-fA-F0-9]+\s+[\w]+[\.]+(h|c|asm)
                //[\w|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\s+[\w]+[\.]+(h|c|asm)+\s+[0-9]+\s+to+\s+[0-9]+\b
                string current_file_name = frmfile.tsFiles.SelectedItem.ToString();

                Regex var_key = new Regex(hoverWord + @"+\s+at+\s+0x[a-fA-F0-9]+\s+" + current_file_name + @"+\s+[0-9]+\s+to+\s+[0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match var_str = var_key.Match(gValiableFile, 0);

                int sub_file_index = 0;
                while (true)
                {
                    var_str = var_key.Match(gValiableFile, sub_file_index);

                    if (var_str.Value == "")
                    {
                        break;
                    }
                    else
                    {
                        string tValiableFile = var_str.Value;
                        Regex sub_var_key = new Regex(@"[0-9]+\s+to", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        Match sub_var_str = sub_var_key.Match(tValiableFile, 0);

                        int lineNume = place.iLine;
                        string str_min_line = sub_var_str.Value.Substring(0, sub_var_str.Value.Length - 2);
                        int min_line = Convert.ToInt32(str_min_line);
                        string str_max_line = tValiableFile.Substring(tValiableFile.LastIndexOf("to ") + 3, tValiableFile.Length - tValiableFile.LastIndexOf("to") - 3);
                        int max_line = Convert.ToInt32(str_max_line);

                        if ((min_line <= lineNume) && (lineNume <= max_line))
                            break;
                        else
                            sub_file_index = var_str.Index + var_str.Value.Length;

                    }

                }//while end

                if (var_str.Value != "") //a temp variable
                {
                    string temp_str = var_str.Value;
                    var_key = new Regex(@"0x[a-fA-F0-9]+\s+" + current_file_name, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    var_str = var_key.Match(temp_str, 0);
                    gaddr_str = var_str.Value.Substring(2, 4);
                    int gaddr = Convert.ToInt32(gaddr_str, 16);

                    if (gaddr <= RAMDataBuffer.Length) //ram max 0x200
                    {
                        if (flag_bit == 0xff)
                        {
                            buffer_value = RAMDataBuffer[gaddr].ToString("X2");
                        }
                        else
                        {
                            buffer_value = ((RAMDataBuffer[gaddr] >> flag_bit) & 0x01).ToString("X2");
                        }
                    }
                }
                else
                {
                    //find keyword from gValiableFile
                    //[\w|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\b
                    //globle variable: R_KEY1+\s+at+\s+0x[a-fA-F0-9]+\s+xxxx.x
                    var_key = new Regex(hoverWord + @"\s+at+\s+0x[a-fA-F0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                    var_str = var_key.Match(gValiableFile, 0);

                    if (var_str.Value == "")
                    {
                        gaddr_str = GetAddr(hoverWord);
                        int gaddr = Convert.ToInt32(gaddr_str, 16);
                        if (gaddr <= RAMDataBuffer.Length && gaddr >= 0)
                        {
                            if (flag_bit == 0xff)
                            {
                                buffer_value = RAMDataBuffer[gaddr].ToString("X2");
                            }
                            else
                            {
                                buffer_value = ((RAMDataBuffer[gaddr] >> flag_bit) & 0x01).ToString("X2");
                            }
                        }
                        //return;
                    }
                    else
                    {
                        string temp_str = var_str.Value;
                        var_key = new Regex(@"0x[a-fA-F0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        var_str = var_key.Match(temp_str, 0);
                        gaddr_str = var_str.Value.Substring(2, 4);
                        int gaddr = Convert.ToInt32(gaddr_str, 16);
                        if (gaddr <= RAMDataBuffer.Length)
                        {
                            if (flag_bit == 0xff)
                            {
                                buffer_value = RAMDataBuffer[gaddr].ToString("X2");
                            }
                            else
                            {
                                buffer_value = ((RAMDataBuffer[gaddr] >> flag_bit) & 0x01).ToString("X2");
                            }
                        }
                    }
                }

            }// if equ_flag==false

            //show tooltip
            // string text = "Help for aaaa" + hoverWord+" "+place.iLine.ToString();
            string text = null;
            if ((flag_bit == 0xff) && (equ_flag == false))
            {
                text = hoverWord + @" ( @ 0x" + gaddr_str + @" )=0x" + buffer_value;
                tt.ToolTipTitle = hoverWord;
            }
            else
            {
                string bittemp = "  bitn.bit" + flag_bit.ToString("D1");
                text = keyword_display + @" (  @ 0x" + gaddr_str + bittemp + @"  )=0x" + buffer_value;
            }

            if (equ_flag == true)
            {
                int temp_index = keyword_display.IndexOf("0x");
                if (temp_index >= 0)
                {
                    gaddr_str = keyword_display.Substring(temp_index, keyword_display.Length - temp_index);
                    int gaddr = Convert.ToInt32(gaddr_str.Substring(2, gaddr_str.Length - 2), 16);
                    buffer_value = RAMDataBuffer[gaddr].ToString("X2");
                    text = text = hoverWord + @" ( @ " + gaddr_str + @" )=0x" + buffer_value;
                }
            }
            //tt.ToolTipTitle = keyword_display;
            tt.SetToolTip(CurrentTB, text);
            tt.Show(text, CurrentTB, new Point(lastMouseCoord.X, lastMouseCoord.Y + CurrentTB.CharHeight));
        }

        private string GetAddr(string varname)
        {
            string srt = frmMAIN.APPLICATION_PATH + "global.ini";
            string path = null;
            XElement rootNode = XElement.Load(srt);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                path = xnode.Attribute("Ppath").Value;
            }

            rootNode.Save(srt);

            string project_path = Path.GetDirectoryName(path);
            string project_name = Path.GetFileNameWithoutExtension(path);
            string fpath = project_path + "\\System\\" + project_name + ".var";

            // proj_path = path; //save path

            string var_path = fpath;// path.Substring(0, path.LastIndexOf(".")) + ".var";
            string gaddr = null;

            if (File.Exists(var_path))
            {
                FileStream textLoad = new FileStream(var_path, FileMode.Open, FileAccess.Read);

                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(var_path);
                string var_file = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();
                Regex var_key = new Regex(@"\W" + varname + @"\s+at+\s+0x[a-fA-F0-9]+\s+xxxx.x", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                Match var_str = var_key.Match(var_file, 0);

                if (var_str.Value != "")
                {
                    string temp_str = var_str.Value;
                    var_key = new Regex(@"0x[a-fA-F0-9]+\s+xxxx.x", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    var_str = var_key.Match(temp_str, 0);
                    gaddr = var_str.Value.Substring(2, 4);
                }
                else
                {
                    string temp_str = varname;
                    var_key = new Regex(@"0x[a-fA-F0-9]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    var_str = var_key.Match(temp_str, 0);
                    if (var_str.Value != "")
                    {
                        temp_str = var_str.Value;
                        gaddr = temp_str.Substring(2, temp_str.Length - 2).PadLeft(4, '0');
                    }
                    else
                    {
                        var_key = new Regex(@"#define+\s+" + varname + @"\s+[^\n\r]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        var_str = var_key.Match(var_file, 0);
                        if (var_str.Value != "")
                        {
                            temp_str = var_str.Value;
                            int iPos = temp_str.IndexOf(varname);
                            temp_str = temp_str.Substring(iPos + varname.Length);
                            temp_str = temp_str.Trim();

                            gaddr = GetAddr(temp_str);
                        }
                        else
                            gaddr = null;
                    }
                }
            }
            return gaddr;
        }


        #endregion

        private void tb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < 25)
            {
                // bookmarkPlusButton_Click(null,null);
                breakpointPlusButton_Click(null, null);
            }
            //else if(e.X>25 && e.X<40)
            //{
            //    if (CurrentTB == null)
            //        return;
            //   // TbInfo info = CurrentTB.Tag as TbInfo;

            //    //get UniqueId of current line
            //    int id = CurrentTB[CurrentTB.Selection.Start.iLine].UniqueId;


            //}
        }
        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.OemMinus)
            {
                NavigateBackward();
                e.Handled = true;
            }

            if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.OemMinus)
            {
                NavigateForward();
                e.Handled = true;
            }

            if (e.KeyData == (Keys.K | Keys.Control))
            {
                try
                {
                    //forced show (MinFragmentLength will be ignored)
                    (CurrentTB.Tag as TbInfo).popupMenu.Show(true);
                    e.Handled = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("keydown menu error!" + ex.ToString());
                }

            }
        }

        void tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            //remember last visit time
            if (tb.Selection.Start == tb.Selection.End && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(tb.Styles[0]);
            if (tb.Selection.Start != tb.Selection.End)
                return;//user selected diapason
            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            //var ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b");//.ToArray();
            //if (ranges.Length > 1)
            //    foreach (var r in ranges)
            //        r.SetStyle(tb.Styles[0]);
        }

        void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            FastColoredTextBox tb = (sender as FastColoredTextBox);
            //rebuild object explorer
            string text = (sender as FastColoredTextBox).Text;
            //ThreadPool.QueueUserWorkItem(
            //    (o) => ReBuildObjectExplorer(text)
            //);

            //show invisible chars
            HighlightInvisibleChars(e.ChangedRange);
        }

        private void HighlightInvisibleChars(Range range)
        {
            //range.ClearStyle(invisibleCharsStyle);
            //if (btInvisibleChars.Checked)
            //    range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
        }

        FastColoredTextBox CurrentTB
        {
            get
            {
                if (frmfile.tsFiles.SelectedItem == null)
                    return null;
                return (frmfile.tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }

            set
            {
                frmfile.tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

        private void dockPanelMain_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void CheckForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update_check();
        }

        private void AboutSinoIDEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.Show();
        }

        private void NewProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewProject_Click();
            //string str = Mathfuntion.GetProjectPath();
            //frmexplorer.ProjectTreeBuild(str);
        }

        private void NewProject_Click()
        {
            NewProject_From frmNewProject = new NewProject_From();
            //frmNewProject.Show();
            frmNewProject.ShowDialog();
            if (frmNewProject.DialogResult == DialogResult.OK)
            {
                string str = Mathfuntion.GetProjectPath();

                //str = frmMAIN.APPLICATION_PATH + "global.ini";
                //XElement rootNode = XElement.Load(str);

                //IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                //                                select myTarget;
                //foreach (XElement xnode in myvalue)
                //{
                //    str = xnode.Attribute("Ppath").Value;
                //}

                if (File.Exists(str) == false)
                {
                    str = frmMAIN.APPLICATION_PATH + "Sample\\aaaa\aaaa.Proj";
                }
                ProjectPath = str;

                frmexplorer.ProjectTreeBuild(str);
                if (frmPC.IsDisposed == false)
                {
                    frmPC.OptionReflash();
                }

                //search mcu id
                XElement rootNode0 = XElement.Load(str);

                MCU_ID = rootNode0.Attribute("ID").Value;
                MCU_TYPE = rootNode0.Attribute("MCU_Type").Value;

                if (frmSys.IsDisposed == false)
                {
                    frmSys.Loadxml(MCU_ID);
                }
                if (frmREG.IsDisposed == false)
                {
                    frmREG.GetXmlNodeInformation(MCU_ID);
                }

                InitalDeviceConfig(MCU_ID);
                if (frmRAM.IsDisposed == false)
                {
                    frmRAM.RamInitial(DeviceConfigXX.MCU_RAMSize);
                }

            }

        }

        private void frmMAIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            // MessageBox.Show("是否保存当前项目");
            dockPanelMain.SaveAsXml(Path.Combine(Application.StartupPath, "CustomUI.xml"));
            //this.Close();
        }

        private void DebugCommandfor7011()
        {
            if (MCU_ID == "0x7011" || MCU_ID == "0x7031")
            {
                byte[] CMD_ReadCPU = { 0x69, 0x03, 0x07, 0x00, 0x01, 0x02, 0x00, 0x20, 0x00, 0x30 };
                if (MCU_TYPE == "RISC")
                {
                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(CMD_ReadCPU);
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Read sysPC error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                        int sysPC = (ReadDataBuffer[0x00] + ReadDataBuffer[0x01] * 0x100) * 2;
                        string pcValue = sysPC.ToString("X4");

                        byte[] MCU_ReadSRom = { 0x69, 0x03, 0x07, 0x00, 0x80, 0x00, 0x00, 0x02, 0x00, 0xbc };
                        MCU_ReadSRom[5] = (byte)(sysPC / 0x100);
                        MCU_ReadSRom[6] = (byte)(sysPC % 0x100);

                        COMPORT.SendCommand(MCU_ReadSRom);
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < 2)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Read Rom error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                        int iRom = ReadDataBuffer[0x00] + ReadDataBuffer[0x01] * 0x100;
                        string RomValue = iRom.ToString("X4");
                        if ((ReadDataBuffer[0x01] & 0xf8) == 0x50 || (ReadDataBuffer[0x01] & 0xf8) == 0x58 ||
                            (ReadDataBuffer[0x01] & 0xff) == 0x07 || (ReadDataBuffer[0x01] & 0xff) == 0x15 ||
                            (ReadDataBuffer[0x01] & 0xff) == 0x16 || (ReadDataBuffer[0x01] & 0xff) == 0x25 ||
                            (ReadDataBuffer[0x01] & 0xff) == 0x26)
                        {
                            byte[] MCU_ReadSRam = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0xbc };
                            MCU_ReadSRam[6] = (byte)(ReadDataBuffer[0x00] - 1);
                            if (ReadDataBuffer[0x00] == 0xe7)
                            {
                                byte[] MCU_ReadRam = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x00, 0x83, 0x02, 0x00, 0xbc };
                                COMPORT.SendCommand(MCU_ReadRam);
                                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                                DataReceiveTimer.Enabled = true;
                                TimeOutFlag = 0;
                                while (COMPORT.port1.BytesToRead < 1)
                                {
                                    if (TimeOutFlag == 0xaa)
                                    {
                                        DataReceiveTimer.Enabled = false;
                                        break;
                                    }
                                }
                                DataReceiveTimer.Enabled = false;
                                if (TimeOutFlag == 0xaa)
                                {
                                    CloseRS232();
                                    MessageBox.Show("Read Ram FSR error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                                    return;
                                }
                                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                                MCU_ReadSRam[5] = ReadDataBuffer[0x01];
                                MCU_ReadSRam[6] = (byte)(ReadDataBuffer[0x00] - 1);
                            }
                            COMPORT.SendCommand(MCU_ReadSRam);
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;
                            while (COMPORT.port1.BytesToRead < 1)
                            {
                                if (TimeOutFlag == 0xaa)
                                {
                                    DataReceiveTimer.Enabled = false;
                                    break;
                                }
                            }
                            DataReceiveTimer.Enabled = false;
                            if (TimeOutFlag == 0xaa)
                            {
                                CloseRS232();
                                MessageBox.Show("Read SRam error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                                return;
                            }
                            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                        }
                    }
                }
            }
        }

        private void ReadSP_Value()
        {
            byte[] cmd_readAX = { 0x69, 0x03, 0x07, 0x00, 0x01, 0x01, 0x00, 0x02, 0x00, 0x30 };
            if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")
            {
                cmd_readAX[7] = 0x20;
            }
            //COMPORT.OpenPort();
            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(cmd_readAX);
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < cmd_readAX[7])
                {
                    if (TimeOutFlag == 0xaa)
                    {
                        DataReceiveTimer.Enabled = false;
                        // ReadDataBuffer[2] = 0xf2;
                        break;
                    }
                }
                DataReceiveTimer.Enabled = false;
                if (TimeOutFlag == 0xaa)
                {
                    CloseRS232();
                    MessageBox.Show("Read Reg SP and A error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                len=len;
            }
        }

        private void WriteSP_Value(byte SP_Value1)
        {
            byte[] MCU_REGbyte = { 0x69, 0x14, 6, 0x00, 0x01, 0x01, 0x01, 0x00, 0xbc };
            MCU_REGbyte[7] = SP_Value1;
            if (frmMAIN.DebugFlag == false)
            {
                return;
            }
            if (frmMAIN.COMPORT.port1.IsOpen)
            {
                frmMAIN.COMPORT.SendCommand(MCU_REGbyte);//set break point
            }
            else
            {
                return;
            }
        }

        private void DebugCommand(string tag)
        {
            byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x19 };

            GotoolStripButton.Checked = false;
            GoNBKtoolStripButton.Checked = false;

            stw_time.Reset();
            stw_time.Start();

            switch (tag)
            {
                case "Reset":
                    CMDdata[3] = DEBUG_COMMAND.CMD_Reset;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    break;
                case "Go":
                    DebugCommandfor7011();
                    CMDdata[3] = DEBUG_COMMAND.CMD_Run;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    GotoolStripButton.Checked = true;
                    break;
                case "GoNBK":
                    if (MCU_TYPE == "RISC" || MCU_TYPE == "CRISC")
                    {
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText6);
                        return;
                    }
                    DebugCommandfor7011();
                    CMDdata[3] = 0xe6; // DEBUG_COMMAND.CMD_Run;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    GoNBKtoolStripButton.Checked = true;
                    GotoolStripButton.Checked = true;
                    break;
                case "Stop":
                    CMDdata[3] = DEBUG_COMMAND.CMD_Stop;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    break;
                case "StepInto":
                    DebugCommandfor7011();
                    CMDdata[3] = DEBUG_COMMAND.CMD_StepInto;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    break;
                case "StepOver":
                    DebugCommandfor7011();
                    CMDdata[3] = DEBUG_COMMAND.CMD_StepOver;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    break;
                case "StepOut":
                    //                     string sp=frmSys.propertyGridEx_sys.Item[1].Value.ToString();
                    //                     if (sp=="0xFF" || sp=="0x00")
                    //                     {
                    //                         CMDdata[3] = DEBUG_COMMAND.CMD_StepOver;
                    //                         CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    //                     } 
                    //                     else
                    {
                        DebugCommandfor7011();
                        CMDdata[3] = DEBUG_COMMAND.CMD_StepOut;
                        CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    }
                    break;
                default:
                    break; ;

            }

            DebugButtonDisplayContral();
            if (DebugFlag)
            {

                // if (((MCU_TYPE!="RISC")||(MCU_TYPE!="CRISC"))&&(COMPORT.port1.IsOpen)) //must read id when start debug
                if (COMPORT.port1.IsOpen) //must read id when start debug
                {
                    byte[] CMD_Read2Byte = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
                    //if(MCU_ID=="0x0311"||MCU_ID=="0301"|| MCU_ID=="0x0224")
                    //{

                    //}
                    if (MCU_ID == "0x3378" || MCU_ID == "0x3394" || MCU_ID == "0x3111" || MCU_ID == "0x3316"
                        || MCU_ID == "0x5312" || MCU_ID == "0x7212" || MCU_ID == "0x7020"
                        || MCU_ID == "0x6060" || MCU_ID == "0x7510" || MCU_ID == "0x3222"
                        || MCU_ID == "0x8132" || MCU_ID == "0x7311" || MCU_ID == "0x7011"
                        || MCU_ID == "0x7511" || MCU_ID == "0x5222" || MCU_ID == "0x7323"
                        || MCU_ID == "0x7031" || MCU_ID == "0x6080" || MCU_ID == "0x6220" || MCU_ID == "0x7321"
                        || MCU_ID == "0x7312" || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722"
                        || MCU_ID == "0x9902" || MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")// 
                    {
                        CMD_Read2Byte[4] = 0x7f;
                        CMD_Read2Byte[5] = 0xff;
                        CMD_Read2Byte[6] = 0xfe;
                    }

                    COMPORT.SendCommand(CMD_Read2Byte);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 2)
                    {

                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMDdata);
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 4)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }
                else
                {
                    CloseRS232();
                    return;
                }

                if (CMDdata[3] != DEBUG_COMMAND.CMD_Run)
                {
                    if (CMDdata[3] != DEBUG_COMMAND.CMD_GoNBK) //go no care breakpoint  66ms
                    {
                        ReadCPU();
                    }
                }
                else //start timer and test busy signal
                {
                    //DebugtoolStripButton.Enabled = false;
                    GotoolStripButton.Enabled = false;
                    StepIntotoolStripButton.Enabled = false;
                    StepOvertoolStripButton.Enabled = false;
                    StepOuttoolStripButton.Enabled = false;
                    busy_Timer.Interval = 130; //5ms interrupt 
                    busy_Timer.Tick += new EventHandler(busy_Timer_Tick);
                    busy_Timer.Enabled = true;
                    interrupt_flag = false;
                }

            }

            stw_time.Stop();
            StopWatch_lab.Text = ShowLanguage.Current.ProgramRunTime + ":" + stw_time.ElapsedMilliseconds.ToString();

        }

        private void busy_Timer_Tick(object sender, EventArgs e)
        {
            busy_Timer.Enabled = false;

            byte[] CMD_ReadBusyFlag = { 0x69, 0x04, 0x00, 0x00, 0x00 };

            if (interrupt_flag == true) //control timer interrupt 
            {
                return;
            }
            //stw_time.Reset();
            //stw_time.Start();
            //lock(this)
            //{
            //if (COMPORT.port1.IsOpen == false)
            //{
            //    COMPORT.OpenPort();
            //}
            try
            {
                // COMPORT.OpenPort();
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMD_ReadBusyFlag);
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 2)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }

                }
                int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                 if (Stop_stat == 0)
                 {
                     obj = new FrmStopMess();
                 }
                if (ReadDataBuffer[0] == 1)
                {
                    SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": Busy!";

                    if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")//STOP唤醒提醒功能
                    {
                        if ((Stop_stat == 1)&&(ReadDataBuffer[1] == 0)) 
                        {
                            StoptoolStripButton.Enabled = true;
                            Stop_stat = 0;
                            obj.Close();
                        }
                    } 
                    
                    //SNLinkStatusDisplay.Image=
                    if ((ReadDataBuffer[1] == 1) && (MCU_TYPE == "RISC"))
                    {
                        SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": Stop!";

                        if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")
                        {
                            if (Stop_stat == 0)
                            {
                                StoptoolStripButton.Enabled = false;
                                Stop_stat = 1;
                                obj.Show();
                            }
                        }
                    }
                    else if (MCU_TYPE == "AHC05")
                    {
                        CMD_ReadBusyFlag[1] = 0x13;
                        CMD_ReadBusyFlag[2] = 0x02;
                        CMD_ReadBusyFlag[3] = 0x91;
                        CMD_ReadBusyFlag[4] = 0x00;

                        //ResetCPUtoolStripButton.Enabled = false;
                        StepIntotoolStripButton.Enabled = false;
                        StepOvertoolStripButton.Enabled = false;
                        StepOuttoolStripButton.Enabled = false;
                        GotoolStripButton.Enabled = false;
                        RunToCursortoolStripButton.Enabled = false;
                        //StoptoolStripButton.Enabled = false;
                        GoNBKtoolStripButton.Enabled = false;

                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(CMD_ReadBusyFlag);
                            //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;

                            while (COMPORT.port1.BytesToRead < 2)
                            {
                                if (TimeOutFlag == 0xaa)
                                {
                                    DataReceiveTimer.Enabled = false;
                                    //ReadDataBuffer[3] = 0;
                                    break;
                                }
                            }
                            DataReceiveTimer.Enabled = false;
                            if (TimeOutFlag == 0xaa)
                            {
                                CloseRS232();
                                MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                                return;
                            }
                            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        }


                        switch (ReadDataBuffer[1] & 0x07)
                        {
                            case 0x04:
                                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": STOP" + ShowLanguage.Current.State;
                                break;
                            case 0x02:
                                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": WAIT";
                                break;
                            case 0x01:
                                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": RST";
                                break;
                            default:
                                StoptoolStripButton.Enabled = true;
                                string temp_value = ReadDataBuffer[1].ToString("X2");
                                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ": Run " + ShowLanguage.Current.State + @"：" + temp_value;
                                break;
                        }

                    }
                    busy_Timer.Enabled = Enabled;
                }
                else
                {
                    busy_Timer.Enabled = false;
                    SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + ":" + ShowLanguage.Current.SimulationDebugging + "!";
                    if ((Stop_stat == 1) && (ReadDataBuffer[1] == 0))
                    {
                        StoptoolStripButton.Enabled = true;
                        Stop_stat = 0;
                        obj.Close();
                    }
                    ReadCPU();
                    //DebugtoolStripButton.Enabled = true;
                    GotoolStripButton.Enabled = true;
                    StepIntotoolStripButton.Enabled = true;
                    StepOvertoolStripButton.Enabled = true;
                    StepOuttoolStripButton.Enabled = true;
                    GotoolStripButton.Checked = false;
                    GoNBKtoolStripButton.Checked = false;
                    DebugButtonDisplayContral();
                    interrupt_flag = true;

                }

            }
            catch //(System.Exception ex)
            {
                //COMPORT.ClosePort();
                //MessageBox.Show("通信端口出错" + ex.ToString());
            }
            finally
            {
                //if (COMPORT.port1.IsOpen == true)
                //{
                //    COMPORT.ClosePort();
                //}
                // stw_time.Stop();
                // StopWatch_lab.Text = "程序运行时间:" + stw_time.ElapsedMilliseconds.ToString();
            }

            // }

        }
        private void DebugCommandMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (System.Windows.Forms.ToolStripMenuItem)sender;
            if (tm.Tag == null)
                return;

            DebugCommand(tm.Tag.ToString());
            if (tm.Tag.ToString() == "Go")
            {
                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")//7333在复位后会清除断点断点，其他型号都会保留
                {
                    BreakPointCheck();
                    BreakPointCheck();
                }

            }    
        }

        private void DebugCommand_Click(object sender, EventArgs e)
        {
            ToolStripButton tm = (System.Windows.Forms.ToolStripButton)sender;
            if (tm.Tag == null)
                return;

            /*ReadSP_Value();
            //sp pointer

                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")
                {
                    while (frmMAIN.SP_Value != ReadDataBuffer[0x02])
                    {
                        WriteSP_Value(ReadDataBuffer[0x02]);
                        ReadSP_Value();
                    }
                }
                else
                {
                    while (frmMAIN.SP_Value != ReadDataBuffer[0x01])
                    {
                        WriteSP_Value(ReadDataBuffer[0x01]);
                        ReadSP_Value();
                    }
                }*/

            DebugCommand(tm.Tag.ToString());
            if (tm.Tag.ToString() == "Go")
            {
                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")//7333在复位后会清除断点断点，其他型号都会保留
                {
                    BreakPointCheck();
                    BreakPointCheck();
                }

            }
        }
        /// <summary>
        /// ReadCPU,ReadRAM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadCPU()
        {

            byte[] CMD_ReadChipState = { 0x69, 0x04, 0x00, 0x00, 0x00, 0x00 };

            // {busy,sleep}
            //  bit7 , bit6  bit5   bit4       bit3     bit2  bit1    bit0
            // {runi , run , step , stepover,stepout , STOP , WAIT ,   RST }
            //COMPORT.OpenPort();
            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(CMD_ReadChipState);
                //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 2)
                {
                    if (TimeOutFlag == 0xaa)
                    {
                        DataReceiveTimer.Enabled = false;
                        //ReadDataBuffer[3] = 0;
                        break;
                    }
                }
                DataReceiveTimer.Enabled = false;
                if (TimeOutFlag == 0xaa)
                {
                    CloseRS232();
                    MessageBox.Show("Read chip state error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

            if (MCU_TYPE == "RISC" || MCU_TYPE == "CRISC")
            {
                if (ReadDataBuffer[0] == 0x01 || ReadDataBuffer[1] == 0x01)
                {
                    //DebugtoolStripButton.Enabled = false;
                    GotoolStripButton.Enabled = false;
                    StepIntotoolStripButton.Enabled = false;
                    StepOvertoolStripButton.Enabled = false;
                    StepOuttoolStripButton.Enabled = false;
                    busy_Timer.Interval = 20; //50ms interrupt 
                    busy_Timer.Tick += new EventHandler(busy_Timer_Tick);
                    busy_Timer.Enabled = true;
                    interrupt_flag = false;
                    return;
                }
                else
                {
                    //System.Threading.Thread.Sleep(10);
                    //if (MCU_ID=="0x3394"||MCU_ID=="0x3221")
                    if (MCU_ID == "0x0311" || MCU_ID == "0x0301")
                    {
                        RISC_ReadCPU();
                    }
                    else
                    {
                        RISC_ReadCPU_3394();
                    }
                }

            }
            else //if(MCU_TYPE=="AHC05")
            {
                if (ReadDataBuffer[0] == 0x01)
                {
                    CMD_ReadChipState[1] = 0x13;
                    CMD_ReadChipState[2] = 0x02;
                    CMD_ReadChipState[3] = 0x91;
                    CMD_ReadChipState[4] = 0x00;
                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(CMD_ReadChipState);
                        //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < 2)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                //ReadDataBuffer[3] = 0;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                    }

                    if (ReadDataBuffer[1] != 0x00)
                    {
                        busy_Timer.Interval = 20; //50ms interrupt 
                        busy_Timer.Tick += new EventHandler(busy_Timer_Tick);
                        busy_Timer.Enabled = true;
                        interrupt_flag = false;
                        return;
                    }
                    else
                    {
                        if (MCU_TYPE == "AHC05")
                            AHC05_ReadCPU();
                        else
                            CHC05_ReadCPU();
                    }
                }
                else //67ms
                {
                    if (MCU_TYPE == "AHC05")
                        AHC05_ReadCPU();
                    else
                        CHC05_ReadCPU();
                }
            }

        }

        private void CloseRS232()
        {
            DebugtoolStripButton.Checked = false;
            DebugFlag = false;
            SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
            DebugButtonDisplayContral();
            if (COMPORT.port1.IsOpen == true)
            {
                COMPORT.ClosePort();
            }

        }
        private void DebugtoolStripButton_Click(object sender, EventArgs e)
        {
            //checked debug button
            if (DebugtoolStripButton.Checked == false)
            {
                DebugtoolStripButton.Checked = true;
                DebugFlag = true;
                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.SimulationDebugging;

                DebugButtonDisplayContral();
                gValiableFile = null;
            }
            else
            {
                DebugtoolStripButton.Checked = false;
                DebugFlag = false;
                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
                byte[] mcu_reset1 = { 0x69, 0x12 };
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(mcu_reset1);
                }
                DebugButtonDisplayContral();
                if (COMPORT.port1.IsOpen == true)
                {
                    COMPORT.ClosePort();
                }
                return;
            }
            //initial com port
            string comName = COMPORT.GetComNume();
            //COMPORT.InitCOM();
            if (comName == null)
            {
                MessageBox.Show("COM Port can not find!");
                DebugtoolStripButton.Checked = false;
                DebugFlag = false;
                SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
                DebugButtonDisplayContral();
                //if (COMPORT.port1.IsOpen == true)
                //{
                //    COMPORT.ClosePort();
                //}
                return;
            }

            string filename = null;

            //get vision 
            //current vision v01.03
            //Data:12.11.20
            byte[] read_firware_vision = { 0x69, 0x45 };

            COMPORT.OpenPort(); //globle open port command

            int len = 0;

            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(read_firware_vision); //reset mcu
                //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;

                while (COMPORT.port1.BytesToRead < 8)
                {
                    if (TimeOutFlag == 0xaa)
                    {
                        DataReceiveTimer.Enabled = false;
                        //ReadDataBuffer[3] = 0;
                        break;
                    }
                }
                DataReceiveTimer.Enabled = false;
                if (TimeOutFlag == 0xaa)
                {
                    CloseRS232();
                    MessageBox.Show("Send fireware vision read error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            }

            //#if debug_RS232
            //    COMPORT.ClosePort();
            //#endif

            //             if ((ReadDataBuffer[3] != 1) || (ReadDataBuffer[4] != 0x04))
            //             {
            //                 MessageBox.Show(ShowLanguage.Current.MessageBoxText8);
            //                 DebugtoolStripButton.Checked = false;
            //                 DebugFlag = false;
            //                 SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
            //                 DebugButtonDisplayContral();
            //                 if (COMPORT.port1.IsOpen == true)
            //                 {
            //                     COMPORT.ClosePort();
            //                 }
            //                 return;
            //             }hide by wj 20150416



            byte[] mcu_reset = { 0x69, 0x12 };
            //#if debug_RS232
            //            COMPORT.OpenPort();
            //            //COMPORT.ClosePort();
            //#endif
            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(mcu_reset); //reset mcu
                //                 DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                 DataReceiveTimer.Enabled = true;
                //                 TimeOutFlag = 0;
                // 
                //                 while (COMPORT.port1.BytesToRead < 5)
                //                 {
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         DataReceiveTimer.Enabled = false;
                //                         //ReadDataBuffer[3] = 0;
                //                         break;
                //                     }
                //                 }
                //                 DataReceiveTimer.Enabled = false;
                //                 if (TimeOutFlag == 0xaa)
                //                 {
                //                     CloseRS232();
                //                     MessageBox.Show("Send reset error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                         MessageBoxDefaultButton.Button1);
                //                     return;
                //                 }
                //                 len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            }

            //#if debug_RS232
            //            //COMPORT.OpenPort();
            //            COMPORT.ClosePort();
            //#endif

            System.Threading.Thread.Sleep(10);

            if (MCU_TYPE == "CHC05")
            {
                filename = ProjectPath/*GetProjectPath()*/; // g:\\aaa.proj
                //string str = Path.GetFileNameWithoutExtension(filename) + ".s19";

                filename = filename.Substring(0, filename.LastIndexOf(".")) + ".s19";

                //#if debug_RS232
                //                COMPORT.OpenPort();
                //                //COMPORT.ClosePort();
                //#endif
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(mcu_reset); //reset mcu
                }
                // COMPORT.ClosePort();
                System.Threading.Thread.Sleep(15);



                //MCU_OPTION = { 0x69, 0x11, 7, 0x91, 0x00, 0x75, 0xfc, 0xff, 0xff, 0xbc };
                byte[] set_option = { 0x69, 0x14, 6, 0x00, 0x00, 0x91, 0x01, 0x75, 0xfc };

                //OPTION = 0xf7;
                set_option[7] = (byte)(OPTION % 0x100);
                set_option[8] = (byte)(OPTION / 0x100);

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_option); //set mcu option
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                //set clk
                byte[] set_clk = { 0x69, 0x06, 0x01, 0x08 };
                set_clk[3] = frmMAIN.FREQ;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_clk);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 3)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            // ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                MCU_Breakpoint[3] = 0x00;
                MCU_Breakpoint[4] = 0x00;
                MCU_Breakpoint[5] = 0x8f;
                MCU_Breakpoint[6] = 0x00;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCU_Breakpoint);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;

                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                //#if debug_RS232
                //                //COMPORT.OpenPort();
                //                COMPORT.ClosePort();
                //#endif
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     byte[] CMD_Read2Byte = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
                // 
                //                     COMPORT.SendCommand(CMD_Read2Byte);
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                //                     while (COMPORT.port1.BytesToRead < 2)
                //                     {
                // 
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }
                // 
                //                 byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x1a };
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     COMPORT.SendCommand(CMDdata);
                //                     //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                // 
                //                     while (COMPORT.port1.BytesToRead < 4)
                //                     {
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }
                // 
                //                 byte[] CMD_ReadChipState = { 0x69, 0x04, 0x00, 0x00, 0x00, 0x00 };
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                //                     while (TimeOutFlag != 0xaa)
                //                     {
                //                         COMPORT.SendCommand(CMD_ReadChipState);
                //                         len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                         if (ReadDataBuffer[0] == 0x00 && ReadDataBuffer[1] == 0x00)
                //                         {
                //                             TimeOutFlag = 0xaa;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (ReadDataBuffer[0] == 0x01 || ReadDataBuffer[1] == 0x01)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show("chip state busy!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                 }

                Loads19(filename);

                load_chc05_codfile();

                DebugInitial();
                //BreakPointCheck();

                DebugCommand("Reset");
            }
            else if (MCU_TYPE == "AHC05")
            {

                filename = ProjectPath/*GetProjectPath()*/; // g:\\aaa.proj
                //string str = Path.GetFileNameWithoutExtension(filename) + ".s19";

                filename = filename.Substring(0, filename.LastIndexOf(".")) + ".s19";


                //#if debug_RS232
                //                COMPORT.OpenPort();
                //                //COMPORT.ClosePort();
                //#endif
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(mcu_reset); //reset mcu
                }
                System.Threading.Thread.Sleep(15);
                // COMPORT.ClosePort();

                byte[] set_option = { 0x69, 0x14, 6, 0x00, 0x00, 0x91, 0x01, 0x75, 0xfc };

                //OPTION = 0xf7;
                set_option[7] = (byte)(OPTION % 0x100);
                set_option[8] = (byte)(OPTION / 0x100);

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_option); //set mcu option

                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }



                //set clk
                byte[] set_clk = { 0x69, 0x06, 0x01, 0x08 };
                //frmMAIN.FREQ = 0x16;
                set_clk[3] = frmMAIN.FREQ;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_clk);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 3)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }



                MCU_Breakpoint[3] = 0x00;
                MCU_Breakpoint[4] = 0x00;
                MCU_Breakpoint[5] = 0x8f;
                MCU_Breakpoint[6] = 0x00;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCU_Breakpoint);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                // COMPORT.ClosePort();
                //#if debug_RS232
                //                //COMPORT.OpenPort();
                //                COMPORT.ClosePort();
                //#endif
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     byte[] CMD_Read2Byte = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
                // 
                //                     COMPORT.SendCommand(CMD_Read2Byte);
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                //                     while (COMPORT.port1.BytesToRead < 2)
                //                     {
                // 
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }
                // 
                //                 byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x1a };
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     COMPORT.SendCommand(CMDdata);
                //                     //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                // 
                //                     while (COMPORT.port1.BytesToRead < 4)
                //                     {
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }
                // 
                //                 byte[] CMD_ReadChipState = { 0x69, 0x04, 0x00, 0x00, 0x00, 0x00 };
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                //                     while (TimeOutFlag != 0xaa)
                //                     {
                //                         COMPORT.SendCommand(CMD_ReadChipState);
                //                         len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                         if (ReadDataBuffer[0] == 0x00 && ReadDataBuffer[1] == 0x00)
                //                         {
                //                             TimeOutFlag = 0xaa;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (ReadDataBuffer[0] == 0x01 || ReadDataBuffer[1] == 0x01)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show("chip state busy!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                 }

                Loads19(filename);

                load_hc05_listfile();

                DebugInitial();
                BreakPointCheck();

                DebugCommand("Reset");
            }
            else if (MCU_TYPE == "RISC")
            {
                //string filename = "G:\\Emulator\\IDE\\SinoSunIDE\\SinoSunIDE\\bin\\Debug\\Sample\\aaaa\\aaaa.s19";
                //reflash option display

                filename = ProjectPath/*GetProjectPath()*/; // g:\\aaa.proj
                filename = filename.Substring(0, filename.LastIndexOf("\\")) + "\\Output";
                filename = bsGetFiles.GetFiles(new DirectoryInfo(filename), "*.s19");

                //COMPORT.OpenPort();
                //#if debug_RS232
                //                COMPORT.OpenPort();
                //                //COMPORT.ClosePort();
                //#endif
                byte[] MCUr_OPTION = { 0x69, 0x14, 21, 0x00, 0x00, 0x91, 0x00, 0x75, 0xfc, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
                if (MCU_ID == "0x0314" || MCU_ID == "0x3221" || MCU_ID == "0x32821" || MCU_ID == "0x3220" || MCU_ID == "0x3394" || (MCU_ID == "0x3111") || (MCU_ID == "0x3378")
                    )
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7311" || MCU_ID == "0x7321")// TODO wj
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    //MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7022")// TODO wj
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3316")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);

                    MCUr_OPTION[14] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[13] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[10] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[9] = (byte)(temp_option % 0x100);
                    // 
                    // 
                    //                     MCUr_OPTION[13] = 0xff;
                    //                     MCUr_OPTION[14] = 0xf8;


                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x5312")
                {
                    //frmMAIN.OPTION = Convert.ToUInt32(frmPC.optionValue.Text, 16);
                    //verify test
                    //                     if (frmPC.optionValue0.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                    //                         MCUr_OPTION[8] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[7] = (byte)(opt % 0x100);
                    //                     }
                    //                     if (frmPC.optionValue1.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                    //                         MCUr_OPTION[10] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[9] = (byte)(opt % 0x100);
                    //                     }
                    //                     if (frmPC.optionValue2.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                    //                         MCUr_OPTION[12] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[11] = (byte)(opt % 0x100);
                    //                     }
                    //                     if (frmPC.optionValue3.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                    //                         MCUr_OPTION[14] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[13] = (byte)(opt % 0x100);
                    //                     }
                    //                     if (frmPC.optionValue4.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                    //                         MCUr_OPTION[16] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[15] = (byte)(opt % 0x100);
                    //                     }
                    //                     if (frmPC.optionValue5.Text != "")
                    //                     {
                    //                         UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                    //                         MCUr_OPTION[18] = (byte)(opt / 0x100);
                    //                         MCUr_OPTION[17] = (byte)(opt % 0x100);
                    //                     }



                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;

                }
                else if (MCU_ID == "0x7212")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }



                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x9902")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }



                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7333")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    /*
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    */
                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;
                    

                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }

                }
                else if (MCU_ID == "0x9903")//add for 9903 from 7333
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    /*
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    */
                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;


                    for (int i = 7; i < 19; i++)        //modify by LYL 7333 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }

                }
                else if (MCU_ID == "0x9904")//add for 9904 from 7333
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    /*
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    */
                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;


                    for (int i = 7; i < 19; i++)        //modify by LYL 7333 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }

                }
                else if (MCU_ID == "0x7122")        //add for 7122 from 7333
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    /*
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    */
                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;


                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }

                }
                else if (MCU_ID == "0x3401")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3264")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;

                }
                else if ((MCU_ID == "0x0301") || (MCU_ID == "0x0311"))
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x91;
                    MCUr_OPTION[6] = 0x00;
                }
                else if (MCU_ID == "0x6060")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7510")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3222")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x8132")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);
                    MCUr_OPTION[16] = 0;
                    MCUr_OPTION[15] = 0;

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue6.Text != "")// modify by lyl 1128
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue6.Text, 16);
                        MCUr_OPTION[20] = (byte)(opt / 0x100);
                        MCUr_OPTION[19] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue7.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue7.Text, 16);
                        MCUr_OPTION[22] = (byte)(opt / 0x100);
                        MCUr_OPTION[21] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xe0;

                }
                else if (MCU_ID == "0x7011" || MCU_ID == "0x7031")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7511")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x5222")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7323" || MCU_ID == "0x7312")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x6080")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x6220")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }


                //byte[] aaa_OPTION = { 0x69, 0x14,9,0x00,0x7f,0xff, 0xf0, 0x00, 0x00, 0xff,0xff,0xbc };
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCUr_OPTION); //set mcu option
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show("Send option error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                    byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x7f, 0xff, 0xf0, 0x10, 0x00, 0x65 };
                    if ((MCU_ID == "0x0301") || (MCU_ID == "0x0311"))
                    {
                        CMD_ReadOption[4] = 0x00;
                        CMD_ReadOption[5] = 0x91;
                        CMD_ReadOption[6] = 0x00;
                    }
                    COMPORT.SendCommand(CMD_ReadOption);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 4)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show("Send option read error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                //set clk
                byte[] set_clk8m = { 0x69, 0x06, 0x01, 0x08 };
                set_clk8m[3] = frmMAIN.FREQ;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_clk8m);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 3)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show("Send set clk error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                if (MCU_ID == "0x0301" || MCU_ID == "0x0311")
                {
                    MCU_Breakpoint[3] = 0x00;
                    MCU_Breakpoint[4] = 0x00;
                    MCU_Breakpoint[5] = 0x8f;
                    MCU_Breakpoint[6] = 0x00;
                }
                else //if (MCU_ID=="0x3221"||MCU_ID=="0x0314")
                {
                    MCU_Breakpoint[3] = 0x00;
                    MCU_Breakpoint[4] = 0x01;
                    MCU_Breakpoint[5] = 0x00;
                    MCU_Breakpoint[6] = 0x00;
                }



                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCU_Breakpoint);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show("Send breakpoint error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                }

                //COMPORT.ClosePort();
                //#if debug_RS232
                //                //COMPORT.OpenPort();
                //                COMPORT.ClosePort();
                //#endif

                if (COMPORT.port1.IsOpen)
                {
                    byte[] CMD_Read2Byte = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
                    if (MCU_ID == "0x3378" || MCU_ID == "0x3394" || MCU_ID == "0x3111" || MCU_ID == "0x3316"
                        || MCU_ID == "0x5312" || MCU_ID == "0x7212" || MCU_ID == "0x7020"
                        || MCU_ID == "0x6060" || MCU_ID == "0x7510" || MCU_ID == "0x3222"
                        || MCU_ID == "0x8132" || MCU_ID == "0x7311" || MCU_ID == "0x7011"
                        || MCU_ID == "0x7511" || MCU_ID == "0x5222" || MCU_ID == "0x7323"
                        || MCU_ID == "0x7031" || MCU_ID == "0x6080" || MCU_ID == "0x6220" || MCU_ID == "0x7321"
                        || MCU_ID == "0x7312" || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722"
                        || MCU_ID == "0x9902" || MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")// TODO 
                    {
                        CMD_Read2Byte[4] = 0x7f;
                        CMD_Read2Byte[5] = 0xff;
                        CMD_Read2Byte[6] = 0xfe;
                    }

                    COMPORT.SendCommand(CMD_Read2Byte);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 2)
                    {

                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x1a };
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMDdata);
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 4)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                byte[] CMD_ReadChipState = { 0x69, 0x04, 0x00, 0x00, 0x00, 0x00 };
                if (COMPORT.port1.IsOpen)
                {
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (TimeOutFlag != 0xaa)
                    {
                        COMPORT.SendCommand(CMD_ReadChipState);
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                        if (ReadDataBuffer[0] == 0x00 && ReadDataBuffer[1] == 0x00)
                        {
                            TimeOutFlag = 0xaa;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (ReadDataBuffer[0] == 0x01 || ReadDataBuffer[1] == 0x01)
                    {
                        CloseRS232();
                        MessageBox.Show("chip state busy!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                Loads19(filename);

                #region MyRegion //modify by LYL 170221 for FOSC 128,256T分频不能正常下载 
                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;

                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(MCUr_OPTION); //set mcu option
                        //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;

                        while (COMPORT.port1.BytesToRead < 5)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                //ReadDataBuffer[3] = 0;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Send option error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x7f, 0xff, 0xf0, 0x10, 0x00, 0x65 };
                        if ((MCU_ID == "0x0301") || (MCU_ID == "0x0311"))
                        {
                            CMD_ReadOption[4] = 0x00;
                            CMD_ReadOption[5] = 0x91;
                            CMD_ReadOption[6] = 0x00;
                        }
                        COMPORT.SendCommand(CMD_ReadOption);
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < 4)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Send option read error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                    }
                }               
                #endregion  

                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     COMPORT.SendCommand(MCUr_OPTION); //set mcu option
                //                     //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                // 
                //                     while (COMPORT.port1.BytesToRead < 5)
                //                     {
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             //ReadDataBuffer[3] = 0;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show("Send option error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                // 
                //                     byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x7f, 0xff, 0xf0, 0x10, 0x00, 0x65 };
                //                     if ((MCU_ID == "0x0301") || (MCU_ID == "0x0311"))
                //                     {
                //                         CMD_ReadOption[4] = 0x00;
                //                         CMD_ReadOption[5] = 0x91;
                //                         CMD_ReadOption[6] = 0x00;
                //                     }
                //                     COMPORT.SendCommand(CMD_ReadOption);
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                //                     while (COMPORT.port1.BytesToRead < 4)
                //                     {
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show("Send option read error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }
                // 
                // 
                //                 //set clk
                //                 byte[] set_clk8m = { 0x69, 0x06, 0x01, 0x08 };
                //                 set_clk8m[3] = frmMAIN.FREQ;
                // 
                //                 if (COMPORT.port1.IsOpen)
                //                 {
                //                     COMPORT.SendCommand(set_clk8m);//set break point
                //                     //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                //                     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //                     DataReceiveTimer.Enabled = true;
                //                     TimeOutFlag = 0;
                // 
                //                     while (COMPORT.port1.BytesToRead < 3)
                //                     {
                //                         if (TimeOutFlag == 0xaa)
                //                         {
                //                             DataReceiveTimer.Enabled = false;
                //                             //ReadDataBuffer[3] = 0;
                //                             break;
                //                         }
                //                     }
                //                     DataReceiveTimer.Enabled = false;
                //                     if (TimeOutFlag == 0xaa)
                //                     {
                //                         CloseRS232();
                //                         MessageBox.Show("Send set clk error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                //                             MessageBoxDefaultButton.Button1);
                //                         return;
                //                     }
                //                     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //                 }

                load_codfile();

                DebugInitial();
                BreakPointCheck();
                WatchDataUpdate();//add by wj for update watch

                DebugCommand("Reset");
                /*
                if (COMPORT.port1.IsOpen)           //modify by LYL 170221 for FOSC 128,256T分频不能正常下载
                {
                    COMPORT.SendCommand(MCU_Breakpoint);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show("Send breakpoint error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                }*/
                //return;
            }
            else if (MCU_TYPE == "CRISC")
            {

                //string filename = "G:\\Emulator\\IDE\\SinoSunIDE\\SinoSunIDE\\bin\\Debug\\Sample\\aaaa\\aaaa.s19";
                //reflash option display

                filename = ProjectPath/*GetProjectPath()*/; // g:\\aaa.proj
                filename = filename.Substring(0, filename.LastIndexOf("\\")) + "\\Output";
                filename = bsGetFiles.GetFiles(new DirectoryInfo(filename), "*.s19");

                //COMPORT.OpenPort();
                //#if debug_RS232
                //                COMPORT.OpenPort();
                //                //COMPORT.ClosePort();
                //#endif
                byte[] MCUr_OPTION = { 0x69, 0x14, 21, 0x00, 0x00, 0x91, 0x00, 0x75, 0xfc, 0xff, 0xff, 0xff, 0xff, 0xbc, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
                if (MCU_ID == "0x0314" || MCU_ID == "0x3221" || MCU_ID == "0x32821" || MCU_ID == "0x3220" || MCU_ID == "0x3394" || (MCU_ID == "0x3111") || (MCU_ID == "0x3378"))
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7212")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }



                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x9902")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }



                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7333")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    /*if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }*/

                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;


                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }
                }
                else if (MCU_ID == "0x9903")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    /*if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }*/

                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;
                    
                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }
                }
                else if (MCU_ID == "0x9904")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    /*if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }*/

                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;

                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }
                }
                else if (MCU_ID == "0x7122")
                {
                    // frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                    //verify test
                    /*if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }*/

                    //UInt32 temp_option = 0;
                    //UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[16] = (byte)(aa / 0x1000000);
                    //temp_option = aa % 0x1000000;
                    //MCUr_OPTION[15] = (byte)(temp_option / 0x10000);
                    //temp_option = temp_option % 0x10000;
                    //MCUr_OPTION[12] = (byte)(temp_option / 0x100);
                    //MCUr_OPTION[11] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;

                    for (int i = 7; i < 19; i++)        //modify by LYL 170221 for FOSC 128,256分频不能正常下载
                    {
                        MCUr_OPTION[i] = 0x00;
                    }
                }
                else if (MCU_ID == "0x7311" || MCU_ID == "0x7321")// TODO
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    //MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    //MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3316")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[14] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[13] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[10] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[9] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x5312")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7022")// TODO wj
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3264")
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[12] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[11] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;

                }
                else if (MCU_ID == "0x0301" || (MCU_ID == "0x0311"))
                {
                    UInt32 temp_option = 0;
                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[10] = (byte)(aa / 0x1000000);
                    temp_option = aa % 0x1000000;
                    MCUr_OPTION[9] = (byte)(temp_option / 0x10000);
                    temp_option = temp_option % 0x10000;
                    MCUr_OPTION[8] = (byte)(temp_option / 0x100);
                    MCUr_OPTION[7] = (byte)(temp_option % 0x100);

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x91;
                    MCUr_OPTION[6] = 0x00;
                }
                else if (MCU_ID == "0x6060")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7510")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x3222")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x8132")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    UInt32 aa = Convert.ToUInt32(OPTION);
                    MCUr_OPTION[8] = (byte)(aa / 0x100);
                    MCUr_OPTION[7] = (byte)(aa % 0x100);

                    MCUr_OPTION[16] = 0;
                    MCUr_OPTION[15] = 0;

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7011" || MCU_ID == "0x7031")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue6.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue6.Text, 16);
                        MCUr_OPTION[20] = (byte)(opt / 0x100);
                        MCUr_OPTION[19] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue7.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue7.Text, 16);
                        MCUr_OPTION[22] = (byte)(opt / 0x100);
                        MCUr_OPTION[21] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xe0;
                }
                else if (MCU_ID == "0x7511")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x5222")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x7323" || MCU_ID == "0x7312")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x6080")// TODO wj
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }
                else if (MCU_ID == "0x6220")// 
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x7f;
                    MCUr_OPTION[5] = 0xff;
                    MCUr_OPTION[6] = 0xf0;
                }

                //byte[] aaa_OPTION = { 0x69, 0x14,9,0x00,0x7f,0xff, 0xf0, 0x00, 0x00, 0xff,0xff,0xbc };
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCUr_OPTION); //set mcu option
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                    byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x7f, 0xff, 0xf0, 0x10, 0x00, 0x65 };
                    COMPORT.SendCommand(CMD_ReadOption);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 4)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                //set clk
                byte[] set_clk8m = { 0x69, 0x06, 0x01, 0x08 };
                set_clk8m[3] = frmMAIN.FREQ;

                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(set_clk8m);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 3)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                if (MCU_ID == "0x0301" || MCU_ID == "0x0311")
                {
                    MCU_Breakpoint[3] = 0x00;
                    MCU_Breakpoint[4] = 0x00;
                    MCU_Breakpoint[5] = 0x8f;
                    MCU_Breakpoint[6] = 0x00;
                }
                else //if (MCU_ID=="0x3221"||MCU_ID=="0x0314")
                {
                    MCU_Breakpoint[3] = 0x00;
                    MCU_Breakpoint[4] = 0x01;
                    MCU_Breakpoint[5] = 0x00;
                    MCU_Breakpoint[6] = 0x00;
                }



                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(MCU_Breakpoint);//set break point
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 5)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            //ReadDataBuffer[3] = 0;
                            break;
                        }
                    }

                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                }

                //COMPORT.ClosePort();
                //#if debug_RS232
                //                //COMPORT.OpenPort();
                //                COMPORT.ClosePort();
                //#endif
                if (COMPORT.port1.IsOpen)
                {
                    byte[] CMD_Read2Byte = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
                    if (MCU_ID == "0x3378" || MCU_ID == "0x3394" || MCU_ID == "0x3111" || MCU_ID == "0x3316"
                        || MCU_ID == "0x5312" || MCU_ID == "0x7212" || MCU_ID == "0x7020"
                        || MCU_ID == "0x6060" || MCU_ID == "0x7510" || MCU_ID == "0x3222"
                        || MCU_ID == "0x8132" || MCU_ID == "0x7311" || MCU_ID == "0x7011"
                        || MCU_ID == "0x7511" || MCU_ID == "0x5222" || MCU_ID == "0x7323"
                        || MCU_ID == "0x7031" || MCU_ID == "0x6080" || MCU_ID == "0x6220" 
                        || MCU_ID == "0x7321" || MCU_ID == "0x7312" 
                        || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722"
                        || MCU_ID == "0x9902" || MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")// TODO 
                    {
                        CMD_Read2Byte[4] = 0x7f;
                        CMD_Read2Byte[5] = 0xff;
                        CMD_Read2Byte[6] = 0xfe;
                    }

                    COMPORT.SendCommand(CMD_Read2Byte);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 2)
                    {

                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x1a };
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMDdata);
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;

                    while (COMPORT.port1.BytesToRead < 4)
                    {
                        if (TimeOutFlag == 0xaa)
                        {
                            DataReceiveTimer.Enabled = false;
                            break;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (TimeOutFlag == 0xaa)
                    {
                        CloseRS232();
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                byte[] CMD_ReadChipState = { 0x69, 0x04, 0x00, 0x00, 0x00, 0x00 };
                if (COMPORT.port1.IsOpen)
                {
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (TimeOutFlag != 0xaa)
                    {
                        COMPORT.SendCommand(CMD_ReadChipState);
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                        if (ReadDataBuffer[0] == 0x00 && ReadDataBuffer[1] == 0x00)
                        {
                            TimeOutFlag = 0xaa;
                        }
                    }
                    DataReceiveTimer.Enabled = false;
                    if (ReadDataBuffer[0] == 0x01 || ReadDataBuffer[1] == 0x01)
                    {
                        CloseRS232();
                        MessageBox.Show("chip state busy!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                Loads19(filename);
                #region MyRegion //modify by LYL 170221 for FOSC 128,256T分频不能正常下载
                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID=="0x7122")
                {
                    if (frmPC.optionValue0.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue0.Text, 16);
                        MCUr_OPTION[8] = (byte)(opt / 0x100);
                        MCUr_OPTION[7] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue1.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue1.Text, 16);
                        MCUr_OPTION[10] = (byte)(opt / 0x100);
                        MCUr_OPTION[9] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue2.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue2.Text, 16);
                        MCUr_OPTION[12] = (byte)(opt / 0x100);
                        MCUr_OPTION[11] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue3.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue3.Text, 16);
                        MCUr_OPTION[14] = (byte)(opt / 0x100);
                        MCUr_OPTION[13] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue4.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue4.Text, 16);
                        MCUr_OPTION[16] = (byte)(opt / 0x100);
                        MCUr_OPTION[15] = (byte)(opt % 0x100);
                    }
                    if (frmPC.optionValue5.Text != "")
                    {
                        UInt32 opt = Convert.ToUInt32(frmPC.optionValue5.Text, 16);
                        MCUr_OPTION[18] = (byte)(opt / 0x100);
                        MCUr_OPTION[17] = (byte)(opt % 0x100);
                    }

                    MCUr_OPTION[4] = 0x00;
                    MCUr_OPTION[5] = 0x80;
                    MCUr_OPTION[6] = 0x00;

                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(MCUr_OPTION); //set mcu option
                        //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;

                        while (COMPORT.port1.BytesToRead < 5)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                //ReadDataBuffer[3] = 0;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Send option error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x7f, 0xff, 0xf0, 0x10, 0x00, 0x65 };
                        if ((MCU_ID == "0x0301") || (MCU_ID == "0x0311"))
                        {
                            CMD_ReadOption[4] = 0x00;
                            CMD_ReadOption[5] = 0x91;
                            CMD_ReadOption[6] = 0x00;
                        }
                        COMPORT.SendCommand(CMD_ReadOption);
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < 4)
                        {
                            if (TimeOutFlag == 0xaa)
                            {
                                DataReceiveTimer.Enabled = false;
                                break;
                            }
                        }
                        DataReceiveTimer.Enabled = false;
                        if (TimeOutFlag == 0xaa)
                        {
                            CloseRS232();
                            MessageBox.Show("Send option read error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                            return;
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                    }
                }
                #endregion  
                //load_codfile();
                load_chc05_codfile();

                DebugInitial();
                BreakPointCheck();
                WatchDataUpdate();//add by wj for update watch

                DebugCommand("Reset");
                //return;
            }
        }


        //funtion
        public void DebugButtonDisplayContral()
        {
            if (DebugtoolStripButton.Checked == true)
            {
                if (GotoolStripButton.Checked == true)
                {
                    //ResetCPUtoolStripButton.Enabled = false;
                    StepIntotoolStripButton.Enabled = false;
                    StepOvertoolStripButton.Enabled = false;
                    StepOuttoolStripButton.Enabled = false;
                    //GotoolStripButton.Enabled = false;
                    RunToCursortoolStripButton.Enabled = false;
                    //StoptoolStripButton.Enabled = false;
                    GoNBKtoolStripButton.Enabled = false;
                }
                else
                {
                    ResetCPUtoolStripButton.Enabled = true;
                    StepIntotoolStripButton.Enabled = true;
                    StepOvertoolStripButton.Enabled = true;
                    StepOuttoolStripButton.Enabled = true;
                    GotoolStripButton.Enabled = true;
                    //RunToCursortoolStripButton.Enabled = true;
                    StoptoolStripButton.Enabled = true;
                    GoNBKtoolStripButton.Enabled = true;
                }
            }
            else
            {
                GotoolStripButton.Checked = false;
                GoNBKtoolStripButton.Checked = false;
                ResetCPUtoolStripButton.Enabled = false;
                StepIntotoolStripButton.Enabled = false;
                StepOvertoolStripButton.Enabled = false;
                StepOuttoolStripButton.Enabled = false;
                GotoolStripButton.Enabled = false;
                RunToCursortoolStripButton.Enabled = false;
                StoptoolStripButton.Enabled = false;
                GoNBKtoolStripButton.Enabled = false;
            }


        }
        //load s19 file 
        public void Loads19(string fileName)
        {
            byte[] codeS19 = new byte[63];
            byte[] MCU_REGbyte = { 0x69, 0x14, 6, 0x00, 0xc0, 0x00, 0x00, 0x00, 0xbc };
            if (File.Exists(fileName) == false)
            {
                MessageBox.Show("Can not find the Object file:" + fileName, "Load S19 Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            StreamReader FileLoad = new StreamReader(fileName);
            string s = FileLoad.ReadToEnd();
            string[] Line = s.Split(new char[] { '\n' }, StringSplitOptions.None);
            FileLoad.Close();

            // COMPORT.OpenPort();
            //#if debug_RS232
            //            COMPORT.OpenPort();
            //            //COMPORT.ClosePort();
            //#endif

            for (int i = 0; i < Line.Length; i++)
            {
                // Line[i].CopyTo(0, codeS19, 0, 8);
                byte t1, t2;
                int j;

                if (Line[i].Length < 1)
                {
                    continue;
                }
                if (Line[i][1] != '1')
                {
                    continue;
                }
                codeS19[0] = 0x69;
                codeS19[1] = 0x11;

                t1 = Line[i][2] > 0x39 ? System.Convert.ToByte((int)Line[i][2] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][2] - 0x30);
                t2 = Line[i][3] > 0x39 ? System.Convert.ToByte((int)Line[i][3] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][3] - 0x30);
                codeS19[2] = (byte)(t1 * 16 + t2);

                for (j = 4; j < Line[i].Length - 2; j += 2)
                {


                    t1 = Line[i][j] > 0x39 ? System.Convert.ToByte((int)Line[i][j] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j] - 0x30);
                    t2 = Line[i][j + 1] > 0x39 ? System.Convert.ToByte((int)Line[i][j + 1] - 0x41 + 0x0A) : System.Convert.ToByte((int)Line[i][j + 1] - 0x30);
                    codeS19[3 + (j - 4) / 2] = (byte)(t1 * 16 + t2);

                }

                int addr = codeS19[3] * 0x100 + codeS19[4];
                if (MCU_ID == "0x5222" && addr >= 0x8000)
                {
                    MCU_REGbyte[5] = System.Convert.ToByte(codeS19[3] - 0x80);
                    MCU_REGbyte[6] = System.Convert.ToByte(codeS19[4] / 2);
                    for (j = 5; j < codeS19[2] + 2; j += 2)
                    {
                        MCU_REGbyte[7] = codeS19[j];

                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(MCU_REGbyte);
                            //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;

                            while (COMPORT.port1.BytesToRead < 6)
                            {
                                if (TimeOutFlag == 0xaa)
                                {
                                    DataReceiveTimer.Enabled = false;
                                    ReadDataBuffer[2] = 0xf2;
                                    break;
                                }
                            }
                            DataReceiveTimer.Enabled = false;
                            if (TimeOutFlag == 0xaa)
                            {
                                CloseRS232();
                                MessageBox.Show(ShowLanguage.Current.MessageBoxText7, ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                                return;
                            }
                            int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                            COMPORT.port1.DiscardOutBuffer();

                        }

                        if (ReadDataBuffer[2] != 0x12)
                        {
                                MessageBox.Show("load S19 error!");
                                return;
                        }
                        MCU_REGbyte[6] += 1;
                    }
                }
                else
                {
                    bool verify_ok = false;
                    int load_retry = 0;

                    while ((verify_ok == false) && (load_retry < 5))
                    {
                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(codeS19);
                            //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;

                            while (COMPORT.port1.BytesToRead < 6)
                            {
                                if (TimeOutFlag == 0xaa)
                                {
                                    DataReceiveTimer.Enabled = false;
                                    ReadDataBuffer[2] = 0xf2;
                                    break;
                                }
                            }
                            DataReceiveTimer.Enabled = false;
                            if (TimeOutFlag == 0xaa)
                            {
                                CloseRS232();
                                MessageBox.Show("Send ram data error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                                return;
                            }
                            int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                            COMPORT.port1.DiscardOutBuffer();

                        }

                        if (ReadDataBuffer[2] == 0x12)
                            verify_ok = true;
                        else
                        {
                            load_retry = load_retry + 1;
                            if (load_retry == 5)
                            {
                                
                                        MessageBox.Show("load S19 error!");
                                        return;
                                
                                
                            }
                        }

                    }//while verify_ok==false
                }
            }


            if (MCU_ID == "0x3264")
            {
                byte[] load_mc32p64_code = { 0x69, 0x16 };
                if (COMPORT.port1.IsOpen)
                    COMPORT.SendCommand(load_mc32p64_code);//set break point
                COMPORT.port1.DiscardOutBuffer();
            }

            //COMPORT.ClosePort();
            //#if debug_RS232
            //            //COMPORT.OpenPort();
            //            COMPORT.ClosePort();
            //#endif

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimeOutFlag = 0xaa;
        }

        private void GotoolStripButton_Click(object sender, EventArgs e)
        {
            GotoolStripButton.Checked = true;
            DebugButtonDisplayContral();
        }

        private void StoptoolStripButton_Click(object sender, EventArgs e)
        {
            GotoolStripButton.Checked = false;
            DebugButtonDisplayContral();
        }

        private void ResetCPUtoolStripButton_Click(object sender, EventArgs e)
        {
            GotoolStripButton.Checked = false;
            GoNBKtoolStripButton.Checked = false;
            DebugButtonDisplayContral();
        }

        #region TteeView 2 XML
        private void ProjectSaveXML()
        {
            //TreeView exproTreeView = frmexplorer.treeView1;

            TreeNode2Xml();

        }
        private void TreeNode2Xml()
        {

            //string projPath = ProjectPath;
            string aa = null;
            string temp = null;
            string num = null;

            string srt = ProjectPath;
            //             string srt = frmMAIN.APPLICATION_PATH + "global.ini";
            //             XElement rootNode = XElement.Load(srt);
            // 
            //             IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
            //                                             select myTarget;
            //             foreach (XElement xnode in myvalue)
            //             {
            //                 srt = xnode.Attribute("Ppath").Value;
            //             }

            if (srt == "")
                return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(srt);

            // XmlNodeList xmlNodes = xmlDoc.SelectSingleNode("/Project/ProWindows").ChildNodes;///SouceCode
            XmlNode xmlNode = xmlDoc.SelectSingleNode("/Project/ProWindows");
            xmlNode.RemoveAll();

            // xmlDoc.Save(ProjectPath);

            TreeNodeCollection treeNodes = frmexplorer.treeView1.Nodes;
            foreach (TreeNode treeNode in treeNodes)
            {
                aa = treeNode.Text;
                XmlElement xel = xmlDoc.CreateElement("Explorer"); //create explorer
                xel.SetAttribute("fName", aa);
                xmlNode.AppendChild(xel);
                if (treeNode.Nodes.Count > 0)
                {
                    foreach (TreeNode treeNode1 in treeNode.Nodes)
                    {
                        aa = treeNode1.Text;
                        XmlElement xesub1 = xmlDoc.CreateElement("SouceCode");
                        xesub1.SetAttribute("fName", aa);
                        xel.AppendChild(xesub1);
                        if (treeNode1.Nodes.Count > 0)
                        {
                            foreach (TreeNode treeNode2 in treeNode1.Nodes)
                            {
                                aa = treeNode2.Text;
                                XmlElement xesub2 = xmlDoc.CreateElement("asmFile");
                                xesub2.SetAttribute("fName", aa);
                                xesub2.SetAttribute("fPath", "\\" + aa);
                                xesub1.AppendChild(xesub2);
                                if (treeNode2.Nodes.Count > 0)
                                {
                                    foreach (TreeNode treeNode3 in treeNode2.Nodes)
                                    {
                                        aa = treeNode3.Text;

                                    }
                                }
                            }
                        }
                    }
                }
            }

            xmlDoc.Save(srt);

            //write option value
            XElement rootNode = XElement.Load(srt);
            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("Option")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                if (xnode.Attribute("Value") == null)
                {
                    xnode.Attribute("Value").Value = OPTION.ToString("X");
                }
                else
                {
                    xnode.Attribute("Value").Value = OPTION.ToString("X");
                }

                if (xnode.Attribute("Value0") == null)
                {
                    xnode.Add(new XAttribute("Value0", frmPC.optionValue0.Text));
                }
                else
                {
                    xnode.Attribute("Value0").Value = frmPC.optionValue0.Text;
                }

                if (xnode.Attribute("Value1") == null)
                {
                    xnode.Add(new XAttribute("Value1", frmPC.optionValue1.Text));
                }
                else
                {
                    xnode.Attribute("Value1").Value = frmPC.optionValue1.Text;
                }

                if (xnode.Attribute("Value2") == null)
                {
                    xnode.Add(new XAttribute("Value2", frmPC.optionValue2.Text));
                }
                else
                {
                    xnode.Attribute("Value2").Value = frmPC.optionValue2.Text;
                }

                if (xnode.Attribute("Value3") == null)
                {
                    xnode.Add(new XAttribute("Value3", frmPC.optionValue3.Text));
                }
                else
                {
                    xnode.Attribute("Value3").Value = frmPC.optionValue3.Text;
                }

                if (xnode.Attribute("Value4") == null)
                {
                    xnode.Add(new XAttribute("Value4", frmPC.optionValue4.Text));
                }
                else
                {
                    xnode.Attribute("Value4").Value = frmPC.optionValue4.Text;
                }

                if (xnode.Attribute("Value5") == null)
                {
                    xnode.Add(new XAttribute("Value5", frmPC.optionValue5.Text));
                }
                else
                {
                    xnode.Attribute("Value5").Value = frmPC.optionValue5.Text;
                }

                /*if (xnode.Attribute("Value6") == null)
                {
                    xnode.Add(new XAttribute("Value6", frmPC.optionValue6.Text));
                }
                else
                {
                    xnode.Attribute("Value6").Value = frmPC.optionValue6.Text;
                }

                if (xnode.Attribute("Value7") == null)
                {
                    xnode.Add(new XAttribute("Value7", frmPC.optionValue7.Text));
                }
                else
                {
                    xnode.Attribute("Value7").Value = frmPC.optionValue7.Text;
                }*/

                if(MCU_ID == "0x7041")  //add by lyl for 240 FOSC fanxian  P587  17/01/19
                {
                    temp = frmPC.propertyGridEx_PC.Item[4].Value.ToString();
                    switch (temp)
                    {
                        case "内部RC振荡器": num = "1";
                            break;
                        case "外部晶体振荡器32768": num = "2";
                            break;
                        case "外部晶体振荡器455K": num = "3";
                            break;
                        case "外部晶体振荡器1M": num = "4";
                            break;
                        case "外部晶体振荡器2M": num = "5";
                            break;
                        case "外部晶体振荡器4M": num = "6";
                            break;
                        case "外部晶体振荡器8M": num = "7";
                            break;
                        case "外部晶体振荡器16M": num = "8";
                            break;
                        case "外部时钟输入<1M": num = "9";
                            break;
                        case "外部时钟输入<2M": num = "10";
                            break;
                        case "外部时钟输入<4M": num = "11";
                            break;
                        case "外部RC振荡器<1M": num = "12";
                            break;
                        case "外部RC振荡器<2M": num = "13";
                            break;
                        case "外部RC振荡器<4M": num = "14";
                            break;
                        default: num = "15";
                    	    break;
                    }

                    if (xnode.Attribute("FOSC_7041") == null)
                    {
                        xnode.Add(new XAttribute("FOSC_7041", num));
                    }
                    else
                    {
                        xnode.Attribute("FOSC_7041").Value = num;
                    } 
                }
            }
            rootNode.Save(srt);
             
        }
        #endregion


        private void ToolstoolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (System.Windows.Forms.ToolStripMenuItem)sender;
            if (tm.Tag == null)
                return;
            switch (tm.Tag.ToString())
            {
                case "FirwareUpdate":
                    //                     string fileName = APPLICATION_PATH + "\\tools\\IAP.exe";
                    //                     Process myProcess = new Process();
                    //                     ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(fileName);
                    //                     myProcess.StartInfo = myProcessStartInfo;
                    //                     myProcess.Start();
                    // myProcess.WaitForExit(); //wait until exe exit
                    break;
                default:

                    break;
            }
        }

        private void ToolsSystemOption(object sender, EventArgs e)
        {
            FrmSystemOption frmSystemOption = new FrmSystemOption();
            // frmSystemOption.Show();
            if (frmSystemOption.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ApplyLanguage();
            }
        }

        private void frmMAIN_Closed(object sender, EventArgs e)
        {
            frmMAIN_FormClosed(null, null);
            this.Close();

        }

        private void AddtoWatchWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sb = CurrentTB.SelectedText;
            if (frmWatch.Visible)
            {
                int current_line = frmWatch.DataGridView_Watch.Rows.Count - 1;
                frmWatch.DataGridView_Watch.Rows.AddCopy(current_line);
                frmWatch.Watch_add_symbol(sb, current_line);
            }
        }

        private void tempDebugtoolStripButton_Clik(object sender, EventArgs e)
        {

        }

        private void ApplyLanguage()
        {
            // menu
            // File
            this.FileToolStripMenuItem.Text = ShowLanguage.Current.File + "(&F)";
            this.NewToolStripMenuItem.Text = ShowLanguage.Current.NewBuilt;
            this.NewProjectToolStripMenuItem1.Text = ShowLanguage.Current.Project;
            this.NewFile_ToolStripMenuItem.Text = ShowLanguage.Current.File;
            this.OpenToolStripMenuItem.Text = ShowLanguage.Current.Open + "(&O)";
            this.OpenProjectToolStripMenuItem.Text = ShowLanguage.Current.Project;
            this.OpenFile_ToolStripMenuItem1.Text = ShowLanguage.Current.File;
            this.CloseFileToolStripMenuItem.Text = ShowLanguage.Current.Close + "(&C)";
            this.CloseProjWindownToolStripMenuItem.Text = ShowLanguage.Current.CloseProject + "(&T)";
            this.Save_ToolStripMenuItem.Text = ShowLanguage.Current.Save;
            this.SaveAs_ToolStripMenuItem.Text = ShowLanguage.Current.SavaAs;
            this.menuSaveAllToolStripMenuItem.Text = ShowLanguage.Current.SaveALL + "(&L)";
            this.ExportTemplateToolStripMenuItem.Text = ShowLanguage.Current.ExportTemplate + "(&E)";
            this.Print_ToolStripMenuItem.Text = ShowLanguage.Current.Print + "(&P)";
            this.PrintSetToolStripMenuItem.Text = ShowLanguage.Current.PrintSetup + "(&U)";
            this.RecentFilesToolStripMenuItem.Text = ShowLanguage.Current.RecentFiles + "(&F)";
            this.RecentProjectToolStripMenuItem.Text = ShowLanguage.Current.RecentProjects + "(&J)";
            this.EXITToolStripMenuItem.Text = ShowLanguage.Current.ExitWindow + "(&X)";
            // Edit
            this.EditToolStripMenuItem.Text = ShowLanguage.Current.Edit + "(&E)";
            this.UndoToolStripMenuItem.Text = ShowLanguage.Current.Undo + "(&U)";
            this.RedoRToolStripMenuItem.Text = ShowLanguage.Current.Redo + "(&R)";
            this.CutToolStripMenuItem.Text = ShowLanguage.Current.Cut + "(&T)";
            this.CopyToolStripMenuItem.Text = ShowLanguage.Current.Copy + "(&C)";
            this.PastToolStripMenuItem.Text = ShowLanguage.Current.Paste + "(&P)";
            this.DeleteToolStripMenuItem.Text = ShowLanguage.Current.Delete + "(&E)";
            this.deleteSelectdToolStripMenuItem.Text = ShowLanguage.Current.Delete + "(&D)       Delect";
            this.deleteCurrentLineLToolStripMenuItem.Text = ShowLanguage.Current.DeleteRow + "(&L)";
            this.SelectAllToolStripMenuItem.Text = ShowLanguage.Current.SelectAll + "(&A)";
            this.FindToolStripMenuItem.Text = ShowLanguage.Current.Find + "(&F)";
            this.tbFindNextToolStripMenuItem.Text = ShowLanguage.Current.FindOne;
            this.ReplaceToolStripMenuItem.Text = ShowLanguage.Current.Replace + "(&R)";
            this.HOptionToolStripMenuItem.Text = ShowLanguage.Current.Advanced + "(&V)";
            this.ToUpperToolStripMenuItem.Text = ShowLanguage.Current.MakeUppercase;
            this.ToLowerToolStripMenuItem.Text = ShowLanguage.Current.MakeLowercase;
            this.CommentSelectedToolStripMenuItem.Text = ShowLanguage.Current.CommentSelected;
            this.UncommentSelectedToolStripMenuItem.Text = ShowLanguage.Current.UncommentSelected;
            this.IndentToolStripMenuItem.Text = ShowLanguage.Current.InsertLineIndentation + "                Tab";
            this.UnindentToolStripMenuItem.Text = ShowLanguage.Current.RemoveLineIndentation + "                Shift+Tab";
            this.UntabifySelectionToolStripMenuItem.Text = ShowLanguage.Current.UntabifySelection;
            this.TabifySelectionToolStripMenuItem.Text = ShowLanguage.Current.TabifySelection;
            this.NavigateBackwardsToolStripMenuItem.Text = ShowLanguage.Current.NavigateBackwards;
            this.NavigateFarwardsToolStripMenuItem.Text = ShowLanguage.Current.NavigatesForwards;
            this.BookMToolStripMenuItem.Text = ShowLanguage.Current.Bookmarks + "(&K)";
            this.ToggleBookmarkToolStripMenuItem.Text = ShowLanguage.Current.ToggleBookmark + "(&T)";
            this.PreBookmarkToolStripMenuItem.Text = ShowLanguage.Current.PreBookmark + "(&P)";
            this.NextBookmarkToolStripMenuItem.Text = ShowLanguage.Current.NextBookmark + "(&B)";
            this.ClearAllBookmarkToolStripMenuItem.Text = ShowLanguage.Current.ClearAllBookmark;
            // View
            this.ViewToolStripMenuItem.Text = ShowLanguage.Current.View + "(&V)";
            this.ProjectWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.ProjectWindow;
            this.EditorToolStripMenuItem.Text = ShowLanguage.Current.BooksWindow;
            this.fnWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.FunctionList;
            this.BuildWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.BuildOutput;
            this.DisassemblyWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.DisassemblyWindow;
            this.REGWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.Register;
            this.SFREGWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.SpecialFunctionRegister;
            this.WatchWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.WatchWindow;
            this.CodeWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.ContentsMemoryRegion;
            this.BreakpointWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.BreakpointsList;
            this.TBWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.Toolbar;
            this.CriterionToolStripMenuItem.Text = ShowLanguage.Current.Criterion;
            this.CompileToolStripMenuItem.Text = ShowLanguage.Current.Edit;
            this.CompileDebuggerToolStripMenuItem.Text = ShowLanguage.Current.CompileDebugger;
            this.ROMWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.ROMData;
            this.RAMWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.RAMData;
            this.MTPWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.MTPData;
            this.MDWindowSH_ToolStripMenuItem.Text = ShowLanguage.Current.MDWindow;
            this.OptionWindowToolStripMenuItem.Text = ShowLanguage.Current.ChipConfiguration;
            // Project
            this.ProjectToolStripMenuItem.Text = ShowLanguage.Current.Project + "(&P)";
            this.ProjectNewToolStripMenuItem.Text = ShowLanguage.Current.NewProject;
            this.ProjectOpenToolStripMenuItem.Text = ShowLanguage.Current.OpenProject;
            this.CloseProjectToolStripMenuItem1.Text = ShowLanguage.Current.CloseProject;
            this.TranslateTargetToolStripMenuItem.Text = ShowLanguage.Current.CompileAssemble;
            this.BuildToolStripMenuItem.Text = ShowLanguage.Current.GeneratedCode;
            this.RebuildToolStripMenuItem.Text = ShowLanguage.Current.RegeneratedCode;
            this.RebuildANDDownloadToolStripMenuItem.Text = ShowLanguage.Current.RegeneratedCodeAndDownload;
            this.PropertiesToolStripMenuItem.Text = ShowLanguage.Current.Properties;
            // Debug
            this.DebugToolStripMenuItem.Text = ShowLanguage.Current.Debug + "(&D)";
            this.StartDebugToolStripMenuItem.Text = ShowLanguage.Current.StartStopDebugger;
            this.ResetCPUToolStripMenuItem.Text = ShowLanguage.Current.ResetCPU;
            this.RunToolStripMenuItem.Text = ShowLanguage.Current.Run;
            this.StopToolStripMenuItem.Text = ShowLanguage.Current.StopRun;
            this.TrackToolStripMenuItem.Text = ShowLanguage.Current.Track;
            this.StepToolStripMenuItem.Text = ShowLanguage.Current.StepIn;
            this.StepOverToolStripMenuItem.Text = ShowLanguage.Current.StepOver;
            this.StepOutToolStripMenuItem.Text = ShowLanguage.Current.StepOut;
            this.RunToCursorToolStripMenuItem.Text = ShowLanguage.Current.RunTo;
            this.GoNeglectBreakpointToolStripMenuItem.Text = ShowLanguage.Current.GoNeglectBreakpoint;
            this.AutomaticStepToolStripMenuItem.Text = ShowLanguage.Current.AutomaticStep;
            this.AutomaticTrackToolStripMenuItem.Text = ShowLanguage.Current.AutomaticTrack;
            this.InsertRemoveBreakpointToolStripMenuItem.Text = ShowLanguage.Current.InsertRemoveBreakpoint;
            this.BreakpointEnableToolStripMenuItem.Text = ShowLanguage.Current.DisableEnableBreakpoint;
            this.DisableAllBreakpointsToolStripMenuItem.Text = ShowLanguage.Current.DisableAllBreakpoints;
            this.KillAllBreakpointsToolStripMenuItem.Text = ShowLanguage.Current.KillAllBreakpoints;
            // Tools
            this.ToolsToolStripMenuItem.Text = ShowLanguage.Current.Tools + "(&T)";
            this.FirwareUpdateToolStripMenuItem.Text = ShowLanguage.Current.FirmwareUpdates;
            this.COMSelectToolStripMenuItem.Text = ShowLanguage.Current.DesignatedCOMPort;
            this.MergeFilesToolStripMenuItem.Text = ShowLanguage.Current.MergeFiles;
            this.SystemOptionToolStripMenuItem.Text = ShowLanguage.Current.Options;
            // Window
            this.WindowToolStripMenuItem.Text = ShowLanguage.Current.Window + "(&W)";
            // Help
            this.HelpToolStripMenuItem.Text = ShowLanguage.Current.Help + "(&H)";
            this.sinoIDEHelpToolStripMenuItem.Text = ShowLanguage.Current.WinScopeHelp;
            this.CheckForUpdateToolStripMenuItem.Text = ShowLanguage.Current.CheckForUpdate;
            this.RegisterOnlineToolStripMenuItem.Text = ShowLanguage.Current.RegisterOnline;
            this.AboutSinoIDEToolStripMenuItem.Text = ShowLanguage.Current.AboutWinScope;
            // toolbar
            this.toolStripButtonNew.ToolTipText = ShowLanguage.Current.NewFile;
            this.toolStripButtonOpen.ToolTipText = ShowLanguage.Current.OpenFile;
            this.toolStripButtonSave.ToolTipText = ShowLanguage.Current.Save;
            this.toolStripButtonSaveAll.ToolTipText = ShowLanguage.Current.SaveALL;
            this.toolStripButtonCut.ToolTipText = ShowLanguage.Current.Cut;
            this.toolStripButtonCopy.ToolTipText = ShowLanguage.Current.Copy;
            this.toolStripButtonPast.ToolTipText = ShowLanguage.Current.Paste;
            this.toolStripSplitButtonUndo.ToolTipText = ShowLanguage.Current.UndoTip;
            this.toolStripSplitButtonRedo.ToolTipText = ShowLanguage.Current.RedoTip;
            this.toolStripSplitButton3.ToolTipText = ShowLanguage.Current.NavigateBackwardsTip;
            this.toolStripSplitButton4.ToolTipText = ShowLanguage.Current.NavigatesForwardsTip;
            this.toolStripButton4.ToolTipText = ShowLanguage.Current.RemoveLineIndentation;
            this.toolStripButton5.ToolTipText = ShowLanguage.Current.InsertLineIndentation;
            this.cloneLinesAndCommenttoolStripButton.ToolTipText = ShowLanguage.Current.CloneLinesAndComment;
            this.cloneLinestoolStripButton.ToolTipText = ShowLanguage.Current.CloneLines;
            this.commentSelectedtoolStripButton.ToolTipText = ShowLanguage.Current.CommentSelected;
            this.uncommentSelectedtoolStripButton.ToolTipText = ShowLanguage.Current.UncommentSelected;
            this.bookmarkPlusButton.ToolTipText = ShowLanguage.Current.InsertBookmark;
            this.toolStripButton2.ToolTipText = ShowLanguage.Current.PreBookmark;
            this.toolStripButton3.ToolTipText = ShowLanguage.Current.NextBookmark;
            this.gotoButton.ToolTipText = ShowLanguage.Current.GoToBookmark;
            this.bookmarkMinusButton.ToolTipText = ShowLanguage.Current.RemoveBookmark;
            this.FindInFilestoolStripButton.ToolTipText = ShowLanguage.Current.FindInFiles;
            this.tbFind.ToolTipText = ShowLanguage.Current.InputCharacter;
            this.FindInActiveFiletoolStripButton.ToolTipText = ShowLanguage.Current.FindInActiveFile;
            this.ProjectWindowtoolStripSplitButton.ToolTipText = ShowLanguage.Current.ProjectWindowTip;
            this.ToolOptiontoolStripButton.ToolTipText = ShowLanguage.Current.ToolOption;
            this.HelptoolStripButton.ToolTipText = ShowLanguage.Current.Help;
            this.DebugtoolStripButton.ToolTipText = ShowLanguage.Current.StartStopDebugger;
            this.ResetCPUtoolStripButton.ToolTipText = ShowLanguage.Current.ResetCPU;
            this.GotoolStripButton.ToolTipText = ShowLanguage.Current.Run;
            this.StoptoolStripButton.ToolTipText = ShowLanguage.Current.StopRun;
            this.StepIntotoolStripButton.ToolTipText = ShowLanguage.Current.StepInTip;
            this.StepOvertoolStripButton.ToolTipText = ShowLanguage.Current.StepOverTip;
            this.StepOuttoolStripButton.ToolTipText = ShowLanguage.Current.StepOutTip;
            this.RunToCursortoolStripButton.ToolTipText = ShowLanguage.Current.RunTo;
            this.GoNBKtoolStripButton.ToolTipText = ShowLanguage.Current.GoNeglectBreakpoint;
            this.BKInsertORRemovetoolStripButton.ToolTipText = ShowLanguage.Current.InsertRemoveBreakpoint;
            this.DisableBKtoolStripButton.ToolTipText = ShowLanguage.Current.DisableEnableBreakpoint;
            this.DisableAllBKtoolStripButton.ToolTipText = ShowLanguage.Current.DisableAllBreakpoints;
            this.ClearAllBKtoolStripButton.ToolTipText = ShowLanguage.Current.KillAllBreakpoints;
            this.BKListtoolStripSplitButton.ToolTipText = ShowLanguage.Current.BreakpointsList;
            this.TranslateTargettoolStripButton.ToolTipText = ShowLanguage.Current.Translatecurrentfile;
            this.BuildTargettoolStripButton.ToolTipText = ShowLanguage.Current.Buildtarget;
            this.RebuildAlltoolStripButton.ToolTipText = ShowLanguage.Current.RegeneratedCodeAndDownload;
            this.OPTIONtoolStripButton.ToolTipText = ShowLanguage.Current.MCUOption;

            this.toolStripStatusLabel1.Text = "^_^" + ShowLanguage.Current.Welcome;
            this.SNLinkStatusDisplay.Text = ShowLanguage.Current.CurrentMode + "：" + ShowLanguage.Current.Edit;
            string strTime = this.toolStripStatusLabel3.Text;
            int nPos = strTime.IndexOf("：");
            if (nPos >= 0)
            {
                string strRepLoginTime = strTime.Substring(0, nPos);
                this.toolStripStatusLabel3.Text = strTime.Replace(strRepLoginTime, ShowLanguage.Current.LoginTime);
            }
            strTime = this.StopWatch_lab.Text;
            nPos = strTime.IndexOf("：");
            if (nPos >= 0)
            {
                string strRepOprationTime = strTime.Substring(0, nPos);
                this.StopWatch_lab.Text = strTime.Replace(strRepOprationTime, ShowLanguage.Current.OperationTime);
            }
            strTime = this.InstructTime_lab.Text;
            nPos = strTime.IndexOf("：");
            if (nPos >= 0)
            {
                string strRepRunTime = strTime.Substring(0, nPos);
                this.InstructTime_lab.Text = strTime.Replace(strRepRunTime, ShowLanguage.Current.RunTime);
            }

            //contexMenu
            this.FEToupperToolStripMenuItem.Text = ShowLanguage.Current.MakeUppercase;
            this.FEToLowerToolStripMenuItem.Text = ShowLanguage.Current.MakeLowercase;
            this.toolStripMenuItem15.Text = ShowLanguage.Current.Cut;
            this.toolStripMenuItem16.Text = ShowLanguage.Current.Copy;
            this.toolStripMenuItem17.Text = ShowLanguage.Current.Paste;
            this.FESelectAllToolStripMenuItem.Text = ShowLanguage.Current.SelectAll;
            this.FEFindToolStripMenuItem.Text = ShowLanguage.Current.Find;
            this.FEReplaceToolStripMenuItem.Text = ShowLanguage.Current.Replace;
            this.FECommentToolStripMenuItem.Text = ShowLanguage.Current.CommentSelected;
            this.FEUncommnetToolStripMenuItem.Text = ShowLanguage.Current.UncommentSelected;
            this.FEBookMarkTogleToolStripMenuItem.Text = ShowLanguage.Current.InsertRemoveBookmark;
            this.FEBookMarkCLRToolStripMenuItem1.Text = ShowLanguage.Current.RemoveCurBookmark;
            this.PropertiesToolStripMenuItem1.Text = ShowLanguage.Current.Properties;
            this.AddtoWatchWindowToolStripMenuItem.Text = ShowLanguage.Current.AddtoWatchWindow;

            // other form
            frmREG.ApplyLanguage();
            frmmessage.ApplyLanguage();
            frmexplorer.ApplyLanguage();
            fs.ApplyLanguage();
            frmfile.ApplyLanguage();
            frmBreakPoint.ApplyLanguage();
            frmRAM.ApplyLanguage();
            frmROM.ApplyLanguage();
            frmPC.ApplyLanguage();
            frmPC.OptionReflash();
            frmSys.ApplyLanguage();
            frmWatch.ApplyLanguage();
        }

        private void ItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            FATabStripItem clickedItem = (FATabStripItem)e.Item;
            string str = clickedItem.Tag as string;
            const string OptionName = "CustomInfo.xml";
            string CurPath = Application.StartupPath; //获取当前EXE路径
            string xmlFileName = CurPath + "\\" + OptionName;
            string strText = OperateIniFile.ReadIniData("Information", "Version", "V0.04bt", xmlFileName);
            this.Text = "WinScope IDE " + strText + " - [" + str + "]";
        }


    } //class

    public class TbInfo
    {
        // Key - Line.UniqueId
        public HashSet<int> bookmarksLineId = new HashSet<int>();
        // Index - bookmark number, Value - Line.UniqueId
        public List<int> bookmarks = new List<int>();

        //
        // Key - Line.UniqueId
        public HashSet<int> EnableBreakpointsLineId = new HashSet<int>();

        // Index - bookmark number, Value - Line.UniqueId
        public List<int> EnableBreakpoints = new List<int>();
        // Key - Line.UniqueId
        public HashSet<int> DisableBreakpointsLineId = new HashSet<int>();
        // Index - bookmark number, Value - Line.UniqueId
        public List<int> DisableBreakpoints = new List<int>();

        public AutocompleteMenu popupMenu;
    }
}
