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
    public partial class FrmREG : DockContent
    {
        //private byte[] MCU_REGbyte = { 0x69, 0x11, 4, 0x80, 0x00, 0xe0, 0xbc };
        private byte[]  MCU_REGbyte = { 0x69, 0x14, 6, 0x00, 0x00, 0x00, 0x00, 0x00, 0xbc };
        private byte[] ReadDataBuffer01 = new byte[20];

        public FrmREG()
        {
            InitializeComponent();
        }

        private void FrmREG_Load(object sender, EventArgs e)
        {

            string mcuID = "0x0301";

            mcuID = frmMAIN.MCU_ID;

            if(frmMAIN.MCU_ID==null)
            {
                mcuID = "0x0301";
            }
            
            GetXmlNodeInformation(mcuID);

        }

        public void GetXmlNodeInformation(string mcuID)
        {
            dataGridView_reg.Columns[2].DefaultCellStyle.Format = "X2";

            dataGridView_reg.Rows.Clear();
            //XML文件保存路径
            string xmlPath = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
            // string xxmlPath = "G:\\Emulator\\IDE\\SinoSunIDE\\XmlGenerator\\XmlGenerator\\XmlGenerator\\bin\\Debug\\OTP.cfg";
            //生成XML文件
            // xml_great(xxmlPath);
            //string mcuID = "0x0301";

            try
            {
                XElement rootNode = XElement.Load(xmlPath);
                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                                where (string)myTarget.Attribute("id").Value == mcuID
                                                select myTarget;

                IEnumerable<XElement> chipcfg = from srootNode in myvalue.Descendants("reg") select srootNode;
                int temp = 0;
                foreach (XElement xnode in chipcfg)
                {

                    dataGridView_reg.Rows.Add();
                    dataGridView_reg.Rows[temp].Cells[0].Value = xnode.Attribute("name").Value;
                    dataGridView_reg.Rows[temp].Cells[1].Value = xnode.Attribute("addr").Value;
                    temp = temp + 1;
                    //int aa = Convert.ToInt16(romfaddr, 16);


                }
                // MessageBox.Show(romfaddr + "  " + romeaddr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            int leng = dataGridView_reg.Rows.Count;
            //initial reg value
            for (int j = 0; j < leng; j++)
            {
                dataGridView_reg.Rows[j].Cells[2].Value = 0xff;
            }
        }

        private void myDataGridViewRAM_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            int c = this.dataGridView_reg.CurrentCellAddress.X;
            int r = this.dataGridView_reg.CurrentCellAddress.Y;
            this.dataGridView_reg[c, r].Style = changeByteStyle;

            string str = dataGridView_reg.CurrentCell.Value.ToString();

            
            //int Boffset = 5 + r * 2;
            byte a = 0;
            // private byte[] MCU_REGbyte = { 0x69, 0x11, 4, 0x80, 0x00, 0xe0, 0xbc };
            MCU_REGbyte[5] =Convert.ToByte(frmMAIN.DeviceConfigXX.aDEV_RAMAddrFirst/0x100);
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
                    dataGridView_reg.CurrentCell.Value=a;
                    UInt16 addr_reg = Convert.ToUInt16(this.dataGridView_reg[c - 1, r].Value.ToString(), 16);

                    MCU_REGbyte[5] = Convert.ToByte(MCU_REGbyte[5] + (addr_reg >>8));
                    MCU_REGbyte[6] = Convert.ToByte(addr_reg & 0xff);
                    MCU_REGbyte[7] = a;
                    USB_SendByte();

                    dataGridView_reg.CurrentCell.Value = ReadDataBuffer01[1];
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText28 + "0xff！");
                    dataGridView_reg.CurrentCell.Value = 0xff;
                }
            }
            catch
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText25);
                dataGridView_reg.CurrentCell.Value = 0xff;
            }
        }

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
            else
            {
                return;
            }

            //read status
            byte[] CMD_Read2Byte2 = { 0x69, 0x07, 0x04, 0x00, 0x00, 0x91, 0x01 };
            CMD_Read2Byte2[5] = MCU_REGbyte[5];
            CMD_Read2Byte2[6] = MCU_REGbyte[6];

            frmMAIN.COMPORT.SendCommand(CMD_Read2Byte2);
            while (frmMAIN.COMPORT.port1.BytesToRead < 2) ;

           int len = frmMAIN.COMPORT.port1.Read(ReadDataBuffer01, 0, frmMAIN.COMPORT.port1.BytesToRead);
            
        }

        public void ApplyLanguage()
        {
            this.dataGridView_reg.Columns[0].HeaderText = ShowLanguage.Current.Name;
            this.dataGridView_reg.Columns[1].HeaderText = ShowLanguage.Current.RegAddr;
            this.dataGridView_reg.Columns[2].HeaderText = ShowLanguage.Current.RegValue;
        }

    }
}
