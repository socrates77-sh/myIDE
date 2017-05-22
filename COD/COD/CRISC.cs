
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

    public class CRISC_cod
    {
        public void crisc_codfile_create(string project_path, string project_name)
        {
            FastColoredTextBox hc05file = new FastColoredTextBox();
            hc05file.AcceptsTab = true;

            //load s19 file
            string fpath = project_path + "\\Output\\" + project_name + ".lst";
            if (File.Exists(fpath) != true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            hc05file.Text = list_tempfile;
            textLoad.Close();

            //search line xx; "abcd.c"
            string save_cod_str = "";
            int line_index = 0;
            Regex line_key = new Regex(@"line\s+[0-9]+;\s+[\""]+[\w]+[\.][c|h|H|C]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            while (true)
            {
                Match filestr = line_key.Match(list_tempfile, line_index);
                if (filestr.Value == "")
                {
                    break;
                }
                //line	76; "YKQ3011.c 
                line_index = filestr.Index;
                string line_txt = filestr.Value;
                //^[a-f0-9]+\s+[a-f0-9]{4}\b
                //Regex pc_key = new Regex(@"[a-f0-9]+\s+[a-f0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Regex pc_key = new Regex(@"\r\n[a-f0-9]+\s+[a-f0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match pcstr = pc_key.Match(list_tempfile, line_index);
                if (pcstr.Value == "")
                {
                    break;
                }
                //00023f   30ba
                line_index = pcstr.Index;
                string pc_txt = pcstr.Value;

                string line_num = line_txt.Substring(5, line_txt.LastIndexOf(";") - 5);
                string file_name = line_txt.Substring(line_txt.LastIndexOf(";") + 3, line_txt.Length - line_txt.LastIndexOf(";") - 3);
                string pc_value = pc_txt.Substring(4, 7);
                save_cod_str += "\nL 8           ; " + line_num + "   [1]\t" + pc_value + "\t " + file_name;
            }

            string save_path = project_path + "\\Debug\\" + project_name + ".cod";
            File.WriteAllText(save_path, save_cod_str, Encoding.Default);
        }

        public void crisc_var_create(string project_path, string project_name)
        {
            FastColoredTextBox hc05file = new FastColoredTextBox();
            hc05file.AcceptsTab = true;

            //load s19 file
            string fpath = project_path + "\\Output\\" + project_name + ".lst";
            FormatMapList(project_path + "\\Output\\" + project_name + ".map"); //Q.2014/1/14 先格式化map文件，加载到内存中

            if (File.Exists(fpath) != true)
            {
                return;
            }
            FileStream textLoad = new FileStream(fpath, FileMode.Open, FileAccess.Read);

            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(fpath);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            hc05file.Text = list_tempfile;
            textLoad.Close();

            //search: ;@initial i Allocated to registers r0x1000
            Regex fun_temp = new Regex(@"@\w+\s+\w+\s+Allocated to registers +r0x[A-F0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            string save_m_str = "";
            int line_index = 0;
            while (true)
            {
                Match m_file_str = fun_temp.Match(list_tempfile, line_index);
                if (m_file_str.Value == "")
                    break;
                line_index = m_file_str.Index + m_file_str.Value.Length;
                //save_m_str += m_file_str.Value + "\n";
                string line_str = m_file_str.Value;
                Regex regex_name = new Regex(@"@\w+\s+\w+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match temp_name_str = regex_name.Match(line_str, 0);
                save_m_str += temp_name_str.Value;

                Regex regex_addr = new Regex(@"r0x[A-F0-9]+\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match temp_addr_str = regex_addr.Match(line_str, 0);

                string[] tempstr = temp_name_str.Value.Split(' ');
                if (tempstr.Length >= 2)
                {
                    string funname = tempstr[0].Substring(1, tempstr[0].Length - 1);
                    string varoldname = tempstr[1];
                    string varname = temp_addr_str.Value;
                    TMaps.addMap(funname, varoldname, varname);
                }
                save_m_str += " " + temp_addr_str.Value + "\n";
            }

            //search funtion start line
            Regex regex_fun_str = new Regex(@"@end Allocation info for local variables in function '+\w+\'", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            line_index = 0;

            while (true)
            {
                Match temp_fun_str = regex_fun_str.Match(list_tempfile, line_index);
                if (temp_fun_str.Value == "")
                    break;
                line_index = temp_fun_str.Index + temp_fun_str.Length;

                string temp_fun_str_00 = temp_fun_str.Value;
                temp_fun_str_00 = temp_fun_str_00.Substring(temp_fun_str_00.IndexOf("'") + 1, temp_fun_str_00.Length - temp_fun_str_00.IndexOf("'") - 2);

                //Q 获取函数名称，给得到结束行号调用
                string funName = temp_fun_str_00;
                save_m_str += temp_fun_str_00 + " ";

                temp_fun_str_00 = "_" + temp_fun_str_00 + @"\s+;Function+\s+start";

                //match : .line  xx; "xxx.c"
                Regex regex_fun_line_str = new Regex(temp_fun_str_00, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match temp_fun_line_str = regex_fun_line_str.Match(list_tempfile, line_index);
                if (temp_fun_line_str.Value == "")
                {
                    MessageBox.Show("reacte var file error!");
                    return;
                }

                int fun_line_index = temp_fun_line_str.Index;

                Regex line_key = new Regex(@"line\s+[0-9]+;\s+[\""]+[\w]+[\.][c|h|H|C]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match filestr = line_key.Match(list_tempfile, fun_line_index);
                if (filestr.Value == "")
                {
                    break;
                }
                //line	76; "YKQ3011.c 
                string line_txt = filestr.Value;
                //^[a-f0-9]+\s+[a-f0-9]{4}\b
                //Regex pc_key = new Regex(@"[a-f0-9]+\s+[a-f0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                //Regex pc_key = new Regex(@"\r\n[a-f0-9]+\s+[a-f0-9]{4}", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                //Match pcstr = pc_key.Match(list_tempfile, line_index);
                //if (pcstr.Value == "")
                //{
                //    break;
                //}
                ////00023f   30ba
                //line_index = pcstr.Index;
                //string pc_txt = pcstr.Value;
                string line_num = line_txt.Substring(5, line_txt.LastIndexOf(";") - 5);
                string file_name = line_txt.Substring(line_txt.LastIndexOf(";") + 3, line_txt.Length - line_txt.LastIndexOf(";") - 3);
                //string pc_value = pc_txt.Substring(4, 7);

                int lineEndNo = GetFunEndLineNo(list_tempfile, funName);
                int linestartNo = Convert.ToInt32(line_num.Trim());
                TMaps.setLineParam(funName, file_name, linestartNo, lineEndNo);
                string oldname = file_name;
                save_m_str += file_name + " " + line_num + "\t" + "\n";

            }


            TMaps.updateAddress();

            //add by wj for data length
            int indexOld = 0;
            int index = 0;
            string varName = null;
            while (true)
            {
                if (TMaps.GetVarName(indexOld, out varName, out index) == false)
                    break;
                Regex fun_temp1 = new Regex(varName + @"\s+res+\s+[0-9]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                string save_m_str1 = "";
                Match m_file_str = fun_temp1.Match(list_tempfile, 0);
                if (m_file_str.Value == "")
                {
                    indexOld = index + 1;
                    continue;
                }
                Regex line_key = new Regex(varName + @"\s+res", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match filestr = line_key.Match(m_file_str.Value, 0);
                if (filestr.Value == "")
                    continue;
                int iData = Convert.ToInt32(m_file_str.Value.Substring(filestr.Value.Length, m_file_str.Value.Length - filestr.Value.Length).Trim());
                TMaps.setDataRes(index,iData);
                indexOld = index+1;
            }

            string save_path = project_path + "\\System\\" + project_name + ".var";
            TMaps.SaveToVarFile(save_path);
            //File.WriteAllText(save_path, save_m_str, Encoding.Default);
        }




        /// <summary>
        /// 得到函数结束行号
        /// </summary>
        /// <param name="contentex"></param>
        /// <param name="funname"></param>
        /// <returns></returns>
        private int GetFunEndLineNo(string contentex, string funname)
        {
            int lastline = 0;
            int returnline = -1;
            lastline = contentex.LastIndexOf("; exit point of _" + funname);
            if (lastline > 0)
            {
                string Temptext = contentex.Substring(0, lastline);
                lastline = Temptext.LastIndexOf(".line");
                Temptext = Temptext.Substring(lastline, Temptext.Length - lastline);

                Regex line_key = new Regex(@"line\s+[0-9]+;\s+[\""]+[\w]+[\.][c|h|H|C]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match filestr = line_key.Match(Temptext, 0);
                string line_txt = filestr.Value;
                try
                {
                    returnline = Convert.ToInt32(line_txt.Substring(5, line_txt.LastIndexOf(";") - 5).Trim());
                }
                catch { }
            }
            return returnline;
        }


        /// <summary>
        /// 将map文件格式化到内存
        /// </summary>
        private void FormatMapList(string filename)
        {
            TMaps.LoadMap(filename);
        }
    }

}