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

        private void MC32T8132_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { /*"000:PWRT=TWDT=4ms", "001:PWRT=TWDT=16ms", */"010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                           /*"100:PWRT=4ms;TWDT=512ms","101:PWRT=16ms;TWDT=1024ms",*/"110:PWRT=64ms;TWDT=2048ms","111:PWRT=256ms;TWDT=4096ms" };
            string[] FCPU = new string[] { /*"000:Fosc//2", "001:Fosc//4", */"010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };
            string[] MCLRE = new string[] { CurLang.MCLREMC32T8132String0, CurLang.MCLREMC32T8132String1 };
            string[] FOSC = new string[] { CurLang.FOSCMC32E22String00, CurLang.FOSCMC32E22String01, CurLang.FOSCMC32E22String11 };
            string[] LVRS = new string[] { CurLang.LVRSMC32E22String000, CurLang.LVRSMC32E22String001, CurLang.LVRSMC32E22String010, CurLang.LVRSMC32E22String011,
                                           CurLang.LVRSMC32E22String100, CurLang.LVRSMC32E22String101, CurLang.LVRSMC32E22String110, CurLang.LVRSMC32E22String111};
            string[] ENCR = new string[] { CurLang.ENCRMC32E22String0, CurLang.ENCRMC32E22String1 };


            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "010:PWRT=TWDT=64ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//256", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32T8132String0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("FOSC", CurLang.FOSCMC32E22String00, false, CurLang.FOSCMC32E22Func, CurLang.FOSCMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC32E22String000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC33P78FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("ENCR", CurLang.ENCRMC32E22String0, false, CurLang.ENCRMC32T8132Func, CurLang.ENCRMC32E22FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ENCR, true);
        }


        private void MC32T8132_Option2Value()
        {
            int optValue_Temp = 0;
            //WDTC
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp = optValue_Temp | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp = optValue_Temp | (1 << 0);
            }
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
                    optValue_Temp = optValue_Temp | (4 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                case "101:PWRT=16ms;TWDT=1024ms":
                    optValue_Temp = optValue_Temp | (5 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp = optValue_Temp | (6 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 2);
                    optValue_Temp = optValue_Temp | (1 << 13);
                    break;
            }

            //FCPU
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

            //MCLRE
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == CurLang.MCLREMC32T8132String0)
                optValue_Temp = optValue_Temp | (0 << 7);
            else
                optValue_Temp = optValue_Temp | (1 << 7);

            //FOSC
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.FOSCMC32E22String00)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else if (temp == CurLang.FOSCMC32E22String01)
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << 8);
            }

            //LVRS
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.LVRSMC32E22String000)
            {
                optValue_Temp = optValue_Temp | (0 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String001)
            {
                optValue_Temp = optValue_Temp | (1 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String010)
            {
                optValue_Temp = optValue_Temp | (2 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String011)
            {
                optValue_Temp = optValue_Temp | (3 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String100)
            {
                optValue_Temp = optValue_Temp | (4 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String101)
            {
                optValue_Temp = optValue_Temp | (5 << 10);
            }
            else if (temp == CurLang.LVRSMC32E22String110)
            {
                optValue_Temp = optValue_Temp | (6 << 10);
            }
            else
            {
                optValue_Temp = optValue_Temp | (7 << 10);
            }

            //ENCR
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == CurLang.ENCRMC32E22String0)
            {
                optValue_Temp = optValue_Temp | (0 << 15);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 15);
            }

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }

        private void MC32T8132_Value2Option(uint pValue)
        {
            frmMAIN.FREQ = 0x16;

            uint temp = 0;

            temp = (pValue >> 0) & 3;

            //WDTC
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

            //WDTT
            temp = (pValue >> 2) & 3;
            uint temp1 = ((pValue >> 13) & 1);
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

            //FCPU
            temp = (pValue >> 4) & 7;
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

            //MCLRE
            temp = (pValue >> 7) & 1;
            if (temp == 0)
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32T8132String0;
            else
                propertyGridEx_PC.Item[3].Value = CurLang.MCLREMC32T8132String1;

            //FOSC
            temp = (pValue >> 8) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.FOSCMC32E22String00;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.FOSCMC32E22String01;
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = CurLang.FOSCMC32E22String11;
                    break;
            }

            //LVRS
            temp = (pValue >> 10) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String110;
                    break;
                default:
                    propertyGridEx_PC.Item[5].Value = CurLang.LVRSMC32E22String111;
                    break;
            }

            //ENCR
            temp = (pValue >> 15) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[6].Value = CurLang.ENCRMC32E22String0;
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = CurLang.ENCRMC32E22String1;
            }
        }
    }
}