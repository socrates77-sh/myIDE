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

        private void MC32P7022_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4.5ms", "001:PWRT=TWDT=18ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                            "100:PWRT=4ms;TWDT=512ms","101:PWRT=16;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256;TWDT=4096ms"};
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };

            string[] MCLRE = new string[] { CurLang.MCLREMC32P21String1, CurLang.MCLREMC32P21String2 };

            string[] OSCM = new string[] { CurLang.OSCMMC33P116String1, CurLang.OSCMMC33P116String2 };

            string[] LVRS = new string[] { CurLang.LVRSMC32P7022String1, CurLang.LVRSMC32P7022String2, CurLang.LVRSMC32P7022String3, CurLang.LVRSMC32P7022String4,
                                           CurLang.LVRSMC32P7022String5, CurLang.LVRSMC32P7022String6, CurLang.LVRSMC32P7022String7, CurLang.LVRSMC32P7022String8,
                                           CurLang.LVRSMC32P7022String9, CurLang.LVRSMC32P7022String10, CurLang.LVRSMC32P7022String11, CurLang.LVRSMC32P7022String12,
                                           CurLang.LVRSMC32P7022String13, CurLang.LVRSMC32P7022String14, CurLang.LVRSMC32P7022String15, CurLang.LVRSMC32P7022String16};

            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 };

            string[] TADJH = new string[] { "0:0", "1:1" };

            string[] VDSEL = new string[] { "0:1.4V", "1:1.7V" };

            string[] DRVS = new string[] { CurLang.DRVSMC32P7022String1, CurLang.DRVSMC32P7022String2};

            string[] HIRCSEL = new string[] { CurLang.HIRCSELMC32P7022String1, CurLang.HIRCSELMC32P7022String2 };

            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };

            string[] FDS = new string[] { "00:FAS/8", "01:FAS/4", "10:FAS/2", "11:FAS" };

            string[] RCSMTB = new string[] { CurLang.RCSMTBMC32P7022String1, CurLang.RCSMTBMC32P7022String2 };


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4.5ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P21String1, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC33P116String1, false, CurLang.OSCMFunc, CurLang.OSCMMC33P116FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32P7022String1, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7022FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

//             propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);

//             propertyGridEx_PC.Item.Add("TADJH", "0:0", false, CurLang.TADJHMC32P7022Func, CurLang.TADJHMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(TADJH, true);
// 
//             propertyGridEx_PC.Item.Add("VDSEL", "0:1.4V", false, CurLang.VDSELMC31P11Func, CurLang.VDSELMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("DRVS", CurLang.DRVSMC32P7022String1, false, CurLang.DRVSMC32P7022Func, CurLang.DRVSMC32P7022FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(DRVS, true);

//             propertyGridEx_PC.Item.Add("HIRCSEL", CurLang.HIRCSELMC32P7022String1, false, CurLang.HIRCSELMC32P7022Func, CurLang.HIRCSELMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(HIRCSEL, true);

            propertyGridEx_PC.Item.Add("FAS", "000:16M", false, CurLang.FASMC32P7022Func, CurLang.FASMC32P7022FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

//             propertyGridEx_PC.Item.Add("FDS", "00:FAS/8", false, CurLang.FDSMC32P7022Func, CurLang.FDSMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FDS, true);
// 
//             propertyGridEx_PC.Item.Add("RCSMTB", CurLang.RCSMTBMC32P7022String1, false, CurLang.RCSMTBMC32P7022Func, CurLang.RCSMTBMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCSMTB, true);
        }

        private void MC32P7022_Value2Option(uint pvalue)
        {
            //propertyGridEx_PC.Item[0].Value = "01:高频晶振模式";
            int i = 0;

            //WDTC
            uint temp = pvalue & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString2;
                    break;
                //case 2:
                //    propertyGridEx_PC.Item[0].Value = "1x:始终开看门狗";
                //    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.WDTCString3;
                    break;
            }

            //WDTT
            i++;
            temp = (pvalue >> 2) & 3;
            uint temp1 = ((pvalue >> 13) & 1);
            temp1 = temp1 << 2;
            temp = temp1 | temp;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:PWRT=TWDT=4.5ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:PWRT=TWDT=18ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:PWRT=TWDT=64ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:PWRT=TWDT=256ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:PWRT=4ms;TWDT=512ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:PWRT=16;TWDT=1024ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:PWRT=64ms;TWDT=2048ms";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:PWRT=256;TWDT=4096ms";
                    break;
            }

            //FCPU
            i++;
            temp = (pvalue >> 4) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:Fosc//16";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:Fosc//32";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:Fosc//64";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:Fosc//128";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:Fosc//256";
                    break;

            }

            //MCLRE
            i++;
            temp = (pvalue >> 7) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P21String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P21String2;
            }

            //FOSC
            i++;
            temp = (pvalue >> 8) & 3;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC33P116String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC33P116String2;
            }

            //VLVRS
            i++;
            temp = (pvalue >> 10) & 7;
            temp1 = ((pvalue >> 14) & 1);
            temp1 = temp1 << 3;
            temp = temp1 | temp;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String2;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String3;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String4;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String5;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String6;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String7;
                    break;
                case 7:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String8;
                    break;
                case 8:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String9;
                    break;
                case 9:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String10;
                    break;
                case 10:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String11;
                    break;
                case 11:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String12;
                    break;
                case 12:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String13;
                    break;
                case 13:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String14;
                    break;
                case 14:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String15;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7022String16;
                    break;
            }

//             temp = (pvalue >> 15) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[6].Value = CurLang.PROTECTMC20P24BString2;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[6].Value = CurLang.PROTECTMC20P24BString1;
//             }

            //TADJH
//             temp = (pvalue >> 16) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[13].Value = "1:1";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[13].Value = "0:0";
//             }

            //VDSEL
//             temp = (pvalue >> 17) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[7].Value = "1:1.7V";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[7].Value = "0:1.4V";
//             }

            //DRVS
            i++;
            temp = (pvalue >> 18) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.DRVSMC32P7022String2;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.DRVSMC32P7022String1;
            }

            //HIRCSEL
//             temp = (pvalue >> 19) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[9].Value = CurLang.HIRCSELMC32P7022String2;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[9].Value = CurLang.HIRCSELMC32P7022String1;
//             }

            //FAS
            i++;
            temp = (pvalue >> 24) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:16M";
                    frmMAIN.FREQ = 0x16;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:8M";
                    frmMAIN.FREQ = 0x08;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:4M";
                    frmMAIN.FREQ = 0x04;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:2M";
                    frmMAIN.FREQ = 0x02;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:1M";
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "101:455K";
                    frmMAIN.FREQ = 0x45;
                    break;
            }

//             temp = (pvalue >> 27) & 3;
//             switch (temp)
//             {
//                 case 0:
//                     propertyGridEx_PC.Item[11].Value = "00:FAS/8";
//                     break;
//                 case 1:
//                     propertyGridEx_PC.Item[11].Value = "01:FAS/4";
//                     break;
//                 case 2:
//                     propertyGridEx_PC.Item[11].Value = "10:FAS/2";
//                     break;
//                 default:
//                     propertyGridEx_PC.Item[11].Value = "11:FAS";
//                     break;
//             }

//             temp = (pvalue >> 29) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[12].Value = CurLang.RCSMTBMC32P7022String2;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[12].Value = CurLang.RCSMTBMC32P7022String1;
//             }

        }
        private void MC32P7022_Option2Value()
        {
            Int32 optValue_Temp = 0;
            int i = 0;

            //WDTC
            string temp = propertyGridEx_PC.Item[i].Value.ToString();
            //"00：始终关闭看门狗", "01：休眠模式下关闭看门狗", "1X：始终开启看门狗"
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp = optValue_Temp | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp = optValue_Temp | (1 << 0);
            }
            //case "1x:始终开看门狗":
            //    optValue_Temp = optValue_Temp | (2 << 0);
            //    break;
            else
            {
                optValue_Temp = optValue_Temp | (3 << 0);
            }

            //WDTT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "001:PWRT=TWDT=18ms":
                    optValue_Temp = optValue_Temp | (1 << 2);
                    optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "010:PWRT=TWDT=64ms":
                    optValue_Temp = optValue_Temp | (2 << 2);
                    optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "011:PWRT=TWDT=256ms":
                    optValue_Temp = optValue_Temp | (3 << 2);
                    optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "100:PWRT=4ms;TWDT=512ms":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                case "101:PWRT=16;TWDT=1024ms":
                    optValue_Temp = optValue_Temp | (1 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp = optValue_Temp | (2 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (3 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
            }
            //FCPU
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//2":
                    optValue_Temp = optValue_Temp | (0 << 4);
                    break;
                case "001:Fosc//4":
                    optValue_Temp = optValue_Temp | (1 << 4);
                    break;
                case "010:Fosc//8":
                    optValue_Temp = optValue_Temp | (2 << 4);
                    break;
                case "011:Fosc//16":
                    optValue_Temp = optValue_Temp | (3 << 4);
                    break;
                case "100:Fosc//32":
                    optValue_Temp = optValue_Temp | (4 << 4);
                    break;
                case "101:Fosc//64":
                    optValue_Temp = optValue_Temp | (5 << 4);
                    break;
                case "110:Fosc//128":
                    optValue_Temp = optValue_Temp | (6 << 4);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 4);
                    break;
            }

            //mclre
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MCLREMC32P21String1)
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }

            //FOSC
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.OSCMMC33P116String1)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }

            //VLVRS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRSMC32P7022String1)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if(temp == CurLang.LVRSMC32P7022String2)
            {
                optValue_Temp = optValue_Temp | (1 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String3)
            {
                optValue_Temp = optValue_Temp | (2 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String4)
            {
                optValue_Temp = optValue_Temp | (3 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String5)
            {
                optValue_Temp = optValue_Temp | (4 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String6)
            {
                optValue_Temp = optValue_Temp | (5 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String7)
            {
                optValue_Temp = optValue_Temp | (6 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String8)
            {
                optValue_Temp = optValue_Temp | (7 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String9)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String10)
            {
                optValue_Temp = optValue_Temp | (1 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String11)
            {
                optValue_Temp = optValue_Temp | (2 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String12)
            {
                optValue_Temp = optValue_Temp | (3 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String13)
            {
                optValue_Temp = optValue_Temp | (4 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String14)
            {
                optValue_Temp = optValue_Temp | (5 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String15)
            {
                optValue_Temp = optValue_Temp | (6 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else
            {
                optValue_Temp = optValue_Temp | (7 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }

            //PROTECT
//             temp = propertyGridEx_PC.Item[6].Value.ToString();
//             if (temp == CurLang.PROTECTMC20P24BString2)
//             {
//                 optValue_Temp = optValue_Temp | (1 << 15);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (0 << 15);
//             }

            //TADJH
//             i++;
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             if (temp == "1:1")
//             {
//                 optValue_Temp = optValue_Temp | (1 << 16);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (0 << 16);
//             }

            //VDSEL
//             temp = propertyGridEx_PC.Item[7].Value.ToString();
//             if (temp == "1:1.7V")
//             {
//                 optValue_Temp = optValue_Temp | (1 << 17);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (0 << 17);
//             }

            //DRVS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.DRVSMC32P7022String2)
            {
                optValue_Temp = optValue_Temp | (1 << 18);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 18);
            }

            //HIRCSEL
            optValue_Temp = optValue_Temp | (1 << 19);
//             i++;
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             if (temp == CurLang.HIRCSELMC32P7022String2)
//             {
//                 optValue_Temp = optValue_Temp | (1 << 19);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (0 << 19);
//             }

            //FAS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp = optValue_Temp | (0 << 24);
                    optValue_Temp = optValue_Temp | (0 << 16);
                    optValue_Temp = optValue_Temp | (1 << 17);
                    optValue_Temp = optValue_Temp | (1 << 29);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp = optValue_Temp | (1 << 24);
                    optValue_Temp = optValue_Temp | (0 << 16);
                    optValue_Temp = optValue_Temp | (0 << 17);
                    optValue_Temp = optValue_Temp | (1 << 29);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp = optValue_Temp | (2 << 24);
                    optValue_Temp = optValue_Temp | (1 << 16);
                    optValue_Temp = optValue_Temp | (1 << 17);
                    optValue_Temp = optValue_Temp | (1 << 29);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp = optValue_Temp | (3 << 24);
                    optValue_Temp = optValue_Temp | (1 << 16);
                    optValue_Temp = optValue_Temp | (0 << 17);
                    optValue_Temp = optValue_Temp | (1 << 29);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp = optValue_Temp | (4 << 24);
                    optValue_Temp = optValue_Temp | (1 << 16);
                    optValue_Temp = optValue_Temp | (0 << 17);
                    optValue_Temp = optValue_Temp | (0 << 29);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp = optValue_Temp | (5 << 24);
                    optValue_Temp = optValue_Temp | (1 << 16);
                    optValue_Temp = optValue_Temp | (0 << 17);
                    optValue_Temp = optValue_Temp | (0 << 29);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            //FDS
            optValue_Temp = optValue_Temp | (3 << 27);
//             i++;
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             switch (temp)
//             {
//                 case "00:FAS/8":
//                     optValue_Temp = optValue_Temp | (0 << 27);
//                     break;
//                 case "01:FAS/4":
//                     optValue_Temp = optValue_Temp | (1 << 27);
//                     break;
//                 case "10:FAS/2":
//                     optValue_Temp = optValue_Temp | (2 << 27);
//                     break;
//                 default:
//                     optValue_Temp = optValue_Temp | (3 << 27);
//                     break;
//             }

            //RCSMTB
//             i++;
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             if (temp == CurLang.RCSMTBMC32P7022String2)
//             {
//                 optValue_Temp = optValue_Temp | (1 << 29);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (0 << 29);
//             }

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

    }


}