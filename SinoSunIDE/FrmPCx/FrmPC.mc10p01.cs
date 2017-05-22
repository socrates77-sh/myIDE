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

        //protected bool bWDTE;
        //protected bool bRESE;
        //protected bool bLVRE;
        //protected bool bLVRS;
        //protected bool bRSTE;
        //protected bool bPROTECT;
        //protected bool bSMTEN;
        private void MC10P01_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            //string[] borsel = new string[] { "LVR=2.1V","LVR=3.6"};
            string[] OSCM = new string[] {CurLang.OSCMMC10P01String1,CurLang.OSCMMC10P01String2,CurLang.OSCMMC10P01String3,CurLang.OSCMMC10P01String4,
                     CurLang.OSCMMC10P01String5,CurLang.OSCMMC10P01String6,CurLang.OSCMMC10P01String7,CurLang.OSCMMC10P01String8};
            string[] RCEN = new string[] { CurLang.RCENMC10P01String1, CurLang.RCENMC10P01String2 }; //0,1
            string[] IROUT = new string[] { CurLang.IROUTMC10P01String1, CurLang.IROUTMC10P01String2 }; //0,1
            //string[] LVRS = new string[] { "LVR电压=2.1V", "LVR电压=3.6V" }; //0,1
            //string[] LVRE = new string[] { "LVR功能开启", "LVR功能关闭" }; //0,1
            string[] RES = new string[] { CurLang.RESMC10P01String1, CurLang.RESMC10P01String2 };//0,1
            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //0,1


            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC10P01String1, false, CurLang.OSCMMC10P01Func, CurLang.OSCMMC10P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("RCEN", CurLang.RCENMC10P01String1, false, CurLang.RCENMC10P01Func, CurLang.RCENMC10P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCEN, true);

            propertyGridEx_PC.Item.Add("IROUT", CurLang.IROUTMC10P01String1, false, CurLang.IROUTMC10P01Func, CurLang.IROUTMC10P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(IROUT, true);

            //propertyGridEx_PC.Item.Add("LVRS", "LVR电压=2.1V", false, "低压复位选择", "LVRS=False LVR电压=2.1V；\nLVRE=True LVR电压=3.6V", true);
           // propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            //propertyGridEx_PC.Item.Add("LVRE", "LVR功能开启", false, "低压复位使能设置", "LVRE=False LVR功能开启；\nLVRE=True LVR功能关闭", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRE, true);

            propertyGridEx_PC.Item.Add("RES", CurLang.RESMC10P01String1, false, CurLang.RESMC10P01Func, CurLang.RESMC10P01FuncExplain1+"\n"+CurLang.RESMC10P01FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RES, true);


            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);
        }


        private void MC10P01_Option2Value()
        {
            int optValue_Temp = 0;
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            frmMAIN.FREQ = 0x08;
            //"000:6分频，载波约为38K@Fosc=455KHz","001:36分频，载波约为56KHz@Fosc=4MHz","010:50分频，载波约为40KHz@Fosc=4MHz","011:53分频，载波约为38KHz@Fosc=4MHz",
            //     "100:56分频，载波约为36KHz@Fosc=4MHz","101:61分频，载波约为33KHz@Fosc=4MHz","110:64分频，载波约为31.5KHz@Fosc=4MHz","111:74分频，载波约为27KHz@Fosc=4MHz"};
            if (temp == CurLang.OSCMMC10P01String1)
            {
                optValue_Temp = optValue_Temp | (0 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String2)
            {
                optValue_Temp = optValue_Temp | (1 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String3)
            {
                optValue_Temp = optValue_Temp | (2 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String4)
            {
                optValue_Temp = optValue_Temp | (3 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String5)
            {
                optValue_Temp = optValue_Temp | (4 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String6)
            {
                optValue_Temp = optValue_Temp | (5 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else if (temp == CurLang.OSCMMC10P01String7)
            {
                optValue_Temp = optValue_Temp | (6 << 0);
                frmMAIN.FREQ = 0x04;
            }
            //case "111:74分频，载波约为27KHz@Fosc=4MHz":
            //    optValue_Temp = optValue_Temp | (2 << 0);
            //    frmMAIN.FREQ = 0x04;
            //    break;
            else
            {
                optValue_Temp = optValue_Temp | (7 << 0);
                frmMAIN.FREQ = 0x04;
            }

            //WDTE
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.RCENMC10P01String1)
            {
                optValue_Temp = optValue_Temp | (0 << 3);
                MessageBox.Show(CurLang.MessageBoxText22 + "4MHZ！");
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 3);
            }

            //RST
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.IROUTMC10P01String1)
            {
                optValue_Temp = optValue_Temp | (0 << 4);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 4);
            }

            ////REG
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.RESMC10P01String1)
            {
                optValue_Temp = optValue_Temp | (0 << 5);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 5);
            }


            optValue_Temp = optValue_Temp | (1 << 6);
            //PROTECT
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.PROTECTMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }


            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

        private void MC10P01_Value2Option(uint pValue)
        {
            uint temp = 0;

            temp = (pValue >> 0) & 7;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String1;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String2;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String3;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 3:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String4;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 4:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String5;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 5:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String6;
                    frmMAIN.FREQ = 0x04;
                    break;
                case 6:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String7;
                    frmMAIN.FREQ = 0x04;
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC10P01String8;
                    frmMAIN.FREQ = 0x04;
                    break;
            }
            //RCEN
            temp = (pValue >> 3) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[1].Value = CurLang.RCENMC10P01String2;
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = CurLang.RCENMC10P01String1;
            }

            //RSTE
            temp = (pValue >> 4) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[2].Value = CurLang.IROUTMC10P01String2;
            }
            else
            {
                propertyGridEx_PC.Item[2].Value = CurLang.IROUTMC10P01String1;
            }
            //LVRS
            temp = (pValue >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.RESMC10P01String2;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.RESMC10P01String1;
            }

            //MCLRE
            temp = (pValue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[4].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[4].Value = CurLang.PROTECTMC20P24BString1;
            }

        }
    }
}