using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.Flow.FlowInstance;

namespace WooDev.Services
{
    /// <summary>
    /// 审批实例
    /// </summary>
    public partial class DevFlowInstanceService
    {
        /// <summary>
        /// 根据条件查询模板
        /// </summary>
        /// <param name="appConditions">流程模板条件</param>
        /// <returns></returns>
        public DEV_FLOW_TEMP GetTemp(AppConditionsInfo appConditions)
        {
            var tempquery = DbClient.Queryable<DEV_FLOW_TEMP>()
                .Where(a => a.OBJ_TYPE == appConditions.FlowObj
                        && (',' + a.CATE_IDS + ',').Contains(',' + appConditions.CateId.ToString() + ',')
                        && (',' + a.FLOW_ITEMS + ',').Contains(',' + appConditions.FlowItem.ToString() + ',')
                        && (',' + a.DEPART_IDS + ',').Contains(',' + appConditions.DeptId.ToString() + ',')

                );
            switch (appConditions.FlowObj)
            {
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    return tempquery.Where(a=>a.MIN_MONERY>= appConditions.Monery&& appConditions.Monery<=a.MAX_MONERY).Single();
                default:
                    return tempquery.Single();
                    

            }
        }
    }
}
