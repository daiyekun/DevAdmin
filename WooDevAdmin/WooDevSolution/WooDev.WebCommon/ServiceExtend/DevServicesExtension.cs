using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Auth;
using WooDev.Auth.Model;
using WooDev.IServices;
using WooDev.Services;

namespace WooDev.WebCommon.ServiceExtend
{
    /// <summary>
    /// 服务注册
    /// 扩展
    /// </summary>
    public static class DevServicesExtension
    {
       
        /// <summary>
        /// 注册数据连接操作对象
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevDbServices(this IServiceCollection services, DbConnOptions dbconnoptions)
        {

            services.AddTransient<ISqlSugarClient>(option =>
            {
                
                SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = dbconnoptions.MasterConnectionString,//"server=localhost;port=3306;database=woodevadmin;user id=root;password=Sasa123",
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    SlaveConnectionConfigs = dbconnoptions.SlaveConnectionConfigs
                    //new List<SlaveConnectionConfig>() {
                    //     new SlaveConnectionConfig() { HitRate=10, ConnectionString="server=localhost;port=3306;database=woodevadmin;user id=root;password=Sasa123" },
                    //     new SlaveConnectionConfig() { HitRate=10, ConnectionString="server=localhost;port=3306;database=woodevadmin;user id=root;password=Sasa123" },
                    //     new SlaveConnectionConfig() { HitRate=10, ConnectionString="server=localhost;port=3306;database=woodevadmin;user id=root;password=Sasa123" },

                    //}
                });

                client.Aop.OnLogExecuting = (sql, par) =>
                {
                    Console.WriteLine($"Sql语句{sql}");
                };

                return client;

            });
        }
        /// <summary>
        /// 注册业务服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevServices(this IServiceCollection services)
        {
            //services.Configure<HttpAPIInvokerOptions>(action);//配置给IOC  其他字段用默认值
            
            services.AddTransient<IDevUserService, DevUserService>();
            services.AddTransient<IDevDatadicService, DevDatadicService>();
            services.AddTransient<IDevDepartmentService, DevDepartmentService>();
            services.AddTransient<IDevRoleService, DevRoleService>();
            services.AddTransient<IDevLoginLogService, DevLoginLogService>();
            services.AddTransient<IDevOptionLogService, DevOptionLogService>();
            services.AddTransient<IDevUserOtherInfoService, DevUserOtherInfoService>();
            services.AddTransient<IDevFunctionMenuService, DevFunctionMenuService>();
            services.AddTransient<IDevRoutemetaService, DevRoutemetaService>();
            services.AddTransient<IDevRoleFunctionService, DevRoleFunctionService>();
            services.AddTransient<IDevRolePermissionService, DevRolePermissionService>();
         
            #region 合同对方
            services.AddTransient<IDevCompanyService,DevCompanyService>();
            services.AddTransient<IDevCompContactsService, DevCompContactsService>();
            services.AddTransient<IDevCompFileService, DevCompFileService>();
            services.AddTransient<IDevCompRecordService, DevCompRecordService>();
            #endregion

            #region 流程
            services.AddTransient<IDevFlowGroupService, DevFlowGroupService>();
            services.AddTransient<IDevFlowtempEdgeService, DevFlowtempEdgeService>();
            services.AddTransient<IDevFlowtempNodeService, DevFlowtempNodeService>();
            services.AddTransient<IDevFlowtempNodeInfoService,DevFlowtempNodeInfoService>();
            services.AddTransient<IDevFlowTempService, DevFlowTempService>();
            services.AddTransient<IDevFlowGroupuserService, DevFlowGroupuserService>();
            services.AddTransient<IDevFlowInstanceService, DevFlowInstanceService>();
            services.AddTransient<IDevFlowInstanceService, DevFlowInstanceService>();
            services.AddTransient<IDevFlowInstanceService, DevFlowInstanceService>();
            services.AddTransient<IDevFlowInstanceService, DevFlowInstanceService>();
            services.AddTransient<IDevFlowInstOptionService, DevFlowInstOptionService>();
            services.AddTransient<IDevFlowInstNodeInfoService, DevFlowInstNodeInfoService>();
            services.AddTransient<IDevFlowInstNodeService, DevFlowInstNodeService>();
            services.AddTransient<IDevFlowInstGroupuserService, DevFlowInstGroupuserService>();
            

            #endregion

        }

    }
}
