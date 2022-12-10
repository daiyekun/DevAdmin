using Google.Protobuf.WellKnownTypes;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel.Flow.FlowInstPdf;

namespace WooDev.Services
{

    /// <summary>
    /// 审批实例生成pdf
    /// </summary>
    public partial class FlowInstPdfService:BaseService<DEV_FLOW_INSTANCE>, IFlowInstPdfService
    {
        public FlowInstPdfService(ISqlSugarClient DbClient)
          : base(DbClient)
        { }
        #region 审批意见
        /// <summary>
        /// 审批意见
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>意见信息字典</returns>
        public Dictionary<string, List<WfOption>> GetWfOptions(DEV_FLOW_INSTANCE appInst)
        {

            var listdata=DbClient.Queryable<DEV_FLOW_INST_OPTION, DEV_FLOW_INST_NODE>((o, i) => new JoinQueryInfos(
                JoinType.Left, o.NODE_STR_ID == i.NODE_STRID //左连接 左链接 左联 

            )).Where(a => a.INST_ID == appInst.ID)
            .Select((o, i) => 
            new OptionView 
            {
                Id = o.ID,
                NodeName = i.TEXT_VALUE,
                CreateDatetime = o.CREATE_TIME,
                CreateUserId = o.CREATE_USERID,
                Opinion = o.APP_OPTION,
            }
            ).ToList();

            var local = from a in listdata
                        select new WfOption
                        {
                            NodeStrId = a.NodeStrId,
                            NodeName = a.NodeName,
                            AppUserName = DevRedisUtility.GetUserField(a.CreateUserId),
                            CreateUserId = a.CreateUserId,
                            Option = a.Opinion,
                            AppDate = a.CreateDatetime,
                            ImgSrc = "" //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "UserEs", a.CreateUserId + ".PNG")
                        };

            return local.ToList().GroupBy(a => a.NodeName).ToDictionary(g => g.Key, g => g.ToList());

        }
        #endregion


        #region 合同对方
        /// <summary>
        /// 合同对方
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public CompanyInfo GetCommpanyFlowPdfData(DEV_FLOW_INSTANCE appInst)
            {
                CompanyInfo companyInfo = GetCompanyInfo(appInst.APP_ID);
                companyInfo.DicWfData = GetWfOptions(appInst);
                return companyInfo;

            }
            /// <summary>
            /// 获取合同对方信息
            /// </summary>
            /// <param name="Id">当前ID</param>
            /// <returns>合同对方相关信息</returns>
           private  CompanyInfo GetCompanyInfo(int compId)
            {
                var query = from a in DbClient.Queryable<DEV_COMPANY>()
                            where a.ID == compId
                            select new
                            {
                                Id = a.ID,
                                Name = a.NAME,
                                Code = a.CODE,
                                LeadUserId = a.LEAD_USERID,
                                CreateDateTime = a.CREATE_TIME,
                                CreateUserId = a.CREATE_USERID,
                                Trade = a.TRADE,//行业
                                CateId = a.CATE_ID,//类别
                                Ctype = a.C_TYPE,

                            };
                var list = query.ToList();
                var local = from a in list
                            select new CompanyInfo
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                CateName = DevRedisUtility.GetDataField(a.CateId),
                                LeadUser = DevRedisUtility.GetUserField(a.LeadUserId??0),
                                Trade = a.Trade,
                                CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                                CreateUserName = DevRedisUtility.GetUserField(a.CreateUserId)

                            };
                return local.FirstOrDefault();

            }





            #endregion


        

    }
}
