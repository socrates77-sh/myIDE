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
    partial class frmMAIN
    {


        #region Debug get LineNumber
        private void GetProjectASM_FilesName()
        {
            string debugProj;
            //string debugFile;

            //string str = frmMAIN.APPLICATION_PATH + "global.ini";
            string strPath = null;

            strPath = ProjectPath/*GetProjectPath()*/;

            if (strPath == "")
                return;
            //debugProj = strPath.Substring(0, strPath.LastIndexOf(@".")); //获取不带扩展名的项目路径
            debugProj = strPath.Substring(0, strPath.LastIndexOf(@"\")); //获取项目路径

            //获取子目录ASM文件名
            XElement rootNode = XElement.Load(strPath);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("SouceCode")
                                            where (string)myTarget.Attribute("fName").Value == "SourceCode"
                                            select myTarget;

            IEnumerable<XElement> asmTarget = from myTarget in myvalue.Descendants("asmFile")
                                              select myTarget;
            // int a = asmTarget.Count;


            ASMFileName.Clear();
            ListFileName.Clear();

            foreach (XElement xnode in asmTarget)
            {
                string temp = xnode.Attribute("fName").Value;
                ASMFileName.Add(debugProj + "\\" + temp);
                ListFileName.Add(debugProj + "\\" + temp.Substring(0, temp.LastIndexOf(@".")) + ".lst");
            }

        }
        private void DebugInitial()
        {
            GetProjectASM_FilesName();//get ASM fileName & List files Name

            //get cod file information 
        }

        private void SerchLinNume_HC05(string pcvalue)
        {
            //MC30P01:
            //[A-Fa-f0-9]+\u0020+[A-Fa-f0-9]+\u0020+[0-9]{5}
            //MC20P24B:
            //[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]{4}
            //[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s+[\w]+[\.]asm\b

            string hc05list = @"[0-9]+\s+\[[^\]]*\]\s+" + pcvalue + @"+\s+[\S]+[\.]asm\b";

            string pattern = hc05list;

            if (DebugFlag == false)
            {

                return;
            }

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //listFile.Selection.Start = new Place(0,34);
            //textIndex = 0;
            //regex
            if (listFile.Text.Length < textIndex)
                textIndex = 0;

            Match matches = r.Match(listFile.Text, textIndex);
            textIndex = matches.Index + 20;
            string temp = matches.Value;
            if (temp == "")
            {
                matches = r.Match(listFile.Text, 0);
                textIndex = matches.Index + 20;
                temp = matches.Value;
                if (temp == "")
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText12);
                    return;
                }
            }
            r = new Regex("[0-9]+\\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            matches = r.Match(temp, 0);
            string numLine = matches.Value;
            //string numLine = temp.Substring(0, temp.IndexOf("\t"));
            DebugLineNumber = Convert.ToInt32(numLine);

            //get asmfileName
            Regex asmf = new Regex(@"[\S]+[\.]asm\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Match asmfstr = asmf.Match(temp, 0);
            string currentAsmfile = asmfstr.Value;

            asmf = new Regex(currentAsmfile, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            if (frmfile.tsFiles.Items.Count == 0)
            {
                for (int i = 0; i < ASMFileName.Count; i++)
                {
                    string str = Path.GetFileName(ASMFileName[i]);
                    Match fequ = asmf.Match(str, 0);
                    if (fequ.Length > 0)
                    {
                        OpenFile_MouseDouble_Click(ASMFileName[i]);
                        break;
                    }
                }

            }
            else if (frmfile.tsFiles.SelectedItem.ToString() != currentAsmfile)
            {
                int i = 0;
                string str_file = frmfile.tsFiles.SelectedItem.ToString();
                Match fequ = asmf.Match(str_file, 0);
                if (fequ.Length == 0)
                {
                    for (i = 0; i < frmfile.tsFiles.Items.Count; i++)
                    {
                        str_file = frmfile.tsFiles.Items[i].ToString();
                        fequ = asmf.Match(str_file, 0);

                        if (fequ.Length > 0)
                        {
                            frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[i];
                            break;
                        }
                    }
                    if (i >= frmfile.tsFiles.Items.Count) //说明已经打开的文件没有
                    {
                        for (int j = 0; j < ASMFileName.Count; j++)
                        {
                            string str = Path.GetFileName(ASMFileName[j]);
                            fequ = asmf.Match(str, 0);
                            if (fequ.Length > 0)
                            {
                                OpenFile_MouseDouble_Click(ASMFileName[j]);
                                break;
                            }
                        }
                    }

                }

            }

            gotoLine_Click(DebugLineNumber);
            DebugFlag = true;
        }

        private void load_codfile()
        {
            //load cod file
            CODDebug cod_discode = new CODDebug();
            string pPath = ProjectPath/*GetProjectPath()*/;
            string projectName = Path.GetFileNameWithoutExtension(pPath);
            pPath = pPath.Substring(0, pPath.LastIndexOf("\\")) + "\\Output\\" + projectName + ".cod";

            if (File.Exists(pPath))
            {
                listFile.Text = cod_discode.ReadBin2Byte(pPath);
            }
            else
            {
                MessageBox.Show("cod File:" + pPath + "is can not find!");
                DebugFlag = false;
                return;
            }
        }

        private void load_hc05_listfile() //load AHC05 cod file
        {
            string pPath = ProjectPath/*GetProjectPath()*/;
            //string projectName = Path.GetFileNameWithoutExtension(pPath);
            //pPath = pPath.Substring(0, pPath.LastIndexOf(".")) + "\\Output\\" + projectName + ".lst";


            AHC05_cod cod = new AHC05_cod();

            cod.ahc05_codfile_create(ListFileName, pPath);

            pPath = pPath.Substring(0, pPath.LastIndexOf(".")) + ".cod";

            if (File.Exists(pPath))
            {
                FileStream textLoad = new FileStream(pPath, FileMode.Open, FileAccess.Read);
                if (textLoad.CanRead)
                {
                    Encoding encoding = Encoding.Unicode;
                    encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(pPath);
                    listFile.Text = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                    textLoad.Close();

                    //tb.Text = File.ReadAllText(fileName);
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText3);
                }
            }
            else
            {
                MessageBox.Show("COD file:" + pPath + "is can not find!");
                DebugFlag = false;
                return;
            }

        }

        private void load_chc05_codfile() //load chc05 cod file
        {
            string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
            string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);
            string pPath = project_path + "\\Debug\\" + project_name + ".cod";//project_path + "\\Debug\\" + project_name +".cod";

            if (File.Exists(pPath))
            {
                FileStream textLoad = new FileStream(pPath, FileMode.Open, FileAccess.Read);
                if (textLoad.CanRead)
                {
                    Encoding encoding = Encoding.Unicode;
                    encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(pPath);
                    listFile.Text = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                    textLoad.Close();
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText3);
                }
            }
            else
            {
                MessageBox.Show("COD file:" + pPath + "is can not find!");
                DebugFlag = false;
                return;
            }

        }

        private bool SerchLinNume_CHC05(string pcvalue) //c ch05 file
        {

            //[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s+[\w]+[\.](h|c)\b
            string chc05cod = @"[0-9]+\s+\[[^\]]*\]\s+" + pcvalue + @"+\s+[\S]+[\.](h|c)\b";
            string pattern = chc05cod;
            if (DebugFlag == false)
            {

                return false;
            }

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (listFile.Text.Length < textIndex)
                textIndex = 0;
            Match matches = r.Match(listFile.Text, 0);
            textIndex = matches.Index + 20;
            string temp = matches.Value;
            if (temp == "")
            {
                matches = r.Match(listFile.Text, 0);
                textIndex = matches.Index + 20;
                temp = matches.Value;
                if (temp == "")
                {
                    return false;
                }
            }

            r = new Regex("[0-9]+\\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            matches = r.Match(temp, 0);
            string numLine = matches.Value;
            //string numLine = temp.Substring(0, temp.IndexOf("\t"));
            DebugLineNumber = Convert.ToInt32(numLine);

            //get asmfileName
            Regex asmf = new Regex(@"[\S]+[\.](c|h)\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Match asmfstr = asmf.Match(temp, 0);
            string currentAsmfile = asmfstr.Value;

            asmf = new Regex(currentAsmfile, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            if (frmfile.tsFiles.Items.Count == 0)
            {
                for (int i = 0; i < ASMFileName.Count; i++)
                {
                    string str = Path.GetFileName(ASMFileName[i]);
                    Match fequ = asmf.Match(str, 0);
                    if (fequ.Length > 0)
                    {
                        OpenFile_MouseDouble_Click(ASMFileName[i]);
                        break;
                    }
                }

            }
            else if (frmfile.tsFiles.SelectedItem.ToString() != currentAsmfile)
            {
                int i = 0;
                string str_file = frmfile.tsFiles.SelectedItem.ToString();
                Match fequ = asmf.Match(str_file, 0);
                if (fequ.Length == 0)
                {
                    for (i = 0; i < frmfile.tsFiles.Items.Count; i++)
                    {
                        str_file = frmfile.tsFiles.Items[i].ToString();
                        fequ = asmf.Match(str_file, 0);

                        if (fequ.Length > 0)
                        {
                            frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[i];
                            break;
                        }
                    }
                    if (i >= frmfile.tsFiles.Items.Count) //说明已经打开的文件没有
                    {
                        for (int j = 0; j < ASMFileName.Count; j++)
                        {
                            string str = Path.GetFileName(ASMFileName[j]);
                            fequ = asmf.Match(str, 0);
                            if (fequ.Length > 0)
                            {
                                OpenFile_MouseDouble_Click(ASMFileName[j]);
                                break;
                            }
                        }
                    }

                }

            }

            gotoLine_Click(DebugLineNumber);
            DebugFlag = true;

            return true;
        }

        private void SerchLinNume_RISC_cod(string pcValue)
        {

            //string pattern =@"[0-9]+\s+[0-9]+\u0020+[A-Fa-f0-9]+\s+80+[\sC(\.)+]+\s+[\w]+[\.]asm\b"; //RISC list;

            //modify by wj string pattern = @"[0-9]+\s+[0-9]+\u0020+" + pcValue + @"+\s+80+[\sC(\.)+]+\s+[\S]+[\.]asm\b"; //RISC list;
            string pattern = @"[0-9]+\s+[0-9]+\u0020+" + pcValue + @"+\s+80+[\sC(\.)+]+\s+[\S]+[\.]+[a-zA-Z]+\b"; //RISC list;
            //FastColoredTextBox listFile = new FastColoredTextBox();
            if (DebugFlag == false)
            {

                return;
            }

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //listFile.Selection.Start = new Place(0,34);
            // textIndex = 0;
            //regex
            if (listFile.Text.Length < textIndex)
                textIndex = 0;
            Match matches = r.Match(listFile.Text, textIndex);
            textIndex = matches.Index + 20;
            string temp = matches.Value;
            if (temp == "")
            {
                matches = r.Match(listFile.Text, 0);
                textIndex = matches.Index + 20;
                temp = matches.Value;
                if (temp == "")
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText12);
                    return;
                }
            }
            string numLine = temp.Substring(8, 5);
            DebugLineNumber = Convert.ToInt32(numLine);

            //get asmfileName
            //modify by wj Regex asmf = new Regex(@"[\S]+[\.]asm\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Regex asmf = new Regex(@"[\S]+[\.]+[a-zA-Z]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Match asmfstr = asmf.Match(temp, 0);
            string currentAsmfile = asmfstr.Value;

            asmf = new Regex(currentAsmfile, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            if (frmfile.tsFiles.Items.Count == 0)
            {
                int i = 0;
                for (i = 0; i < ASMFileName.Count; i++)
                {
                    string str = Path.GetFileName(ASMFileName[i]);
                    Match fequ = asmf.Match(str, 0);
                    if (str == currentAsmfile)
                    {
                        OpenFile_MouseDouble_Click(ASMFileName[i]);
                        break;
                    }
                }
                if (i >= ASMFileName.Count) //文件链表中不存在，到当前文件中查找
                {
                    String projPath = ProjectPath.Substring(0, ProjectPath.LastIndexOf(@"\")); //获取项目路径
                    string str = projPath + "\\" + currentAsmfile;
                    if (Directory.Exists(str) == true)
                    {
                        OpenFile_MouseDouble_Click(str);
                    }
                }
            }
            else if (frmfile.tsFiles.SelectedItem.ToString() != currentAsmfile)
            {
                int i = 0;
                int j = 0;
                string str_file = frmfile.tsFiles.SelectedItem.ToString();
                Match fequ = asmf.Match(str_file, 0);
                //if (fequ.Length==0)
                if (currentAsmfile != str_file)
                {
                    for (i = 0; i < frmfile.tsFiles.Items.Count; i++)
                    {
                        str_file = frmfile.tsFiles.Items[i].ToString();
                        //fequ = asmf.Match(str_file, 0);

                        if (currentAsmfile == str_file)
                        {
                            frmfile.tsFiles.SelectedItem = frmfile.tsFiles.Items[i];
                            break;
                        }
                    }
                    if (i >= frmfile.tsFiles.Items.Count) //说明已经打开的文件没有
                    {
                        for (j = 0; j < ASMFileName.Count; j++)
                        {
                            string str = Path.GetFileName(ASMFileName[j]);
                            //fequ = asmf.Match(str, 0);
                            //if (fequ.Length>0)
                            if (str == currentAsmfile)
                            {
                                OpenFile_MouseDouble_Click(ASMFileName[j]);
                                break;
                            }
                        }
                    }
                    if (j >= ASMFileName.Count) //文件链表中不存在，到当前文件中查找
                    {
                        String projPath = ProjectPath.Substring(0, ProjectPath.LastIndexOf(@"\")); //获取项目路径
                        string str = projPath + "\\" + currentAsmfile;
                        if (File.Exists(str) == true)
                        {
                            OpenFile_MouseDouble_Click(str);
                        }
                    }
                }

            }


            gotoLine_Click(DebugLineNumber);
            DebugFlag = true;

            //  BreakPointCheck();

        }

        private void SerchLinNume_RISC(string pcValue)
        {
            //MC30P01:
            //[A-Fa-f0-9]+\u0020+[A-Fa-f0-9]+\u0020+[0-9]{5}
            //MC20P24B:
            //[0-9]+\s\[[^\]]*\]\s+[A-Fa-f0-9]{4}

            //string hc05list = @"[A-Fa-f0-9]+\u0020+[A-Fa-f0-9]+\u0020+[0-9]{5}";

            string pattern = pcValue + @"\u0020+[A-Fa-f0-9]+\u0020+[0-9]{5}"; //RISC list;

            FastColoredTextBox listFile = new FastColoredTextBox();

            FileStream textLoad = new FileStream(ListFileName[0], FileMode.Open, FileAccess.Read);
            if (textLoad.CanRead)
            {
                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(ListFileName[0]);
                listFile.Text = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();
            }
            else
            {
                MessageBox.Show(ListFileName[0] + ShowLanguage.Current.MessageBoxText3);
            }

            Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //listFile.Selection.Start = new Place(0,34);
            // textIndex = 0;
            //regex
            Match matches = r.Match(listFile.Text, 0);
            textIndex = matches.Index + 20;
            string temp = matches.Value;
            if (temp == "")
            {
                return;
            }
            string numLine = temp.Substring(temp.Length - 6);
            DebugLineNumber = Convert.ToInt32(numLine);
            if (frmfile.tsFiles.Items.Count == 0)
            {
                OpenFile_MouseDouble_Click(ASMFileName[0]);
            }
            gotoLine_Click(DebugLineNumber);
            DebugFlag = true;
        }

        //private void tempDebugtoolStripButton_Clik(object sender, EventArgs e)
        //{
        //    //RegeditFuntion rg = new RegeditFuntion();
        //    //rg.FilesRelatingEXE(frmMAIN.APPLICATION_PATH + "WinScope IDE.exe", ".Proj");

        //    //string project_path =Path.GetDirectoryName(GetProjectPath());
        //    //string project_name = Path.GetFileNameWithoutExtension(GetProjectPath());

        //    //CHC05_cod tempf = new CHC05_cod();

        //    //tempf.chc05_codfile_create(project_path, project_name);

        //}

        #endregion

        #region Drawing funtions
        void tb_PaintLine(object sender, PaintLineEventArgs e)
        {
            TbInfo info = (sender as FastColoredTextBox).Tag as TbInfo;
            Font font = (sender as FastColoredTextBox).Font as Font;
            int iFontHeight = font.Height;
            //draw bookmark
            Graphics g = e.Graphics;

            Rectangle drawingPosition = new Rectangle(0, 0, iFontHeight, 399);

            Rectangle rect1 = new Rectangle(1, e.LineRect.Top, iFontHeight, iFontHeight);

            g.FillRectangle(SystemBrushes.MenuBar, new Rectangle(drawingPosition.X, rect1.Top, drawingPosition.Width - 1, rect1.Height));
            g.DrawLine(SystemPens.ControlDark, iFontHeight, rect1.Top, iFontHeight, rect1.Bottom);

            if (info.bookmarksLineId.Contains((sender as FastColoredTextBox)[e.LineIndex].UniqueId))
            {
                // e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 30, 30), Color.White, Color.PowderBlue, 45), 0, e.LineRect.Top, 30, 30);
                //e.Graphics.DrawEllipse(Pens.PowderBlue, 0, e.LineRect.Top, 15, 15);
                // e.Graphics.DrawRectangle(Pens.Red, new Rectangle(0, e.LineRect.Top, 30, 30));
                //Rectangle rect = new Rectangle(1, e.LineRect.Top+4, 23, 12);
                //using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                //                                             new Point(rect.Right, rect.Bottom),
                //                                             Color.SkyBlue,
                //                                             Color.SeaGreen))
                //{
                //    FillRoundRect(g, brush, rect);
                //}
                DrawBookmark(g, e.LineRect.Top, iFontHeight, true);

            }
            if (info.EnableBreakpointsLineId.Contains((sender as FastColoredTextBox)[e.LineIndex].UniqueId))
            {
                //e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 20, 20), Color.Red, Color.Gold, 45), 0, e.LineRect.Top, 20, 20);

                DrawBreakpoint(g, e.LineRect.Top, iFontHeight, true, true);
            }
            if (info.DisableBreakpointsLineId.Contains((sender as FastColoredTextBox)[e.LineIndex].UniqueId))
            {
                //e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 20, 20), Color.Red, Color.Gold, 45), 0, e.LineRect.Top, 20, 20);
                //e.Graphics.DrawEllipse(Pens.Red, 0, e.LineRect.Top, 20, 20);
                DrawBreakpoint(g, e.LineRect.Top, iFontHeight, false, true);
            }
            //draw line marker
            if ((e.LineIndex == DebugLineNumber - 1) && (DebugFlag == true))
            {
                // e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 15, 15), Color.LightPink, Color.Red, 90), 0, e.LineRect.Top, 15, 15);
                DrawArrow(g, e.LineRect.Top);
            }
            //draw current line marker
            //if (e.LineIndex == CurrentTB.Selection.Start.iLine)
            //    e.Graphics.FillEllipse(new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 15, 15), Color.LightPink, Color.Red, 90), 0, e.LineRect.Top, 15, 15);
        }

        public void DrawBreakpoint(Graphics g, int y, int diameter, bool isEnabled, bool isHealthy)
        {
            //int diameter = Math.Min(iconBarWidth - 2, textArea.TextView.FontHeight);
            //int diameter = Math.Min(23,16);
            Rectangle rect = new Rectangle(1,
                                           y,
                                           diameter - 1,
                                           diameter - 1);


            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush pthGrBrush = new PathGradientBrush(path))
                {
                    pthGrBrush.CenterPoint = new PointF(rect.Left + rect.Width / 3, rect.Top + rect.Height / 3);
                    pthGrBrush.CenterColor = Color.MistyRose;
                    Color[] colors = { isHealthy ? Color.Firebrick : Color.Olive };
                    pthGrBrush.SurroundColors = colors;

                    if (isEnabled)
                    {
                        g.FillEllipse(pthGrBrush, rect);
                    }
                    else
                    {
                        g.FillEllipse(SystemBrushes.Control, rect);
                        using (Pen pen = new Pen(pthGrBrush))
                        {
                            g.DrawEllipse(pen, new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2));
                        }
                    }
                }
            }
        }

        public void DrawBookmark(Graphics g, int y, int delta, bool isEnabled)
        {
            //int delta = 16/8;
            Rectangle rect = new Rectangle(1, y + delta / 4, delta, delta / 2);

            if (isEnabled)
            {
                using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                                                             new Point(rect.Right, rect.Bottom),
                                                             Color.SkyBlue,
                                                             Color.White))
                {
                    FillRoundRect(g, brush, rect);
                }
            }
            else
            {
                FillRoundRect(g, Brushes.White, rect);
            }
            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                                                         new Point(rect.Right, rect.Bottom),
                                                         Color.SkyBlue,
                                                         Color.Blue))
            {
                using (Pen pen = new Pen(brush))
                {
                    DrawRoundRect(g, pen, rect);
                }
            }
        }

        void DrawRoundRect(Graphics g, Pen p, Rectangle r)
        {
            using (GraphicsPath gp = CreateRoundRectGraphicsPath(r))
            {
                g.DrawPath(p, gp);
            }
        }

        void FillRoundRect(Graphics g, Brush b, Rectangle r)
        {
            using (GraphicsPath gp = CreateRoundRectGraphicsPath(r))
            {
                g.FillPath(b, gp);
            }
        }

        GraphicsPath CreateRoundRectGraphicsPath(Rectangle r)
        {
            GraphicsPath gp = new GraphicsPath();
            int radius = r.Width / 2;
            gp.AddLine(r.X + radius, r.Y, r.Right - radius, r.Y);
            gp.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90);

            gp.AddLine(r.Right, r.Y + radius, r.Right, r.Bottom - radius);
            gp.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);

            gp.AddLine(r.Right - radius, r.Bottom, r.X + radius, r.Bottom);
            gp.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90);

            gp.AddLine(r.X, r.Bottom - radius, r.X, r.Y + radius);
            gp.AddArc(r.X, r.Y, radius, radius, 180, 90);

            gp.CloseFigure();
            return gp;
        }

        public void DrawArrow(Graphics g, int y)
        {
            int delta = 16 / 8; //textArea.TextView.FontHeight / 8;
            //Rectangle rect = new Rectangle(1, y + delta, base.drawingPosition.Width - 4, textArea.TextView.FontHeight - delta * 2);
            Rectangle rect = new Rectangle(1, y + delta, 23, 12);

            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                                                         new Point(rect.Right, rect.Bottom),
                                                         Color.LightYellow,
                                                         Color.Yellow))
            {
                FillArrow(g, brush, rect);
            }

            using (Brush brush = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                                                         new Point(rect.Right, rect.Bottom),
                                                         Color.Yellow,
                                                         Color.Brown))
            {
                using (Pen pen = new Pen(brush))
                {
                    DrawArrow(g, pen, rect);
                }
            }
        }

        GraphicsPath CreateArrowGraphicsPath(Rectangle r)
        {
            GraphicsPath gp = new GraphicsPath();
            int halfX = r.Width / 2;
            int halfY = r.Height / 2;
            gp.AddLine(r.X, r.Y + halfY / 2, r.X + halfX, r.Y + halfY / 2);
            gp.AddLine(r.X + halfX, r.Y + halfY / 2, r.X + halfX, r.Y);
            gp.AddLine(r.X + halfX, r.Y, r.Right, r.Y + halfY);
            gp.AddLine(r.Right, r.Y + halfY, r.X + halfX, r.Bottom);
            gp.AddLine(r.X + halfX, r.Bottom, r.X + halfX, r.Bottom - halfY / 2);
            gp.AddLine(r.X + halfX, r.Bottom - halfY / 2, r.X, r.Bottom - halfY / 2);
            gp.AddLine(r.X, r.Bottom - halfY / 2, r.X, r.Y + halfY / 2);
            gp.CloseFigure();
            return gp;
        }
        void DrawArrow(Graphics g, Pen p, Rectangle r)
        {
            using (GraphicsPath gp = CreateArrowGraphicsPath(r))
            {
                g.DrawPath(p, gp);
            }
        }

        void FillArrow(Graphics g, Brush b, Rectangle r)
        {
            using (GraphicsPath gp = CreateArrowGraphicsPath(r))
            {
                g.FillPath(b, gp);
            }
        }
        #endregion

        #region WatchDataUpdate
        private void WatchDataUpdate()
        {
            if (DebugFlag == false)
            {
                return;
            }

            if (!frmWatch.IsDisposed)
            {
                int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1;
                for (int i = 0; i < datagrid_len; i++)
                {
                    string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[0].Value.ToString();
                    frmWatch.Watch_add_symbol(addr_temp, i);
                }
            }
        }
        #endregion


        #region BreakpointCheck
        private void BreakPointCheck()
        {
            //FastColoredTextBox listFile = new FastColoredTextBox();
            if (DebugFlag == false)
            {
                return;
            }
            else if (listFile.Text.Length == 0)
            {
                return;
            }

            string line_number = null;

            //RISC list;
            int bk_counter = 0;
            if (frmBreakPoint.IsDisposed != true)
            {
                for (int j = 0; j < 16; j++)
                {
                    BreakPointList[j] = 0xffff;
                    frmBreakPoint.myDataGridViewBK.Rows[j].Cells[1].Value = 0xffff;
                    frmBreakPoint.myDataGridViewBK.Rows[j].Cells[0].Value = 0;
                    frmBreakPoint.myDataGridViewBK.Rows[j].Cells[2].Value = " ";

                }
            }


            foreach (Control tab in frmfile.tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                TbInfo info = tb.Tag as TbInfo;

                for (int i = 0; i < info.EnableBreakpoints.Count; i++)
                {
                    // get the asm line number
                    int id = info.EnableBreakpoints[i];
                    int a = 0;
                    for (; a < tb.LinesCount; a++)
                    {
                        if (tb[a].UniqueId == id)
                            break;
                    }
                    if (a >= tb.LinesCount)
                    {
                        continue;
                    }
                    a = a + 1;

                    string asm = Path.GetFileNameWithoutExtension(tab.Tag as String);

                    String pattern = null;
                    if (MCU_TYPE == "RISC")
                    {
                        line_number = a.ToString("D5");
                        //[0-9]+\s+00015+\u0020+[A-Fa-f0-9]+\s+80+[\sC(\.)+]+\s+sub2\.asm
                        pattern = @"[0-9]+\s+" + line_number + @"+\u0020+[A-Fa-f0-9]+\s+80+[\sC(\.)+]+\s+" + asm + @"\.asm";
                    }
                    else if (MCU_TYPE == "AHC05")
                    {
                        line_number = a.ToString();
                        //[0-9]+\s\[[^\]]*\]\s+[A-Fa-f0-9]{4}
                        pattern = "\\s" + line_number + @"\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s+" + asm + @"\.asm";
                    }
                    else //CHC05
                    {

                        line_number = a.ToString();

                        //[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s+[\w]+[\.](h|c)\b
                        pattern = "\\s" + line_number + @"\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s+" + asm + @"+[\.](h|c)\b";

                    }

                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                    //regex
                    Match matches = r.Match(listFile.Text, 0);
                    if (matches.Length == 0)
                    {
                        MessageBox.Show(asm + ShowLanguage.Current.MessageBoxText13 + line_number + ShowLanguage.Current.MessageBoxText14);
                        //remove BK
                        info.EnableBreakpoints.Remove(id);
                        info.EnableBreakpointsLineId.Remove(id);

                        //repaint
                        tb.Invalidate();
                    }
                    else
                    {
                        string pc_addr = matches.Value;
                        if (MCU_TYPE == "RISC")
                        {
                            pc_addr = pc_addr.Substring(16, 4);
                        }
                        else
                        {
                            r = new Regex(@"[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            matches = r.Match(pc_addr, 0);
                            pc_addr = matches.Value;
                            pc_addr = pc_addr.Substring(pc_addr.Length - 4, 4);
                        }
                        bk_counter = bk_counter + 1;
                        if (bk_counter > 15)
                        {
                            MessageBox.Show(ShowLanguage.Current.MessageBoxText15);
                            info.EnableBreakpoints.Remove(id);
                            info.EnableBreakpointsLineId.Remove(id);

                            //repaint
                            tb.Invalidate();

                        }
                        else
                        {
                            BreakPointList[bk_counter] = Convert.ToUInt16(pc_addr, 16);
                            if (frmBreakPoint.IsDisposed != true)
                            {
                                frmBreakPoint.myDataGridViewBK.Rows[bk_counter].Cells[1].Value = pc_addr;
                                frmBreakPoint.myDataGridViewBK.Rows[bk_counter].Cells[0].Value = line_number;
                                frmBreakPoint.myDataGridViewBK.Rows[bk_counter].Cells[2].Value = asm + ".asm";
                            }

                        }
                    }
                }
            }

            //send data to sn-link
            fs_SendBK2Link();
        }

        private void fs_SendBK2Link()
        {
            for (int i = 0; i < 16; i++)
            {
                //frmBreakPoint.myDataGridViewBK.Rows[i].Cells[1].Value = 0xffff;
                UInt16 bk_value = BreakPointList[i];
                MCU_Breakpoint[i * 2 + 7] = (byte)(bk_value % 0x100);
                MCU_Breakpoint[i * 2 + 8] = (byte)(bk_value / 0x100);
            }
            if (DebugFlag)
            {
                //COMPORT.OpenPort();
                //#if debug_RS232
                //            COMPORT.OpenPort();
                //            //COMPORT.ClosePort();
                //#endif
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
                    int len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                //#if debug_RS232
                //            //COMPORT.OpenPort();
                //            COMPORT.ClosePort();
                //#endif
                //COMPORT.ClosePort();
            }

        }


        #endregion


        #region read ram pc reg
        private void AHC05_ReadCPU()
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle NchangeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            NchangeByteStyle.ForeColor = Color.Black;

            byte[] CMD_ReadCPU = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x90, 0x00, 0x07, 0x00, 0x30 };

            int len = 0;
            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(CMD_ReadCPU);
                //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;

                while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

            }
            //COMPORT.OpenPort();
            // asm list line
            int sysPC = ReadDataBuffer[0x04] + ReadDataBuffer[0x05] * 0x100;
            string pcValue = sysPC.ToString("X4");



            //time 74ms
            SerchLinNume_HC05(pcValue); //time 77ms

            if (!frmSys.IsDisposed) //88ms
            {

                //sys value display A
                string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                frmSys.propertyGridEx_sys.Item[2].Value = sysTepm;

                //PC
                //int sysStack = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                sysTepm = "0x" + pcValue;// sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[0].Value = sysTepm;

                //sp pointer
                byte sp_temp = (byte)(ReadDataBuffer[3] | 0xc0);
                //sysTepm = "0x" + ReadDataBuffer[3].ToString("X2");
                sysTepm = "0x" + sp_temp.ToString("X2");
                frmSys.propertyGridEx_sys.Item[1].Value = sysTepm;
                //X
                //int sysStack = ReadDataBuffer[0x30] + ReadDataBuffer[0x31] * 0x100;
                sysTepm = "0x" + ReadDataBuffer[1].ToString("X2");
                frmSys.propertyGridEx_sys.Item[3].Value = sysTepm;

                //C
                sysTepm = "0x" + ReadDataBuffer[2].ToString("X2");
                frmSys.propertyGridEx_sys.Item[4].Value = sysTepm;

                //bit:4 H
                int c = ReadDataBuffer[2];
                //frmSys.propertyGridEx_sys.Item[8].Value = (status >> 7) & 1;

                frmSys.propertyGridEx_sys.Item[5].Value = (c >> 4) & 1;
                //bit:3 I                
                frmSys.propertyGridEx_sys.Item[6].Value = (c >> 3) & 1;
                //bit:2 N
                frmSys.propertyGridEx_sys.Item[7].Value = (c >> 2) & 1;
                //bit:1 z
                frmSys.propertyGridEx_sys.Item[8].Value = (c >> 1) & 1;
                //bit:0 c
                frmSys.propertyGridEx_sys.Item[9].Value = (c >> 0) & 1;

                frmSys.propertyGridEx_sys.Refresh();
            }
            //---------------------------------------------------------------------------------88ms
            byte[] CMD_ReadRAM = { 0x69, 0x03, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02 };

            if (COMPORT.port1.IsOpen)
            {
                COMPORT.SendCommand(CMD_ReadRAM);
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;

                while (COMPORT.port1.BytesToRead < DeviceConfigXX.MCU_RAMSize)
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
                //while (COMPORT.port1.BytesToRead < 0x15) ;
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

            }
            int count = 0;
            //---------------------------------------------------------------------------------330ms

            //reflash
            if (!frmRAM.IsDisposed)
            {
                for (int i = 0; i < DeviceConfigXX.MCU_RAMSize / 16; i++)
                //for (int i = 0; i < 0xff/ 16; i++)
                {
                    for (int j = 1; j < 17; j++)
                    {
                        //object ram_data = frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value;
                        int old = Convert.ToInt32(frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value.ToString());
                        int newdata = ReadDataBuffer[count];
                        // int c = frmRAM.myDataGridViewRAM.CurrentCellAddress.X;
                        //int r = frmRAM.myDataGridViewRAM.CurrentCellAddress.Y;
                        if (old == newdata)
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = NchangeByteStyle;
                        }
                        else
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = changeByteStyle;
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value = newdata;
                        }

                        count = count + 1;
                    }
                    // myDataGridViewRAM.Rows[i].Cells[0].Value = i * 16;
                    //myDataGridViewRAM.Rows[i].Cells[0].ReadOnly = true;
                }
            }
            //-----------------------------------------------------------------------338ms
            for (int i = 0; i < DeviceConfigXX.aMCU_RAMSize; i++)
            {
                RAMDataBuffer[i] = ReadDataBuffer[i];
            }
            //reflash reg display
            if (!frmREG.IsDisposed)
            {
                int datagrid_len = frmREG.dataGridView_reg.Rows.Count;
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[2].Value.ToString());
                    int datagrid_addr = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = ReadDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (old == newdata)
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = changeByteStyle;
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Value = newdata;
                    }
                }


            }

            //reflash watch display
            if (!frmWatch.IsDisposed)
            {
                int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1; // the last line is idle
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = 0;
                    string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString();
                    if (addr_temp != "??" && addr_temp != "Error:not found")
                    {
                        old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString());
                    }

                    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString() == "??")
                    {
                        continue;
                    }
                    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = RAMDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[3].Value.ToString()))
                    {
                        string s = Convert.ToString(newdata, 2).PadLeft(8, '0').Substring(int.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[4].Value.ToString()), 1);
                        newdata = Convert.ToInt32(s);
                    }
                    if (!bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[7].Value.ToString()))
                    {
                        int iDataRes = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[6].Value.ToString(), 10);
                        for (int k = 1; k < iDataRes; k++)
                        {
                            int newdataH = RAMDataBuffer[datagrid_addr + k];
                            newdata += newdataH * (int)Math.Pow(0x100, k);
                        }
                    }
                    if (old == newdata)
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                    }
                    else
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                    }
                }


            }

            if (!frmBreakPoint.IsDisposed)
            {
                byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x91, 0x00, 0x04, 0x00, 0x65 };

                COMPORT.SendCommand(CMD_ReadOption);
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 4)
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                byte[] CMD_ReadBK = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x8f, 0x00, 32, 0x00, 0x65 };
                COMPORT.SendCommand(CMD_ReadBK);

                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 32)
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //reflash BK
                for (int i = 0; i < 16; i++)
                {
                    MCU_Breakpoint[i * 2 + 7] = ReadDataBuffer[i * 2];
                    MCU_Breakpoint[i * 2 + 8] = ReadDataBuffer[i * 2 + 1];
                    frmBreakPoint.myDataGridViewBK.Rows[i].Cells[1].Value = MCU_Breakpoint[i * 2 + 7] + MCU_Breakpoint[i * 2 + 8] * 0x100;
                }
            }

            //read instruct time
            byte[] CMD_ReadTime = { 0x69, 0x13, 0x02, 0x91, 0x00 };
            COMPORT.SendCommand(CMD_ReadTime);
            DataReceiveTimer.Enabled = true;
            TimeOutFlag = 0;
            while (COMPORT.port1.BytesToRead < 4)
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
            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            int time_temp = 0;
            double time_temp1 = 0;
            time_temp = ReadDataBuffer[2] * 0x100 + ReadDataBuffer[3] + 3;
            time_temp1 = (double)time_temp / 72;
            InstructTime_lab.Text = ShowLanguage.Current.RunTime + ": " + time_temp1.ToString();// Convert.ToString(time_temp1);


        }

        private void CHC05_ReadCPU()
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle NchangeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            NchangeByteStyle.ForeColor = Color.Black;

            byte[] CMD_ReadCPU = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x90, 0x00, 0x07, 0x00, 0x30 };


            int sysPC = 0;
            int len = 0;
            string pcValue = null;
            // SerchLinNume_HC05(pcValue); //time 77ms
            while (true)
            {
                //COMPORT.OpenPort();
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMD_ReadCPU);
                    //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
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
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }


                // asm list line
                sysPC = ReadDataBuffer[0x04] + ReadDataBuffer[0x05] * 0x100;
                pcValue = sysPC.ToString("X4");

                if (SerchLinNume_CHC05(pcValue))
                {
                    break;
                }
                else
                {
                    byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x19 };

                    CMDdata[3] = DEBUG_COMMAND.CMD_StepInto;
                    CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(CMDdata);
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < 4)
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
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                    }

                }
            }

            if (!frmSys.IsDisposed) //88ms
            {

                //sys value display A
                string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                frmSys.propertyGridEx_sys.Item[2].Value = sysTepm;

                //PC
                //int sysStack = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                sysTepm = "0x" + pcValue;// sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[0].Value = sysTepm;

                //sp pointer
                byte sp_temp = (byte)(ReadDataBuffer[3] | 0xc0);
                //sysTepm = "0x" + ReadDataBuffer[3].ToString("X2");
                sysTepm = "0x" + sp_temp.ToString("X2");

                frmSys.propertyGridEx_sys.Item[1].Value = sysTepm;
                //X
                //int sysStack = ReadDataBuffer[0x30] + ReadDataBuffer[0x31] * 0x100;
                sysTepm = "0x" + ReadDataBuffer[1].ToString("X2");
                frmSys.propertyGridEx_sys.Item[3].Value = sysTepm;

                //C
                sysTepm = "0x" + ReadDataBuffer[2].ToString("X2");
                frmSys.propertyGridEx_sys.Item[4].Value = sysTepm;

                //bit:4 H
                int c = ReadDataBuffer[2];
                //frmSys.propertyGridEx_sys.Item[8].Value = (status >> 7) & 1;

                frmSys.propertyGridEx_sys.Item[5].Value = (c >> 4) & 1;
                //bit:3 I                
                frmSys.propertyGridEx_sys.Item[6].Value = (c >> 3) & 1;
                //bit:2 N
                frmSys.propertyGridEx_sys.Item[7].Value = (c >> 2) & 1;
                //bit:1 z
                frmSys.propertyGridEx_sys.Item[8].Value = (c >> 1) & 1;
                //bit:0 c
                frmSys.propertyGridEx_sys.Item[9].Value = (c >> 0) & 1;

                frmSys.propertyGridEx_sys.Refresh();
            }
            //---------------------------------------------------------------------------------88ms
            byte[] CMD_ReadRAM = { 0x69, 0x03, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };

            if (COMPORT.port1.IsOpen)
            {

                COMPORT.SendCommand(CMD_ReadRAM);
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < DeviceConfigXX.MCU_RAMSize)
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            }
            int count = 0;
            //---------------------------------------------------------------------------------330ms
            for (int i = 0; i < DeviceConfigXX.aMCU_RAMSize; i++)
            {
                RAMDataBuffer[i] = ReadDataBuffer[i];
            }
            //reflash
            if (!frmRAM.IsDisposed)
            {
                for (int i = 0; i < DeviceConfigXX.MCU_RAMSize / 16; i++)
                //for (int i = 0; i < 0xff/ 16; i++)
                {
                    for (int j = 1; j < 17; j++)
                    {
                        //object ram_data = frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value;
                        int old = Convert.ToInt32(frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value.ToString());
                        int newdata = ReadDataBuffer[count];
                        // int c = frmRAM.myDataGridViewRAM.CurrentCellAddress.X;
                        //int r = frmRAM.myDataGridViewRAM.CurrentCellAddress.Y;
                        if (old == newdata)
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = NchangeByteStyle;
                        }
                        else
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = changeByteStyle;
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value = newdata;
                        }

                        count = count + 1;
                    }
                    // myDataGridViewRAM.Rows[i].Cells[0].Value = i * 16;
                    //myDataGridViewRAM.Rows[i].Cells[0].ReadOnly = true;
                }
            }
            //-----------------------------------------------------------------------338ms

            //reflash reg display
            if (!frmREG.IsDisposed)
            {
                int datagrid_len = frmREG.dataGridView_reg.Rows.Count;
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[2].Value.ToString());
                    int datagrid_addr = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = ReadDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (old == newdata)
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = changeByteStyle;
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Value = newdata;
                    }
                }


            }

            //reflash watch display
            if (!frmWatch.IsDisposed)
            {
                int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1; // the last line is idle
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = 0;
                    string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString();
                    if (addr_temp != "??" && addr_temp != "Error:not found")
                    {
                        old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString());
                    }

                    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString() == "??")
                    {
                        continue;
                    }
                    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = RAMDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[3].Value.ToString()))
                    {
                        string s = Convert.ToString(newdata, 2).PadLeft(8, '0').Substring(int.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[4].Value.ToString()), 1);
                        newdata = Convert.ToInt32(s);
                    }
                    if (!bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[7].Value.ToString()))
                    {
                        int iDataRes = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[6].Value.ToString(), 10);
                        for (int k = 1; k < iDataRes; k++)
                        {
                            int newdataH = RAMDataBuffer[datagrid_addr + k];
                            newdata += newdataH * (int)Math.Pow(0x100, k);
                        }
                    }
                    if (old == newdata)
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                    }
                    else
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                    }
                }

            }


        }

        private void RISC_ReadCPU_3394()
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle NchangeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            NchangeByteStyle.ForeColor = Color.Black;

            if (MCU_ID == "0x5222")
            {
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
                    int len1 = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                }
            }

            
            //if (MCU_ID == "0x7333")
            //{
                byte[] CMD_ReadCPU = {0x69,0x03,0x07,0x00,0x01,0x02,0x00,0x20,0x00,0x30};   
            //}
            /*else
            {
                CMD_ReadCPU = { 0x69, 0x03, 0x07, 0x00, 0x01, 0x02, 0x00, 0x20, 0x00, 0x30 };// command:0x03,0x07 length
            }*/
            int len = 0;
            if (MCU_TYPE == "RISC")
            {
                //COMPORT.OpenPort();
                if (COMPORT.port1.IsOpen)
                {
                    if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")  // add by LYL for A380 堆栈问题
                    {
                        CMD_ReadCPU[7] = 0x40;          //
                    }
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
                    len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                }

                // asm list line
                /*if (MCU_ID == "0x7311")
                {
                    int sysPC = ReadDataBuffer[0x00] + ReadDataBuffer[0x01] * 0x100 +1;
                    string pcValue = sysPC.ToString("X4");
                    SerchLinNume_RISC_cod(pcValue);
                }*/
                int sysPC = ReadDataBuffer[0x00] + ReadDataBuffer[0x01] * 0x100;
                string pcValue = sysPC.ToString("X4");


                // SerchLinNume_RISC(pcValue);
                 SerchLinNume_RISC_cod(pcValue);

                ////read rom
                // frmROM.ROMDataDisplay.Text = "";
                // byte[] CMD_ReadROM = { 0x69, 0x02, 0x03,0x00,0x00,0x00,0x00,0xff,0x00};
                // if (COMPORT.port1.IsOpen)
                // {
                //     COMPORT.SendCommand(CMD_ReadROM);
                //     //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                //     DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //     DataReceiveTimer.Enabled = true;
                //     TimeOutFlag = 0;
                //     while (COMPORT.port1.BytesToRead < CMD_ReadROM[7])
                //     {
                //         if (TimeOutFlag == 0xaa)
                //         {
                //             DataReceiveTimer.Enabled = false;
                //             ReadDataBuffer[2] = 0xf2;
                //             break;
                //         }
                //     }
                //     DataReceiveTimer.Enabled = false;
                //     if (TimeOutFlag == 0xaa)
                //     {
                //         CloseRS232();
                //         MessageBox.Show("连接通信超时，请重新进入调试模式！", "接收数据超时", MessageBoxButtons.OK, MessageBoxIcon.Error,
                //             MessageBoxDefaultButton.Button1);
                //         return;
                //     }
                //     len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                // }


                // if (ROMDATA_flag==false)
                // {
                //     ROMDATA_flag = true;
                //     for (int i = 0; i < (CMD_ReadROM[7] + CMD_ReadROM[8] * 0x100); i++)
                //     {
                //         ROMData[i] = ReadDataBuffer[i];
                //     }
                // }

                // //ROMDisplayrichTextBox.AppendText("0000" + "\t");
                // for (int i = 0x0000; i <(CMD_ReadROM[7]+CMD_ReadROM[8]*0x100); )
                // {
                //     //for (int aaa = 0; aaa < 16; aaa++)
                //     //{
                //     //    //ROMDisplayrichTextBox.AppendText(Convert.ToString(ASM05.ROMData[6144+i],16)+"\t");
                //     //    frmROM.ROMDataDisplay.AppendText(ReadDataBuffer[i].ToString("X2"));
                //     //    frmROM.ROMDataDisplay.AppendText( " ");
                //     //    i++;
                //     //}
                //     //frmROM.ROMDataDisplay.AppendText("\n");

                //     if (ReadDataBuffer[i]!=ROMData[i])
                //     {
                //         frmROM.ROMDataDisplay.AppendText(i.ToString("X4") +":");
                //         frmROM.ROMDataDisplay.AppendText(ROMData[i].ToString("X2")+" ");
                //         frmROM.ROMDataDisplay.AppendText(ReadDataBuffer[i].ToString("X2"));
                //         frmROM.ROMDataDisplay.AppendText("\n");

                //         ROMData[i] = ReadDataBuffer[i];
                //     }

                //     i++;

                // }

            }
            else
            {
                while (true)
                {
                    //COMPORT.OpenPort();
                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(CMD_ReadCPU);
                        //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
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
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                    }

                    // asm list line
                    int sysPC = ReadDataBuffer[0x00] + ReadDataBuffer[0x01] * 0x100;
                    string pcValue = sysPC.ToString("X4");
                    //// asm list line
                    //sysPC = ReadDataBuffer[0x04] + ReadDataBuffer[0x05] * 0x100;
                    //pcValue = sysPC.ToString("X4");

                    if (SerchLinNume_CHC05(pcValue))
                    {
                        break;
                    }
                    else
                    {
                        byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x19 };

                        CMDdata[3] = DEBUG_COMMAND.CMD_StepInto;
                        CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(CMDdata);
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;
                            while (COMPORT.port1.BytesToRead < 4)
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
                            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        }

                    }
                }

            }

            if (!frmSys.IsDisposed)
            {

                //    ////sys value display A
                string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                //frmSys.propertyGridEx_sys.Item[0].Value = sysTepm;

                int sysStack = ReadDataBuffer[0x0] + ReadDataBuffer[0x01] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[0].Value = sysTepm;

                //    ////sp pointer
                //    //sysTepm = "0x" + ReadDataBuffer[0x20].ToString("X2");
                //    //frmSys.propertyGridEx_sys.Item[2].Value = sysTepm;
                if (MCU_ID == "0x7011" || MCU_ID == "0x7031" || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                {
                    
                }
                else
                {
                    //Stack1
                    sysStack = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[3].Value = sysTepm;

                    //Stack2
                    sysStack = ReadDataBuffer[0x12] + ReadDataBuffer[0x13] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[4].Value = sysTepm;

                    //Stack3
                    sysStack = ReadDataBuffer[0x14] + ReadDataBuffer[0x15] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[5].Value = sysTepm;

                    //Stack4
                    sysStack = ReadDataBuffer[0x16] + ReadDataBuffer[0x17] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[6].Value = sysTepm;
                }

                if (MCU_ID == "0x3401" || MCU_ID == "0x0311" || MCU_ID == "0x6060" || MCU_ID == "0x6080" || MCU_ID == "0x6220")
                {
                    //Stack5
                    sysStack = ReadDataBuffer[0x18] + ReadDataBuffer[0x19] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;
                }

                if (MCU_ID == "0x7212")
                {
                    //Stack5
                    sysStack = ReadDataBuffer[0x18] + ReadDataBuffer[0x19] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;
                    //Stack6
                    sysStack = ReadDataBuffer[0x1a] + ReadDataBuffer[0x1b] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[8].Value = sysTepm;
                }
                if (MCU_ID == "0x9902")
                {
                    //Stack5
                    sysStack = ReadDataBuffer[0x18] + ReadDataBuffer[0x19] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;
                    //Stack6
                    sysStack = ReadDataBuffer[0x1a] + ReadDataBuffer[0x1b] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[8].Value = sysTepm;
                }
                if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122") //add by LYL for A380 堆栈地址跟其他芯片有区别
                {
                    //Stack1
                    sysStack = ReadDataBuffer[0x20] + ReadDataBuffer[0x21] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[3].Value = sysTepm;
                    //Stack2
                    sysStack = ReadDataBuffer[0x22] + ReadDataBuffer[0x23] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[4].Value = sysTepm;
                    //Stack3
                    sysStack = ReadDataBuffer[0x24] + ReadDataBuffer[0x25] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[5].Value = sysTepm;
                    //Stack4
                    sysStack = ReadDataBuffer[0x26] + ReadDataBuffer[0x27] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[6].Value = sysTepm;
                    //Stack5
                    sysStack = ReadDataBuffer[0x28] + ReadDataBuffer[0x29] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;
                    //Stack6
                    sysStack = ReadDataBuffer[0x2a] + ReadDataBuffer[0x2b] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[8].Value = sysTepm;
                    //Stack7
                    sysStack = ReadDataBuffer[0x2c] + ReadDataBuffer[0x2d] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[9].Value = sysTepm;
                    //Stack8
                    sysStack = ReadDataBuffer[0x2e] + ReadDataBuffer[0x2f] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[10].Value = sysTepm;
                }

                if (MCU_ID == "0x3264" || MCU_ID == "0x32821" || MCU_ID == "0x3316" || MCU_ID == "0x7311" ||
                    MCU_ID == "0x3378" || MCU_ID == "0x8132" || MCU_ID == "0x5222" || MCU_ID == "0x7323" || 
                    MCU_ID == "0x7321" || MCU_ID == "0x7312")// TODO
                {
                    //Stack5
                    sysStack = ReadDataBuffer[0x18] + ReadDataBuffer[0x19] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;
                    //Stack6
                    sysStack = ReadDataBuffer[0x1a] + ReadDataBuffer[0x1b] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[8].Value = sysTepm;

                    //Stack7
                    sysStack = ReadDataBuffer[0x1c] + ReadDataBuffer[0x1d] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[9].Value = sysTepm;

                    //Stack8
                    sysStack = ReadDataBuffer[0x1e] + ReadDataBuffer[0x1f] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[10].Value = sysTepm;
                }

                frmSys.propertyGridEx_sys.Refresh();
            }

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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

            }

            if (!frmSys.IsDisposed)
            {
                //    ////sys value display A
                if (MCU_ID == "0x7011" || MCU_ID == "0x7031" || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722")
                {
                    string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                    frmSys.propertyGridEx_sys.Item[1].Value = sysTepm;
                }
                else
                {
                    string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                    frmSys.propertyGridEx_sys.Item[2].Value = sysTepm;

                    //sp pointer
                    if (MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")
                    {
                        sysTepm = "0x" + ReadDataBuffer[0x02].ToString("X2");
                        frmMAIN.SP_Value = ReadDataBuffer[0x02];
                    }
                    else
                    {
                        sysTepm = "0x" + ReadDataBuffer[0x01].ToString("X2");
                        frmMAIN.SP_Value = ReadDataBuffer[0x01];
                    }
                    frmSys.propertyGridEx_sys.Item[1].Value = sysTepm;
                }

                frmSys.propertyGridEx_sys.Refresh();
            }


            byte[] CMD_ReadRAM = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x00, 0x00, 0x50, 0x00, 0x2a };

            if (MCU_ID == "0x0314" || MCU_ID == "0x3221" || MCU_ID == "0x32821" || MCU_ID == "0x3220"
                || MCU_ID == "0x3394" || MCU_ID == "0x3264" || MCU_ID == "0x3378" || MCU_ID == "0x3316"
                || MCU_ID == "0x5312" || MCU_ID == "0x7212" || MCU_ID == "0x7022" || MCU_ID == "0x7510"
                || MCU_ID == "0x6060" || MCU_ID == "0x3222" || MCU_ID == "0x8132" || MCU_ID == "0x7311"
                || MCU_ID == "0x7011" || MCU_ID == "0x7511" || MCU_ID == "0x5222" || MCU_ID == "0x7323"
                || MCU_ID == "0x7031" || MCU_ID == "0x6080" || MCU_ID == "0x6220" || MCU_ID == "0x7321" || MCU_ID == "0x7312"
                || MCU_ID == "0x7041" || MCU_ID == "0x9905" || MCU_ID == "0x6021" || MCU_ID == "0x2722"
                || MCU_ID == "0x9902" || MCU_ID == "0x7333" || MCU_ID == "0x9903" || MCU_ID == "0x9904" || MCU_ID == "0x7122")// TODO wj
            {
                CMD_ReadRAM[7] = (byte)(DeviceConfigXX.MCU_RAMSize % 0x100);
                CMD_ReadRAM[8] = (byte)(DeviceConfigXX.MCU_RAMSize / 0x100);

                //rlength = 0x134;
            }
            int count = 0;
            bool bset = true;
            if (MCU_ID == "0x8132")
            {
                for (int i = 0; i < 20; i++)
                {
                    if (i > 15)
                    {
                        CMD_ReadRAM[5] = 0x01;
                        if (bset)
                        {
                            count = 0;
                            bset = false;
                        }
                    }
                    for (int j = 0; j < 16; j++)
                    {
                        CMD_ReadRAM[6] = (byte)(count);
                        CMD_ReadRAM[7] = 0x01;
                        CMD_ReadRAM[8] = 0x00;
                        count = count + 1;
                        if (j == 7 || j == 15)
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j + 1].ReadOnly = true;
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j + 1].Style.BackColor = Color.Gray;
                            continue;
                        }
                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(CMD_ReadRAM);
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;
                            while (COMPORT.port1.BytesToRead < 1)
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
                                MessageBox.Show("Read Ram error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                                return;
                            }
                        }
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        RAMDataBuffer[CMD_ReadRAM[5] * 256 + count - 1] = ReadDataBuffer[0];
                    }
                }
                CMD_ReadRAM[5] = 0x01;
                CMD_ReadRAM[6] = 0x40;
                CMD_ReadRAM[7] = 0xc0;
                CMD_ReadRAM[8] = 0x00;
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMD_ReadRAM);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < 192)
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
                        MessageBox.Show("Read Ram error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                for (int i = 320; i < DeviceConfigXX.aMCU_RAMSize; i++)
                {
                    RAMDataBuffer[i] = ReadDataBuffer[i - 320];
                }
            }
            else
            {
                if (COMPORT.port1.IsOpen)
                {
                    COMPORT.SendCommand(CMD_ReadRAM);
                    DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    DataReceiveTimer.Enabled = true;
                    TimeOutFlag = 0;
                    while (COMPORT.port1.BytesToRead < DeviceConfigXX.MCU_RAMSize)
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
                        MessageBox.Show("Read Ram error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                for (int i = 0; i < DeviceConfigXX.aMCU_RAMSize; i++)
                {
                    RAMDataBuffer[i] = ReadDataBuffer[i];
                }
            }
            count = 0;
            //reflash
            if (!frmRAM.IsDisposed)
            {
                for (int i = 0; i < DeviceConfigXX.MCU_RAMSize / 16; i++)
                {
                    for (int j = 1; j < 17; j++)
                    {
                        int old = Convert.ToInt32(frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value.ToString());
                        int newdata = RAMDataBuffer[count];
                        // int c = frmRAM.myDataGridViewRAM.CurrentCellAddress.X;
                        //int r = frmRAM.myDataGridViewRAM.CurrentCellAddress.Y;
                        if (old == newdata)
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = NchangeByteStyle;
                        }
                        else
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = changeByteStyle;
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value = newdata;
                        }

                        count = count + 1;
                    }
                    // myDataGridViewRAM.Rows[i].Cells[0].Value = i * 16;
                    //myDataGridViewRAM.Rows[i].Cells[0].ReadOnly = true;
                }
            }

            ////reflash reg display
            if (!frmREG.IsDisposed)
            {
                int datagrid_len = frmREG.dataGridView_reg.Rows.Count;
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[2].Value.ToString());
                    int datagrid_addr = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = RAMDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (old == newdata)
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = changeByteStyle;
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Value = newdata;
                    }
                }


            }

            //reflash watch display
            if (!frmWatch.IsDisposed)
            {
                //int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count-1; // the last line is idle
                //for (int i = 0; i < datagrid_len; i++)
                //{
                //    int old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString());
                //    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value=="??")
                //    {
                //        break;
                //    }
                //    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                //    int newdata = ReadDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                //    if (old == newdata)
                //    {
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                //    }
                //    else
                //    {
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                //    }
                //}
                int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1; // the last line is idle
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = 0;
                    string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString();
                    if (addr_temp != "??" && addr_temp != "Error:not found")
                    {
                        old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString(), 16);
                    }

                    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString() == "??")
                    {
                        continue;
                    }
                    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = RAMDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[3].Value.ToString()))
                    {
                        string s = Convert.ToString(newdata, 2).PadLeft(8, '0').Substring(int.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[4].Value.ToString()), 1);
                        newdata = Convert.ToInt32(s);
                    }
                    if (!bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[7].Value.ToString()))
                    {
                        int iDataRes = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[6].Value.ToString(), 10);
                        for (int k = 1; k < iDataRes; k++)
                        {
                            int newdataH = RAMDataBuffer[datagrid_addr + k];
                            newdata += newdataH * (int)Math.Pow(0x100, k);
                        }
                    }
                    if (old == newdata)
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = changeByteStyle;
                    }
                    frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata.ToString("X");
                    frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                }

            }

            // read bk
            if (!frmBreakPoint.IsDisposed)
            {
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
                    MessageBox.Show("Read option error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                byte[] CMD_ReadBK = { 0x69, 0x03, 0x07, 0x00, 0x01, 0x00, 0x00, 32, 0x00, 0x65 };
                COMPORT.SendCommand(CMD_ReadBK);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 32)
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
                    MessageBox.Show("Read breakpoint error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //reflash BK
                for (int i = 0; i < 16; i++)
                {
                    MCU_Breakpoint[i * 2 + 7] = ReadDataBuffer[i * 2];
                    MCU_Breakpoint[i * 2 + 8] = ReadDataBuffer[i * 2 + 1];
                    frmBreakPoint.myDataGridViewBK.Rows[i].Cells[1].Value = MCU_Breakpoint[i * 2 + 7] + MCU_Breakpoint[i * 2 + 8] * 0x100;
                }
            }

            // read mtp
            if (!frmMTPRAM.IsDisposed)
            {
                byte[] CMD_ReadMTPRAM = { 0x69, 0x03, 0x07, 0x00, 0xc0, 0x00, 0x00, 0x00, 0x04, 0x65 };
                COMPORT.SendCommand(CMD_ReadMTPRAM);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 0x400)
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
                    MessageBox.Show("Read MTP error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                count = 0;
                for (int i = 0; i < 0x400 / 16; i++)
                {
                    for (int j = 1; j < 17; j++)
                    {
                        int old = Convert.ToInt32(frmMTPRAM.myDataGridViewMTPRAM.Rows[i].Cells[j].Value.ToString());
                        int newdata = ReadDataBuffer[count];
                        // int c = frmRAM.myDataGridViewRAM.CurrentCellAddress.X;
                        //int r = frmRAM.myDataGridViewRAM.CurrentCellAddress.Y;
                        if (old == newdata)
                        {
                            frmMTPRAM.myDataGridViewMTPRAM.Rows[i].Cells[j].Style = NchangeByteStyle;
                        }
                        else
                        {
                            frmMTPRAM.myDataGridViewMTPRAM.Rows[i].Cells[j].Style = changeByteStyle;
                            frmMTPRAM.myDataGridViewMTPRAM.Rows[i].Cells[j].Value = newdata;
                        }

                        count = count + 1;
                    }
                }
            }

            //read instruct time
            byte[] CMD_ReadTime = { 0x69, 0x13, 0x02, 0x91, 0x00 };
            COMPORT.SendCommand(CMD_ReadTime);
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
                MessageBox.Show("Read instruct time error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            int time_temp = 0;
            double time_temp1 = 0;
            time_temp = ReadDataBuffer[2] * 0x100 + ReadDataBuffer[3] + 3;
            time_temp1 = (double)time_temp / 72;
            InstructTime_lab.Text = ShowLanguage.Current.RunTime + ": " + time_temp1.ToString();// Convert.ToString(time_temp1);

        }


        private void RISC_ReadCPU()
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle NchangeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            NchangeByteStyle.ForeColor = Color.Black;

            byte[] CMD_ReadCPU = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x90, 0x00, 0x3a, 0x00, 0x30 };
            if (MCU_ID == "0x0314")
            {
                CMD_ReadCPU[7] = 0x3f;
            }
            //COMPORT.OpenPort();
            if (COMPORT.port1.IsOpen == false)
            {
                return;
            }

            int len = 0;

            if (MCU_TYPE == "RISC")
            {
                COMPORT.SendCommand(CMD_ReadCPU);
                //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;

                while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
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
                    MessageBox.Show("Read sysPC error!", ShowLanguage.Current.MessageBoxCaption7, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                // asm list line
                int sysPC = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                string pcValue = sysPC.ToString("X4");


                // SerchLinNume_RISC(pcValue);
                SerchLinNume_RISC_cod(pcValue);
            }
            else
            {
                while (true)
                {
                    //COMPORT.OpenPort();
                    if (COMPORT.port1.IsOpen)
                    {
                        COMPORT.SendCommand(CMD_ReadCPU);
                        //System.Timers.Timer DataReceiveTimer = new System.Timers.Timer();
                        DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        DataReceiveTimer.Enabled = true;
                        TimeOutFlag = 0;
                        while (COMPORT.port1.BytesToRead < CMD_ReadCPU[7])
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
                        len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                    }

                    // asm list line
                    int sysPC = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                    string pcValue = sysPC.ToString("X4");
                    //// asm list line
                    //sysPC = ReadDataBuffer[0x04] + ReadDataBuffer[0x05] * 0x100;
                    //pcValue = sysPC.ToString("X4");

                    if (SerchLinNume_CHC05(pcValue))
                    {
                        break;
                    }
                    else
                    {
                        byte[] CMDdata = { 0x69, 0x01, 0x02, 0xe3, 0x19 };

                        CMDdata[3] = DEBUG_COMMAND.CMD_StepInto;
                        CMDdata[4] = (byte)(0xfd - CMDdata[3]);
                        if (COMPORT.port1.IsOpen)
                        {
                            COMPORT.SendCommand(CMDdata);
                            DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                            DataReceiveTimer.Enabled = true;
                            TimeOutFlag = 0;
                            while (COMPORT.port1.BytesToRead < 4)
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
                            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                        }

                    }
                }

            }




            if (!frmSys.IsDisposed)
            {

                //sys value display A
                string sysTepm = "0x" + ReadDataBuffer[0].ToString("X2");
                frmSys.propertyGridEx_sys.Item[2].Value = sysTepm;

                int sysStack = ReadDataBuffer[0x10] + ReadDataBuffer[0x11] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[0].Value = sysTepm;

                //sp pointer
                sysTepm = "0x" + ReadDataBuffer[0x20].ToString("X2");
                frmSys.propertyGridEx_sys.Item[1].Value = sysTepm;
                //Stack1
                sysStack = ReadDataBuffer[0x30] + ReadDataBuffer[0x31] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[3].Value = sysTepm;

                //Stack2
                sysStack = ReadDataBuffer[0x32] + ReadDataBuffer[0x33] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[4].Value = sysTepm;

                //Stack3
                sysStack = ReadDataBuffer[0x34] + ReadDataBuffer[0x35] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[5].Value = sysTepm;

                //Stack4
                sysStack = ReadDataBuffer[0x36] + ReadDataBuffer[0x37] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[6].Value = sysTepm;

                //Stack5
                sysStack = ReadDataBuffer[0x38] + ReadDataBuffer[0x39] * 0x100;
                sysTepm = "0x" + sysStack.ToString("X4");
                frmSys.propertyGridEx_sys.Item[7].Value = sysTepm;

                if (MCU_ID == "0x0314")
                {
                    //Stack6
                    sysStack = ReadDataBuffer[0x3a] + ReadDataBuffer[0x3b] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[8].Value = sysTepm;

                    //Stack7
                    sysStack = ReadDataBuffer[0x3c] + ReadDataBuffer[0x3d] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[9].Value = sysTepm;

                    //Stack8
                    sysStack = ReadDataBuffer[0x3e] + ReadDataBuffer[0x3f] * 0x100;
                    sysTepm = "0x" + sysStack.ToString("X4");
                    frmSys.propertyGridEx_sys.Item[10].Value = sysTepm;
                }

                frmSys.propertyGridEx_sys.Refresh();
            }


            byte[] CMD_ReadRAM = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x80, 0x00, 0x50, 0x00, 0x2a };

            if (MCU_ID == "0x0314")
            {
                CMD_ReadRAM[7] = (byte)(DeviceConfigXX.MCU_RAMSize % 0x100);
                CMD_ReadRAM[8] = (byte)(DeviceConfigXX.MCU_RAMSize / 0x100);
                //rlength = 0x134;
            }

            COMPORT.SendCommand(CMD_ReadRAM);

            DataReceiveTimer.Enabled = true;
            TimeOutFlag = 0;
            while (COMPORT.port1.BytesToRead < DeviceConfigXX.MCU_RAMSize)
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
            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            int count = 0;


            for (int i = 0; i < DeviceConfigXX.aMCU_RAMSize; i++)
            {
                RAMDataBuffer[i] = ReadDataBuffer[i];
            }
            //reflash
            if (!frmRAM.IsDisposed)
            {
                for (int i = 0; i < DeviceConfigXX.MCU_RAMSize / 16; i++)
                {
                    for (int j = 1; j < 17; j++)
                    {
                        int old = Convert.ToInt32(frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value.ToString());
                        int newdata = ReadDataBuffer[count];
                        // int c = frmRAM.myDataGridViewRAM.CurrentCellAddress.X;
                        //int r = frmRAM.myDataGridViewRAM.CurrentCellAddress.Y;
                        if (old == newdata)
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = NchangeByteStyle;
                        }
                        else
                        {
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Style = changeByteStyle;
                            frmRAM.myDataGridViewRAM.Rows[i].Cells[j].Value = newdata;
                        }

                        count = count + 1;
                    }
                    // myDataGridViewRAM.Rows[i].Cells[0].Value = i * 16;
                    //myDataGridViewRAM.Rows[i].Cells[0].ReadOnly = true;
                }
                if ((MCU_ID == "0x0301" || MCU_ID == "0x0311") && (!frmSys.IsDisposed))
                {
                    int status = ReadDataBuffer[0x03];
                    frmSys.propertyGridEx_sys.Item[8].Value = (status >> 7) & 1;
                    frmSys.propertyGridEx_sys.Item[9].Value = (status >> 6) & 1;
                    frmSys.propertyGridEx_sys.Item[10].Value = (status >> 5) & 1;
                    frmSys.propertyGridEx_sys.Item[11].Value = (status >> 4) & 1;
                    frmSys.propertyGridEx_sys.Item[12].Value = (status >> 3) & 1;
                    frmSys.propertyGridEx_sys.Item[13].Value = (status >> 2) & 1;
                    frmSys.propertyGridEx_sys.Item[14].Value = (status >> 1) & 1;
                    frmSys.propertyGridEx_sys.Item[15].Value = (status >> 0) & 1;

                    frmSys.propertyGridEx_sys.Refresh();
                }
            }

            //reflash reg display
            if (!frmREG.IsDisposed)
            {
                int datagrid_len = frmREG.dataGridView_reg.Rows.Count;
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[2].Value.ToString());
                    int datagrid_addr = Convert.ToInt32(frmREG.dataGridView_reg.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = ReadDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (old == newdata)
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Style = changeByteStyle;
                        frmREG.dataGridView_reg.Rows[i].Cells[2].Value = newdata;
                    }
                }


            }

            //reflash watch display
            if (!frmWatch.IsDisposed)
            {
                //int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count-1; // the last line is idle
                //for (int i = 0; i < datagrid_len; i++)
                //{
                //    int old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString());
                //    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value=="??")
                //    {
                //        break;
                //    }
                //    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                //    int newdata = ReadDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                //    if (old == newdata)
                //    {
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                //    }
                //    else
                //    {
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                //        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
                //    }
                //}
                int datagrid_len = frmWatch.DataGridView_Watch.Rows.Count - 1; // the last line is idle
                for (int i = 0; i < datagrid_len; i++)
                {
                    int old = 0;
                    string addr_temp = frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString();
                    if (addr_temp != "??" && addr_temp != "Error:not found")
                    {
                        old = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value.ToString(), 16);
                    }

                    if (frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString() == "??")
                    {
                        continue;
                    }
                    int datagrid_addr = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[1].Value.ToString(), 16);
                    int newdata = RAMDataBuffer[datagrid_addr]; //addr has a 0x00 too!
                    if (bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[3].Value.ToString()))
                    {
                        string s = Convert.ToString(newdata, 2).PadLeft(8, '0').Substring(int.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[4].Value.ToString()), 1);
                        newdata = Convert.ToInt32(s);
                    }
                    if (!bool.Parse(frmWatch.DataGridView_Watch.Rows[i].Cells[7].Value.ToString()))
                    {
                        int iDataRes = Convert.ToInt32(frmWatch.DataGridView_Watch.Rows[i].Cells[6].Value.ToString(), 10);
                        for (int k = 1; k < iDataRes; k++)
                        {
                            int newdataH = RAMDataBuffer[datagrid_addr + k];
                            newdata += newdataH * (int)Math.Pow(0x100, k);
                        }
                    }
//                     if (old == newdata)
//                     {
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = NchangeByteStyle;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
//                     }
//                     else
//                     {
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = changeByteStyle;
//                         frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
//                     }
                    if (old == newdata)
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = NchangeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = NchangeByteStyle;
                    }
                    else
                    {
                        frmWatch.DataGridView_Watch.Rows[i].Cells[2].Style = changeByteStyle;
                        frmWatch.DataGridView_Watch.Rows[i].Cells[5].Style = changeByteStyle;
                    }
                    frmWatch.DataGridView_Watch.Rows[i].Cells[2].Value = newdata.ToString("X");
                    frmWatch.DataGridView_Watch.Rows[i].Cells[5].Value = newdata;
                }

            }

            //read bk
            if (!frmBreakPoint.IsDisposed)
            {
                byte[] CMD_ReadOption = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x91, 0x00, 0x04, 0x00, 0x65 };
                COMPORT.SendCommand(CMD_ReadOption);
                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 4)
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);

                byte[] CMD_ReadBK = { 0x69, 0x03, 0x07, 0x00, 0x00, 0x8f, 0x00, 32, 0x00, 0x65 };
                COMPORT.SendCommand(CMD_ReadBK);

                DataReceiveTimer.Enabled = true;
                TimeOutFlag = 0;
                while (COMPORT.port1.BytesToRead < 32)
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
                len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
                //reflash BK
                for (int i = 0; i < 16; i++)
                {
                    MCU_Breakpoint[i * 2 + 7] = ReadDataBuffer[i * 2];
                    MCU_Breakpoint[i * 2 + 8] = ReadDataBuffer[i * 2 + 1];
                    frmBreakPoint.myDataGridViewBK.Rows[i].Cells[1].Value = MCU_Breakpoint[i * 2 + 7] + MCU_Breakpoint[i * 2 + 8] * 0x100;
                }
            }

            //read instruct time
            byte[] CMD_ReadTime = { 0x69, 0x13, 0x02, 0x91, 0x00 };
            COMPORT.SendCommand(CMD_ReadTime);
            DataReceiveTimer.Enabled = true;
            TimeOutFlag = 0;
            while (COMPORT.port1.BytesToRead < 4)
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
            len = COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            int time_temp = 0;
            double time_temp1 = 0;
            time_temp = ReadDataBuffer[2] * 0x100 + ReadDataBuffer[3] + 3;
            time_temp1 = (double)time_temp / 72;
            InstructTime_lab.Text = ShowLanguage.Current.RunTime + ": " + time_temp1.ToString();// Convert.ToString(time_temp1);

        }

        #endregion

        #region get project path
        public static string GetProjectPath()
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

            if (path == "")
                path = frmMAIN.APPLICATION_PATH + "Sample\\aaaa\\aaaa.Proj";
            rootNode.Save(srt);

            return path;
        }

        private int GetMCUOption()
        {
            int option = 0;
            string str = ProjectPath/*GetProjectPath()*/;

            XElement rootNode = XElement.Load(str);

            IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("Option")
                                            select myTarget;
            foreach (XElement xnode in myvalue)
            {
                option = Convert.ToInt32(xnode.Attribute("Value").Value);
            }

            rootNode.Save(str);

            return option;
        }

        #endregion

//         [DllImport("IAES.dll", SetLastError = true)]
//         public static extern void EncryptFile(string path, string pwd);
// 
//         [DllImport("IAES.dll", SetLastError = true)]
//         public static extern void DecryptFile(string path, string pwd);
// 
// 
//         #region 加密文件
//         public void EncryptFile(string path)
//         {
//             try
//             {
//                 EncryptFile(path, "sinomcu");
//             }
//             catch (Exception ex)
//             {
// 
//             }
//         }
//         #endregion
// 
//         #region 解密文件
//         public void DecryptFile(string path)
//         {
//             try
//             {
//                 DecryptFile(path, "sinomcu");
//             }
//             catch (Exception ex)
//             {
// 
//             }
//         }
//         #endregion
    }
}
