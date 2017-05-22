using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace COD
{
    public class TMap
    {
        /// <summary>
        /// c源名称
        /// </summary>
        public string fileName_sourceC = "";

        /// <summary>
        /// 函数名称
        /// </summary>
        public string funName = "";

        /// <summary>
        /// 变量地址
        /// </summary>
        public int address = 0;
        /// <summary>
        /// 变量名称
        /// </summary>
        public string varName = "";

        /// <summary>
        /// 变量原始名称
        /// </summary>
        public string varoldName = "";

        /// <summary>
        /// 是否是list文件添加标记
        /// </summary>
        public bool isLstFlag = false;

        /// <summary>
        /// 是否是全局变量
        /// </summary>
        public bool isGloab = false;

        public int linestarNo = 0;
        public int lineEndNo = 0;

        public int datares = 0;

        public TMap()
        {

        }

        public static TMap NewMap(string tempstr)
        {
            TMap tempMap = new TMap();
            if (tempstr.Length < 68) return null;
            string[] temp_fun_str = new string[5];
            temp_fun_str[0] = tempstr.Substring(0, 25).Trim();
            temp_fun_str[1] = tempstr.Substring(25, 11).Trim();
            temp_fun_str[2] = tempstr.Substring(36, 11).Trim();
            temp_fun_str[3] = tempstr.Substring(47, 11).Trim();
            temp_fun_str[4] = tempstr.Substring(58, tempstr.Length - 59).Trim();

            if (temp_fun_str.Length == 5)
            {
                if (temp_fun_str[2] != "data")
                    return null;
                tempMap.fileName_sourceC = temp_fun_str[4].Replace(".asm", ".c");
                try
                {
                    tempMap.address = Convert.ToInt32(temp_fun_str[1].Trim(), 16);
                }
                catch { }
                tempMap.varName = temp_fun_str[0];
                if (temp_fun_str[3] == "extern")
                {
                    tempMap.isGloab = true;
                    if (temp_fun_str[0][0] == '_')
                        tempMap.varoldName = temp_fun_str[0].Substring(1, temp_fun_str[0].Length - 1);
                    else
                        tempMap.varoldName = temp_fun_str[0];
                }
                else if (temp_fun_str[3] == "static")
                {
                    tempMap.varoldName = temp_fun_str[0];
                    tempMap.isGloab = false;
                }
                else
                    return null;

                return tempMap;
            }
            else
                return tempMap;
        }
    }

    public static class TMaps
    {
        static List<TMap> myMap = new List<TMap>();
        public static void LoadMap(string filename)
        {
            myMap.Clear();
            FileStream textLoad = new FileStream(filename, FileMode.Open, FileAccess.Read);

            Encoding encoding = Encoding.Unicode;
            encoding = FastColoredTextBoxNS.TxtFileEncoding.GetEncoding(filename);
            string list_tempfile = FastColoredTextBoxNS.FileReader.ReadFileContent(textLoad, ref encoding);
            textLoad.Close();

            list_tempfile = trimTop(list_tempfile);
            string[] temp_line_str = list_tempfile.Split('\n');
            for (int i = 0; i < temp_line_str.Length; i++)
            {
                TMap tmap = TMap.NewMap(temp_line_str[i]);
                if (tmap != null)
                    myMap.Add(tmap);
            }
        }

        public static void addMap(string funname, string oldname, string varname)
        {
            TMap tempmap = new TMap();
            tempmap.funName = funname;
            tempmap.varoldName = oldname;
            tempmap.varName = varname;
            tempmap.isLstFlag = true;
            myMap.Add(tempmap);
        }


        /// <summary>
        /// 踢掉map文件内容头部分，直到Symbols分割线
        /// </summary>
        /// <param name="tempstr"></param>
        /// <returns></returns>
        private static string trimTop(string tempstr)
        {
            string resultstr = "";
            int indexend = -1;
            indexend = tempstr.LastIndexOf("---------");
            if (indexend >= 0)
            {
                indexend += 11;
                resultstr = tempstr.Substring(indexend, tempstr.Length - indexend);
            }

            return resultstr;
        }

        /// <summary>
        /// 得到变量的物理地址，暂时没有用 varname的格式为 '0rx1001'
        /// filename格式为****.c
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private static string GetAddress(string varName, string filename)
        {
            for (int i = 0; i < myMap.Count; i++)
            {
                if (myMap[i].varName == varName && myMap[i].fileName_sourceC == filename)
                {
                    return myMap[i].address.ToString("X2");
                }
            }

            return null;
        }

        /// <summary>
        /// 得到变量的物理地址，暂时没有用 varname的格式为 '0rx1001'
        /// funcName格式为****.c
        /// </summary>
        public static bool setLineParam(string funcName, string filename, int beginNo, int endNo)
        {
            for (int i = 0; i < myMap.Count; i++)
            {
                if (myMap[i].funName == funcName)
                {
                    if ((myMap[i].isGloab == false) && (myMap[i].isLstFlag))
                    {
                        myMap[i].linestarNo = beginNo;
                        myMap[i].lineEndNo = endNo;
                        myMap[i].fileName_sourceC = filename;
                    }
                }
            }
            return true;
        }

        public static void updateAddress()
        {
            for (int i = 0; i < myMap.Count; i++)
            {
                if (myMap[i].isLstFlag)
                {
                    for (int j = 0; j < myMap.Count; j++)
                    {
                        if ((myMap[i].fileName_sourceC == myMap[j].fileName_sourceC)
                            && myMap[i].varName == myMap[j].varName
                            && (i != j) && (myMap[i].varName != "")
                            )
                        {
                            myMap[i].address = myMap[j].address;
                            break;
                        }
                    }
                }
            }

            ClearTemp();
        }

        private static void ClearTemp()
        {
            for (int i = 0; i < myMap.Count; i++)
            {
                if (myMap[i].isGloab && myMap[i].isLstFlag)
                {
                    myMap.Remove(myMap[i]);
                    i--;
                }
            }
        }


        public static bool setVarOldName(string varName, string funcName, string oldname)
        {
            for (int i = 0; i < myMap.Count; i++)
            {
                if (myMap[i].varName == varName)
                {
                    if ((myMap[i].funName == "") && (!myMap[i].isGloab))
                    {
                        myMap[i].funName = funcName;
                        myMap[i].varoldName = oldname;
                        return true;
                    }
                }
            }

            return false;
        }


        public static bool GetVarName(int OldIndex, out string strVarName, out int Index)
        {
            for (int i = OldIndex; i < myMap.Count; i++)
            {
                if (!(myMap[i].isLstFlag || myMap[i].isGloab)) continue;
                strVarName = myMap[i].varName;
                Index = i;
                return true;
            }
            strVarName = null;
            Index = 0;
            return false;
        }

        public static void setDataRes(int index,int iDataRes)
        {
            myMap[index].datares = iDataRes;
        }



        /// <summary>
        /// 讲map文件转换为.var后缀名文件
        /// </summary>
        /// <param name="filaname"></param>
        /// <returns></returns>
        public static bool SaveToVarFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                    File.Delete(filename);
                string tempstr = "";
                for (int i = 0; i < myMap.Count; i++)
                {
                    if (!(myMap[i].isLstFlag || myMap[i].isGloab)) continue;
                    tempstr = tempstr + myMap[i].varoldName + " at 0x" + myMap[i].address.ToString("X4") + " " + myMap[i].datares.ToString("d4") + " ";

                    if (myMap[i].isGloab)
                        tempstr += "xxxx.x";
                    else
                        tempstr += myMap[i].fileName_sourceC;

                    tempstr += " " + myMap[i].linestarNo.ToString("d4") + " to " + myMap[i].lineEndNo.ToString("d4") + "\r\n";
                }

                System.IO.File.AppendAllText(filename, tempstr);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
