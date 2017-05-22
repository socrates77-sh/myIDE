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

        private void MC32P7011_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] MODE = new string[] { "10x:2K", "01x:1K" };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] FMODE = new string[] { CurLang.FMODE32P7011String1, CurLang.FMODE32P7011String2 };
            string[] FCPU = new string[] { "001:Fosc//2", "010:Fosc//4", "011:Fosc//8", "100:Fosc//16", "101:Fosc//32", "110:Fosc//64", "111:Fosc//128" };
            string[] MCLRE = new string[] { CurLang.MCLREMC30P6060String0, CurLang.MCLREMC30P6060String1 };
            string[] FOSC = new string[] { CurLang.FOSC32P7011String000, CurLang.FOSC32P7011String100, CurLang.FOSC32P7011String101, CurLang.FOSC32P7011String110, CurLang.FOSC32P7011String111 };
            string[] FIL = new string[] { CurLang.FILSMC30P6060String1, CurLang.FILSMC30P6060String0 };
            string[] CLKSEL = new string[] { CurLang.CLKSELMC32P7510String0, CurLang.CLKSELMC32P7510String1 };
            string[] VDSEL = new string[] { "0:1.4V", "1:1.7V" };
            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };
            string[] FDS = new string[] { "00:FAS/8", "01:FAS/4", "10:FAS/2", "11:FAS" };
            string[] RCSMTB = new string[] { CurLang.RCSMTBMC32P7022String1, CurLang.RCSMTBMC32P7022String2 }; //0,1

            string[] XTDRVB = new string[] { CurLang.XTDRVBMC30P6060String0, CurLang.XTDRVBMC30P6060String1 }; //1,0

//             propertyGridEx_PC.Item.Add("MODE", "10x:2K", false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

//             propertyGridEx_PC.Item.Add("FMODE", CurLang.FMODE32P7011String1, false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FMODE, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//128", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC30P6060String0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("FOSC", CurLang.FOSC32P7011String000, false, CurLang.OSCMFunc, CurLang.OSCMMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

//             propertyGridEx_PC.Item.Add("FIL", CurLang.FILSMC30P6060String1, false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FIL, true);

//             propertyGridEx_PC.Item.Add("CLKSEL", CurLang.CLKSELMC32P7510String1, false, CurLang.CLKSELMC32P7510Func, CurLang.CLKSELMC32P7510FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CLKSEL, true);

//             propertyGridEx_PC.Item.Add("VDSEL", "0:1.4V", false, "", CurLang.VDSELMC32P7022FuncExplain, true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("FAS", "000:16M", false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

//             propertyGridEx_PC.Item.Add("FDS", "00:FAS/8", false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FDS, true);
// 
//             propertyGridEx_PC.Item.Add("RCSMTB", CurLang.RCSMTBMC32P7022String1, false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCSMTB, true);
// 
//             propertyGridEx_PC.Item.Add("XTDRVB", CurLang.XTDRVBMC30P6060String0, false, "", "", true);
//             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(XTDRVB, true);

        }

        private void MC32P7011_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            int i = 0;
            uint temp = pValue0 & 7;
//             if (temp == 4)
//             {
//                 propertyGridEx_PC.Item[0].Value = "10x:2K";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[0].Value = "01x:1K";
//             }

            temp = pValue1 & 3;
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

//             i++;
//             temp = (pValue1 >> 2) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[i].Value = CurLang.FMODE32P7011String1;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[i].Value = CurLang.FMODE32P7011String2;
//             }

            i++;
            temp = (pValue1 >> 4) & 7;
            switch (temp)
            {
//                 case 0:
//                     propertyGridEx_PC.Item[3].Value = "000:Fosc//1";
//                     break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:Fosc//2";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:Fosc//4";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:Fosc//8";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:Fosc//16";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:Fosc//32";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:Fosc//64";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:Fosc//128";
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

            i++;
            temp = (pValue1 >> 8) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String000;
                    break;
//                 case 2:
//                     propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String010;
//                     break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String110;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.FOSC32P7011String111;
                    break;
            }

//             temp = (pValue1 >> 14) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[6].Value = CurLang.FILSMC30P6060String1;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[6].Value = CurLang.FILSMC30P6060String0;
//             }

//             temp = (pValue1 >> 15) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[7].Value = CurLang.CLKSELMC32P7510String0;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[7].Value = CurLang.CLKSELMC32P7510String1;
//             }

//             temp = pValue3 & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[8].Value = "0:1.4V";
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[8].Value = "1:1.7V";
//             }

            i++;
            temp = (pValue3 >> 1) & 7;
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

//             temp = (pValue3 >> 4) & 3;
//             switch (temp)
//             {
//                 case 0:
//                     propertyGridEx_PC.Item[10].Value = "00:FAS/8";
//                     break;
//                 case 1:
//                     propertyGridEx_PC.Item[10].Value = "01:FAS/4";
//                     break;
//                 case 2:
//                     propertyGridEx_PC.Item[10].Value = "10:FAS/2";
//                     break;
//                 default:
//                     propertyGridEx_PC.Item[10].Value = "11:FAS";
//                     break;
//             }

//             temp = (pValue3 >> 6) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[11].Value = CurLang.RCSMTBMC32P7022String1;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[11].Value = CurLang.RCSMTBMC32P7022String2;
//             }

//             temp = (pValue3 >> 7) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[12].Value = CurLang.XTDRVBMC30P6060String0;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[12].Value = CurLang.XTDRVBMC30P6060String1;
//             }
        }
        private void MC32P7011_Option2Value()
        {
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3, optValue_Temp4;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = optValue_Temp3 = optValue_Temp4 = 0;
            int i;

            //MODE
            optValue_Temp0 = optValue_Temp0 | (3 << 0);
//             string temp = propertyGridEx_PC.Item[0].Value.ToString();
//             if (temp == "10x:2K")
//             {
//                 optValue_Temp0 = optValue_Temp0 | (4 << 0);
//             }
//             else
//             {
//                 optValue_Temp0 = optValue_Temp0 | (3 << 0);
//             }

            //WDTC
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }

            //FMODE
            optValue_Temp1 = optValue_Temp1 | (1 << 2);
//             temp = propertyGridEx_PC.Item[2].Value.ToString();
//             if (temp == CurLang.FMODE32P7011String1)
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 2);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 2);
//             }

            //FCPUS
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//1":
                    optValue_Temp1 = optValue_Temp1 | (0 << 4);
                    break;
                case "001:Fosc//2":
                    optValue_Temp1 = optValue_Temp1 | (1 << 4);
                    break;
                case "010:Fosc//4":
                    optValue_Temp1 = optValue_Temp1 | (2 << 4);
                    break;
                case "011:Fosc//8":
                    optValue_Temp1 = optValue_Temp1 | (3 << 4);
                    break;
                case "100:Fosc//16":
                    optValue_Temp1 = optValue_Temp1 | (4 << 4);
                    break;
                case "101:Fosc//32":
                    optValue_Temp1 = optValue_Temp1 | (5 << 4);
                    break;
                case "110:Fosc//64":
                    optValue_Temp1 = optValue_Temp1 | (6 << 4);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (7 << 4);
                    break;
            }

            //mclre
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.MCLREMC30P6060String0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 7);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 7);
            }

            //FOSC
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.FOSC32P7011String000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 8);
            }
//             else if (temp == CurLang.FOSC32P7011String010)
//             {
//                 optValue_Temp1 = optValue_Temp1 | (2 << 8);
//             }
            else if (temp == CurLang.FOSC32P7011String100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 8);
            }
            else if (temp == CurLang.FOSC32P7011String101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 8);
            }
            else if (temp == CurLang.FOSC32P7011String110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 8);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 8);
            }

            i = (optValue_Temp1 >> 8) & 7;
            if ( i== 0)
                propertyGridEx_PC.Item[4].Visible = true;
            else if(i == 2)
                propertyGridEx_PC.Item[4].Visible = true;
            else
                propertyGridEx_PC.Item[4].Visible = false;
            propertyGridEx_PC.Refresh();

            //FIL
            optValue_Temp1 = optValue_Temp1 | (1 << 14);
//             temp = propertyGridEx_PC.Item[6].Value.ToString();
//             if (temp == CurLang.FILSMC30P6060String1)
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 14);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 14);
//             }

            //CLKSEL
            optValue_Temp1 = optValue_Temp1 | (1 << 15);
//             temp = propertyGridEx_PC.Item[7].Value.ToString();
//             if (temp == CurLang.CLKSELMC32P7510String0)
//             {
//                 optValue_Temp1 = optValue_Temp1 | (0 << 15);
//             }
//             else
//             {
//                 optValue_Temp1 = optValue_Temp1 | (1 << 15);
//             }

            //VDSEL
            optValue_Temp3 = optValue_Temp3 | (0 << 0);
//             temp = propertyGridEx_PC.Item[8].Value.ToString();
//             if (temp == "0:1.4V")
//             {
//                 optValue_Temp3 = optValue_Temp3 | (0 << 0);
//             }
//             else
//             {
//                 optValue_Temp3 = optValue_Temp3 | (1 << 0);
//             }

            //FAS
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp3 = optValue_Temp3 | (0 << 1);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp3 = optValue_Temp3 | (1 << 1);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp3 = optValue_Temp3 | (2 << 1);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp3 = optValue_Temp3 | (3 << 1);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp3 = optValue_Temp3 | (4 << 1);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp3 = optValue_Temp3 | (5 << 1);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            //FDS
            optValue_Temp3 = optValue_Temp3 | (3 << 4);
//             temp = propertyGridEx_PC.Item[10].Value.ToString();
//             switch (temp)
//             {
//                 case "00:FAS/8":
//                     optValue_Temp3 = optValue_Temp3 | (0 << 4);
//                     break;
//                 case "01:FAS/4":
//                     optValue_Temp3 = optValue_Temp3 | (1 << 4);
//                     break;
//                 case "10:FAS/2":
//                     optValue_Temp3 = optValue_Temp3 | (2 << 4);
//                     break;
//                 default:
//                     optValue_Temp3 = optValue_Temp3 | (3 << 4);
//                     break;
//             }

            //RCSMTB
            optValue_Temp3 = optValue_Temp3 | (0 << 6);
//             temp = propertyGridEx_PC.Item[11].Value.ToString();
//             if (temp == CurLang.RCSMTBMC32P7022String1)
//             {
//                 optValue_Temp3 = optValue_Temp3 | (0 << 6);
//             }
//             else
//             {
//                 optValue_Temp3 = optValue_Temp3 | (1 << 6);
//             }

            //XTDRVB
            optValue_Temp3 = optValue_Temp3 | (1 << 7);
//             temp = propertyGridEx_PC.Item[12].Value.ToString();
//             if (temp == CurLang.XTDRVBMC30P6060String0)
//             {
//                 optValue_Temp3 = optValue_Temp3 | (0 << 7);
//             }
//             else
//             {
//                 optValue_Temp3 = optValue_Temp3 | (1 << 7);
//             }

            optValue_Temp4 = 0x80;

            optionValue.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);

        }

    }


}