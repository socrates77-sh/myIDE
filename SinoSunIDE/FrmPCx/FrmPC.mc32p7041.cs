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
using PropertyGridEx;
using System.IO;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    partial class FrmPC : DockContent
    {

        private void MC32P7041_OptionFill()
        {
            string str_FOSC = "外部晶体振荡器32768";
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] MODE = new string[] { "OTP", "MTP" };
            /*string[] LVRS = new string[] { CurLang.LVRSMC30P6060String0000, CurLang.LVRSMC30P6060String0001, CurLang.LVRSMC30P6060String0010, CurLang.LVRSMC30P6060String0011,
                                           CurLang.LVRSMC30P6060String0100, CurLang.LVRSMC30P6060String0101, CurLang.LVRSMC30P6060String0110, CurLang.LVRSMC30P6060String0111,
                                           CurLang.LVRSMC30P6060String1000, CurLang.LVRSMC30P6060String1001, CurLang.LVRSMC30P6060String1010, CurLang.LVRSMC30P6060String1011,
                                           CurLang.LVRSMC30P6060String1100, CurLang.LVRSMC30P6060String1101, CurLang.LVRSMC30P6060String1110, CurLang.LVRSMC30P6060String1111 };*/
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] MCLRE = new string[] { CurLang.MCLREMC30P6060String0, CurLang.MCLREMC30P6060String1 };
            string[] FCPU = new string[] { "Fosc//2", "Fosc//4", "Fosc//8", "Fosc//16", "Fosc//32", "Fosc//64", "Fosc//128", "Fosc//256" };
            string[] FOSC = new string[] { "内部RC振荡器", "外部晶体振荡器32768", "外部晶体振荡器455K", "外部晶体振荡器1M", "外部晶体振荡器2M", 
                                            "外部晶体振荡器4M", "外部晶体振荡器8M", "外部晶体振荡器16M", "外部时钟输入<1M", "外部时钟输入<2M", 
                                            "外部时钟输入<4M", "外部RC振荡器<1M", "外部RC振荡器<2M", "外部RC振荡器<4M", "外部RC振荡器<8M" }; 
            //string[] HMLSEL = new string[] { "4M-8M", "455K", "32K", "12M~20M" };

            //string[] FMODE = new string[] { "兼容模式", "扩展模式" };
            string[] DRVS = new string[] { "0", "1" };
            //string[] SPDS = new string[] { "0", "1" };
            //string[] VOUTEN = new string[] { CurLang.VOUTEN_00, CurLang.VOUTEN_10, CurLang.VOUTEN_11, CurLang.VOUTEN_01, };

            propertyGridEx_PC.Item.Add("PAGE", "OTP", false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);

            //propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC30P6060String0000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC30P6060FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC30P6060String0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("FCPU", "Fosc//4", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

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
            rootNode = XElement.Load(path);
            myvalue = from myTarget in rootNode.Descendants("Option")
                      select myTarget;
            foreach (XElement xnode in myvalue)
            {
                if (xnode.Attribute("FOSC_7041") != null)
                {
                    switch (xnode.Attribute("FOSC_7041").Value)
                    {
                        case "1": str_FOSC = "内部RC振荡器";
                            break;
                        case "2": str_FOSC = "外部晶体振荡器32768";
                            break;
                        case "3": str_FOSC = "外部晶体振荡器455K";
                            break;
                        case "4": str_FOSC = "外部晶体振荡器1M";
                            break;
                        case "5": str_FOSC = "外部晶体振荡器2M";
                            break;
                        case "6": str_FOSC = "外部晶体振荡器4M";
                            break;
                        case "7": str_FOSC = "外部晶体振荡器8M";
                            break;
                        case "8": str_FOSC = "外部晶体振荡器16M";
                            break;
                        case "9": str_FOSC = "外部时钟输入<1M";
                            break;
                        case "10": str_FOSC = "外部时钟输入<2M";
                            break;
                        case "11": str_FOSC = "外部时钟输入<4M";
                            break;
                        case "12": str_FOSC = "外部RC振荡器<1M";
                            break;
                        case "13": str_FOSC = "外部RC振荡器<2M";
                            break;
                        case "14": str_FOSC = "外部RC振荡器<4M";
                            break;
                        default: str_FOSC = "外部RC振荡器<8M";
                            break;
                    }
                    //propertyGridEx_PC.Item[4].Value = str_FOSC;
                }
            }
            propertyGridEx_PC.Item.Add("FOSC", str_FOSC, false, CurLang.OSCMFunc, CurLang.OSCMMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            //propertyGridEx_PC.Item.Add("HMLSEL", "4M-8M", false, "", "", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(HMLSEL, true);


            //propertyGridEx_PC.Item.Add("ENHANCE", "兼容模式", false, "", "", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FMODE, true);
 

            propertyGridEx_PC.Item.Add("DRVS", "0", false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(DRVS, true);

            //propertyGridEx_PC.Item.Add("SPDS", "0", false, "", "", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SPDS, true);

            //propertyGridEx_PC.Item.Add("VOUTEN", CurLang.VOUTEN_00, false, "", "", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VOUTEN, true);
        }

        private void MC32P7041_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5, uint pValue6, uint pValue7)
        {
            int i = 0;
            string str_FOSC = null;
            uint temp = pValue0 & 7;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "OTP";
                frmMAIN.RomSpace_stat = 1;                  //for 7041 2K7041   by lyl  170418
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "MTP";
                frmMAIN.RomSpace_stat = 0;
            }

            //LVRS
            /*i++;
            temp = (pValue1 >> 0) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1111;
                    break;
            }*/

            i++;
            temp = (pValue1 >> 4) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString2;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString3;
                    break;
            }

            i++;
            temp = (pValue1 >> 7) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC30P6060String1;
            }

            //FCPUS
            i++;
            temp = (pValue1 >> 8) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "Fosc//16";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "Fosc//32";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "Fosc//64";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "Fosc//128";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "Fosc//256";
                    break;
            }

            //FOSCS
            i++;

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
            rootNode = XElement.Load(path);
            myvalue = from myTarget in rootNode.Descendants("Option")
                      select myTarget;
            foreach (XElement xnode in myvalue)
            {
                if (xnode.Attribute("FOSC_7041") != null)
                {
                    switch (xnode.Attribute("FOSC_7041").Value)
                    {
                        case "1": str_FOSC = "内部RC振荡器";
                            break;
                        case "2": str_FOSC = "外部晶体振荡器32768";
                            break;
                        case "3": str_FOSC = "外部晶体振荡器455K";
                            break;
                        case "4": str_FOSC = "外部晶体振荡器1M";
                            break;
                        case "5": str_FOSC = "外部晶体振荡器2M";
                            break;
                        case "6": str_FOSC = "外部晶体振荡器4M";
                            break;
                        case "7": str_FOSC = "外部晶体振荡器8M";
                            break;
                        case "8": str_FOSC = "外部晶体振荡器16M";
                            break;
                        case "9": str_FOSC = "外部时钟输入<1M";
                            break;
                        case "10": str_FOSC = "外部时钟输入<2M";
                            break;
                        case "11": str_FOSC = "外部时钟输入<4M";
                            break;
                        case "12": str_FOSC = "外部RC振荡器<1M";
                            break;
                        case "13": str_FOSC = "外部RC振荡器<2M";
                            break;
                        case "14": str_FOSC = "外部RC振荡器<4M";
                            break;
                        default: str_FOSC = "外部RC振荡器<8M";
                            break;
                    }
                    propertyGridEx_PC.Item[4].Value = str_FOSC;
                }
            }
            /*temp = pValue3 & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "内部RC振荡器";
                    break;
                case 5:
                    if (((pValue3 >> 3) & 3) == 2)
                    {
                        propertyGridEx_PC.Item[i].Value = "外部晶体振荡器32768";
                    }
                    else if (((pValue3 >> 3) & 3) == 1)
                    {
                        propertyGridEx_PC.Item[i].Value = "外部晶体振荡器455K";   //455k---8M
                    }
                    else
                    {
                        propertyGridEx_PC.Item[i].Value = "外部晶体振荡器16M";
                    }                    
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "外部时钟输入<1M";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "外部RC振荡器<1M";
                    break;
            }*/

            //HMLSEL
            /*i++;
            temp = (pValue3 >> 3) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "4M-8M";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "455K";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "32K";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "12M~20M";
                    break;
            }*/

            //ENHANCE
            /*i++;
            temp = (pValue3 >> 5) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "兼容模式";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "扩展模式";
            }*/

            //DRVS
            i++;
            temp = (pValue3 >> 8) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "0";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "1";
            }

            //SPDS
            /*i++;
            temp = (pValue3 >> 9) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "0";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "1";
            }*/

            //VOUTEN
            /*i++;
            temp = (pValue7 >> 8) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.VOUTEN_00;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.VOUTEN_01;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.VOUTEN_10;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.VOUTEN_11;
                    break;
            }*/

        }
        private void MC32P7041_Option2Value()
        {
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3, optValue_Temp4, optValue_Temp5, optValue_Temp6, optValue_Temp7;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = optValue_Temp3 = optValue_Temp4 = optValue_Temp5 = optValue_Temp6 = optValue_Temp7 = 0;
            int i;
            string num="1";    //num for  show FOSCS 
            int j = 0;

            //MODE
            string temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "OTP")
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 0);
                frmMAIN.RomSpace_stat = 1;              //for 7041 2K7041   by lyl  170418
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 0);
                frmMAIN.RomSpace_stat = 0;
            }

            //LVRS
            /*j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == CurLang.LVRSMC30P6060String0000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1000)
            {
                optValue_Temp1 = optValue_Temp1 | (8 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1001)
            {
                optValue_Temp1 = optValue_Temp1 | (9 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1010)
            {
                optValue_Temp1 = optValue_Temp1 | (10 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1011)
            {
                optValue_Temp1 = optValue_Temp1 | (11 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1100)
            {
                optValue_Temp1 = optValue_Temp1 | (12 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1101)
            {
                optValue_Temp1 = optValue_Temp1 | (13 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1110)
            {
                optValue_Temp1 = optValue_Temp1 | (14 << 0);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (15 << 0);
            }*/

            //WDTC
            j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 4);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 4);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 4);
            }

            //mclre
            j++;
            
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == CurLang.MCLREMC30P6060String0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 7);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 7);
            }

            //FCPUS
            j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            switch (temp)
            {
                case "Fosc//2":
                    optValue_Temp1 = optValue_Temp1 | (0 << 8);
                    break;
                case "Fosc//4":
                    optValue_Temp1 = optValue_Temp1 | (1 << 8);
                    break;
                case "Fosc//8":
                    optValue_Temp1 = optValue_Temp1 | (2 << 8);
                    break;
                case "Fosc//16":
                    optValue_Temp1 = optValue_Temp1 | (3 << 8);
                    break;
                case "Fosc//32":
                    optValue_Temp1 = optValue_Temp1 | (4 << 8);
                    break;
                case "Fosc//64":
                    optValue_Temp1 = optValue_Temp1 | (5 << 8);
                    break;
                case "Fosc//128":
                    optValue_Temp1 = optValue_Temp1 | (6 << 8);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (7 << 8);
                    break;
            }

            //LVRS 0000
            optValue_Temp1 = optValue_Temp1 | (0 << 0);

            optValue_Temp1 = optValue_Temp1 | (0 << 13);

            //FOSCS
            j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "内部RC振荡器")
            {
                num = "1";
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 11
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器32768")
            {
                num = "2";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (2 << 3);     //HMLSEL 10
                optValue_Temp4 = optValue_Temp4 | (0 << 3);     //XTDRVB 0
            }
            else if (temp == "外部晶体振荡器455K")
            {
                num = "3";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (1 << 3);     //HMLSEL 01
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器1M")
            {
                num = "4";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (1 << 3);     //HMLSEL 01
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器2M")
            {
                num = "5";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (1 << 3);     //HMLSEL 01
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器4M")
            {
                num = "6";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (1 << 3);     //HMLSEL 01
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器8M")
            {
                num = "7";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (1 << 3);     //HMLSEL 01
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部晶体振荡器16M")
            {
                num = "8";
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
                optValue_Temp3 = optValue_Temp3 | (0 << 3);     //HMLSEL 00
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部时钟输入<1M")
            {
                num = "9";
                optValue_Temp3 = optValue_Temp3 | (6 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部时钟输入<2M")
            {
                num = "10";
                optValue_Temp3 = optValue_Temp3 | (6 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部时钟输入<4M")
            {
                num = "11";
                optValue_Temp3 = optValue_Temp3 | (6 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部RC振荡器<1M")
            {
                num = "12";
                optValue_Temp3 = optValue_Temp3 | (7 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部RC振荡器<2M")
            {
                num = "13";
                optValue_Temp3 = optValue_Temp3 | (7 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else if (temp == "外部RC振荡器<4M")
            {
                num = "14";
                optValue_Temp3 = optValue_Temp3 | (7 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }
            else
            {
                num = "15";
                optValue_Temp3 = optValue_Temp3 | (7 << 0);
                optValue_Temp3 = optValue_Temp3 | (3 << 3);     //HMLSEL 3
                optValue_Temp4 = optValue_Temp4 | (1 << 3);     //XTDRVB 1
            }

            //write Foscs 记录模式（为了反显模式）FrmMAIN P6622  by LYL 17/01/18 
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
            rootNode = XElement.Load(path);
            myvalue = from myTarget in rootNode.Descendants("Option")
                      select myTarget;
            foreach (XElement xnode in myvalue)
            {
                if (xnode.Attribute("FOSC_7041") == null)
                {
                     xnode.Add(new XAttribute("FOSC_7041", num));
                }
                else
                {
                    xnode.Attribute("FOSC_7041").Value = num;
                }
            }
            rootNode.Save(path);

            //HMLSEL
            /*j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "4M-8M")
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 3);
            }
            else if (temp == "455K")
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 3);
            }
            else if (temp == "32K")
            {
                optValue_Temp3 = optValue_Temp3 | (2 << 3);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (3 << 3);
            }*/

            //ENHANCE
            /*j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "兼容模式")
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 5);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 5);
            }*/
            if (mcuID == "0x7041" || mcuID == "0x6021")
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 5);         //7041 6021固定扩展模式
            }
            else if (mcuID == "0x9905" || mcuID == "0x2722")                               
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 5);         //MC9905 2722固定兼容模式
            }

            //DRVS
            j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "0")
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 8);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 8);
            }

            //SPDS
            /*j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == "0")
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 9);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 9);
            }*/

            //FILTER
            optValue_Temp4 = optValue_Temp4 | (0 << 0);

            //VOUTEN
            /*j++;
            temp = propertyGridEx_PC.Item[j].Value.ToString();
            if (temp == CurLang.VOUTEN_00)
            {
                optValue_Temp7 = optValue_Temp7 | (0 << 8);
            }
            else if (temp == CurLang.VOUTEN_01)
            {
                optValue_Temp7 = optValue_Temp7 | (1 << 8);
            }
            else if (temp == CurLang.VOUTEN_10)
            {
                optValue_Temp7 = optValue_Temp7 | (2 << 8);
            }
            else
            {
                optValue_Temp7 = optValue_Temp7 | (3 << 8);
            }*/

            if (((optValue_Temp3 & 7) == 0) && (((optValue_Temp1 >> 8) & 7) == 0) && (((optValue_Temp3 >> 5) & 1) == 1))
            {
                MessageBox.Show("当选择内部RC时，禁止选2T！请重新选择分频或振荡器模式，详见用户手册", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //propertyGridEx_PC.Item[5].Value = "兼容模式";
                propertyGridEx_PC.Item[3].Value = "Fosc//4";
            }

            optionValue.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp2, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);
            optionValue5.Text = Convert.ToString(optValue_Temp5, 16);
            optionValue6.Text = Convert.ToString(optValue_Temp6, 16);
            optionValue7.Text = Convert.ToString(optValue_Temp7, 16);

            

        }

    }


}