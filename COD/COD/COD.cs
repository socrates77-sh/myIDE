using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastColoredTextBoxNS;

namespace COD
{
    public class CODDebug 
    {
        #region const define

        private const UInt32 COD_DIR_CODE = 0;    /* code block indices are at the start */
        private const UInt32 COD_DIR_SOURCE = 257;    /* source file name */
        private const UInt32 COD_DIR_DATE = 321;    /* date .cod file was created */
        private const UInt32 COD_DIR_TIME = 328;    /* time .cod file was created */
        private const UInt32 COD_DIR_VERSION = 331;    /* Compiler version */
        private const UInt32 COD_DIR_COMPILER = 351;    /* Compiler name */
        private const UInt32 COD_DIR_NOTICE = 363;    /* Compiler copyright */
        private const UInt32 COD_DIR_SYMTAB = 426;    /* Start block of short symbol table */
        private const UInt32 COD_DIR_NAMTAB = 430;    /* Start block of file name table */
        private const UInt32 COD_DIR_LSTTAB = 434;    /* Start block of list file cross reference */
        private const UInt32 COD_DIR_ADDRSIZE = 438;    /* # of bytes for an address */
        private const UInt32 COD_DIR_HIGHADDR = 439;    /* High word of address for 64K Code block */
        private const UInt32 COD_DIR_NEXTDIR = 441;    /* Next directory block */
        private const UInt32 COD_DIR_MEMMAP = 443;    /* Start block of memory map */
        private const UInt32 COD_DIR_LOCALVAR = 447;    /* Start block of local variables */
        private const UInt32 COD_DIR_CODTYPE = 451;    /* Type of .cod file */
        private const UInt32 COD_DIR_PROCESSOR = 454;    /* Target processor */
        private const UInt32 COD_DIR_LSYMTAB = 462;    /* Start block of long symbol table */
        private const UInt32 COD_DIR_MESSTAB = 466;    /* Start block of debug message area */

        private const UInt32 COD_BLOCK_SIZE = 512;

        string[] SymbolType4 = {
  "a_reg          ", "x_reg          ", "c_short        ", "c_long         ",
  "c_ushort       ", "c_ulong        ", "c_pointer      ", "c_upointer     ",
  "table          ", "m_byte         ", "m_boolean      ", "m_index        ",
  "byte_array     ", "u_byte_array   ", "word_array     ", "u_word_array   ",
  "func_void_none ", "func_void_byte ", "funcVoidTwobyte", "func_void_long ",
  "func_void_undef", "func_int_none  ", "func_int_byte  ", "func_int_twobyt",
  "func_int_long  ", "func_int_undef ", "func_long_none ", "func_long_byte ",
  "funcLongTwobyte", "func_long_long ", "func_long_undef", "pfun_void_none ",
  "pfun_void_byte ", "pfunVoidTwobyte", "pfun_void_long ", "pfun_void_undef",
  "pfun_int_none  ", "pfun_int_byte  ", "pfunIntTwobyte ", "pfun_int_long  ",
  "pfun_int_undef ", "pfun_long_none ", "pfun_long_byte ", "pfun_long_twoby",
  "pfun_long_long ", "pfun_long_undef", "address        ", "constant       ",
  "bad_und        ", "br_und         ", "upper_und      ", "byte_und       ",
  "add_und        ", "m_keyword      ", "add_mi_und     ", "vector         ",
  "port_w         ", "port_r         ", "port_rw        ", "port_rmw       ",
  "endif          ", "exundef        ", "macro_t        ", "macro_s        ",
  "macro_a        ", "macro_c        ", "c_keyword      ", "void           ",
  "c_enum         ", "c_typedef1     ", "c_utypedef1    ", "c_typedef2     ",
  "c_utypedef2    ", "cp_typedef1    ", "cp_utypedef1   ", "cp_typedef2    ",
  "cp_utypedef2   ", "c_struct       ", "i_struct       ", "l_const        ",
  "s_short        ", "s_ushort       ", "s_long         ", "s_ulong        ",
  "sa_short       ", "sa_ushort      ", "sa_long        ", "sa_ulong       ",
  "sp_short       ", "sp_ushort      ", "sp_long        ", "sp_ulong       ",
  "FixedPointer   ", "PointerFunction", "cc_reg         ", "PtrF_void_none ",
  "PtrF_void_byte ", "PtrF_void_twobyt", "PtrF_void_long ", "PtrF_void_undef",
  "PtrF_int_none  ", "PtrF_int_byte  ", "PtrF_int_twobyte", "PtrF_int_long  ",
  "PtrF_int_undef ", "PtrF_long_none ", "PtrF_long_byte ",
  "PtrF_long_twobyte", "PtrF_long_long ", "PtrF_long_undef",
  "PfAR_void_none ", "PfAR_void_byte ", "PfAR_void_twobyte",
  "PfAR_void_long ", "PfAR_void_undef", "PfAR_int_none  ", "PfAR_int_byte  ",
  "PfAR_int_twobyte", "PfAR_int_long  ", "PfAR_int_undef ", "PfAR_long_none ",
  "PfAR_long_byte ", "PfAR_long_twobyte", "PfAR_long_long ",
  "PfAR_long_undef", "FWDlit         ", "Pfunc          ", "mgoto          ",
  "mcgoto         ", "mcgoto2        ", "mcgoto3        ", "mcgoto4        ",
  "mcgoto74       ", "mcgoto17       ", "mccall17       ", "mcall          ",
  "mc_call        ", "res_word       ", "local          ", "unknown        ",
  "varlabel       ", "external       ", "global         ", "segment        ",
  "Bankaddr       ", "bit_0          ", "bit_1          ", "bit_2          ",
  "bit_3          ", "bit_4          ", "bit_5          ", "bit_6          ",
  "bit_7          ", "debug          "
};

        #endregion

            //LineSymbol
    //public struct LineSymbol{

    //    char sfile;
    //    char smod;
    //    UInt16 sline;
    //    UInt16 sloc;
    //};

        private byte[] CodArr = null;
        private List<string> source_file_names = new List<string>();
        

        private FastColoredTextBox DebugFile = new FastColoredTextBox();

        public string ReadBin2Byte(string cod_file_path)
        {
            //bsGetFiles.GetFiles(frmMAIN.ProjectPath,cod)
            //   FileStream fs=File.OpenRead("G:\Emulator\IDE\testCode\top.cod");

            //FileStream fsMyfile = new FileStream(@"G:\Emulator\IDE\testCode\top.cod", FileMode.Create, FileAccess.ReadWrite);

            CodArr = File.ReadAllBytes(cod_file_path);

            directory_block();
            dump_memmap();
            demp_code();
            dump_symbols();
            duymp_lsymbols();
            dump_source_files();
            demp_line_symbols();

             //File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
//             string SaveFilePath = Path.GetDirectoryName(cod_file_path);
//             File.WriteAllText(SaveFilePath + "\\" + "aaa" + @".lst", DebugFile.Text, Encoding.Default);
            return DebugFile.Text;
            
        }

        void directory_block()
        {
            DebugFile.AppendText("directory block \n");

            UInt32 Inttemp = gp_getUint16(COD_DIR_CODTYPE);

            DebugFile.AppendText("COD file Version" + " " + Inttemp.ToString() + "\n");
            Inttemp = gp_getUint16(COD_DIR_SOURCE);
            DebugFile.AppendText("Source file" + "  " + gp_get64Bytes2String(COD_DIR_SOURCE) + "\n");
            DebugFile.AppendText("Date" + "  " + gp_getNBytes2String(COD_DIR_DATE, 7) + "\n");

            Inttemp = gp_getUint16(COD_DIR_TIME);
            string h = (Inttemp / 100).ToString();
            string m = (Inttemp % 100).ToString();
            DebugFile.AppendText("Time" + "  " + h + ":" + m + "\n");

            DebugFile.AppendText("compiler version" + "  " + gp_getNBytes2String(COD_DIR_VERSION, 19) + "\n");
            DebugFile.AppendText("Notice   " + gp_getNBytes2String(COD_DIR_NOTICE, 64) + "\n");
            DebugFile.AppendText("Processor     " + gp_getNBytes2String(COD_DIR_PROCESSOR, 8) + "\n");

            UInt32 byte_for_address = gp_getUint16(COD_DIR_ADDRSIZE);
            DebugFile.AppendText("Bytes for address:  " + byte_for_address.ToString() + "\n");

            if (byte_for_address != 4)
            {
                DebugFile.AppendText("WARNING: address size looks suspicious\n");
            }

            // Inttemp=gp_getUint16(COD_DIR_HIGHADDR);
            DebugFile.AppendText("High word of 64K address  " + gp_getStr16(COD_DIR_HIGHADDR) + "\n");

            DebugFile.AppendText("short symbol table block:         " + gp_getStr16(COD_DIR_SYMTAB) + "     end block:   " + gp_getStr16(COD_DIR_SYMTAB + 2) + "\n");

            DebugFile.AppendText("Long symbol table start block:    " + gp_getStr16(COD_DIR_LSYMTAB) + "     end block:   " + gp_getStr16(COD_DIR_LSYMTAB + 2) + "\n");
            DebugFile.AppendText("File name table start block:      " + gp_getStr16(COD_DIR_NAMTAB) + "     end block:   " + gp_getStr16(COD_DIR_NAMTAB + 2) + "\n");

            DebugFile.AppendText("Source info table start block:    " + gp_getStr16(COD_DIR_LSTTAB) + "     end block:   " + gp_getStr16(COD_DIR_LSTTAB + 2) + "\n");
            DebugFile.AppendText("Rom table start block:            " + gp_getStr16(COD_DIR_MEMMAP) + "     end block:   " + gp_getStr16(COD_DIR_MEMMAP + 2) + "\n");
            DebugFile.AppendText("Local scope table start block:    " + gp_getStr16(COD_DIR_LOCALVAR) + "     end block:   " + gp_getStr16(COD_DIR_LOCALVAR + 2) + "\n");
            DebugFile.AppendText("Debug messages start block:       " + gp_getStr16(COD_DIR_MESSTAB) + "     end block:   " + gp_getStr16(COD_DIR_MESSTAB + 2) + "\n");

            DebugFile.AppendText("\nNext directory block");
            if (gp_getUint16(COD_DIR_NEXTDIR) != 0)
            {
                DebugFile.AppendText(": " + gp_getStr16(COD_DIR_NEXTDIR) + "\n"); ;
            }
            else
            {
                DebugFile.AppendText(" is empty\n");
            }


           // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
        }

        /// <summary>
        /// ROM usage information
        /// </summary>
        private void dump_memmap()
        {
            const UInt32 COD_MAPTAB_START = 0;
            const UInt32 COD_MAPTAB_LAST = 2;
            const UInt32 COD_MAPENTRY_SIZE = 4;

            UInt32 _64k_base,OffsetFlag;
            UInt32 i,j,start_block,end_block;
            UInt32 first=1;

            //UInt32 dbi=20; //?
            OffsetFlag = 0;
            //do 
            //{
                   _64k_base =(UInt32) (gp_getUint16(COD_DIR_HIGHADDR) << 16);
                   start_block =gp_getUint16(COD_DIR_MEMMAP);
                   end_block =gp_getUint16(COD_DIR_MEMMAP + 2);
                    if (first==1)
                    {
                        DebugFile.AppendText("\n\nROM Usage\n");
                        DebugFile.AppendText("--------------------------------------------------\n");
                        first = 0;
                    }
                    for (j = start_block; j <= end_block;j++ )
                    {
                        OffsetFlag = (j * COD_BLOCK_SIZE); //读入一个BLOCK ,512字节.
                        for(i=0;i<128;i++)
                        {
                            UInt32 start;
                            UInt32 last;
                            start =gp_getUint16(OffsetFlag + i * COD_MAPENTRY_SIZE + COD_MAPTAB_START);
                            last = gp_getUint16(OffsetFlag + i * COD_MAPENTRY_SIZE + COD_MAPTAB_LAST);

                            if(!((start==0)&&(last==0)))
                            {
                                last = last + 1;
                               // int Faddr = gp_getUint16(_64k_base + start);
                                string Fstr = (start >> 1).ToString("X6");
                                //int Eaddr = gp_getUint16(_64k_base + last)+1;
                                string Estr = ((last >> 1) - 1).ToString("X6");
                                DebugFile.AppendText("using ROM  0x" + Fstr+" to  0x"+Estr+"\n");//fution less
                            }
                        }
                    }

               
                if (first==1)
                {
                    DebugFile.AppendText("  No ROM usage information availabe.\n");
                }

               
                
           // } while (true);
            

        }

        ///<summary>
        ///Dump all of the machine code in the .cod file
        ///</summary>
        void demp_code()
        {
            UInt32 _64k_base,OffsetFlag;
            UInt16 i, j, k, all_zero_line, index;

            //demp_memmap();
            DebugFile.AppendText("\n\nFormatted Code Dump\n");
            DebugFile.AppendText("------------------------------\n");

            _64k_base = (UInt32)(gp_getUint16(COD_DIR_HIGHADDR) << 16);
            for(k=0;k<=127;k++)
            {
                index = gp_getUint16(2 * (COD_DIR_CODE + k));
                if(index !=0)
                {
                    OffsetFlag = (UInt32)(index * 512);
                    i = 0;
                    do 
                    { 
                        for (j = 0, all_zero_line = 1; j < 8;j++ )
                        {
                            UInt32 offset = (UInt32)(OffsetFlag + (i + j) * 2);
                            if (gp_getUint16(offset)!=0)
                            {
                                all_zero_line = 0;
                            }
                        }
                        if (all_zero_line==1)
                        {
                            i += 8;
                        }
                        else
                        {
                            UInt32 addr=(UInt32)(_64k_base+2*(i+k*256));
                           string StrAddr=(addr>>1).ToString("X6");
                           DebugFile.AppendText("\n" + StrAddr+":   ");
                           for (j = 0; j < 8;j++ )
                           {
                               UInt32 offset = (UInt32)(OffsetFlag + 2 * i);
                               DebugFile.AppendText(gp_getStr16(offset)+"   ");
                               i++;
                           }
                        }
                    } while (i<COD_BLOCK_SIZE/2);

                    DebugFile.AppendText("\n");
                }

            }

           // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
        }

        private void dump_symbols()
        {
            const UInt32 SSYMBOL_SIZE = 16;
            const UInt32 SYMBOLS_PER_BLOCK = COD_BLOCK_SIZE / SSYMBOL_SIZE;

            //const UInt32 SR_LEN = 0;
            const UInt32 SR_NAME = 1;
            const UInt32 SR_TYPE = 13;
            const UInt32 SR_VALUE = 14;

            UInt32 offsetFlag=0;
            UInt16 i, j, start_block, end_block;
            //char b[16];

            start_block=gp_getUint16(COD_DIR_SYMTAB);

            if (start_block !=0)
            {
                end_block=gp_getUint16(COD_DIR_SYMTAB+2);
                DebugFile.AppendText("\nSymbol Table Information\n");
                DebugFile.AppendText("---------------------------------------\n");
                for(j=start_block;j<=end_block;j++)
                {
                    offsetFlag=j*COD_BLOCK_SIZE;

                    for(i=0;i<SYMBOLS_PER_BLOCK;i++)
                    {
                        if(CodArr[offsetFlag+i*SSYMBOL_SIZE+SR_NAME] !=0 )
                        {
                            UInt32 ofs=(UInt32)(offsetFlag+i*SSYMBOL_SIZE+SR_NAME);
                            UInt32 ofi=(UInt32)(offsetFlag+i*SSYMBOL_SIZE+SR_VALUE);
                            UInt32 oft=(UInt32)(offsetFlag+i*SSYMBOL_SIZE+SR_TYPE);
                            string s1=gp_getNBytes2String(ofs,12);
                            string val=gp_getStr16(ofi);
                            DebugFile.AppendText(s1+"="+val+",  type="+SymbolType4[oft]+"\n");
                        }
                    }

                }
            } 
            else
            {
                DebugFile.AppendText("No symbol talbe info\n");
            }

           // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
        }

        /*************************************************
         * 
         * Dump all of the Long Symbol Table stuff in the .cod file
         * *************************************************/
        private void duymp_lsymbols()
        {
            UInt16 s,length;
            UInt16 type;
            UInt16 i, j, start_block, end_block;
            UInt32 value;
            //char[256] b;
            UInt32 offsetFlag;

            start_block = gp_getUint16(COD_DIR_LSYMTAB);

            if(start_block !=0)
            {
                end_block = gp_getUint16(COD_DIR_LSYMTAB + 2);
                DebugFile.AppendText("\nLong Symbole Table Information\n");
                DebugFile.AppendText("----------------------------------------\n\n");
                for(j=start_block;j<=end_block;j++)
                {
                    offsetFlag = j * COD_BLOCK_SIZE;
                    for(i=0;i<COD_BLOCK_SIZE;)
                    {
                        s = CodArr[offsetFlag + i];
                        if (s == 0)
                            break;
                        length=s;
                        type = gp_getUint16(offsetFlag +i+ length + 1); //?
                        if (type > 128)
                            type = 0;
                        /*read big endian */
                        UInt32 ofi = (UInt32)(offsetFlag +i+ length + 3);
                        value = gp_getUint32(ofi);

                        string svalue = value.ToString("X");
                        string str = gp_getNBytes2String(offsetFlag + i + 1, length);
                        string stype = SymbolType4[type];

                        DebugFile.AppendText(str + "=" + svalue + ",  type=" + stype + "\n");

                        i += (UInt16)(length + 7);

                    }
                }
            }
            else
            {
                DebugFile.AppendText("No long Symbol table info\n");
            }
           // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);

        }

        /*-------------------------------------------------------------------*/
        /*
         * Source files
         */
         void dump_source_files()
         {
             const UInt32 FILE_SIZE = 64;
             const UInt32 FILE_PER_BLOCK = COD_BLOCK_SIZE / FILE_SIZE;

             UInt16 i, j, start_block, end_block,sub_offset;
             UInt32 offsetFlag;
             UInt16 number_of_source_files=0;

             start_block = gp_getUint16(COD_DIR_NAMTAB);

             source_file_names.Clear();

             if(start_block !=0)
             {
                 end_block = gp_getUint16(COD_DIR_NAMTAB + 2);

                 DebugFile.AppendText("\nSource File Information\n");
                 DebugFile.AppendText("----------------------------------\n");
                 for(j=start_block;j<=end_block;j++)
                 {
                     offsetFlag = j * COD_BLOCK_SIZE;
                     for(i=0;i<FILE_PER_BLOCK;i++)
                     {
                         sub_offset =(UInt16) (i * FILE_SIZE);
                         string fileName = gp_getNBytes2String(offsetFlag + sub_offset + 1, FILE_SIZE-1);

                         if (fileName.EndsWith("\0"))
                         {
                             fileName = fileName.Substring(0, fileName.IndexOf("\0")); 
                         }
                         else
                         {
                             fileName = "INC.inc";
                         }                         
                         
                         if (CodArr[offsetFlag+sub_offset] !=0)
                         {
                             //source_file_names[number_of_source_files] = fileName;
                             source_file_names.Add(fileName);
                             if(source_file_names[number_of_source_files]==null)
                             {
                                 DebugFile.AppendText("system error 2\n");
                                 return;
                             }
                             DebugFile.AppendText(source_file_names[number_of_source_files]+"\n");
                             //
                             //
                             number_of_source_files++;
                             if (number_of_source_files>=100)
                             {
                                 DebugFile.AppendText("Too many files;increase MAX_SOURCE_FILES and recompile\n");
                                 return;
                             }
                         }
                     }
                 }
             }
             else
             {
                 DebugFile.AppendText("No source file info\n");
             }

            // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
         }


        private char[] smod_flags(UInt16 smod)
        {
            char[] f=new char[9];

            f[0] = (smod & 0x80)==0x80 ? 'C' : '.';
            f[1] = (smod & 0x40) == 0x40 ? 'F' : '.';
            f[2] = (smod & 0x20) == 0x20 ? 'I' : '.';
            f[3] = (smod & 0x10) == 0x10 ? 'D' : '.';
            f[4] = (smod & 0x08) == 0x08 ? 'C' : '.';
            f[5] = (smod & 0x04) == 0x04 ? 'L' : '.';
            f[6] = (smod & 0x02) == 0x02 ? 'N' : '.';
            f[7] = (smod & 0x01) == 0x01 ? 'A' : '.';
            f[8] = '\0';
            return f;
        }
        
        /************************************************
         * 
         * Line number info from the source files
         * 
         * *************************************************/
        private void demp_line_symbols()
        {
            int lst_line_number = 1;
            //int last_src_line = 0;

            UInt16 i, j, start_block, end_block;
            UInt32 offsetFlag;

           // LineSymbol ls = new LineSymbol();

            start_block = gp_getUint16(COD_DIR_LSTTAB);

            if(start_block!=0)
            {
                end_block = gp_getUint16(COD_DIR_LSTTAB + 2);
                DebugFile.AppendText("\n\nLine Number Information\n");
                DebugFile.AppendText("LstLn   SrcLn   Addr    Flags         FileName\n");
                DebugFile.AppendText("-----   -----   -----   -----------   ---------------------------------------------------------\n");
                for (j = start_block; j <= end_block; j++)
                {
                    UInt16 sline; //source line number
                    UInt16 sloc; // operation code
                    offsetFlag = (UInt32)(j * COD_BLOCK_SIZE);

                    for(i=0;i<COD_BLOCK_SIZE;)
                    {
                        
                        UInt32 offset_sfile = (UInt32)(offsetFlag + i);
                        UInt32 offset_smod = (UInt32)(offset_sfile+1);
                        UInt32 offset_sline = (UInt32)(offset_smod+1);
                        UInt32 offset_sloc = (UInt32)(offset_sline+2);


                        sline = gp_getUint16(offset_sline);
                        sloc = gp_getUint16(offset_sloc);

                        UInt16 sfile = CodArr[offset_sfile];
                        UInt16 smod = CodArr[offset_smod]; // gp_getUint16(offset_smod);

                        char[] smodFlag=smod_flags(smod);
                        string smodstr = new string(smodFlag).TrimEnd('\0');
                        if((smod & 4)==0)
                        {
                            //
                            DebugFile.AppendText((lst_line_number++).ToString("D5") + "   " + sline.ToString("D5")
                                + "   " + sloc.ToString("X4") + "   "+smod.ToString("X2")+" "+smodstr+"   "+source_file_names[sfile]+"\n");
                        }
                        i += 6;
                    }
                }
            }
           // File.WriteAllText(@"G:\Emulator\IDE\testCode\tryaaaa.txt", DebugFile.Text, Encoding.Default);
        }

        #region gpsystem funtion

        // get 64byte string
        private string gp_get64Bytes2String(UInt32 BetysOffset)
        {
            string str;
            int aa = (int)BetysOffset;
            str = System.Text.Encoding.ASCII.GetString(CodArr, aa, 64);

            return str;
        }

        //get N bytes string
        private string gp_getNBytes2String(UInt32 BetysOffset, UInt32 Length)
        {
            string str;
            int aa = (int)BetysOffset;
            int length = (int)Length;
            str = System.Text.Encoding.ASCII.GetString(CodArr, aa, length);

            return str;
        }

        private UInt32 gp_getUint32(UInt32 BetyOffset)
        {
            int value;
            value = CodArr[BetyOffset] << 24;
            value |= CodArr[BetyOffset + 1] << 16;
            value |= CodArr[BetyOffset + 2] << 8;
            value |= CodArr[BetyOffset + 3];

            return (UInt32)value;
        }

        private UInt16 gp_getUint16(UInt32 BetysOffset)
        {
            int value;

            value = CodArr[BetysOffset];
            value |= CodArr[BetysOffset + 1] << 8;

            return (UInt16)value;
        }

        private string gp_getStr16(UInt32 BetysOffset)
        {
            int value;

            value = CodArr[BetysOffset];
            value |= CodArr[BetysOffset + 1] << 8;

            return value.ToString("X4");
        }
        #endregion

    }
}
