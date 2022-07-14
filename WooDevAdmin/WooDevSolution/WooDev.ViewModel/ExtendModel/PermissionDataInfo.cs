using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.ExtendModel
{
    /// <summary>
    /// 操作权限实体类
    /// </summary>
   public class PermissionDataInfo
    {
        /// <summary>
        /// 状态码0：有权限操作，1无权限操作，3：部分有权限,4部分状态不允许
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 权限消息
        /// </summary>
        public string Msg { get; set; } = string.Empty;
        /// <summary>
        /// 允许操作的数据ID集合
        /// </summary>
        public IList<int> OptionIds = new List<int>();
        /// <summary>
        /// 不允许操作的对象描述集合（名称、编号....）
        /// </summary>
        public IList<string> noteAllow = new List<string>();
        /// <summary>
        /// 获取权限描述
        /// </summary>
        /// <param name="code">权限状态码</param>
        /// <returns></returns>
        public string GetOptionMsg(int code)
        {
            switch (code)
            {
                case 0:
                    return "有权限";

                case 1:
                    return "无权限";
                case 3:
                    return "部分数据无权限";
                case 4:
                    return "部分数据状态无权限操作";
                default:
                    return "未知数据状态码:" + code;

            }

        }


    }
    /// <summary>
    /// 按钮权限类
    /// </summary>
    public class BtnPremissionInfo
    {
        /// <summary>
        /// 修改按钮
        /// </summary>
        public int Update { get; set; } = 0;
        /// <summary>
        /// 删除按钮
        /// </summary>
        public int Delete { get; set; } = 0;
        /// <summary>
        /// 变更按钮
        /// </summary>
        public int Change { get; set; } = 0;

    }

    
}
