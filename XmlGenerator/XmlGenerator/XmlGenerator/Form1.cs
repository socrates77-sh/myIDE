
//#define  xml_hengnai
//#define xml_zhongji
#define all
#define mc31p11
//#define mc33p716
//#define mc10p716
#define gm20p38
#define mc32p64
#define gm20p04
#define mc32p821
#define mc33p78
#define mc33p116
#define  mc34p01
#define mc32p5312
#define mc32p7212
#define mc32pa300



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using PropertyGridEx;



namespace XmlGenerator
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
            //XML文件保存路径
            string xxmlPath = "F:\\Emulator\\IDE\\SinoSunIDE\\XmlGenerator\\XmlGenerator\\XmlGenerator\\bin\\Debug\\OTP.cfg";
            //生成XML文件

#if xml_hengnai

            xml_great_hn(xxmlPath);
#elif   xml_zhongji
            xml_great(xxmlPath);
#else
            xml_great(xxmlPath);
#endif
             custominf();
            //GetXmlNodeInformation(xxmlPath);

            //显示OPTION属性
            //MCU_OptionSet mc301 = new MCU_OptionSet();
            //propertyGrid_option.Text = "MC30P01 OPTION";
            FillPropertyGrid1(FilterPropertyType.None);
            
        }

        
         
        private void xml_great(string xmlPath)
        {
            try
            {   //定义XDocoument结构
                XDocument myXDoc = new XDocument(
                 new XDeclaration("1.01","utf-8","yes"),
                 new XComment("mcu reg config "),
                 new XComment("add mc32p64 @2013.06.19"),
                 new XComment("add mc32e22 @2013.03.13"),
                 new XComment("fixed mc30xx Frmsys SP & A items @2012.12.13"),
                 new XComment("expand other chips as mc20p10,mc10p01b,mc10p11b,mc20p801,mc10p02,mc20p38,mc20p04 @2012.10.25 "),
                 new XComment("expand evchip addr to 32bit @2012.10.25 "),
                 new XElement("WinScope",new XAttribute("Name","confige"),new XAttribute("Version","1.04"),
                     new XAttribute("Author","Mike.Mo"),new XAttribute("Date","2013-07-14"),

                    new XElement("MCUS",
                        //mc30p011
                        new XElement("MCU", new XAttribute("name", "mc30p011"),new XAttribute("type","RISC14"), new XAttribute("id", "0x0311"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize", "0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x004f"),
                        new XAttribute("chip_ramSize", "0x50")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x0000"),
                        new XAttribute("ev_romEndAddr", "0x07ff"),
                        new XAttribute("ev_ramFirstAddr", "0x8000"),
                        new XAttribute("ev_ramEndAddr", "0x804f"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9039"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x03ff"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the Second stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the third stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fourth stack addr ")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fifth stack addr")),
                            new XElement("sys", new XAttribute("name", "STATUS(7).RST"), new XAttribute("value", "0"), new XAttribute("describe", "唤醒源标志：1=P6变化唤醒；0=其它复位唤醒")),
                            new XElement("sys", new XAttribute("name", "STATUS(6).GP1"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(5).GP0"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(4).TO"), new XAttribute("value", "0"), new XAttribute("describe", "超时位")),
                            new XElement("sys", new XAttribute("name", "STATUS(3).PD"), new XAttribute("value", "0"), new XAttribute("describe", "掉电位")),
                             new XElement("sys", new XAttribute("name", "STATUS(2).Z"), new XAttribute("value", "0"), new XAttribute("describe", "结果为0标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(1).DC"), new XAttribute("value", "0"), new XAttribute("describe", "半进位/借位标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(0).C"), new XAttribute("value", "0"), new XAttribute("describe", "进位/借位标志位"))

                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "STATUS"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "FSR"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "GPR"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "PCLATH"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "PDCON"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "ODCON"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "PUCON"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "INTECON"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "INTFLAG"), new XAttribute("addr", "0x0F")),

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x41")),

                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x45")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x46")),

                           new XElement("reg", new XAttribute("name", "TMCR"), new XAttribute("addr", "0x4B")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x4C")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x4D")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x4E")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x4F"))
                           )
                     ),
                 //-----------------------------------------------------------------------------------------
                    //mc34p01
#if mc34p01
                        new XElement("MCU", new XAttribute("name", "mc34p01"), new XAttribute("type", "RISC14"), new XAttribute("id", "0x3401"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize", "0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x003f"),
                        new XAttribute("chip_ramSize", "0x40")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x8007ff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x00004f"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x03ff"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the Second stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the third stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fourth stack addr ")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fifth stack addr")),
                            new XElement("sys", new XAttribute("name", "STATUS(7).RST"), new XAttribute("value", "0"), new XAttribute("describe", "唤醒源标志：1=P6变化唤醒；0=其它复位唤醒")),
                            new XElement("sys", new XAttribute("name", "STATUS(6).GP1"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(5).GP0"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(4).TO"), new XAttribute("value", "0"), new XAttribute("describe", "超时位")),
                            new XElement("sys", new XAttribute("name", "STATUS(3).PD"), new XAttribute("value", "0"), new XAttribute("describe", "掉电位")),
                             new XElement("sys", new XAttribute("name", "STATUS(2).Z"), new XAttribute("value", "0"), new XAttribute("describe", "结果为0标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(1).DC"), new XAttribute("value", "0"), new XAttribute("describe", "半进位/借位标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(0).C"), new XAttribute("value", "0"), new XAttribute("describe", "进位/借位标志位"))

                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "STATUS"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "FSR"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "GPR"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "GPR"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "PCLATH"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "PDCON"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "ODCON"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "PUCON"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "INTECON"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "INTFLAG"), new XAttribute("addr", "0x0F"))

                           //new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x41")),

                           //new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x45")),
                           //new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x46")),

                           //new XElement("reg", new XAttribute("name", "TMCR"), new XAttribute("addr", "0x4B")),
                           //new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x4C")),
                           //new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x4D")),
                           //new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x4E")),
                           //new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x4F"))
                           )
                     ),
#endif
                 //-----------------------------------------------------------------------------------------------------------------
#if mc31p11
                 new XElement("MCU", new XAttribute("name", "mc31p11"), new XAttribute("type", "RISC14"), new XAttribute("id", "0x3111"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize","0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x004f"),
                        new XAttribute("chip_ramSize","0x50")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x8007ff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x00004f"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x03ff"), new XAttribute("describe", "A state reg")),
                            //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the Second stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the third stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fourth stack addr "))
                            //new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fifth stack addr")),
                            //new XElement("sys", new XAttribute("name", "STATUS(7).RST"), new XAttribute("value", "0"), new XAttribute("describe", "唤醒源标志：1=P6变化唤醒；0=其它复位唤醒")),
                            //new XElement("sys", new XAttribute("name", "STATUS(6).GP1"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            //new XElement("sys", new XAttribute("name", "STATUS(5).GP0"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            //new XElement("sys", new XAttribute("name", "STATUS(4).TO"), new XAttribute("value", "0"), new XAttribute("describe", "超时位")),
                            //new XElement("sys", new XAttribute("name", "STATUS(3).PD"), new XAttribute("value", "0"), new XAttribute("describe", "掉电位")),
                            // new XElement("sys", new XAttribute("name", "STATUS(2).Z"), new XAttribute("value", "0"), new XAttribute("describe", "结果为0标志位")),
                            //new XElement("sys", new XAttribute("name", "STATUS(1).DC"), new XAttribute("value", "0"), new XAttribute("describe", "半进位/借位标志位")),
                            //new XElement("sys", new XAttribute("name", "STATUS(0).C"), new XAttribute("value", "0"), new XAttribute("describe", "进位/借位标志位"))
                            
                            ),
                        new XElement("option",
                           new XElement("CP","abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg",new XAttribute("name","INDF"),new XAttribute("addr","0x00")),
                           new XElement("reg",new XAttribute("name","FSR"),new XAttribute("addr","0x01")),
                           new XElement("reg",new XAttribute("name","PCL"),new XAttribute("addr","0x02")),
                           new XElement("reg",new XAttribute("name","PFLAG"),new XAttribute("addr","0x03")),
                           new XElement("reg",new XAttribute("name","MCR"),new XAttribute("addr","0x04")),
                           new XElement("reg",new XAttribute("name","INTE"),new XAttribute("addr","0x05")), 
                           new XElement("reg",new XAttribute("name","INTF"),new XAttribute("addr","0x06")),
                           new XElement("reg",new XAttribute("name","IOP0"),new XAttribute("addr","0x07")),
                           new XElement("reg",new XAttribute("name","OEP0"),new XAttribute("addr","0x08")),
                           new XElement("reg",new XAttribute("name","PUP0"),new XAttribute("addr","0x09")),
                           new XElement("reg",new XAttribute("name","DKWP0"),new XAttribute("addr","0x0A")),
                           new XElement("reg",new XAttribute("name","IOP1"),new XAttribute("addr","0x0B")),
                           new XElement("reg",new XAttribute("name","OEP1"),new XAttribute("addr","0x0C")),
                           new XElement("reg",new XAttribute("name","PUP1"),new XAttribute("addr","0x0D")),
                           new XElement("reg",new XAttribute("name","DKWP1"),new XAttribute("addr","0x0E")),
                           new XElement("reg",new XAttribute("name","DWK"),new XAttribute("addr","0x0F"))
                           //new XElement("reg",new XAttribute("name","OPTION"),new XAttribute("addr","0x41"))

                           )
                     ),
#endif
                 //-----------------------------------------------------------------------------------------------------------------
#if all
                 new XElement("MCU", new XAttribute("name", "mc30p01"), new XAttribute("type", "RISC14"), new XAttribute("id", "0x0301"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize","0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x004f"),
                        new XAttribute("chip_ramSize","0x50")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x0000"),
                        new XAttribute("ev_romEndAddr", "0x07ff"),
                        new XAttribute("ev_ramFirstAddr", "0x8000"),
                        new XAttribute("ev_ramEndAddr", "0x804f"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9039"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x03ff"), new XAttribute("describe", "A state reg")),
                            //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the Second stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the third stack addr")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fourth stack addr ")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "the fifth stack addr")),
                            new XElement("sys", new XAttribute("name", "STATUS(7).RST"), new XAttribute("value", "0"), new XAttribute("describe", "唤醒源标志：1=P6变化唤醒；0=其它复位唤醒")),
                            new XElement("sys", new XAttribute("name", "STATUS(6).GP1"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(5).GP0"), new XAttribute("value", "0"), new XAttribute("describe", "通用寄存器位")),
                            new XElement("sys", new XAttribute("name", "STATUS(4).TO"), new XAttribute("value", "0"), new XAttribute("describe", "超时位")),
                            new XElement("sys", new XAttribute("name", "STATUS(3).PD"), new XAttribute("value", "0"), new XAttribute("describe", "掉电位")),
                             new XElement("sys", new XAttribute("name", "STATUS(2).Z"), new XAttribute("value", "0"), new XAttribute("describe", "结果为0标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(1).DC"), new XAttribute("value", "0"), new XAttribute("describe", "半进位/借位标志位")),
                            new XElement("sys", new XAttribute("name", "STATUS(0).C"), new XAttribute("value", "0"), new XAttribute("describe", "进位/借位标志位"))
                            
                            ),
                        new XElement("option",
                           new XElement("CP","abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg",new XAttribute("name","INDF"),new XAttribute("addr","0x00")),
                           new XElement("reg",new XAttribute("name","T0"),new XAttribute("addr","0x01")),
                           new XElement("reg",new XAttribute("name","PCL"),new XAttribute("addr","0x02")),
                           new XElement("reg",new XAttribute("name","STATUS"),new XAttribute("addr","0x03")),
                           new XElement("reg",new XAttribute("name","FSR"),new XAttribute("addr","0x04")),
                           new XElement("reg",new XAttribute("name","P5"),new XAttribute("addr","0x05")), 
                           new XElement("reg",new XAttribute("name","P6"),new XAttribute("addr","0x06")),
                           new XElement("reg",new XAttribute("name","GPR"),new XAttribute("addr","0x07")),
                           new XElement("reg",new XAttribute("name","PCON"),new XAttribute("addr","0x08")),
                           new XElement("reg",new XAttribute("name","IOCB"),new XAttribute("addr","0x09")),
                           new XElement("reg",new XAttribute("name","PCLATH"),new XAttribute("addr","0x0A")),
                           new XElement("reg",new XAttribute("name","PDCON"),new XAttribute("addr","0x0B")),
                           new XElement("reg",new XAttribute("name","ODCON"),new XAttribute("addr","0x0C")),
                           new XElement("reg",new XAttribute("name","PHCON"),new XAttribute("addr","0x0D")),
                           new XElement("reg",new XAttribute("name","INTECON"),new XAttribute("addr","0x0E")),
                           new XElement("reg",new XAttribute("name","INTFLAG"),new XAttribute("addr","0x0F")),
                           new XElement("reg",new XAttribute("name","OPTION"),new XAttribute("addr","0x41")),
                           new XElement("reg",new XAttribute("name","P5IOC"),new XAttribute("addr","0x45")),
                           new XElement("reg",new XAttribute("name","P6IOC"),new XAttribute("addr","0x46"))
                           )
                     ),
                     //--------------------------------------------------------------------------------------
#endif 
#if mc33p116
                     new XElement("MCU", new XAttribute("name", "mc33p116"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3316"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x3fff"),
                        new XAttribute("chip_romSize", "0x4000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x02ff"),
                        new XAttribute("chip_ramSize", "0x0300")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x807fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x0002ff"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x1B6")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x1B7")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x1BA")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x1BB")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),
                            new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1BD")),
                           new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1BE")),
                           new XElement("reg", new XAttribute("name", "LXTCP"), new XAttribute("addr", "0x1BF")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x1C2")),
                           new XElement("reg", new XAttribute("name", "DKWP0"), new XAttribute("addr", "0x1C3")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x1C6")),
                           new XElement("reg", new XAttribute("name", "LCDIOSP1"), new XAttribute("addr", "0x1C7")),

                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "PUP2"), new XAttribute("addr", "0x1CA")),
                           new XElement("reg", new XAttribute("name", "LCDIOSP2"), new XAttribute("addr", "0x1CB")),
                           new XElement("reg", new XAttribute("name", "IOP3"), new XAttribute("addr", "0x1CC")),
                           new XElement("reg", new XAttribute("name", "OEP3"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "PUP3"), new XAttribute("addr", "0x1CE")),
                           new XElement("reg", new XAttribute("name", "LCDIOSP3"), new XAttribute("addr", "0x1CF")),

                           new XElement("reg", new XAttribute("name", "IOP4"), new XAttribute("addr", "0x1D0")),
                           new XElement("reg", new XAttribute("name", "OEP4"), new XAttribute("addr", "0x1D1")),
                           new XElement("reg", new XAttribute("name", "PUP4"), new XAttribute("addr", "0x1D2")),
                           new XElement("reg", new XAttribute("name", "LCDIOSP4"), new XAttribute("addr", "0x1D3")),
                           new XElement("reg", new XAttribute("name", "IOP5"), new XAttribute("addr", "0x1D4")),
                           new XElement("reg", new XAttribute("name", "OEP5"), new XAttribute("addr", "0x1D5")),
                           new XElement("reg", new XAttribute("name", "PUP5"), new XAttribute("addr", "0x1D6")),
                           new XElement("reg", new XAttribute("name", "LCDIOSP5"), new XAttribute("addr", "0x1D7")),

                           new XElement("reg", new XAttribute("name", "T0CR0"), new XAttribute("addr", "0x1D8")),
                           new XElement("reg", new XAttribute("name", "T0CR1"), new XAttribute("addr", "0x1D9")),
                           //new XElement("reg", new XAttribute("name", "T0LOADL"), new XAttribute("addr", "0x1DA")),
                           //new XElement("reg", new XAttribute("name", "T0TATRH"), new XAttribute("addr", "0x1DB")),
                           new XElement("reg", new XAttribute("name", "T0LOADH"), new XAttribute("addr", "0x1DC")),
                           new XElement("reg", new XAttribute("name", "T0LOADL"), new XAttribute("addr", "0x1DD")),
                           new XElement("reg", new XAttribute("name", "T0DATAH"), new XAttribute("addr", "0x1DE")),
                           new XElement("reg", new XAttribute("name", "T0DATAL"), new XAttribute("addr", "0x1DF")),

                           new XElement("reg", new XAttribute("name", "T0LATRH"), new XAttribute("addr", "0x1E0")),
                           new XElement("reg", new XAttribute("name", "T0LATRL"), new XAttribute("addr", "0x1E1")),
                           new XElement("reg", new XAttribute("name", "T0LATFH"), new XAttribute("addr", "0x1E2")),
                           new XElement("reg", new XAttribute("name", "T0LATFL"), new XAttribute("addr", "0x1E3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1E4")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1E5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1E6")),
                           //new XElement("reg", new XAttribute("name", "DWK1"), new XAttribute("addr", "0x1E7"))

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1E8")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1E9")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1EA")),
                           //new XElement("reg", new XAttribute("name", "T0LATFL"), new XAttribute("addr", "0x1EB")),
                           new XElement("reg", new XAttribute("name", "ADDRH"), new XAttribute("addr", "0x1EC")),
                           new XElement("reg", new XAttribute("name", "ADDRL"), new XAttribute("addr", "0x1ED")),
                           //new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1EE")),
                           //new XElement("reg", new XAttribute("name", "DWK1"), new XAttribute("addr", "0x1EF"))

                           new XElement("reg", new XAttribute("name", "OPCR0"), new XAttribute("addr", "0x1F0")),
                           new XElement("reg", new XAttribute("name", "OPCR1"), new XAttribute("addr", "0x1F1")),
                           new XElement("reg", new XAttribute("name", "LCDCR0"), new XAttribute("addr", "0x1F2")),
                           new XElement("reg", new XAttribute("name", "LCDCR1"), new XAttribute("addr", "0x1F3")),
                           new XElement("reg", new XAttribute("name", "DWK"), new XAttribute("addr", "0x1F4")),
                           new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x1F5")),
                          new XElement("reg", new XAttribute("name", "LCDDRV"), new XAttribute("addr", "0x1F6"))
                           //new XElement("reg", new XAttribute("name", "DWK1"), new XAttribute("addr", "0x1E7"))

                           )
                     ),

#endif

#if mc33p78
                     new XElement("MCU", new XAttribute("name", "mc33p78"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3378"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x2000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x03ff"),
                        new XAttribute("chip_ramSize", "0x0400")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x803fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x0003ff"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x1B6")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x1B7")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x1BA")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x1BB")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),

                          // new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x1C0")),
                          // new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x1C1")),
                          // new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x1C2")),
                          //// new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                          // new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x1C4")),
                          // new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x1C5")),
                          // new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x1C6")),
                          // //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x1CA")),
                            // new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           //new XElement("reg", new XAttribute("name", "IOP3"), new XAttribute("addr", "0x1CC")),
                           //new XElement("reg", new XAttribute("name", "OEP3"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "DKWP1"), new XAttribute("addr", "0x1CE")),
                            //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x1D0")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x1D1")),
                           new XElement("reg", new XAttribute("name", "PUP2"), new XAttribute("addr", "0x1D2")),
                           //new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1D3")),
                           //new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1D4")),
                           //new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1D5")),
                           new XElement("reg", new XAttribute("name", "DKWP2"), new XAttribute("addr", "0x1D6")),
                           new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x1D7")),

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1D8")),
                           new XElement("reg", new XAttribute("name", "T0LOADH"), new XAttribute("addr", "0x1D9")),
                           new XElement("reg", new XAttribute("name", "T0LOADL"), new XAttribute("addr", "0x1DA")),
                           new XElement("reg", new XAttribute("name", "T0TATRH"), new XAttribute("addr", "0x1DB")),
                           new XElement("reg", new XAttribute("name", "T0LATFH"), new XAttribute("addr", "0x1DC")),
                           new XElement("reg", new XAttribute("name", "T0LATFL"), new XAttribute("addr", "0x1DD")),

                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1E0")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1E1")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1E2")),
                           //new XElement("reg", new XAttribute("name", "TK0CNTL"), new XAttribute("addr", "0x1E3")),
                           new XElement("reg", new XAttribute("name", "OPCR0"), new XAttribute("addr", "0x1E4")),
                           new XElement("reg", new XAttribute("name", "OPCR1"), new XAttribute("addr", "0x1E5")),
                           new XElement("reg", new XAttribute("name", "DWK0"), new XAttribute("addr", "0x1E6")),
                           new XElement("reg", new XAttribute("name", "DWK1"), new XAttribute("addr", "0x1E7"))
                           )
                     ),                    

#endif


#if mc32p5312
 new XElement("MCU", new XAttribute("name", "mc32p5312"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x5312"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x0fff"),
                        new XAttribute("chip_romSize", "0x1000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01cf"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x000200"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈"))
                            //new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            //new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            //new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            //new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x1B6")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x1B7")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x1BA")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x1BB")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1BD")),
                           new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1BE")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x1C2")),
                           new XElement("reg", new XAttribute("name", "DKWP0"), new XAttribute("addr", "0x1C3")),
                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1C6")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1C7")),

                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1CA")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1CB")),
                           new XElement("reg", new XAttribute("name", "LCDCR0"), new XAttribute("addr", "0x1CC")),
                           new XElement("reg", new XAttribute("name", "LCDCR1"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "DKW"), new XAttribute("addr", "0x1CE"))
                    //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),                         
                           )
                     ),


#endif

#if mc32p7022
 new XElement("MCU", new XAttribute("name", "mc32p7022"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x7022"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x0fef"),
                        new XAttribute("chip_romSize", "0x1000"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x000200"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x18B")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           new XElement("reg", new XAttribute("name", "PDP0"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "T0CR0"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA0"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "T0CR1"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "T0DTAT1"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1AC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AD")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "ADIOS"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "ADVR0"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADVR1"), new XAttribute("addr", "0x1B5")),  
                           new XElement("reg", new XAttribute("name", "ADVR2"), new XAttribute("addr", "0x1B6")), 
                     
                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "KEYCR0"), new XAttribute("addr", "0x1BC")),  
                           new XElement("reg", new XAttribute("name", "KEYCR1"), new XAttribute("addr", "0x1BD")), 

                           new XElement("reg", new XAttribute("name", "FUNT1"), new XAttribute("addr", "0x1F9")),
                           new XElement("reg", new XAttribute("name", "FUNT2"), new XAttribute("addr", "0x1FA")),
                           new XElement("reg", new XAttribute("name", "FUNT3"), new XAttribute("addr", "0x1FB")),
                           new XElement("reg", new XAttribute("name", "FUNT4"), new XAttribute("addr", "0x1FC")),
                           new XElement("reg", new XAttribute("name", "FUNT5"), new XAttribute("addr", "0x1FD")),  
                           new XElement("reg", new XAttribute("name", "FUNT6"), new XAttribute("addr", "0x1FE"))
                           )
                     ),


#endif

#if mc32p7212
 new XElement("MCU", new XAttribute("name", "mc32p7212"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x7212"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x0fff"),
                        new XAttribute("chip_romSize", "0x1000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x000200"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈"))
                    //new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                    //new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE0"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF0"), new XAttribute("addr", "0x18B")),
                           //new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),
                           //new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1BD")),
                           //new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1BE")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),

                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x198")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x199")),

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "HIRCCR0"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "HIRCCR1"), new XAttribute("addr", "0x1AA")),
                           new XElement("reg", new XAttribute("name", "HIRCCR2"), new XAttribute("addr", "0x1AB")),
                          // new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "PUMP"), new XAttribute("addr", "0x1AD")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AF")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "TPSC"), new XAttribute("addr", "0x1B3")),
                           //new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "ADRM"), new XAttribute("addr", "0x1B6")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B7")),

                           new XElement("reg", new XAttribute("name", "LCDCR0"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "LCDCR1"), new XAttribute("addr", "0x1B9")),

                           new XElement("reg", new XAttribute("name", "LCDDS0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "LCDDS1"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "LCDDS2"), new XAttribute("addr", "0x1C2")),
                           new XElement("reg", new XAttribute("name", "LCDDS3"), new XAttribute("addr", "0x1C3")),
                           new XElement("reg", new XAttribute("name", "LCDDS4"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "LCDDS5"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "LCDDS6"), new XAttribute("addr", "0x1C6")),
                           new XElement("reg", new XAttribute("name", "LCDDS7"), new XAttribute("addr", "0x1C7")),
                         
                           new XElement("reg", new XAttribute("name", "LCDDS8"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "LCDDS9"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "LCDDS10"), new XAttribute("addr", "0x1CA")),
                           new XElement("reg", new XAttribute("name", "LCDDS11"), new XAttribute("addr", "0x1CB"))
                           )
                     ),
#endif
#if mc32pa300
 new XElement("MCU", new XAttribute("name", "mcA300"), new XAttribute("type", "RISC16"), new XAttribute("id", "0xa300"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x07ff"),
                        new XAttribute("chip_romSize", "0x1000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x800fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x000200"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈"))
                    //new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                    //new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE0"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF0"), new XAttribute("addr", "0x18B")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1BD")),
                           //new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1BE")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           new XElement("reg", new XAttribute("name", "PSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),

                           //0X198--

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                          // new XElement("reg", new XAttribute("name", "LXTCR"), new XAttribute("addr", "0x1A8")),
                          // new XElement("reg", new XAttribute("name", "HIRCCR0"), new XAttribute("addr", "0x1A9")),
                          // new XElement("reg", new XAttribute("name", "HIRCCR1"), new XAttribute("addr", "0x1AA")),
                          // new XElement("reg", new XAttribute("name", "HIRCCR2"), new XAttribute("addr", "0x1AB")),
                          // new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                          // new XElement("reg", new XAttribute("name", "PUMP"), new XAttribute("addr", "0x1AD")),
                          // new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                          // new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AF")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1B2")),
                          // new XElement("reg", new XAttribute("name", "TPSC"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRM"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B6")),

                           new XElement("reg", new XAttribute("name", "IICSCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "IICADDR"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "IICDATA"), new XAttribute("addr", "0x1BA"))
                           )
                     ),
#endif
#if mc32p64
 new XElement("MCU", new XAttribute("name", "gm32p64"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3264"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x0fff"),
                        new XAttribute("chip_romSize", "0x1000"), //4096
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x013f"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x000200"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x1B2")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x1B3")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x1B6")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x1B7")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x1BA")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x1BB")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1BC")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x1C2")),
                          // new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x1C6")),
                           //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "PUP2"), new XAttribute("addr", "0x1CA")),
                            // new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP3"), new XAttribute("addr", "0x1CC")),
                           new XElement("reg", new XAttribute("name", "OEP3"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "PUP3"), new XAttribute("addr", "0x1CE")),
                            //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1D0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1D1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1D2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1D3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1D4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1D5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1D6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1D7")),

                           new XElement("reg", new XAttribute("name", "T2CR"), new XAttribute("addr", "0x1D8")),
                           new XElement("reg", new XAttribute("name", "T2CNTH"), new XAttribute("addr", "0x1D9")),
                           new XElement("reg", new XAttribute("name", "T2CNTL"), new XAttribute("addr", "0x1DA")),
                           new XElement("reg", new XAttribute("name", "T2LOADH"), new XAttribute("addr", "0x1DB")),
                           new XElement("reg", new XAttribute("name", "T2LOADL"), new XAttribute("addr", "0x1DC")),

                           new XElement("reg", new XAttribute("name", "TK0CRH"), new XAttribute("addr", "0x1E0")),
                           new XElement("reg", new XAttribute("name", "TK0CRL"), new XAttribute("addr", "0x1E1")),
                           new XElement("reg", new XAttribute("name", "TK0CNTH"), new XAttribute("addr", "0x1E2")),
                           new XElement("reg", new XAttribute("name", "TK0CNTL"), new XAttribute("addr", "0x1E3")),
                           new XElement("reg", new XAttribute("name", "TK1CRH"), new XAttribute("addr", "0x1E4")),
                           new XElement("reg", new XAttribute("name", "TK1CRL"), new XAttribute("addr", "0x1E5")),
                           new XElement("reg", new XAttribute("name", "TK1CNTH"), new XAttribute("addr", "0x1E6")),
                           new XElement("reg", new XAttribute("name", "TK1CNTL"), new XAttribute("addr", "0x1E7")),

                           new XElement("reg", new XAttribute("name", "TK2CRH"), new XAttribute("addr", "0x1E8")),
                           new XElement("reg", new XAttribute("name", "TK2CRL"), new XAttribute("addr", "0x1E9")),
                           new XElement("reg", new XAttribute("name", "TK2CNTH"), new XAttribute("addr", "0x1EA")),
                           new XElement("reg", new XAttribute("name", "TK2CNTL"), new XAttribute("addr", "0x1EB")),
                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1EC")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1ED")),
                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1EE")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1EF")),

                           new XElement("reg", new XAttribute("name", "I2CCR"), new XAttribute("addr", "0x1F0")),
                           new XElement("reg", new XAttribute("name", "I2CADDR"), new XAttribute("addr", "0x1F1")),
                           new XElement("reg", new XAttribute("name", "I2CDATA"), new XAttribute("addr", "0x1F2")),

                           new XElement("reg", new XAttribute("name", "OSCCAL0"), new XAttribute("addr", "0x1FB")),
                           new XElement("reg", new XAttribute("name", "OSCCAL1"), new XAttribute("addr", "0x1FC"))
                           )
                     ),
                    

#endif
#if mc32p821
                    new XElement("MCU", new XAttribute("name", "mc32p7020"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x32821"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize", "0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x007f"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x8007ff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x00007f"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",
                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x18B")),
                          // new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x18C")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x197")),
                          

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "T2CR"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "T2CNT"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "T2LOAD"), new XAttribute("addr", "0x1AA")),

                           new XElement("reg", new XAttribute("name", "PWMCR"), new XAttribute("addr", "0x1AC")),

                           //new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AD")),
                            new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),

                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "OSCCAL"), new XAttribute("addr", "0x1FB"))
                           )
                     ),
#endif
 new XElement("MCU", new XAttribute("name", "mc32p21"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3221"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x03ff"),
                        new XAttribute("chip_romSize", "0x400"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x007f"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x8007ff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x00007f"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",
                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈"))
                  
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF"), new XAttribute("addr", "0x18B")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x197")),
                          

                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AF")),
                           
                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),

                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "OSCCAL"), new XAttribute("addr", "0x1FB"))
                           )
                     ),
                     //-------mc33p716---------------------------------------------------------------------------------
#if mc33p716
                      new XElement("MCU", new XAttribute("name", "mc33p716"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3394"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x1fef"),
                        new XAttribute("chip_romSize", "0x1ff0"), //2048
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fef"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x0001ff"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))

                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE0"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF0"), new XAttribute("addr", "0x18B")),
                    //new XElement("reg", new XAttribute("name", "INTTE1"), new XAttribute("addr", "0x18C")),
                    //new XElement("reg", new XAttribute("name", "INTF1"), new XAttribute("addr", "0x18D")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                    //new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                    //new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),
                    //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x198")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x199")),
                    //new XElement("reg", new XAttribute("name", "EECTR"), new XAttribute("addr", "0x19A")),
                    //new XElement("reg", new XAttribute("name", "EECON0"), new XAttribute("addr", "0x19B")),
                    //new XElement("reg", new XAttribute("name", "EECON1"), new XAttribute("addr", "0x19C")),
                    //new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x19D")),
                    //new XElement("reg", new XAttribute("name", "CCDATH"), new XAttribute("addr", "0x19E")),
                    //new XElement("reg", new XAttribute("name", "CCDATL"), new XAttribute("addr", "0x19F")),


                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "UCR"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "UBR"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "UFR"), new XAttribute("addr", "0x1AA")),
                           new XElement("reg", new XAttribute("name", "UTR"), new XAttribute("addr", "0x1AB")),
                           new XElement("reg", new XAttribute("name", "URR"), new XAttribute("addr", "0x1AC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AD")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                           new XElement("reg", new XAttribute("name", "POWCR"), new XAttribute("addr", "0x1AF")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1B2")),

                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRM"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B6")),

                           new XElement("reg", new XAttribute("name", "LCDCR0"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "LCDCR1"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "LCDIOS"), new XAttribute("addr", "0x1BA")),
                    //new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "LCDDS0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "LCDDS1"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "LCDDS2"), new XAttribute("addr", "0x1C2")),
                           new XElement("reg", new XAttribute("name", "LCDDS3"), new XAttribute("addr", "0x1C3")),
                           new XElement("reg", new XAttribute("name", "LCDDS4"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "LCDDS5"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "LCDDS6"), new XAttribute("addr", "0x1C6")),
                           new XElement("reg", new XAttribute("name", "LCDDS7"), new XAttribute("addr", "0x1C7")),

                           new XElement("reg", new XAttribute("name", "LCDDS8"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "LCDDS9"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "LCDDS10"), new XAttribute("addr", "0x1CA")),
                           new XElement("reg", new XAttribute("name", "LCDDS11"), new XAttribute("addr", "0x1CB")),
                           new XElement("reg", new XAttribute("name", "LCDDS12"), new XAttribute("addr", "0x1CC")),
                           new XElement("reg", new XAttribute("name", "LCDDS13"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "LCDDS14"), new XAttribute("addr", "0x1CE")),
                           new XElement("reg", new XAttribute("name", "LCDDS15"), new XAttribute("addr", "0x1CF"))
                           )
                     ),
#endif
                     //--------------------------------------------------------------------------------------
#if all
                     new XElement("MCU", new XAttribute("name", "mc33p94"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3394"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x1fef"),
                        new XAttribute("chip_romSize", "0x1ff0"), //2048
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x801fef"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x0001ff"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))

                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE0"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF0"), new XAttribute("addr", "0x18B")),
                           //new XElement("reg", new XAttribute("name", "INTTE1"), new XAttribute("addr", "0x18C")),
                           //new XElement("reg", new XAttribute("name", "INTF1"), new XAttribute("addr", "0x18D")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           //new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           //new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),
                           //new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x198")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x199")),
                           //new XElement("reg", new XAttribute("name", "EECTR"), new XAttribute("addr", "0x19A")),
                           //new XElement("reg", new XAttribute("name", "EECON0"), new XAttribute("addr", "0x19B")),
                           //new XElement("reg", new XAttribute("name", "EECON1"), new XAttribute("addr", "0x19C")),
                    //new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x19D")),
                           //new XElement("reg", new XAttribute("name", "CCDATH"), new XAttribute("addr", "0x19E")),
                           //new XElement("reg", new XAttribute("name", "CCDATL"), new XAttribute("addr", "0x19F")),


                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "UCR"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "UBR"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "UFR"), new XAttribute("addr", "0x1AA")),
                           new XElement("reg", new XAttribute("name", "UTR"), new XAttribute("addr", "0x1AB")),
                           new XElement("reg", new XAttribute("name", "URR"), new XAttribute("addr", "0x1AC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AD")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                           new XElement("reg", new XAttribute("name", "POWCR"), new XAttribute("addr", "0x1AF")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),
                           new XElement("reg", new XAttribute("name", "ADCR2"), new XAttribute("addr", "0x1B2")),

                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRM"), new XAttribute("addr", "0x1B5")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B6")),

                           new XElement("reg", new XAttribute("name", "LCDCR0"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "LCDCR1"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "LCDIOS"), new XAttribute("addr", "0x1BA")),
                    //new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "LCDDS0"), new XAttribute("addr", "0x1C0")),
                           new XElement("reg", new XAttribute("name", "LCDDS1"), new XAttribute("addr", "0x1C1")),
                           new XElement("reg", new XAttribute("name", "LCDDS2"), new XAttribute("addr", "0x1C2")),
                           new XElement("reg", new XAttribute("name", "LCDDS3"), new XAttribute("addr", "0x1C3")),
                           new XElement("reg", new XAttribute("name", "LCDDS4"), new XAttribute("addr", "0x1C4")),
                           new XElement("reg", new XAttribute("name", "LCDDS5"), new XAttribute("addr", "0x1C5")),
                           new XElement("reg", new XAttribute("name", "LCDDS6"), new XAttribute("addr", "0x1C6")),
                           new XElement("reg", new XAttribute("name", "LCDDS7"), new XAttribute("addr", "0x1C7")),

                           new XElement("reg", new XAttribute("name", "LCDDS8"), new XAttribute("addr", "0x1C8")),
                           new XElement("reg", new XAttribute("name", "LCDDS9"), new XAttribute("addr", "0x1C9")),
                           new XElement("reg", new XAttribute("name", "LCDDS10"), new XAttribute("addr", "0x1CA")),
                           new XElement("reg", new XAttribute("name", "LCDDS11"), new XAttribute("addr", "0x1CB")),
                           new XElement("reg", new XAttribute("name", "LCDDS12"), new XAttribute("addr", "0x1CC")),
                           new XElement("reg", new XAttribute("name", "LCDDS13"), new XAttribute("addr", "0x1CD")),
                           new XElement("reg", new XAttribute("name", "LCDDS14"), new XAttribute("addr", "0x1CE")),
                           new XElement("reg", new XAttribute("name", "LCDDS15"), new XAttribute("addr", "0x1CF"))
                           )
                     ),
                    //--------------------------------------------------------------------------------------
#if mc32e22
                    new XElement("MCU", new XAttribute("name", "mc32e22"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x3220"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x07ff"),
                        new XAttribute("chip_romSize", "0x800"), //2048
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x007f"),
                        new XAttribute("chip_ramSize", "0x0200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x800000"),
                        new XAttribute("ev_romEndAddr", "0x8007ff"),
                        new XAttribute("ev_ramFirstAddr", "0x000000"),
                        new XAttribute("ev_ramEndAddr", "0x00007f"),
                        new XAttribute("ev_bkFirstAddr", "0x010000"),
                        new XAttribute("ev_bkEndAddr", "0x01003f"),
                        new XAttribute("ev_pcFirstAddr", "0x010100"),
                        new XAttribute("ev_pcEndAddr", "0x01021f"),
                        new XAttribute("ev_optionAddr", "0x7ffff0"),
                        new XAttribute("ev_optionCnt", "0x0f"),
                        new XAttribute("ev_traceFirstAddr", "0x01fffe"),
                        new XAttribute("ev_traceEndAddr", "0x02ffff")
                            ),
                        new XElement("syss",

                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))

                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x180")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x181")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x182")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x183")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x184")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x185")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x186")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x187")),

                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x188")),
                           new XElement("reg", new XAttribute("name", "INDF3"), new XAttribute("addr", "0x189")),
                           new XElement("reg", new XAttribute("name", "INTE0"), new XAttribute("addr", "0x18A")),
                           new XElement("reg", new XAttribute("name", "INTF0"), new XAttribute("addr", "0x18B")),
                           new XElement("reg", new XAttribute("name", "INTTE1"), new XAttribute("addr", "0x18C")),
                           new XElement("reg", new XAttribute("name", "INTF1"), new XAttribute("addr", "0x18D")),

                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x190")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x191")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x192")),
                           //new XElement("reg", new XAttribute("name", "ANSEL"), new XAttribute("addr", "0x193")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x194")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x195")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x196")),
                           new XElement("reg", new XAttribute("name", "KBCR"), new XAttribute("addr", "0x197")),

                           new XElement("reg", new XAttribute("name", "EEADR"), new XAttribute("addr", "0x198")),
                           new XElement("reg", new XAttribute("name", "EEDAT"), new XAttribute("addr", "0x199")),
                           new XElement("reg", new XAttribute("name", "EECTR"), new XAttribute("addr", "0x19A")),
                           new XElement("reg", new XAttribute("name", "EECON0"), new XAttribute("addr", "0x19B")),
                           new XElement("reg", new XAttribute("name", "EECON1"), new XAttribute("addr", "0x19C")),
                           //new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x19D")),
                           new XElement("reg", new XAttribute("name", "CCDATH"), new XAttribute("addr", "0x19E")),
                           new XElement("reg", new XAttribute("name", "CCDATL"), new XAttribute("addr", "0x19F")),


                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x1A0")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x1A1")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x1A2")),
                           new XElement("reg", new XAttribute("name", "T0DATA"), new XAttribute("addr", "0x1A3")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1A4")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x1A5")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x1A6")),
                           new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1A7")),

                           new XElement("reg", new XAttribute("name", "T2CR"), new XAttribute("addr", "0x1A8")),
                           new XElement("reg", new XAttribute("name", "T2CNTH"), new XAttribute("addr", "0x1A9")),
                           new XElement("reg", new XAttribute("name", "T2CNTL"), new XAttribute("addr", "0x1AA")),
                           new XElement("reg", new XAttribute("name", "CCMCR"), new XAttribute("addr", "0x1AB")),
                           //new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x1AC")),
                           new XElement("reg", new XAttribute("name", "LVDCR"), new XAttribute("addr", "0x1AD")),
                           new XElement("reg", new XAttribute("name", "OSCM"), new XAttribute("addr", "0x1AE")),
                           //new XElement("reg", new XAttribute("name", "T1DATA"), new XAttribute("addr", "0x1AF")),

                           new XElement("reg", new XAttribute("name", "ADCR0"), new XAttribute("addr", "0x1B0")),
                           new XElement("reg", new XAttribute("name", "ADCR1"), new XAttribute("addr", "0x1B1")),

                           new XElement("reg", new XAttribute("name", "ADRH"), new XAttribute("addr", "0x1B4")),
                           new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "CMCR"), new XAttribute("addr", "0x1B8")),
                           new XElement("reg", new XAttribute("name", "VRCR"), new XAttribute("addr", "0x1B9")),
                           new XElement("reg", new XAttribute("name", "CMCAL"), new XAttribute("addr", "0x1BA")),
                           //new XElement("reg", new XAttribute("name", "ADRL"), new XAttribute("addr", "0x1B5")),

                           new XElement("reg", new XAttribute("name", "OSCCAL"), new XAttribute("addr", "0x1F3"))
                           )
                     ),
#endif
                    //--------------------------------------------------------------------------------------
#if mc30p44
                    new XElement("MCU", new XAttribute("name", "mc30p44"), new XAttribute("type", "RISC16"), new XAttribute("id", "0x0314"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x0000"),
                        new XAttribute("chip_romEndAddr", "0x0fff"),
                        new XAttribute("chip_romSize", "0x2000"), //1024
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x0134"),
                        new XAttribute("chip_ramSize", "0x134")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x0000"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x8000"),
                        new XAttribute("ev_ramEndAddr", "0x8134"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f2f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x903f"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x0000"), new XAttribute("describe", "A state reg")),
                    //new XElement("sys", new XAttribute("name", "PCL"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP pointer reg")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A state reg")),
                            new XElement("sys", new XAttribute("name", "Stack1"), new XAttribute("value", "0xffff"), new XAttribute("describe", "堆栈地址")),
                            new XElement("sys", new XAttribute("name", "Stack2"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第二级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack3"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第三级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack4"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第四级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack5"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第五级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack6"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第六级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack7"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第七级堆栈")),
                            new XElement("sys", new XAttribute("name", "Stack8"), new XAttribute("value", "0xffff"), new XAttribute("describe", "第八级堆栈"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd")
                            ),
                        new XElement("regs",
                           new XElement("reg", new XAttribute("name", "INDF0"), new XAttribute("addr", "0x100")),
                           new XElement("reg", new XAttribute("name", "INDF1"), new XAttribute("addr", "0x101")),
                           new XElement("reg", new XAttribute("name", "INDF2"), new XAttribute("addr", "0x102")),
                           new XElement("reg", new XAttribute("name", "HIBYTE"), new XAttribute("addr", "0x103")),
                           new XElement("reg", new XAttribute("name", "FSR0"), new XAttribute("addr", "0x104")),
                           new XElement("reg", new XAttribute("name", "FSR1"), new XAttribute("addr", "0x105")),
                           new XElement("reg", new XAttribute("name", "PCL"), new XAttribute("addr", "0x106")),
                           new XElement("reg", new XAttribute("name", "PFLAG"), new XAttribute("addr", "0x107")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x108")),
                           new XElement("reg", new XAttribute("name", "---"), new XAttribute("addr", "0x109")),
                           new XElement("reg", new XAttribute("name", "INTECON"), new XAttribute("addr", "0x10A")),
                           new XElement("reg", new XAttribute("name", "INTFLAG"), new XAttribute("addr", "0x10B")),
                           new XElement("reg", new XAttribute("name", "ADSCR"), new XAttribute("addr", "0x10C")),
                           new XElement("reg", new XAttribute("name", "ADIOS"), new XAttribute("addr", "0x10D")),
                           new XElement("reg", new XAttribute("name", "ADDRH"), new XAttribute("addr", "0x10E")),
                           new XElement("reg", new XAttribute("name", "ADDRL"), new XAttribute("addr", "0x10F")),
                           new XElement("reg", new XAttribute("name", "IOP0"), new XAttribute("addr", "0x110")),
                           new XElement("reg", new XAttribute("name", "OEP0"), new XAttribute("addr", "0x111")),
                           new XElement("reg", new XAttribute("name", "PUP0"), new XAttribute("addr", "0x112")),
                           new XElement("reg", new XAttribute("name", "IOP1"), new XAttribute("addr", "0x113")),
                           new XElement("reg", new XAttribute("name", "OEP1"), new XAttribute("addr", "0x114")),
                           new XElement("reg", new XAttribute("name", "PUP1"), new XAttribute("addr", "0x115")),
                           new XElement("reg", new XAttribute("name", "IOP2"), new XAttribute("addr", "0x116")),
                           new XElement("reg", new XAttribute("name", "OEP2"), new XAttribute("addr", "0x117")),
                           new XElement("reg", new XAttribute("name", "IOP3"), new XAttribute("addr", "0x118")),
                           new XElement("reg", new XAttribute("name", "OEP3"), new XAttribute("addr", "0x119")),
                           new XElement("reg", new XAttribute("name", "T0CR"), new XAttribute("addr", "0x11A")),
                           new XElement("reg", new XAttribute("name", "T0CNT"), new XAttribute("addr", "0x11B")),
                           new XElement("reg", new XAttribute("name", "T0LOAD"), new XAttribute("addr", "0x11C")),
                           new XElement("reg", new XAttribute("name", "T0DATA1"), new XAttribute("addr", "0x11D")),
                           new XElement("reg", new XAttribute("name", "T1CR"), new XAttribute("addr", "0x11E")),
                           new XElement("reg", new XAttribute("name", "T1CNT"), new XAttribute("addr", "0x11F")),
                           new XElement("reg", new XAttribute("name", "T1LOAD"), new XAttribute("addr", "0x120")),
                           new XElement("reg", new XAttribute("name", "T1DATA1"), new XAttribute("addr", "0x121")),
                           new XElement("reg", new XAttribute("name", "UCR"), new XAttribute("addr", "0x122")),
                           new XElement("reg", new XAttribute("name", "UBR"), new XAttribute("addr", "0x123")),
                           new XElement("reg", new XAttribute("name", "UFR"), new XAttribute("addr", "0x124")),
                           new XElement("reg", new XAttribute("name", "UTR"), new XAttribute("addr", "0x125")),
                           new XElement("reg", new XAttribute("name", "URR"), new XAttribute("addr", "0x126")),
                           new XElement("reg", new XAttribute("name", "LEDCON"), new XAttribute("addr", "0x127")),
                           new XElement("reg", new XAttribute("name", "LEDIOS0"), new XAttribute("addr", "0x128")),
                           new XElement("reg", new XAttribute("name", "LEDIOS1"), new XAttribute("addr", "0x129")),
                           new XElement("reg", new XAttribute("name", "LEDIOS2"), new XAttribute("addr", "0x12A")),
                           new XElement("reg", new XAttribute("name", "LEDDS1"), new XAttribute("addr", "0x12B")),
                           new XElement("reg", new XAttribute("name", "LEDDS2"), new XAttribute("addr", "0x12C")),
                           new XElement("reg", new XAttribute("name", "LEDDS3"), new XAttribute("addr", "0x12D")),
                           new XElement("reg", new XAttribute("name", "LEDDS4"), new XAttribute("addr", "0x12E")),
                           new XElement("reg", new XAttribute("name", "LEDDS5"), new XAttribute("addr", "0x12F")),
                           new XElement("reg", new XAttribute("name", "LEDDS6"), new XAttribute("addr", "0x130")),
                           new XElement("reg", new XAttribute("name", "LEDDS7"), new XAttribute("addr", "0x131")),
                           new XElement("reg", new XAttribute("name", "LEDDS8"), new XAttribute("addr", "0x132")),
                           new XElement("reg", new XAttribute("name", "LEDDS9"), new XAttribute("addr", "0x133")),
                           new XElement("reg", new XAttribute("name", "LEDDS10"), new XAttribute("addr", "0x134"))
                           )
                     ),
#endif
                     //------------------------------------------------------------------------------------------------------
#endif                   
                     new XElement("MCU", new XAttribute("name", "MC20P24B"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0224"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1000"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize","0x1000"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize","0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1000"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "TOCNT"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "TODATA"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "T0CON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "BTCON"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "BTCNT"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x10")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x11")),
                           new XElement("reg", new XAttribute("name", "P2"), new XAttribute("addr", "0x12")),
                           new XElement("reg", new XAttribute("name", "P0CONH"), new XAttribute("addr", "0x16")),
                           new XElement("reg", new XAttribute("name", "P0CONL"), new XAttribute("addr", "0x17")),
                           new XElement("reg", new XAttribute("name", "P0PND"), new XAttribute("addr", "0x18")),
                           new XElement("reg", new XAttribute("name", "P1CON"), new XAttribute("addr", "0x19")),
                           new XElement("reg", new XAttribute("name", "P2CONH"), new XAttribute("addr", "0x1A")),
                           new XElement("reg", new XAttribute("name", "P2CONL"), new XAttribute("addr", "0x1B")),
                           new XElement("reg", new XAttribute("name", "PWMDATA"), new XAttribute("addr", "0x22")),
                           new XElement("reg", new XAttribute("name", "PWMCON"), new XAttribute("addr", "0x23")),
                           new XElement("reg", new XAttribute("name", "ADCON"), new XAttribute("addr", "0x27")),
                           new XElement("reg", new XAttribute("name", "ADDATAH"), new XAttribute("addr", "0x28")),
                           new XElement("reg", new XAttribute("name", "ADDATAL"), new XAttribute("addr", "0x29"))
                           )
                     ),
                    //------------------------------------------------------------------------------------------------------
#if mc10p716
                    new XElement("MCU", new XAttribute("name", "MC10P716"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0716"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1000"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x1000"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x01ff"),
                        new XAttribute("chip_ramSize", "0x200")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1000"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x01ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "KEYL"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "KEYH"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "TLOAD0L"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "TLOAD0H"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "TLATL"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TLATH"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TCR0"), new XAttribute("addr", "0x0A")),
                          // new XElement("reg", new XAttribute("name", "TLATLL"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TDATA1"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TLOAD1"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "TCR1"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "OPA"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "P0LCON"), new XAttribute("addr", "0x10")),
                           new XElement("reg", new XAttribute("name", "P0KCON"), new XAttribute("addr", "0x11"))
                          // new XElement("reg", new XAttribute("name", "ADDATAL"), new XAttribute("addr", "0x29"))
                           )
                     ),
#endif
                     //---mc20p22 2012.11.15 datasheet v1.0---------------------------------------------------------------------------------------------------
                                           new XElement("MCU", new XAttribute("name", "MC20P22"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0222"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1800"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x800"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1800"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "P1HCON"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "TCNT0"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "TCR0"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TDATA1"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TCNT1"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "TCR1"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TDATA2H"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "TCNT2H"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "TDATA2L"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "TCNT2L"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "TCR2"), new XAttribute("addr", "0x10")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x11")),
                           new XElement("reg", new XAttribute("name", "ADCM"), new XAttribute("addr", "0x12")),
                           new XElement("reg", new XAttribute("name", "ADCON"), new XAttribute("addr", "0x13")),
                           new XElement("reg", new XAttribute("name", "ADTH"), new XAttribute("addr", "0x14")),
                           new XElement("reg", new XAttribute("name", "ADTL"), new XAttribute("addr", "0x15")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x16")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x17"))
                           )
                     ),
                     //---mc20p01----------------------------------------------------------------------------------------------------------
                     new XElement("MCU", new XAttribute("name", "MC20P01"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0201"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1c00"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x400"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1c00"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1c00"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "P1HCON"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "P1LCON"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "P1DCON"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TCNT0"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TCR0"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "DATA1"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TCNT1"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "TCR1"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x10"))
                           )
                     ),
                    //--mc20p801----------------------------------------------------------------------------------------------------
                    new XElement("MCU", new XAttribute("name", "MC20P801"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0281"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1c00"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x400"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1c00"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                             // new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x00")),
                           //new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x01")),
                           //new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "P1HCON"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "P1LCON"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "P1DCON"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TCNT0"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TCR0"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "DATA1"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TCNT1"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "TCR1"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x10"))
                           )
                     ),
                    //--mc20p02b 2012.11.15 @ datasheet v1.0-------------------------------------------------------------------------
                    new XElement("MCU", new XAttribute("name", "MC20P02B"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0202"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1800"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x800"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1800"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P0LCON"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "P1HCON"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "BKIM"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "P2"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "DDR2"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TDR"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "TCR"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x0E"))
                           //new XElement("reg", new XAttribute("name", "CMPC"), new XAttribute("addr", "0x0F")),
                           //new XElement("reg", new XAttribute("name", "CMP0A"), new XAttribute("addr", "0x10")),
                           //new XElement("reg", new XAttribute("name", "CMP1A"), new XAttribute("addr", "0x11"))

                           )
                     ),
#if gm20p04
                    //--mc20p04 2012.11.15 @ datasheet v1.0-------------------------------------------------------------------------
                    new XElement("MCU", new XAttribute("name", "mc20p04"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0204"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1000"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x1000"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1000"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                         new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "P0"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "DDR0"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "P0HCON"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "P0LCON"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "DDR1"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "P1HCON"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "BKIM"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "P2"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "DDR2"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TDR"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "TCR"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "INTC"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "CMPC"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "CMP0A"), new XAttribute("addr", "0x10")),
                           new XElement("reg", new XAttribute("name", "CMP1A"), new XAttribute("addr", "0x11"))
                           
                           )
                     ),
#endif
                     //-mc10p01b 2012.11.15 datashet v1.1------------------------------------------------------------------------------
                     new XElement("MCU", new XAttribute("name", "MC10P01B"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0101"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1c00"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x400"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1c00"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "PA"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "PB"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "DDRA"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "DDRB"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "TDR"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TCR"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "PC"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "DDRC"), new XAttribute("addr", "0x0E"))           
                           )
                     ),
                     //-MC10p11b 2012.11.15 datasheet v--------------------------------------------------------------------------------------------
                    new XElement("MCU", new XAttribute("name", "MC10P11B"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0111"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1C00"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x400"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1C00"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "KEY"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "IOR"), new XAttribute("addr", "0x02"))
                          // new XElement("reg", new XAttribute("name", "P1"), new XAttribute("addr", "0x03"))
                      
                           )
                     ),
                     //-MC10P02 2012.11.15 datasheet v1.2---------------------------------------------------------------------------
                     new XElement("MCU", new XAttribute("name", "MC10P02"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0102"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x1800"),
                        new XAttribute("chip_romEndAddr", "0x1fff"),
                        new XAttribute("chip_romSize", "0x800"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x1800"),
                        new XAttribute("ev_romEndAddr", "0x1fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "PA"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "PB"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "DDRA"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "DDRB"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "TDR"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TCR"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "KBIM"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "PC"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "DDRC"), new XAttribute("addr", "0x0E"))
                 
                           )
                     ),
                     //-MC20P38 2012.11.15 datasheet v1.0--------------------------------------------------------------------------
#if gm20p38
                     
                     new XElement("MCU", new XAttribute("name", "mc20p38"), new XAttribute("type", "HC05"), new XAttribute("id", "0x0238"),
                        new XElement(
                        "chip_cfg",
                        new XAttribute("chip_romFirstAddr", "0x2000"),
                        new XAttribute("chip_romEndAddr", "0x3fff"),
                        new XAttribute("chip_romSize", "0x2000"),
                        new XAttribute("chip_ramFirstAddr", "0x0000"),
                        new XAttribute("chip_ramEndAddr", "0x00ff"),
                        new XAttribute("chip_ramSize", "0x100")

                        ),
                        new XElement("ev_cfg",
                        new XAttribute("ev_romFirstAddr", "0x2000"),
                        new XAttribute("ev_romEndAddr", "0x3fff"),
                        new XAttribute("ev_ramFirstAddr", "0x0000"),
                        new XAttribute("ev_ramEndAddr", "0x00ff"),
                        new XAttribute("ev_bkFirstAddr", "0x8f00"),
                        new XAttribute("ev_bkEndAddr", "0x8f1f"),
                        new XAttribute("ev_pcFirstAddr", "0x9000"),
                        new XAttribute("ev_pcEndAddr", "0x9007"),
                        new XAttribute("ev_optionAddr", "0x9100"),
                        new XAttribute("ev_optionCnt", "0x04"),
                        new XAttribute("ev_traceFirstAddr", "0xc000"),
                        new XAttribute("ev_traceEndAddr", "0xc7ff")
                            ),
                        new XElement("syss",
                            new XElement("sys", new XAttribute("name", "PC"), new XAttribute("value", "0x1000"), new XAttribute("describe", "PC程序计数器")),
                            new XElement("sys", new XAttribute("name", "SP"), new XAttribute("value", "0xff"), new XAttribute("describe", "SP 堆栈指针")),
                            new XElement("sys", new XAttribute("name", "A"), new XAttribute("value", "0xff"), new XAttribute("describe", "A累加寄存器")),
                            new XElement("sys", new XAttribute("name", "X"), new XAttribute("value", "0x00"), new XAttribute("describe", "X变址寄存器")),
                            new XElement("sys", new XAttribute("name", "C"), new XAttribute("value", "0xff"), new XAttribute("describe", "C条件标示寄存器")),
                            new XElement("sys", new XAttribute("name", "Bit4:H "), new XAttribute("value", "0"), new XAttribute("describe", "H：半字节进位标志位")),
                            new XElement("sys", new XAttribute("name", "Bit3:I"), new XAttribute("value", "1"), new XAttribute("describe", "I：中断屏蔽标志位。为1时，禁止中断 ")),
                            new XElement("sys", new XAttribute("name", "Bit2:N"), new XAttribute("value", "0"), new XAttribute("describe", "N：负标志位")),
                            new XElement("sys", new XAttribute("name", "Bit1:Z"), new XAttribute("value", "0"), new XAttribute("describe", "Z:零标志位")),
                            new XElement("sys", new XAttribute("name", "Bit0:C"), new XAttribute("value", "0"), new XAttribute("describe", "C:进位、借位标志位"))
                            ),
                        new XElement("option",
                           new XElement("CP", "abcddd"),
                           new XElement("OSCM", "11:"),
                           new XElement("MCUtype", "1:mcu1 0:mcu2")
                            ),
                         new XElement("regs",
                           new XElement("reg", new XAttribute("name", "PA"), new XAttribute("addr", "0x00")),
                           new XElement("reg", new XAttribute("name", "PB"), new XAttribute("addr", "0x01")),
                           new XElement("reg", new XAttribute("name", "PC"), new XAttribute("addr", "0x02")),
                           new XElement("reg", new XAttribute("name", "DDRA"), new XAttribute("addr", "0x03")),
                           new XElement("reg", new XAttribute("name", "DDRB"), new XAttribute("addr", "0x04")),
                           new XElement("reg", new XAttribute("name", "PAHCON"), new XAttribute("addr", "0x05")),
                           new XElement("reg", new XAttribute("name", "PBHCON"), new XAttribute("addr", "0x06")),
                           new XElement("reg", new XAttribute("name", "TDR0"), new XAttribute("addr", "0x07")),
                           new XElement("reg", new XAttribute("name", "TCR0"), new XAttribute("addr", "0x08")),
                           new XElement("reg", new XAttribute("name", "TDR1"), new XAttribute("addr", "0x09")),
                           new XElement("reg", new XAttribute("name", "TCR1"), new XAttribute("addr", "0x0A")),
                           new XElement("reg", new XAttribute("name", "TDR2"), new XAttribute("addr", "0x0B")),
                           new XElement("reg", new XAttribute("name", "TCR2"), new XAttribute("addr", "0x0C")),
                           new XElement("reg", new XAttribute("name", "PPGC"), new XAttribute("addr", "0x0D")),
                           new XElement("reg", new XAttribute("name", "PPGT"), new XAttribute("addr", "0x0E")),
                           new XElement("reg", new XAttribute("name", "NTCON"), new XAttribute("addr", "0x0F")),
                           new XElement("reg", new XAttribute("name", "INTDB"), new XAttribute("addr", "0x10")),
                           new XElement("reg", new XAttribute("name", "CVREF"), new XAttribute("addr", "0x11")),
                           new XElement("reg", new XAttribute("name", "PPGDL"), new XAttribute("addr", "0x12")),
                           new XElement("reg", new XAttribute("name", "CMP0C"), new XAttribute("addr", "0x13")),
                           new XElement("reg", new XAttribute("name", "CMP1C"), new XAttribute("addr", "0x14")),
                           new XElement("reg", new XAttribute("name", "CMP2C"), new XAttribute("addr", "0x15")),
                           new XElement("reg", new XAttribute("name", "OPAC"), new XAttribute("addr", "0x16")),
                           new XElement("reg", new XAttribute("name", "INTC0"), new XAttribute("addr", "0x17")),

                            new XElement("reg", new XAttribute("name", "INTC1"), new XAttribute("addr", "0x18")),
                           new XElement("reg", new XAttribute("name", "ADCON"), new XAttribute("addr", "0x19")),
                           new XElement("reg", new XAttribute("name", "ADCM"), new XAttribute("addr", "0x1A")),
                           new XElement("reg", new XAttribute("name", "ADTH"), new XAttribute("addr", "0x1B")),
                           new XElement("reg", new XAttribute("name", "ADTL"), new XAttribute("addr", "0x1C")),
                           new XElement("reg", new XAttribute("name", "MCR"), new XAttribute("addr", "0x1D")),
                           new XElement("reg", new XAttribute("name", "RSTFR"), new XAttribute("addr", "0x1E")),
                           new XElement("reg", new XAttribute("name", "MCRX"), new XAttribute("addr", "0x1F"))
                         
                           )
                     )
#endif
                     //-------------------------------------------------------------------------------------------------------------
                   )
                  )
                 );
                //保存：会自动生成一个文件
                myXDoc.Save(xmlPath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
        //---------
        private void GetXmlNodeInformation(string xmlPath)
        {
            try
            {
                XElement rootNode = XElement.Load(xmlPath);
                IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("MCU")
                                                where (string)myTarget.Attribute("id").Value == "0x0224"
                                                select myTarget;

                IEnumerable<XElement> chipcfg=from srootNode in myvalue.Descendants("reg")select srootNode;
                int temp=0;
                foreach (XElement xnode in chipcfg)
                {

                    dataGridView_reg.Rows.Add();
                    dataGridView_reg.Rows[temp].Cells[0].Value = xnode.Attribute("name").Value;
                    dataGridView_reg.Rows[temp].Cells[1].Value = xnode.Attribute("addr").Value;
                    temp = temp + 1;
                    //int aa = Convert.ToInt16(romfaddr, 16);
                       
                   
                }
               // MessageBox.Show(romfaddr + "  " + romeaddr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public enum FilterPropertyType
        {
            None,
            FilterXmlSerializer,
            FilterBinaryFormatter
        }
        //displsy propertygrid
        private void FillPropertyGrid1(FilterPropertyType filter)
        {
            string[] Languages = new string[] { "English", "Italian", "Spanish", "Dutch" };
            MyOwnClass[] ListValues = new MyOwnClass[] { new MyOwnClass("English", 0), new MyOwnClass("Italian", 1), new MyOwnClass("Spanish", 2), new MyOwnClass("Dutch", 3) };
            int[] Values = new int[] { 1, 2, 3, 4 };
            MyOwnClass oInstance = new MyOwnClass("String value", 0);

            // The variable filter is used in the "Serialization Example"
            // The filter remove from the grid the properties not correctly supported
            // or not supported at all.

            Properties.ShowCustomProperties = true;
            Properties.Item.Clear();

            // Simple properties
            Properties.Item.Add("My Integer", 100, false, "Simple properties", "This is an integer", true);
            Properties.Item.Add("My Double", 10.4, false, "Simple properties", "This is a double", true);
            Properties.Item.Add("My String", "My Value", false, "Simple properties", "This is a string", true);
            if (filter != FilterPropertyType.FilterXmlSerializer)
            {
                Properties.Item.Add("My Font", new Font("Arial", 9), false, "Simple properties", "This is a font class", true);
                Properties.Item.Add("My Color", new Color(), false, "Simple properties", "This is a color class", true);
                Properties.Item.Add("My Point", new Point(10, 10), false, "Simple properties", "This is point class", true);
            }
            //Properties.Item.Add("My Date", new DateTime(DateAndTime.Today.Ticks), true, "Simple properties", "This is date class", true);
            Properties.Item.Add("My Enum", MyEnum.FirstEntry, false, "Simple properties", "Work with Enum too!", true);
            Properties.Item.Add("My Enum", myy.aa, false, "aaa", "显示中文不是可以的嘛", true);
            
            object ab = Properties;
            Properties.Item.Add("LVRE", ref ab, "LVRE", false, "Simple properties", "This is lvr fution select option.. Try it!", true);
            Properties.Item.Add("WDT", ref ab, "WDT", false, "Simple properties", "This is lvr fution select option.. Try it!", true);            

            // IsPassword attribute
            Properties.Item.Add("My Password", "password", false, ".NET v2.0 only", "This is a masked string." + "\r\n" + "(This feature is available only under .NET v2.0)", true);
            Properties.Item[Properties.Item.Count - 1].IsPassword = true;

            // Filename editor
            Properties.Item.Add("Filename", "", false, "Properties with custom UITypeEditor", "This property is a filename path. It define a custom UITypeConverter that show a OpenFileDialog or a SaveFileDialog when the user press the 3 dots button to edit the value.", true);
            Properties.Item[Properties.Item.Count - 1].UseFileNameEditor = true;
            Properties.Item[Properties.Item.Count - 1].FileNameDialogType = UIFilenameEditor.FileDialogType.LoadFileDialog;
            Properties.Item[Properties.Item.Count - 1].FileNameFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                // Custom Editor
                Properties.Item.Add("My Custom Editor", "", false, "Properties with custom UITypeEditor", "The component accept custom UITypeEditor.", true);
                Properties.Item[Properties.Item.Count - 1].CustomEditor = new MyEditor();

                // Custom Event Editor
               // Properties.Item.Add("My Custom Event", "Click me", false, "Properties with custom UITypeEditor", "The component accept custom UITypeEditor.", true);
               // Properties.Item[Properties.Item.Count - 1].OnClick += this.CustomEventItem_OnClick;

                // Custom TypeConverter
                Properties.Item.Add("Integer", 1, false, "Properties with custom TypeConverter", "This property have a custom type converter that show a custom error message.", true);
                Properties.Item[Properties.Item.Count - 1].CustomTypeConverter = new MyTypeConverter();
            }

            // Custom Choices Type Converter
            if (filter != FilterPropertyType.FilterXmlSerializer)
            {
                Properties.Item.Add("Language", "", false, "Properties with custom TypeConverter", "This property uses a TypeConverter to dropdown a list of values.", true);
                Properties.Item[Properties.Item.Count - 1].Choices = new CustomChoices(Languages, true);

                Properties.Item.Add("Values", 1, false, "Properties with custom TypeConverter", "This property uses a TypeConverter to dropdown a list of values.", true);
                Properties.Item[Properties.Item.Count - 1].Choices = new CustomChoices(Values, false);
            }

            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                // Expandable Type Converter			
                Properties.Item.Add("My object", oInstance, false, "Properties with custom TypeConverter", "This property make a \'MyOwnClass\' instance browsable.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;
                Properties.Item[Properties.Item.Count - 1].BrowsableLabelStyle = BrowsableTypeConverter.LabelStyle.lsEllipsis;
            }

            // Dynamic properties
            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                object grid = Properties;
                Properties.Item.Add("Autosize properties", ref grid, "AutoSizeProperties", false, "Dynamic Properties", "This is a dynamic bound property. It changes the autosize property of this grid. Try it!", true);
                Properties.Item.Add("Draw flat toolbar", ref grid, "DrawFlatToolbar", false, "Dynamic Properties", "This is a dynamic bound property. It changes the DrawFlatToolbar property of this grid. Try it!", true);

                object form = this;
                Properties.Item.Add("Form opacity", ref form, "Opacity", false, "Dynamic Properties", "This is a dynamic bound property. It changes the Opacity property of this form. Try it!", true);
                Properties.Item[Properties.Item.Count - 1].IsPercentage = true;

                // PropertyGridEx
                Properties.Item.Add("Item", ref grid, "Item", false, "PropertyGridEx", "Represent the PropertyGridEx Item collection.", true);
                Properties.Item[Properties.Item.Count - 1].Parenthesize = true;

                Properties.Item.Add("DocComment", ref grid, "DocComment", false, "PropertyGridEx", "Represent the DocComment usercontrol of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;

                Properties.Item.Add("Image", ref grid, "DocCommentImage", false, "PropertyGridEx", "Represent the DocComment usercontrol of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].DefaultValue = null;
                Properties.Item[Properties.Item.Count - 1].DefaultType = typeof(Image);

                Properties.Item.Add("Toolstrip", ref grid, "Toolstrip", false, "PropertyGridEx", "Represent the toolstrip of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;

            }
            if (filter == FilterPropertyType.FilterBinaryFormatter)
            {

                // Databinding works with serialization
                Properties.Item.Add("Array of objects", ListValues[2].Text, false, "Databinding", "This is a UITypeEditor that implement a listbox", true);
                Properties.Item[Properties.Item.Count - 1].ValueMember = "Value";
                Properties.Item[Properties.Item.Count - 1].DisplayMember = "Text";
                Properties.Item[Properties.Item.Count - 1].Datasource = ListValues;
            }

            Properties.Refresh();

        }

        private void Properties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            label1.Text = e.ChangedItem.Value.ToString();
            label2.Text = Properties.Item[13].Value.ToString();
        }
        public int a=0;
        private void button1_Click(object sender, EventArgs e)
        {
             
            a=a+1;
            Properties.Item[0].Value = a;
            a += 1;
            Properties.Item[1].Value=a;
            
            Properties.Refresh();
        }
    }

}
