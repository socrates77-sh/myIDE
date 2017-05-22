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
using WeifenLuo.WinFormsUI.Docking;
using System.Text.RegularExpressions;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmWatch : DockContent
    {
        private byte[] MCU_REGbyte = { 0x69, 0x11, 4, 0x80, 0x00, 0xe0, 0xbc };
        private string proj_path = null;

        public FrmWatch()
        {
            InitializeComponent();
        }

        private void FrmWatch_Load(object sender, EventArgs e)
        {
            InitialWatchWindow();
        }

        private void InitialWatchWindow()
        {
            //get project path
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

            proj_path = path;

            rootNode = XElement.Load(path); //load project file
            myvalue = from mySymbol in rootNode.Descendants("WatchSymbol")
                      select mySymbol;

            DataGridView_Watch.Rows.Clear();
            //DataGridView_Watch.Columns[2].DefaultCellStyle.Format = "X2";
            int index = 0;
            foreach (XElement xnode1 in myvalue)
            {
                DataGridView_Watch.Rows.Add();
                DataGridView_Watch.Rows[index].Cells[0].Value = xnode1.Attribute("Name").Value;
                DataGridView_Watch.Rows[index].Cells[1].Value = xnode1.Attribute("Addr").Value;
                DataGridView_Watch.Rows[index].Cells[2].Value = "??";//xnode1.Attribute("Value").Value;
                DataGridView_Watch.Rows[index].Cells[5].Value = "??";
                if (xnode1.Attribute("BitFlag") == null)
                    DataGridView_Watch.Rows[index].Cells[3].Value = "false";
                else
                    DataGridView_Watch.Rows[index].Cells[3].Value = xnode1.Attribute("BitFlag").Value;
                if (xnode1.Attribute("Bit") == null)
                    DataGridView_Watch.Rows[index].Cells[4].Value = "0";
                else
                    DataGridView_Watch.Rows[index].Cells[4].Value = xnode1.Attribute("Bit").Value;
                if (xnode1.Attribute("DataRes") == null)
                    DataGridView_Watch.Rows[index].Cells[6].Value = "0";
                else
                    DataGridView_Watch.Rows[index].Cells[6].Value = xnode1.Attribute("DataRes").Value;
                if (xnode1.Attribute("Array") == null)
                    DataGridView_Watch.Rows[index].Cells[7].Value = "false";
                else
                    DataGridView_Watch.Rows[index].Cells[7].Value = xnode1.Attribute("Array").Value;
                index = index + 1;
            }
        }

        private void myDataGridViewWatch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            int c = this.DataGridView_Watch.CurrentCellAddress.X;
            int r = this.DataGridView_Watch.CurrentCellAddress.Y;
            //this.DataGridView_Watch[c, r].Style = changeByteStyle;

            if (c == 0 && DataGridView_Watch.CurrentCell.Value == null)
            {
                if (r == 0)
                    return;
                try
                {
                    DataGridView_Watch.Rows.Remove(DataGridView_Watch.CurrentRow);
                    SaveSymbol2Proj(proj_path);
                }
                catch
                {

                }

                return;
            }
            string str = null;
            if (DataGridView_Watch.CurrentCell.Value != null)
            {
                str = DataGridView_Watch.CurrentCell.Value.ToString();
            }


            if (c == 0)
            {
                Watch_add_symbol(str, r);
            }
            else
            {
                //int Boffset = 5 + r * 2;
                byte a = 0;
                //// private byte[] MCU_REGbyte = { 0x69, 0x11, 4, 0x80, 0x00, 0xe0, 0xbc };
                MCU_REGbyte[3] = Convert.ToByte(frmMAIN.DeviceConfigXX.aDEV_RAMAddrFirst / 0x100);
                try
                {


                    if (str == "255")
                    {
                        a = 0xff;
                    }
                    else
                    {
                        a = System.Convert.ToByte(str, 16);
                    }

                    if (a >= 0 && a <= 0xff)
                    {
                        DataGridView_Watch.CurrentCell.Value = a;
                        UInt16 addr_reg = Convert.ToUInt16(this.DataGridView_Watch[c - 1, r].Value.ToString(), 16);

                        MCU_REGbyte[3] = Convert.ToByte(MCU_REGbyte[3] + (addr_reg >> 16));
                        MCU_REGbyte[4] = Convert.ToByte(addr_reg);
                        MCU_REGbyte[5] = a;
                        USB_SendByte();
                    }
                    else
                    {
                        MessageBox.Show(ShowLanguage.Current.MessageBoxText28 + "0xff！");
                        DataGridView_Watch.CurrentCell.Value = 0xff;
                    }
                }
                catch
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText25);
                    DataGridView_Watch.CurrentCell.Value = 0xff;
                }
            }

        }

        public void Watch_add_symbol(string str_symbol, int r)
        {
            //string path=frmMAIN.GetProjectPath();
            //get project path
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
            bool bflag = false;
            string strbit = "0";
            int iArray = 0;
            bool bArray = false;
            string str_symbol_temp = str_symbol;
            int iDataRes = 0;

            if (File.Exists(var_path))
            {
                FileStream textLoad = new FileStream(var_path, FileMode.Open, FileAccess.Read);

                Encoding encoding = Encoding.Unicode;
                encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(var_path);
                string var_file = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
                textLoad.Close();

                //int sub_file_index = 0;

                //find keyword from gValiableFile
                //[\w|(\[[^\]\])]+\s+at+\s+0x[a-fA-F0-9]+\b
                //globle variable: R_KEY1+\s+at+\s+0x[a-fA-F0-9]+\s+xxxx.x
                Regex var_key = new Regex(@"[" + "[0-9]+" + "]", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Match var_str = var_key.Match(str_symbol, 0);
                if (var_str.Value != "")
                {
                    bArray = true;
                    iArray = Convert.ToInt32(var_str.Value.Substring(1, var_str.Value.Length - 2));
                    str_symbol = str_symbol.Substring(0, str_symbol.Length - var_str.Value.Length);
                }

                var_key = new Regex(str_symbol + @"\s+at+\s+0x[a-fA-F0-9]+\s+[0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                var_str = var_key.Match(var_file, 0);

                if (var_str.Value != "")
                {
                    iDataRes = Convert.ToInt32(var_str.Value.Substring(var_str.Value.Length - 5, 5).Trim());
                }

                var_key = new Regex(str_symbol + @"\s+at+\s+0x[a-fA-F0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                var_str = var_key.Match(var_file, 0);

                if (var_str.Value != "")
                {
                    string temp_str = var_str.Value;
                    var_key = new Regex(@"0x[a-fA-F0-9]+\s", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    var_str = var_key.Match(temp_str, 0);
                    gaddr = var_str.Value.Substring(2, 4);
                    //int gaddr = Convert.ToInt32(gaddr_str, 16);

                    //if (flag_bit == 0xff)
                    //{
                    //    buffer_value = RAMDataBuffer[gaddr].ToString("X2");
                    //}
                    //else
                    //{
                    //    buffer_value = ((RAMDataBuffer[gaddr] >> flag_bit) & 0x01).ToString("X2");
                    //}

                }
                else
                {
                    var_key = new Regex(@"#define+\s+" + str_symbol + @"\s+[^\n\r]+", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    var_str = var_key.Match(var_file, 0);
                    if (var_str.Value != "")
                    {
                        string temp_str = var_str.Value;
                        int iPos = temp_str.IndexOf(str_symbol);
                        temp_str = temp_str.Substring(iPos + str_symbol.Length);
                        temp_str = temp_str.Trim();

                        var_key = new Regex(@"[\w]+,+[0-9]\b", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        var_str = var_key.Match(temp_str, 0);
                        if (var_str.Value != "")
                        {
                            temp_str = var_str.Value;
                            string var_name = temp_str.Substring(0, temp_str.Length - 2);
                            string var_value = temp_str.Substring(temp_str.Length - 1, 1);
                            gaddr = GetAddr(var_name);
                            if (gaddr != null)
                            {
                                bflag = true;
                                strbit = var_value;
                            }
                        }
                        else
                        {
                            gaddr = GetAddr(temp_str);
                        }
                    }
                    else
                        gaddr = null;
                }

                //--------------------------------------------------
                if (gaddr == null)
                {
                    DataGridView_Watch.Rows[r].Cells[0].Value = str_symbol_temp;
                    DataGridView_Watch.Rows[r].Cells[1].Value = "??";
                    DataGridView_Watch.Rows[r].Cells[2].Value = "Error:not found";
                    DataGridView_Watch.Rows[r].Cells[3].Value = bflag.ToString();
                    DataGridView_Watch.Rows[r].Cells[4].Value = strbit;
                    DataGridView_Watch.Rows[r].Cells[5].Value = "Error:not found";
                    DataGridView_Watch.Rows[r].Cells[6].Value = iDataRes;
                    DataGridView_Watch.Rows[r].Cells[7].Value = bArray.ToString();
                }
                else
                {
                    string adr = gaddr;
                    if (bArray)
                    {
                        adr = (Convert.ToInt32(adr, 16) + iArray).ToString("X4");
                    }

                    // string adr = Watch_add_symbol(str);
                    DataGridView_Watch.Rows[r].Cells[0].Value = str_symbol_temp;

                    DataGridView_Watch.Rows[r].Cells[1].Value = gaddr;
                    DataGridView_Watch.Rows[r].Cells[2].Value = "??";
                    DataGridView_Watch.Rows[r].Cells[3].Value = bflag.ToString();
                    DataGridView_Watch.Rows[r].Cells[4].Value = strbit;
                    DataGridView_Watch.Rows[r].Cells[5].Value = "??";
                    DataGridView_Watch.Rows[r].Cells[6].Value = iDataRes;
                    DataGridView_Watch.Rows[r].Cells[7].Value = bArray.ToString();
                    //save watch symbol

                }
                SaveSymbol2Proj(proj_path);
            }
            else
            {
                return;
            }
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

        private void DeletekeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && DataGridView_Watch.SelectedCells.Count != 0)
            {
                try
                {
                    DataGridView_Watch.Rows.Remove(DataGridView_Watch.CurrentRow);
                    SaveSymbol2Proj(proj_path);
                }
                catch
                {

                }

            }
        }

        #region save symbol of proj
        private void SaveSymbol2Proj(string pro)
        {
            //save watch symbol
            XElement rootNode = XElement.Load(pro);

            rootNode.Elements("WatchSymbol").Remove();

            for (int index = 0; index < DataGridView_Watch.Rows.Count - 1; index++)
            {
                rootNode.Add(new XElement("WatchSymbol",
                  new XAttribute("Name", DataGridView_Watch.Rows[index].Cells[0].Value),
                  new XAttribute("Addr", DataGridView_Watch.Rows[index].Cells[1].Value),
                  new XAttribute("Value", DataGridView_Watch.Rows[index].Cells[2].Value),
                  new XAttribute("BitFlag", DataGridView_Watch.Rows[index].Cells[3].Value),
                  new XAttribute("Bit", DataGridView_Watch.Rows[index].Cells[4].Value),
                  new XAttribute("DataRes", DataGridView_Watch.Rows[index].Cells[6].Value),
                  new XAttribute("Array", DataGridView_Watch.Rows[index].Cells[7].Value)
                  )
                  );
            }
            rootNode.Save(pro);

        }
        #endregion

        private void USB_SendByte()
        {
            //frmMAIN.COMPORT.OpenPort();
            if (frmMAIN.DebugFlag == false)
            {
                return;
            }
            if (frmMAIN.COMPORT.port1.IsOpen)
            {
                frmMAIN.COMPORT.SendCommand(MCU_REGbyte);//set break point
            }

            //while (frmMAIN.COMPORT.port1.BytesToRead < 5) ;
            //int len = frmMAIN.COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
            //frmMAIN.COMPORT.ClosePort();
        }

        public void ApplyLanguage()
        {

        }
    }
}
