// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Mo" email="gfmo@dzmi.com"/>
//     <version>$Revision: 2682 $</version>
// </file>

using System;
using System.IO;
using System.Text;

namespace FastColoredTextBoxNS
{
	/// <summary>
	/// Class that can open text files with auto-detection of the encoding.
	/// </summary>
	public static class FileReader
	{
		public static bool IsUnicode(Encoding encoding)
		{
			int codepage = encoding.CodePage;
			// return true if codepage is any UTF codepage
			return codepage == 65001 || codepage == 65000 || codepage == 1200 || codepage == 1201;
		}
		
		public static string ReadFileContent(Stream fs, ref Encoding encoding)
		{
			using (StreamReader reader = OpenStream(fs, encoding)) {
				reader.Peek();
				encoding = reader.CurrentEncoding;
				return reader.ReadToEnd();
			}
		}
		
		public static string ReadFileContent(string fileName, Encoding encoding)
		{
			using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				return ReadFileContent(fs, ref encoding);
			}
		}
		
		public static StreamReader OpenStream(Stream fs, Encoding defaultEncoding)
		{
			if (fs == null)
				throw new ArgumentNullException("fs");
			
			if (fs.Length >= 2) {
				// the autodetection of StreamReader is not capable of detecting the difference
				// between ISO-8859-1 and UTF-8 without BOM.
				int firstByte = fs.ReadByte();
				int secondByte = fs.ReadByte();
				switch ((firstByte << 8) | secondByte) {
					case 0x0000: // either UTF-32 Big Endian or a binary file; use StreamReader
					case 0xfffe: // Unicode BOM (UTF-16 LE or UTF-32 LE)
					case 0xfeff: // UTF-16 BE BOM
					case 0xefbb: // start of UTF-8 BOM
						// StreamReader autodetection works
						fs.Position = 0;
						return new StreamReader(fs);
					default:
						return AutoDetect(fs, (byte)firstByte, (byte)secondByte, defaultEncoding);
				}
			} else {
				if (defaultEncoding != null) {
					return new StreamReader(fs, defaultEncoding);
				} else {
					return new StreamReader(fs);
				}
			}
		}
		
		static StreamReader AutoDetect(Stream fs, byte firstByte, byte secondByte, Encoding defaultEncoding)
		{
			int max = (int)Math.Min(fs.Length, 500000); // look at max. 500 KB
			const int ASCII = 0;
			const int Error = 1;
			const int UTF8  = 2;
			const int UTF8Sequence = 3;
			int state = ASCII;
			int sequenceLength = 0;
			byte b;
			for (int i = 0; i < max; i++) {
				if (i == 0) {
					b = firstByte;
				} else if (i == 1) {
					b = secondByte;
				} else {
					b = (byte)fs.ReadByte();
				}
				if (b < 0x80) {
					// normal ASCII character
					if (state == UTF8Sequence) {
						state = Error;
						break;
					}
				} else if (b < 0xc0) {
					// 10xxxxxx : continues UTF8 byte sequence
					if (state == UTF8Sequence) {
						--sequenceLength;
						if (sequenceLength < 0) {
							state = Error;
							break;
						} else if (sequenceLength == 0) {
							state = UTF8;
						}
					} else {
						state = Error;
						break;
					}
				} else if (b >= 0xc2 && b < 0xf5) {
					// beginning of byte sequence
					if (state == UTF8 || state == ASCII) {
						state = UTF8Sequence;
						if (b < 0xe0) {
							sequenceLength = 1; // one more byte following
						} else if (b < 0xf0) {
							sequenceLength = 2; // two more bytes following
						} else {
							sequenceLength = 3; // three more bytes following
						}
					} else {
						state = Error;
						break;
					}
				} else {
					// 0xc0, 0xc1, 0xf5 to 0xff are invalid in UTF-8 (see RFC 3629)
					state = Error;
					break;
				}
			}
			fs.Position = 0;
			switch (state) {
				case ASCII:
				case Error:
					// when the file seems to be ASCII or non-UTF8,
					// we read it using the user-specified encoding so it is saved again
					// using that encoding.
					if (IsUnicode(defaultEncoding)) {
						// the file is not Unicode, so don't read it using Unicode even if the
						// user has choosen Unicode as the default encoding.
						
						// If we don't do this, SD will end up always adding a Byte Order Mark
						// to ASCII files.
						defaultEncoding = Encoding.Default; // use system encoding instead
					}
					return new StreamReader(fs, defaultEncoding);
				default:
					return new StreamReader(fs);
			}
		}
	}


    /// <summary>
    ///  直接使用类的静态函数TxtFileEncoding.GetEncoding(string fileName)
    /// 用于取得一个文本文件的编码方式(Encoding)。
    /// </summary>
    public class TxtFileEncoding
    {
        /// <summary>
        /// 构造
        /// </summary>
        public TxtFileEncoding()
        {

        }

        /// <summary>
        /// 取得一个文本文件的编码方式。如果无法在文件头部找到有效的前导符，Encoding.Default将被返回。
        /// </summary>
        /// <param name="fileName">文件名。</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string fileName)
        {
            return GetEncoding(fileName, Encoding.Default);
        }
        /// <summary>
        /// 取得一个文本文件流的编码方式。
        /// </summary>
        /// <param name="stream">文本文件流。</param>
        /// <returns></returns>

        public static Encoding GetEncoding(FileStream stream)
        {
            return GetEncoding(stream, Encoding.Default);
        }
        /// <summary>
        /// 取得一个文本文件的编码方式。
        /// </summary>
        /// <param name="fileName">文件名。</param>
        /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string fileName, Encoding defaultEncoding)
        {
            FileStream fs = null;
            Encoding targetEncoding = defaultEncoding;
            try
            {
                fs = new FileStream(fileName, FileMode.Open);
                targetEncoding = GetEncoding(fs, defaultEncoding);
            }
            catch
            {
            }
            if (fs != null)
            {
                fs.Close();
            }
            return targetEncoding;
        }

        /// <summary>
        /// 取得一个文本文件流的编码方式。
        /// </summary>
        /// <param name="stream">文本文件流。</param>
        /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
        /// <returns></returns>
        public static Encoding GetEncoding(FileStream stream, Encoding defaultEncoding)
        {
            Encoding targetEncoding = defaultEncoding;
            if (stream != null && stream.Length >= 2)
            {
                //保存文件流的前4个字节
                byte byte1 = 0;
                byte byte2 = 0;
                byte byte3 = 0;
                byte byte4 = 0;
                //保存当前Seek位置
                long origPos = stream.Seek(0, SeekOrigin.Begin);
                stream.Seek(0, SeekOrigin.Begin);
                int nByte = stream.ReadByte();
                byte1 = Convert.ToByte(nByte);
                byte2 = Convert.ToByte(stream.ReadByte());
                if (stream.Length >= 3)
                {
                    byte3 = Convert.ToByte(stream.ReadByte());
                }
                if (stream.Length >= 4)
                {
                    byte4 = Convert.ToByte(stream.ReadByte());
                }
                //根据文件流的前4个字节判断Encoding
                //Unicode {0xFF, 0xFE};
                //BE-Unicode {0xFE, 0xFF};
                //UTF8 = {0xEF, 0xBB, 0xBF};
                if (byte1 == 0xFE && byte2 == 0xFF)//UnicodeBe
                {
                    targetEncoding = Encoding.BigEndianUnicode;
                }
                else if (byte1 == 0xFF && byte2 == 0xFE && byte3 != 0xFF)//Unicode
                {
                    targetEncoding = Encoding.Unicode;
                }
                else if (byte1 == 0xEF && byte2 == 0xBB && byte3 == 0xBF)//UTF8
                {
                    targetEncoding = Encoding.UTF8;
                }
                else if (byte2 == 0x0)//Unicode
                {
                    targetEncoding = Encoding.Unicode;
                }
                //恢复Seek位置　　　  
                stream.Seek(origPos, SeekOrigin.Begin);
            }
            return targetEncoding;
        }
    }
    

}
