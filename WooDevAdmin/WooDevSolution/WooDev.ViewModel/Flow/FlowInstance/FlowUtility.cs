using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;
using WooDev.ViewModel.Enums;

namespace WooDev.ViewModel.Flow.FlowInstance
{

    /// <summary>
    /// 审批流程工具
    /// </summary>
    public class FlowUtility
    {
        /// <summary>
        /// 获取对象名称
        /// </summary>
        /// <param name="objType">操作对象</param>
        /// <param name="objId">操作ID</param>
        /// <returns></returns>
        public static string GetObjName(int objType, int objId)
        {
            switch (objType)
            {
                case (int)OptTypeEnum.UserRes:
                    return DevRedisUtility.GetUserField(objId);

                case (int)OptTypeEnum.FlowGroup:
                    return DevRedisUtility.GetGroupField(objId);
                default:
                    return "";
            }

        }
    }
}
