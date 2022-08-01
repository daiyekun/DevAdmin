using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WooDev.Common.Utility
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
   public class EmunUtility
    {

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>return list<int></returns>
        public static IList<int> GetValues(Type enumType)
        {
            Attribute[] atts = Attribute.GetCustomAttributes(enumType, typeof(EnumClassAttribute));
            if (atts.Length<1)
            {
                throw new Exception("没有特性EnumClassAttribute表示");
            }
            EnumClassAttribute classAtt = (EnumClassAttribute)atts[0];
            IList<int> list = new List<int>();
            foreach (int item in Enum.GetValues(enumType))
            {
                if (item >= classAtt.Min && item <= classAtt.Max)
                {
                    list.Add(item);
                }

            }
            return list;
        }
       /// <summary>
       /// 得到自定义类型集合
       /// </summary>
       /// <param name="enumType">枚举类型</param>
       /// <returns></returns>
        public static IList<EnumItemAttribute> GetAttr(Type enumType) {
            IList<EnumItemAttribute> lst = new List<EnumItemAttribute>();
            EnumClassAttribute classAtt = GetClassAttribute(enumType);
            MemberInfo[] members = enumType.GetMembers();
            foreach (MemberInfo item in members)
            {
                EnumItemAttribute itemAttr = (EnumItemAttribute)Attribute.GetCustomAttribute(item,typeof(EnumItemAttribute));
                if (itemAttr == null)
                    continue;
                lst.Add(itemAttr);
            }
            return lst;
        }
        /// <summary>
        /// 得到特性属性
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static EnumClassAttribute GetClassAttribute(Type enumType) {
            EnumClassAttribute classAtt = (EnumClassAttribute)Attribute.GetCustomAttribute(enumType, typeof(EnumClassAttribute));
            if (classAtt == null)
                throw new Exception("没有找到EnumClassAttribute特性");
            return classAtt;


        }

        
       /// <summary>
        /// 得到描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDesc(Type enumType, int value)
        {
            var clsAttr = GetClassAttribute(enumType);
            MemberInfo[] members = enumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;
                if (itemAtt.Value == value)
                {
                    
                    return itemAtt.Desc;
                }
            }
            return "";
        }
        /// <summary>
        /// 根据枚举描述得到枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static int GetValue(Type enumType, string desc)
        {
            var clsAttr = GetClassAttribute(enumType);
            MemberInfo[] members = enumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;
                if (itemAtt.Desc == desc)
                {

                    return itemAtt.Value;
                }
            }
            return -1;
        }
        /// <summary>
        /// 得到默认描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetDefaultDesc(Type enumType)
        {
            return GetDesc(enumType, GetDefaultValue(enumType));
        }
        /// <summary>
        /// 得到默认值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int GetDefaultValue(Type enumType)
        {
            EnumClassAttribute classAtt = GetClassAttribute(enumType);
            if (classAtt.HasDefault)
            {
                return classAtt.Default;
            }
            return classAtt.Min;
        }
        /// <summary>
        /// 得到枚举项上的自定义属性
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static EnumItemAttribute GetEnumItemAttribute(Type EnumType, int Value)
        {
            foreach (var item in EmunUtility.GetEnumerator(EnumType))
            {
                if (item.Value == Value)
                    return item;
            }
            return null;
        }
        /// <summary>
        /// 得到枚举项上的自定义属性
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static EnumItemExAttribute GetEnumItemExAttribute(Type EnumType, int Value)
        {
            foreach (var item in EmunUtility.GetEnumeratorEx(EnumType))
            {
                if (item.Value == Value)
                    return (EnumItemExAttribute)item;
            }
            return null;
        }
        /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumItemAttribute> GetEnumerator(Type EnumType)
        {
            //EnumClassAttribute classAtt = GetClassAttribute(EnumType);

            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;

                yield return itemAtt;
            }
        }
        /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumItemExAttribute> GetEnumeratorEx(Type EnumType)
        {
            //EnumClassAttribute classAtt = GetClassAttribute(EnumType);

            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemExAttribute itemAtt = (EnumItemExAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemExAttribute));
                if (itemAtt == null)
                    continue;

                yield return itemAtt;
            }
        }



    }
    /// <summary>
    /// 枚举属性
    /// </summary>
    public class EnumClassAttribute : Attribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 空值
        /// </summary>
        public int None { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public int Default { get; set; }
        /// <summary>
        /// 是否有默认值
        /// </summary>
        public bool HasDefault {
            get {
                return Default >= Min && Default <= Max;
            }
        }
    }
    /// <summary>
    /// 枚举项特性属性
    /// </summary>
    public class EnumItemAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string? Desc { get; set; }
       /// <summary>
       /// 值
       /// </summary>
        public int Value { get; set; }
    }
  
    /// <summary>
    /// 枚举项扩展特性属性
    /// </summary>
    public class EnumItemExAttribute : EnumItemAttribute
    {
        /// <summary>
        /// 字符串值
        /// </summary>
        public string StringValue { get; set; }
        /// <summary>
        /// 整数值
        /// </summary>
        public int IntValue { get; set; }
        /// <summary>
        /// 字符串数组
        /// </summary>
        public string[] ArrayString { get; set; }
        /// <summary>
        /// byte
        /// </summary>
        public byte ByteValue { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type TypeValue { get; set; }
    }
}
