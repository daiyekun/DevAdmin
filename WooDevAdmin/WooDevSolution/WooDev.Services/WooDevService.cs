using WooDev.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Data.Common;
using System.Linq;
using SqlSugar;
using WooDev.Model.Models;
///****************************************************
///代码自动生成,需要修改builder里构造函数数据库连接字符串即可
///如果有个性业务在建立一个public partial interface 
///如有报错，添加引用NuGet  PetaPoco..NET6、T4 (2.0.1)
///****************************************************
namespace WooDev.Services
{
 
 public partial class DevCompanyService : BaseService<DEV_COMPANY>, IDevCompanyService
    {
         public DevCompanyService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevCompContactsService : BaseService<DEV_COMP_CONTACTS>, IDevCompContactsService
    {
         public DevCompContactsService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevCompFileService : BaseService<DEV_COMP_FILE>, IDevCompFileService
    {
         public DevCompFileService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevCompRecordService : BaseService<DEV_COMP_RECORD>, IDevCompRecordService
    {
         public DevCompRecordService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevCurrencyService : BaseService<DEV_CURRENCY>, IDevCurrencyService
    {
         public DevCurrencyService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevDatadicService : BaseService<DEV_DATADIC>, IDevDatadicService
    {
         public DevDatadicService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevDepartmentService : BaseService<DEV_DEPARTMENT>, IDevDepartmentService
    {
         public DevDepartmentService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevDepartMainService : BaseService<DEV_DEPART_MAIN>, IDevDepartMainService
    {
         public DevDepartMainService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempEdgeService : BaseService<DEV_FLOWTEMP_EDGE>, IDevFlowtempEdgeService
    {
         public DevFlowtempEdgeService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempEdgeHistService : BaseService<DEV_FLOWTEMP_EDGE_HIST>, IDevFlowtempEdgeHistService
    {
         public DevFlowtempEdgeHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempNodeService : BaseService<DEV_FLOWTEMP_NODE>, IDevFlowtempNodeService
    {
         public DevFlowtempNodeService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempNodeHistService : BaseService<DEV_FLOWTEMP_NODE_HIST>, IDevFlowtempNodeHistService
    {
         public DevFlowtempNodeHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempNodeInfoService : BaseService<DEV_FLOWTEMP_NODE_INFO>, IDevFlowtempNodeInfoService
    {
         public DevFlowtempNodeInfoService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowtempNodeInfoHistService : BaseService<DEV_FLOWTEMP_NODE_INFO_HIST>, IDevFlowtempNodeInfoHistService
    {
         public DevFlowtempNodeInfoHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowGroupService : BaseService<DEV_FLOW_GROUP>, IDevFlowGroupService
    {
         public DevFlowGroupService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowGroupuserService : BaseService<DEV_FLOW_GROUPUSER>, IDevFlowGroupuserService
    {
         public DevFlowGroupuserService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowTempService : BaseService<DEV_FLOW_TEMP>, IDevFlowTempService
    {
         public DevFlowTempService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowTempHistService : BaseService<DEV_FLOW_TEMP_HIST>, IDevFlowTempHistService
    {
         public DevFlowTempHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFunctionMenuService : BaseService<DEV_FUNCTION_MENU>, IDevFunctionMenuService
    {
         public DevFunctionMenuService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevLoginLogService : BaseService<DEV_LOGIN_LOG>, IDevLoginLogService
    {
         public DevLoginLogService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevOptionLogService : BaseService<DEV_OPTION_LOG>, IDevOptionLogService
    {
         public DevOptionLogService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevRoleService : BaseService<DEV_ROLE>, IDevRoleService
    {
         public DevRoleService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevRoleFunctionService : BaseService<DEV_ROLE_FUNCTION>, IDevRoleFunctionService
    {
         public DevRoleFunctionService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevRolePermissionService : BaseService<DEV_ROLE_PERMISSION>, IDevRolePermissionService
    {
         public DevRolePermissionService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevRoutemetaService : BaseService<DEV_ROUTEMETA>, IDevRoutemetaService
    {
         public DevRoutemetaService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevUserService : BaseService<DEV_USER>, IDevUserService
    {
         public DevUserService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevUserFunctionService : BaseService<DEV_USER_FUNCTION>, IDevUserFunctionService
    {
         public DevUserFunctionService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevUserOtherInfoService : BaseService<DEV_USER_OTHER_INFO>, IDevUserOtherInfoService
    {
         public DevUserOtherInfoService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevUserRoleService : BaseService<DEV_USER_ROLE>, IDevUserRoleService
    {
         public DevUserRoleService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
    
}

