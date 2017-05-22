using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    public partial class FrmRAM : DockContent
    {
        private byte[] MCU_REGbyte={ 0x69, 0x14, 6, 0x00,0x00, 0x00, 0x00,0x00,0xbc };

        public FrmRAM()
        {
            InitializeComponent();
        }

        private void FrmRAM_Load(object sender, EventArgs e)
        {
            RamInitial(frmMAIN.DeviceConfigXX.MCU_RAMSize/*0x100*/);
        }

        public void RamInitial(int RAMSize)
        {
            if (RAMSize == 0)
                RAMSize = 0x100;

            int count = RAMSize / 16;
            myDataGridViewRAM.Rows.Clear();
            myDataGridViewRAM.Rows.Add(count);
            myDataGridViewRAM.DefaultCellStyle.Format = "X2";
            myDataGridViewRAM.Columns[0].DefaultCellStyle.Format = "X4";


            for (int i = 0; i < count; i++)
            {
                for (int j = 1; j < 17; j++)
                {
                    myDataGridViewRAM.Rows[i].Cells[j].Value = 0xff;
                }
                myDataGridViewRAM.Rows[i].Cells[0].Value = i * 16;
                myDataGridViewRAM.Rows[i].Cells[0].ReadOnly = true;
            }
        }

        private void myDataGridViewRAM_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            int c = this.myDataGridViewRAM.CurrentCellAddress.X;
            int r = this.myDataGridViewRAM.CurrentCellAddress.Y;
            this.myDataGridViewRAM[c, r].Style = changeByteStyle;

            string str = myDataGridViewRAM.CurrentCell.Value.ToString();
            //int Boffset = 5 + r * 2;
            //int tempL, tempH;
            byte a = 0;
            try
            {

                if(str=="255")
                {
                   a = 0xff;
                }
                else
                {
                    a = System.Convert.ToByte(str,16);
                }

                // MCU_REGbyte={ 0x69, 0x14, 5, 0x00,0x00, 0x00, 0x00,0x00,0xbc };
                MCU_REGbyte[5] = Convert.ToByte(frmMAIN.DeviceConfigXX.aDEV_RAMAddrFirst / 0x100);

                if (a >= 0 && a <= 0xff)
                {
                    myDataGridViewRAM.CurrentCell.Value=a;
                    UInt16 addr_reg =Convert.ToUInt16(r * 16 + c - 1);

                    MCU_REGbyte[5] = Convert.ToByte(MCU_REGbyte[5] + (addr_reg >>8));

                    MCU_REGbyte[6] =Convert.ToByte(addr_reg & 0xff);
                    MCU_REGbyte[7]=a;
                    USB_SendByte();
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText11 + "0xff！");
                    myDataGridViewRAM.CurrentCell.Value =0xff;
                }
            }
            catch
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText25, ShowLanguage.Current.MessageBoxCaption25, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                myDataGridViewRAM.CurrentCell.Value =0xff;
            }
        }

        /// <summary>
        ///  find data addr
        /// </summary>
        private void DataFind_KeyDown_Clink(object sender, KeyEventArgs e)
        {

            if(e.KeyCode==Keys.Enter)
            {
                string str = RAM_addr.Text;
                UInt16 data_addr = 0;
                try
                {
                     data_addr = Convert.ToUInt16(str, 16);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText26 + "\n" + ex.ToString(), ShowLanguage.Current.MessageBoxCaption26);
                    return;
                }
                
                int count=myDataGridViewRAM.RowCount*16-1;
                if (count<data_addr)
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText27);
                    return;
                }

                myDataGridViewRAM.ClearSelection();
                int row =data_addr / 16;
                int cell =data_addr % 16;
                myDataGridViewRAM.Rows[row].Cells[cell+1].Selected = true;

                if (row>3)
                {
                    myDataGridViewRAM.FirstDisplayedScrollingRowIndex = row-2;
                }
                else
                {
                    myDataGridViewRAM.FirstDisplayedScrollingRowIndex = 0;
                }
            }
        }

        private void USB_SendByte()
        {
            //frmMAIN.COMPORT.OpenPort();
            if (frmMAIN.DebugFlag==false)
            {
                return;
            }
            if (frmMAIN.COMPORT.port1.IsOpen)
            {
                frmMAIN.COMPORT.SendCommand(MCU_REGbyte);//set break point
            }
            
            //while (frmMAIN.COMPORT.port1.BytesToRead < 5) ;
            //int len = frmMAIN.COMPORT.port1.Read(ReadDataBuffer, 0, COMPORT.port1.BytesToRead);
           // frmMAIN.COMPORT.ClosePort();
        }

        public void ApplyLanguage()
        {
            this.toolStripLabel1.Text = ShowLanguage.Current.RegAddr + "：0x";
        }
        
    }
}
