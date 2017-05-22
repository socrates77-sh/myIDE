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
        private void MC30P6060_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] MODE = new string[] { CurLang.MODEMC32P6060String1, CurLang.MODEMC32P6060String2 };
            string[] LVRS = new string[] { CurLang.LVRSMC30P6060String0000, CurLang.LVRSMC30P6060String0001, CurLang.LVRSMC30P6060String0010, CurLang.LVRSMC30P6060String0011,
                                           CurLang.LVRSMC30P6060String0100, CurLang.LVRSMC30P6060String0101, CurLang.LVRSMC30P6060String0110, CurLang.LVRSMC30P6060String0111,
                                           CurLang.LVRSMC30P6060String1000, CurLang.LVRSMC30P6060String1001, CurLang.LVRSMC30P6060String1010, CurLang.LVRSMC30P6060String1011,
                                           CurLang.LVRSMC30P6060String1100, CurLang.LVRSMC30P6060String1101, CurLang.LVRSMC30P6060String1110, CurLang.LVRSMC30P6060String1111 };
            string[] MCLRE = new string[] { CurLang.MCLREMC30P6060String0, CurLang.MCLREMC30P6060String1 };
            //string[] FINTOSC = new string[] { "111:FAS", "110:FAS/2", "101:FAS/4", "100:FAS/8", "011:LIRC", "010:FAS/32", "001:FAS/64", "000:FAS" };
            string[] FCPUS = new string[] { CurLang.FCPUSMC30P6060String0, CurLang.FCPUSMC30P6060String1 };
            string[] WDTT = new string[] { "111:PWRT=TWDT=18ms", "110:PWRT=TWDT=4.5ms", "101:PWRT=TWDT=288ms", "100:PWRT=TWDT=72ms",
                                           "011:PWRT=140us;TWDT=18ms", "010:PWRT=140us;TWDT=4.5ms", "001:PWRT=140us;TWDT=288ms", "000:PWRT=140us;TWDT=72m" };
            string[] WDTE = new string[] { CurLang.WDTEMC20P24BString1, CurLang.WDTEMC20P24BString2 };
            string[] CLKSEL = new string[] { CurLang.CLKSELMC30P6060String0, CurLang.CLKSELMC30P6060String1 };
            //string[] CP = new string[] { CurLang.CPMC30P6060String0, CurLang.CPMC30P6060String1 };
            //string[] ROTP = new string[] { CurLang.ROTPMC30P6060String0, CurLang.ROTPMC30P6060String1 };
            string[] IODRV = new string[] { CurLang.IODRVMC30P6060String0, CurLang.IODRVMC30P6060String1 };
            string[] RDPORT = new string[] { CurLang.RDPORTMC30P6060String0, CurLang.RDPORTMC30P6060String1 };
            string[] SMTEN = new string[] { CurLang.SMTENMC30P6060String0, CurLang.SMTENMC30P6060String1 };
            string[] MCUSEL = new string[] { CurLang.MCUSELMC30P6060String0, CurLang.MCUSELMC30P6060String1 };
            string[] SMTSEL = new string[] { "0:1.77V/1.1V", "1:0.7VDD/0.3VDD" };
            //string[] FILS = new string[] { CurLang.FILSMC30P6060String0, CurLang.FILSMC30P6060String1 };
            string[] ODSEL = new string[] { CurLang.ODSELMC30P6060String0, CurLang.ODSELMC30P6060String1 };
            string[] OSCM = new string[] { CurLang.OSCMMC30P6060String000, CurLang.OSCMMC30P6060String001, CurLang.OSCMMC30P6060String010, CurLang.OSCMMC30P6060String011,
                                           /*CurLang.OSCMMC30P6060String100,*/ CurLang.OSCMMC30P6060String101, CurLang.OSCMMC30P6060String1101, CurLang.OSCMMC30P6060String1102,
                                           CurLang.OSCMMC30P6060String1103, CurLang.OSCMMC30P6060String1104, CurLang.OSCMMC30P6060String1105, CurLang.OSCMMC30P6060String1106/*,*/
                                           /*CurLang.OSCMMC30P6060String111*/};
            //string[] XTDRVB = new string[] { CurLang.XTDRVBMC30P6060String0, CurLang.XTDRVBMC30P6060String1 };
            //string[] VDSEL = new string[] { "0:1.7V", "1:1.9V" };
            string[] RESSEL = new string[] { "0:80K", "1:20K" };
            string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };
            //string[] FDS = new string[] { "11:FHRC", "10:FHRC/2", "01:FHRC/4", "00:FHRC/8" };
            //string[] RCSMTB = new string[] { CurLang.RCSMTBMC30P6060String0, CurLang.RCSMTBMC30P6060String1 };

            //propertyGridEx_PC.Item.Add("MODE", CurLang.MODEMC32P6060String1, false, CurLang.MODEMC32P6060Func, CurLang.MODEMC32P6060FuncExplain, true);
            //propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC30P6060String0000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC30P6060String0, false, CurLang.MCLREMC30P6060Func, CurLang.MCLREMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            //             propertyGridEx_PC.Item.Add("FINTOSC", "111:FAS", false, CurLang.FINTOSCMC30P6060Func, CurLang.FINTOSCMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FINTOSC, true);

            propertyGridEx_PC.Item.Add("FCPUS", CurLang.FCPUSMC30P6060String0, false, CurLang.FCPUSMC30P6060Func, CurLang.FCPUSMC30P6060FuncExplain1 + "\n" + CurLang.FCPUSMC30P6060FuncExplain2 + "\n" + CurLang.FCPUSMC30P6060FuncExplain3, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPUS, true);

            propertyGridEx_PC.Item.Add("WDTT", "111:PWRT=TWDT=18ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("WDTE", CurLang.WDTEMC20P24BString2, false, CurLang.WDTEMC20P01Func, CurLang.WDTEMC20P01FuncExplain1 + "\n" + CurLang.WDTEMC20P01FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTE, true);

            //             propertyGridEx_PC.Item.Add("CLKSEL", CurLang.CLKSELMC30P6060String0, false, CurLang.CLKSELMC32P7510Func, CurLang.CLKSELMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CLKSEL, true);

            //             propertyGridEx_PC.Item.Add("CP", CurLang.CPMC30P6060String0, false, CurLang.CPMC30P6060Func, CurLang.CPMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(CP, true);

            //             propertyGridEx_PC.Item.Add("ROTP", CurLang.ROTPMC30P6060String0, false, CurLang.ROTPMC30P6060Func, CurLang.ROTPMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ROTP, true);

            propertyGridEx_PC.Item.Add("IODRV", CurLang.IODRVMC30P6060String0, false, CurLang.IODRVMC30P6060Func, CurLang.IODRVMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(IODRV, true);

            propertyGridEx_PC.Item.Add("RDPORT", CurLang.RDPORTMC30P6060String0, false, CurLang.RDPORTMC30P6060Func, CurLang.RDPORTMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RDPORT, true);

            propertyGridEx_PC.Item.Add("SMTEN", CurLang.SMTENMC30P6060String0, false, CurLang.SMTENMC30P6060Func, CurLang.SMTENMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SMTEN, true);

            propertyGridEx_PC.Item.Add("MCUSEL", CurLang.MCUSELMC30P6060String0, false, CurLang.MCUSELMC30P6060Func, CurLang.MCUSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCUSEL, true);

            propertyGridEx_PC.Item.Add("SMTSEL", "0:1.77V/1.1V", false, CurLang.SMTSELMC30P6060Func, CurLang.SMTSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SMTSEL, true);

            //             propertyGridEx_PC.Item.Add("FILS", CurLang.FILSMC30P6060String0, false, CurLang.FILSMC30P6060Func, CurLang.FILSMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FILS, true);

            propertyGridEx_PC.Item.Add("ODSEL", CurLang.ODSELMC30P6060String0, false, CurLang.ODSELMC30P6060Func, CurLang.ODSELMC30P6060FuncExplain1 + "\n" + CurLang.ODSELMC30P6060FuncExplain2 + "\n" + CurLang.ODSELMC30P6060FuncExplain3, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ODSEL, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC30P6060String1101, false, CurLang.OSCMMC30P6060Func, CurLang.OSCMMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            //             propertyGridEx_PC.Item.Add("XTDRVB", CurLang.XTDRVBMC30P6060String0, false, CurLang.XTDRVBMC30P6060Func, CurLang.XTDRVBMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(XTDRVB, true);

            //             propertyGridEx_PC.Item.Add("VDSEL", "0:1.7V", false, CurLang.VDSELMC30P6060Func, CurLang.VDSELMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(VDSEL, true);

            propertyGridEx_PC.Item.Add("RESSEL", "0:80K", false, CurLang.RESSELMC30P6060Func, CurLang.RESSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RESSEL, true);

            propertyGridEx_PC.Item.Add("FHIRCS", "000:16M", false, CurLang.FASMC30P6060Func, CurLang.FASMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

            //             propertyGridEx_PC.Item.Add("FDS", "11:FHRC", false, CurLang.FDSMC30P6060Func, CurLang.FDSMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FDS, true);

            //             propertyGridEx_PC.Item.Add("RCSMTB", CurLang.RCSMTBMC30P6060String0, false, CurLang.RCSMTBMC30P6060Func, CurLang.RCSMTBMC30P6060FuncExplain, true);
            //             propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCSMTB, true);
        }


        private void MC30P6060_Option2Value()
        {
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3, optValue_Temp4;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = optValue_Temp3 = optValue_Temp4 = 0;

//            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            //MODE
//             if (temp == CurLang.MODEMC32P6060String1)
//             {
//                 optValue_Temp0 = optValue_Temp0 | (0 << 0);
//             }
//             else
//             {
//                 optValue_Temp0 = optValue_Temp0 | (1 << 0);
//             }

            optValue_Temp0 = 254;

            //LVRS
            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            if (temp == CurLang.LVRSMC30P6060String0000)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0001)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0010)
            {
                optValue_Temp1 = optValue_Temp1 | (2 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0011)
            {
                optValue_Temp1 = optValue_Temp1 | (3 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0100)
            {
                optValue_Temp1 = optValue_Temp1 | (4 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0101)
            {
                optValue_Temp1 = optValue_Temp1 | (5 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0110)
            {
                optValue_Temp1 = optValue_Temp1 | (6 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0111)
            {
                optValue_Temp1 = optValue_Temp1 | (7 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1000)
            {
                optValue_Temp1 = optValue_Temp1 | (8 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1001)
            {
                optValue_Temp1 = optValue_Temp1 | (9 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1010)
            {
                optValue_Temp1 = optValue_Temp1 | (10 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1011)
            {
                optValue_Temp1 = optValue_Temp1 | (11 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1100)
            {
                optValue_Temp1 = optValue_Temp1 | (12 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1101)
            {
                optValue_Temp1 = optValue_Temp1 | (13 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1110)
            {
                optValue_Temp1 = optValue_Temp1 | (14 << 0);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (15 << 0);
            }

            //MCLRE
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == CurLang.MCLREMC30P6060String0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 4);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 4);
            }

            //FINTOSC
            //             temp = propertyGridEx_PC.Item[3].Value.ToString();
            //             switch (temp)
            //             {
            //                 case "111:FAS":
            //                     optValue_Temp1 = optValue_Temp1 | (7 << 5);
            //                     break;
            //                 case "110:FAS/2":
            //                     optValue_Temp1 = optValue_Temp1 | (6 << 5);
            //                     break;
            //                 case "101:FAS/4":
            //                     optValue_Temp1 = optValue_Temp1 | (5 << 5);
            //                     break;
            //                 case "100:FAS/8":
            //                     optValue_Temp1 = optValue_Temp1 | (4 << 5);
            //                     break;
            //                 case "011:LIRC":
            //                     optValue_Temp1 = optValue_Temp1 | (3 << 5);
            //                     break;
            //                 case "010:FAS/32":
            //                     optValue_Temp1 = optValue_Temp1 | (2 << 5);
            //                     break;
            //                 case "001:FAS/64":
            //                     optValue_Temp1 = optValue_Temp1 | (1 << 5);
            //                     break;
            //                 default:
            //                     optValue_Temp1 = optValue_Temp1 | (0 << 5);
            //                     break;
            //             }

            //FCPUS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            if (temp == CurLang.FCPUSMC30P6060String0)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 8);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 8);
            }

            //WDTT
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            switch (temp)
            {
                case "111:PWRT=TWDT=18ms":
                    optValue_Temp1 = optValue_Temp1 | (7 << 9);
                    break;
                case "110:PWRT=TWDT=4.5ms":
                    optValue_Temp1 = optValue_Temp1 | (6 << 9);
                    break;
                case "101:PWRT=TWDT=288ms":
                    optValue_Temp1 = optValue_Temp1 | (5 << 9);
                    break;
                case "100:PWRT=TWDT=72ms":
                    optValue_Temp1 = optValue_Temp1 | (4 << 9);
                    break;
                case "011:PWRT=140us;TWDT=18ms":
                    optValue_Temp1 = optValue_Temp1 | (3 << 9);
                    break;
                case "010:PWRT=140us;TWDT=4.5ms":
                    optValue_Temp1 = optValue_Temp1 | (2 << 9);
                    break;
                case "001:PWRT=140us;TWDT=288ms":
                    optValue_Temp1 = optValue_Temp1 | (1 << 9);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (0 << 9);
                    break;
            }

            //WDTE
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.WDTEMC20P24BString2)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 12);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 12);
            }

            //CP
            //optValue_Temp1 = optValue_Temp1 | (0 << 13);
            //CLKSEL
            optValue_Temp1 = optValue_Temp1 | (0 << 13);
            //             temp = propertyGridEx_PC.Item[6].Value.ToString();
            //             if (temp == CurLang.CLKSELMC30P6060String0)
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (0 << 13);
            //             }
            //             else
            //             {
            //                 optValue_Temp1 = optValue_Temp1 | (1 << 13);
            //             }

            //ROTP
            //             temp = propertyGridEx_PC.Item[6].Value.ToString();
            //             if (temp == CurLang.ROTPMC30P6060String1)
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (1 << 0);
            //             }
            //             else
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (0 << 0);
            //             }

            //IODRV
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == CurLang.IODRVMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 1);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 1);
            }

            //RDPORT
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == CurLang.RDPORTMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 2);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 2);
            }

            //SMTEN
            temp = propertyGridEx_PC.Item[7].Value.ToString();
            if (temp == CurLang.SMTENMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 3);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 3);
            }

            //MCUSEL
            temp = propertyGridEx_PC.Item[8].Value.ToString();
            if (temp == CurLang.MCUSELMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 4);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 4);
            }

            //SMTSEL
            temp = propertyGridEx_PC.Item[9].Value.ToString();
            if (temp == "1:0.7VDD/0.3VDD")
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 5);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 5);
            }

            //FILS
            //             temp = propertyGridEx_PC.Item[14].Value.ToString();
            //             if (temp == CurLang.FILSMC30P6060String1)
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (1 << 6);
            //             }
            //             else
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (0 << 6);
            //             }

            //ODSEL
            temp = propertyGridEx_PC.Item[10].Value.ToString();
            if (temp == CurLang.ODSELMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 7);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 7);
            }

            //OSCM
            temp = propertyGridEx_PC.Item[11].Value.ToString();
            if (temp == CurLang.OSCMMC30P6060String111)
            {
                optValue_Temp3 = optValue_Temp3 | (7 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1106)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (1 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1105)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (2 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1104)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (4 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1103)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (5 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1102)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (6 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String1101)
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String101)
            {
                optValue_Temp3 = optValue_Temp3 | (5 << 8);
                optValue_Temp3 = optValue_Temp3 | (0 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String100)
            {
                optValue_Temp3 = optValue_Temp3 | (4 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String011)
            {
                optValue_Temp3 = optValue_Temp3 | (3 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String010)
            {
                optValue_Temp3 = optValue_Temp3 | (2 << 8);
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (3 << 5);
                optValue_Temp3 = optValue_Temp3 | (1 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6060String001)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 8);
                optValue_Temp3 = optValue_Temp3 | (0 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 8);
                optValue_Temp3 = optValue_Temp3 | (0 << 6);
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
                optValue_Temp1 = optValue_Temp1 | (7 << 5);
                optValue_Temp3 = optValue_Temp3 | (1 << 0);
            }

            //XTDRVB
            //             temp = propertyGridEx_PC.Item[17].Value.ToString();
            //             if (temp == CurLang.XTDRVBMC30P6060String1)
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (1 << 11);
            //             }
            //             else
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (0 << 11);
            //             }

            //VDSEL
            //             temp = propertyGridEx_PC.Item[18].Value.ToString();
            //             if (temp == "1:1.9V")
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (1 << 12);
            //             }
            //             else
            //             {
            //                 optValue_Temp3 = optValue_Temp3 | (0 << 12);
            //             }

            //RESSEL
            temp = propertyGridEx_PC.Item[12].Value.ToString();
            if (temp == "1:20K")
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 13);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 13);
            }

            //FAS
            temp = propertyGridEx_PC.Item[13].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp4 = optValue_Temp4 | (0 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (0 << 12);
                    optValue_Temp4 = optValue_Temp4 | (1 << 5);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp4 = optValue_Temp4 | (1 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (1 << 12);
                    optValue_Temp4 = optValue_Temp4 | (1 << 5);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp4 = optValue_Temp4 | (2 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (1 << 12);
                    optValue_Temp4 = optValue_Temp4 | (1 << 5);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp4 = optValue_Temp4 | (3 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (0 << 12);
                    optValue_Temp4 = optValue_Temp4 | (1 << 5);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp4 = optValue_Temp4 | (4 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (0 << 12);
                    optValue_Temp4 = optValue_Temp4 | (0 << 5);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp4 = optValue_Temp4 | (5 << 0);
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    optValue_Temp3 = optValue_Temp3 | (0 << 12);
                    optValue_Temp4 = optValue_Temp4 | (0 << 5);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            //FDS
            //             temp = propertyGridEx_PC.Item[21].Value.ToString();
            //             switch (temp)
            //             {
            //                 case "11:FHRC":
            //                     optValue_Temp4 = optValue_Temp4 | (3 << 3);
            //                     break;
            //                 case "10:FHRC/2":
            //                     optValue_Temp4 = optValue_Temp4 | (2 << 3);
            //                     break;
            //                 case "01:FHRC/4":
            //                     optValue_Temp4 = optValue_Temp4 | (1 << 3);
            //                     break;
            //                 default:
            //                     optValue_Temp4 = optValue_Temp4 | (0 << 3);
            //                     break;
            //             }

            //             temp = propertyGridEx_PC.Item[22].Value.ToString();
            //             if (temp == CurLang.RCSMTBMC30P6060String0)
            //             {
            //                 optValue_Temp4 = optValue_Temp4 | (0 << 5);
            //             }
            //             else
            //             {
            //                 optValue_Temp4 = optValue_Temp4 | (1 << 5);
            //             }

            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);
        }

        private void MC30P6060_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            uint temp = 0;

            //MODE
//             temp = (pValue0 >> 0) & 1;
//             if (temp == 0)
//             {
//                 propertyGridEx_PC.Item[0].Value = CurLang.MODEMC32P6060String1;
//             }
//             else
//             {
//                 propertyGridEx_PC.Item[0].Value = CurLang.MODEMC32P6060String2;
//             }

            //LVRS
            temp = (pValue1 >> 0) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0110;
                    break;
                case 7:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSMC30P6060String1111;
                    break;
            }

            //MCLRE
            temp = (pValue1 >> 4) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[1].Value = CurLang.MCLREMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = CurLang.MCLREMC30P6060String1;
            }

            //FINTOSC
            //             temp = (pValue1 >> 5) & 7;
            //             switch (temp)
            //             {
            //                 case 7:
            //                     propertyGridEx_PC.Item[3].Value = "111:FAS";
            //                     break;
            //                 case 6:
            //                     propertyGridEx_PC.Item[3].Value = "110:FAS/2";
            //                     break;
            //                 case 5:
            //                     propertyGridEx_PC.Item[3].Value = "101:FAS/4";
            //                     break;
            //                 case 4:
            //                     propertyGridEx_PC.Item[3].Value = "100:FAS/8";
            //                     break;
            //                 case 3:
            //                     propertyGridEx_PC.Item[3].Value = "011:LIRC";
            //                     break;
            //                 case 2:
            //                     propertyGridEx_PC.Item[3].Value = "010:FAS/32";
            //                     break;
            //                 case 1:
            //                     propertyGridEx_PC.Item[3].Value = "001:FAS/64";
            //                     break;
            //                 default:
            //                     propertyGridEx_PC.Item[3].Value = "000:FAS";
            //                     break;
            //             }

            //FCPUS
            temp = (pValue1 >> 8) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[2].Value = CurLang.FCPUSMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[2].Value = CurLang.FCPUSMC30P6060String1;
            }

            //WDTT
            temp = (pValue1 >> 9) & 7;
            switch (temp)
            {
                case 7:
                    propertyGridEx_PC.Item[3].Value = "111:PWRT=TWDT=18ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[3].Value = "110:PWRT=TWDT=4.5ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[3].Value = "101:PWRT=TWDT=288ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[3].Value = "100:PWRT=TWDT=72ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[3].Value = "011:PWRT=140us;TWDT=18ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[3].Value = "010:PWRT=140us;TWDT=4.5ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[3].Value = "001:PWRT=140us;TWDT=288ms";
                    break;
                default:
                    propertyGridEx_PC.Item[3].Value = "000:PWRT=140us;TWDT=72m";
                    break;
            }

            //WDTE
            temp = (pValue1 >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[4].Value = CurLang.WDTEMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[4].Value = CurLang.WDTEMC20P24BString1;
            }

            //CLKSEL
            //             temp = (pValue1 >> 13) & 1;
            //             if (temp == 0)
            //             {
            //                 propertyGridEx_PC.Item[6].Value = CurLang.CLKSELMC30P6060String0;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[6].Value = CurLang.CLKSELMC30P6060String1;
            //             }

            //ROTP
            //             temp = (pValue3 >> 0) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[6].Value = CurLang.ROTPMC30P6060String1;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[6].Value = CurLang.ROTPMC30P6060String0;
            //             }

            //IODRV
            temp = (pValue3 >> 1) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[5].Value = CurLang.IODRVMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[5].Value = CurLang.IODRVMC30P6060String0;
            }

            //RDPORT
            temp = (pValue3 >> 2) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = CurLang.RDPORTMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = CurLang.RDPORTMC30P6060String0;
            }

            //SMTEN
            temp = (pValue3 >> 3) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[7].Value = CurLang.SMTENMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[7].Value = CurLang.SMTENMC30P6060String0;
            }

            //MCUSEL
            temp = (pValue3 >> 4) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[8].Value = CurLang.MCUSELMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[8].Value = CurLang.MCUSELMC30P6060String0;
            }

            //SMTSEL
            temp = (pValue3 >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[9].Value = "1:0.7VDD/0.3VDD";
            }
            else
            {
                propertyGridEx_PC.Item[9].Value = "0:1.77V/1.1V";
            }

            //FILS
            //             temp = (pValue3 >> 6) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[14].Value = CurLang.FILSMC30P6060String1;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[14].Value = CurLang.FILSMC30P6060String0;
            //             }

            //ODSEL
            temp = (pValue3 >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[10].Value = CurLang.ODSELMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[10].Value = CurLang.ODSELMC30P6060String0;
            }

            //OSCM
            temp = (pValue3 >> 8) & 7;
            uint temp1 = (pValue1 >> 5) & 7;
            if (temp == 7)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String111;
            }
            else if (temp == 6)
            {
                if (temp1 == 7)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1101;
                }
                else if (temp1 == 6)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1102;
                }
                else if (temp1 == 5)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1103;
                }
                else if (temp1 == 4)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1104;
                }
                else if (temp1 == 2)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1105;
                }
                else if (temp1 == 1)
                {
                    propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String1106;
                }
            }
            else if (temp == 5)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String101;
            }
            else if (temp == 4)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String100;
            }
            else if (temp == 3)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String011;
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String010;
            }
            else if (temp == 1)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String001;
            }
            else
            {
                propertyGridEx_PC.Item[11].Value = CurLang.OSCMMC30P6060String000;
            }

            //XTDRVB
            //             temp = (pValue3 >> 11) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[17].Value = CurLang.XTDRVBMC30P6060String1;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[17].Value = CurLang.XTDRVBMC30P6060String0;
            //             }

            //VDSEL
            //             temp = (pValue3 >> 12) & 1;
            //             if (temp == 1)
            //             {
            //                 propertyGridEx_PC.Item[18].Value = "1:1.9V";
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[18].Value = "0:1.7V";
            //             }

            //RESSEL
            temp = (pValue3 >> 13) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[12].Value = "1:20K";
            }
            else
            {
                propertyGridEx_PC.Item[12].Value = "0:80K";
            }

            //FAS
            temp = (pValue4 >> 0) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[13].Value = "000:16M";
                    frmMAIN.FREQ = 0x16;
                    break;
                case 1:
                    propertyGridEx_PC.Item[13].Value = "001:8M";
                    frmMAIN.FREQ = 0x08;
                    break;
                case 2:
                    propertyGridEx_PC.Item[13].Value = "010:4M";
                    frmMAIN.FREQ = 0x04;
                    break;
                case 3:
                    propertyGridEx_PC.Item[13].Value = "011:2M";
                    frmMAIN.FREQ = 0x02;
                    break;
                case 4:
                    propertyGridEx_PC.Item[13].Value = "100:1M";
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    propertyGridEx_PC.Item[13].Value = "101:455K";
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            //FDS
            //             temp = (pValue4 >> 3) & 3;
            //             switch (temp)
            //             {
            //                 case 3:
            //                     propertyGridEx_PC.Item[21].Value = "11:FHRC";
            //                     break;
            //                 case 2:
            //                     propertyGridEx_PC.Item[21].Value = "10:FHRC/2";
            //                     break;
            //                 case 1:
            //                     propertyGridEx_PC.Item[21].Value = "01:FHRC/4";
            //                     break;
            //                 default:
            //                     propertyGridEx_PC.Item[21].Value = "00:FHRC/8";
            //                     break;
            //             }

            //            temp = (pValue4 >> 5) & 1;
            //             if (temp == 0)
            //             {
            //                 propertyGridEx_PC.Item[22].Value = CurLang.RCSMTBMC30P6060String0;
            //             }
            //             else
            //             {
            //                 propertyGridEx_PC.Item[22].Value = CurLang.RCSMTBMC30P6060String1;
            //             }
        }
    }
}