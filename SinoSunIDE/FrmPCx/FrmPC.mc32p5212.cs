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

        private void MC32P5212_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=16ms", "001:PWRT=TWDT=64ms", "010:PWRT=TWDT=256ms", "011:PWRT=TWDT=1024ms",
                                            "100:PWRT=16ms;TWDT=2048ms","101:PWRT=64ms;TWDT=4096ms","110:PWRT=256ms;TWDT=8192ms","111:PWRT=1024ms;TWDT=16384ms"};
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };

            string[] MCLRE = new string[] { CurLang.MCLREMC32P21String1, CurLang.MCLREMC32P21String2 };

            string[] ROTP = new string[] { CurLang.ROTPMC30P6060String0, CurLang.ROTPMC30P6060String1 };

            string[] LVRS = new string[] { "0000:LVR Close","0010:LVR=1.4V", "0011:LVR=1.5V",
                                           "0100:LVR=1.6V", "0101:LVR=1.7V", "0110:LVR=1.8V", "0111:LVR=1.9V",
                                           "1000:LVR=2.0V", "1001:LVR=2.1V", "1010:LVR=2.2V", "1011:LVR=2.3V",
                                           "1100:LVR=2.4V", "1101:LVR=2.5V", "1110:LVR=2.6V", "1111:LVR=2.7V"};

            string[] CLKSEL = new string[] { CurLang.CLKSELMC32P7510String0, CurLang.CLKSELMC32P7510String1 };

            //string[] TADJH = new string[] { "0", "1" };

            //string[] VDSEL = new string[] { "0:1.4V", "1:1.7V" };

            string[] DSEL = new string[] { "00:125mA", "01:250mA", "10:375mA", "11:500mA" };

            //string[] ISEL = new string[] { "00:6ua", "01:3ua", "10:1.5ua", "11:0.75ua" };

            //string[] PSEL = new string[] { "00:10ns", "01:1us", "10:2us", "11:500ns" };

            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };

            //string[] FDS = new string[] { "00:FAS/8", "01:FAS/4", "10:FAS/2", "11:FAS" };

            //string[] RCSMTB = new string[] { CurLang.RCSMTBMC32P7022String1, CurLang.RCSMTBMC32P7022String2 };

            string[] LVDPD = new string[] { CurLang.LVDPDMC32P5212String0, CurLang.LVDPDMC32P5212String1 };


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=16ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "011:Fosc//16", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P21String1, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("ROTP", CurLang.ROTPMC30P6060String0, false, CurLang.ROTPMC30P6060Func, CurLang.ROTPMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ROTP, true);

            propertyGridEx_PC.Item.Add("LVRS", "1000:LVR=2.0V", false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC33P78FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            //propertyGridEx_PC.Item.Add("CLKSEL", CurLang.CLKSELMC32P7510String1, false, CurLang.CLKSELMC32P7510Func, CurLang.CLKSELMC32P7510FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CLKSEL, true);

            //propertyGridEx_PC.Item.Add("TADJH", "0", false, "", "", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(TADJH, true);

            //propertyGridEx_PC.Item.Add("VDSEL", "0:1.4V", false, CurLang.VDSELMC31P11Func, CurLang.VDSELMC32P7022FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("DSEL", "00:125mA", false, CurLang.ISELMC31P11Func, CurLang.ISELMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(DSEL, true);

            //propertyGridEx_PC.Item.Add("ISEL", "11:0.75ua", false, CurLang.ISELMC31P11Func, CurLang.ISELMC31P11FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ISEL, true);

            //propertyGridEx_PC.Item.Add("PSEL", "11:500ns", false, CurLang.PSELMC31P11Func, "10ns--2us", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PSEL, true);

            propertyGridEx_PC.Item.Add("FAS", "000:16M", false, CurLang.FASMC32P7022Func, CurLang.FASMC32P7022FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

            //propertyGridEx_PC.Item.Add("FDS", "11:FAS", false, CurLang.FDSMC32P7022Func, CurLang.FDSMC32P7022FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FDS, true);

            //propertyGridEx_PC.Item.Add("RCSMTB", CurLang.RCSMTBMC32P7022String1, false, CurLang.RCSMTBMC32P7022Func, CurLang.RCSMTBMC32P7022FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCSMTB, true);

            propertyGridEx_PC.Item.Add("LVDPD", CurLang.LVDPDMC32P5212String0, false, CurLang.LVDPDMC32P5212Func, CurLang.LVDPDMC32P5212FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVDPD, true);

        }

        private void MC32P5212_Value2Option(uint pvalue0, uint pvalue2)
        {
            int i = 0;
            uint temp = pvalue0 & 3;
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

            i++;
            temp = (pvalue0 >> 2) & 3;
            uint temp1 = ((pvalue0 >> 13) & 1);
            temp1 = temp1 << 2;
            temp = temp1 | temp;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:PWRT=TWDT=16ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:PWRT=TWDT=64ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:PWRT=TWDT=256ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:PWRT=TWDT=1024ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:PWRT=16ms;TWDT=2048ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:PWRT=64ms;TWDT=4096ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:PWRT=256ms;TWDT=8192ms";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:PWRT=1024ms;TWDT=16384ms";
                    break;
            }

            i++;
            temp = (pvalue0 >> 4) & 7;
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

            i++;
            temp = (pvalue0 >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P21String2;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P21String1;
            }

            i++;
            //ROTP
            temp = (pvalue0 >> 8) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ROTPMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ROTPMC30P6060String0;
            }

            i++;
            //LVRS
            temp = (pvalue0 >> 10) & 7;
            temp1 = ((pvalue0 >> 14) & 1);
            temp1 = temp1 << 3;
            temp = temp1 | temp;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "0000:LVR Close";
            }
//             else if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[i].Value = "0001:LVR=1.3V";
//             }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[i].Value = "0010:LVR=1.4V";
            }
            else if (temp == 3)
            {
                propertyGridEx_PC.Item[i].Value = "0011:LVR=1.5V";
            }
            else if (temp == 4)
            {
                propertyGridEx_PC.Item[i].Value = "0100:LVR=1.6V";
            }
            else if (temp == 5)
            {
                propertyGridEx_PC.Item[i].Value = "0101:LVR=1.7V";
            }
            else if (temp == 6)
            {
                propertyGridEx_PC.Item[i].Value = "0110:LVR=1.8V";
            }
            else if (temp == 7)
            {
                propertyGridEx_PC.Item[i].Value = "0111:LVR=1.9V";
            }
            else if (temp == 8)
            {
                propertyGridEx_PC.Item[i].Value = "1000:LVR=2.0V";
            }
            else if (temp == 9)
            {
                propertyGridEx_PC.Item[i].Value = "1001:LVR=2.1V";
            }
            else if (temp == 10)
            {
                propertyGridEx_PC.Item[i].Value = "1010:LVR=2.2V";
            }
            else if (temp == 11)
            {
                propertyGridEx_PC.Item[i].Value = "1011:LVR=2.3V";
            }
            else if (temp == 12)
            {
                propertyGridEx_PC.Item[i].Value = "1100:LVR=2.4V";
            }
            else if (temp == 13)
            {
                propertyGridEx_PC.Item[i].Value = "1101:LVR=2.5V";
            }
            else if (temp == 14)
            {
                propertyGridEx_PC.Item[i].Value = "1110:LVR=2.6V";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "1111:LVR=2.7V";
            }

            //CLKSEL
//             temp = (pvalue0 >> 15) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[5].Value = CurLang.CLKSELMC32P7510String1;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[5].Value = CurLang.CLKSELMC32P7510String0;
//             }

            //TADJH
//             temp = (pvalue2 >> 0) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[6].Value = "1";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[6].Value = "0";
//             }

            //VDSEL
//             temp = (pvalue2 >> 1) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[7].Value = "1:1.7V";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[7].Value = "0:1.4V";
//             }

            i++;
            //DSEL
            temp = (pvalue2 >> 2) & 3;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "00:125mA";
            }
            else if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = "01:250mA";
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[i].Value = "10:375mA";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "11:500mA";
            }

//             i++;
//             //ISEL
//             temp = (pvalue2 >> 4) & 3;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[i].Value = "00:6ua";
//             }
//             else if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[i].Value = "01:3ua";
//             }
//             else if (temp == 2)
//             {
//                 propertyGridEx_PC.Item[i].Value = "10:1.5ua";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[i].Value = "11:0.75ua";
//             }

//             i++;
//             //PSEL
//             temp = (pvalue2 >> 6) & 3;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[i].Value = "00:10ns";
//             }
//             else if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[i].Value = "01:1us";
//             }
//             else if (temp == 2)
//             {
//                 propertyGridEx_PC.Item[i].Value = "10:2us";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[i].Value = "11:500ns";
//             }

            i++;
            //FAS
            temp = (pvalue2 >> 8) & 7;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = "000:16M";
                frmMAIN.FREQ = 0x16;
            }
            else if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = "001:8M";
                frmMAIN.FREQ = 0x08;
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[i].Value = "010:4M";
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == 3)
            {
                propertyGridEx_PC.Item[i].Value = "011:2M";
                frmMAIN.FREQ = 0x02;
            }
            else if (temp == 4)
            {
                propertyGridEx_PC.Item[i].Value = "100:1M";
                frmMAIN.FREQ = 0x01;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "101:455K";
                frmMAIN.FREQ = 0x45;
            }

            //FDS
//             temp = (pvalue2 >> 11) & 3;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[12].Value = "00:FAS/8";
//             }
//             else if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[12].Value = "01:FAS/4";
//             }
//             else if (temp == 2)
//             {
//                 propertyGridEx_PC.Item[12].Value = "10:FAS/2";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[12].Value = "11:FAS";
//             }

            //RCSMTB
//             temp = (pvalue2 >> 13) & 1;
//             if (temp == 1)
//             {
//                 propertyGridEx_PC.Item[13].Value = CurLang.RCSMTBMC32P7022String2;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[13].Value = CurLang.RCSMTBMC32P7022String1;
//             }

            i++;
            //LVDPD
            temp = (pvalue2 >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVDPDMC32P5212String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVDPDMC32P5212String0;
            }


        }
        private void MC32P5212_Option2Value()
        {
            Int32 optValue_Temp0, optValue_Temp1, optValue_Temp2;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = 0;
            int i = 0;

            string temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 0);
            }
            //case "1x:始终开看门狗":
            //    optValue_Temp = optValue_Temp | (2 << 0);
            //    break;
            else
            {
                optValue_Temp0 = optValue_Temp0 | (3 << 0);
            }

            i++;
            //WDTT
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=16ms":
                    optValue_Temp0 = optValue_Temp0 | (0 << 2);
                    optValue_Temp0 = optValue_Temp0 | (0 << 13);
                    break;
                case "001:PWRT=TWDT=64ms":
                    optValue_Temp0 = optValue_Temp0 | (1 << 2);
                    optValue_Temp0 = optValue_Temp0 | (0 << 13);
                    break;
                case "010:PWRT=TWDT=256ms":
                    optValue_Temp0 = optValue_Temp0 | (2 << 2);
                    optValue_Temp0 = optValue_Temp0 | (0 << 13);
                    break;
                case "011:PWRT=TWDT=1024ms":
                    optValue_Temp0 = optValue_Temp0 | (3 << 2);
                    optValue_Temp0 = optValue_Temp0 | (0 << 13);
                    break;
                case "100:PWRT=16ms;TWDT=2048ms":
                    optValue_Temp0 = optValue_Temp0 | (0 << 2);
                    optValue_Temp0 = optValue_Temp0 | (1 << 13);
                    break;
                case "101:PWRT=64ms;TWDT=4096ms":
                    optValue_Temp0 = optValue_Temp0 | (1 << 2);
                    optValue_Temp0 = optValue_Temp0 | (1 << 13);
                    break;
                case "110:PWRT=256ms;TWDT=8192ms":
                    optValue_Temp0 = optValue_Temp0 | (2 << 2);
                    optValue_Temp0 = optValue_Temp0 | (1 << 13);
                    break;
                default:
                    optValue_Temp0 = optValue_Temp0 | (3 << 2);
                    optValue_Temp0 = optValue_Temp0 | (1 << 13);
                    break;
            }

            i++;
            //fcpus
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//2":
                    optValue_Temp0 = optValue_Temp0 | (0 << 4);
                    break;
                case "001:Fosc//4":
                    optValue_Temp0 = optValue_Temp0 | (1 << 4);
                    break;
                case "010:Fosc//8":
                    optValue_Temp0 = optValue_Temp0 | (2 << 4);
                    break;
                case "011:Fosc//16":
                    optValue_Temp0 = optValue_Temp0 | (3 << 4);
                    break;
                case "100:Fosc//32":
                    optValue_Temp0 = optValue_Temp0 | (4 << 4);
                    break;
                case "101:Fosc//64":
                    optValue_Temp0 = optValue_Temp0 | (5 << 4);
                    break;
                case "110:Fosc//128":
                    optValue_Temp0 = optValue_Temp0 | (6 << 4);
                    break;
                default:
                    optValue_Temp0 = optValue_Temp0 | (7 << 4);
                    break;
            }

            i++;
            //mclre
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MCLREMC32P21String1)
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 7);
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 7);
            }

            i++;
            //ROTP
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.ROTPMC30P6060String1)
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 8);
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 8);
            }

            i++;
            //LVRS
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == "0000:LVR Close")
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
//             else if (temp == "0001:LVR=1.3V")
//             {
//                 optValue_Temp0 = optValue_Temp0 | (1 << 10);
//                 optValue_Temp0 = optValue_Temp0 | (0 << 14);
//             }
            else if (temp == "0010:LVR=1.4V")
            {
                optValue_Temp0 = optValue_Temp0 | (2 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "0011:LVR=1.5V")
            {
                optValue_Temp0 = optValue_Temp0 | (3 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "0100:LVR=1.6V")
            {
                optValue_Temp0 = optValue_Temp0 | (4 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "0101:LVR=1.7V")
            {
                optValue_Temp0 = optValue_Temp0 | (5 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "0110:LVR=1.8V")
            {
                optValue_Temp0 = optValue_Temp0 | (6 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "0111:LVR=1.9V")
            {
                optValue_Temp0 = optValue_Temp0 | (7 << 10);
                optValue_Temp0 = optValue_Temp0 | (0 << 14);
            }
            else if (temp == "1000:LVR=2.0V")
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1001:LVR=2.1V")
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1010:LVR=2.2V")
            {
                optValue_Temp0 = optValue_Temp0 | (2 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1011:LVR=2.3V")
            {
                optValue_Temp0 = optValue_Temp0 | (3 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1100:LVR=2.4V")
            {
                optValue_Temp0 = optValue_Temp0 | (4 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1101:LVR=2.5V")
            {
                optValue_Temp0 = optValue_Temp0 | (5 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else if (temp == "1110:LVR=2.6V")
            {
                optValue_Temp0 = optValue_Temp0 | (6 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (7 << 10);
                optValue_Temp0 = optValue_Temp0 | (1 << 14);
            }

            optValue_Temp0 = optValue_Temp0 | (1 << 15);
            //CLKSEL
//             temp = propertyGridEx_PC.Item[5].Value.ToString();
//             if (temp == CurLang.CLKSELMC32P7510String1)
//             {
//                 optValue_Temp0 = optValue_Temp0 | (1 << 15);
//             }
//             else
//             {
//                 optValue_Temp0 = optValue_Temp0 | (0 << 15);
//             }

            //TADJH
//             temp = propertyGridEx_PC.Item[6].Value.ToString();
//             if (temp == "1")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 0);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 0);
//             }

            //VDSEL
//             temp = propertyGridEx_PC.Item[7].Value.ToString();
//             if (temp == "1:1.7V")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 1);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 1);
//             }

            i++;
            //DSEL
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == "00:125mA")
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 2);
                optValue_Temp1 = optValue_Temp1 | (3 << 4);
            }
            else if (temp == "01:250mA")
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 2);
                optValue_Temp1 = optValue_Temp1 | (2 << 4);
            }
            else if (temp == "10:375mA")
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 2);
                optValue_Temp1 = optValue_Temp1 | (2 << 4);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 2);
                optValue_Temp1 = optValue_Temp1 | (1 << 4);
            }

//             i++;
//             //ISEL
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             if (temp == "00:6ua")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 4);
//             }
//             else if (temp == "01:3ua")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 4);
//             }
//             else if (temp == "10:1.5ua")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (2 << 4);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (3 << 4);
//             }

            optValue_Temp1 = optValue_Temp1 | (3 << 6);
//             i++;
//             //PSEL
//             temp = propertyGridEx_PC.Item[i].Value.ToString();
//             if (temp == "00:10ns")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 6);
//             }
//             else if (temp == "01:1us")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 6);
//             }
//             else if (temp == "10:2us")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (2 << 6);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (3 << 6);
//             }

            i++;
            //FAS
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == "000:16M")
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 8);
                frmMAIN.FREQ = 0x16;
            }
            else if (temp == "001:8M")
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 8);
                frmMAIN.FREQ = 0x08;
            }
            else if (temp == "010:4M")
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 8);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == "011:2M")
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 8);
                frmMAIN.FREQ = 0x02;
            }
            else if (temp == "100:1M")
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 8);
                frmMAIN.FREQ = 0x01;
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 8);
                frmMAIN.FREQ = 0x45;
            }

            //FDS
//             temp = propertyGridEx_PC.Item[12].Value.ToString();
//             if (temp == "00:FAS/8")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 11);
//             }
//             else if (temp == "01:FAS/4")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 11);
//             }
//             else if (temp == "10:FAS/2")
//             {
//                 optValue_Temp1 = optValue_Temp1 | (2 << 11);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (3 << 11);
//             }

            //RCSMTB
//             temp = propertyGridEx_PC.Item[13].Value.ToString();
//             if (temp == CurLang.RCSMTBMC32P7022String2)
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 13);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 13);
//             }

            i++;
            //LVDPD
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVDPDMC32P5212String1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 15);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 15);
            }

            optValue_Temp2 = optValue_Temp2 | (0x3f << 0);

            optionValue.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp2, 16);

        }

    }


}