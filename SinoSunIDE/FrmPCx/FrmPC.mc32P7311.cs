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

        private void MC32P7311_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            //string[] LVRS = new string[] { "000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4.5ms", "001:PWRT=TWDT=18ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                            "100:PWRT=4ms;TWDT=512ms","101:PWRT=16;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256;TWDT=4096ms"};
            string[] FCPU = new string[] { "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };

            string[] OSCM = new string[] { CurLang.OSCMMC32P21String1, CurLang.OSCMMC32P21String2, CurLang.OSCMMC32P21String3, CurLang.OSCMMC32P21String4 };

            string[] MCLRE = new string[] { CurLang.MCLREMC32P21String1, CurLang.MCLREMC32P21String2 };

            string[] LVRS = new string[] { CurLang.LVRSMC30P6060String0000, CurLang.LVRSMC30P6060String0001, CurLang.LVRSMC30P6060String0010, CurLang.LVRSMC30P6060String0011,
                                           CurLang.LVRSMC30P6060String0100, CurLang.LVRSMC30P6060String0101, CurLang.LVRSMC30P6060String0110, CurLang.LVRSMC30P6060String0111,
                                           CurLang.LVRSMC30P6060String1000, CurLang.LVRSMC30P6060String1001, CurLang.LVRSMC30P6060String1010, CurLang.LVRSMC30P6060String1011,
                                           CurLang.LVRSMC30P6060String1100, CurLang.LVRSMC30P6060String1101, CurLang.LVRSMC30P6060String1110, CurLang.LVRSMC30P6060String1111}; //0,1

            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //1,0


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4.5ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P21String1, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

//             propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC32P21String1, false, CurLang.OSCMFunc, CurLang.OSCMMC20P24BFuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC30P6060String0000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);


        }

        private void MC32P7311_Value2Option(uint pvalue)
        {
            frmMAIN.FREQ = 0x16;

            uint temp = pvalue & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString2;
                    break;
                //case 2:
                //    propertyGridEx_PC.Item[0].Value = "1x:始终开看门狗";
                //    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString3;
                    break;
            }

            temp = (pvalue >> 2) & 7;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = "000:PWRT=TWDT=4.5ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = "001:PWRT=TWDT=18ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[1].Value = "010:PWRT=TWDT=64ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[1].Value = "011:PWRT=TWDT=256ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[1].Value = "100:PWRT=4ms;TWDT=512ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[1].Value = "101:PWRT=16;TWDT=1024ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[1].Value = "110:PWRT=64ms;TWDT=2048ms";
                    break;
                default:
                    propertyGridEx_PC.Item[1].Value = "111:PWRT=256;TWDT=4096ms";
                    break;
            }

            temp = (pvalue >> 5) & 7;
            switch (temp)
            {
                case 1:
                    propertyGridEx_PC.Item[2].Value = "001:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[2].Value = "010:Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = "011:Fosc//16";
                    break;
                case 4:
                    propertyGridEx_PC.Item[2].Value = "100:Fosc//32";
                    break;
                case 5:
                    propertyGridEx_PC.Item[2].Value = "101:Fosc//64";
                    break;
                case 6:
                    propertyGridEx_PC.Item[2].Value = "110:Fosc//128";
                    break;
                default:
                    propertyGridEx_PC.Item[2].Value = "111:Fosc//256";
                    break;

            }

            temp = (pvalue >> 8) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String1;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String2;
            }

//             temp = (pvalue >> 9) & 3;
//             switch (temp)
//             {
//                 case 0:
//                     propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String1;
//                     break;
//                 case 1:
//                     propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String2;
//                     break;
//                 case 2:
//                     propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String3;
//                     break;
//                 default:
//                     propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String4;
//                     break;
//             }

            temp = (pvalue >> 11) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC30P6060String1111;
                    break;
            }

            temp = (pvalue >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString1;
            }


        }
        private void MC32P7311_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            //"11:内部8MHZ & RTC", "10:内部8MHZ", "01:高频晶振模式","00:低频晶振模式"
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

            //FCPUS
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            //"11:Fosc//16","10:Fosc//8","01:Fosc//4" ,"00:Fosc//2" 
            switch (temp)
            {
                case "000:PWRT=TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    break;
                case "001:PWRT=TWDT=18ms":
                    optValue_Temp = optValue_Temp | (1 << 2);
                    break;
                case "010:PWRT=TWDT=64ms":
                    optValue_Temp = optValue_Temp | (2 << 2);
                    break;
                case "011:PWRT=TWDT=256ms":
                    optValue_Temp = optValue_Temp | (3 << 2);
                    break;
                case "100:PWRT=4ms;TWDT=512ms":
                    optValue_Temp = optValue_Temp | (4 << 2);
                    break;
                case "101:PWRT=16;TWDT=1024ms":
                    optValue_Temp = optValue_Temp | (5 << 2);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp = optValue_Temp | (6 << 2);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 2);
                    break;
            }
            //LVRS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" 
            switch (temp)
            {
                case "001:Fosc//4":
                    optValue_Temp = optValue_Temp | (1 << 5);
                    break;
                case "010:Fosc//8":
                    optValue_Temp = optValue_Temp | (2 << 5);
                    break;
                case "011:Fosc//16":
                    optValue_Temp = optValue_Temp | (3 << 5);
                    break;
                case "100:Fosc//32":
                    optValue_Temp = optValue_Temp | (4 << 5);
                    break;
                case "101:Fosc//64":
                    optValue_Temp = optValue_Temp | (5 << 5);
                    break;
                case "110:Fosc//128":
                    optValue_Temp = optValue_Temp | (6 << 5);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 5);
                    break;
            }

            //mclre
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.MCLREMC32P21String1)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }

            //OSCM
//             temp = propertyGridEx_PC.Item[4].Value.ToString();
//             if (temp == CurLang.OSCMMC32P21String1)
//             {
//                 optValue_Temp = optValue_Temp | (0 << 9);
//             }
//             else if (temp == CurLang.OSCMMC32P21String2)
//             {
//                 optValue_Temp = optValue_Temp | (1 << 9);
//             }
//             else if (temp == CurLang.OSCMMC32P21String3)
//             {
//                 optValue_Temp = optValue_Temp | (2 << 9);
//             }
//             else
//             {
//                 optValue_Temp = optValue_Temp | (3 << 9);
//             }

            temp = propertyGridEx_PC.Item[4].Value.ToString();
            //"00:PWRT=TWDT=4.5ms", "01:PWRT=TWDT=18ms", "10:PWRT=TWDT=72ms", "11:PWRT=TWDT=288ms"
            if (temp == CurLang.LVRSMC30P6060String0000)
            {
                optValue_Temp = optValue_Temp | (0 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0001)
            {
                optValue_Temp = optValue_Temp | (1 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0010)
            {
                optValue_Temp = optValue_Temp | (2 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0011)
            {
                optValue_Temp = optValue_Temp | (3 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0100)
            {
                optValue_Temp = optValue_Temp | (4 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0101)
            {
                optValue_Temp = optValue_Temp | (5 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0110)
            {
                optValue_Temp = optValue_Temp | (6 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String0111)
            {
                optValue_Temp = optValue_Temp | (7 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1000)
            {
                optValue_Temp = optValue_Temp | (8 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1001)
            {
                optValue_Temp = optValue_Temp | (9 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1010)
            {
                optValue_Temp = optValue_Temp | (10 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1011)
            {
                optValue_Temp = optValue_Temp | (11 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1100)
            {
                optValue_Temp = optValue_Temp | (12 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1101)
            {
                optValue_Temp = optValue_Temp | (13 << 11);
            }
            else if (temp == CurLang.LVRSMC30P6060String1110)
            {
                optValue_Temp = optValue_Temp | (14 << 11);
            }
            else
            {
                optValue_Temp = optValue_Temp | (15 << 11);
            }

            //PROTECT
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.PROTECTMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (1 << 15);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 15);
            }

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

    }


}