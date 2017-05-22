using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Microsoft.Win32;

namespace COD
{

    public class RegeditFuntion
    {
        public void FilesRelatingEXE(string softPath,string fileExtentName)
        {
            try
            {
                string notepath=softPath; //设置关联文件的程序路径
                string extName=fileExtentName;//设置关联文件的扩展名
                string mtype="myDefindType";//定义描述信息
                string mContent="text/asm"; //定义类型
                RegistryKey key=Registry.ClassesRoot.OpenSubKey(extName); //创建RegistryKey对象

                if (key==null || key.ValueCount==0)
                {
                   RegistryKey  mreg=Registry.ClassesRoot.CreateSubKey(extName); //实例化创建关联文件扩展名的子项
                    mreg.SetValue("",mtype);//设置属性值
                    mreg.SetValue("Content Type",mContent); //设置属性值
                    mreg=mreg.CreateSubKey("shell\\open\\command");//打开或创建一个子项用于写访问
                    mreg.SetValue("",notepath+" %1");//设置打开程序的路径
                    mreg.Close();
                }
                else if(!key.GetValue("").ToString().Equals(mtype))
                {
                    key.DeleteSubKey(extName);
                    RegistryKey mreg = Registry.ClassesRoot.CreateSubKey(extName); //实例化创建关联文件扩展名的子项
                    mreg.SetValue("", mtype);//设置属性值
                    mreg.SetValue("Content Type", mContent); //设置属性值
                    mreg = mreg.CreateSubKey("shell\\open\\command");//打开或创建一个子项用于写访问
                    mreg.SetValue("", notepath + " %1");//设置打开程序的路径
                    mreg.Close();
                }

                //if (MessageBox.Show("设置完毕")==DialogRusult.Ok)
                //{
                //    //RefreshSystem();
                //}
            }
            catch (System.Exception ex)
            {
            	MessageBox.Show(ex.Message);
            }
        }


    }
}