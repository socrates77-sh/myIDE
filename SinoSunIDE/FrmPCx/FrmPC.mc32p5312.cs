
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

        private void MC32P5312_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };

            string[] FOSC = new string[] { CurLang.FOSCMC32P5312String1, CurLang.FOSCMC32P5312String2 };

            string[] LVRS = new string[] { CurLang.LVRSMC32P5312String1, CurLang.LVRSMC32P5312String2, CurLang.LVRSMC32P5312String3, CurLang.LVRSMC32P5312String4,
                                           CurLang.LVRSMC32P5312String5, CurLang.LVRSMC32P5312String6, CurLang.LVRSMC32P5312String7, CurLang.LVRSMC32P5312String8,
                                           CurLang.LVRSMC32P5312String9, CurLang.LVRSMC32P5312String10, CurLang.LVRSMC32P5312String11, CurLang.LVRSMC32P5312String12,
                                           CurLang.LVRSMC32P5312String13, CurLang.LVRSMC32P5312String14, CurLang.LVRSMC32P5312String15, CurLang.LVRSMC32P5312String16};

            string[] ENCR = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //1,0

            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };


            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("FOSC", CurLang.FOSCMC32P5312String1, false, CurLang.FOSCMC32P5312Func, CurLang.FOSCMC32P5312FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32P5312String1, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P5312FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("ENCR", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ENCR, true);

            propertyGridEx_PC.Item.Add("FAS", "000:16M", false, CurLang.OSCMFunc, CurLang.FASMC32P5312FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

        }

        private void MC32P5312_Value2Option(uint pvalue)
        {
            uint temp = (pvalue >> 4) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = "000:Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = "001:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = "010:Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[0].Value = "011:Fosc//16";
                    break;
                case 4:
                    propertyGridEx_PC.Item[0].Value = "100:Fosc//32";
                    break;
                case 5:
                    propertyGridEx_PC.Item[0].Value = "101:Fosc//64";
                    break;
                case 6:
                    propertyGridEx_PC.Item[0].Value = "110:Fosc//128";
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = "111:Fosc//256";
                    break;
            }

            temp = (pvalue >> 8) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[1].Value = CurLang.FOSCMC32P5312String1;
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = CurLang.FOSCMC32P5312String2;
            }

            //LVRS
            temp = (pvalue >> 10) & 7;
            uint temp1 = ((pvalue >> 14) & 1);
            temp1 = temp1 << 2;
            temp = temp1 | temp;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String2;
                    break;
                case 2:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String3;
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String4;
                    break;
                case 4:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String5;
                    break;
                case 5:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String6;
                    break;
                case 6:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String7;
                    break;
                case 7:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String8;
                    break;
                case 8:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String9;
                    break;
                case 9:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String10;
                    break;
                case 10:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String11;
                    break;
                case 11:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String12;
                    break;
                case 12:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String13;
                    break;
                case 13:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String14;
                    break;
                case 14:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String15;
                    break;
                default:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSMC32P5312String16;
                    break;
            }

            //ENCR
            temp = (pvalue >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.PROTECTMC20P24BString1;
            }

            temp = (pvalue >> 24) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = "000:16M";
                    frmMAIN.FREQ = 0x16;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = "001:8M";
                    frmMAIN.FREQ = 0x08;
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = "010:4M";
                    frmMAIN.FREQ = 0x04;
                    break;
                case 3:
                    propertyGridEx_PC.Item[4].Value = "011:2M";
                    frmMAIN.FREQ = 0x02;
                    break;
                case 4:
                    propertyGridEx_PC.Item[4].Value = "100:1M";
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = "101:455K";
                    frmMAIN.FREQ = 0x45;
                    break;
            }


        }
        private void MC32P5312_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();

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

            //FOSC
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.FOSCMC32P5312String1)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }

            //LVRS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.LVRSMC32P5312String1)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String2)
            {
                optValue_Temp = optValue_Temp | (1 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String3)
            {
                optValue_Temp = optValue_Temp | (2 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String4)
            {
                optValue_Temp = optValue_Temp | (3 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String5)
            {
                optValue_Temp = optValue_Temp | (4 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String6)
            {
                optValue_Temp = optValue_Temp | (5 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String7)
            {
                optValue_Temp = optValue_Temp | (6 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String8)
            {
                optValue_Temp = optValue_Temp | (7 << 10);
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String9)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String10)
            {
                optValue_Temp = optValue_Temp | (1 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String11)
            {
                optValue_Temp = optValue_Temp | (2 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String12)
            {
                optValue_Temp = optValue_Temp | (3 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String13)
            {
                optValue_Temp = optValue_Temp | (4 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String14)
            {
                optValue_Temp = optValue_Temp | (5 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else if (temp == CurLang.LVRSMC32P5312String15)
            {
                optValue_Temp = optValue_Temp | (6 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }
            else
            {
                optValue_Temp = optValue_Temp | (7 << 10);
                optValue_Temp = optValue_Temp | (1 << 14);
            }

            //ENCR
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.PROTECTMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (1 << 15);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 15);
            }

            temp = propertyGridEx_PC.Item[4].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp = optValue_Temp | (0 << 24);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp = optValue_Temp | (1 << 24);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp = optValue_Temp | (2 << 24);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp = optValue_Temp | (3 << 24);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp = optValue_Temp | (4 << 24);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp = optValue_Temp | (5 << 24);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            optValue_Temp = optValue_Temp | (3 << 27);


            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

    }


}