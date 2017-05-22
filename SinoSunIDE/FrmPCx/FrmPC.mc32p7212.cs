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

        private void MC32P7212_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4ms", "001:PWRT=TWDT=16ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                           "100:PWRT=4ms;TWDT=512ms","101:PWRT=16ms;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256ms;TWDT=4096ms" };
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };
            string[] MCLRE = new string[] { CurLang.MCLREMC32P7212String0, CurLang.MCLREMC32P7212String1 };
            string[] FOSC = new string[] { CurLang.FOSCMC32E22String00, CurLang.FOSCMC32E22String01 };
            string[] LVRS = new string[] { CurLang.LVRSMC32P7212String0000, CurLang.LVRSMC32P7212String0001, CurLang.LVRSMC32P7212String0010, CurLang.LVRSMC32P7212String0011,
                                           CurLang.LVRSMC32P7212String0100, CurLang.LVRSMC32P7212String0101, CurLang.LVRSMC32P7212String0110, CurLang.LVRSMC32P7212String0111,
                                           CurLang.LVRSMC32P7212String1000, CurLang.LVRSMC32P7212String1001, CurLang.LVRSMC32P7212String1010, CurLang.LVRSMC32P7212String1011,
                                           CurLang.LVRSMC32P7212String1100, CurLang.LVRSMC32P7212String1101, CurLang.LVRSMC32P7212String1110, CurLang.LVRSMC32P7212String1111};


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "000:Fosc//2", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P7212String0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("FOSC", CurLang.FOSCMC32E22String00, false, CurLang.FOSCMC32E22Func, CurLang.FOSCMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32P7212String0000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7212FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

           
        }


        private void MC32P7212_Option2Value()
        {
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3;
            optValue_Temp0 = 8192;
            optValue_Temp1 = 14464;
            optValue_Temp2 = optValue_Temp3 = 0;
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 0);
            }
            else
            {
                optValue_Temp2 = optValue_Temp2 | (3 << 0);
            }

            //WDTT
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=4ms":
                    optValue_Temp2 = optValue_Temp2 | (0 << 2);
                    optValue_Temp2 = optValue_Temp2 | (0 << 13);
                    break;
                case "001:PWRT=TWDT=16ms":
                    optValue_Temp2 = optValue_Temp2 | (1 << 2);
                    optValue_Temp2 = optValue_Temp2 | (0 << 13);
                    break;
                case "010:PWRT=TWDT=64ms":
                    optValue_Temp2 = optValue_Temp2 | (2 << 2);
                    optValue_Temp2 = optValue_Temp2 | (0 << 13);
                    break;
                case "011:PWRT=TWDT=256ms":
                    optValue_Temp2 = optValue_Temp2 | (3 << 2);
                    optValue_Temp2 = optValue_Temp2 | (0 << 13);
                    break;
                case "100:PWRT=4ms;TWDT=512ms":
                    optValue_Temp2 = optValue_Temp2 | (0 << 2);
                    optValue_Temp2 = optValue_Temp2 | (1 << 13);
                    break;
                case "101:PWRT=16ms;TWDT=1024ms":
                    optValue_Temp2 = optValue_Temp2 | (1 << 2);
                    optValue_Temp2 = optValue_Temp2 | (1 << 13);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp2 = optValue_Temp2 | (2 << 2);
                    optValue_Temp2 = optValue_Temp2 | (1 << 13);
                    break;
                default:
                    optValue_Temp2 = optValue_Temp2 | (3 << 2);
                    optValue_Temp2 = optValue_Temp2 | (1 << 13);
                    break;

            }

            //fcpus
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//2":
                    optValue_Temp2 = optValue_Temp2 | (0 << 4);
                    break;
                case "001:Fosc//4":
                    optValue_Temp2 = optValue_Temp2 | (1 << 4);
                    break;
                case "010:Fosc//8":
                    optValue_Temp2 = optValue_Temp2 | (2 << 4);
                    break;
                case "011:Fosc//16":
                    optValue_Temp2 = optValue_Temp2 | (3 << 4);
                    break;
                case "100:Fosc//32":
                    optValue_Temp2 = optValue_Temp2 | (4 << 4);
                    break;
                case "101:Fosc//64":
                    optValue_Temp2 = optValue_Temp2 | (5 << 4);
                    break;
                case "110:Fosc//128":
                    optValue_Temp2 = optValue_Temp2 | (6 << 4);
                    break;
                default:
                    optValue_Temp2 = optValue_Temp2 | (7 << 4);
                    break;
            }

            //mclre
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.MCLREMC32P7212String0)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 7);
            }
            else
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 7);
            }

            //FOSC
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.FOSCMC32E22String00)
            {
                optValue_Temp2 = optValue_Temp2 | (0 << 8);
            }
            else
            {
                optValue_Temp2 = optValue_Temp2 | (1 << 8);
            }

            optValue_Temp2 = optValue_Temp2 | (15 << 9);
            optValue_Temp2 = optValue_Temp2 | (2 << 14);

            //LVRS
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.LVRSMC32P7212String0000)
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0001)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0010)
            {
                optValue_Temp3 = optValue_Temp3 | (2 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0011)
            {
                optValue_Temp3 = optValue_Temp3 | (3 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0100)
            {
                optValue_Temp3 = optValue_Temp3 | (4 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0101)
            {
                optValue_Temp3 = optValue_Temp3 | (5 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0110)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String0111)
            {
                optValue_Temp3 = optValue_Temp3 | (7 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1000)
            {
                optValue_Temp3 = optValue_Temp3 | (8 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1001)
            {
                optValue_Temp3 = optValue_Temp3 | (9 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1010)
            {
                optValue_Temp3 = optValue_Temp3 | (10 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1011)
            {
                optValue_Temp3 = optValue_Temp3 | (11 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1100)
            {
                optValue_Temp3 = optValue_Temp3 | (12 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1101)
            {
                optValue_Temp3 = optValue_Temp3 | (13 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7212String1110)
            {
                optValue_Temp3 = optValue_Temp3 | (14 << 11);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (15 << 11);
            }

            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp2, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);

        }

        private void MC32P7212_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            frmMAIN.FREQ = 0x8;

            uint temp = pValue2 & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString2;
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.WDTCString3;
                    break;
            }

            temp = (pValue2 >> 2) & 3;
            uint temp1 = ((pValue2 >> 13) & 1);
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

            temp = (pValue2 >> 4) & 7;
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

            temp = pValue2 & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P7212String1;
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32P7212String0;
            }

            temp = (pValue2 >> 8) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[4].Value = CurLang.FOSCMC32E22String01;
            }
            else
            {
                propertyGridEx_PC.Item[4].Value = CurLang.FOSCMC32E22String00;
            }

            temp = (pValue3 >> 11) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32P7212String1111;
                    break;
            }
        }
    }
}