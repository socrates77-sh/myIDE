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
        private void MC34P01_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] borsel = new string[] { "000:1.5V", "001:1.8V", "010:2.0V", "011:2.2V", "100:2.4V", "101:2.6V", "110:3.0V", "111:3.6V" };
            string[] FOSC = new string[] { "111:16MHz", "110:8MHz", "101:4MHz", "100:2MHz", "011:1MHz", "010:444K"};
            string[] TWDT = new string[] { "PWRT=TWDT=18ms", "PWRT=TWDT=4.5ms", "PWRT=TWDT=288ms", "PWRT=TWDT=72ms", "PWRT=540us,TWDT=18ms",
                 "PWRT=540us,TWDT=4.5ms", "PWRT=540us,TWDT=288ms","PWRT=540us,TWDT=72ms"};
            string[] FCPU = new string[] { "1-4T", "0-2T" };
            string[] OSCM = new string[] { CurLang.OSCMMC34P01String1, CurLang.OSCMMC34P01String2 };

            string[] SMTSEL = new string[] { "1:1.5V/0.6V", "0:0.7VDD/0.3VDD" };
            string[] RESSEL = new string[] { CurLang.RESSELMC34P01String1, CurLang.RESSELMC34P01String2 };

            propertyGridEx_PC.Item.Add("borsel", "000:1.5V", false, CurLang.BorselFunc, CurLang.BorselFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(borsel, true);

            propertyGridEx_PC.Item.Add("MCLRE", ref ab, "RSTE", false, CurLang.MCLREFunc, CurLang.MCLREFuncExplain1+"\n\t"+CurLang.MCLREFuncExplain2+"\n\t" + CurLang.MCLREFuncExplain3, true);

            propertyGridEx_PC.Item.Add("FOSC", "111:16MHz", false, CurLang.FOSCFunc, CurLang.FOSCMC34P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            propertyGridEx_PC.Item.Add("FCPU", "1-4T", false, CurLang.FCPUFunc, CurLang.FCPUFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("TWDT", "PWRT=TWDT=18ms", false, CurLang.TWDTFunc, CurLang.TWDTFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(TWDT, true);

            propertyGridEx_PC.Item.Add("WDTE", ref ab, "WDT", false, CurLang.WDTEFunc, CurLang.WDTEFuncExplain1+"\n\t"+CurLang.WDTEFuncExplain2+"\n\t"+CurLang.WDTEFuncExplain3, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMMC34P01, false, CurLang.OSCMFunc, CurLang.OSCMMC34P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("MCUtype", ref ab, "LVRS", false, CurLang.MCUtypeFunc, CurLang.MCUtypeFuncExplain1+"\n"+CurLang.MCUtypeFuncExplain2+"\n" + CurLang.MCUtypeFuncExplain3, true);

            propertyGridEx_PC.Item.Add("FILSEL", ref ab, "FILSEL", false, CurLang.RDPORTFunc, CurLang.FILSELMC34P01FuncExplain1+"\n\t"+CurLang.FILSELMC34P01FuncExplain2+"\n\t"+CurLang.FILSELMC34P01FuncExplain3, true);

            propertyGridEx_PC.Item.Add("RDPORT", ref ab, "LVRE", false, CurLang.RDPORTFunc, CurLang.RDPORTFuncExplain1+"\n\t"+CurLang.RDPORTFuncExplain2+"\n\t"+CurLang.RDPORTFuncExplain3, true);

            propertyGridEx_PC.Item.Add("SMTEN", ref ab, "SMTEN", false, CurLang.SMTENFunc, CurLang.SMTENFuncExplain1+"\n\t"+CurLang.SMTENFuncExplain2+"\n\t"+CurLang.SMTENFuncExplain3, true);

            propertyGridEx_PC.Item.Add("SMTSEL", CurLang.SMTSELMC34P01, false, CurLang.SMTSELMC34P01Func, CurLang.SMTSELMC34P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(SMTSEL, true);

            propertyGridEx_PC.Item.Add("RESSEL", CurLang.RESSELMC34P01, false, CurLang.RESSELMC34P01Func, CurLang.RESSELMC34P01FuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(RESSEL, true);

            propertyGridEx_PC.Item.Add("CP", ref ab, "PROTECT", false, CurLang.CPFunc, CurLang.CPFuncExplain, true);

        }

        private void MC34P01_Value2Option(uint pValue)

        {
            uint temp = 0;
            temp = (pValue >> 0) & 7;
            frmMAIN.FREQ = 0x16;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = "000:1.5V";
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = "001:1.8V";
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = "010:2.0V";
                    break;
                case 3:
                    propertyGridEx_PC.Item[0].Value = "011:2.2V";
                    break;
                case 4:
                    propertyGridEx_PC.Item[0].Value = "100:2.4V";
                    break;
                case 5:
                    propertyGridEx_PC.Item[0].Value = "101:2.6V";
                    break;
                case 6:
                    propertyGridEx_PC.Item[0].Value = "110:3.0V";
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = "111:3.6V";
                    break;
            }

            //MCLRE
            temp = (pValue >> 3) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[1].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[1].Value = "False";
            }

            temp = (pValue >> 4) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = CurLang.Retain000;
                    break;
                case 1:
                    propertyGridEx_PC.Item[2].Value = CurLang.Retain001;
                    break;
                case 2:
                    propertyGridEx_PC.Item[2].Value = "010:444K";
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = "011:1MHz";// "011:WDTRC 28K";
                    break;
                case 4:
                    propertyGridEx_PC.Item[2].Value = "100:2MHz";
                    break;
                case 5:
                    propertyGridEx_PC.Item[2].Value = "101:4MHz";
                    break;
                case 6:
                    propertyGridEx_PC.Item[2].Value = "110:8MHz";
                    break;
                default:
                    propertyGridEx_PC.Item[2].Value = "111:16MHz";
                    break;
            }

            temp = (pValue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = "1-4T";
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = "0-2T";
            }

            temp = (pValue >> 8) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = "PWRT=540us,TWDT=72ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = "PWRT=540us,TWDT=288ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = "PWRT=540us,TWDT=4.5ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[4].Value = "PWRT=540us,TWDT=18ms";
                    break;
                case 4:
                    propertyGridEx_PC.Item[4].Value = "PWRT=TWDT=72ms";
                    break;
                case 5:
                    propertyGridEx_PC.Item[4].Value = "PWRT=TWDT=288ms";
                    break;
                case 6:
                    propertyGridEx_PC.Item[4].Value = "PWRT=TWDT=4.5ms";
                    break;
                default:
                    propertyGridEx_PC.Item[4].Value = "PWRT=TWDT=18ms";
                    break;
            }

            //WDTE
            temp = (pValue >> 11) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[5].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[5].Value = "False";
            }

       
            //OSM
            temp = (pValue >> 12) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = CurLang.OSCMMC34P01String1;
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = CurLang.OSCMMC34P01String2;
            }
            //---------------------------------------------------
            //MCUSEL
            temp = (pValue >> 13) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[7].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[7].Value = "False";
            }

            //FILSET
            temp = (pValue >> 14) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[8].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[8].Value = "False";
            }

            //PDPORT
            temp = (pValue >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[9].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[9].Value = "False";
            }
            //SMTEN
            temp = (pValue >> 25) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[10].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[10].Value = "False";
            }
            //SMTSEL
            temp = (pValue >> 26) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[11].Value = CurLang.RESSELMC34P01String1;
            }
            else
            {
                propertyGridEx_PC.Item[11].Value = CurLang.RESSELMC34P01String2;
            }


            temp = (pValue >> 28) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[12].Value = "False";
            }
            else
            {
                propertyGridEx_PC.Item[12].Value = "True";
            }
        }



        private void MC34P01_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            frmMAIN.FREQ = 0x16;
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V","110:3.0V" "111:3.6V"
            switch (temp)
            {
                case "000:1.5V":
                    optValue_Temp = optValue_Temp | (0 << 0);
                    break;
                case "001:1.8V":
                    optValue_Temp = optValue_Temp | (1 << 0);
                    break;
                case "010:2.0V":
                    optValue_Temp = optValue_Temp | (2 << 0);
                    break;
                case "011:2.2V":
                    optValue_Temp = optValue_Temp | (3 << 0);
                    break;
                case "100:2.4V":
                    optValue_Temp = optValue_Temp | (4 << 0);
                    break;
                case "101:2.6V":
                    optValue_Temp = optValue_Temp | (5 << 0);
                    break;
                case "110:3.0V":
                    optValue_Temp = optValue_Temp | (6 << 0);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 0);
                    break;
            }
            //MCLRE
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 3);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 3);
            }

            //"111:8MHz", "110:4MHz", "101:2MHz", "100:1MHz", "011:WDTRC 28K", "010:455K", "001:32K", "000:62.5K"
            temp = propertyGridEx_PC.Item[2].Value.ToString();

            if (temp == CurLang.Retain000)
            {
                optValue_Temp = optValue_Temp | (0 << 4);
            }
            else if (temp == CurLang.Retain001)
            {
                optValue_Temp = optValue_Temp | (1 << 4);
            }
            else if (temp == "010:444K")
            {
                optValue_Temp = optValue_Temp | (2 << 4);
            }
            else if (temp == "011:1MHz") /*"011:WDTRC 28K":*/
            {
                optValue_Temp = optValue_Temp | (3 << 4);
            }
            else if (temp == "100:2MHz")
            {
                optValue_Temp = optValue_Temp | (4 << 4);
            }
            else if (temp == "101:4MHz")
            {
                optValue_Temp = optValue_Temp | (5 << 4);
            }
            else if (temp == "110:8MHz")
            {
                optValue_Temp = optValue_Temp | (6 << 4);
            }
            else
            {
                optValue_Temp = optValue_Temp | (7 << 4);
            }

            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == "1-4T")
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }


            //"PWRT=TWDT=18ms", "PWRT=TWDT=4.5ms", "PWRT=TWDT=288ms", "PWRT=TWDT=72ms", "PWRT=140us,TWDT=18ms",
            //   "PWRT=140us,TWDT=4.5ms", "PWRT=140us,TWDT=288ms","PWRT=140us,TWDT=72ms"

            temp = propertyGridEx_PC.Item[4].Value.ToString();

            switch (temp)
            {
                case "PWRT=540us,TWDT=72ms":
                    optValue_Temp = optValue_Temp | (0 << 8);
                    break;
                case "PWRT=540us,TWDT=288ms":
                    optValue_Temp = optValue_Temp | (1 << 8);
                    break;
                case "PWRT=540us,TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (2 << 8);
                    break;
                case "PWRT=540us,TWDT=18ms":
                    optValue_Temp = optValue_Temp | (3 << 8);
                    break;
                case "PWRT=TWDT=72ms":
                    optValue_Temp = optValue_Temp | (4 << 8);
                    break;
                case "PWRT=TWDT=288ms":
                    optValue_Temp = optValue_Temp | (5 << 8);
                    break;
                case "PWRT=TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (6 << 8);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 8);
                    break;
            }

            //WDTE
            temp = propertyGridEx_PC.Item[5].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 11);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 11);
            }

            //OSCM
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == CurLang.OSCMMC34P01String2)
            {
                optValue_Temp = optValue_Temp | (0 << 12);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 12);
            }

            //MCUSEL
            temp = propertyGridEx_PC.Item[7].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 13);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 13);
            }

            //FILSEL
            temp = propertyGridEx_PC.Item[8].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 14);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 14);
            }

           //RDPORT
            temp = propertyGridEx_PC.Item[9].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 15);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 15);
            }
   
             //SMTEN
            temp = propertyGridEx_PC.Item[10].Value.ToString();
            if (temp == "True")
            {
                optValue_Temp = optValue_Temp | (1 << 25);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 25);
            }

            //SMTSEL
            temp = propertyGridEx_PC.Item[11].Value.ToString();
            if (temp == "True")
            {
                optValue_Temp = optValue_Temp | (1 << 26);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 26);
            }

            //RESSEL
            temp = propertyGridEx_PC.Item[12].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 27);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 27);
            }

            //CP
            temp = propertyGridEx_PC.Item[13].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (1 << 28);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 28);
            }

            //optValue_Temp = optValue_Temp | (3 << 12);

            //optValue_Temp = optValue_Temp | (0xff << 16);

            //optValue_Temp = optValue_Temp | (0x0f <<28);

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }


    }


}









