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
    public partial class FrmBreakPoint : DockContent
    {
        public FrmBreakPoint()
        {
            InitializeComponent();
            try
            {
                int ID_PointValue;
                if (frmMAIN.MCU_ID == "0x7333" || frmMAIN.MCU_ID == "0x9903" || frmMAIN.MCU_ID == "0x9904" || frmMAIN.MCU_ID == "0x7122")
                {
                    ID_PointValue = 4;
                }
                else
                {
                    ID_PointValue = 16;
                }

                myDataGridViewBK.Rows.Add(16);
                myDataGridViewBK.Columns[0].ValueType = typeof(string);
                // myDataGridViewBK.Columns[1].ValueType = typeof(byte);

                myDataGridViewBK.DataError += new DataGridViewDataErrorEventHandler(myDataGridViewBK_DataError);

                myDataGridViewBK.Columns[1].DefaultCellStyle.Format = "x4";

                for (int i = 0; i < 16; i++)
                {
                    myDataGridViewBK.Rows[i].Cells[0].Value = i;
                    myDataGridViewBK.Rows[i].Cells[1].Value = 0xffff;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }
        public void Load_Click(object sender,EventArgs e)
        {


        }

        private void myDataGridViewBK_DataError(object sender,DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex==0)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText9);
                return;
            }
            if (e.ColumnIndex==1)
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText10);
                return;
            }
        }

        private void MyDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            //DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            //changeByteStyle.ForeColor=Color.Red;
            //this.myDataGridViewBK[1,1].Style=changeByteStyle;
            if (e.KeyChar=='r')
            {
                MessageBox.Show("skkskss");
            } 
        }

        private void myDataGridViewBK_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle changeByteStyle = new DataGridViewCellStyle();
            changeByteStyle.ForeColor = Color.Red;
            int c = this.myDataGridViewBK.CurrentCellAddress.X;
            int r = this.myDataGridViewBK.CurrentCellAddress.Y;
            this.myDataGridViewBK[c,r].Style = changeByteStyle;

            string str = myDataGridViewBK.CurrentCell.Value.ToString();
            int Boffset = 5+r*2;
            int tempL, tempH;
            try
            {


                int a = System.Convert.ToInt32(str, 16);
                if (a >= 0 && a <=0xffff)
                {                        
                        tempL = a % 0x100;
                        tempH = a / 0x100;
                        frmMAIN.MCU_Breakpoint[Boffset] = Convert.ToByte(tempL);
                        frmMAIN.MCU_Breakpoint[Boffset + 1] = Convert.ToByte(tempH);
                       // Boffset = Boffset + 2;
                    
                    return;
                }
                else
                {
                    MessageBox.Show(ShowLanguage.Current.MessageBoxText11 + "0xffff！");
                    myDataGridViewBK.CurrentCell.Value = 0xffff;
                }
            }
            catch
            {
                MessageBox.Show(ShowLanguage.Current.MessageBoxText11 + "0xffff！");
                myDataGridViewBK.CurrentCell.Value =0xffff;
            }
            
        }

        public void ApplyLanguage()
        {
            this.myDataGridViewBK.Columns[0].HeaderText = ShowLanguage.Current.Name;
            this.myDataGridViewBK.Columns[1].HeaderText = ShowLanguage.Current.RegAddr;
            this.myDataGridViewBK.Columns[2].HeaderText = ShowLanguage.Current.Files;
        }
            

    }
    public sealed class MyDataGridView: DataGridView
    {
        protected override bool ProcessCmdKey(ref Message msg,Keys KeyData)
        {
            //if (KeyData == Keys.Enter)
            //{
            //    this.OnKeyPress(new KeyPressEventArgs('r'));
            //    return true;
            //}
            //else 
               return base.ProcessCmdKey(ref msg, KeyData);
               
        }
    }
}
