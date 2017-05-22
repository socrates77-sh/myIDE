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
        private void MC20P38_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            //string[] borsel = new string[] { "LVR=2.1V","LVR=3.6"};
            //string[] OSCM = new string[] { "11:8MHz 内部RC振荡器", "10:4MHz 内部RC振荡器", "01:外接400K-8MHZ晶振", "00:2MHz 内部RC振荡器" };
            string[] WDTE = new string[] { CurLang.WDTEMC20P24BString2, CurLang.WDTEMC20P24BString1 }; //0,1
            string[] RSTE = new string[] { CurLang.RSTEMC20P38String1, CurLang.RSTEMC20P38String2 }; //0,1
           // string[] LVRS = new string[] { "LVR电压=2.1V", "LVR电压=3.6V" }; //0,1
            string[] LVRE = new string[] { CurLang.LVREMC20P24BString2, CurLang.LVREMC20P24BString1 }; //0,1
            //string[] RES = new string[] { "PC1上拉电阻无效", "P17上拉电阻有效" };//0,1
            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //0,1


            //propertyGridEx_PC.Item.Add("OSCM", "11:8MHz 内部RC振荡器", false, "振荡器选择", "内部IRC 8MHZ,4MHZ,2MHZ 和 外部晶振", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);



            propertyGridEx_PC.Item.Add("RSTE", CurLang.RSTEMC20P38String1, false, CurLang.RSTEMC20P24BFunc, CurLang.RSTEMC20P38FuncExplain1+"\n"+CurLang.RSTEMC20P38FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RSTE, true);

            propertyGridEx_PC.Item.Add("LVRE", CurLang.LVREMC20P24BString2, false, CurLang.LVREMC20P24BFunc, CurLang.LVREMC20P01FuncExplain1+"\n"+CurLang.LVREMC20P01FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRE, true);


            propertyGridEx_PC.Item.Add("WDTE", CurLang.WDTEMC20P24BString2, false, CurLang.WDTEMC20P01Func, CurLang.WDTEMC20P04FuncExplain1+"\n"+CurLang.WDTEMC20P04FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTE, true);
           
            //propertyGridEx_PC.Item.Add("LVRS", "LVR电压=2.1V", false, "低压复位选择", "LVRS=False LVR电压=2.1V；\nLVRE=True LVR电压=3.6V", true);
           // propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);


            //propertyGridEx_PC.Item.Add("RES", "P17上拉电阻无效", false, "P17上拉电阻设置", "RES=False P17上拉电阻无效；\nLVRE=True P17上拉电阻有效", true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RES, true);


            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);
        }


        private void MC20P38_Option2Value()
        {
            int optValue_Temp = 0;
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            frmMAIN.FREQ = 0x08;

            //RST
            //temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.RSTEMC20P38String1)
            {
                optValue_Temp = optValue_Temp | (0 << 4);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 4);
            }

            //LVRE
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.LVREMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (0 << 5);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 5);
            }

            //WDTE
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.WDTEMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (0 << 6);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 6);
            }

            //PROTECT
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.PROTECTMC20P24BString2)
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }

            optValue_Temp = optValue_Temp | (0x0f << 0);// low 4 bits

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

        private void MC20P38_Value2Option(uint pValue)
        {
            uint temp = 0;

           // temp = (pValue >> 0) & 3;
            frmMAIN.FREQ = 0x08;

            //RSTE
            temp = (pValue >> 4) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[0].Value = CurLang.RSTEMC20P38String2;
            }
            else
            {
                propertyGridEx_PC.Item[0].Value = CurLang.RSTEMC20P38String1;
            }


            //LVRE
            temp = (pValue >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[1].Value = CurLang.LVREMC20P24BString1;
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = CurLang.LVREMC20P24BString2;
            }

            //WDT
            temp = (pValue >> 6) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[2].Value = CurLang.WDTEMC20P24BString1;
            }
            else
            {
                propertyGridEx_PC.Item[2].Value = CurLang.WDTEMC20P24BString2;
            }


            //MCLRE
            temp = (pValue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.PROTECTMC20P24BString1;
            }

        }
    }
}