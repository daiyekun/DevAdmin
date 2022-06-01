using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.Common.Utility
{
    /// <summary>
    /// String 帮助类
    /// </summary>
   public class StringHelper
    {
        /// <summary>
        /// 字符串转为int型数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sep">分割符</param>
        /// <param name="AllowSame">允许相同的内容</param>
        /// <returns></returns>
        public static IList<int> String2ArrayInt(string str, char sep = ',', bool AllowSame = false)
        {
            if (str == null || str.Trim() == "")
                return new List<int>();
            string[] arr = str.Split(sep);
            IList<int> lst = new List<int>();
            int intTemp;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() == "")
                    continue;
                if (!Int32.TryParse(arr[i], out intTemp))
                    continue;

                if (lst.Contains(intTemp))
                {
                    if (AllowSame == false)
                    {
                        continue;
                    }
                }
                lst.Add(intTemp);
            }
            return lst;
        }
        /// <summary>
        /// 字符串转为decimal型数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sep">分割符</param>
        /// <param name="AllowSame">允许相同的内容</param>
        /// <returns></returns>
        public static IList<decimal> String2ArrayDecimal(string str, char sep, bool AllowSame = false)
        {
            if (str == null || str.Trim() == "")
                return new List<decimal>();
            string[] arr = str.Split(sep);
            IList<decimal> lst = new List<decimal>();
            decimal intTemp;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() == "")
                    continue;
                if (!Decimal.TryParse(arr[i], out intTemp))
                    continue;

                if (lst.Contains(intTemp))
                {
                    if (AllowSame == false)
                    {
                        continue;
                    }
                }
                lst.Add(intTemp);
            }
            return lst;
        }
        /// <summary>
        /// 字符串转换为int列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IList<int> String2ArrayInt(string str)
        {
            return String2ArrayInt(str, ',');
        }
        /// <summary>
        /// int型数组转为字符串
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ArrayInt2String(IList<int> lst)
        {
            if (lst == null || lst.Count == 0)
                return "";
            string strRet = "";
            foreach (int value in lst)
            {
                if (strRet != "")
                {
                    strRet += ",";
                }
                strRet += value.ToString();
            }
            return strRet;
        }

        /// <summary>
        /// 字符串转为string型数组
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="sep">分隔符</param>
        /// <param name="IgnoreSame">忽略形同项</param>
        /// <param name="AllowEmpty">允许空字符串</param>
        /// <returns></returns>
        public static IList<string> Strint2ArrayString(string str, string sep, bool IgnoreSame = true, bool AllowEmpty = false)
        {
            string[] arr = str.Split(sep.ToCharArray());
            IList<string> lst = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                string strTemp = arr[i].Trim();
                if (!AllowEmpty)
                {
                    if (strTemp == "")
                        continue;
                }

                if (IgnoreSame)
                {
                    if (lst.Contains(strTemp))
                        continue;
                }

                lst.Add(strTemp);
            }
            return lst;
        }

        /// <summary>
        /// 字符串转为string型数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="IgnoreSame">忽略形同项</param>
        /// <param name="AllowEmpty">允许空字符串</param>
        /// <returns></returns>
        public static IList<string> Strint2ArrayString(string str, bool IgnoreSame = true, bool AllowEmpty = false)
        {
            return Strint2ArrayString(str, ",", IgnoreSame: IgnoreSame, AllowEmpty: AllowEmpty);
        }
        /// <summary>
        /// 字符串转为string型数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IList<string> Strint2ArrayString1(string str)
        {
            return Strint2ArrayString(str, " |,;");
        }
        /// <summary>
        /// string型数组转为字符串,用“,”分隔
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ArrayString2String(IList<string> lst)
        {
            string strRet = "";
            foreach (string value in lst)
            {
                if (strRet != "")
                {
                    strRet += ",";
                }
                strRet += value;
            }
            return strRet;
        }
        /// <summary>
        /// string型数组转为字符串,用“ ”分隔
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ArrayString2String1(IList<string> lst)
        {
            string strRet = "";
            foreach (string value in lst)
            {
                if (strRet != "")
                {
                    strRet += " ";
                }
                strRet += value;
            }
            return strRet;
        }
        /// <summary>
        /// string型数组转为字符串,用“|”分隔
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string ArrayString2String2(IList<string> lst)
        {
            string strRet = "";
            foreach (string value in lst)
            {
                if (strRet != "")
                {
                    strRet += "|";
                }
                strRet += value;
            }
            return strRet.TrimEnd('|');
        }
        /// <summary>
        /// 得到单字节的字符长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetAscLen(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if ((int)ch <= 255)
                {
                    num++;
                }
                else
                {
                    num += 2;
                }
            }
            return num;
        }
        /// <summary>
        /// 根据单字节的字符长度截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetAscSubstring(string str, int length)
        {
            int num = 0;
            string strRet = "";
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if ((int)ch <= 255)
                {
                    num++;
                }
                else
                {
                    num += 2;
                }
                if (num > length)
                    break;

                strRet += ch;
            }
            return strRet;
        }
        /// <summary>
        /// 转换到int型的值
        /// 默认返回null   
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ConvertToIntNull(object obj)
        {
            if (obj == null)
                return null;

            int intTemp = -1;
            if (Int32.TryParse(obj.ToString(), out intTemp))
            {
                return intTemp;
            }
            return null;
        }
        /// <summary>
        /// 转换到byte型的值
        /// 默认返回null   
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte? ConvertToByteNull(object obj)
        {
            if (obj == null)
                return null;

            byte Temp;
            if (byte.TryParse(obj.ToString(), out Temp))
            {
                return Temp;
            }
            return null;
        }
        /// <summary>
        ///  转换到decimal 型的值
        ///  默认返回null   
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ConvertToDecimalNull(object obj)
        {
            if (obj == null)
                return null;

            decimal temp;
            if (decimal.TryParse(obj.ToString(), out temp))
            {
                return temp;
            }
            return null;
        }
        /// <summary>
        /// 转换到bool 型的值
        ///  默认返回null  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? ConvertToBooleanNull(object obj)
        {
            if (obj == null)
                return null;

            bool temp;
            if (bool.TryParse(obj.ToString(), out temp))
            {
                return temp;
            }
            return null;
        }
        /// <summary>
        /// 转换到DateTime 型的值
        ///  默认返回null  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ConverToDateTimeNull(object obj)
        {
            if (obj == null)
                return null;

            DateTime temp;
            if (DateTime.TryParse(obj.ToString(), out temp))
            {
                return temp;
            }
            return null;
        }

        public static string RepleaceStr(string stringData)
        {
            return stringData.Replace("Checked", "checked");
        }

        /// <summary>
        /// 字符串生成驼峰命名规则
        /// 将DEV_USER_ROLE=>DevUserRole
        /// </summary>
        /// <param name="str">原始字符串 DEV_USER_ROLE</param>
        /// <returns>返回 类似 DevUserRole</returns>
        public static string ToStrHump(string str)
        {
            var str2 = str.ToLower();//先全部转小写
            //var str3 = str.ToLowerInvariant();//先全部转小写
            StringBuilder strb = new StringBuilder();

            var arr = str2.Split('_');//先用字符串分割成数组
            for (var i = 0; i < arr.Length; i++)
            {
                var tstr = arr[i];

                strb.Append(tstr.Remove(1).ToUpper() + tstr.Substring(1));
            }

            return strb.ToString();
        }
    }
}
