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
        private void MC10P11_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            //string[] borsel = new string[] { "LVR=2.1V","LVR=3.6"};
            string[] PROTECT = new string[] { CurLang.PROTECTMC20P24BString1, CurLang.PROTECTMC20P24BString2 }; //0,1


            propertyGridEx_PC.Item.Add("PROTECT", CurLang.PROTECTMC20P24BString1, false, CurLang.PROTECTMC20P24BFunc, CurLang.PROTECTMC20P24BFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(PROTECT, true);
        }


        private void MC10P11_Option2Value()
        {
            int optValue_Temp = 0;

            frmMAIN.FREQ = 0x04;
            //string temp = propertyGridEx_PC.Item[0].Value.ToString();

            optValue_Temp = optValue_Temp | (0x7f);
            //PROTECT
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
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

        private void MC10P11_Value2Option(uint pValue)
        {
            uint temp = 0;


           frmMAIN.FREQ = 0x04;

            //MCLRE
            temp = (pValue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[0].Value = CurLang.PROTECTMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[0].Value = CurLang.PROTECTMC20P24BString1;
            }

        }
    }
}