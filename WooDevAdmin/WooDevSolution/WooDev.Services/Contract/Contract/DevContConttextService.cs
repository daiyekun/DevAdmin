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
    /// 合同文本
    /// </summary>
    public partial class DevContConttextService
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
        public ResultPageData<DevContConttextList> GetList(PageInfo<DEV_CONT_CONTTEXT> pageInfo, Expression<Func<DEV_CONT_CONTTEXT, bool>> whereLambda,
             Expression<Func<DEV_CONT_CONTTEXT, object>> orderbyLambda, bool isAsc)
        {

            var tempquery = DbClient.Queryable<DEV_CONT_CONTTEXT>().Where(whereLambda);
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
                            CATE_ID = a.CATE_ID,//类别ID
                            FILE_NAME = a.FILE_NAME,//文件名称
                            EXTEND = a.EXTEND,//扩展名称
                            TEXT_PATH = a.TEXT_PATH,//文件路径
                            REMARK = a.REMARK,//备注
                            DOWN_TIMES = a.DOWN_TIMES,
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            STAGE=a.STAGE,//阶段
                            CONT_ID=a.CONT_ID,//合同ID


                        };
            int totalCount = 0;
            if ((pageInfo is NoPageInfo<DEV_CONT_CONTTEXT>))
            { //分页
                pageInfo.PageSize = 200000;
                pageInfo.PageIndex = 0;
            }
            var list = query.ToPageList(pageInfo.PageIndex, pageInfo.PageSize, ref totalCount);
            pageInfo.TotalCount = totalCount;
            var local = from a in list
                        select new DevContConttextList
                        {
                            ID = a.ID,
                            NAME = a.NAME,//显示名称
                            CATE_ID = a.CATE_ID,//类别ID
                            FILE_NAME = a.FILE_NAME,//文件名称
                            EXTEND = a.EXTEND,//扩展名称
                            TEXT_PATH = a.TEXT_PATH,//文件路径
                            REMARK = a.REMARK,//备注
                            DOWN_TIMES = a.DOWN_TIMES,
                            CREATE_USERID = a.CREATE_USERID,//创建人
                            CREATE_TIME = a.CREATE_TIME,//创建时间
                            CreateUserName = DevRedisUtility.GetUserField(a.CREATE_USERID),
                            CateName = DevRedisUtility.GetDataField(a.CATE_ID),
                            STAGE = a.STAGE,//阶段
                            CONT_ID = a.CONT_ID,//合同ID
                        };
            return new ResultPageData<DevContConttextList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">字段对象</param>
        /// <returns></returns>
        public int UpdateField(UpdateField updateField)
        {
            StringBuilder strb = new StringBuilder();
            string strsql = "";
            switch (updateField.Field)
            {
                case "CATE_ID"://类别
                case "CateName":
                    strsql = $"update  DEV_CONT_CONTTEXT set CATE_ID={updateField.Value} where ID={updateField.Id}";
                    strb.Append(strsql);
                    break;
                case "REMARK"://备注
                    strsql = $"update  DEV_CONT_CONTTEXT set REMARK='{updateField.Value}' where ID={updateField.Id}";
                    strb.Append(strsql);
                    break;

            }

            if (!string.IsNullOrEmpty(strb.ToString()))
            {
                return ExecuteCommand(strb.ToString());

            }
            return 0;
        }
    }
}
