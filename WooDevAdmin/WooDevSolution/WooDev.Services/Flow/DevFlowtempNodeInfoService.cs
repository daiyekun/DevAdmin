using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.Model.Models;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel;
using WooDev.ViewModel.Flow;

namespace WooDev.Services
{

    /// <summary>
    /// 节点信息
    /// </summary>
    public partial class DevFlowtempNodeInfoService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where 条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public ResultPageData<DevFlowTempNodeInfoList> GetList(PageInfo<DEV_FLOWTEMP_NODE_INFO> pageInfo, Expression<Func<DEV_FLOWTEMP_NODE_INFO, bool>>? whereLambda,
            Expression<Func<DEV_FLOWTEMP_NODE_INFO, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_FLOWTEMP_NODE_INFO>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_FLOWTEMP_NODE_INFO>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = tempquery.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount, a => new {
                ID = a.ID,
                OPT_ID=a.OPT_ID,
                OPT_NAME=a.OPT_NAME,
                NODE_ID= a.NODE_ID,
                NODE_STRID=a.NODE_STRID,
                O_TYPE = a.O_TYPE,
                RE_TEXT=a.RE_TEXT,
                NRULE=a.NRULE,
                INFO_STATE=a.INFO_STATE,

            });
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevFlowTempNodeInfoList
                        {
                            ID = a.ID,
                            OPT_ID = a.OPT_ID,
                            OPT_NAME = a.OPT_NAME,
                            NODE_ID = a.NODE_ID,
                            NODE_STRID = a.NODE_STRID,
                            O_TYPE=a.O_TYPE,
                            RE_TEXT = a.RE_TEXT,
                            NRULE = a.NRULE,
                            INFO_STATE = a.INFO_STATE,
                        };
            return new ResultPageData<DevFlowTempNodeInfoList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
