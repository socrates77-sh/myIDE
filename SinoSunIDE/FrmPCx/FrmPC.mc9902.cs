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

        private void MC9902_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] RCSMTB = new string[] {CurLang.RCSMTB_0,CurLang.RCSMTB_1};
            string[] VDSEL = new string[] { CurLang.VDSEL_0, CurLang.VDSEL_1 };
            string[] WDTC = new string[] { CurLang.WDT_OFF, CurLang.WDT_OFF_STOP, CurLang.WDT_ON };
            string[] WDTT = new string[] { CurLang.WDTT_000,CurLang.WDTT_001,CurLang.WDTT_010,CurLang.WDTT_011,CurLang.WDTT_100,CurLang.WDTT_101,CurLang.WDTT_110,
                                            CurLang.WDTT_111};
            string[] FCPU = new string[] {CurLang.FCPU_000,CurLang.FCPU_001,CurLang.FCPU_010,CurLang.FCPU_011,CurLang.FCPU_100,CurLang.FCPU_101,
                                            CurLang.FCPU_110,CurLang.FCPU_111};
 
            string[] MCLRE = new string[] { CurLang.MCLRE_0, CurLang.MCLRE_1 };

            string[] SPDS = new string[] { CurLang.SPDS_0,CurLang.SPDS_1};
            string[] RESSEL = new string[] { CurLang.RESSEL_0, CurLang.RESSEL_1 }; 
            string[] FAS = new string[] { CurLang.FAS_000, CurLang.FAS_001, CurLang.FAS_010, CurLang.FAS_011, CurLang.FAS_100, CurLang.FAS_101 };
            string[] LVRS = new string[] { CurLang.LVR_0000,CurLang.LVR_0001,CurLang.LVR_0010,CurLang.LVR_0011,CurLang.LVR_0100,CurLang.LVR_0101,
                                            CurLang.LVR_0110,CurLang.LVR_0111,CurLang.LVR_1000,CurLang.LVR_1001,CurLang.LVR_1010,CurLang.LVR_1011,
                                            CurLang.LVR_1100,CurLang.LVR_1101,CurLang.LVR_1110,CurLang.LVR_1111};
            string[] ENCR = new string[] { CurLang.ENCR_0, CurLang.ENCR_1 }; 

            //propertyGridEx_PC.Item.Add("OSCCAL","0x0100",false,);
            propertyGridEx_PC.Item.Add("RCSMTB", CurLang.RCSMTB_0, false, CurLang.RCSMTBMC30P6060Func, CurLang.RCSMTBMC30P6060FuncExplain,true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCSMTB, true);

            propertyGridEx_PC.Item.Add("VDSEL", CurLang.VDSEL_0, false, CurLang.VDSELMC30P6060Func, CurLang.VDSELMC30P6060FuncExplain,true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDT_OFF, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", CurLang.WDTT_000, false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", CurLang.FCPU_000, false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLRE_0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("SPDS", CurLang.SPDS_0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SPDS, true);

            propertyGridEx_PC.Item.Add("RESSEL", CurLang.RESSEL_0, false, CurLang.RESSELMC30P6060Func, CurLang.RESSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RESSEL, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVR_0000,false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7212FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("FAS", CurLang.FAS_000, false, CurLang.FOSCMC32E22Func, CurLang.FOSCMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

            propertyGridEx_PC.Item.Add("ENCR", CurLang.ENCR_0, false, CurLang.CENCRMC32E22Func, CurLang.ENCRMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ENCR, true);

        }


        private void MC9902_Option2Value()
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
            if(temp==CurLang.RCSMTB_1)
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 11);
            }

            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp==CurLang.VDSEL_1)
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 12);
            }

            //OPBIT2 wdtc
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.WDT_OFF)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.WDT_OFF_STOP)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else if (temp == CurLang.WDT_ON)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }

            //WDTT
            temp = propertyGridEx_PC.Item[3].Value.ToString();            
            if(temp==CurLang.WDTT_000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 2);
            }
            else if (temp == CurLang.WDTT_001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 2);
            }
            else if (temp == CurLang.WDTT_010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 2);
            }
            else if (temp == CurLang.WDTT_011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 2);
            }
            else if (temp == CurLang.WDTT_100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 2);
            }
            else if (temp ==CurLang.WDTT_101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 2);
            }
            else if (temp == CurLang.WDTT_110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 2);
            }
            else if (temp == CurLang.WDTT_111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 2);
            }             
            
            //FCPUS
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.FCPU_000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 5);
            }
            else if (temp == CurLang.FCPU_001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 5);
            }
            else if (temp == CurLang.FCPU_010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 5);
            }
            else if (temp == CurLang.FCPU_011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 5);
            }
            else if (temp == CurLang.FCPU_100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 5);
            }
            else if (temp == CurLang.FCPU_101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 5);
            }
            else if (temp == CurLang.FCPU_110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 5);
            }
            else if (temp == CurLang.FCPU_111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
            }
            

            //MCLRE
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.MCLRE_0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 8);
            }
            else if (temp == CurLang.MCLRE_1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 8);
            }

            //SPDS
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == CurLang.SPDS_1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 10);
            }

            //RESSEL
            temp = propertyGridEx_PC.Item[7].Value.ToString();
            if (temp == CurLang.RESSEL_1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 11);
            }
          
            //OPBIT3 VLVRS
            temp = propertyGridEx_PC.Item[8].Value.ToString();
            if (temp == CurLang.LVR_0000)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 0);
            }
            else if (temp == CurLang.LVR_0001)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 0);
            }
            else if (temp == CurLang.LVR_0010)
            {
                optValue_Temp2 = optValue_Temp2 | (2 << 0);
            }
            else if (temp == CurLang.LVR_0011)
            {
                optValue_Temp2 = optValue_Temp2 | (3 << 0);
            }
            else if (temp == CurLang.LVR_0100)
            {
                optValue_Temp2 = optValue_Temp2 | (4 << 0);
            }
            else if (temp == CurLang.LVR_0101)
            {
                optValue_Temp2 = optValue_Temp2 | (5 << 0);
            }
            else if (temp == CurLang.LVR_0110)
            {
                optValue_Temp2 = optValue_Temp2 | (6 << 0);
            }
            else if (temp == CurLang.LVR_0111)
            {
                optValue_Temp2 = optValue_Temp2 | (7 << 0);
            }
            else if (temp == CurLang.LVR_1000)
            {
                optValue_Temp2 = optValue_Temp2 | (8 << 0);
            }
            else if (temp == CurLang.LVR_1001)
            {
                optValue_Temp2 = optValue_Temp2 | (9 << 0);
            }
            else if (temp == CurLang.LVR_1010)
            {
                optValue_Temp2 = optValue_Temp2 | (10 << 0);
            }
            else if (temp == CurLang.LVR_1011)
            {
                optValue_Temp2 = optValue_Temp2 | (11 << 0);
            }
            else if (temp == CurLang.LVR_1100)
            {
                optValue_Temp2 = optValue_Temp2 | (12 << 0);
            }
            else if (temp == CurLang.LVR_1101)
            {
                optValue_Temp2 = optValue_Temp2 | (13 << 0);
            }
            else if (temp == CurLang.LVR_1110)
            {
                optValue_Temp2 = optValue_Temp2 | (14 << 0);
            }
            else if (temp == CurLang.LVR_1111)
            {
                optValue_Temp2 = optValue_Temp2 | (15 << 0);
            }


            //FAS
            temp = propertyGridEx_PC.Item[9].Value.ToString();
            if (temp == CurLang.FAS_000)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 4);
            }
            else if (temp == CurLang.FAS_001)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 4);
            }
            else if (temp == CurLang.FAS_010)
            {
                optValue_Temp2 = optValue_Temp2 | (2 << 4);
            }
            else if (temp == CurLang.FAS_011)
            {
                optValue_Temp2 = optValue_Temp2 | (3 << 4);
            }
            else if (temp == CurLang.FAS_100)
            {
                optValue_Temp2 = optValue_Temp2 | (4 << 4);
            }
            else if (temp == CurLang.FAS_101)
            {
                optValue_Temp2 = optValue_Temp2 | (5 << 4);
            } 

            //ENCR
            temp = propertyGridEx_PC.Item[10].Value.ToString();
            if (temp == CurLang.ENCR_0)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 13);
            }
            else if (temp == CurLang.ENCR_1)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 13);
            }

            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp2, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);

        }

        private void MC9902_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            frmMAIN.FREQ = 0x8;

            //RCSMTB
            uint temp = ((pValue0 >> 11) & 1);
            switch  (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.RCSMTB_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.RCSMTB_1;
                    break;
            }

            //VDSEL
            temp = ((pValue0 >> 12) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = CurLang.VDSEL_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = CurLang.VDSEL_1;
                    break;
            }

            //WDTC
            temp = pValue1 & 3;
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
            temp = ((pValue1>>2) & 7);
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
            }

            //FCPUS
            temp = ((pValue1 >> 5) & 7);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[4].Value = CurLang.FCPU_111;
                    break;
            }

            //MCLRE
            temp = ((pValue1 >> 8) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.MCLRE_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = CurLang.MCLRE_1;
                    break;
            }


            //SPDS
            temp = ((pValue1 >> 10) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[6].Value = CurLang.SPDS_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[6].Value = CurLang.SPDS_1;
                    break;
            }

            //RESSEL
            temp = ((pValue1 >> 11) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[7].Value = CurLang.RESSEL_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[7].Value = CurLang.RESSEL_1;
                    break;
            }

            //VLVRS
            temp = (pValue2 & 15);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1110;
                    break;
                case 15:
                    propertyGridEx_PC.Item[8].Value = CurLang.LVR_1111;
                    break;
            }

            //FAS
            temp = ((pValue1 >> 4) & 7);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[9].Value = CurLang.FAS_101;
                    break;
            }

            //ENCR
            temp = ((pValue1 >> 13) & 1);
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[10].Value = CurLang.ENCR_0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[10].Value = CurLang.ENCR_1;
                    break;
            }



        }
    }
}