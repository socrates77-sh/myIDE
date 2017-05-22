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

        private void MC31P11_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            //string[] LVRS = new string[] { "000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            //string[] WDTT = new string[] { "000:PWRT=TWDT=4.5ms", "001:PWRT=TWDT=18ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
            //                                "100:PWRT=4ms;TWDT=512ms","101:PWRT=16;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256;TWDT=4096ms"};
            string[] FCPU = new string[] { "00:Fosc//2", "01:Fosc//4", "10:Fosc//8", "11:Fosc//16" };

            string[] ISEL = new string[] { "00:6uA", "01:3uA", "10:1.5uA", "11:0.75uA" };

            string[] VDSEL = new string[] { "0:1.7V", "1:1.5V" };

            string[] PSEL = new string[] { "00:10ns","01:2us","10:1us","11:500ns" }; //0,1

            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //1,0


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("FCPU", "000:Fosc//2", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("ISEL", "00:6uA", false, CurLang.ISELMC31P11Func, CurLang.ISELMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ISEL, true);

            propertyGridEx_PC.Item.Add("VDSEL", "0:1.7V", false, CurLang.VDSELMC31P11Func, CurLang.VDSELMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("PSEL", "00:10ns", false, CurLang.PSELMC31P11Func, "10ns--2us", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PSEL, true);

            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);


        }

        private void MC31P11_Value2Option(uint pvalue)
        {
            //propertyGridEx_PC.Item[0].Value = "01:高频晶振模式";

            frmMAIN.FREQ = 0x08;

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
            //uint temp1 = ((pvalue >> 10) & 1);
            //temp1 = temp1 << 2;
            //temp = temp1 | temp;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = "00:Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = "01:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[1].Value = "10:Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[1].Value = "11:Fosc//16";
                    break;
               
                default:
                    propertyGridEx_PC.Item[1].Value = "11:Fosc//16";
                    break;
            }

            temp = (pvalue >> 4) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = "00:6uA";
                    break;
                case 1:
                    propertyGridEx_PC.Item[2].Value = "01:3uA";
                    break;
                case 2:
                    propertyGridEx_PC.Item[2].Value = "10:1.5uA";
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = "11:0.75uA";
                    break;                                             
                default:
                    propertyGridEx_PC.Item[2].Value = "11:0.75uA";
                    break;

            }

            temp = (pvalue >> 6) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = "1:1.5V";
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = "0:1.7V";
            }

            //temp = (pvalue >> 8) & 3;
            //switch (temp)
            //{
            //    case 0:
            //        propertyGridEx_PC.Item[4].Value = "00:内部高频&内部低频";
            //        break;
            //    case 1:
            //        propertyGridEx_PC.Item[4].Value = "01:内部高频&外部低频";
            //        break;
            //    case 2:
            //        propertyGridEx_PC.Item[4].Value = "10:外部高频&内部低频";
            //        break;
            //    default:
            //        propertyGridEx_PC.Item[4].Value = "11:保留";
            //        break;
            //}

            //temp = (pvalue >> 10) & 3;
            //switch (temp)
            //{
            //    case 0:
            //        propertyGridEx_PC.Item[5].Value = "000:LVR电压=3.0V";
            //        break;
            //    default:
            //        propertyGridEx_PC.Item[5].Value = "其它：保留";
            //        break;
            //}

            temp = (pvalue >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString1;
            }

            temp = (pvalue >> 24) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = "00:10ns";
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = "01:2us";
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = "10:1us";
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = "11:500ns";
                    break;
            }
            
        }


        private void MC31P11_Option2Value()
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
                case "00:Fosc//2":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    
                    break;
                case "01:Fosc//4":
                    optValue_Temp = optValue_Temp | (1 << 2);
                   // optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "10:Fosc//8":
                    optValue_Temp = optValue_Temp | (2 << 2);
                    //optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "11:Fosc//16":
                    optValue_Temp = optValue_Temp | (3 << 2);
                    //optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                
                default:
                    optValue_Temp = optValue_Temp | (3 << 2);
                   // optValue_Temp = optValue_Temp | (1 << 13);
                    break;
            }
            //LVRS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" 
            switch (temp)
            {
                case "00:6uA":
                    optValue_Temp = optValue_Temp | (0 << 4);
                    break;
                case "01:3uA":
                    optValue_Temp = optValue_Temp | (1 << 4);
                    break;
                case "10:1.5uA":
                    optValue_Temp = optValue_Temp | (2 << 4);
                    break;
                case "11:0.75uA":
                    optValue_Temp = optValue_Temp | (3 << 4);
                    break;
                
                default:
                    optValue_Temp = optValue_Temp | (3 << 4);
                    break;
            }

            //mclre
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == "0:1.7V")
            {
                optValue_Temp = optValue_Temp | (0 << 6);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 6);
            }

            //WDTC
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.PROTECTMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (1 << 12); 
            } 
            else
            {
                optValue_Temp = optValue_Temp | (0 << 12); 
            }

            temp = propertyGridEx_PC.Item[4].Value.ToString();
            switch (temp)
            {
                case "00:10ns":
                    optValue_Temp = optValue_Temp | (0 <<24);
                    break;
                case "01:2us":
                    optValue_Temp = optValue_Temp | (1 << 24);
                    break;
                case "10:1us":
                    optValue_Temp = optValue_Temp | (2 << 24);
                    break;
                case "11:500ns":
                    optValue_Temp = optValue_Temp | (3 << 24);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (3 << 24);
                    break;
            }



            // bit11--7  reserve
            optValue_Temp = optValue_Temp | (0x1f << 7);
            optValue_Temp = optValue_Temp | (0x7ff << 13);

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

    }


}