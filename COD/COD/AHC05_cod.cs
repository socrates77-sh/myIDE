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


namespace COD
{

    public class AHC05_cod
    {
        public void ahc05_codfile_create(List<string> listfile ,string path)
        {

            //get asmfileName
            string list_key=@"[0-9]+\s+\[[^\]]*\]\s+[A-Fa-f0-9]{4}";

            Regex asmf = new Regex(list_key, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
           // Match asmfstr = asmf.Match(temp, 0);
           // string currentAsmfile = asmfstr.Value;

            FastColoredTextBox codfile = new FastColoredTextBox();
            codfile.AcceptsTab = true;

            for (int i = 0; i < listfile.Count; i++)
            {
                int file_index=0;
                FileStream textLoad = new FileStream(listfile[i], FileMode.Open, FileAccess.Read);
                string asmfileNmae=Path.GetFileNameWithoutExtension(listfile[i])+".asm";
                if (textLoad.CanRead)
                {
                    Encoding encoding = Encoding.Unicode;
                    encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(listfile[i]);
                     string list_tempfile= FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                    textLoad.Close();
                    //tb.Text = File.ReadAllText(fileName);

                     while(true)
                    {
                        Match listfstr = asmf.Match(list_tempfile, file_index);
                        if (listfstr.Value=="")
                        {
                            break;
                        } 
                        else
                        {
                            codfile.AppendText(listfstr.Value.ToString() + "   " + asmfileNmae+"\n");
                            file_index =listfstr.Index+10;
                        }
                    }
                }
               
               
            }
            //save file
            string save_path = path.Substring(0,path.LastIndexOf("."))+".cod";
            File.WriteAllText(save_path,codfile.Text, Encoding.Default);

         }

    }
}