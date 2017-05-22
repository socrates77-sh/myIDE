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
    public partial class FrmPC : DockContent
    {
        public string mcuID = "0x0222";
        public int loadState = 0;
        IShowLanguage CurLang = ShowLanguage.Current;

        public FrmPC()
        {
            InitializeComponent();
            //string mcuID = "0x0301";
        }

        public void ApplyLanguage()
        {
            CurLang = ShowLanguage.Current;
            this.Text = CurLang.MCUOptionValue;
            this.btSaveOpitonSet.Text = CurLang.SaveSettings;
            this.toolStripButton1.Text = CurLang.SaveOption;
            this.propertyGridEx_PC.ToolStrip.Items[0].ToolTipText = CurLang.Sorting;
            this.propertyGridEx_PC.ToolStrip.Items[1].ToolTipText = CurLang.Alphabetical;
            this.propertyGridEx_PC.ToolStrip.Items[4].ToolTipText = CurLang.PropertyPage;
            this.bt_NoSort.ToolTipText = CurLang.NoSort;
        }

        private void FrmPC_Load(object sender, EventArgs e)
        {
            OptionReflash();
        }
        #region Fill OPTION display

        public void OptionReflash()
        {
            string path = null;
            string str = null;
            String stv, stv0, stv1, stv2, stv3, stv4, stv5, stv6, stv7;
            stv = stv0 = stv1 = stv2 = stv3 = stv4 = stv5 = stv6 = stv7 = null;
            UInt32 option, option0, option1, option2, option3, option4, option5,option6,option7;
            option = option0 = option1 = option2 = option3 = option4 = option5 = option6 = option7 = 0;
            if (loadState == 0) //load old project
            {
                path = frmMAIN.APPLICATION_PATH + "global.ini";
                XElement rootNode = XElement.Load(path);

                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                                select myTarget;
                foreach (XElement xnode in myvalue)
                {
                    str = xnode.Attribute("Ppath").Value;
                    if (File.Exists(str) == false)
                    {
                        str = frmMAIN.APPLICATION_PATH + "Sample\\aaaa\\aaaa.Proj";
                        xnode.Attribute("Ppath").Value = str;
                    }
                }
                rootNode.Save(path);

                //search mcu id
                XElement rootNode0 = XElement.Load(str);

                mcuID = rootNode0.Attribute("ID").Value;

                lab_mcuName.Text = rootNode0.Attribute("MCU").Value;


                myvalue = from myTarget in rootNode0.Descendants("Option")
                          select myTarget;
                foreach (XElement xnode1 in myvalue)
                {
                    stv = xnode1.Attribute("Value").Value;

                    if (xnode1.Attribute("Value0") != null)
                    {
                        stv0 = xnode1.Attribute("Value0").Value;
                    }

                    if (xnode1.Attribute("Value1") != null)
                    {
                        stv1 = xnode1.Attribute("Value1").Value;
                    }

                    if (xnode1.Attribute("Value2") != null)
                    {
                        stv2 = xnode1.Attribute("Value2").Value;
                    }

                    if (xnode1.Attribute("Value3") != null)
                    {
                        stv3 = xnode1.Attribute("Value3").Value;
                    }

                    if (xnode1.Attribute("Value4") != null)
                    {
                        stv4 = xnode1.Attribute("Value4").Value;
                    }

                    if (xnode1.Attribute("Value5") != null)
                    {
                        stv5 = xnode1.Attribute("Value5").Value;
                    }

                    if (xnode1.Attribute("Value6") != null)
                    {
                        stv6 = xnode1.Attribute("Value6").Value;
                    }

                    if (xnode1.Attribute("Value7") != null)
                    {
                        stv7 = xnode1.Attribute("Value7").Value;
                    }
                }

                //rootNode0.Save(str);

                if (stv != "")
                {
                    option = Convert.ToUInt32(stv, 16);
                }

                if (stv0 != "")
                {
                    option0 = Convert.ToUInt32(stv0, 16);
                }

                if (stv1 != "")
                {
                    option1 = Convert.ToUInt32(stv1, 16);
                }

                if (stv2 != "")
                {
                    option2 = Convert.ToUInt32(stv2, 16);
                }

                if (stv3 != "")
                {
                    option3 = Convert.ToUInt32(stv3, 16);
                }

                if (stv4 != "")
                {
                    option4 = Convert.ToUInt32(stv4, 16);
                }

                if (stv5 != "")
                {
                    option5 = Convert.ToUInt32(stv5, 16);
                }

                if (stv6 != "")
                {
                    option6 = Convert.ToUInt32(stv6, 16);
                }

                if (stv7 != "")
                {
                    option7 = Convert.ToUInt32(stv7, 16);
                }

                FillPropertyGrid1(mcuID);
                //set option display
                MCU_Value2Opiton(mcuID, option, option0, option1, option2, option3, option4, option5,option6,option7);

                frmMAIN.OPTION = Convert.ToUInt32(option);
                frmMAIN.OPTION0 = Convert.ToUInt32(option0);
                frmMAIN.OPTION1 = Convert.ToUInt32(option1);
                frmMAIN.OPTION2 = Convert.ToUInt32(option2);
                frmMAIN.OPTION3 = Convert.ToUInt32(option3);
                frmMAIN.OPTION4 = Convert.ToUInt32(option4);
                frmMAIN.OPTION5 = Convert.ToUInt32(option5);
                frmMAIN.OPTION6 = Convert.ToUInt32(option6);
                frmMAIN.OPTION7 = Convert.ToUInt32(option7);
                optionValue.Text = stv;
                optionValue0.Text = stv0;
                optionValue1.Text = stv1;
                optionValue2.Text = stv2;
                optionValue3.Text = stv3;
                optionValue4.Text = stv4;
                optionValue5.Text = stv5;
                optionValue6.Text = stv6;
                optionValue7.Text = stv7;
                propertyGridEx_PC.Refresh();
            }
            else //new project 
            {
                str = frmMAIN.APPLICATION_PATH + "\\reg\\OTP.cfg";
                //string xmlPath = 
                XElement rootNode2 = XElement.Load(str);
                IEnumerable<XElement> myvalue2 = from myTarget in rootNode2.Descendants("MCU")
                                                 where (string)myTarget.Attribute("id").Value == mcuID
                                                 select myTarget;

                // IEnumerable<XElement> chipcfg = from srootNode in myvalue.Descendants("sys") select srootNode;
                foreach (XElement xnode2 in myvalue2)
                {
                    lab_mcuName.Text = xnode2.Attribute("name").Value;
                }
                FillPropertyGrid1(mcuID);
            }

            // FillPropertyGrid1(mcuID);
        }
        private void MCU_Value2Opiton(string mcuID, uint option, uint option0, uint option1, uint option2, uint option3, uint option4, uint option5,uint option6,uint option7)
        {
            switch (mcuID)
            {
                case "0x0301":
                case "0x0311":
                    MC30P01_Value2Option(option);
                    break;
                case "0x3401":
                    MC34P01_Value2Option(option);
                    break;
                case "0x3111":
                    MC31P11_Value2Option(option);
                    break;
                case "0x0314":
                    MC30P44_Value2Option(option);
                    break;
                case "0x3221":
                    MC32P21_Value2Option(option);
                    break;
                case "0x32821":
                    MC32P821_Value2Option(option);
                    break;
                case "0x3264":
                    MC32P64_Value2Option(option);
                    break;
                case "0x3378":
                    MC33P78_Value2Option(option);
                    break;
                case "0x3316":
                    MC33P116_Value2Option(option);
                    break;
                case "0x3512":
                    MC33P116_Value2Option(option);
                    break;
                case "0x7212":
                    MC32P7212_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x9902":
                    MC9902_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x7333":
                case "0x9903":
                case "0x9904":
                    MC32T7333_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x7122":
                    MC32F7122_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x3220":
                    MC32P21_Value2Option(option);
                    break;
                case "0x3394":
                    MC33P94_Value2Option(option);
                    break;
                //---ADC--------------------------------------
                case "0x0224":
                    MC20P24B_Value2Option(option);
                    break;
                case "0x0716":
                    MC20P24B_Value2Option(option);
                    break;
                case "0x0222":
                    MC20P22_Value2Option(option);
                    break;
                case "0x0238":
                    MC20P38_Value2Option(option);
                    break;
                //---GPIO---------------------------------------
                case "0x0201":
                    MC20P01_Value2Option(option);
                    break;
                case "0x0281":
                    MC20P801_Value2Option(option);
                    break;
                case "0x0204":
                case "0x0202":
                    MC20P04_Value2Option(option);
                    break;
                //----IROUT------------------------------
                case "0x0101":
                    MC10P01_Value2Option(option);
                    break;
                case "0x0111":
                    MC10P11_Value2Option(option);
                    break;
                case "0x0102":
                    MC10P02_Value2Option(option);
                    break;
                case "0x7510":// TODO wj
                    MC32P7510_Value2Option(option);
                    break;
                case "0x8132":
                    MC32T8132_Value2Option(option);
                    break;
                case "0x5312":
                    MC32P5312_Value2Option(option);
                    break;
                case "0x7022":
                    MC32P7022_Value2Option(option);
                    break;
                case "0x7311":
                case "0x7321":
                    MC32P7311_Value2Option(option);
                    break;
                case "0x7011":
                    MC32P7011_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x7031":
                    MC32P7031_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x7041":
                case "0x9905":
                case "0x6021":
                case "0x2722":
                    MC32P7041_Value2Option(option, option0, option1, option2, option3, option4, option5,option6,option7);
                    break;
                case "0x6060":
                    MC30P6060_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x7511":
                    MC32P7511_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x5222":
                    MC32P5212_Value2Option(option0, option2);
                    break;
                case "0x7323":
                case "0x7312":
                    MC32P7323_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x6080":
                    MC30P6080_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                case "0x6220":
                    MC30P6220_Value2Option(option, option0, option1, option2, option3, option4, option5);
                    break;
                //----------------------------------
                default:
                    break;
            }
        }
        private void FillPropertyGrid1(string mcuID)
        {
            if (mcuID == "0x7011" || mcuID == "0x7031" || mcuID == "0x5222" || mcuID == "0x7323" || mcuID == "0x7312" || mcuID == "0x6080" || mcuID == "0x6220")
            {
                optionValue.Enabled = false;
                optionValue0.Enabled = false;
                optionValue1.Enabled = false;
                optionValue2.Enabled = false;
                optionValue3.Enabled = false;
                optionValue4.Enabled = false;
                optionValue5.Enabled = false;
            }
            else if (mcuID == "0x7511")
            {
                optionValue.Enabled = false;
                optionValue0.Enabled = true;
                optionValue1.Enabled = true;
                optionValue2.Enabled = true;
                optionValue3.Enabled = true;
                optionValue4.Enabled = true;
                optionValue5.Enabled = true;
            }
            else
            {
                optionValue.Enabled = true;
                optionValue0.Enabled = true;
                optionValue1.Enabled = true;
                optionValue2.Enabled = true;
                optionValue3.Enabled = true;
                optionValue4.Enabled = true;
                optionValue5.Enabled = true;
            }
            switch (mcuID)
            {
                case "0x0301":
                    MC30P01_OptionFill();
                    MC30P01_Option2Value();
                    break;
                case "0x3401":
                    MC34P01_OptionFill();
                    MC34P01_Option2Value();
                    break;
                case "0x0311":
                    MC30P01_OptionFill();
                    MC30P01_Option2Value();
                    break;
                case "0x3111":
                    MC31P11_OptionFill();
                    MC31P11_Option2Value();
                    break;
                case "0x0314":
                    MC30P44_OptionFill();
                    MC30P44_Option2Value();
                    break;
                //----------------------------------------
                case "0x3221":
                    MC32P21_OptionFill();
                    MC32P21_Option2Value();
                    break;
                case "0x32821":
                    MC32P821_OptionFill();
                    MC32P821_Option2Value();
                    break;
                case "0x3264":
                    MC32P64_OptionFill();
                    MC32P64_Option2Value();
                    break;
                case "0x3378":
                    MC33P78_OptionFill();
                    MC33P78_Option2Value();
                    break;
                case "0x3316":
                    MC33P116_OptionFill();
                    MC33P116_Option2Value();
                    break;
                case "0x5312":
                    MC32P5312_OptionFill();
                    MC32P5312_Option2Value();
                    break;
                case "0x7212":
                    MC32P7212_OptionFill();
                    MC32P7212_Option2Value();
                    break;
                case "0x9902":
                    MC9902_OptionFill();
                    MC9902_Option2Value();
                    break;
                case "0x7333":
                case "0x9903":
                case "0x9904":
                    MC32T7333_OptionFill();
                    MC32T7333_Option2Value();
                    break;
                case "0x7122":
                    MC32F7122_OptionFill();
                    MC32F7122_Option2Value();
                    break;
                case "0x3220":
                    MC32P21_OptionFill();
                    MC32P21_Option2Value();
                    break;
                case "0x3394":
                    MC33P94_OptionFill();
                    MC33P94_Option2Value();
                    break;
                //--ADC ------------------------------------------
                case "0x0224":
                    MC20P24B_OptionFill();
                    MC20P24B_Option2Value();
                    break;
                case "0x0716":
                    MC20P24B_OptionFill();
                    MC20P24B_Option2Value();
                    break;
                case "0x0222":
                    MC20P22_OptionFill();
                    MC20P22_Option2Value();
                    break;
                case "0x0238":
                    MC20P38_OptionFill();
                    MC20P38_Option2Value();
                    break;
                //---GPIO-----------------------------------------------
                case "0x0201":
                    MC20P01_OptionFill();
                    MC20P01_Option2Value();
                    break;
                case "0x0281":
                    MC20P801_OptionFill();
                    MC20P801_Option2Value();
                    break;
                case "0x0204":
                case "0x0202":
                    MC20P04_OptionFill();
                    MC20P04_Option2Value();
                    break;
                //---IROUT-----------------------------------------------
                case "0x0101":
                    MC10P01_OptionFill();
                    MC10P01_Option2Value();
                    break;
                case "0x0111":
                    MC10P11_OptionFill();
                    MC10P11_Option2Value();
                    break;
                case "0x0102":
                    MC10P02_OptionFill();
                    MC10P02_Option2Value();
                    break;
                case "0x7022":// TODO wj
                    MC32P7022_OptionFill();
                    MC32P7022_Option2Value();
                    break;
                case "0x6060":// TODO wj
                    MC30P6060_OptionFill();
                    MC30P6060_Option2Value();
                    break;
                case "0x7510":// TODO wj
                    MC32P7510_OptionFill();
                    MC32P7510_Option2Value();
                    break;
                case "0x3222":// TODO wj
                    MC32E22_OptionFill();
                    MC32E22_Option2Value();
                    break;
                case "0x8132":// TODO wj
                    MC32T8132_OptionFill();
                    MC32T8132_Option2Value();
                    break;
                case "0x7311":// TODO wj
                case "0x7321":
                    MC32P7311_OptionFill();
                    MC32P7311_Option2Value();
                    break;
                case "0x7011":// TODO wj
                    MC32P7011_OptionFill();
                    MC32P7011_Option2Value();
                    break;
                case "0x7031":// TODO wj
                    MC32P7031_OptionFill();
                    MC32P7031_Option2Value();
                    break;
                case "0x7041":// TODO wj
                case "0x9905":
                case "0x6021":
                case "0x2722":
                    MC32P7041_OptionFill();
                    MC32P7041_Option2Value();
                    break;
                case "0x7511":// TODO wj
                    MC32P7511_OptionFill();
                    MC32P7511_Option2Value();
                    break;
                case "0x5222":// TODO wj
                    MC32P5212_OptionFill();
                    MC32P5212_Option2Value();
                    break;
                case "0x7323":// TODO wj
                case "0x7312":
                    MC32P7323_OptionFill();
                    MC32P7323_Option2Value();
                    break;
                case "0x6080":// TODO wj
                    MC30P6080_OptionFill();
                    MC30P6080_Option2Value();
                    break;
                case "0x6220":// TODO LYL 0411
                    MC30P6220_OptionFill();
                    MC30P6220_Option2Value();
                    break;
                //-------------------------------------------
                default:
                    //MC30P01_OptionFill();
                    //MC30P01_Option2Value();
                    MessageBox.Show("there is not any comfort option fille ");
                    break;
            }
            propertyGridEx_PC.Refresh();
        }
        //protected bool bWDTE;
        //protected bool bRESE;
        //protected bool bLVRE;
        //protected bool bLVRS;
        //protected bool bRSTE;
        //protected bool bPROTECT;
        //protected bool bSMTEN;
        //               bFILSEL


        private void MC30P01_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] borsel = new string[] { "000:1.5V", "001:1.8V", "010:2.0V", "011:2.2V", "100:2.4V", "101:2.6V", "110:3.0V", "111:3.6V" };
            string[] FOSC = new string[] { "111:8MHz", "110:4MHz", "101:2MHz", "100:1MHz", CurLang.Retain011, "010:444K", CurLang.Retain001, CurLang.Retain000 };
            string[] TWDT = new string[] { "PWRT=TWDT=18ms", "PWRT=TWDT=4.5ms", "PWRT=TWDT=288ms", "PWRT=TWDT=72ms", "PWRT=140us,TWDT=18ms",
                 "PWRT=140us,TWDT=4.5ms", "PWRT=140us,TWDT=288ms","PWRT=140us,TWDT=72ms"};
            string[] FCPU = new string[] { "1-4T", "0-2T" };
            string[] OSCM = new string[] { CurLang.OSCM11, CurLang.OSCM10, CurLang.OSCM01, CurLang.OSCM00 };

            propertyGridEx_PC.Item.Add("borsel", "000:1.5V", false, CurLang.BorselFunc, CurLang.BorselFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(borsel, true);

            propertyGridEx_PC.Item.Add("MCLRE", ref ab, "RSTE", false, CurLang.MCLREFunc, CurLang.MCLREFuncExplain1 + "\n\t" + CurLang.MCLREFuncExplain2 + "\n\t" + CurLang.MCLREFuncExplain3, true);

            propertyGridEx_PC.Item.Add("FOSC", "111:8MHz", false, CurLang.FOSCFunc, CurLang.FOSCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FOSC, true);

            propertyGridEx_PC.Item.Add("FCPU", "1-4T", false, CurLang.FCPUFunc, CurLang.FCPUFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("TWDT", "PWRT=TWDT=18ms", false, CurLang.TWDTFunc, CurLang.TWDTFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(TWDT, true);

            propertyGridEx_PC.Item.Add("WDTE", ref ab, "WDT", false, CurLang.WDTEFunc, CurLang.WDTEFuncExplain1 + "\n\t" + CurLang.WDTEFuncExplain2 + "\n\t" + CurLang.WDTEFuncExplain3, true);

            propertyGridEx_PC.Item.Add("RDPORT", ref ab, "LVRE", false, CurLang.RDPORTFunc, CurLang.RDPORTFuncExplain1 + "\n\t" + CurLang.RDPORTFuncExplain2 + "\n\t" + CurLang.RDPORTFuncExplain3, true);

            propertyGridEx_PC.Item.Add("SMTEN", ref ab, "SMTEN", false, CurLang.SMTENFunc, CurLang.SMTENFuncExplain1 + "\n\t" + CurLang.SMTENFuncExplain2 + "\n\t" + CurLang.SMTENFuncExplain3, true);

            propertyGridEx_PC.Item.Add("MCUtype", ref ab, "LVRS", false, CurLang.MCUtypeFunc, CurLang.MCUtypeFuncExplain1 + "\n" + CurLang.MCUtypeFuncExplain2 + "\n" + CurLang.MCUtypeFuncExplain3, true);

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCM10, false, CurLang.OSCMFunc, CurLang.OSCMFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("CP", ref ab, "PROTECT", false, CurLang.CPFunc, CurLang.CPFuncExplain, true);

        }

        private void MC30P44_OptionFill()
        {
            propertyGridEx_PC.ShowCustomProperties = true;
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
            propertyGridEx_PC.Item.Clear();

            object ab = propertyGridEx_PC;

            string[] LVRS = new string[] { "000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", CurLang.LVRSString7 };
            string[] WDTC = new string[] { CurLang.WDTCString1, CurLang.WDTCString2, CurLang.WDTCString3 };
            string[] WDTT = new string[] { "00:PWRT=TWDT=4.5ms", "01:PWRT=TWDT=18ms", "10:PWRT=TWDT=72ms", "11:PWRT=TWDT=288ms" };
            string[] FCPU = new string[] { "11:Fosc//16", "10:Fosc//8", "01:Fosc//4", "00:Fosc//2" };
            string[] OSCM = new string[] { CurLang.OSCMString1, CurLang.OSCMString2, CurLang.OSCMString3, CurLang.OSCMString4 };

            propertyGridEx_PC.Item.Add("OSCM", CurLang.OSCMString1, false, CurLang.OSCMPCFunc, CurLang.OSCMPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(OSCM, true);

            propertyGridEx_PC.Item.Add("FCPU", "11:Fosc//16", false, CurLang.FCPUPCFunc, CurLang.FCPUPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(FCPU, true);

            propertyGridEx_PC.Item.Add("LVRS", CurLang.LVRSString7, false, CurLang.LVRSPCFunc, CurLang.LVRSPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(LVRS, true);

            propertyGridEx_PC.Item.Add("MCLRE", ref ab, "RSTE", false, CurLang.MCLREPCFunc, CurLang.MCLREPCFuncExplain, true);

            propertyGridEx_PC.Item.Add("WDTC", CurLang.WDTCString3, false, CurLang.WDTCPCFunc, CurLang.WDTCPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTC, true);

            propertyGridEx_PC.Item.Add("WDTT", "11:PWRT=TWDT=288ms", false, CurLang.WDTTPCFunc, CurLang.WDTTPCFuncExplain, true);
            propertyGridEx_PC.Item[propertyGridEx_PC.Item.Count - 1].Choices = new CustomChoices(WDTT, true);

            propertyGridEx_PC.Item.Add("NECR", ref ab, "PROTECT", false, CurLang.CPFunc, CurLang.CPFuncExplain, true);

        }

        #endregion

        private void bt_NoSort_Click(object sender, EventArgs e)
        {
            propertyGridEx_PC.PropertySort = PropertySort.NoSort;
        }
        #region property changed funtion
        private void Properties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            switch (mcuID)
            {
                case "0x0301":
                    MC30P01_Option2Value();
                    break;
                case "0x3401":
                    MC34P01_Option2Value();
                    break;
                case "0x0311":
                    MC30P01_Option2Value();
                    break;
                case "0x3111":
                    MC31P11_Option2Value();
                    break;
                case "0x0314":
                    MC30P44_Option2Value();
                    break;
                case "0x3221":
                    MC32P21_Option2Value();
                    break;
                case "0x32821":
                    MC32P821_Option2Value();
                    break;
                case "0x3264":
                    MC32P64_Option2Value();
                    break;
                case "0x3378":
                    MC33P78_Option2Value();
                    break;
                case "0x3316":
                    if (e.ChangedItem.Label == "OSCM")
                    {
                        if ((string)e.ChangedItem.Value == CurLang.OSCMMC33P116String2)
                        {
                            MessageBox.Show("仿真芯片此选项无法在休眠下扫描端口，实际芯片支持!");
                        }
                    }
                    MC33P116_Option2Value();
                    break;
                case "0x5312":
                    MC32P5312_Option2Value();
                    break;
                case "0x7212":
                    MC32P7212_Option2Value();
                    break;
                case "0x9902":
                    MC9902_Option2Value();
                    break;
                case "0x7333":
                case "0x9903":
                case "0x9904":
                    MC32T7333_Option2Value();
                    break;
                case "0x7122":
                    MC32F7122_Option2Value();
                    break;
                case "0x3220":
                    MC32P21_Option2Value();
                    break;
                case "0x3394":
                    MC33P94_Option2Value();
                    break;
                //----ADC----------------------------------
                case "0x0224":
                    MC20P24B_Option2Value();
                    break;
                case "0x0716":
                    MC20P24B_Option2Value();
                    break;
                case "0x0222":
                    MC20P22_Option2Value();
                    break;
                case "0x0238":
                    MC20P38_Option2Value();
                    break;
                //----GPIO----------------------------------
                case "0x0201":
                    MC20P01_Option2Value();
                    break;
                case "0x0281":
                    MC20P801_Option2Value();
                    break;
                case "0x0204":
                case "0x0202":
                    MC20P04_Option2Value();
                    break;
                //----IROUT----------------------------------
                case "0x0101":
                    MC10P01_Option2Value();
                    break;
                case "0x0111":
                    MC10P11_Option2Value();
                    break;
                case "0x0102":
                    MC10P02_Option2Value();
                    break;
                case "0x7022":// TODO wj
                    MC32P7022_Option2Value();
                    break;
                case "0x6060":// TODO wj
                    MC30P6060_Option2Value();
                    break;
                case "0x7510":// TODO wj
                    MC32P7510_Option2Value();
                    break;
                case "0x3222":// TODO wj
                    MC32E22_Option2Value();
                    break;
                case "0x8132":// TODO wj
                    MC32T8132_Option2Value();
                    break;
                case "0x7311":// TODO wj
                case "0x7321":
                    MC32P7311_Option2Value();
                    break;
                case "0x7011":// TODO wj
                    MC32P7011_Option2Value();
                    break;
                case "0x7031":// TODO wj
                    MC32P7031_Option2Value();
                    break;
                case "0x7041":// TODO wj
                case "0x9905"://TODO LYL For MC9905
                case "0x6021":
                case "0x2722":
                    MC32P7041_Option2Value();
                    break;
                case "0x7511":// TODO wj
                    MC32P7511_Option2Value();
                    break;
                case "0x5222":// TODO wj
                    MC32P5212_Option2Value();
                    break;
                case "0x7323":// TODO wj
                case "0x7312":
                    MC32P7323_Option2Value();
                    break;
                case "0x6080":// TODO wj
                    MC30P6080_Option2Value();
                    break;
                case "0x6220":// TODO 
                    MC30P6220_Option2Value();
                    break;
                //-------------------------------------------
                default:
                    break;
            }
            if (optionValue.Text != "")
            {
                frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                frmMAIN.OPTIONNullFlag = false;
            }
            else
            {
                frmMAIN.OPTION = 0x00;
                frmMAIN.OPTIONNullFlag = true;
            }

            if (optionValue0.Text != "")
            {
                frmMAIN.OPTION0 = Convert.ToUInt32(optionValue0.Text, 16);
                frmMAIN.OPTION0NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION0 = 0x00;
                frmMAIN.OPTION0NullFlag = true;
            }

            if (optionValue1.Text != "")
            {
                frmMAIN.OPTION1 = Convert.ToUInt32(optionValue1.Text, 16);
                frmMAIN.OPTION1NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION1 = 0x00;
                frmMAIN.OPTION1NullFlag = true;
            }

            if (optionValue0.Text != "")
            {
                frmMAIN.OPTION2 = Convert.ToUInt32(optionValue0.Text, 16);
                frmMAIN.OPTION2NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION2 = 0x00;
                frmMAIN.OPTION2NullFlag = true;
            }

            if (optionValue1.Text != "")
            {
                frmMAIN.OPTION3 = Convert.ToUInt32(optionValue1.Text, 16);
                frmMAIN.OPTION3NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION3 = 0x00;
                frmMAIN.OPTION3NullFlag = true;
            }

            if (optionValue2.Text != "")
            {
                frmMAIN.OPTION4 = Convert.ToUInt32(optionValue2.Text, 16);
                frmMAIN.OPTION4NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION4 = 0x00;
                frmMAIN.OPTION4NullFlag = true;
            }

            if (optionValue5.Text != "")
            {
                frmMAIN.OPTION5 = Convert.ToUInt32(optionValue5.Text, 16);
                frmMAIN.OPTION5NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION5 = 0x00;
                frmMAIN.OPTION5NullFlag = true;
            }

            if (optionValue6.Text != "")
            {
                frmMAIN.OPTION6 = Convert.ToUInt32(optionValue6.Text, 16);
                frmMAIN.OPTION6NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION6 = 0x00;
                frmMAIN.OPTION6NullFlag = true;
            }
            if (optionValue7.Text != "")
            {
                frmMAIN.OPTION7 = Convert.ToUInt32(optionValue7.Text, 16);
                frmMAIN.OPTION7NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION7 = 0x00;
                frmMAIN.OPTION7NullFlag = true;
            }
        }

        private void MC30P01_Value2Option(uint pValue)
        {
            uint temp = 0;
            temp = (pValue >> 0) & 7;
            frmMAIN.FREQ = 0x08;
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
                    propertyGridEx_PC.Item[0].Value = CurLang.LVRSString7;
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
                    propertyGridEx_PC.Item[2].Value = CurLang.Retain011;// "011:WDTRC 28K";
                    break;
                case 4:
                    propertyGridEx_PC.Item[2].Value = "100:1MHz";
                    break;
                case 5:
                    propertyGridEx_PC.Item[2].Value = "101:2MHz";
                    break;
                case 6:
                    propertyGridEx_PC.Item[2].Value = "110:4MHz";
                    break;
                default:
                    propertyGridEx_PC.Item[2].Value = "111:8MHz";
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
                    propertyGridEx_PC.Item[4].Value = "PWRT=140us,TWDT=72ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = "PWRT=140us,TWDT=288ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[4].Value = "PWRT=140us,TWDT=4.5ms";
                    break;
                case 3:
                    propertyGridEx_PC.Item[4].Value = "PWRT=140us,TWDT=18ms";
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
            //RDPORT
            temp = (pValue >> 24) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = "False";
            }

            //SMTEN
            temp = (pValue >> 25) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[7].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[7].Value = "False";
            }

            //MCU TYPE
            temp = (pValue >> 26) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[8].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[8].Value = "False";
            }

            //"11:内部高频RC & RTC","10:内部高频RC","01:高频晶体振荡器","00:低频晶体振荡器"
            temp = (pValue >> 27) & 3;
            switch (temp)
            {
                case 3:
                    propertyGridEx_PC.Item[9].Value = CurLang.OSCM11;
                    break;
                case 2:
                    propertyGridEx_PC.Item[9].Value = CurLang.OSCM10;
                    break;
                case 1:
                    propertyGridEx_PC.Item[9].Value = CurLang.OSCM01;
                    break;
                default:
                    propertyGridEx_PC.Item[9].Value = CurLang.OSCM00;
                    break;

            }

            temp = (pValue >> 29) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[10].Value = "False";
            }
            else
            {
                propertyGridEx_PC.Item[10].Value = "True";
            }
        }
        private void MC30P01_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            frmMAIN.FREQ = 0x08;
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V"
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
            else if (temp == CurLang.Retain011) /*"011:WDTRC 28K":*/
            {
                optValue_Temp = optValue_Temp | (3 << 4);
            }
            else if (temp == "100:1MHz")
            {
                optValue_Temp = optValue_Temp | (4 << 4);
            }
            else if (temp == "101:2MHz")
            {
                optValue_Temp = optValue_Temp | (5 << 4);
            }
            else if (temp == "110:4MHz")
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
                case "PWRT=140us,TWDT=72ms":
                    optValue_Temp = optValue_Temp | (0 << 8);
                    break;
                case "PWRT=140us,TWDT=288ms":
                    optValue_Temp = optValue_Temp | (1 << 8);
                    break;
                case "PWRT=140us,TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (2 << 8);
                    break;
                case "PWRT=140us,TWDT=18ms":
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

            //RDPORT
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 24);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 24);
            }

            //SMTEN
            temp = propertyGridEx_PC.Item[7].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 25);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 25);
            }

            //MCU TYPE
            temp = propertyGridEx_PC.Item[8].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (0 << 26);
            }
            else
            {
                optValue_Temp = optValue_Temp | (1 << 26);
            }

            //"11:内部高频RC & RTC","10:内部高频RC","01:高频晶体振荡器","00:低频晶体振荡器"
            temp = propertyGridEx_PC.Item[9].Value.ToString();

            if (temp == CurLang.OSCM11)
            {
                optValue_Temp = optValue_Temp | (3 << 27);
            }
            else if (temp == CurLang.OSCM10)
            {
                optValue_Temp = optValue_Temp | (2 << 27);
            }
            else if (temp == CurLang.OSCM01)
            {
                optValue_Temp = optValue_Temp | (1 << 27);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 27);
            }

            temp = propertyGridEx_PC.Item[10].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (1 << 29);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 29);
            }

            optValue_Temp = optValue_Temp | (3 << 12);

            optValue_Temp = optValue_Temp | (0xff << 16);

            //optValue_Temp = optValue_Temp | (0x0f <<28);

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }
        private void MC30P44_Value2Option(uint pvalue)
        {
            //propertyGridEx_PC.Item[0].Value = "01:高频晶振模式";

            uint temp = pvalue & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMString4;
                    break;
                case 1:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMString3;
                    break;
                case 2:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMString2;
                    break;
                default:
                    propertyGridEx_PC.Item[0].Value = CurLang.OSCMString1;
                    break;
            }

            temp = (pvalue >> 2) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[1].Value = "00:Fosc//2";
                    break;
                case 1:
                    propertyGridEx_PC.Item[1].Value = "01:Fosc//4";
                    break;
                case 2:
                    propertyGridEx_PC.Item[1].Value = "10:Fosc//8";
                    break;
                default:
                    propertyGridEx_PC.Item[1].Value = "11:Fosc//16";
                    break;
            }

            temp = (pvalue >> 4) & 7;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[2].Value = "000:1.5V";
                    break;
                case 1:
                    propertyGridEx_PC.Item[2].Value = "001:1.8v";
                    break;
                case 2:
                    propertyGridEx_PC.Item[2].Value = "010:2.0V";
                    break;
                case 3:
                    propertyGridEx_PC.Item[2].Value = "011:2.2v";
                    break;
                case 4:
                    propertyGridEx_PC.Item[2].Value = "100:2.4v";
                    break;
                case 5:
                    propertyGridEx_PC.Item[2].Value = "101:2.6v";
                    break;
                default:
                    propertyGridEx_PC.Item[2].Value = CurLang.LVRSString7;
                    break;

            }

            temp = (pvalue >> 7) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[3].Value = "True";
            }
            else
            {
                propertyGridEx_PC.Item[3].Value = "False";
            }

            temp = (pvalue >> 8) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[4].Value = CurLang.WDTCString1;
                    break;
                case 1:
                    propertyGridEx_PC.Item[4].Value = CurLang.WDTCString2;
                    break;

                default:
                    propertyGridEx_PC.Item[4].Value = CurLang.WDTCString3;
                    break;
            }

            temp = (pvalue >> 10) & 3;
            switch (temp)
            {
                case 0:
                    propertyGridEx_PC.Item[5].Value = "00:PWRT=TWDT=4.5ms";
                    break;
                case 1:
                    propertyGridEx_PC.Item[5].Value = "01:PWRT=TWDT=18ms";
                    break;
                case 2:
                    propertyGridEx_PC.Item[5].Value = "10:PWRT=TWDT=72ms";
                    break;

                default:
                    propertyGridEx_PC.Item[5].Value = "11:PWRT=TWDT=288ms";
                    break;
            }
            temp = (pvalue >> 15) & 1;
            if (temp == 1)
            {
                propertyGridEx_PC.Item[6].Value = "False";
            }
            else
            {
                propertyGridEx_PC.Item[6].Value = "True";
            }

        }
        private void MC30P44_Option2Value()
        {
            Int32 optValue_Temp = 0;

            string temp = propertyGridEx_PC.Item[0].Value.ToString();
            //"11:内部8MHZ & RTC", "10:内部8MHZ", "01:高频晶振模式","00:低频晶振模式"
            if (temp == CurLang.OSCMString4)
            {
                optValue_Temp = optValue_Temp | (0 << 0);
            }
            else if (temp == CurLang.OSCMString3)
            {
                optValue_Temp = optValue_Temp | (1 << 0);
            }
            else if (temp == CurLang.OSCMString2)
            {
                optValue_Temp = optValue_Temp | (2 << 0);
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << 0);
            }

            //FCPUS
            temp = propertyGridEx_PC.Item[1].Value.ToString();
            //"11:Fosc//16","10:Fosc//8","01:Fosc//4" ,"00:Fosc//2" 
            switch (temp)
            {
                case "00:Fosc//2":
                    optValue_Temp = optValue_Temp | (0 << 2);
                    break;
                case "01:Fosc//4":
                    optValue_Temp = optValue_Temp | (1 << 2);
                    break;
                case "10:Fosc//8":
                    optValue_Temp = optValue_Temp | (2 << 2);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (3 << 2);
                    break;
            }
            //LVRS
            temp = propertyGridEx_PC.Item[2].Value.ToString();
            //"000:1.5V", "001:1.8v", "010:2.0V", "011:2.2v", "100:2.4V", "101:2.6V", "其它:3.6V" 
            switch (temp)
            {
                case "000:1.5V":
                    optValue_Temp = optValue_Temp | (0 << 4);
                    break;
                case "001:1.8v":
                    optValue_Temp = optValue_Temp | (1 << 4);
                    break;
                case "010:2.0V":
                    optValue_Temp = optValue_Temp | (2 << 4);
                    break;
                case "011:2.2v":
                    optValue_Temp = optValue_Temp | (3 << 4);
                    break;
                case "100:2.4V":
                    optValue_Temp = optValue_Temp | (4 << 4);
                    break;
                case "101:2.6V":
                    optValue_Temp = optValue_Temp | (5 << 4);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (7 << 4);
                    break;
            }

            //mclre
            temp = propertyGridEx_PC.Item[3].Value.ToString();
            if (temp == "True")
            {
                optValue_Temp = optValue_Temp | (1 << 7);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 7);
            }

            //WDTC
            temp = propertyGridEx_PC.Item[4].Value.ToString();
            if (temp == CurLang.WDTCString1)
            {
                optValue_Temp = optValue_Temp | (0 << 8);
            }
            else if (temp == CurLang.WDTCString2)
            {
                optValue_Temp = optValue_Temp | (1 << 8);
            }
            else
            {
                optValue_Temp = optValue_Temp | (3 << 8);
            }

            temp = propertyGridEx_PC.Item[5].Value.ToString();
            switch (temp)
            {
                //"00:PWRT=TWDT=4.5ms", "01:PWRT=TWDT=18ms", "10:PWRT=TWDT=72ms", "11:PWRT=TWDT=288ms"
                case "00:PWRT=TWDT=4.5ms":
                    optValue_Temp = optValue_Temp | (0 << 10);
                    break;
                case "01:PWRT=TWDT=18ms":
                    optValue_Temp = optValue_Temp | (1 << 10);
                    break;
                case "10:PWRT=TWDT=72ms":
                    optValue_Temp = optValue_Temp | (2 << 10);
                    break;
                default:
                    optValue_Temp = optValue_Temp | (3 << 10);
                    break;
            }


            // bit14--12  reserve
            optValue_Temp = optValue_Temp | (7 << 12);

            //PROTECT
            temp = propertyGridEx_PC.Item[6].Value.ToString();
            if (temp == "False")
            {
                optValue_Temp = optValue_Temp | (1 << 15);
            }
            else
            {
                optValue_Temp = optValue_Temp | (0 << 15);
            }

            optionValue.Text = Convert.ToString(optValue_Temp, 16);

        }


        #endregion


        #region bt OK OR CANCEL
        private void option_value_change(object sender, EventArgs e)
        {
            if (optionValue.Text != "")
            {
                frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
            }

            if (optionValue0.Text != "")
            {
                frmMAIN.OPTION0 = Convert.ToUInt32(optionValue0.Text, 16);
            }

            if (optionValue1.Text != "")
            {
                frmMAIN.OPTION1 = Convert.ToUInt32(optionValue1.Text, 16);
            }

            if (optionValue0.Text != "")
            {
                frmMAIN.OPTION2 = Convert.ToUInt32(optionValue0.Text, 16);
            }

            if (optionValue1.Text != "")
            {
                frmMAIN.OPTION3 = Convert.ToUInt32(optionValue1.Text, 16);
            }

            if (optionValue2.Text != "")
            {
                frmMAIN.OPTION4 = Convert.ToUInt32(optionValue2.Text, 16);
            }

            if (optionValue5.Text != "")
            {
                frmMAIN.OPTION5 = Convert.ToUInt32(optionValue5.Text, 16);
            }
        }


        private void bt_OK(object sender, EventArgs e)
        {
            if (optionValue.Text != "")
            {
                frmMAIN.OPTION = Convert.ToUInt32(optionValue.Text, 16);
                frmMAIN.OPTIONNullFlag = false;
            }
            else
            {
                frmMAIN.OPTION = 0x00;
                frmMAIN.OPTIONNullFlag = true;
            }

            if (optionValue0.Text != "")
            {
                frmMAIN.OPTION0 = Convert.ToUInt32(optionValue0.Text, 16);
                frmMAIN.OPTION0NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION0 = 0x00;
                frmMAIN.OPTION0NullFlag = true;
            }

            if (optionValue1.Text != "")
            {
                frmMAIN.OPTION1 = Convert.ToUInt32(optionValue1.Text, 16);
                frmMAIN.OPTION1NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION1 = 0x00;
                frmMAIN.OPTION1NullFlag = true;
            }

            if (optionValue2.Text != "")
            {
                frmMAIN.OPTION2 = Convert.ToUInt32(optionValue2.Text, 16);
                frmMAIN.OPTION2NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION2 = 0x00;
                frmMAIN.OPTION2NullFlag = true;
            }

            if (optionValue3.Text != "")
            {
                frmMAIN.OPTION3 = Convert.ToUInt32(optionValue3.Text, 16);
                frmMAIN.OPTION3NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION3 = 0x00;
                frmMAIN.OPTION3NullFlag = true;
            }

            if (optionValue4.Text != "")
            {
                frmMAIN.OPTION4 = Convert.ToUInt32(optionValue4.Text, 16);
                frmMAIN.OPTION4NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION4 = 0x00;
                frmMAIN.OPTION4NullFlag = true;
            }

            if (optionValue5.Text != "")
            {
                frmMAIN.OPTION5 = Convert.ToUInt32(optionValue5.Text, 16);
                frmMAIN.OPTION5NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION5 = 0x00;
                frmMAIN.OPTION5NullFlag = true;
            }

            if (optionValue6.Text != "")
            {
                frmMAIN.OPTION6 = Convert.ToUInt32(optionValue6.Text, 16);
                frmMAIN.OPTION6NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION6 = 0x00;
                frmMAIN.OPTION6NullFlag = true;
            }

            if (optionValue7.Text != "")
            {
                frmMAIN.OPTION7 = Convert.ToUInt32(optionValue7.Text, 16);
                frmMAIN.OPTION7NullFlag = false;
            }
            else
            {
                frmMAIN.OPTION7 = 0x00;
                frmMAIN.OPTION7NullFlag = true;
            }

            if (loadState == 1) //new project
            {
                //Button okbt = new Button();
                this.DialogResult = DialogResult.OK;
            }
            else //change opiton when debug
            {
                string srt = frmMAIN.APPLICATION_PATH + "global.ini";
                string path = null;
                XElement rootNode = XElement.Load(srt);

                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("ProjectPath")
                                                select myTarget;
                foreach (XElement xnode in myvalue)
                {
                    path = xnode.Attribute("Ppath").Value;
                }

                rootNode.Save(srt);

                //write option value
                rootNode = XElement.Load(path);
                myvalue = from myTarget in rootNode.Descendants("Option")
                          select myTarget;
                foreach (XElement xnode in myvalue)
                {
                    if (xnode.Attribute("Value") == null)
                    {
                        xnode.Attribute("Value").Value = optionValue.Text;
                    }
                    else
                    {
                        xnode.Attribute("Value").Value = optionValue.Text;
                    }

                    if (xnode.Attribute("Value0") == null)
                    {
                        xnode.Add(new XAttribute("Value0", optionValue0.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value0").Value = optionValue0.Text;
                    }

                    if (xnode.Attribute("Value1") == null)
                    {
                        xnode.Add(new XAttribute("Value1", optionValue1.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value1").Value = optionValue1.Text;
                    }

                    if (xnode.Attribute("Value2") == null)
                    {
                        xnode.Add(new XAttribute("Value2", optionValue2.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value2").Value = optionValue2.Text;
                    }

                    if (xnode.Attribute("Value3") == null)
                    {
                        xnode.Add(new XAttribute("Value3", optionValue3.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value3").Value = optionValue3.Text;
                    }

                    if (xnode.Attribute("Value4") == null)
                    {
                        xnode.Add(new XAttribute("Value4", optionValue4.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value4").Value = optionValue4.Text;
                    }

                    if (xnode.Attribute("Value5") == null)
                    {
                        xnode.Add(new XAttribute("Value5", optionValue5.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value5").Value = optionValue5.Text;
                    }

                    if (xnode.Attribute("Value6") == null)
                    {
                        xnode.Add(new XAttribute("Value6", optionValue6.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value6").Value = optionValue6.Text;
                    }

                    if (xnode.Attribute("Value7") == null)
                    {
                        xnode.Add(new XAttribute("Value7", optionValue7.Text));
                    }
                    else
                    {
                        xnode.Attribute("Value7").Value = optionValue7.Text;
                    }
                }

                rootNode.Save(path);

            }

        }
        private void bt_Cancel(object sender, EventArgs e)
        {
            if (loadState == 1) //new project
            {
                //Button okbt = new Button();
                this.DialogResult = DialogResult.Cancel;
            }
            else //change opiton when debug
            {
            }
        }
        #endregion


    }
}
