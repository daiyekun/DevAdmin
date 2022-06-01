using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDeV.UnitTest
{
    public class StringUnitTest
    {

        //将字符串转小写，有下划线的去除下划线，并将下划线后的首字母转大写（驼峰）
        public string ToLowerUp(string str)
        {
            var  str2 = str.ToLower();//先全部转小写
            var str3 = str.ToLowerInvariant();//先全部转小写
            StringBuilder strb = new StringBuilder();
          
            var arr = str2.Split('_');//先用字符串分割成数组
            for (var i=0;i< arr.Length;i++ )
            {
                var tstr = arr[i];

                strb.Append(tstr.Remove(1).ToUpper() + tstr.Substring(1));
            }
           
            return strb.ToString();
        }
       
    }
}
