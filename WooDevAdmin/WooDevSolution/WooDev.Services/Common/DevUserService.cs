
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WooDev.Common.Extend;
using WooDev.Common.Models;
using WooDev.Common.Utility;
using WooDev.IServices;
using WooDev.Model.Models;
using WooDev.ViewModel;
using WooDev.ViewModel.Enums;
using WooDev.ViewModel.ExtendModel;

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
        public ResultPageData<DevUserList> GetList(PageInfo<DEV_USER> pageInfo, Expression<Func<DEV_USER, bool>> whereLambda,
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
            return new ResultPageData<DevUserList>()
            {
                items = local.ToList(),
                total = pageInfo.TotalCount,
                page = pageInfo.PageIndex,
                pageSize = pageInfo.PageSize


            };
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName">登录名称</param>
        /// <param name="Pwd">密码</param>
        /// <returns></returns>
        public LoginResult Login(string LoginName, string Pwd)
        {
            LoginResult loginResult = new LoginResult();
            var userinfo = DbClient.Queryable<DEV_USER>().Where(a => a.LOGIN_NAME == LoginName).First();
            if (userinfo != null)
            {
                var encryptpwd = EncryptUtility.PwdToMD5(Pwd, LoginName);
                if (userinfo.PWD == encryptpwd)
                {
                    loginResult.UserId= userinfo.ID;
                    loginResult.Reult = 0; //表示验证通过
                    loginResult.Role = new RoleInfo { RoleName = "dev", Value = "100" };
                    var loginuser = new LoginUser();
                    loginuser.Id = userinfo.ID;
                    loginuser.LoginName = userinfo.LOGIN_NAME;
                    loginuser.Name = userinfo.NAME;
                    loginuser.DeptId = userinfo.DEPART_ID;
                    loginuser.RoleId = userinfo.ROLE_ID;
                    loginuser.DeptName = RedisUtility.HashGet($"{RedisKeys.DepartHashKey}", "Name");
                    loginuser.RoleIds = GetRoleIdsByUserId(userinfo.ID);
                    loginResult.LoginUser = loginuser;

                }
                else
                {
                    loginResult.Reult = 2;//密码验证错误
                }
            }
            else
            {
                loginResult.Reult = 1;//当前用户名称不存在
            }

            return loginResult;

        }


     

        /// <summary>
        /// 根据用户获取角色Id集合
        /// </summary>
        private string GetRoleIdsByUserId(int userId)
        {
            var list = DbClient.Queryable<DEV_USER_ROLE>().Where(a => a.USER_ID == userId).Select(a => a.ROLE_ID).ToList();
            return StringHelper.ArrayInt2String(list);
        }
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public CurrLoginUser GetUserInfoById(int userId)
        {
            var otheruser = DbClient.Queryable<DEV_USER_OTHER_INFO>()
                .Where(a => a.USER_ID == userId).First();
            var query = DbClient.Queryable<DEV_USER>().Where(a => a.ID == userId)
                  .Select(a => new
                  {
                      UserId = a.ID,
                      UserName = a.LOGIN_NAME,
                      RealName = a.NAME,
                      DepartId = a.DEPART_ID,//所属部门ID
                       RoleId = a.ROLE_ID,//角色ID

                   });
            var firstuser = query.First();

            
            if (firstuser != null) {
            var loginuser = new CurrLoginUser()
            {
                UserId = firstuser.UserId,
                UserName = firstuser.UserName,
                RealName = firstuser.RealName,
                DepartId = firstuser.DepartId,//所属部门ID
                DepartName = DevRedisUtility.GetDeptName(firstuser.DepartId),//部门名称,
                Avatar = GetHead(otheruser),//otheruser != null ? otheruser.HEADPATH : "",//头像
                Desc = otheruser != null ? otheruser.REMARK : "",//描述
                RoleId = firstuser.RoleId,//角色ID
            };

            return loginuser;
            }
            return null;

        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="otheruser"></param>
        /// <returns></returns>
        public string GetHead(DEV_USER_OTHER_INFO otheruser)
        {
            string headstr = "/Uploads/UserHead/woohead.png";
            if (otheruser!=null&&!string.IsNullOrEmpty(otheruser.HEADPATH))
            {
                headstr = otheruser.HEADPATH;
            }
            return headstr;
        }

        /// <summary>
        /// 根据where条件判断用户是否存在
        /// </summary>
        /// <param name="whereLambda">where条件</param>
        /// <returns>是否存在</returns>
        public bool IsAccountExist(Expression<Func<DEV_USER, bool>> whereLambda)
        {
            return DbClient.Queryable<DEV_USER>().Where(whereLambda).Any();

        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<DevUserDTO> GetAll()
        {
           
                var query = from a in DbClient.Queryable<DEV_USER>()
                            select new
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                LOGIN_NAME = a.LOGIN_NAME,//登录
                                TEL=a.TEL,//电话
                                EMAIL=a.EMAIL,//电话
                                WX_CODE=a.WX_CODE,//微信账号
                                ROLE_ID=a.ROLE_ID,
                                DEPART_ID=a.DEPART_ID,
                                


                            };
                var local = from a in query
                            select new DevUserDTO
                            {
                                ID = a.ID,
                                NAME = a.NAME,//名称
                                CODE = a.CODE,//编号
                                LOGIN_NAME = a.LOGIN_NAME,//登录
                                TEL = a.TEL,//电话
                                EMAIL = a.EMAIL,//电话
                                WX_CODE = a.WX_CODE,//微信账号
                                ROLE_ID = a.ROLE_ID,
                                DEPART_ID = a.DEPART_ID,

                            };
            return  local.ToList();
               


            
           


        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{RedisKeys.RoleHashKey}";
                RedisUtility.KeyDeleteAsync(RedisKeys.RoleAllListKey);
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevUserDTO>($"{curdickey}", (a, c) =>
                    {
                        return $"{a}:{c}";
                    });
                }
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
            }


        }

        /// <summary>
        /// 根据ID获取用户详细信息
        /// </summary>
        /// <returns>用户详细信息</returns>
        public DevUserViewInfo GetViewInfoById(int id)
        {
            var otheruser = DbClient.Queryable<DEV_USER_OTHER_INFO>()
                   .Where(a => a.USER_ID == id).First();
            var tempquery = DbClient.Queryable<DEV_USER>().Where(a => a.ID == id);
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
                            ROLE_ID=a.ROLE_ID,
                            
                           
                        };
            var local = from a in query.ToList()
                        select new DevUserViewInfo
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
                            SexDic = EmunUtility.GetDesc(typeof(UserSexEnum), a.SEX),
                            DepName= DevRedisUtility.GetDeptName(a.DEPART_ID),//经办机构//a.DeptName,,
                            StateDic = EmunUtility.GetDesc(typeof(UserStateEnum), a.USTATE),
                            ROLE_ID = a.ROLE_ID,
                            REMARK= otheruser!=null? otheruser.REMARK:"",
                            ADDRESS = otheruser != null ? otheruser.ADDRESS : "",



                        };
            return local.First();

            

        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Ids">选择用户</param>
        /// <returns></returns>
        public int RestPwd(string Ids)
        {
            var listids = StringHelper.String2ArrayInt(Ids);
            var list = DbClient.Queryable<DEV_USER>().Where(a => listids.Contains(a.ID)).ToList();
            List<DEV_USER> users = new List<DEV_USER>();
            foreach (var item in list)
            {

                item.PWD = EncryptUtility.PwdToMD5("1", item.LOGIN_NAME);

                users.Add(item);
            }

            Update(users);
            return users.Count();




        }





    }
}
