
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using FarsiLibrary.Win;
using System.IO.Ports;
using System.Timers;
using WeifenLuo.WinFormsUI.Docking;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using CmdSNLink;
using SinoSunIDE.Languages;

namespace SinoSunIDE
{
    partial class frmMAIN
    {

        #region keyworks list

        string[] keywords = { 
//"define","ds","db","else","end","endc","endif","endm","endw","equ","error","errorlevel","exitm","expand",
//"extern","idata","if","ifdef","ifndef","list","local","macro","noexpand ","nolist","org","fdb","fcb",
//"page","pagesel","processor","radix","res","rmb","set","space",
//"subtitle","title","udata","udata_ovr","udata_shr","variable","while","adc","add","and",
//"asl","asla","aslx","asr","asra","asrx","bcc","bclr","bcs","beq","bhcc","bhcs","bhi","bhs",
//"bih","bil","bit","blo","bls","bmc","bmi","bms","bne","bpl","bra","brclr","brn","brset","bset", 
//"bsr","clc","cli","clr","clra","clrx","cmp","com","coma","comx","cpx","dec","deca","decx","eor",
//"end","inc","inca","incx","jmp","jsr","lda","ldx","lsl","lsla","lslx","lsr","lsra","lsrx","mul", 
//"neg","nega","negx","nop","ora","rol","rola","rolx","ror","rora","rorx","rsp","rti","rts","sbc", 
//"sec","sei","sta","stop","stx","sub","swi","tax","tst","tsta","tstx","txa","wait","t0cnt", 
//"t0data","t0con","tdr","tcr","mcr","btcon","btclr","btcnt","adcon","addatah","addatal",
//"kbim","ddr2","ddr1","ddr0","p0","p1","p2","p0hcon","p0lcon","p1hcon","port0","port1","port2",
//"p0conh","p0conl","p0pnd","p1con","p2conh","p2conl","pwmdata","pwmcon","rstfr",
//"DEFINE","DS","DB","ELSE","END","ENDC","ENDIF","ENDM","ENDW","EQU","ERROR","ERRORLEVEL","EXITM","EXPAND",
//"EXTERN","IDATA","IF","IFDEF","IFNDEF","LIST","LOCAL","MACRO","NOEXPAND ","NOLIST","ORG","FDB","FCB",
//"PAGE","PAGESEL","PROCESSOR","RADIX","RES","RMB","SET","SPACE",
//"SUBTITLE","TITLE","UDATA","UDATA_OVR","UDATA_SHR","VARIABLE","WHILE","ADC","ADD","AND",
//"ASL","ASLA","ASLX","ASR","ASRA","ASRX","BCC","BCLR","BCS","BEQ","BHCC","BHCS","BHI","BHS",
//"BIH","BIL","BIT","BLO","BLS","BMC","BMI","BMS","BNE","BPL","BRA","BRCLR","BRN","BRSET","BSET", 
//"BSR","CLC","CLI","CLR","CLRA","CLRX","CMP","COM","COMA","COMX","CPX","DEC","DECA","DECX","EOR",
//"END","INC","INCA","INCX","JMP","JSR","LDA","LDX","LSL","LSLA","LSLX","LSR","LSRA","LSRX","MUL", 
//"NEG","NEGA","NEGX","NOP","ORA","ROL","ROLA","ROLX","ROR","RORA","RORX","RSP","RTI","RTS","SBC", 
//"SEC","SEI","STA","STOP","STX","SUB","SWI","TAX","TST","TSTA","TSTX","TXA","WAIT","T0CNT", 
//"T0DATA","T0CON","TDR","TCR","MCR","BTCON","BTCLR","BTCNT","ADCON","ADDATAH","ADDATAL",
//"KBIM","DDR2","DDR1","DDR0","P0","P1","P2","P0HCON","P0LCON","P1HCON","PORT0","PORT1","PORT2",
//"P0CONH","P0CONL","P0PND","P1CON","P2CONH","P2CONL","PWMDATA","PWMCON","RSTFR"
                            };
        static string keyword_str = @"define ds db else end endc endif endm endw equ error errorlevel exitm expand 
                extern idata if ifdef ifndef list local macro noexpand  nolist org fdb fcb 
                page pagesel processor radix res rmb set space subtitle title udata 
                udata_ovr udata_shr variable while adc add and asl asla aslx asr asra asrx
                bcc bclr bcs beq bhcc bhcs bhi bhs bih bil bit blo bls bmc bmi bms bne bpl 
                bra brclr brn brset bset bsr clc cli clr clra clrx cmp com coma comx cpx 
                dec deca decx eor end inc inca incx jmp jsr lda ldx lsl lsla lslx lsr lsra 
                lsrx mul negnega negx nop ora rol rola rolx ror rora rorx rsp rti rts sbc 
                sec sei sta stop stx sub swi tax tst tsta tstx txa wait
                DEFINE DS DB ELSE END ENDC ENDIF ENDM ENDW EQU ERROR ERRORLEVEL EXITM EXPAND 
                EXTERN IDATA IF IFDEF IFNDEF LIST LOCAL MACRO NOEXPAND  NOLIST ORG FDB FCB 
                PAGE PAGESEL PROCESSOR RADIX RES RMB SET SPACE SUBTITLE TITLE UDATA 
                UDATA_OVR UDATA_SHR VARIABLE WHILE ADC ADD AND ASL ASLA ASLX ASR ASRA ASRX
                BCC BCLR BCS BEQ BHCC BHCS BHI BHS BIH BIL BIT BLO BLS BMC BMI BMS BNE BPL 
                BRA BRCLR BRN BRSET BSET BSR CLC CLI CLR CLRA CLRX CMP COM COMA COMX CPX 
                DEC DECA DECX EOR END INC INCA INCX JMP JSR LDA LDX LSL LSLA LSLX LSR LSRA 
                LSRX MUL NEGNEGA NEGX NOP ORA ROL ROLA ROLX ROR RORA RORX RSP RTI RTS SBC 
                SEC SEI STA STOP STX SUB SWI TAX TST TSTA TSTX TXA WAIT";
        #endregion


        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do\n{\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
        string[] declarationSnippets = { 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };

        // Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        //Color currentLineColor = Color.FromArgb(200, 210, 210, 255);
        Color currentLineColor = Color.Red;
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

        private FrmREG frmREG = new FrmREG();
        private FrmMessage frmmessage = new FrmMessage();
        private FrmExplorer frmexplorer = new FrmExplorer();
        private FrmEditor fs = new FrmEditor();
        private FrmFile frmfile = new FrmFile();
        private FrmBreakPoint frmBreakPoint = new FrmBreakPoint();
        private FrmRAM frmRAM = new FrmRAM();
        private FrmROM frmROM = new FrmROM();
        private FrmPC frmPC = new FrmPC();// option 
        private FrmSys frmSys = new FrmSys();
        private FrmWatch frmWatch = new FrmWatch();
        private FrmMTPRAM frmMTPRAM = new FrmMTPRAM();



        private DeserializeDockContent m_deserializeDockContent;
        private RecentFileHandler recentFileHandler = new RecentFileHandler();
        private RecentFileHandler recentProjectHandler = new RecentFileHandler();

        private System.Windows.Forms.Timer busy_Timer = new System.Windows.Forms.Timer();
        private bool interrupt_flag = true;
        public static bool DebugFlag = false;

        //data recieve timeout
        System.Timers.Timer DataReceiveTimer = new System.Timers.Timer(1000);

        //DataReceiveTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //DataReceiveTimer.Enabled = false;
        //stopwatch 
        private Stopwatch stw_time = new Stopwatch(); //watch stepin time

        //watcher asm file change
        FileSystemWatcher ASM_watcher = new FileSystemWatcher();

        public static DeviceConfig DeviceConfigXX = new DeviceConfig();

        public int BookmarkPointer = 0;
        public string AccessTime;
        public byte TimeOutFlag = 0;
        public byte[] ReadDataBuffer = new byte[4096];
        public byte[] ROMData = new byte[4096];
        public byte[] RAMDataBuffer = new byte[1024];
        public bool ROMDATA_flag = false;
        public byte[] MCU_OPTION = { 0x69, 0x14, 9, 0x00, 0x00, 0x91, 0x00, 0x75, 0xfc, 0xff, 0xff, 0xbc };
        public static byte[] MCU_Breakpoint ={0x69,0x14,37,0x00,0x00,0x8f,0x00,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                             0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0x6d};
        // path : E:/xxxx/xxx/  EXE的目录
        public static string APPLICATION_PATH = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public string ProjectPath = null;
        //private string[] ASMFileName=new string[];
        public static PCport COMPORT = new PCport();
        private UInt16[] BreakPointList = { 0xffff, 0xffff, 0xffff, 0xffff, 0xffff,
                                            0xffff, 0xffff, 0xffff, 0xffff, 0xffff,
                                            0xffff, 0xffff, 0xffff, 0xffff, 0xffff,0xffff};//new UInt16[16];

        public static string MCU_ID = null;
        public static string MCU_TYPE = null;
        public static UInt32 OPTION = 0x00;
        public static UInt32 OPTION0 = 0x00;
        public static UInt32 OPTION1 = 0x00;
        public static UInt32 OPTION2 = 0x00;
        public static UInt32 OPTION3 = 0x00;
        public static UInt32 OPTION4 = 0x00;
        public static UInt32 OPTION5 = 0x00;
        public static UInt32 OPTION6 = 0x00;
        public static UInt32 OPTION7 = 0x00;
        public static bool OPTIONNullFlag = true;
        public static bool OPTION0NullFlag = true;
        public static bool OPTION1NullFlag = true;
        public static bool OPTION2NullFlag = true;
        public static bool OPTION3NullFlag = true;
        public static bool OPTION4NullFlag = true;
        public static bool OPTION5NullFlag = true;
        public static bool OPTION6NullFlag = true;
        public static bool OPTION7NullFlag = true;

        public static byte FREQ = 0x08;
        public static byte SP_Value = 0x08;
        //private bool fileChangedFlag = false;
        private bool compile_result = true;

        private List<string> ASMFileName = new List<string>(); //storage project asm file
        private List<string> ListFileName = new List<string>(); //storage project list file
        private List<string> NewEditeFileList = new List<string>(); //storage openfile last write info
        private List<string> OldEditeFileList = new List<string>();//storage the openfile old write info
        private int textIndex = 0;
        private int DebugLineNumber = 0;

        FastColoredTextBox listFile = new FastColoredTextBox();
        string gValiableFile = null;

        //tool tips
        Point lastMouseCoord = Point.Empty;

        //timer counter
        private const int timer_count = 100; // 100ms

        private static string stroutput = null;
        private static string strerror = null;

        public void update_check()
        {
//             string AutoUpdaterFileName = @"http://www.dzmi.com/WinScopeIDE/update.xml";
//             string theLastsUpdateValue = "";
//             string thePreUpdateValue = "";
//             string theFileUrl = "";
//             try
//             {
//                 //XElement rootNode = XElement.Load(AutoUpdaterFileName);
//                 WebClient wc = new WebClient();
//                 Stream sm = wc.OpenRead(AutoUpdaterFileName);
//                 XmlTextReader xml = new XmlTextReader(sm);
//                 while (xml.Read())
//                 {
//                     if (xml.Name == "Option")
//                     {
//                         theLastsUpdateValue = xml.GetAttribute("Value");
//                         theFileUrl = xml.GetAttribute("fileUrl");
//                         break;
//                     }
//                 }
//                 xml.Close();
//                 sm.Close();
//                 wc.Dispose();
// 
//                 string str = frmMAIN.APPLICATION_PATH + "global.ini";
//                 XElement rootNode = XElement.Load(str);
// 
//                 IEnumerable<XElement> myvalue = from myTarget in rootNode.Descendants("LastUpDate")
//                                                 select myTarget;
//                 foreach (XElement xnode in myvalue)
//                 {
//                     thePreUpdateValue = xnode.Attribute("Value").Value;
//                 }
// 
//             }
//             catch
//             {
//                 MessageBox.Show(ShowLanguage.Current.MessageBoxText16 + "\n" + ShowLanguage.Current.MessageBoxText17,
//                     ShowLanguage.Current.MessageBoxCaption16, MessageBoxButtons.OK, MessageBoxIcon.Error);
//                 return;
//             }
// 
//             if (ShouldUpdate(theLastsUpdateValue, thePreUpdateValue))
//             {
//                 DialogResult BResult = MessageBox.Show("软件版本有更新，是否进行更新？", "更新提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
//                 if (BResult == DialogResult.OK)
//                 {
//                     System.Diagnostics.Process.Start(Application.StartupPath + @"\AutoUpdater.exe", theFileUrl);
//                 }
//             }
//             else
//             {
//                 MessageBox.Show("当前软件已经是最新的，无需更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                 RemoveOldSetupFile();
//             }

            //             if (Convert.ToDateTime(thePreUpdateDate) >= Convert.ToDateTime(theLastsUpdateDate))
            //             {
            //                 //MessageBox.Show("当前软件已经是最新的，无需更新！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // 
            //             }
            //             else
            //             {
            //                 MessageBox.Show(ShowLanguage.Current.MessageBoxText18, ShowLanguage.Current.MessageBoxCaption18, MessageBoxButtons.OK,
            //                     MessageBoxIcon.Information);
            //                 //this.Close();
            //             }
        }

        private void RemoveOldSetupFile()
        {
            try
            {
                string temp = System.Environment.GetEnvironmentVariable("TEMP");
                string folder = new DirectoryInfo(temp).FullName;
                if (File.Exists(folder + @"\" + "Setup.exe"))
                {
                    File.Delete(folder + @"\" + "Setup.exe");
                }
                if (File.Exists(folder + @"\" + "Setup.msi"))
                {
                    File.Delete(folder + @"\" + "Setup.msi");
                }
            }
            catch { }

        }

        private bool ShouldUpdate(string serverVersion, string localVersion)
        {
            if (!string.IsNullOrEmpty(serverVersion) && !string.IsNullOrEmpty(localVersion))
            {
                return serverVersion.CompareTo(localVersion) > 0;
            }
            return true;
        }


        internal class DynamicCollection : IEnumerable<AutocompleteItem>
        {
            private FastColoredTextBox tb;

            public DynamicCollection(FastColoredTextBox tb)
            {
                this.tb = tb;
            }

            public IEnumerator<AutocompleteItem> GetEnumerator()
            {
                return BuildList().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private IEnumerable<AutocompleteItem> BuildList()
            {
                //find all words of the text
                var words = new Dictionary<string, string>();

                foreach (Match m in Regex.Matches(keyword_str, @"\b\w+\b"))
                    words[m.Value] = m.Value;
                //tb.AppendText(keyword_str);
                foreach (Match m in Regex.Matches(tb.Text, @"\b\w+\b"))
                    words[m.Value] = m.Value;

                //return autocomplete items
                int index = 0;
                int index_m = 2;
                foreach (var word in words.Keys)
                {
                    index++;
                    if (index > 260)
                        index_m = 3;
                    yield return new AutocompleteItem(word, index_m);
                }
            }
        }

    }


}