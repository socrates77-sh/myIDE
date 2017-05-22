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

        private void MC32F7122_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] FCPU = new string[] {CurLang.FCPU_000,CurLang.FCPU_001,CurLang.FCPU_010,CurLang.FCPU_011,CurLang.FCPU_100,CurLang.FCPU_101,
                                            CurLang.FCPU_110,CurLang.FCPU_111};
            string[] RSTEN = new string[] { CurLang.RSTEN_0, CurLang.RSTEN_1 };
            string[] WDTM = new string[] { CurLang.WDT_OFF, CurLang.WDT_OFF_STOP, CurLang.WDT_ON };
            //string[] WDTT = new string[] { CurLang.WDTT_000,CurLang.WDTT_001,CurLang.WDTT_010,CurLang.WDTT_011,CurLang.WDTT_100,CurLang.WDTT_101,CurLang.WDTT_110,
            //                                CurLang.WDTT_111};
            string[] ENCR = new string[] { CurLang.ENCR_0, CurLang.ENCR_1 };
            string[] LVRSLP = new string[] { CurLang.LVRSLP_0, CurLang.LVRSLP_1 };
            string[] LVRS = new string[] { CurLang.LVR_7122_000, CurLang.LVR_7122_001, CurLang.LVR_7122_010, CurLang.LVR_7122_011, CurLang.LVR_7122_100 };

            //propertyGridEx_PC.Item.Add("OSCCAL","0x0100",false,);
            propertyGridEx_PC.Item.Add("FCPU", CurLang.FCPU_000, false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("RSTEN", CurLang.RSTEN_0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RSTEN, true);

            propertyGridEx_PC.Item.Add("WDTM", CurLang.WDT_OFF, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTM, true);

            //propertyGridEx_PC.Item.Add("WDTT", CurLang.WDTT_000, false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("ENCR", CurLang.ENCR_0, false, CurLang.CENCRMC32E22Func, CurLang.ENCRMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ENCR, true);

            propertyGridEx_PC.Item.Add("LVRSLP", CurLang.LVRSLP_0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRSLP, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVR_7122_000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7212FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

        }


        private void MC32F7122_Option2Value()
        {
            //int copbit;
            //int dopbit0,dopbit1,dopbit2,dopbit3,dopbit4,dopbit5;

            //int uopbit0~uopbit5
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3;
            optValue_Temp0 = 0;
            optValue_Temp1 = 0;
            optValue_Temp2 = optValue_Temp3 = 0;

            //OPBIT1
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            if (temp == CurLang.FCPU_000)
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 4);
            }
            else if (temp == CurLang.FCPU_001)
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 4);
            }
            else if (temp == CurLang.FCPU_010)
            {
                optValue_Temp0 = optValue_Temp0 | (2 << 4);
            }
            else if (temp == CurLang.FCPU_011)
            {
                optValue_Temp0 = optValue_Temp0 | (3 << 4);
            }
            else if (temp == CurLang.FCPU_100)
            {
                optValue_Temp0 = optValue_Temp0 | (4 << 4);
            }
            else if (temp == CurLang.FCPU_101)
            {
                optValue_Temp0 = optValue_Temp0 | (5 << 4);
            }
            else if (temp == CurLang.FCPU_110)
            {
                optValue_Temp0 = optValue_Temp0 | (6 << 4);
            }
            else if (temp == CurLang.FCPU_111)
            {
                optValue_Temp0 = optValue_Temp0 | (7 << 4);
            }

            //OPBIT2 RSTEN
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.RSTEN_0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 7);
            }
            else if (temp == CurLang.RSTEN_1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 7);
            }

            //WDTM
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.WDT_OFF)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 4);
            }
            else if (temp == CurLang.WDT_OFF_STOP)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 4);
            }
            else if (temp == CurLang.WDT_ON)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 4);
            }

            //WDTT
            /*temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.WDTT_000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.WDTT_001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else if (temp == CurLang.WDTT_010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 0);
            }
            else if (temp == CurLang.WDTT_011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }
            else if (temp == CurLang.WDTT_100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 0);
            }
            else if (temp == CurLang.WDTT_101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 0);
            }
            else if (temp == CurLang.WDTT_110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 0);
            }
            else if (temp == CurLang.WDTT_111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 0);
            }*/

            //opbit3 bit[7] 固定为1
            optValue_Temp2 = optValue_Temp2 | (1 << 7);

            //ENCR
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.ENCR_0)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 5);
            }
            else if (temp == CurLang.ENCR_1)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 5);
            }

            //LVRSLP
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.LVRSLP_0)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 4);
            }
            else if (temp == CurLang.LVRSLP_1)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 4);
            }

            // VLVRS
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.LVR_7122_000)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 0);
            }
            else if (temp == CurLang.LVR_7122_001)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 0);
            }
            else if (temp == CurLang.LVR_7122_010)
            {
                optValue_Temp2 = optValue_Temp2 | (2 << 0);
            }
            else if (temp == CurLang.LVR_7122_011)
            {
                optValue_Temp2 = optValue_Temp2 | (3 << 0);
            }
            else if (temp == CurLang.LVR_7122_100)
            {
                optValue_Temp2 = optValue_Temp2 | (4 << 0);
            }


            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp2, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);

        }

        private void MC32F7122_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            frmMAIN.FREQ = 0x8;

            //FCPUS
            uint temp = ((pValue0 >> 4) & 7);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[0].Value = CurLang.FCPU_111;
                    break;
            }

            //RSTEN
            temp = ((pValue1 >> 7) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = CurLang.RSTEN_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = CurLang.RSTEN_1;
                    break;
            }



            //WDTM
            temp = ((pValue1 >> 4) & 3);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = CurLang.WDT_OFF;
                    break;
                case 1:
                    propertyGridEx_PC.Item[2].Value = CurLang.WDT_OFF_STOP;
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = CurLang.WDT_ON;
                    break;
            }

            //WDTT
            /*temp = pValue1 & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[3].Value = CurLang.WDTT_111;
                    break;
            }*/

            //ENCR
            temp = ((pValue2 >> 5) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[3].Value = CurLang.ENCR_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[3].Value = CurLang.ENCR_1;
                    break;
            }

            //LVRSLP
            temp = ((pValue2 >> 4) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSLP_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.LVRSLP_1;
                    break;
            }

            //VLVRS
            temp = (pValue2 & 15);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVR_7122_000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVR_7122_001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVR_7122_010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVR_7122_011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVR_7122_100;
                    break;
            }



        }
    }
}