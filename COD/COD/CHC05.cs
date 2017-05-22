using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using COD;

///process hc05 c code debug file
namespace COD
{
    public class CHC05_cod 
    {
        public void chc05_codfile_create(string project_path,string project_name)
        {
            FastColoredTextBox h05file = new FastColoredTextBox();
            h05file.AcceptsTab = true;

            //string fpath = @"G:\Emulator\IDE\testCode\mc20p24\mc20p24.lst";
            string fpath = project_path + "\\" + project_name + ".lst";
            if (File.Exists(fpath)!=true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);
            
            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            textLoad.Close();

            //string filename = @"G:\Emulator\IDE\testCode\mc20p24\mc20p24.txt";
            string filename = project_path + "\\System\\" + project_name + ".m";
            if (File.Exists(filename) != true)
            {
                return;
            }
            FileStream filenameLoad = new FileStream(filename, FileMode.Open, FileAccess.Read);
            string filename_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(filenameLoad, ref encoding);
            //Regex file_key = new Regex(@"[\w]+[\.]+(h|c)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            Regex file_key = new Regex(@"[\S]+[\.]+(h|c)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            int filename_index = 0;
            List<int> addr_storage = new List<int>();
            List<string> name_storage = new List<string>();
            List<string> name_storageALL = new List<string>();
            while (true)
            {
                Match filestr = file_key.Match(filename_tempfile, filename_index);
                if (filestr.Value == "")
                {
                    break;
                }
                int filename_fir_index = filestr.Index;
                string filename_txt = filestr.Value;
                name_storageALL.Add(filename_txt);
                filename_index = filestr.Index + filestr.Length;
                filestr = file_key.Match(filename_tempfile, filename_index);

                int filename_sec_index = 0;
                if (filestr.Value == "")
                    filename_sec_index = filename_tempfile.LastIndexOf("\n");                   
                else
                 filename_sec_index = filestr.Index + filestr.Length;

                string file_temp = filename_tempfile.Substring(filename_fir_index, filename_sec_index - filename_fir_index);

                
                //at+\s+0x[a-f0-9]+\-+0x[a-f0-9]{4}  addr;
                Regex fileaddr_key = new Regex(@"lines+\s[0-9]+\sto+\s[0-9]+\s+at+\s+0x[a-f0-9]+\-", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match fileaddrstr = fileaddr_key.Match(file_temp, 0);

                filename_sec_index = fileaddrstr.Index;
                //string addr_max=file_temp.Substring()

                string min_addr = null;
                string max_addr = null;
                if (fileaddrstr.Value != "")
                {
                     min_addr = fileaddrstr.Value.Substring(fileaddrstr.Value.LastIndexOf("0x"), 6);
                    addr_storage.Add(Convert.ToInt32(min_addr, 16));
                     max_addr = file_temp.Substring(file_temp.LastIndexOf("-0x")+1, 6);
                    addr_storage.Add(Convert.ToInt32(max_addr,16));
                    name_storage.Add(filename_txt);
                    //h05file.AppendText(filename_txt + "  " + fileaddrstr.Value +" "+max_addr+ "\n");
                }

                //var file process
                //[\w]+\s+at+\s+0x[a-fA-F0-9]+\b
                //[\w|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\b
                string variable_globle=null;
                if (filename_sec_index == 0)
                    variable_globle = file_temp;
                else
                    variable_globle = file_temp.Substring(0, filename_sec_index); //just collect globle valiable

                int var_index = 0;
                while(true)
                {
                    fileaddr_key = new Regex(@"[\S|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    fileaddrstr = fileaddr_key.Match(variable_globle, var_index);

                    if (fileaddrstr.Value=="")
                    {
                        break;
                    } 
                    else
                    {
                        h05file.AppendText(fileaddrstr.Value + "  xxxx.x  0000 to 0000\n");
                        var_index = fileaddrstr.Index + fileaddrstr.Length;
                    }

                }
                //collect temp variable
                string variable_temp = null;
                if (filename_sec_index==0)
                {
                    continue;
                }
                variable_temp = file_temp.Substring(filename_sec_index, file_temp.Length - filename_sec_index);

                var_index = 0;
                while (true)
                {
                    fileaddr_key = new Regex(@"[\S|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\r", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    fileaddrstr = fileaddr_key.Match(variable_temp, var_index);

                    if (fileaddrstr.Value == "")
                    {
                        break;
                    }
                    else
                    {
                        //lines calculate
                        string str_lines = variable_temp.Substring(0, fileaddrstr.Index);
                        int lines_index=0;
                        while(true)
                        {
                            Regex lines_key = new Regex(@"[0-9]+\s+to+\s+[0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            Match lines_match = lines_key.Match(str_lines, lines_index);
                            if (lines_match.Value=="")
                            {
                                break;
                            } 
                            else
                            {
                                min_addr=lines_match.Value;
                                lines_index = lines_match.Index + lines_match.Length;
                            }
                          }

                        string temp = fileaddrstr.Value;
                        temp = temp.Substring(0, temp.Length - 1);
                        h05file.AppendText(temp +"  "+filename_txt+"  "+min_addr+"\n");
                        var_index = fileaddrstr.Index + fileaddrstr.Length;
                    }
                }
            }

            //-------add define flag string------------------------
            int file_count = name_storageALL.Count;
            for (int i = 0; i < file_count;i++ )
            {
                string file_path = project_path + "\\" + name_storageALL[i];
                //string filename_tempfile = null;
                if (File.Exists(file_path)==false) //file exists
                {
                    file_path = Application.StartupPath + "\\WinC_V\\H6805\\" + name_storageALL[i];
                }

                if (File.Exists(file_path) == false) //file exists
                {
                    //file_path = Application.StartupPath + "\\WinC_V\\H6805\\" + name_storageALL[i];
                    continue;
                }

                filenameLoad = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                filename_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(filenameLoad, ref encoding);

                int define_index = 0;
                while(true)
                {
                    Regex lines_key = new Regex(@"#define+\s+[\S|(\[[^\]\])]+\s+((0x[A-Fa-f0-9]+\b)|([0-9]+\b)|([\w]+[\.]bitn[\.]bit[0-9]))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    Match lines_match = lines_key.Match(filename_tempfile,define_index);

                    if (lines_match.Value=="")
                    {
                        break;
                    }

                    define_index = lines_match.Index + lines_match.Length;
                    h05file.AppendText(lines_match.Value + "\n");
                }
                

            }

            string var_path = filename.Substring(0, filename.LastIndexOf(".")) + ".var";
            File.WriteAllText(var_path, h05file.Text, Encoding.Default);

            h05file.Text = "";

            //list file process
            //get asmfileName
            //L+\s+[0-9]+\s+;+\s+[0-9]+\s
            int file_index = 0;
            Regex line_key = new Regex(@"(L+\s+[0-9]+\s+;+\s+[0-9]+\s)|(xdef)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            while(true)
            {
                Match asmfstr = line_key.Match(list_tempfile, file_index);
                if (asmfstr.Value == "")
                {
                    //MessageBox.Show("empty string!");
                    break;
                }
                int file_fir_index = asmfstr.Index;
                string line_value = asmfstr.Value;

                file_index = file_fir_index + asmfstr.Length;

                asmfstr = line_key.Match(list_tempfile, file_index);
                if (asmfstr.Value == "")
                {
                    //MessageBox.Show("empty string!");
                    break;
                }
                int file_sec_index = asmfstr.Index+asmfstr.Length;
                //file_index = file_sec_index + asmfstr.Length;

                string cod_temp = list_tempfile.Substring(file_fir_index, file_sec_index - file_fir_index);

                //[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]+\s  addr;
                Regex addr_key = new Regex(@"\[[^\]]*\]\s+[A-Fa-f0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match addrstr = addr_key.Match(cod_temp,0);
                if (addrstr.Value!="")
                {
                    string current_addr_str = addrstr.Value.Substring(addrstr.Value.Length-5,4);
                    int current_addr = Convert.ToInt32(current_addr_str,16);

                    string file_str = null;

                    for (int a = 0; a < addr_storage.Count; )
                    {
                        if ((addr_storage[a]<=current_addr)&&(current_addr<=addr_storage[a+1]))
                        {
                            file_str = name_storage[a / 2];
                            break;
                        } 
                        if(a==addr_storage.Count)
                        {
                            file_str = fpath.Substring(0, fpath.LastIndexOf(".")) + ".c";
                            break;
                        }
                        a = a + 2;
                    }

                    h05file.AppendText(line_value+"  "+addrstr.Value+" "+file_str+"\n");
                }
               

            }
            string save_path = project_path + "\\Debug\\" + project_name +".cod";
            File.WriteAllText(save_path, h05file.Text, Encoding.Default);

        }
    }
}