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

        private void MC32P7511_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] MODE = new string[] { CurLang.MODEMC32P7511String0, CurLang.MODEMC32P7511String1, CurLang.MODEMC32P7511String2 };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "000:PWRT=TWDT=4ms", "001:PWRT=TWDT=16ms", "010:PWRT=TWDT=64ms", "011:PWRT=TWDT=256ms",
                                           "100:PWRT=4ms;TWDT=512ms","101:PWRT=16ms;TWDT=1024ms","110:PWRT=64ms;TWDT=2048ms","111:PWRT=256ms;TWDT=4096ms" };
            string[] FCPU = new string[] { "000:Fosc//2", "001:Fosc//4", "010:Fosc//8", "011:Fosc//16", "100:Fosc//32", "101:Fosc//64", "110:Fosc//128", "111:Fosc//256" };
            string[] MCLRE = new string[] { CurLang.MCLREMC32P7510String0, CurLang.MCLREMC32P7510String1 };
            string[] VLVRS = new string[] { CurLang.LVRSMC32P7511String0000, CurLang.LVRSMC32P7511String0001, CurLang.LVRSMC32P7511String0010, CurLang.LVRSMC32P7511String0011,
                                            CurLang.LVRSMC32P7511String0100, CurLang.LVRSMC32P7511String0101, CurLang.LVRSMC32P7511String0110, CurLang.LVRSMC32P7511String0111,
                                            CurLang.LVRSMC32P7511String1000, CurLang.LVRSMC32P7511String1001, CurLang.LVRSMC32P7511String1010, CurLang.LVRSMC32P7511String1011,
                                            CurLang.LVRSMC32P7511String1100, CurLang.LVRSMC32P7511String1101, CurLang.LVRSMC32P7511String1110, CurLang.LVRSMC32P7511String1111};
            string[] CLKSEL = new string[] { CurLang.CLKSELMC32P7510String0, CurLang.CLKSELMC32P7510String1 };
            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };
            string[] LVRPDS = new string[] { CurLang.LVRPDSMC32P7323String0, CurLang.LVRPDSMC32P7323String1 };
            string[] SPDS = new string[] { CurLang.SPDSMC32P7323String0, CurLang.SPDSMC32P7323String1 };


            propertyGridEx_PC.Item.Add("MODE", CurLang.MODEMC32P7511String0, false, CurLang.MODEMC32P6060Func, CurLang.MODEMC32P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString1, false, CurLang.WDTCMC31P11Func, CurLang.WDTCMC31P11FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "000:PWRT=TWDT=4ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("FCPU", "000:Fosc//2", false, CurLang.FCPUMC31P11Func, CurLang.FCPUMC32P21FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC32P7510String0, false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("VLVRS", CurLang.LVRSMC32P7511String0001, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC32P7511FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VLVRS, true);

            //             propertyGridEx_PC.Item.Add("CLKSEL", CurLang.CLKSELMC32P7510String1, false, CurLang.CLKSELMC32P7510Func, CurLang.CLKSELMC32P7510FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CLKSEL, true);

            propertyGridEx_PC.Item.Add("FAS", "000:16M", false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

            propertyGridEx_PC.Item.Add("SPDS", CurLang.SPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SPDS, true);

            propertyGridEx_PC.Item.Add("LVRPDS", CurLang.LVRPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRPDS, true);
        }

        private void MC32P7511_Option2Value()
        {
            frmMAIN.FREQ = 0x16;
            int optValue_Temp0, optValue_Temp1, optValue_Temp3, optValue_Temp4;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp3 = optValue_Temp4 = 0;
            int i = 0;
            string temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MODEMC32P7511String0)
            {
                optValue_Temp0 = optValue_Temp0 | (5 << 0);
            }
            else if (temp == CurLang.MODEMC32P7511String1)
            {
                optValue_Temp0 = optValue_Temp0 | (3 << 0);
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (2 << 0);
            }

            optValue_Temp0 = optValue_Temp0 | (2 << 3);

            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }

            //WDTT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:PWRT=TWDT=4ms":
                    optValue_Temp1 = optValue_Temp1 | (0 << 2);
                    break;
                case "001:PWRT=TWDT=16ms":
                    optValue_Temp1 = optValue_Temp1 | (1 << 2);
                    break;
                case "010:PWRT=TWDT=64ms":
                    optValue_Temp1 = optValue_Temp1 | (2 << 2);
                    break;
                case "011:PWRT=TWDT=256ms":
                    optValue_Temp1 = optValue_Temp1 | (3 << 2);
                    break;
                case "100:PWRT=4ms;TWDT=512ms":
                    optValue_Temp1 = optValue_Temp1 | (4 << 2);
                    break;
                case "101:PWRT=16ms;TWDT=1024ms":
                    optValue_Temp1 = optValue_Temp1 | (5 << 2);
                    break;
                case "110:PWRT=64ms;TWDT=2048ms":
                    optValue_Temp1 = optValue_Temp1 | (6 << 2);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (7 << 2);
                    break;

            }

            //fcpus
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:Fosc//2":
                    optValue_Temp1 = optValue_Temp1 | (0 << 5);
                    break;
                case "001:Fosc//4":
                    optValue_Temp1 = optValue_Temp1 | (1 << 5);
                    break;
                case "010:Fosc//8":
                    optValue_Temp1 = optValue_Temp1 | (2 << 5);
                    break;
                case "011:Fosc//16":
                    optValue_Temp1 = optValue_Temp1 | (3 << 5);
                    break;
                case "100:Fosc//32":
                    optValue_Temp1 = optValue_Temp1 | (4 << 5);
                    break;
                case "101:Fosc//64":
                    optValue_Temp1 = optValue_Temp1 | (5 << 5);
                    break;
                case "110:Fosc//128":
                    optValue_Temp1 = optValue_Temp1 | (6 << 5);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (7 << 5);
                    break;
            }

            //mclre
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MCLREMC32P7510String0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 8);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 8);
            }

            //VLVRS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRSMC32P7511String0000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String0111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1000)
            {
                optValue_Temp1 = optValue_Temp1 | (8 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1001)
            {
                optValue_Temp1 = optValue_Temp1 | (9 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1010)
            {
                optValue_Temp1 = optValue_Temp1 | (10 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1011)
            {
                optValue_Temp1 = optValue_Temp1 | (11 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1100)
            {
                optValue_Temp1 = optValue_Temp1 | (12 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1101)
            {
                optValue_Temp1 = optValue_Temp1 | (13 << 11);
            }
            else if (temp == CurLang.LVRSMC32P7511String1110)
            {
                optValue_Temp1 = optValue_Temp1 | (14 << 11);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (15 << 11);
            }

            //CLKSEL
            //             i++;
            //             temp = propertyGridEx_PC.Item[i].Value.ToString();
            //             if (temp == CurLang.CLKSELMC32P7510String0)
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (0 << 15);
            //             }
            //             else
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (1 << 15);
            //             }
            optValue_Temp1 = optValue_Temp1 | (1 << 15);

            optValue_Temp3 = optValue_Temp3 | (0x80 << 0);
            //FAS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp3 = optValue_Temp3 | (0 << 8);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp3 = optValue_Temp3 | (1 << 8);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp3 = optValue_Temp3 | (2 << 8);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp3 = optValue_Temp3 | (3 << 8);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp3 = optValue_Temp3 | (4 << 8);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp3 = optValue_Temp3 | (5 << 8);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.SPDSMC32P7323String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 11);
            }

            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRPDSMC32P7323String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 12);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 12);
            }

            optValue_Temp4 = optValue_Temp4 | (0x04 << 0);

            optionValue.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);
        }

        private void MC32P7511_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            frmMAIN.FREQ = 0x16;
            int i = 0;

            uint temp = pValue0 & 7;
            switch (temp)
            {
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7511String0;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7511String1;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P7511String2;
                    break;
            }

            i++;
            temp = pValue1 & 3;
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
            temp = (pValue1 >> 2) & 7;
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
                    propertyGridEx_PC.Item[i].Value = "101:PWRT=16ms;TWDT=1024ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:PWRT=64ms;TWDT=2048ms";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:PWRT=256ms;TWDT=4096ms";
                    break;
            }

            i++;
            temp = (pValue1 >> 5) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:Fosc//8";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:Fosc//16";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:Fosc//32";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:Fosc//64";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:Fosc//128";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "111:Fosc//256";
                    break;
            }

            i++;
            temp = (pValue1 >> 8) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P7510String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC32P7510String0;
            }

            i++;
            temp = (pValue1 >> 11) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC32P7511String1111;
                    break;
            }

            //             i++;
            //             temp = (pValue1 >> 15) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[i].Value = CurLang.CLKSELMC32P7510String1;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[i].Value = CurLang.CLKSELMC32P7510String0;
            //             }

            i++;
            temp = (pValue3 >> 8) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = "000:16M";
                    frmMAIN.FREQ = 0x16;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:8M";
                    frmMAIN.FREQ = 0x08;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:4M";
                    frmMAIN.FREQ = 0x04;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:2M";
                    frmMAIN.FREQ = 0x02;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:1M";
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "101:455K";
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            i++;
            temp = (pValue3 >> 11) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String0;
            }

            i++;
            temp = (pValue3 >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String0;
            }
        }
    }
}