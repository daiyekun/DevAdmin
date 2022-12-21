using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.ViewModel.ExtendModel;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 合同列表
    /// </summary>
    public partial class DevContractList: IDevEntityHandle
    {
        /// <summary>
        /// 审批事项
        /// </summary>
        public string WfFlowDic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public string WfState { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string LeadUserName { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 合同对方名称
        /// </summary>
        public string ComName { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ANT_MONERYThod { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public string MainDeptName { get; set; }
        /// <summary>
        /// 合同属性
        /// </summary>
        public string ContPro { get; set; }

        /// <summary>
        /// 导出excel使用
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();
            var obj = this.GetType().GetProperty(propName);
            fieldinfo.FileType = obj.PropertyType;
            fieldinfo.FileValue = obj.GetValue(this, null);
            return fieldinfo;
        }
    }
}
