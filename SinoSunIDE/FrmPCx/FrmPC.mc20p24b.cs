﻿using System;
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
        private void MC20P24B_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            //string[] borsel = new string[] { "LVR=2.1V","LVR=3.6"};
            string[] OSCM = new string[] { CurLang.OSCMMC20P24BString1, CurLang.OSCMMC20P24BString2, CurLang.OSCMMC20P24BString3, CurLang.OSCMMC20P24BString4 };
            string[] WDTE = new string[] { CurLang.WDTEMC20P24BString1, CurLang.WDTEMC20P24BString2 }; //0,1
            string[] RSTE = new string[] { CurLang.RSTEMC20P24BString1, CurLang.RSTEMC20P24BString2 }; //0,1
            string[] LVRS = new string[] { CurLang.LVRSMC20P24BString1, CurLang.LVRSMC20P24BString2 }; //0,1
            string[] LVRE = new string[] { CurLang.LVREMC20P24BString1, CurLang.LVREMC20P24BString2 }; //0,1
            string[] RES = new string[] { CurLang.RESMC20P24BString1, CurLang.RESMC20P24BString2 };//0,1
            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //0,1


            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC20P24BString1, false, CurLang.OSCMFunc, CurLang.OSCMMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            //propertyGridEx_PC.Item.Add("WDTE", "WDT时钟关闭", false, "看门狗时钟设置", "WDTE=False WDT时钟使能；\nWDTE=True WDT时钟关闭", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTE, true);
            propertyGridEx_PC.Item.Add("LVRE", CurLang.LVREMC20P24BString1, false, CurLang.LVREMC20P24BFunc, CurLang.LVREMC20P24BFuncExplain1+"\n"+CurLang.LVREMC20P24BFuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRE, true);

            propertyGridEx_PC.Item.Add("RSTE", CurLang.RSTEMC20P24BString1, false, CurLang.RSTEMC20P24BFunc, CurLang.RSTEMC20P24BFuncExplain1+"\n"+CurLang.RSTEMC20P24BFuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RSTE, true);

            propertyGridEx_PC.Item.Add("RES", CurLang.RESMC20P24BString1, false, CurLang.RESMC20P24BFunc, CurLang.RESMC20P24BFuncExplain1+"\n"+CurLang.RESMC20P24BFuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RES, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC20P24BString1, false, CurLang.LVRSMC20P24BFunc, CurLang.LVRSMC20P24BFuncExplain1+"\n"+CurLang.LVRSMC20P24BFuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);
        }


        private void MC20P24B_Option2Value()
        {
            int optValue_Temp = 0;
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            frmMAIN.FREQ = 0x08;
                //"11:8MHz 内部RC振荡器", "10:4MHz 内部RC振荡器", "01:2MHz 内部RC振荡器", "00:外接400K-8MHZ晶振" 
            if(temp == CurLang.OSCMMC20P24BString4)
            {
                optValue_Temp = optValue_Temp | (0 << 0);
                MessageBox.Show(CurLang.MessageBoxText24);
                frmMAIN.FREQ = 0x08;
            }
            else if(temp == CurLang.OSCMMC20P24BString3)
            {
                optValue_Temp = optValue_Temp | (1 << 0);
                frmMAIN.FREQ = 0x02;
            }
            else if(temp == CurLang.OSCMMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (2 << 0);
                frmMAIN.FREQ = 0x04;
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << 0);
                frmMAIN.FREQ = 0x08;
            }

            //WDTE
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.LVREMC20P24BString1)
            {
                optValue_Temp = optValue_Temp | (0 << 2);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 2);
            }

            //RST
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.RSTEMC20P24BString1)
            {
                optValue_Temp = optValue_Temp | (0 << 3);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 3);
            }

            //LVRS
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.RESMC20P24BString1)
            {
                optValue_Temp = optValue_Temp | (0 << 4);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 4);
            }

            //LVRE
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.LVRSMC20P24BString1)
            {
                optValue_Temp = optValue_Temp | (0 << 5);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 5);
            }

            ////REG
            //temp = propertyGridEx_PC.Item[5].Value.ToString();
            //if (temp == "P17上拉电阻无效")
            //{
            //    optValue_Temp = optValue_Temp | (0 << 6);
            //}
            //else
            //{
            //    optValue_Temp = optValue_Temp | (1 << 6);
            //}
            optValue_Temp = optValue_Temp | (1 << 6);
            //PROTECT
            temp = propertyGridEx_PC.Item[5].Value.ToString();
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

        private void MC20P24B_Value2Option(uint pValue)
        {
            uint temp = 0;

            temp = (pValue >> 0) & 3;

            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC20P24BString4;
                    frmMAIN.FREQ = 0x08;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC20P24BString3;
                    frmMAIN.FREQ = 0x02;
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC20P24BString2;
                    frmMAIN.FREQ = 0x04;
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMMC20P24BString1;
                    frmMAIN.FREQ = 0x08;
                    break;
            }
            //WDT
            temp = (pValue >> 2) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[1].Value = CurLang.LVREMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = CurLang.LVREMC20P24BString1;
            }

            //RSTE
            temp = (pValue >> 3) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[2].Value = CurLang.RSTEMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[2].Value = CurLang.RSTEMC20P24BString1;
            }
            //LVRS
            temp = (pValue >> 4) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.RESMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.RESMC20P24BString1;
            }


            //LVRE
            temp = (pValue >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[4].Value = CurLang.LVRSMC20P24BString1;
            }
            //REG
            //temp = (pValue >> 5) & 1;
            //if (temp == 1)
            //{
            //    propertyGridEx_PC.Item[4].Value = "P17上拉电阻有效";
            //}
            //else
            //{
            //    propertyGridEx_PC.Item[5].Value = "P17上拉电阻无效";
            //}

            //MCLRE
            temp = (pValue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[5].Value = CurLang.PROTECTMC20P24BString1;
            }

        }
    }
}