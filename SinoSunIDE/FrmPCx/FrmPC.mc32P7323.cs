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

        private void MC32P7323_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] MODE = new string[] { CurLang.MODEMC32P7323String0, CurLang.MODEMC32P7323String1/*, CurLang.MODEMC32P7323String2*/ };//8K 4K
            string[] VDSEL = new string[] { "1.7V", "1.9V" };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4ms", "001:PWRT=TWDT=16ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                            "100:PWRT=4ms;TWDT=512ms","101:PWRT=16;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256;TWDT=4096ms"};
            string[] FCPU = new string[] { "000:Fosc//4", "001:Fosc//8", "010:Fosc//16", "011:Fosc//32", "100:Fosc//64", "101:Fosc//128", "110:Fosc//256", "111:Fosc//512" };
            string[] LVRS = new string[] { CurLang.VLVRSMC32P7323String0, CurLang.VLVRSMC32P7323String1, CurLang.VLVRSMC32P7323String2, CurLang.VLVRSMC32P7323String3,
                                           CurLang.VLVRSMC32P7323String4, CurLang.VLVRSMC32P7323String5, CurLang.VLVRSMC32P7323String6, CurLang.VLVRSMC32P7323String7,
                                           CurLang.VLVRSMC32P7323String8, CurLang.VLVRSMC32P7323String9, CurLang.VLVRSMC32P7323String10, CurLang.VLVRSMC32P7323String11,
                                           CurLang.VLVRSMC32P7323String12, CurLang.VLVRSMC32P7323String13, CurLang.VLVRSMC32P7323String14, CurLang.VLVRSMC32P7323String15};

            string[] CLKSEL = new string[] { CurLang.CLKSELMC32P7510String0, CurLang.CLKSELMC32P7510String1 };
            string[] LVRPDS = new string[] { CurLang.LVRPDSMC32P7323String0, CurLang.LVRPDSMC32P7323String1 };
            string[] SPDS = new string[] { CurLang.SPDSMC32P7323String0, CurLang.SPDSMC32P7323String1 };


            if (mcuID == "0x7323")
            {
                propertyGridEx_PC.Item.Add("MODE", CurLang.MODEMC32P7323String0, false, "", "", true);
                propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);
            }

            //             propertyGridEx_PC.Item.Add("VDSEL", "1.7V", false, "", "", true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "111:Fosc//512", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.VLVRSMC32P7323String1, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            //             propertyGridEx_PC.Item.Add("CLKSEL", CurLang.CLKSELMC32P7510String0, false, "", "", true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CLKSEL, true);

            propertyGridEx_PC.Item.Add("LVRPDS", CurLang.LVRPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRPDS, true);

            propertyGridEx_PC.Item.Add("SPDS", CurLang.SPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SPDS, true);

        }

        private void MC32P7323_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            frmMAIN.FREQ = 0x16;
            int i = 0;

            uint temp = 0;
            if (mcuID == "0x7323")
            {
                temp = pValue0 & 7;
                switch (temp)
                {
                    case 4:
                        propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7323String0;
                        frmMAIN.RomSpace_stat = 1;
                        break;
                    //case 3:
                    //    propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7323String1;
                    //    break;
                    default:
                        propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7323String1;
                        frmMAIN.RomSpace_stat = 0;
                        break;
                }
                i++;
            }


            //             i++;
            //             temp = (pValue1 >> 15) & 1;
            //             if (temp == 0)
            //             {
            //                 propertyGridEx_PC.Item[i].Value = "1.7V";
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[i].Value = "1.9V";
            //             }

            temp = pValue4 & 3;
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

            i++;
            temp = (pValue4 >> 2) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:PWRT=TWDT=4ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:PWRT=TWDT=16ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:PWRT=TWDT=64ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:PWRT=TWDT=256ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:PWRT=4ms;TWDT=512ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:PWRT=16;TWDT=1024ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:PWRT=64ms;TWDT=2048ms";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:PWRT=256;TWDT=4096ms";
                    break;
            }

            i++;
            temp = (pValue4 >> 5) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:Fosc//4";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:Fosc//8";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:Fosc//16";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:Fosc//32";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:Fosc//64";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:Fosc//128";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:Fosc//256";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:Fosc//512";
                    break;
            }

            i++;
            temp = (pValue4 >> 11) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String0;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String1;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String2;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String3;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String4;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String5;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String6;
                    break;
                case 7:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String7;
                    break;
                case 8:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String8;
                    break;
                case 9:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String9;
                    break;
                case 10:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String10;
                    break;
                case 11:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String11;
                    break;
                case 12:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String12;
                    break;
                case 13:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String13;
                    break;
                case 14:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String14;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.VLVRSMC32P7323String15;
                    break;
            }

            //             i++;
            //             temp = (pValue4 >> 15) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[i].Value = CurLang.CLKSELMC32P7510String1;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[i].Value = CurLang.CLKSELMC32P7510String0;
            //             }

            i++;
            temp = (pValue5 >> 1) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String0;
            }

            i++;
            temp = (pValue5 >> 2) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String0;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String1;
            }
        }
        private void MC32P7323_Option2Value()
        {
            Int32 optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3, optValue_Temp4, optValue_Temp5;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = optValue_Temp3 = optValue_Temp4 = optValue_Temp5 = 0;
            int i = 0;

            frmMAIN.FREQ = 0x16;
            string temp = null;

            if (mcuID == "0x7323")
            {
                temp = propertyGridEx_PC.Item[i].Value.ToString();
                if (temp == CurLang.MODEMC32P7323String0)
                {
                    optValue_Temp0 = optValue_Temp0 | (4 << 0);
                    frmMAIN.RomSpace_stat = 1;
                }
                //else if (temp == CurLang.MODEMC32P7323String1)
                //{
                //    optValue_Temp0 = optValue_Temp0 | (3 << 0);
                //}
                else
                {
                    optValue_Temp0 = optValue_Temp0 | (0 << 0);
                    frmMAIN.RomSpace_stat = 0;
                }
                i++;
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 0);
            }

            //             i++;
            //             temp = propertyGridEx_PC.Item[i].Value.ToString();
            //             if (temp == "1.7V")
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (0 << 15);
            //             }
            //             else
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (1 << 15);
            //             }

            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 0);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (3 << 0);
            }

            //WDTT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=4ms":
                    optValue_Temp4 = optValue_Temp4 | (0 << 2);
                    break;
                case "001:PWRT=TWDT=16ms":
                    optValue_Temp4 = optValue_Temp4 | (1 << 2);
                    break;
                case "010:PWRT=TWDT=64ms":
                    optValue_Temp4 = optValue_Temp4 | (2 << 2);
                    break;
                case "011:PWRT=TWDT=256ms":
                    optValue_Temp4 = optValue_Temp4 | (3 << 2);
                    break;
                case "100:PWRT=4ms;TWDT=512ms":
                    optValue_Temp4 = optValue_Temp4 | (4 << 2);
                    break;
                case "101:PWRT=16;TWDT=1024ms":
                    optValue_Temp4 = optValue_Temp4 | (5 << 2);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp4 = optValue_Temp4 | (6 << 2);
                    break;
                default:
                    optValue_Temp4 = optValue_Temp4 | (7 << 2);
                    break;
            }

            //FCPU
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//4":
                    optValue_Temp4 = optValue_Temp4 | (0 << 5);
                    break;
                case "001:Fosc//8":
                    optValue_Temp4 = optValue_Temp4 | (1 << 5);
                    break;
                case "010:Fosc//16":
                    optValue_Temp4 = optValue_Temp4 | (2 << 5);
                    break;
                case "011:Fosc//32":
                    optValue_Temp4 = optValue_Temp4 | (3 << 5);
                    break;
                case "100:Fosc//64":
                    optValue_Temp4 = optValue_Temp4 | (4 << 5);
                    break;
                case "101:Fosc//128":
                    optValue_Temp4 = optValue_Temp4 | (5 << 5);
                    break;
                case "110:Fosc//256":
                    optValue_Temp4 = optValue_Temp4 | (6 << 5);
                    break;
                default:
                    optValue_Temp4 = optValue_Temp4 | (7 << 5);
                    break;
            }

            //VLVRS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.VLVRSMC32P7323String0)
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String1)
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String2)
            {
                optValue_Temp4 = optValue_Temp4 | (2 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String3)
            {
                optValue_Temp4 = optValue_Temp4 | (3 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String4)
            {
                optValue_Temp4 = optValue_Temp4 | (4 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String5)
            {
                optValue_Temp4 = optValue_Temp4 | (5 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String6)
            {
                optValue_Temp4 = optValue_Temp4 | (6 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String7)
            {
                optValue_Temp4 = optValue_Temp4 | (7 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String8)
            {
                optValue_Temp4 = optValue_Temp4 | (8 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String9)
            {
                optValue_Temp4 = optValue_Temp4 | (9 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String10)
            {
                optValue_Temp4 = optValue_Temp4 | (10 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String11)
            {
                optValue_Temp4 = optValue_Temp4 | (11 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String12)
            {
                optValue_Temp4 = optValue_Temp4 | (12 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String13)
            {
                optValue_Temp4 = optValue_Temp4 | (13 << 11);
            }
            else if (temp == CurLang.VLVRSMC32P7323String14)
            {
                optValue_Temp4 = optValue_Temp4 | (14 << 11);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (15 << 11);
            }

            //CLKSEL
            optValue_Temp4 = optValue_Temp4 | (1 << 15);
            //             i++;
            //             temp = propertyGridEx_PC.Item[i].Value.ToString();
            //             if (temp == CurLang.CLKSELMC32P7510String1)
            //             {
            //                 optValue_Temp4 = optValue_Temp4 | (1 << 15);
            //             }
            //             else
            //             {
            //                 optValue_Temp4 = optValue_Temp4 | (0 << 15);
            //             }

            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRPDSMC32P7323String1)
            {
                optValue_Temp5 = optValue_Temp5 | (1 << 1);
            }
            else
            {
                optValue_Temp5 = optValue_Temp5 | (0 << 1);
            }

            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.SPDSMC32P7323String0)
            {
                optValue_Temp5 = optValue_Temp5 | (1 << 2);
            }
            else
            {
                optValue_Temp5 = optValue_Temp5 | (0 << 2);
            }

            optionValue.Text = Convert.ToString(optValue_Temp4, 16);
            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue2.Text = Convert.ToString(optValue_Temp2, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);
            optionValue5.Text = Convert.ToString(optValue_Temp5, 16);

        }

    }


}