using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Xml;
using System.Text;
using FarsiLibrary.Win;
using WeifenLuo.WinFormsUI.Docking;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace SinoSunIDE
{
    /**/
    /// <summary>
    /// bsGetFiles 的摘要描述。
    /// </summary>
    public class bsGetFiles
    {
        public bsGetFiles()
        {
        }
        
        /**/
        /// <summary>
        /// 得某文件夹下所有的文件
        /// </summary>
        /// <param name="directory">文件夹名称</param>
        /// <param name="pattern">搜寻指类型</param>
        /// <returns></returns>
        public static string GetFiles(DirectoryInfo directory, string pattern)
        {
            int filesCounter = 0;
            string result = null;
            if (directory.Exists || pattern.Trim() != string.Empty)
            {

                foreach (FileInfo info in directory.GetFiles(pattern))
                {
                    result =info.FullName.ToString();
                    //result = result + info.Name.ToString() + ";";
                    filesCounter = filesCounter + 1;
                }

                //foreach (DirectoryInfo info in directory.GetDirectories())
                //{
                //    GetFiles(info, pattern);
                //}
            }
            if (filesCounter==0)
            {
                MessageBox.Show("S19 file can not fined!", "Load S19 file Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return null;
            }
            if (filesCounter==1)
            {
                string returnString = result;
                return returnString;
            } 
            else 
            {
                MessageBox.Show("There are multi-S19 files in the Directory!", "Load S19 file Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                string returnString = result;
                return returnString;
            }


        }

    }

    public class Mathfuntion
     {

        public static string GetProjectPath()
        {
            string str="";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(frmMAIN.APPLICATION_PATH + "global.ini");
            XmlNodeList xmlNodes = xmlDoc.SelectSingleNode("/GLOBAL").ChildNodes;///SouceCode

            foreach (XmlNode xn1 in xmlNodes)
            {
                XmlElement xe2 = (XmlElement)xn1;
                if (xe2.Name == "ProjectPath")
                {
                    //xe2.Attributes["Ppath"].Value = str + ".Proj";
                    str = xe2.Attributes["Ppath"].Value;
                    break;//找到退出来就可以了   
                }
            }
            xmlDoc.Save(frmMAIN.APPLICATION_PATH + "global.ini");
            return str;
        }



#region 各进制之间的转换
        public static string TenConvertToOther(int num,int type)
        {
            //10进制转换为2，8，10，16
            //numm是十进制数
            //type是转换类型：2，8，10，16
            string str = Convert.ToString(num, type);
            return str;
        }

        public static string OtherConvertToTen( string num, int type)
        {
            //10进制转换为2，8，10，16
            //numm是十进制数
            //type是转换类型：2，8，10，16
            string str = Convert.ToInt32(num, type).ToString();
            return str;
        }
#endregion

     }

    public class DEBUG_COMMAND
    {
        public const byte CMD_Run = 0xe0;
        public const byte CMD_StepInto = 0xe1;
        public const byte CMD_Stop = 0xe2;
        public const byte CMD_Reset = 0xe3;
        public const byte CMD_StepOver = 0xe4;
        public const byte CMD_StepOut = 0xe5;
        public const byte CMD_GoNBK = 0xe6;

    }


    public class DeviceConfig
{
    public UInt16 aMCU_RAMAddrFirst, aMCU_RAMAddrEnd, aMCU_ROMAddrFirst, aMCU_ROMAddrEnd = 0;
    public UInt16 aMCU_RAMSize, aMCU_ROMSize = 0;
    public uint aDEV_RAMAddrFirst, aDEV_RAMAddrEnd, aDEV_ROMAddrFirst, aDEV_ROMAddrEnd = 0;
    public uint aDEV_PCAddrFirst, aDEV_PCAddrEnd, aDEV_BKAddrFirst, aDEV_BKAddrEnd = 0;
    public UInt32 aDEV_OPTIONAddrFirst, aDEV_OPTIONAddrEnd = 0;
    public UInt32 aDEV_TRACEAddrFirst, aDEV_TRACEAddrEnd = 0;


    ///<summary>
    ///MCU RAM Size
    ///</summary>
    public ushort MCU_RAMSize
    {
        get { return aMCU_RAMSize; }
        set { aMCU_RAMSize = value; }
    }

    ///<summary>
    ///MCU ROM Size
    ///</summary>
    public ushort MCU_ROMSize
    {
        get { return aMCU_ROMSize; }
        set { aMCU_ROMSize = value; }
    }

    ///<summary>
    ///MCU RAM地址
    ///</summary>
    public ushort MCU_RAMAddrFirst
    {
        get { return aMCU_RAMAddrFirst; }
        set { aMCU_RAMAddrFirst = value; }
    }
    ///<summary>
    ///MCU RAM end addr
    ///</summary>
    public ushort MCU_RAMAddrEnd
    {
        get { return aMCU_RAMAddrEnd; }
        set { aMCU_RAMAddrEnd = value; }
    }

    ///<summary>
    ///MCU ROM addrfirst
    ///</summary>
    public ushort MCU_ROMAddrFirst
    {
        get { return aMCU_ROMAddrFirst; }
        set { aMCU_ROMAddrFirst = value; }
    }
    ///<summary>
    ///MCU ROM END addr
    ///</summary>
    public ushort MCU_ROMAddrEnd
    {
        get { return aMCU_ROMAddrEnd; }
        set { aMCU_ROMAddrEnd = value; }
    }

    ///<summary>
    ///DEV ram addr
    ///</summary>
    public uint DEV_RAMAddrFirst
    {
        get { return aDEV_RAMAddrFirst; }
        set{aDEV_RAMAddrFirst=value;}
    }
    ///<summary>
    ///DEV RAM End
    ///</summary>
    public uint DEV_RAMAddrEnd
    {
        get { return aDEV_RAMAddrEnd; }
        set { aDEV_RAMAddrEnd = value; }
    }
    ///<summary>
    ///DEV RoM first addr
    ///</summary>
    public uint DEV_ROMAddrFirst
    {
        get { return aDEV_ROMAddrFirst; }
        set{aDEV_ROMAddrFirst=value;}
    }
    ///<summary>
    ///DEV RoM End
    ///</summary>
    public uint DEV_ROMAddrEnd
    {
        get { return aDEV_ROMAddrEnd; }
        set { aDEV_ROMAddrEnd = value; }
    }
    ///<summary>
    ///DEV PC
    ///</summary>
    public uint DEV_PCAddrFirst
    {
        get { return aDEV_PCAddrFirst; }
        set { aDEV_PCAddrFirst = value; }

    }
    ///<summary>
    ///DEV pc end addr
    ///</summary>
    public uint DEV_PCAddrEnd
    {
        get { return aDEV_PCAddrEnd; }
        set { aDEV_PCAddrEnd = value; }
    }

    ///<summary>
    ///DEV  KB first addr
    ///</summary>
    public uint DEV_BKAddrFirst
    {
        get { return aDEV_BKAddrFirst; }
        set { aDEV_BKAddrFirst = value; }
    }
    ///<summary>
    ///DEV KB end addr
    ///</summary>
    public uint DEV_BKAddrEnd
    {
        get { return aDEV_BKAddrEnd; }
        set { aDEV_BKAddrEnd = value; }
    }

    ///<summary>
    ///DEV option first addr
    ///</summary>
    public uint DEV_OPTIONAddrFirst
    {
        get { return aDEV_OPTIONAddrFirst; }
        set { aDEV_OPTIONAddrFirst = value; }
    }
    ///<summary>
    ///DEV option end addr
    ///</summary>
    public uint DEV_OPTIONAddrEnd
    {
        get { return aDEV_OPTIONAddrEnd; }
        set { aDEV_OPTIONAddrEnd = value; }
    }
    ///<summary>
    ///DEV trace first addr
    ///</summary>
    public uint DEV_TRACEAddrFirst
    {
        get { return aDEV_TRACEAddrFirst; }
        set { aDEV_TRACEAddrFirst = value; }
    }
    ///<summary>
    ///DEV trace end addr
    ///</summary>
    public uint DEV_TRACEAddrEnd
    {
        get { return aDEV_TRACEAddrEnd; }
        set {aDEV_TRACEAddrEnd=value;}
    }


}


    public class OperateIniFile
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);


        #endregion

        #region 读Ini文件

        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region 写Ini文件

        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

   
}