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
using WooDev.ViewModel;

namespace WooDev.Services
{

    /// <summary>
    /// 对方联系人
    /// </summary>
    public partial class DevCompContactsService
    {

        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public ResultPageData<DevCompContactsList> GetList(PageInfo<DEV_COMP_CONTACTS> pageInfo, Expression<Func<DEV_COMP_CONTACTS, bool>> whereLambda,
             Expression<Func<DEV_COMP_CONTACTS, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_COMP_CONTACTS>().Where(whereLambda);
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Asc);
            }
            else
            {
                tempquery = tempquery.OrderBy(orderbyLambda, OrderByType.Desc);
            }

            var query = from a in tempquery
                        select new
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            DEPART=a.DEPART,//所属部门
                            COMP_ID=a.COMP_ID,//合同对方ID
                            EMAIL=a.EMAIL,//E-mail
                            POSITION=a.POSITION,//职位
                            CREATE_TIME=a.CREATE_TIME,
                            TEL=a.TEL,//电话
                            PHONE= a.PHONE,//手机
                            REMARK=a.REMARK,//备注
                            CREATE_USERID = a.CREATE_USERID,//创建人
                           


                        };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_COMP_CONTACTS>))
            { //分页
                pageInfo.PageSize = 2000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevCompContactsList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            DEPART = a.DEPART,//所属部门
                            COMP_ID = a.COMP_ID,//合同对方ID
                            EMAIL = a.EMAIL,//E-mail
                            POSITION = a.POSITION,//职位
                            CREATE_TIME = a.CREATE_TIME,
                            TEL = a.TEL,//电话
                            PHONE = a.PHONE,//手机
                            REMARK = a.REMARK,//备注
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                           
                        };
            return new ResultPageData<DevCompContactsList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }
    }
}
