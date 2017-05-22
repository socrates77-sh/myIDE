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
    //6220在6080基础上进行更改，固定一些项
    partial class FrmPC : DockContent
    {
        private void MC30P6220_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;
            string[] MODE = new string[] { CurLang.MODEMC32P6060String1, CurLang.MODEMC32P6060String2 };        //1K    0.5K

            //string[] ROTP = new string[] { CurLang.ROTPMC30P6060String0, CurLang.ROTPMC30P6060String1 };
            string[] ROTP = new string[] { CurLang.ROTPMC30P6060String0 };

            string[] LVRPD = new string[] { CurLang.LVRPDSMC32P7323String0, CurLang.LVRPDSMC32P7323String1 };

            //string[] SPDS = new string[] { CurLang.SPDSMC32P7323String0, CurLang.SPDSMC32P7323String1 };
            string[] SPDS = new string[] { CurLang.SPDSMC32P7323String0 };

            //string[] RESSEL = new string[] { "0:80K", "1:20K" };
            string[] RESSEL = new string[] { "0:80K" };

            string[] WDTT = new string[] { "111:PWRT=TWDT=18ms", "110:PWRT=TWDT=4.5ms", "101:PWRT=TWDT=288ms", "100:PWRT=TWDT=72ms",
                                           "011:PWRT=140us;TWDT=18ms", "010:PWRT=140us;TWDT=4.5ms", "001:PWRT=140us;TWDT=288ms", "000:PWRT=140us;TWDT=72m" };

            /*string[] LVRS = new string[] { CurLang.LVRSMC30P6060String0000, CurLang.LVRSMC30P6060String0001, CurLang.LVRSMC30P6060String0010, CurLang.LVRSMC30P6060String0011,
                                           CurLang.LVRSMC30P6060String0100, CurLang.LVRSMC30P6060String0101, CurLang.LVRSMC30P6060String0110, CurLang.LVRSMC30P6060String0111,
                                           CurLang.LVRSMC30P6060String1000, CurLang.LVRSMC30P6060String1001, CurLang.LVRSMC30P6060String1010, CurLang.LVRSMC30P6060String1011,
                                           CurLang.LVRSMC30P6060String1100, CurLang.LVRSMC30P6060String1101, CurLang.LVRSMC30P6060String1110, CurLang.LVRSMC30P6060String1111 };*/
            string[] LVRS = new string[] {  CurLang.LVRSMC30P6060String0010, 
                                           CurLang.LVRSMC30P6060String0100, CurLang.LVRSMC30P6060String0101, "LVR电压=2.3V", 
                                           CurLang.LVRSMC30P6060String1000, CurLang.LVRSMC30P6060String1010, 
                                           CurLang.LVRSMC30P6060String1100, CurLang.LVRSMC30P6060String1110 };

            //string[] MCLRE = new string[] { CurLang.MCLREMC30P6060String0, CurLang.MCLREMC30P6060String1 };
            string[] MCLRE = new string[] { CurLang.MCLREMC30P6060String0 };

            string[] MCUSEL = new string[] { CurLang.MCUSELMC30P6060String0, CurLang.MCUSELMC30P6060String1 };

            //string[] RDPORT = new string[] { CurLang.RDPORTMC30P6060String0, CurLang.RDPORTMC30P6060String1 };
            string[] RDPORT = new string[] { CurLang.RDPORTMC30P6060String1 };

            string[] SMTEN = new string[] { CurLang.SMTENMC30P6060String0, CurLang.SMTENMC30P6060String1 };

            //string[] SMTSEL = new string[] { "0:1.77V/1.1V", "1:0.7VDD/0.3VDD" };
            string[] SMTSEL = new string[] { "1:0.7VDD/0.3VDD" };

            //string[] P0SEL = new string[] { CurLang.P0SELMC30P6080String0, CurLang.P0SELMC30P6080String1 };
            string[] P0SEL = new string[] { CurLang.P0SELMC30P6080String0 };

            //string[] P0RES = new string[] { CurLang.P0RESMC30P6080String0, CurLang.P0RESMC30P6080String1 };
            string[] P0RES = new string[] { CurLang.P0RESMC30P6080String1 };

            //string[] LOADS = new string[] { CurLang.LOADSMC30P6080String0, CurLang.LOADSMC30P6080String1 };
            string[] LOADS = new string[] { CurLang.LOADSMC30P6080String1 };

            //string[] ODSEL = new string[] { CurLang.ODSELMC30P6060String0, CurLang.ODSELMC30P6060String1 };
            string[] ODSEL = new string[] { CurLang.ODSELMC30P6060String0 };

            //string[] OSCM = new string[] { CurLang.OSCMMC30P6080String000, CurLang.OSCMMC30P6080String001, CurLang.OSCMMC30P6080String010, CurLang.OSCMMC30P6080String011,
              //                             CurLang.OSCMMC30P6080String100, CurLang.OSCMMC30P6080String101, CurLang.OSCMMC30P6080String110, CurLang.OSCMMC30P6080String111};
            string[] OSCM = new string[] { CurLang.OSCMMC30P6080String111};

            //string[] FAS = new string[] { "000:16M", "001:8M", "010:4M", "011:2M", "100:1M", "101:455K" }; 
            string[] FAS = new string[] {"001:8M", "010:4M", "011:2M", "100:1M", "101:455K" };

            //string[] FINTOSC = new string[] { "000:Fsys=FAS", "001:Fsys=FAS/2", "010:Fsys=FAS/4", "011:Fsys=FAS/8", "100:Fsys=FAS/16", "101:Fsys=FAS/32", "110:Fsys=FAS/64", "111:Fsys=FAS/128" };
            string[] FINTOSC = new string[] { "000:Fsys=FAS" };

            //string[] FILS = new string[] { CurLang.FILSMC30P6060String1, CurLang.FILSMC30P6060String0 };
            string[] FILS = new string[] { CurLang.FILSMC30P6060String1 };

            //string[] FCPUS = new string[] { CurLang.FCPUSMC30P6060String0, CurLang.FCPUSMC30P6060String1 };
            string[] FCPUS = new string[] { CurLang.FCPUSMC30P6060String0 };

            string[] WDTE = new string[] { CurLang.WDTEMC20P24BString1, CurLang.WDTEMC20P24BString2 };

            //string[] RCOUT = new string[] { CurLang.RCOUTMC30P6080String0, CurLang.RCOUTMC30P6080String1 };
            string[] RCOUT = new string[] { CurLang.RCOUTMC30P6080String1 };

            propertyGridEx_PC.Item.Add("MODE", CurLang.MODEMC32P6060String1, false, CurLang.MODEMC32P6060Func, CurLang.MODEMC32P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MODE, true);

            propertyGridEx_PC.Item.Add("ROTP", CurLang.ROTPMC30P6060String0, false, CurLang.ROTPMC30P6060Func, CurLang.ROTPMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ROTP, true);

            propertyGridEx_PC.Item.Add("LVRPD", CurLang.LVRPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRPD, true);

            propertyGridEx_PC.Item.Add("SPDS", CurLang.SPDSMC32P7323String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SPDS, true);

            propertyGridEx_PC.Item.Add("RESSEL", "0:80K", false, CurLang.RESSELMC30P6060Func, CurLang.RESSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RESSEL, true);

            propertyGridEx_PC.Item.Add("WDTT", "111:PWRT=TWDT=18ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            //propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC30P6060String0000, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSMC30P6060String0010, false, CurLang.LVRSMC32P21Func, CurLang.LVRSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("MCLRE", CurLang.MCLREMC30P6060String0, false, CurLang.MCLREMC30P6060Func, CurLang.MCLREMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCLRE, true);

            propertyGridEx_PC.Item.Add("MCUSEL", CurLang.MCUSELMC30P6060String0, false, CurLang.MCUSELMC30P6060Func, CurLang.MCUSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(MCUSEL, true);

            //propertyGridEx_PC.Item.Add("RDPORT", CurLang.RDPORTMC30P6060String0, false, CurLang.RDPORTMC30P6060Func, CurLang.RDPORTMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item.Add("RDPORT", CurLang.RDPORTMC30P6060String1, false, CurLang.RDPORTMC30P6060Func, CurLang.RDPORTMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RDPORT, true);

            propertyGridEx_PC.Item.Add("SMTEN", CurLang.SMTENMC30P6060String0, false, CurLang.SMTENMC30P6060Func, CurLang.SMTENMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SMTEN, true);

            //propertyGridEx_PC.Item.Add("SMTSEL", "0:1.77V/1.1V", false, CurLang.SMTSELMC30P6060Func, CurLang.SMTSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item.Add("SMTSEL", "1:0.7VDD/0.3VDD", false, CurLang.SMTSELMC30P6060Func, CurLang.SMTSELMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SMTSEL, true);

            propertyGridEx_PC.Item.Add("P0SEL", CurLang.P0SELMC30P6080String0, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(P0SEL, true);

            //propertyGridEx_PC.Item.Add("P0RES", CurLang.P0RESMC30P6080String0, false, "", "", true);
            propertyGridEx_PC.Item.Add("P0RES", CurLang.P0RESMC30P6080String1, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(P0RES, true);

            //propertyGridEx_PC.Item.Add("LOADS", CurLang.LOADSMC30P6080String0, false, "", "", true);
            propertyGridEx_PC.Item.Add("LOADS", CurLang.LOADSMC30P6080String1, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LOADS, true);

            propertyGridEx_PC.Item.Add("ODSEL", CurLang.ODSELMC30P6060String0, false, CurLang.ODSELMC30P6060Func, CurLang.ODSELMC30P6060FuncExplain1 + "\n" + CurLang.ODSELMC30P6060FuncExplain2 + "\n" + CurLang.ODSELMC30P6060FuncExplain3, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(ODSEL, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC30P6080String111, false, CurLang.OSCMMC30P6060Func, CurLang.OSCMMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            //propertyGridEx_PC.Item.Add("FAS", "000:16M", false, CurLang.FASMC30P6060Func, CurLang.FASMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item.Add("FAS", "001:8M", false, CurLang.FASMC30P6060Func, CurLang.FASMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FAS, true);

            propertyGridEx_PC.Item.Add("FINTOSC", "000:Fsys=FAS", false, CurLang.FINTOSCMC30P6060Func, CurLang.FINTOSCMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FINTOSC, true);

            //propertyGridEx_PC.Item.Add("FILS", CurLang.FILSMC30P6060String0, false, CurLang.FILSMC30P6060Func, CurLang.FILSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item.Add("FILS", CurLang.FILSMC30P6060String1, false, CurLang.FILSMC30P6060Func, CurLang.FILSMC30P6060FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FILS, true);

            propertyGridEx_PC.Item.Add("FCPUS", CurLang.FCPUSMC30P6060String0, false, CurLang.FCPUSMC30P6060Func, CurLang.FCPUSMC30P6060FuncExplain1 + "\n" + CurLang.FCPUSMC30P6060FuncExplain2 + "\n" + CurLang.FCPUSMC30P6060FuncExplain3, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPUS, true);

            propertyGridEx_PC.Item.Add("WDTE", CurLang.WDTEMC20P24BString2, false, CurLang.WDTEMC20P01Func, CurLang.WDTEMC20P01FuncExplain1 + "\n" + CurLang.WDTEMC20P01FuncExplain2, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTE, true);

            propertyGridEx_PC.Item.Add("RCOUT", CurLang.RCOUTMC30P6080String1, false, "", "", true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RCOUT, true);
        }


        private void MC30P6220_Option2Value()
        {
            int optValue_Temp0, optValue_Temp1, optValue_Temp2, optValue_Temp3, optValue_Temp4;
            optValue_Temp0 = optValue_Temp1 = optValue_Temp2 = optValue_Temp3 = optValue_Temp4 = 0;
            int i = 0;

            string temp = propertyGridEx_PC.Item[i].Value.ToString();
            //MODE
            if (temp == CurLang.MODEMC32P6060String1)
            {
                optValue_Temp0 = optValue_Temp0 | (0 << 0);
                frmMAIN.RomSpace_stat = 1;          //add for 6220 RomSpace 1K
            }
            else
            {
                optValue_Temp0 = optValue_Temp0 | (1 << 0);
                frmMAIN.RomSpace_stat = 0;
            }

            //ROTP
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.ROTPMC30P6060String1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 1);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 1);
            }

            //LVRPD
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRPDSMC32P7323String1)
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 2);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 2);
            }

            //SPDS          6080SPDS跟开发手册不一致
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.SPDSMC32P7323String1)
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 3);
                //optValue_Temp1 = optValue_Temp1 | (1 << 3);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 3);
                //optValue_Temp1 = optValue_Temp1 | (0 << 3);
            }

            //RESSEL
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == "1:20K")
            {
                optValue_Temp1 = optValue_Temp1 | (1 << 5);
            }
            else
            {
                optValue_Temp1 = optValue_Temp1 | (0 << 5);
            }

            //WDTT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "111:PWRT=TWDT=18ms":
                    optValue_Temp1 = optValue_Temp1 | (7 << 8);
                    break;
                case "110:PWRT=TWDT=4.5ms":
                    optValue_Temp1 = optValue_Temp1 | (6 << 8);
                    break;
                case "101:PWRT=TWDT=288ms":
                    optValue_Temp1 = optValue_Temp1 | (5 << 8);
                    break;
                case "100:PWRT=TWDT=72ms":
                    optValue_Temp1 = optValue_Temp1 | (4 << 8);
                    break;
                case "011:PWRT=140us;TWDT=18ms":
                    optValue_Temp1 = optValue_Temp1 | (3 << 8);
                    break;
                case "010:PWRT=140us;TWDT=4.5ms":
                    optValue_Temp1 = optValue_Temp1 | (2 << 8);
                    break;
                case "001:PWRT=140us;TWDT=288ms":
                    optValue_Temp1 = optValue_Temp1 | (1 << 8);
                    break;
                default:
                    optValue_Temp1 = optValue_Temp1 | (0 << 8);
                    break;
            }

            optValue_Temp1 = optValue_Temp1 | (7 << 11);

            //LVRS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LVRSMC30P6060String0000)
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0001)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0010)
            {
                optValue_Temp3 = optValue_Temp3 | (2 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0011)
            {
                optValue_Temp3 = optValue_Temp3 | (3 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0100)
            {
                optValue_Temp3 = optValue_Temp3 | (4 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0101)
            {
                optValue_Temp3 = optValue_Temp3 | (5 << 0);
            }
            else if (temp == "LVR电压=2.3V")
            {
                optValue_Temp3 = optValue_Temp3 | (6 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String0111)
            {
                optValue_Temp3 = optValue_Temp3 | (7 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1000)
            {
                optValue_Temp3 = optValue_Temp3 | (8 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1001)
            {
                optValue_Temp3 = optValue_Temp3 | (9 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1010)
            {
                optValue_Temp3 = optValue_Temp3 | (10 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1011)
            {
                optValue_Temp3 = optValue_Temp3 | (11 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1100)
            {
                optValue_Temp3 = optValue_Temp3 | (12 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1101)
            {
                optValue_Temp3 = optValue_Temp3 | (13 << 0);
            }
            else if (temp == CurLang.LVRSMC30P6060String1110)
            {
                optValue_Temp3 = optValue_Temp3 | (14 << 0);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (15 << 0);
            }

            //MCLRE
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MCLREMC30P6060String0)
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 4);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 4);
            }

            //MCUSEL
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.MCUSELMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 5);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 5);
            }

            //RDPORT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.RDPORTMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 6);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 6);
            }

            //SMTEN
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.SMTENMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 7);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 7);
            }

            //SMTSEL
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == "1:0.7VDD/0.3VDD")
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 8);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 8);
            }

            //P0SEL
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.P0SELMC30P6080String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 9);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 9);
            }

            //P0RES
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.P0RESMC30P6080String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 10);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 10);
            }

            //LOADS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.LOADSMC30P6080String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 11);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 11);
            }

            //ODSEL
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.ODSELMC30P6060String1)
            {
                optValue_Temp3 = optValue_Temp3 | (1 << 12);
            }
            else
            {
                optValue_Temp3 = optValue_Temp3 | (0 << 12);
            }

            //OSCM
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.OSCMMC30P6080String111)
            {
                optValue_Temp4 = optValue_Temp4 | (7 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String110)
            {
                optValue_Temp4 = optValue_Temp4 | (6 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String101)
            {
                optValue_Temp4 = optValue_Temp4 | (5 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String100)
            {
                optValue_Temp4 = optValue_Temp4 | (4 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String011)
            {
                optValue_Temp4 = optValue_Temp4 | (3 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String010)
            {
                optValue_Temp4 = optValue_Temp4 | (2 << 0);
            }
            else if (temp == CurLang.OSCMMC30P6080String001)
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 0);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 0);
            }

            //FAS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "000:16M":
                    optValue_Temp4 = optValue_Temp4 | (0 << 3);
                    frmMAIN.FREQ = 0x16;
                    break;
                case "001:8M":
                    optValue_Temp4 = optValue_Temp4 | (1 << 3);
                    frmMAIN.FREQ = 0x08;
                    break;
                case "010:4M":
                    optValue_Temp4 = optValue_Temp4 | (2 << 3);
                    frmMAIN.FREQ = 0x04;
                    break;
                case "011:2M":
                    optValue_Temp4 = optValue_Temp4 | (3 << 3);
                    frmMAIN.FREQ = 0x02;
                    break;
                case "100:1M":
                    optValue_Temp4 = optValue_Temp4 | (4 << 3);
                    frmMAIN.FREQ = 0x01;
                    break;
                default:
                    optValue_Temp4 = optValue_Temp4 | (5 << 3);
                    frmMAIN.FREQ = 0x45;
                    break;
            }

            //FINTOSC
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            switch (temp)
            {
                case "111:Fsys=FAS/128":
                    optValue_Temp4 = optValue_Temp4 | (7 << 6);
                    break;
                case "110:Fsys=FAS/64":
                    optValue_Temp4 = optValue_Temp4 | (6 << 6);
                    break;
                case "101:Fsys=FAS/32":
                    optValue_Temp4 = optValue_Temp4 | (5 << 6);
                    break;
                case "100:Fsys=FAS/16":
                    optValue_Temp4 = optValue_Temp4 | (4 << 6);
                    break;
                case "011:Fsys=FAS/8":
                    optValue_Temp4 = optValue_Temp4 | (3 << 6);
                    break;
                case "010:Fsys=FAS/4":
                    optValue_Temp4 = optValue_Temp4 | (2 << 6);
                    break;
                case "001:Fsys=FAS/2":
                    optValue_Temp4 = optValue_Temp4 | (1 << 6);
                    break;
                default:
                    optValue_Temp4 = optValue_Temp4 | (0 << 6);
                    break;
            }

            //FILS        6080FILS跟开发手册不一致
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.FILSMC30P6060String0)
            {
                //optValue_Temp4 = optValue_Temp4 | (1 << 9);
                optValue_Temp4 = optValue_Temp4 | (0 << 9);
            }
            else
            {
                //optValue_Temp4 = optValue_Temp4 | (0 << 9);
                optValue_Temp4 = optValue_Temp4 | (1 << 9);
            }

            //FCPUS
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.FCPUSMC30P6060String0)
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 10);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 10);
            }

            //WDTE
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.WDTEMC20P24BString2)
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 11);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 11);
            }

            //RCOUT
            i++;
            temp = propertyGridEx_PC.Item[i].Value.ToString();
            if (temp == CurLang.RCOUTMC30P6080String1)
            {
                optValue_Temp4 = optValue_Temp4 | (1 << 12);
            }
            else
            {
                optValue_Temp4 = optValue_Temp4 | (0 << 12);
            }


            optionValue0.Text = Convert.ToString(optValue_Temp0, 16);
            optionValue1.Text = Convert.ToString(optValue_Temp1, 16);
            optionValue3.Text = Convert.ToString(optValue_Temp3, 16);
            optionValue4.Text = Convert.ToString(optValue_Temp4, 16);
        }

        private void MC30P6220_Value2Option(uint pValue, uint pValue0, uint pValue1, uint pValue2, uint pValue3, uint pValue4, uint pValue5)
        {
            uint temp = 0;
            int i = 0;
            //MODE
            temp = (pValue0 >> 0) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P6060String1;
                frmMAIN.RomSpace_stat = 1;          //add for 6220 RomSpace 1K
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MODEMC32P6060String2;
                frmMAIN.RomSpace_stat = 0;          //add for 6220 RomSpace 0.5K
            }

            //ROTP
            i++;
            temp = (pValue1 >> 1) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ROTPMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ROTPMC30P6060String0;
            }

            //LVRPD
            i++;
            temp = (pValue1 >> 2) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LVRPDSMC32P7323String0;
            }

            //SPDS
            i++;
            temp = (pValue1 >> 3) & 1;
            //if (temp == 1)            6080SPDS跟开发手册不一致
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SPDSMC32P7323String0;
            }

            //RESSEL
            i++;
            temp = (pValue1 >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = "1:20K";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "0:80K";
            }

            //WDTT
            i++;
            temp = (pValue1 >> 8) & 7;
            switch (temp)
            {
                case 7:
                    propertyGridEx_PC.Item[i].Value = "111:PWRT=TWDT=18ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:PWRT=TWDT=4.5ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:PWRT=TWDT=288ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:PWRT=TWDT=72ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:PWRT=140us;TWDT=18ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:PWRT=140us;TWDT=4.5ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:PWRT=140us;TWDT=288ms";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "000:PWRT=140us;TWDT=72m";
                    break;
            }

            //LVRS
            i++;
            temp = (pValue3 >> 0) & 15;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0010;
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0011;
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0100;
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0101;
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "LVR电压=2.3V";
                    break;
                case 7:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String0111;
                    break;
                case 8:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1000;
                    break;
                case 9:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1001;
                    break;
                case 10:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1010;
                    break;
                case 11:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1011;
                    break;
                case 12:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1100;
                    break;
                case 13:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1101;
                    break;
                case 14:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1110;
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = CurLang.LVRSMC30P6060String1111;
                    break;
            }

            //MCLRE
            i++;
            temp = (pValue3 >> 4) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCLREMC30P6060String1;
            }

            //MCUSEL
            i++;
            temp = (pValue3 >> 5) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCUSELMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.MCUSELMC30P6060String0;
            }

            //RDPORT
            i++;
            temp = (pValue3 >> 6) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.RDPORTMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.RDPORTMC30P6060String0;
            }

            //SMTEN
            i++;
            temp = (pValue3 >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SMTENMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.SMTENMC30P6060String0;
            }

            //SMTSEL
            i++;
            temp = (pValue3 >> 8) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = "1:0.7VDD/0.3VDD";
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = "0:1.77V/1.1V";
            }

            //P0SEL
            i++;
            temp = (pValue3 >> 9) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.P0SELMC30P6080String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.P0SELMC30P6080String0;
            }

            //P0RES
            i++;
            temp = (pValue3 >> 10) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.P0RESMC30P6080String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.P0RESMC30P6080String0;
            }

            //LOADS
            i++;
            temp = (pValue3 >> 11) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LOADSMC30P6080String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.LOADSMC30P6080String0;
            }

            //ODSEL
            i++;
            temp = (pValue3 >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ODSELMC30P6060String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.ODSELMC30P6060String0;
            }

            //OSCM
            i++;
            temp = (pValue4 >> 0) & 7;
            if (temp == 7)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String111;
            }
            else if (temp == 6)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String110;
            }
            else if (temp == 5)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String101;
            }
            else if (temp == 4)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String100;
            }
            else if (temp == 3)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String011;
            }
            else if (temp == 2)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String010;
            }
            else if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String001;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.OSCMMC30P6080String000;
            }

            //FAS
            i++;
            temp = (pValue4 >> 3) & 7;
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

            //FINTOSC
            i++;
            temp = (pValue4 >> 6) & 7;
            switch (temp)
            {
                case 7:
                    propertyGridEx_PC.Item[i].Value = "111:Fsys=FAS/128";
                    break;
                case 6:
                    propertyGridEx_PC.Item[i].Value = "110:Fsys=FAS/64";
                    break;
                case 5:
                    propertyGridEx_PC.Item[i].Value = "101:Fsys=FAS/32";
                    break;
                case 4:
                    propertyGridEx_PC.Item[i].Value = "100:Fsys=FAS/16";
                    break;
                case 3:
                    propertyGridEx_PC.Item[i].Value = "011:Fsys=FAS/8";
                    break;
                case 2:
                    propertyGridEx_PC.Item[i].Value = "010:Fsys=FAS/4";
                    break;
                case 1:
                    propertyGridEx_PC.Item[i].Value = "001:Fsys=FAS/2";
                    break;
                default:
                    propertyGridEx_PC.Item[i].Value = "000:Fsys=FAS";
                    break;
            }

            //FILS
            i++;
            temp = (pValue4 >> 9) & 1;
            //if (temp == 1)            6080FILS跟开发手册不一致
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.FILSMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.FILSMC30P6060String1;
            }

            //FCPUS
            i++;
            temp = (pValue4 >> 10) & 1;
            if (temp == 0)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.FCPUSMC30P6060String0;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.FCPUSMC30P6060String1;
            }

            //WDTE
            i++;
            temp = (pValue4 >> 11) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.WDTEMC20P24BString2;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.WDTEMC20P24BString1;
            }

            //RCOUT
            i++;
            temp = (pValue4 >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[i].Value = CurLang.RCOUTMC30P6080String1;
            }
            else
            {
                propertyGridEx_PC.Item[i].Value = CurLang.RCOUTMC30P6080String0;
            }
        }
    }
}