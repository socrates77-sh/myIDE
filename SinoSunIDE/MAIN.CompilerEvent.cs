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
using System.Globalization;
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
        /// <summary>
        /// HC05 CPU C compiler instruct function  
        /// </summary>
        private void HC05_compilerC()
        {

            System.Diagnostics.Process compile = new System.Diagnostics.Process();
            //compile.StartInfo.FileName = APPLICATION_PATH+"\\WinC_V\\mc_com.bat";
            compile.StartInfo.FileName = "cmd.exe";
            compile.StartInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "Compile Starting....\n";


            string mainFileName = null;
            if (frmexplorer.treeView1.Nodes.Count == 0)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText31);
                return;
            }

            GetProjectASM_FilesName();
            //get asmfile path
            string pPath = ProjectPath/*GetProjectPath()*/;

            if (pPath == "")
                return;

            string projectName = pPath.Substring(0, pPath.LastIndexOf(".")); //Path.GetFileNameWithoutExtension(pPath);
            mainFileName = projectName + ".c";

            pPath = Path.GetDirectoryName(pPath);

            if (File.Exists(mainFileName) == false)
            {
                if (File.Exists(pPath + "\\main.c") == false)
                {
                    //MessageBox.Show("Can't Find " + mainFileName + "!");
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText32);
                    return;
                }
                mainFileName = "main";

            }
            else
            {
                mainFileName = Path.GetFileNameWithoutExtension(mainFileName);
            }

            string bat_name = null;
            switch (frmMAIN.MCU_ID)
            {
                case "0x0224":
                    bat_name = "x224";// "MC20P24";
                    break;
                case "0x0222":
                    bat_name = "x222"; // "MC20P22";
                    break;
                case "0x0238":
                    bat_name = "x238";// "MC20P38";
                    break;
                //---------------------------------------
                case "0x0202":
                    bat_name = "x202";// "MC20P02";
                    break;
                case "0x0201":
                    bat_name = "x201";// "MC20P01";
                    break;
                case "0x0281":
                    bat_name = "x281";// "MC20P801";
                    break;
                case "0x0204":
                    bat_name = "x204";// "MC20P04";
                    break;
                //-------------------------------------------
                case "0x0101":
                    bat_name = "x101";// "MC10P01";
                    break;
                case "0x0111":
                    bat_name = "x111";// "MC10P11";
                    break;
                case "0x0102":
                    bat_name = "x102";// "MC10P02";
                    break;

                default:
                    bat_name = "x224";// "MC20P24";
                    break;
            }

            string pathroot = Path.GetPathRoot(pPath);
            pathroot = pathroot.Substring(0, 2);
            compile.StartInfo.Arguments = pathroot;
            // compile.StartInfo.WorkingDirectory="G:\\Emulator\\IDE\\SinoSunIDE\\SinoSunIDE\\bin\\Debug\\Sample\\mc20p68_c";
            compile.StartInfo.RedirectStandardInput = true;
            compile.StartInfo.RedirectStandardOutput = true;
            compile.StartInfo.RedirectStandardError = true;
            compile.StartInfo.CreateNoWindow = true;
            compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            compile.Start();
            compile.StandardInput.WriteLine(@"cd\");
            compile.StandardInput.WriteLine(pathroot);
            compile.StandardInput.WriteLine(@"cd " + pPath);

            compile.StandardInput.WriteLine(APPLICATION_PATH + "\\WinC_V\\mc_com.bat " + mainFileName + " " + bat_name + ">Debug\\log");
            // string output = compile.StandardOutput.ReadToEnd();//编译信息输出变量

            compile.StandardInput.WriteLine("exit");
            //output = compile.StandardOutput.ReadToEnd();//编译信息输出变量
            string output = compile.StandardError.ReadToEnd();
            compile.WaitForExit();

            frmmessage.fastColoredTextBox.AppendText(output);
            string ListFileName2 = pPath + "\\Debug\\log";
            FileStream textLoad = new FileStream(ListFileName2, FileMode.Open, FileAccess.Read);
            if (textLoad.CanRead)
            {
                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(ListFileName2);
                frmmessage.fastColoredTextBox.AppendText(FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding));
                textLoad.Close();
            }
            else
            {
                MessageBox.Show(ListFileName[0] + ShowLanguage.Current.MessageBoxText3);
            }

            // frmmessage.fastColoredTextBox.Load(@"G:\\Emulator\\IDE\\SinoSunIDE\\SinoSunIDE\\bin\\Debug\\Sample\\mc20p68_c\\log");
            frmmessage.fastColoredTextBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


            string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
            string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);

            CHC05_cod tempf = new CHC05_cod();

            tempf.chc05_codfile_create(project_path, project_name);
            return;


        }

        /// <summary>
        /// HC05 CPU ASM compiler instruct function 
        /// v1.0 assembly
        /// </summary>
        private void HC05_compilerASM()
        {
            System.Diagnostics.Process compile = new System.Diagnostics.Process();
            compile.StartInfo.FileName = "tools\\Assembly.exe";
            compile.StartInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");

            if (File.Exists(APPLICATION_PATH + "\\tools\\Assembly.exe") == false)
            {
                //MessageBox.Show("Can't Find " + ASM05_Constant_Define.ASSEMBLY_NAME + "!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText33);
                return;
            }

            string mainFileName = null;
            if (frmexplorer.treeView1.Nodes.Count == 0)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText31);
                return;
            }

            GetProjectASM_FilesName();
            //get asmfile path
            string pPath = ProjectPath/*GetProjectPath()*/;
            string projectName = pPath.Substring(0, pPath.LastIndexOf(".")); //Path.GetFileNameWithoutExtension(pPath);
            mainFileName = projectName + ".asm";

            if (File.Exists(mainFileName) == false)
            {
                //MessageBox.Show("Can't Find " + mainFileName + "!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + mainFileName + ShowLanguage.Current.MessageBoxText35);
                return;
            }
            else
            {
                saveAllToolStripMenuItem_Click(null, null);

                if (CurrentTB == null)
                {
                    OpenFile_MouseDouble_Click(mainFileName);
                }


                compile.StartInfo.Arguments = mainFileName;
                compile.StartInfo.RedirectStandardInput = true;
                compile.StartInfo.RedirectStandardOutput = true;
                compile.StartInfo.RedirectStandardError = true;
                compile.StartInfo.CreateNoWindow = true;
                compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                compile.Start();
                string output = compile.StandardOutput.ReadToEnd();//编译信息输出变量
                compile.WaitForExit();

                frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                compile_result = output.EndsWith("done!\r\n\r\n");
                if (!compile_result)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                }
                //file process
                //string lisf = mainFileName.Substring(0, mainFileName.LastIndexOf("."));
                //string path = Path.GetDirectoryName(mainFileName);
                //string list_name = Path.GetFileNameWithoutExtension(mainFileName);
                //File.Move(path+list_name+".s19", path + "\\Output\\"+list_name+".s19");


                if (!frmmessage.IsDisposed)
                    frmmessage.Show(dockPanelMain);

                AHC05_VARfileProcess();
                return;
            }
            //}
        }

        private void AHC05_VARfileProcess()
        {


            string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
            string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);
            //string fpath = project_path + "\\System\\" + project_name + ".var";
            string fpath = project_path + "\\" + project_name + ".var";
            if (File.Exists(fpath) != true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string gValiableFile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            textLoad.Close();

            FastColoredTextBox newValiableFile = new FastColoredTextBox();
            newValiableFile.AcceptsTab = true;

            //at+\s+0x[a-f0-9]+\-+0x[a-f0-9]{4}  addr;
            Regex line_key = new Regex(@"[\w|(\[[^\]\])]+\s+[A-Fa-f0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            int file_index = 0;
            while (true)
            {
                Match str_line = line_key.Match(gValiableFile, file_index);
                if (str_line.Value == "")
                {
                    break;
                }
                else
                {
                    string temp_str = str_line.Value;
                    file_index = str_line.Index + str_line.Length;
                    string var_name = temp_str.Substring(0, temp_str.IndexOf("\t"));
                    int a = temp_str.IndexOf("\t") + 1;
                    int b = temp_str.Length - a;

                    string var_value = "0x00" + temp_str.Substring(a, b);

                    //Regex line_val_key = new Regex(@"\t[A-Fa-f0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    newValiableFile.AppendText(var_name + " at " + var_value + "  xxxx.x  0000 to 0000\n");
                }
            }

            string var_path = project_path + "\\System\\" + project_name + ".var";
            File.WriteAllText(var_path, newValiableFile.Text, Encoding.Default);

        }

        private void RISC_compile_currentfile()
        {
            System.Diagnostics.Process compile = new System.Diagnostics.Process();
            compile.StartInfo.FileName = "tools\\gpasm.exe";
            compile.StartInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");

            string gpasm_path = APPLICATION_PATH + "\\tools\\gpasm.exe";
            Regex objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.exe$");
            if (objNotPositivePattern.IsMatch(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText37, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(gpasm_path) == false)
            {
                //MessageBox.Show("Can't Find " + ASM05_Constant_Define.ASSEMBLY_NAME + "!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText33);
                return;
            }
            else if (CurrentTB == null)
            {
                //MessageBox.Show("Please Open a Project!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText38);
                return;
            }
            else
            {
                string str_mcuName = null;
                switch (MCU_ID)
                {
                    case "0x0301":
                    case "0x0311":
                        str_mcuName = "-p 0311 -c ";
                        break;
                    case "0x3401":
                        str_mcuName = "-p 3401 -c ";
                        break;
                    case "0x3111":
                        str_mcuName = "-p 3111 -c ";
                        break;
                    case "0x0314":
                        str_mcuName = "-p 30p44 -c ";
                        break;
                    case "0x3221":
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x32821":
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x3264":
                        str_mcuName = "-p 3264 -c ";
                        break;
                    case "0x3378":
                        str_mcuName = "-p 3394 -c ";
                        break;
                    case "0x3316":
                        str_mcuName = "-p 3316 -c ";
                        break;
                    case "0x5312":
                        str_mcuName = "-p 3316 -c ";
                        break;
                    case "0x7212":
                        str_mcuName = "-p 3394 -c ";
                        break;
                    case "0x9902":
                        str_mcuName = "-p 9902 -c ";
                        break;
                    case "0x7333":                  //使用A300（9902）的内核（STM32指令集）
                        str_mcuName = "-p 7333 -c ";
                        break;
                    case "0x9903":                  //使用A300（9902）的内核（STM32指令集）
                        str_mcuName = "-p 9903 -c ";
                        break;
                    case "0x9904":                  //使用A300（9902）的内核（STM32指令集）
                        str_mcuName = "-p 9904 -c ";
                        break;
                    case "0x7122":                  //使用A300（9902）的内核（STM32指令集）
                        str_mcuName = "-p 7122 -c ";
                        break;
                    case "0x3220":
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x3394":
                        str_mcuName = "-p 3394 -c ";
                        break;
                    case "0x7022":// TODO wj
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x6060":// TODO wj
                        str_mcuName = "-p 0311 -c ";
                        break;
                    case "0x7510":// TODO wj
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x3222":// TODO wj
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x8132":// TODO wj
                        str_mcuName = "-p 3264 -c ";
                        break;
                    case "0x7311":// TODO wj
                        //str_mcuName = "-p 3264 -c ";
                        str_mcuName = "-p 7311 -c ";
                        break;
                    case "0x7321":
                        str_mcuName = "-p 3264 -c ";
                        break;
                    case "0x7011":
                    case "0x7031":
                    //case "0x7041":
                        //str_mcuName = "-p 7041 -c ";
                        str_mcuName = "-p 7011 -c ";
                        break;
                    case "0x7041":
                    case "0x9905":
                    case "0x6021":
                    case "0x2722":
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "-p 2K7041 -c ";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "-p 1K7041 -c ";
                        }
                        break;
                    case "0x7511":// TODO wj
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x5222":
                        str_mcuName = "-p 5222 -c ";
                        break;
                    case "0x7323":// TODO wj
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "-p 8K7323 -c ";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "-p 4K7323 -c ";
                        }
                        break;
                    case "0x7312":
                        str_mcuName = "-p 3221 -c ";
                        break;
                    case "0x6080":// TODO wj
                        str_mcuName = "-p 0311 -c ";
                        break;
                    case "0x6220":// TODO LYL
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "-p 1K6220 -c ";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "-p 05K6220 -c ";
                        }      
                        break;
                    default:
                        MessageBox.Show("no this mcu!");
                        return;
                    // break;
                }

                // GetProjectASM_FilesName();
                //get asmfile path
                string pPath = ProjectPath/*GetProjectPath()*/;
                // pPath = pPath.Substring(0, pPath.LastIndexOf("."));
                pPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
                string mainFileName = frmfile.tsFiles.SelectedItem.Tag.ToString();

                objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.(asm|ASM)$");
                if (objNotPositivePattern.IsMatch(mainFileName) == false)
                {
                    MessageBox.Show(mainFileName + ShowLanguage.Current.MessageBoxText39, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string objectfilename = Path.GetFileNameWithoutExtension(mainFileName);
                //string   yihao1="-I "+""";
                string yihao2 = APPLICATION_PATH + @"INC ";
                //yihao2 = "\""+yihao2 + "\"";
                str_mcuName = str_mcuName + "-I " + yihao2 + "-o System\\" + objectfilename + " ";

                if (File.Exists(mainFileName) == false)
                {
                    //MessageBox.Show("Can't Find " + mainFileName + "!");
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + mainFileName);
                    return;
                }
                else
                {
                    //  saveAllToolStripMenuItem_Click(null, null);

                    compile.StartInfo.Arguments = str_mcuName + mainFileName;
                    compile.StartInfo.RedirectStandardInput = true;
                    compile.StartInfo.RedirectStandardOutput = true;
                    compile.StartInfo.RedirectStandardError = true;
                    compile.StartInfo.CreateNoWindow = true;
                    compile.StartInfo.WorkingDirectory = pPath; //APPLICATION_PATH + "\\tools"; //这句很关键
                    compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    compile.Start();
                    string output = compile.StandardOutput.ReadToEnd();//编译信息输出变量
                    compile.WaitForExit();

                    frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


                    string errorflag = output.IndexOf(@"(GPASM) Unsuccessful ").ToString();
                    if (errorflag != "-1")
                    {
                        compile_result = false;
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");

                    }
                    if (!frmmessage.IsDisposed)
                        frmmessage.Show(dockPanelMain);
                    return;
                }
            }

        }

        ///<summary> C RISC compiler
        /// 2013.08.01
        /// 
        /// </summary>
        private void CRISC_compile_currentfile()
        {
            System.Diagnostics.Process compile = new System.Diagnostics.Process();
            //compile.StartInfo.FileName = "tools\\sdcc.exe";
            compile.StartInfo.FileName = "cmd.exe";
            compile.StartInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");

            string gpasm_path = APPLICATION_PATH + "\\tools\\gpasm.exe";
            Regex objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.exe$");
            if (objNotPositivePattern.IsMatch(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText37, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (CurrentTB == null)
            {
                //MessageBox.Show("Please Open a Project!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText38);
                return;
            }
            else
            {
                string str_mcuName = null;
                string str_lkrName = null;
                switch (MCU_ID)
                {
                    case "0x0301":
                    case "0x0311":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p0311 ";
                        str_lkrName = "0311.lkr";
                        break;
                    case "0x3401":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3401 ";
                        str_lkrName = "3401.lkr";
                        break;
                    case "0x3111":
                        //str_mcuName = "-p 31p11 -c ";
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3111 ";
                        str_lkrName = "3111.lkr";
                        break;
                    case "0x0314":
                        //str_mcuName = "-p 30p44 -c ";
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p30p44 ";
                        str_lkrName = "mc30p44.lkr";
                        break;
                    case "0x3221":
                        //str_mcuName = "-p 32p21 -c ";
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                        str_lkrName = "3221.lkr";
                        break;
                    case "0x32821":
                        //str_mcuName = "-p 32p21 -c ";
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                        str_lkrName = "3221.lkr";
                        break;
                    case "0x3264":
                        //str_mcuName = "-p 32p21 -c ";
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3264 ";
                        str_lkrName = "3264.lkr";
                        break;
                    case "0x3378":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3378 ";
                        str_lkrName = "3378.lkr";
                        break;
                    case "0x3316":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3316 ";
                        str_lkrName = "3316.lkr";
                        break;
                    case "0x5312":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p5312 ";
                        str_lkrName = "3316.lkr";
                        break;
                    case "0x7212":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7212 ";
                        str_lkrName = "7212.lkr";
                        break;
                    case "0x9902":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9902 ";
                        str_lkrName = "9902.lkr";
                        break;
                    case "0x7333":                                      
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7333 ";
                        str_lkrName = "7333.lkr";
                        break;
                    case "0x9903":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9903 ";
                        str_lkrName = "9903.lkr";
                        break;
                    case "0x9904":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9904 ";
                        str_lkrName = "9904.lkr";
                        break;
                    case "0x7122":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7122 ";
                        str_lkrName = "7122.lkr";
                        break;
                    case "0x3220":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3264 ";
                        str_lkrName = "3221.lkr";
                        break;
                    case "0x3394":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3264 ";
                        str_lkrName = "3394.lkr";
                        break;
                    case "0x7022":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7022 ";
                        str_lkrName = "7022.lkr";
                        break;
                    case "0x6060":// TODO wj
                    case "0x6080":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p6060 ";
                        str_lkrName = "0311.lkr";
                        break;
                    case "0x6220":
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p1K6220 ";
                            str_lkrName = "1K6220.lkr";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p05K6220 ";
                            str_lkrName = "05K6220.lkr";
                        }
                        break;
                    case "0x7510":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7510 ";
                        str_lkrName = "3221.lkr";
                        break;
                    case "0x3222":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                        str_lkrName = "3222.lkr";
                        break;
                    case "0x8132":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p8132 ";
                        str_lkrName = "8132.lkr";
                        break;
                    case "0x7311":// TODO wj
                    case "0x7321":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7311 ";
                        str_lkrName = "7311.lkr";
                        break;
                    case "0x7511":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7511 ";
                        str_lkrName = "7511.lkr";
                        break;
                    case "0x5222":// TODO wj
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p5222 ";
                        str_lkrName = "5222.lkr";
                        break;
                    case "0x7323":// By LYL
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p8K7323 ";
                            str_lkrName = "8K7323.lkr";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p4K7323 ";
                            str_lkrName = "4K7323.lkr";
                        }
                        break;
                    case "0x7312":
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7510 ";
                        str_lkrName = "3221.lkr";
                        break;
                    default:
                        MessageBox.Show("no this mcu!");
                        return;
                    // break;
                }

                // GetProjectASM_FilesName();
                //get asmfile path
                string pPath = ProjectPath/*GetProjectPath()*/;

                string pathroot = Path.GetPathRoot(pPath);
                pathroot = pathroot.Substring(0, 2);
                // pPath = pPath.Substring(0, pPath.LastIndexOf("."));
                pPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
                string mainFileName = frmfile.tsFiles.SelectedItem.Tag.ToString();

                string objectfilename = Path.GetFileNameWithoutExtension(mainFileName);
                //string   yihao1="-I "+""";
                string yihao2 = @" -Wa -I" + APPLICATION_PATH + @"tools\share\header -I" + APPLICATION_PATH + @"tools\share\include"
                    + @" -Wl -s" + APPLICATION_PATH + @"tools\share\lkr\" + str_lkrName;
                //yihao2 = "\""+yihao2 + "\"";
                str_mcuName = str_mcuName + yihao2 + " -c " + objectfilename + ".c";// +" -o System\\" + objectfilename;



                objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.(c|C)$");
                if (objNotPositivePattern.IsMatch(mainFileName) == false)
                {
                    MessageBox.Show(mainFileName + ShowLanguage.Current.MessageBoxText39, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(mainFileName) == false)
                {
                    //MessageBox.Show("Can't Find " + mainFileName + "!");
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText40 + mainFileName);
                    return;
                }
                else
                {
                    //  saveAllToolStripMenuItem_Click(null, null);

                    compile.StartInfo.Arguments = pathroot;// str_mcuName;// +mainFileName;
                    compile.StartInfo.RedirectStandardInput = true;
                    compile.StartInfo.RedirectStandardOutput = true;
                    compile.StartInfo.RedirectStandardError = true;
                    compile.StartInfo.CreateNoWindow = true;
                    compile.StartInfo.WorkingDirectory = pPath; //APPLICATION_PATH + "\\tools"; //这句很关键
                    //compile.StartInfo.WorkingDirectory = APPLICATION_PATH + "\\tools"; //这句很关键
                    compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    compile.Start();

                    compile.StandardInput.WriteLine("PATH=" + APPLICATION_PATH + "\\tools");
                    compile.StandardInput.WriteLine(str_mcuName);

                    compile.StandardInput.WriteLine("exit");

                    string output = compile.StandardOutput.ReadToEnd();//编译信息输出变量
                    output += compile.StandardError.ReadToEnd();
                    compile.WaitForExit();

                    frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");


                    string errorflag = output.IndexOf(@"(GPASM) Successful ").ToString();
                    if (errorflag == "-1")
                    {
                        compile_result = false;
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");

                    }
                    if (!frmmessage.IsDisposed)
                        frmmessage.Show(dockPanelMain);
                    return;
                }
            }
        }
        /// <summary>RISC CPU 14bit/16bit C compiler
        /// V 0.0.4
        /// add funtion for display registers:
        /// </summary>
        private void CRISC_compiler()
        {
            //System.Diagnostics.Process compile = new System.Diagnostics.Process();
            //compile.StartInfo.FileName = "cmd.exe";
            //compile.StartInfo.UseShellExecute = false;
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");
            frmmessage.Refresh();

            string gpasm_path = APPLICATION_PATH + "\\tools\\gpasm.exe";
            Regex objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.exe$");
            if (objNotPositivePattern.IsMatch(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText37, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(gpasm_path) == false)
            {
                //MessageBox.Show("Can't Find " + ASM05_Constant_Define.ASSEMBLY_NAME + "!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText33);
                return;
            }

            GetProjectASM_FilesName();
            //get asmfile path
            string pPath = ProjectPath/*GetProjectPath()*/;

            string pathroot = Path.GetPathRoot(pPath);
            pathroot = pathroot.Substring(0, 2);

            string projectName = Path.GetFileNameWithoutExtension(pPath);
            //pPath = pPath.Substring(0, pPath.LastIndexOf("."));
            pPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
            string output = null;

            //             compile.StartInfo.RedirectStandardInput = true;
            //             compile.StartInfo.RedirectStandardOutput = true;
            //             compile.StartInfo.RedirectStandardError = true;
            //             compile.StartInfo.CreateNoWindow = true;
            //             compile.StartInfo.WorkingDirectory = pPath; //APPLICATION_PATH + "\\tools"; //这句很关键
            //             compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.WorkingDirectory = pPath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            string str_mcuName = null;
            string str_lkr = null;
            string str_libName = null;
            switch (MCU_ID)
            {
                case "0x0301":
                case "0x0311":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p0311 ";
                    str_lkr = "0311.lkr";
                    //str_libName = " libsdcc-mc30.lib mc30p011.lib";
                    str_libName = " 0311.lib";
                    break;
                case "0x3401":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3401 ";
                    str_lkr = "3401.lkr";
                    str_libName = " 3401.lib";
                    //str_libName = " libsdcc-mc34.lib mc34p01.lib";
                    break;
                case "0x3111":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3111 ";
                    str_lkr = "3111.lkr";
                    str_libName = " 3111.lib";
                    //str_libName = " libsdcc-mc31.lib mc31p11.lib";
                    break;
                case "0x0314":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p30p44 ";
                    str_lkr = "mc30p44.lkr";
                    break;
                case "0x3221":
                    //str_mcuName = "-p 32p21 -c ";
                    //str_lkr = " -s x3721.lkr -o output\\";
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                    str_lkr = "3221.lkr";
                    //str_libName = " libsdcc-mc32.lib mc32p21.lib";
                    str_libName = " 3221.lib";
                    break;
                case "0x32821":
                    //str_mcuName = "-p 32p21 -c ";
                    //str_lkr = " -s x3721.lkr -o output\\";
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                    str_lkr = "3221.lkr";
                    str_libName = " 3221.lib";
                    break;
                case "0x3264":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3264 ";
                    str_lkr = "3264.lkr";
                    str_libName = " 3264.lib";
                    break;
                case "0x3378":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3378 ";
                    str_lkr = "3378.lkr";
                    str_libName = " 3378.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x3316":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3316 ";
                    str_lkr = "3316.lkr";
                    str_libName = " 3316.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x5312":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p5312 ";
                    str_lkr = "3316.lkr";
                    str_libName = " 5312.lib";
                    break;
                case "0x7212":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7212 ";
                    str_lkr = "7212.lkr";
                    str_libName = " 7212.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x9902":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9902 ";
                    str_lkr = "9902.lkr";
                    str_libName = " 9902.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x7333":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7333 ";
                    str_lkr = "7333.lkr";
                    str_libName = " 7333.lib";
                    break;
                case "0x9903":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9903 ";
                    str_lkr = "9903.lkr";
                    str_libName = " 9903.lib";
                    break;
                case "0x9904":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p9904 ";
                    str_lkr = "9904.lkr";
                    str_libName = " 9904.lib";
                    break;
                case "0x7122":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7122 ";
                    str_lkr = "7122.lkr";
                    str_libName = " 7122.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x3220":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                    str_lkr = "3221.lkr";
                    str_libName = " 3221.lib";
                    //MessageBox.Show("it does not support this MCU");
                    break;
                case "0x3394":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -3264 ";
                    str_lkr = "3394.lkr";
                    str_libName = " 3264.lib";
                    break;
                case "0x7022":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7022 ";
                    str_lkr = "7022.lkr";
                    str_libName = " 7022.lib";
                    break;
                case "0x6060":// TODO wj
                case "0x6080":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p6060 ";
                    str_lkr = "0311.lkr";
                    str_libName = " 6060.lib";
                    break;
                case "0x6220":
                    if (frmMAIN.RomSpace_stat == 1)
                    {
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p1K6220 ";
                        str_lkr = "1K6220.lkr";
                        str_libName = " 6060.lib";
                    }
                    else if (frmMAIN.RomSpace_stat == 0)
                    {
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p05K6220 ";
                        str_lkr = "05K6220.lkr";
                        str_libName = " 6060.lib";
                    }
                    break;
                case "0x7510":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7510 ";
                    str_lkr = "3221.lkr";
                    str_libName = " 7510.lib";
                    break;
                case "0x3222":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p3221 ";
                    str_lkr = "3222.lkr";
                    str_libName = " 3221.lib";
                    break;
                case "0x8132":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p8132 ";
                    str_lkr = "8132.lkr";
                    str_libName = " 8132.lib";
                    break;
                case "0x7311":// TODO wj
                case "0x7321":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7311 ";
                    str_lkr = "7311.lkr";
                    str_libName = " 7311.lib";
                    break;
                case "0x7511":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7511 ";
                    str_lkr = "7511.lkr";
                    str_libName = " 7511.lib";
                    break;
                case "0x5222":// TODO wj
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p5222 ";
                    str_lkr = "5222.lkr";
                    str_libName = " 5222.lib";
                    break;
                case "0x7323":// By LYL
                    if (frmMAIN.RomSpace_stat == 1)
                    {
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p8K7323 ";
                        str_lkr = "8K7323.lkr";
                        str_libName = "8K7323.lib";
                    }
                    else if (frmMAIN.RomSpace_stat == 0)
                    {
                        str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p4K7323 ";
                        str_lkr = "4K7323.lkr";
                        str_libName = "4K7323.lib";
                    }
                    break;
                case "0x7312":
                    str_mcuName = "sdcc -V --verbose --use-non-free -mmc3x -p7510 ";
                    str_lkr = "3221.lkr";
                    str_libName = " 7510.lib";
                    break;
                default:
                    MessageBox.Show("no this mcu!");
                    return;
            }


            for (int i = 0; i < ASMFileName.Count; i++)
            {
                objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.(c|C)$");
                if (objNotPositivePattern.IsMatch(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ASMFileName[i] + ShowLanguage.Current.MessageBoxText39, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                else
                {
                    string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[i]);
                    string yihao2 = @" -Wa -I" + APPLICATION_PATH + @"tools\share\header -I" + APPLICATION_PATH + @"tools\share\include"
                        + @" -Wl -s" + APPLICATION_PATH + @"tools\share\lkr\" + str_lkr;
                    //yihao2 = "\""+yihao2 + "\"";
                    string str_fileName = str_mcuName + yihao2 + " -c " + objectfilename + ".c ";// +" -o System\\" + objectfilename;

                    try
                    {
                        startInfo.Arguments = pathroot;
                        System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                        compile.Start();

                        compile.StandardInput.WriteLine("PATH=" + APPLICATION_PATH + "\\tools");
                        compile.StandardInput.WriteLine(str_fileName);

                        compile.StandardInput.WriteLine("exit");

                        //output = output + compile.StandardOutput.ReadToEnd();//编译信息输出变量
                        stroutput = null;
                        strerror = null;
                        compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                        compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                        compile.BeginOutputReadLine();
                        compile.BeginErrorReadLine();
                        //output += compile.StandardError.ReadToEnd();
                        compile.WaitForExit();
                        compile.Close();
                        output = output + stroutput + strerror;//似乎仿真外部编译器时根本进不了此处，此段有可能为僵尸代码

                        string errorflag = stroutput.IndexOf(@"(GPASM) Successful ").ToString();
                        if (errorflag == "-1")
                        {
                            output = output + "---------Compiler code Error!...\n\n";
                            frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            compile_result = false;
                            MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                            if (!frmmessage.IsDisposed)
                                frmmessage.Show(dockPanelMain);
                            return;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            frmmessage.Refresh();
            //link----------------------------------------------------------------------------------------------
            startInfo.FileName = "cmd.exe";
            string strObjectfile = null;

            for (int i = 0; i < ASMFileName.Count; i++)
            {
                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                strObjectfile = strObjectfile + pPath + "\\" + Path.GetFileNameWithoutExtension(ASMFileName[i]) + ".o ";
            }
            //compile.StartInfo.Arguments = "-I " + APPLICATION_PATH + "LIB " + " -s 30p01a.lkr -o output\\" + projectName + " -c " + strObjectfile;
            startInfo.Arguments = pathroot;// "gplink -I " + APPLICATION_PATH + @"tools\share\lib " + "-s " + APPLICATION_PATH + @"tools\share\lkr\" + str_lkr + " -w -m -c -o Output\\" + projectName + " " + strObjectfile;
            try
            {
                System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                compile.Start();
                //compile.StandardInput.WriteLine(@"cd\");
                //compile.StandardInput.WriteLine(dir);
                compile.StandardInput.WriteLine("PATH=" + APPLICATION_PATH + "\\tools");
                compile.StandardInput.WriteLine("gplink -I " + APPLICATION_PATH + @"tools\share\lib " + "-s " + APPLICATION_PATH + @"tools\share\lkr\" + str_lkr + " -w -m -c -o Output\\" + projectName + " " + strObjectfile
                    + str_libName);

                compile.StandardInput.WriteLine("exit");
                //output = output + compile.StandardOutput.ReadToEnd();//编译信息输出变量
                stroutput = null;
                strerror = null;
                compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                compile.BeginOutputReadLine();
                compile.BeginErrorReadLine();
                compile.WaitForExit();
                compile.Close();
                output = output + stroutput + strerror;
                //output += compile.StandardError.ReadToEnd();
                string str = "";
                string strRamUse = "";
                int errorflag1 = output.IndexOf(@"Program Memory Words Used");
                if (errorflag1 != -1)
                {
                    strRamUse = GetRamFromMap();
                    str = @"(FF) Output\" + projectName + ".lst";
                    int errorflag2 = output.IndexOf(str);
                    if (errorflag2 != -1)
                    {
                        str = output.Substring(errorflag1, errorflag2 - errorflag1);
                        output = output.Remove(errorflag1, errorflag2 - errorflag1);
                    }
                }
                //compile.WaitForExit();
                frmmessage.fastColoredTextBox.InsertText(output + strRamUse + str + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string indexValue = output.IndexOf("(GPLINK) Successful").ToString();
                compile_result = true;
                if (indexValue == "-1")
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText41, "Error!");
                    compile_result = false;
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (!frmmessage.IsDisposed)
                frmmessage.Show(dockPanelMain);

            //--var file process--------------------------------------------------------------
            //ARISC_VARfileProcess();

            //--debug file process---------------------------
            string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
            string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);

            CRISC_cod tempf = new CRISC_cod();

            tempf.crisc_codfile_create(project_path, project_name);
            tempf.crisc_var_create(project_path, project_name);
            return;
        }

        private void CRISC_compiler_2711()
        {
            //System.Diagnostics.Process compile = new System.Diagnostics.Process();
            //compile.StartInfo.FileName = "cmd.exe";
            //compile.StartInfo.UseShellExecute = false;
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");
            frmmessage.Refresh();

            string gpasm_path = APPLICATION_PATH + "\\tools\\gpasm.exe";
            Regex objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.exe$");
            if (objNotPositivePattern.IsMatch(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText37, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText33);
                return;
            }

            GetProjectASM_FilesName();
            //get asmfile path
            string pPath = ProjectPath/*GetProjectPath()*/;

            string pathroot = Path.GetPathRoot(pPath);
            pathroot = pathroot.Substring(0, 2);

            string projectName = Path.GetFileNameWithoutExtension(pPath);
            pPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
            string output = null;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.WorkingDirectory = pPath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            string COMPILER = APPLICATION_PATH + @"\tools\Bin\Build\sn8pc.exe";
            string GMA = APPLICATION_PATH + @"\tools\Bin\Build\gma.exe";
            string LINKER = APPLICATION_PATH + @"\tools\Bin\Build\slink.exe";
            string CONVERT = APPLICATION_PATH + @"\tools\Bin\Build\RcvSN8.exe";

            string INI_PATH = APPLICATION_PATH + @"\tools\Bin\Build";
            string C_INC_PATH = APPLICATION_PATH + @"\tools\C\include";
            string INC_PATH = APPLICATION_PATH + @"\tools\share\include";
            string PRJ_PATH = pPath;
            if (Directory.Exists(pPath + @"\Obj") == false)
            {
                Directory.CreateDirectory(pPath + @"\Obj"); //如果项目文件夹不存在，则新建一个
                //File.SetAttributes(pPath + @"\Obj", FileAttributes.Hidden);
            }
            string OBJ_PATH = pPath + @"\Obj";
            if (Directory.Exists(pPath + @"\Bin") == false)
            {
                Directory.CreateDirectory(pPath + @"\Bin"); //如果项目文件夹不存在，则新建一个
                //File.SetAttributes(pPath + @"\Bin", FileAttributes.Hidden);
            }
            string OUT_PATH = pPath + @"\Bin";
            File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\sample.prj", pPath + @"\sample.prj", true);
            //File.SetAttributes(pPath + @"\sample.prj", FileAttributes.Hidden);
            if (MCU_ID == "0x7011")
            {
                File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\7030.cfg", pPath + @"\sample.cfg", true);
                //File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\7030.h", pPath + @"\sample_inc.h", true);
            }
            else if (MCU_ID == "0x7031")
            {
                File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\7031.cfg", pPath + @"\sample.cfg", true);
                //File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\7031.h", pPath + @"\sample_inc.h", true);
            }
            else
            {
                File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\7041.cfg", pPath + @"\sample.cfg", true);
            }
            //File.SetAttributes(pPath + @"\sample.cfg", FileAttributes.Hidden);
            File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\sample_inc.h", pPath + @"\sample_inc.h", true);
            //File.SetAttributes(pPath + @"\sample_inc.h", FileAttributes.Hidden);
            File.Copy(APPLICATION_PATH + @"\tools\Bin\Build\sample.cop", pPath + @"\sample.cop", true);
            //File.SetAttributes(pPath + @"\sample.cop", FileAttributes.Hidden);

            string COMPILER_FLAGS = " -target=SN8P2722 -INI=\"" + INI_PATH + "\\SN8P2700A.ini\" -PROJECTNAME=\"" +
                                PRJ_PATH + "\\sample.prj\" -WL=3 -Chip_Series=2 -A -g -I" + C_INC_PATH + " -I" + INC_PATH + " -NoCALLHL -NoMUL -NoHL -NoX -PUSH2 -NoGlobalOpt -tempdir=\"" +
                                OBJ_PATH + "\"  -cpp_skip_asm -DICE_Mode=0";
            string GMA_FLAGS = " /INI:\"SN8P2700A.ini\" /ID1:0 /ID2:0  /MACHINE:SN8P2722  /Chip_Series:2 /NOPeephole: /PROJECTNAME:\"" +
                PRJ_PATH + "\\sample.prj\" /WL:3 /DEFINE:ICE_Mode=0 /DEFINE:SN8P2722=1  /OutputPath:\"" + OBJ_PATH + "\"";
            string LINK_FLAGS = " /MACHINE:SN8P2722 /Chip_Series:2 /INI:\"SN8P2700A.ini\" /WL:3 /OutputFile:\"" +
                OUT_PATH + "\\sample.out\"  /MAP:\"" + OBJ_PATH + "\\sample.map\"   /LISTFILE:  /STDLIB:\"" + OUT_PATH + "\"    /STACK: /PROJECTNAME:\"" + PRJ_PATH + "\\sample.prj\"";
            string CONV_FLAGS = " /MACHINE:SN8P2722 /INI:\"SN8P2700A.ini\" /WL:3 /OutputFile:\"" + OUT_PATH + "\\sample.sn8\"  /PROJECTNAME:\"" +
                PRJ_PATH + "\\sample.prj\" /IDSVersion:V1.20.219.140n /Chip_Series:2 /OSLIB:\"" + OUT_PATH + "\"";
            string LINK_DEP_FILES = null;

            if (MCU_ID == "0x7011")
            {
                COMPILER_FLAGS = " -target=SN8P2711 -INI=\"" + INI_PATH + "\\SN8P2700A.ini\" -PROJECTNAME=\"" +
                PRJ_PATH + "\\sample.prj\" -WL=3 -Chip_Series=2 -A -g -I" + C_INC_PATH + " -I" + INC_PATH + " -NoCALLHL -NoMUL -NoHL -NoX -PUSH2 -NoGlobalOpt -tempdir=\"" +
                OBJ_PATH + "\"  -cpp_skip_asm -DICE_Mode=0";
                GMA_FLAGS = " /INI:\"SN8P2700A.ini\" /ID1:0 /ID2:0  /MACHINE:SN8P2711  /Chip_Series:2 /NOPeephole: /PROJECTNAME:\"" +
                    PRJ_PATH + "\\sample.prj\" /WL:3 /DEFINE:ICE_Mode=0 /DEFINE:SN8P2711=1  /OutputPath:\"" + OBJ_PATH + "\"";
                LINK_FLAGS = " /MACHINE:SN8P2711 /Chip_Series:2 /INI:\"SN8P2700A.ini\" /WL:3 /OutputFile:\"" +
                    OUT_PATH + "\\sample.out\"  /MAP:\"" + OBJ_PATH + "\\sample.map\"   /LISTFILE:  /STDLIB:\"" + OUT_PATH + "\"    /STACK: /PROJECTNAME:\"" + PRJ_PATH + "\\sample.prj\"";
                CONV_FLAGS = " /MACHINE:SN8P2711 /INI:\"SN8P2700A.ini\" /WL:3 /OutputFile:\"" + OUT_PATH + "\\sample.sn8\"  /PROJECTNAME:\"" +
                    PRJ_PATH + "\\sample.prj\" /IDSVersion:V1.20.219.140n /Chip_Series:2 /OSLIB:\"" + OUT_PATH + "\"";
                LINK_DEP_FILES = null;
            }

            string Str7041_temp = null;
            if (MCU_ID == "0x7041")
            {
                Str7041_temp = "MC35P7041";
            }
            else if (MCU_ID == "0x9905")
            {
                Str7041_temp = "MC9905";
            }
            else if (MCU_ID == "0x6021")
            {
                Str7041_temp = "MC35P6021";
            }
            else if (MCU_ID == "0x2722")
            {
                Str7041_temp = "MC2722";
            }

            Regex error_key = new Regex(@"[0-9]+\s+error", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            string temp_out = null;
            int line_index = 0;
            int i = 0;
            for (i = 0; i < ASMFileName.Count; i++)
            {
                objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.(c|C)$");
                if (objNotPositivePattern.IsMatch(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ASMFileName[i] + ShowLanguage.Current.MessageBoxText39, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                else
                {
                    string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[i]);
                    string str_Compiler = null;
                    if (MCU_ID == "0x7011")
                    {
                        str_Compiler = COMPILER + COMPILER_FLAGS + " -o " + OBJ_PATH + "\\" + objectfilename + ".asm " + @".\" + objectfilename + ".c " + INC_PATH + @"\mc32p7030.h";
                    }
                    else if (MCU_ID == "0x7031")
                    {
                        str_Compiler = COMPILER + COMPILER_FLAGS + " -o " + OBJ_PATH + "\\" + objectfilename + ".asm " + @".\" + objectfilename + ".c " + INC_PATH + @"\mc32p7031.h";
                    }
                    else
                    {
                        str_Compiler = COMPILER + COMPILER_FLAGS + " -o " + OBJ_PATH + "\\" + objectfilename + ".asm " + @".\" + objectfilename + ".c " + INC_PATH + @"\MC35P7041.h";
                    }
                    try
                    {
                        startInfo.Arguments = pathroot;
                        System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                        compile.Start();

                        compile.StandardInput.WriteLine(str_Compiler);
                        compile.StandardInput.WriteLine("exit");

                        stroutput = null;
                        strerror = null;
                        compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                        compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                        compile.BeginOutputReadLine();
                        compile.BeginErrorReadLine();

                        compile.WaitForExit();
                        compile.Close();
                        temp_out = stroutput + strerror;
                        /*
                            更改松翰信息至Sinomcu
                            在SN8P2700A.ini中
	                        SN8P2711		1K			7030
	                        SN8P2722		2K			7031	7041；
	                        由外部编译器跟内部型号的的对应关系，此时只是在显示上替换掉外部名称，是为了以后仿真软件的更好维护
                        */
                        output = output + temp_out;
                        output = output.Replace("Sonix", "Sinomcu");        
                        output = output.Replace("SONiX", "SINOMCU");
                        output = output.Replace("SN8P2711", "MC32P7030");
                        output = output.Replace("SN8P2722", Str7041_temp);
                        output = output.Replace("SN8P2700A.ini", "7011.ini");
                        line_index = 0;
                        while (true)
                        {
                            Match filestr = error_key.Match(temp_out, line_index + 10);
                            if (filestr.Value == "")
                            {
                                break;
                            }
                            line_index = filestr.Index;
                            int error_num = Convert.ToInt32(filestr.Value.Substring(0, filestr.Value.Length - 5).Trim());
                            if (error_num > 0)
                            {
                                output = output + "---------Compiler code Error!...\n\n";
                                frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                                compile_result = false;
                                MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                                if (!frmmessage.IsDisposed)
                                    frmmessage.Show(dockPanelMain);
                                //deleteAllTempFile(pPath);
                                return;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            for (i = 0; i < ASMFileName.Count; i++)
            {
                string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[i]);
                string str_GMA = GMA + GMA_FLAGS + " /CSource: /CASE: " + OBJ_PATH + "\\" + objectfilename + ".asm";
                try
                {
                    startInfo.Arguments = pathroot;
                    System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                    compile.Start();
                    compile.StandardInput.WriteLine(str_GMA);
                    compile.StandardInput.WriteLine("exit");

                    stroutput = null;
                    strerror = null;
                    compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                    compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                    compile.BeginOutputReadLine();
                    compile.BeginErrorReadLine();

                    compile.WaitForExit();
                    compile.Close();
                    temp_out = stroutput + strerror;
                    output = output + temp_out;
                    output = output.Replace("Sonix", "Sinomcu");
                    output = output.Replace("SONiX", "SINOMCU");
                    output = output.Replace("SN8P2711", "MC32P7030");
                    output = output.Replace("SN8P2722", Str7041_temp);
                    output = output.Replace("SN8P2700A.ini", "7011.ini");

                    line_index = 0;
                    while (true)
                    {
                        Match filestr = error_key.Match(temp_out, line_index + 10);
                        if (filestr.Value == "")
                        {
                            break;
                        }
                        line_index = filestr.Index;
                        int error_num = Convert.ToInt32(filestr.Value.Substring(0, filestr.Value.Length - 5).Trim());
                        if (error_num > 0)
                        {
                            output = output + "---------Compiler code Error!...\n\n";
                            frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                            compile_result = false;
                            MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                            if (!frmmessage.IsDisposed)
                                frmmessage.Show(dockPanelMain);
                            //deleteAllTempFile(pPath);
                            return;
                        }
                    }
                    LINK_DEP_FILES += OBJ_PATH + "\\" + objectfilename + ".o ";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            string str_Linker = LINKER + LINK_FLAGS + " /CSource: /CASE: " + LINK_DEP_FILES;
            try
            {
                startInfo.Arguments = pathroot;
                System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                compile.Start();
                compile.StandardInput.WriteLine(str_Linker);
                compile.StandardInput.WriteLine("exit");

                stroutput = null;
                strerror = null;
                compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                compile.BeginOutputReadLine();
                compile.BeginErrorReadLine();

                compile.WaitForExit();
                compile.Close();
                temp_out = stroutput + strerror;
                output = output + temp_out;
                output = output.Replace("Sonix", "Sinomcu");
                output = output.Replace("SONiX", "SINOMCU");
                output = output.Replace("SN8P2711", "MC32P7030");
                output = output.Replace("SN8P2722", Str7041_temp);
                output = output.Replace("SN8P2700A.ini", "7011.ini");
                line_index = 0;
                while (true)
                {
                    Match filestr = error_key.Match(temp_out, line_index + 10);
                    if (filestr.Value == "")
                    {
                        break;
                    }
                    line_index = filestr.Index;
                    int error_num = Convert.ToInt32(filestr.Value.Substring(0, filestr.Value.Length - 5).Trim());
                    if (error_num > 0)
                    {
                        output = output + "---------Compiler code Error!...\n\n";
                        frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        compile_result = false;
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                        if (!frmmessage.IsDisposed)
                            frmmessage.Show(dockPanelMain);
                        //deleteAllTempFile(pPath);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string str_Convert = CONVERT + CONV_FLAGS + " " + OUT_PATH + @"\sample.out" + " " + PRJ_PATH + @"\sample.cop";
            try
            {
                startInfo.Arguments = pathroot;
                System.Diagnostics.Process compile = System.Diagnostics.Process.Start(startInfo);
                compile.Start();
                compile.StandardInput.WriteLine(str_Convert);
                compile.StandardInput.WriteLine("exit");

                stroutput = null;
                strerror = null;
                compile.OutputDataReceived += new DataReceivedEventHandler(OnDataReceivedOutput);
                compile.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceivedError);
                compile.BeginOutputReadLine();
                compile.BeginErrorReadLine();

                compile.WaitForExit();
                compile.Close();
                temp_out = stroutput + strerror;
                output = output + temp_out;
                output = output.Replace("Sonix","Sinomcu");
                output = output.Replace("SONiX", "SINOMCU");
                output = output.Replace("SN8P2711", "MC32P7030");
                output = output.Replace("SN8P2722", Str7041_temp);
                output = output.Replace("SN8P2700A.ini", "7011.ini");
                line_index = 0;
                while (true)
                {
                    Match filestr = error_key.Match(temp_out, line_index + 10);
                    if (filestr.Value == "")
                    {
                        break;
                    }
                    line_index = filestr.Index;
                    int error_num = Convert.ToInt32(filestr.Value.Substring(0, filestr.Value.Length - 5).Trim());
                    if (error_num > 0)
                    {
                        output = output + "---------Compiler code Error!...\n\n";
                        frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        compile_result = false;
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                        if (!frmmessage.IsDisposed)
                            frmmessage.Show(dockPanelMain);
                        //deleteAllTempFile(pPath);
                        return;
                    }
                }
                frmmessage.fastColoredTextBox.InsertText(output);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            compile_result = true;

            frmmessage.Refresh();
            if (!frmmessage.IsDisposed)
                frmmessage.Show(dockPanelMain);

            load_sn8(pPath);

            crisc_codfile_2711_create(pPath, projectName);
            crisc_var_create(pPath, projectName);
            deleteAllTempFile(pPath);
        }

        private void deleteAllTempFile(string project_path)
        {
            if (File.Exists(project_path + @"\sample.prj"))
            {
                File.Delete(project_path + @"\sample.prj");
            }
            if (File.Exists(project_path + @"\sample.cfg"))
            {
                File.Delete(project_path + @"\sample.cfg");
            }
            if (File.Exists(project_path + @"\sample.cop"))
            {
                File.Delete(project_path + @"\sample.cop");
            }
            if (File.Exists(project_path + @"\sample.prj.stb"))
            {
                File.Delete(project_path + @"\sample.prj.stb");
            }
            if (File.Exists(project_path + @"\sample_inc.h"))
            {
                File.Delete(project_path + @"\sample_inc.h");
            }
            if (Directory.Exists(project_path + @"\Obj"))
            {
                Directory.Delete(project_path + @"\Obj", true);
            }
            if (Directory.Exists(project_path + @"\Bin"))
            {
                Directory.Delete(project_path + @"\Bin", true);
            }
        }

        private byte[] CodArr = null;
        public void load_sn8(string path)
        {
            string fpath = path + "\\Bin\\sample.sn8";
            if (File.Exists(fpath) != true)
            {
                return;
            }
            CodArr = File.ReadAllBytes(fpath);
            UInt32 EndOffset = 0;
            if (MCU_ID == "0x7011")
            {
                EndOffset = 0x900;
            }
            else
            {
                EndOffset = 0x1100;
            }
            string ListFile = null;
            int LineNum = 0;
            UInt32 ByteOffset = 0x100;
            while (ByteOffset < EndOffset)
            {
                UInt16 CodeValue = gp_getUint16(ByteOffset);
                string str_Addr = LineNum.ToString("X4");
                string str_CodeValue = CodeValue.ToString("X4");

                ListFile += "@" + str_Addr + " " + str_CodeValue + ":";

                ListFile += str_CodeValue + "   ";
                ListFile += "\n";

                ByteOffset += 2;
                LineNum += 1;
            }

            string projectName = Path.GetFileNameWithoutExtension(ProjectPath);
            File.WriteAllText(path + "\\Output\\" + projectName + @".lst", ListFile, Encoding.Default);

            generationS19(path + "\\Output\\" + projectName + @".lst");
        }

        private UInt16 gp_getUint16(UInt32 BetysOffset)
        {
            int value;

            value = CodArr[BetysOffset];
            value |= CodArr[BetysOffset + 1] << 8;

            return (UInt16)value;
        }

        public void generationS19(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string SaveFilePath = Path.GetDirectoryName(path);
            string S19Name = Path.GetFileNameWithoutExtension(path);

            string BinCode = null;
            string S19File = "S1230000";
            int Addr = 0;
            int CheckSum = 0;
            int end_addr = 0;

            while ((BinCode = file.ReadLine()) != null)
            {
                string mcodeH = BinCode.Substring(11, 2);
                string mcodeL = BinCode.Substring(13, 2);
                S19File += mcodeL + mcodeH;

                CheckSum += Convert.ToInt16(mcodeH, 16) + Convert.ToInt16(mcodeL, 16);

                Addr += 2;
                if (Addr % 0x20 == 0)
                {
                    CheckSum += 0x23 + (Addr - 0x20) / 0x100 + (Addr - 0x20) % 0x100;
                    CheckSum = CheckSum & 0xff;
                    CheckSum = 0xff - CheckSum;
                    string str_CheckSum = CheckSum.ToString("X2");
                    S19File += str_CheckSum + "\r\n";
                    if (MCU_ID == "0x7011")
                        end_addr = 0x800;
                    else
                        end_addr = 0x1000;              //add by lyl for 7031 2K problem
                    if (Addr < end_addr)
                        S19File += "S123" + Addr.ToString("X4");
                    CheckSum = 0;

                }

            }
            file.Close();
            S19File += "S30500000134C5\r\nS903FFFFFE\r\n";

            File.WriteAllText(SaveFilePath + "\\" + S19Name + ".s19", S19File, Encoding.ASCII);

        }

        public void crisc_codfile_2711_create(string project_path, string project_name)
        {
            string fpath = project_path + "\\Obj\\sample.lst";
            if (File.Exists(fpath) != true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);
            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            textLoad.Close();

            int i = 0;
            string save_cod_str = null;
            int line_index = 0;
            if (ASMFileName.Count == 1)
            {
                string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[0]);
                string asmName = project_path + "\\Obj\\" + objectfilename + ".asm";
                FileStream asmLoad = new FileStream(asmName, FileMode.Open, FileAccess.Read);
                Encoding encodingasm = Encoding.Unicode;
                encodingasm = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(asmName);
                string list_asmfile = FastColoredTextBoxNS.FileReader.ReadFileContent(asmLoad, ref encodingasm);
                asmLoad.Close();
                line_index = 0;
                while (true)
                {
                    Regex line_key = new Regex(@"\bL+[0-9]+[\:]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match filestr = line_key.Match(list_asmfile, line_index);
                    if (filestr.Value != "")
                    {
                        line_index = filestr.Index + filestr.Value.Length;
                        save_cod_str = SearchLineNum(list_asmfile, line_index, filestr.Value, objectfilename, list_tempfile, save_cod_str);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                int iStartLine = 0;
                int iEndLine = 0;
                for (i = 0; i < ASMFileName.Count; i++)
                {
                    string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[i]);
                    iStartLine = iEndLine;
                    iEndLine = list_tempfile.Length;
                    string ListLine = null;
                    if (i == ASMFileName.Count - 1)
                    {
                        ListLine = list_tempfile.Substring(iStartLine, iEndLine - iStartLine);
                    }
                    else
                    {
                        string objectfilename1 = Path.GetFileNameWithoutExtension(ASMFileName[i + 1]);
                        while (true)
                        {
                            Regex line_key = new Regex(@"FileName[\:]" + objectfilename + @"[\.]c", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            Match filestr = line_key.Match(list_tempfile, 0);
                            if (filestr.Value != "")
                            {
                                iStartLine = filestr.Index;
                            }
                            else
                            {
                                break;
                            }
                            Regex line_key1 = new Regex(@"FileName[\:]" + objectfilename1 + @"[\.]c", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            Match filestr1 = line_key1.Match(list_tempfile, 0);
                            if (filestr1.Value != "")
                            {
                                iEndLine = filestr1.Index;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        ListLine = list_tempfile.Substring(iStartLine, iEndLine - iStartLine);
                    }

                    string asmName = project_path + "\\Obj\\" + objectfilename + ".asm";
                    FileStream asmLoad = new FileStream(asmName, FileMode.Open, FileAccess.Read);
                    Encoding encodingasm = Encoding.Unicode;
                    encodingasm = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(asmName);
                    string list_asmfile = FastColoredTextBoxNS.FileReader.ReadFileContent(asmLoad, ref encodingasm);
                    asmLoad.Close();
                    line_index = 0;
                    while (true)
                    {
                        Regex line_key = new Regex(@"\bL+[0-9]+[\:]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        Match filestr = line_key.Match(list_asmfile, line_index);
                        if (filestr.Value != "")
                        {
                            line_index = filestr.Index + filestr.Value.Length;
                            save_cod_str = SearchLineNum(list_asmfile, line_index, filestr.Value, objectfilename, ListLine, save_cod_str);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            string save_path = project_path + "\\Debug\\" + project_name + ".cod";
            File.WriteAllText(save_path, save_cod_str, Encoding.Default);
        }

        public void crisc_var_create(string project_path, string project_name)
        {
            string fpath = project_path + "\\Obj\\sample.map";
            if (File.Exists(fpath) != true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            textLoad.Close();

            Regex fun_temp = new Regex(@"0X[A-F0-9]+\s+0X[A-F0-9]+\s+Data+\s+\w+@segment", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            string save_var = "";
            int line_index = 0;
            while (true)
            {
                Match m_file_str = fun_temp.Match(list_tempfile, line_index);
                if (m_file_str.Value == "")
                    break;
                line_index = m_file_str.Index + m_file_str.Value.Length;
                string[] varCfg = m_file_str.Value.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string varName = varCfg[3].Substring(1, varCfg[3].Length - 9);
                string varAddr = "0x" + Convert.ToInt32(varCfg[0].Substring(2, varCfg[0].Length - 2).Trim(), 16).ToString("X4");
                string varNum = varCfg[1].Substring(2, varCfg[1].Length - 2).Trim();
                save_var += varName + " at " + varAddr + " " + varNum + " xxxx.x 0000 to 0000\n";
            }

            string save_path = project_path + "\\System\\" + project_name + ".var";
            File.WriteAllText(save_path, save_var, Encoding.Default);
        }

        private string SearchLineNum(string asmfile, int index, string sLine, string project_name, string LineList, string cod_str)
        {
            string str = null;
            int line_index = asmfile.Length;
            while (true)
            {
                Regex line_key0 = new Regex(@"L+[0-9]+[\:]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match filestr0 = line_key0.Match(asmfile, index);
                if (filestr0.Value != "")
                {
                    line_index = filestr0.Index + filestr0.Value.Length;
                    str = asmfile.Substring(index, line_index - index);
                    Regex line_key1 = new Regex(@"Line+[\#]+[0-9]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match filestr1 = line_key1.Match(str, 0);
                    if (filestr1.Value != "")
                    {
                        int asm_index = 0;
                        while (true)
                        {
                            Regex line_key = new Regex(sLine, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            Match filestr = line_key.Match(LineList, asm_index);
                            if (filestr.Value != "")
                            {
                                asm_index = filestr.Index + filestr.Value.Length;
                                int asm_index1 = LineList.Length;
                                Regex line_key3 = new Regex(@"L+[0-9]+[\:]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                Match filestr3 = line_key3.Match(LineList, asm_index);
                                if (filestr3.Value != "")
                                {
                                    asm_index1 = filestr3.Index + filestr3.Value.Length;
                                }
                                string strtemp = LineList.Substring(asm_index, asm_index1 - asm_index);
                                while (true)
                                {
                                    Regex line_key2 = new Regex(@"[A-F0-9]+[\:]+\s[A-F0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                    Match filestr2 = line_key2.Match(strtemp, 0);
                                    if (filestr2.Value != "")
                                    {
                                        string line_num = filestr1.Value.Substring(5, filestr1.Value.Length - 5).Trim();
                                        string file_name = project_name + ".c";
                                        int iValue = Convert.ToInt32(filestr2.Value.Substring(0, filestr2.Value.IndexOf(":")).Trim(), 16);
                                        string pc_value = iValue.ToString("X4");
                                        cod_str += "\nL 8           ; " + line_num + "   [1]\t" + pc_value + "\t " + file_name;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    str = asmfile.Substring(index, line_index - index);
                    Regex line_key1 = new Regex(@"Line+[\#]+[0-9]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match filestr1 = line_key1.Match(str, 0);
                    if (filestr1.Value != "")
                    {
                        int asm_index = 0;
                        while (true)
                        {
                            Regex line_key = new Regex(sLine, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            Match filestr = line_key.Match(LineList, asm_index);
                            if (filestr.Value != "")
                            {
                                asm_index = filestr.Index + filestr.Value.Length;
                                int asm_index1 = LineList.Length;
                                Regex line_key3 = new Regex(@"L+[0-9]+[\:]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                Match filestr3 = line_key3.Match(LineList, asm_index);
                                if (filestr3.Value != "")
                                {
                                    asm_index1 = filestr3.Index + filestr3.Value.Length;
                                }
                                string strtemp = LineList.Substring(asm_index, asm_index1 - asm_index);
                                while (true)
                                {
                                    Regex line_key2 = new Regex(@"[A-F0-9]+[\:]+\s[A-F0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                    Match filestr2 = line_key2.Match(strtemp, 0);
                                    if (filestr2.Value != "")
                                    {
                                        string line_num = filestr1.Value.Substring(5, filestr1.Value.Length - 5).Trim();
                                        string file_name = project_name + ".c";
                                        int iValue = Convert.ToInt32(filestr2.Value.Substring(0, filestr2.Value.IndexOf(":")).Trim(), 16);
                                        string pc_value = iValue.ToString("X4");
                                        cod_str += "\nL 8           ; " + line_num + "   [1]\t" + pc_value + "\t " + file_name;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return cod_str;
        }

        private string GetRamFromMap()
        {
            string save_cod_str = "";
            char[] RAMDataUserBuffer = new char[1024];
            int i = 0;
            for (i = 0; i < DeviceConfigXX.MCU_RAMSize; i++)
            {
                RAMDataUserBuffer[i] = '-';
            }
            if (MCU_TYPE == "CRISC")
            {
                string project_path = Path.GetDirectoryName(ProjectPath);
                string project_name = Path.GetFileNameWithoutExtension(ProjectPath);
                string fpath = project_path + "\\Output\\" + project_name + ".map";
                if (File.Exists(fpath) != true)
                {
                    return save_cod_str;
                }
                FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
                string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();

                save_cod_str += "\nRAM USAGE MAP ('X' = Used,  '-' = Unused)";
                int line_index = 0;
                Regex line_key = new Regex(@"[\w]+\s+udata+\s+[\w]+\s+data+\s+[\w]{8}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                while (true)
                {
                    Match filestr = line_key.Match(list_tempfile, line_index + 20);
                    if (filestr.Value == "")
                    {
                        break;
                    }
                    line_index = filestr.Index;
                    string line_txt = filestr.Value;
                    int line_index1 = 0;
                    Regex pc_key = new Regex(@"udata+\s+[\w]{8}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match pcstr = pc_key.Match(line_txt, line_index1);
                    if (pcstr.Value == "")
                    {
                        break;
                    }
                    line_index1 = pcstr.Index;
                    string pc_txt = pcstr.Value;

                    Regex pc_key1 = new Regex(@"data+\s+[\w]{8}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match pcstr1 = pc_key1.Match(line_txt, line_index1 + 5);
                    if (pcstr1.Value == "")
                    {
                        break;
                    }
                    line_index1 = pcstr1.Index;
                    string pc_txt1 = pcstr1.Value;

                    int line_num = int.Parse(pc_txt.Substring(pc_txt.Length - 6, 6), NumberStyles.HexNumber);
                    int file_name = int.Parse(pc_txt1.Substring(pc_txt1.Length - 6, 6), NumberStyles.HexNumber);

                    for (i = line_num; i < line_num + file_name; i++)
                    {
                        RAMDataUserBuffer[i] = 'X';
                    }
                }
            }
            int index = 0;
            for (i = 0; i < DeviceConfigXX.MCU_RAMSize; i++)
            {
                if (index % 0x40 == 0)
                {
                    save_cod_str += "\n" + index.ToString("X4") + " : ";
                }
                save_cod_str += RAMDataUserBuffer[i];
                index++;
                if (index % 0x10 == 0)
                {
                    save_cod_str += " ";
                }
            }
            save_cod_str += "\n\n";
            return save_cod_str;
        }

        private static void OnDataReceivedOutput(object Sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                stroutput += e.Data + "\n";
            }
        }

        private static void OnDataReceivedError(object Sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                strerror += e.Data + "\n";
            }
        }

        /// <summary>RISC CPU 14bit/16bit ASM compiler
        /// V 0.0.4
        /// 
        /// </summary>
        private void RISC_compiler()
        {
            System.Diagnostics.Process compile = new System.Diagnostics.Process();
            compile.StartInfo.FileName = "tools\\gpasm.exe";
            //compile.StartInfo.FileName = "cmd.exe";
            compile.StartInfo.UseShellExecute = false;
            frmmessage.fastColoredTextBox.Text = "";
            frmmessage.fastColoredTextBox.InsertText("Compile Starting....\n");

            string gpasm_path = APPLICATION_PATH + "\\tools\\gpasm.exe";
            if (File.Exists(gpasm_path) == false)
            {
                //MessageBox.Show("Can't Find " + ASM05_Constant_Define.ASSEMBLY_NAME + "!");
                MessageBox.Show(ShowLanguage.Current.MessageBoxText33);
                return;
            }


            Regex objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.exe$");
            if (objNotPositivePattern.IsMatch(gpasm_path) == false)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText37, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            GetProjectASM_FilesName();
            //get asmfile path
            string pPath = ProjectPath/*GetProjectPath()*/;
            string projectName = Path.GetFileNameWithoutExtension(pPath);
            //pPath = pPath.Substring(0, pPath.LastIndexOf("."));
            pPath = pPath.Substring(0, pPath.LastIndexOf("\\"));
            string output = null;

            compile.StartInfo.RedirectStandardInput = true;
            compile.StartInfo.RedirectStandardOutput = true;
            compile.StartInfo.RedirectStandardError = true;
            compile.StartInfo.CreateNoWindow = true;
            compile.StartInfo.WorkingDirectory = pPath; //APPLICATION_PATH + "\\tools"; //这句很关键
            compile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            string str_mcuName = null;
            string str_lkr = null;

            switch (MCU_ID)
            {
                case "0x0301":
                case "0x0311":
                    str_mcuName = "-p 0311 -c ";
                    str_lkr = " -s x3501.lkr -o output\\";
                    break;
                case "0x3401":
                    str_mcuName = "-p 3401 -c ";
                    str_lkr = " -s x3501.lkr -o output\\";
                    break;
                case "0x3111":
                    str_mcuName = "-p 3111 -c ";
                    str_lkr = " -s x3502.lkr -o output\\";
                    break;
                case "0x0314":
                    str_mcuName = "-p 30p44 -c ";
                    str_lkr = " -s x0314.lkr -o output\\";
                    break;
                case "0x3221":
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3721.lkr -o output\\";
                    break;
                case "0x32821":
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3721.lkr -o output\\";
                    break;
                case "0x3264":
                    str_mcuName = "-p 3264 -c ";
                    str_lkr = " -s x3764.lkr -o output\\";
                    break;
                case "0x3378":
                    str_mcuName = "-p 3394 -c ";
                    str_lkr = " -s x3778.lkr -o output\\";
                    break;
                case "0x3316":
                    str_mcuName = "-p 3316 -c ";
                    str_lkr = " -s x3716.lkr -o output\\";
                    break;
                case "0x5312":
                    str_mcuName = "-p 3316 -c ";
                    str_lkr = " -s x3716.lkr -o output\\";
                    break;
                case "0x7212":
                    str_mcuName = "-p 3394 -c ";
                    str_lkr = " -s x3778.lkr -o output\\";
                    break;
                case "0x9902":
                    str_mcuName = "-p 9902 -c ";
                    str_lkr = " -s x9902.lkr -o output\\";
                    break;
                case "0x7333":
                    str_mcuName = "-p 7333 -c ";
                    str_lkr = " -s x7333.lkr -o output\\";
                    break;
                case "0x9903":
                    str_mcuName = "-p 9903 -c ";
                    str_lkr = " -s x9903.lkr -o output\\";
                    break;
                case "0x9904":
                    str_mcuName = "-p 9904 -c ";
                    str_lkr = " -s x9904.lkr -o output\\";
                    break;
                case "0x7122":
                    str_mcuName = "-p 7122 -c ";
                    str_lkr = " -s x7122.lkr -o output\\";
                    break;
                case "0x3220":
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3221.lkr -o output\\";
                    break;
                case "0x3394":
                    str_mcuName = "-p 3394 -c ";
                    str_lkr = " -s x3894.lkr -o output\\";
                    break;
                case "0x7022":// TODO wj
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3221.lkr -o output\\";
                    break;
                case "0x6060":// TODO wj
                    str_mcuName = "-p 0311 -c ";
                    str_lkr = " -s x3501.lkr -o output\\";
                    break;
                case "0x7510":// TODO wj
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3721.lkr -o output\\";
                    break;
                case "0x3222":// TODO wj
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3222.lkr -o output\\";
                    break;
                case "0x8132":// TODO wj
                    str_mcuName = "-p 3264 -c ";
                    str_lkr = " -s x3764.lkr -o output\\";
                    break;
                case "0x7311":// TODO wj
                    //str_mcuName = "-p 3264 -c ";
                    str_mcuName = "-p 7311 -c ";
                    str_lkr = " -s x7311.lkr -o output\\";
                    break;
                case "0x7321":
                    str_mcuName = "-p 3264 -c ";
                    str_lkr = " -s x7311.lkr -o output\\";
                    break;
                case "0x7011":// TODO wj
                case "0x7031":
                    str_mcuName = "-p 7011 -c ";
                    str_lkr = " -s x7011.lkr -o output\\";
                    break;
                case "0x7041":
                case "0x9905":
                case "0x6021":
                case "0x2722":
                    if (frmMAIN.RomSpace_stat == 1)
                    {
                        str_mcuName = "-p 2K7041 -c ";
                        str_lkr = " -s x2K7041.lkr -o output\\";
                    }

                    else if (frmMAIN.RomSpace_stat == 0)
                    {
                        str_mcuName = "-p 1K7041 -c ";
                        str_lkr = " -s x1K7041.lkr -o output\\";
                    }
                    break;
                case "0x7511":// TODO wj
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3721.lkr -o output\\";
                    break;
                case "0x5222":
                    str_mcuName = "-p 5222 -c ";
                    str_lkr = " -s x5222.lkr -o output\\";
                    break;
                case "0x7323":// TODO wj
                    {
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "-p 8K7323 -c ";
                            str_lkr = " -s x8K7323.lkr -o output\\";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "-p 4K7323 -c ";
                            str_lkr = " -s x4K7323.lkr -o output\\";
                        }   
                    }
                    break;
                case "0x7312":
                    str_mcuName = "-p 3221 -c ";
                    str_lkr = " -s x3721.lkr -o output\\";
                    break;
                case "0x6080":// TODO wj
                    str_mcuName = "-p 0311 -c ";
                    str_lkr = " -s x3501.lkr -o output\\";
                    break;
                case "0x6220":      //modify by 6080 lyl
                    {
                        if (frmMAIN.RomSpace_stat == 1)
                        {
                            str_mcuName = "-p 1K6220 -c ";
                            str_lkr = " -s x1K6220.lkr -o output\\";
                        }
                        else if (frmMAIN.RomSpace_stat == 0)
                        {
                            str_mcuName = "-p 05K6220 -c ";
                            str_lkr = " -s x05K6220.lkr -o output\\";
                        }
                    }                
                    break;
                default:
                    MessageBox.Show("no this mcu!");
                    return;
            }


            for (int i = 0; i < ASMFileName.Count; i++)
            {
                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                else
                {
                    objNotPositivePattern = new Regex(@"([a-zA-Z]:\\)[^\/\:\s\*\?\""\<\>\|\,]+.(asm|ASM)$");
                    if (objNotPositivePattern.IsMatch(ASMFileName[i]) == false)
                    {
                        MessageBox.Show(ASMFileName[i] + ShowLanguage.Current.MessageBoxText39, ShowLanguage.Current.MessageBoxText36, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string objectfilename = Path.GetFileNameWithoutExtension(ASMFileName[i]);
                    str_mcuName = str_mcuName + "-I " + APPLICATION_PATH + "INC " + "-o System\\" + objectfilename + " ";

                    compile.StartInfo.Arguments = str_mcuName + objectfilename + ".asm";
                    compile.Start();
                    //compile.StandardInput.WriteLine(@"cd\");
                    //compile.StandardInput.WriteLine(dir);
                    //compile.StandardInput.WriteLine("cd "+APPLICATION_PATH+"\\tools");
                    //compile.StandardInput.WriteLine(@"gpasm.exe " + mainFileName);

                    //compile.StandardInput.WriteLine("exit");
                    output = output + compile.StandardOutput.ReadToEnd();//编译信息输出变量
                    compile.WaitForExit();

                    string errorflag = output.IndexOf(@"(GPASM) Successful ").ToString();
                    if (errorflag == "-1")
                    {
                        output = output + "---------Compiler code Error!...\n\n";
                        frmmessage.fastColoredTextBox.InsertText(output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        compile_result = false;
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText36, "Error!");
                        if (!frmmessage.IsDisposed)
                            frmmessage.Show(dockPanelMain);
                        return;
                    }
                }
            }

            //link----------------------------------------------------------------------------------------------
            compile.StartInfo.FileName = "tools\\gplink.exe";
            string strObjectfile = null;

            for (int i = 0; i < ASMFileName.Count; i++)
            {
                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                strObjectfile = strObjectfile + "System\\" + Path.GetFileNameWithoutExtension(ASMFileName[i]) + ".o ";
            }
            //compile.StartInfo.Arguments = "-I " + APPLICATION_PATH + "LIB " + " -s 30p01a.lkr -o output\\" + projectName + " -c " + strObjectfile;
            compile.StartInfo.Arguments = "-I " + APPLICATION_PATH + "LIB " + str_lkr + projectName + " -c " + strObjectfile;
            compile.Start();
            //compile.StandardInput.WriteLine(@"cd\");
            //compile.StandardInput.WriteLine(dir);
            //compile.StandardInput.WriteLine("cd "+APPLICATION_PATH+"\\tools");
            //compile.StandardInput.WriteLine(@"gpasm.exe " + mainFileName);

            //compile.StandardInput.WriteLine("exit");
            output = output + compile.StandardOutput.ReadToEnd();//编译信息输出变量
            string str = "";
            int errorflag1 = output.IndexOf(@"Program Memory Words Used");
            if (errorflag1 != -1)
            {
                str = @"(FF) output\" + projectName + ".lst";
                int errorflag2 = output.IndexOf(str);
                if (errorflag2 != -1)
                {
                    str = output.Substring(errorflag1, errorflag2 - errorflag1);
                    output = output.Remove(errorflag1, errorflag2 - errorflag1);
                }
            }
            compile.WaitForExit();
            frmmessage.fastColoredTextBox.InsertText(output + str + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));// = output + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            errorflag1 = output.IndexOf("(GPLINK) Successful ");
            compile_result = true;
            if (errorflag1 == -1)
            {
                compile_result = false;
                MessageBox.Show(ShowLanguage.Current.MessageBoxText41, "Error!");
            }
            if (!frmmessage.IsDisposed)
                frmmessage.Show(dockPanelMain);

            //--var file process--------------------------------------------------------------
            ARISC_VARfileProcess();

            return;
        }



        private void ARISC_VARfileProcess()
        {

            FastColoredTextBox newValiableFile = new FastColoredTextBox();
            newValiableFile.AcceptsTab = true;

            string project_path = Path.GetDirectoryName(ProjectPath/*GetProjectPath()*/);
            string project_name = Path.GetFileNameWithoutExtension(ProjectPath/*GetProjectPath()*/);

            newValiableFile.Text = "\n";

            for (int i = 0; i < ASMFileName.Count; i++)
            {
                if (File.Exists(ASMFileName[i]) == false)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText34 + ASMFileName[i]);
                    return;
                }
                string list_file_name = "\\System\\" + Path.GetFileNameWithoutExtension(ASMFileName[i]) + ".lst";

                string fpath = project_path + list_file_name;

                if (File.Exists(fpath) != true)
                {
                    return;
                }

                FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
                string gValiableFile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();

                //at+\s+0x[a-f0-9]+\-+0x[a-f0-9]{4}  addr;
                Regex line_key = new Regex(@"\b[\w]+[^\n\r]+[A-Fa-f0-9]{8}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
                int file_index = 0;
                while (true)
                {
                    Match str_line = line_key.Match(gValiableFile, file_index);
                    if (str_line.Value == "")
                    {
                        break;
                    }
                    else
                    {
                        string temp_str = str_line.Value;
                        file_index = str_line.Index + str_line.Length;
                        string var_name = temp_str.Substring(0, temp_str.Length - 8);

                        int a = temp_str.Length - 4;
                        // int b = temp_str.Length;
                        string var_value = "0x" + temp_str.Substring(a, 4);

                        newValiableFile.AppendText(var_name + " at " + var_value + "  xxxx.x  0000 to 0000\n");
                    }

                }

                //create bit variable
                //#define+\s+[\w]+\s+[\w]+,+[0-9]\b
                //                 Regex line_key_def = new Regex(@"#define+\s+[\w]+\s+[\w]+,+[0-9]\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
                //                 int file_def_index = 0;
                //                 while (true)
                //                 {
                //                     Match str_line = line_key_def.Match(gValiableFile, file_def_index);
                //                     if (str_line.Value == "")
                //                     {
                //                         break;
                //                     }
                //                     else
                //                     {
                //                         string temp_str = str_line.Value;
                //                         file_def_index = str_line.Index + str_line.Length;
                //                         string var_name = temp_str.Substring(0, temp_str.Length - 2);
                // 
                //                         int a = temp_str.Length - 1;
                //                         // int b = temp_str.Length;
                //                         string var_value = ".bitn.bit" + temp_str.Substring(a, 1);
                // 
                //                         newValiableFile.AppendText(var_name +var_value + "\n");
                //                     }
                // 
                //                 }

                //create owner define
                //#define+\s+[\w]+\s+[\w]\b
                Regex line_key_def1 = new Regex(@"#define+\s+[\w]+\s+[^\r\n;]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
                int file_def_index = 0;
                while (true)
                {
                    Match str_line = line_key_def1.Match(gValiableFile, file_def_index);
                    if (str_line.Value == "")
                    {
                        break;
                    }
                    else
                    {
                        string temp_str = str_line.Value;
                        //Regex line_key_def2 = new Regex(@"#define+\s+[\w]+\s+[\w]+,+[0-9]\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
                        //Match str_line1 = line_key_def2.Match(temp_str, 0);
                        //if (str_line1.Value == "")
                        newValiableFile.AppendText(temp_str + "\n");
                        file_def_index = str_line.Index + str_line.Length;
                        //string var_name = temp_str.Substring(0, temp_str.Length - 2);

                        //int a = temp_str.Length - 1;
                        // int b = temp_str.Length;
                        //string var_value = ".bitn.bit" + temp_str.Substring(a, 1);

                        //newValiableFile.AppendText(temp_str + "\n");
                    }

                }


            }
            string var_path = project_path + "\\System\\" + project_name + ".var";
            File.WriteAllText(var_path, newValiableFile.Text, Encoding.Default);

        }

        private void uinASM_compiler()
        {
            //显示提示信息
            //string path = Application.StartupPath;
            //string FileName = path +@"\tools\hc05-as.exe";
            StreamWriter sw;  // 定义输 sw 作为Shell的标准输入，即命令 
            StreamReader sr; // 定义输 sr 作为Shell的标准输出，即正常结果
            StreamReader err; // 定义输 err 作为Shell的错误输出，即出错结果
            string strerr = "";

            System.Diagnostics.Process ASMCompiler = new System.Diagnostics.Process();
            frmmessage.fastColoredTextBox.Text = ""; //clear messageOutput box
            ASMCompiler.StartInfo = new System.Diagnostics.ProcessStartInfo();
            ASMCompiler.StartInfo.FileName = "tools\\hc05-as.exe";
            ASMCompiler.StartInfo.Arguments = "-l" + " " + "G:\\TempCode\\ABCDE\\onlyASM002.s"; //命令+空格+绝对路径

            frmmessage.fastColoredTextBox.InsertText("Start compile....\n");
            frmmessage.fastColoredTextBox.InsertText(ASMCompiler.StartInfo.FileName + ASMCompiler.StartInfo.Arguments + "\n");

            ASMCompiler.StartInfo.RedirectStandardOutput = true;
            ASMCompiler.StartInfo.RedirectStandardError = true;
            ASMCompiler.StartInfo.RedirectStandardInput = true;
            ASMCompiler.StartInfo.UseShellExecute = false;
            ASMCompiler.StartInfo.CreateNoWindow = true;//让窗体不显示 
            ASMCompiler.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ASMCompiler.Start();
            sw = ASMCompiler.StandardInput;
            sr = ASMCompiler.StandardOutput;
            err = ASMCompiler.StandardError;
            sw.AutoFlush = true;
            sw.Close();
            string aaaa = "";
            // System.IO.StreamReader reader = ASMCompiler.StandardOutput; //截取输出结果
            //只有等进程退出后才能真正拿到 输出的内容
            aaaa = sr.ReadToEnd() + err.ReadToEnd();
            ASMCompiler.WaitForExit();//等待控制台程序执行完毕
            //ASMCompiler.Close(); //关闭进程
            frmmessage.fastColoredTextBox.InsertText(aaaa);

            //// ASMCompiler.StartInfo.FileName = "hc05-as.exe";
            ASMCompiler.StartInfo.Arguments = "-l" + " " + "G:\\TempCode\\ABCDE\\sun001.s";

            frmmessage.fastColoredTextBox.InsertText(ASMCompiler.StartInfo.FileName + ASMCompiler.StartInfo.Arguments + "\n");

            ASMCompiler.Start();
            sw = ASMCompiler.StandardInput;
            sr = ASMCompiler.StandardOutput;
            err = ASMCompiler.StandardError;
            sw.AutoFlush = true;
            sw.Close();
            aaaa = "";
            // // System.IO.StreamReader reader = ASMCompiler.StandardOutput; //截取输出结果
            // //只有等进程退出后才能真正拿到 输出的内容
            strerr = err.ReadToEnd();
            aaaa = sr.ReadToEnd() + strerr;
            ASMCompiler.WaitForExit();//等待控制台程序执行完毕
            //// ASMCompiler.Close(); //关闭进程
            frmmessage.fastColoredTextBox.InsertText(aaaa);
            if (strerr.Length > 50)
            {
                ASMCompiler.Close(); //关闭进程
                return;
            }
            ASMCompiler.StartInfo.FileName = "tools\\hc05-ld.exe";
            ASMCompiler.StartInfo.Arguments = "-o" + " " + "G:\\TempCode\\ABCDE\\onlyASM002.elf" + " " + "G:\\TempCode\\ABCDE\\onlyASM002.obj" + " " + "G:\\TempCode\\ABCDE\\sun001.obj";

            frmmessage.fastColoredTextBox.InsertText(ASMCompiler.StartInfo.FileName + ASMCompiler.StartInfo.Arguments + "\n");

            ASMCompiler.Start();
            sw = ASMCompiler.StandardInput;
            sr = ASMCompiler.StandardOutput;
            err = ASMCompiler.StandardError;
            sw.AutoFlush = true;
            sw.Close();
            aaaa = "";
            // // System.IO.StreamReader reader = ASMCompiler.StandardOutput; //截取输出结果
            // //只有等进程退出后才能真正拿到 输出的内容
            strerr = sr.ReadToEnd();
            aaaa = strerr + err.ReadToEnd();
            ASMCompiler.WaitForExit();//等待控制台程序执行完毕
            ASMCompiler.Close(); //关闭进程

            frmmessage.fastColoredTextBox.InsertText(aaaa);
            if (strerr.Length == 0)
            {
                frmmessage.fastColoredTextBox.InsertText("\nCompile is OK.");
            }
            else
            {
                frmmessage.fastColoredTextBox.InsertText("\nCompile error.");
            }

        }
    }
}