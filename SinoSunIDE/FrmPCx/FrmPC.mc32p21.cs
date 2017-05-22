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

        private void MC32P21_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            //string[] LVRS = new string[] { "000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4.5ms", "001:PWRT=TWDT=18ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                            "100:PWRT=4ms;TWDT=512ms","101:PWRT=16;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256;TWDT=4096ms"};
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16","100:Fosc//32","101:Fosc//64","110:Fosc//128","111:Fosc//256" };

            string[] OSCM = new string[] { CurLang.OSCMMC32P21String1, CurLang.OSCMMC32P21String2, CurLang.OSCMMC32P21String3, CurLang.OSCMMC32P21String4 };

            string[] MCLRE = new string[] { CurLang.MCLREMC32P21String1, CurLang.MCLREMC32P21String2 };

            string[] LVRS = new string[] { CurLang.LVRSMC32P21String1, CurLang.LVRSMC32P21String2 }; //0,1

            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //1,0


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4.5ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P21String1, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC32P21String1, false, CurLang.OSCMFunc, CurLang.OSCMMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32P21String1, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);
   

        }

        private void MC32P21_Value2Option(uint pvalue)
        {
            //propertyGridEx_PC.Item[0].Value = "01:高频晶振模式";

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

            temp = (pvalue >> 2) & 3;
            uint temp1=((pvalue>>10)&1);
            temp1=temp1<<2;
            temp = temp1 | temp;

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

            temp = (pvalue >> 4) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = "000:Fosc//2";
                    break;
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

            temp = (pvalue >> 7) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String1;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String2;
            }

            temp = (pvalue >> 8) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String2;
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String3;
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC32P21String4;
                    break;
            }

            temp = (pvalue >> 10) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P21String1;
                    break;       
                default:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P21String2;
                    break;
            }

            temp = (pvalue >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = CurLang.PROTECTMC20P24BString1;
            }


        }
        private void MC32P21_Option2Value()
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
                    optValue_Temp = optValue_Temp | (1<< 2);
                    optValue_Temp = optValue_Temp | (1 <<13);
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
            //LVRS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" 
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
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.MCLREMC32P21String1)
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }

            //WDTC
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if(temp == CurLang.OSCMMC32P21String1)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else if(temp == CurLang.OSCMMC32P21String2)
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }
            else if(temp == CurLang.OSCMMC32P21String3)
            {
                optValue_Temp = optValue_Temp | (2 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << 8);
            }

            temp = propertyGridEx_PC.Item[5].Value.ToString();
                //"00:PWRT=TWDT=4.5ms", "01:PWRT=TWDT=18ms", "10:PWRT=TWDT=72ms", "11:PWRT=TWDT=288ms"
            if (temp == CurLang.LVRSMC32P21String1)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
            }
            else
            {
                optValue_Temp = optValue_Temp | (7 << 10);
            }

            

            // bit14--12  reserve
            optValue_Temp = optValue_Temp | (1 << 14);

            //PROTECT
            temp = propertyGridEx_PC.Item[6].Value.ToString();
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