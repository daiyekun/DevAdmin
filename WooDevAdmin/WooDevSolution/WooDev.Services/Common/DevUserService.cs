using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Enums;

namespace WooDev.Services
{

    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class DevUserService
    {
       

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevUserList> GetList<s>(PageInfo<DEV_USER> pageInfo, Expression<Func<DEV_USER, bool>> whereLambda,
             Expression<Func<DEV_USER, object>> orderbyLambda, bool isAsc)
        {
            
            var tempquery = DbClient.Queryable<DEV_USER>().Where(whereLambda);
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
                            LOGIN_NAME = a.LOGIN_NAME,//登录名称
                            SEX = a.SEX,//性别
                            TEL = a.TEL,//电话
                            ENTRY_TIME = a.ENTRY_TIME,//入职日期
                            EMAIL = a.EMAIL,//Email
                            ID_NO = a.ID_NO,//移动电话
                            CODE = a.CODE,//编号
                            DEPART_ID = a.DEPART_ID,//部门ID
                            USTATE = a.USTATE,//状态
                            WX_CODE = a.WX_CODE,//微信账号
                            CREATE_TIME = a.CREATE_TIME,
                            CREATE_USERID = a.CREATE_USERID,
                        };
            int totalCount = 0;
            var list= query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevUserList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            LOGIN_NAME = a.LOGIN_NAME,//登录名称
                            SEX = a.SEX,//性别
                            TEL = a.TEL,//电话
                            ENTRY_TIME = a.ENTRY_TIME,//入职日期
                            EMAIL = a.EMAIL,//Email
                            ID_NO = a.ID_NO,//移动电话
                            CODE = a.CODE,//编号
                            DEPART_ID = a.DEPART_ID,//部门ID
                            USTATE = a.USTATE,//状态
                            WX_CODE = a.WX_CODE,//微信账号
                            CREATE_TIME = a.CREATE_TIME,
                            CREATE_USERID = a.CREATE_USERID,
                            SexDic= EmunUtility.GetDesc(typeof(UserSexEnum), a.SEX),

                        };
            return new AjaxListResult<DevUserList>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }


    




    }
}
