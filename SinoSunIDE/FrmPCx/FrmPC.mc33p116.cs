
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

        private void MC33P116_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };

            string[] WDTT = new string[] { "000:PWRT=TWDT=4ms", "001:PWRT=TWDT=16ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                            "100:PWRT=4ms;TWDT=512ms","101:PWRT=16ms;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256ms;TWDT=4096ms"};
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };

            string[] OSCM = new string[] { CurLang.OSCMMC34P01String2, CurLang.OSCMMC33P116String2 };

            string[] MCLRE = new string[] { CurLang.MCLREMC32P21String1, CurLang.MCLREMC32P21String2 };

            string[] LVRS = new string[] { CurLang.LVRSMC32P7022String1, CurLang.LVRSMC32P7022String2, CurLang.LVRSMC32P7022String3, CurLang.LVRSMC32P7022String4,
                                           CurLang.LVRSMC32P7022String5, CurLang.LVRSMC32P7022String6, CurLang.LVRSMC32P7022String7, CurLang.LVRSMC32P7022String8,
                                           CurLang.LVRSMC32P7022String9, CurLang.LVRSMC32P7022String10, CurLang.LVRSMC32P7022String11, CurLang.LVRSMC32P7022String12,
                                           CurLang.LVRSMC32P7022String13, CurLang.LVRSMC32P7022String14, CurLang.LVRSMC32P7022String15, CurLang.LVRSMC32P7022String16};

            string[] DSEL = new string[] { "00:125mA", "01:250mA", "10:375mA", "11:500mA" };

            string[] FAS = new string[] { "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4.5ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P21String1, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC34P01String2, false, CurLang.OSCMFunc, CurLang.OSCMMC33P116FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32P7022String1, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7022FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("DSEL", "00:125mA", false, CurLang.ISELMC31P11Func, CurLang.ISELMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(DSEL, true);

            propertyGridEx_PC.Item.Add("FAS", "001:8M", false, CurLang.FASMC32P7022Func, CurLang.FASMC32P7022FuncExplain + "\n" + CurLang.FASMC33P116FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

        }

        private void MC33P116_Value2Option(uint pvalue)
        {
            uint temp = (pvalue>>0) & 3;
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

            temp = (pvalue >>2) & 3;
            uint temp1 = ((pvalue >>13) & 1);
            temp1 = temp1 << 2;
            temp = temp1 | temp;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = "000:PWRT=TWDT=4ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = "001:PWRT=TWDT=16ms";
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
                    propertyGridEx_PC.Item[1].Value = "101:PWRT=16ms;TWDT=1024ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[1].Value = "110:PWRT=64ms;TWDT=2048ms";
                    break;
                default:
                    propertyGridEx_PC.Item[1].Value = "111:PWRT=256ms;TWDT=4096ms";
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
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String2;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P21String1;
            }

            //{ "0:内部高频", "1:外部晶振" };
            temp = (pvalue >> 8) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC33P116String2;
            }
            else
            {
                propertyGridEx_PC.Item[4].Value = CurLang.OSCMMC34P01String2;
            }

            temp = (pvalue >> 10) & 7;
            temp1 = ((pvalue >> 14) & 1);
            temp1 = temp1 << 3;
            temp = temp1 | temp;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String2;
                    break;
                case 2:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String3;
                    break;
                case 3:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String4;
                    break;
                case 4:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String5;
                    break;
                case 5:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String6;
                    break;
                case 6:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String7;
                    break;
                case 7:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String8;
                    break;
                case 8:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String9;
                    break;
                case 9:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String10;
                    break;
                case 10:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String11;
                    break;
                case 11:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String12;
                    break;
                case 12:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String13;
                    break;
                case 13:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String14;
                    break;
                case 14:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String15;
                    break;
                default:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7022String16;
                    break;
            }

            //DSEL
            temp = (pvalue >> (16 + 2)) & 3;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[6].Value = "00:125mA";
            }
            else if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = "01:250mA";
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[6].Value = "10:375mA";
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = "11:500mA";
            }

            //FAS
            temp = (pvalue >> (16 +8)) & 7;
            /*if (temp == 0)
            {
                propertyGridEx_PC.Item[7].Value = "000:16M";
                frmMAIN.FREQ = 0x16;
            }
            else */if (temp == 1)
            {
                propertyGridEx_PC.Item[7].Value = "001:8M";
                frmMAIN.FREQ = 0x08;
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[7].Value = "010:4M";
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == 3)
            {
                propertyGridEx_PC.Item[7].Value = "011:2M";
                frmMAIN.FREQ = 0x02;
            }
            else if (temp == 4)
            {
                propertyGridEx_PC.Item[7].Value = "100:1M";
                frmMAIN.FREQ = 0x01;
            }

            else
            {
                propertyGridEx_PC.Item[7].Value = "101:455K";
                frmMAIN.FREQ = 0x45;
            }
        }
        private void MC33P116_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();
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
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=4ms":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    optValue_Temp = optValue_Temp | (0 << 13);
                    break;
                case "001:PWRT=TWDT=16ms":
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
                case "101:PWRT=16ms;TWDT=1024ms":
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

            //fcpus
            temp = propertyGridEx_PC.Item[2].Value.ToString();
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

            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.OSCMMC34P01String2)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }

            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.LVRSMC32P7022String1)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P7022String2)
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

            //DSEL
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == "00:125mA")
            {
                optValue_Temp = optValue_Temp | (0 << (16 + 2));
                optValue_Temp = optValue_Temp | (3 << (16 + 4));
            }
            else if (temp == "01:250mA")
            {
                optValue_Temp = optValue_Temp | (1 << (16 + 2));
                optValue_Temp = optValue_Temp | (2 << (16 + 4));
            }
            else if (temp == "10:375mA")
            {
                optValue_Temp = optValue_Temp | (2 << (16 + 2));
                optValue_Temp = optValue_Temp | (2 << (16 + 4));
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << (16 + 2));
                optValue_Temp = optValue_Temp | (1 << (16 + 4));
            }

            optValue_Temp = optValue_Temp | (3 << (16 + 6));

            //FAS
            temp = propertyGridEx_PC.Item[7].Value.ToString();
            /*if (temp == "000:16M")
            {
                optValue_Temp = optValue_Temp | (0 << (16 + 8));
                frmMAIN.FREQ = 0x16;
            }
            else */if (temp == "001:8M")
            {
                optValue_Temp = optValue_Temp | (1 << (16 + 8));
                frmMAIN.FREQ = 0x08;
            }
            else if (temp == "010:4M")
            {
                optValue_Temp = optValue_Temp | (2 << (16 + 8));
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == "011:2M")
            {
                optValue_Temp = optValue_Temp | (3 << (16 + 8));
                frmMAIN.FREQ = 0x02;
            }
            else if (temp == "100:1M")
            {
                optValue_Temp = optValue_Temp | (4 << (16 + 8));
                frmMAIN.FREQ = 0x01;
            }
            else
            {
                optValue_Temp = optValue_Temp | (5 << (16 + 8));
                frmMAIN.FREQ = 0x45;
            }


            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

    }


}