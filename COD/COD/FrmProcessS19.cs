using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace COD
{
    public partial class FrmProcessS19 : Form
    {
        public FrmProcessS19()
        {
            InitializeComponent();
        }
        private void Selectbt_Click(object sender, EventArgs e)
        {
            //this.S19openFileDialog.ShowDialog();
            if (this.S19openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
             this.S19openFileDialog.FileName.Length > 0)
            {
                //path = this.S19openFileDialog.FileName;
                textBox1.Text = this.S19openFileDialog.FileName;
            }

        }

        private void cancelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKbt_Click(object sender, EventArgs e)
        {
            string temps19;
            string temp = textBox1.Text; //Directory.GetDirectories(textBox1.Text);
            string temp1 = Path.GetDirectoryName(temp);
            string[] list = Directory.GetFileSystemEntries(temp1, "*.S19");

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != temp)
                {
                    temps19 = S19Read(temp, list[i]);
                    StreamWriter S19result = new StreamWriter(temp, true, Encoding.Default);
                    S19result.Write(temps19);
                    S19result.Close();
                }
            }
            MessageBox.Show("合并成功！");
            this.Close();

        }

        private void ProcessS19_Load(object sender, EventArgs e)
        {

        }
        public string S19Read(string dir, string dir2)
        {
            StreamReader S19ader = new StreamReader(dir2);
            string allstring = S19ader.ReadToEnd();
            int len = allstring.Length;
            allstring = allstring.Substring(0, len - 12);
            StreamReader S19main = new StreamReader(dir);//main S19
            string mains19 = S19main.ReadToEnd();
            allstring = allstring + mains19;
            S19main.Close();
            S19ader.Close();
            FileStream fs = new FileStream(dir, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            fs.SetLength(0);
            fs.Close();
            return allstring;

        }

    }
}
