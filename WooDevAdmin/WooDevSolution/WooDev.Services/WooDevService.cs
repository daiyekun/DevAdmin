﻿using WooDev.IServices;
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
    
   
     
 public partial class DevContractService : BaseService<DEV_CONTRACT>, IDevContractService
    {
         public DevContractService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContractHistService : BaseService<DEV_CONTRACT_HIST>, IDevContractHistService
    {
         public DevContractHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContActfinceService : BaseService<DEV_CONT_ACTFINCE>, IDevContActfinceService
    {
         public DevContActfinceService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContAttachmentService : BaseService<DEV_CONT_ATTACHMENT>, IDevContAttachmentService
    {
         public DevContAttachmentService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContConsultService : BaseService<DEV_CONT_CONSULT>, IDevContConsultService
    {
         public DevContConsultService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContConttextService : BaseService<DEV_CONT_CONTTEXT>, IDevContConttextService
    {
         public DevContConttextService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContConttextHistService : BaseService<DEV_CONT_CONTTEXT_HIST>, IDevContConttextHistService
    {
         public DevContConttextHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContDescService : BaseService<DEV_CONT_DESC>, IDevContDescService
    {
         public DevContDescService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContPlanfinceCheckService : BaseService<DEV_CONT_PLANFINCE_CHECK>, IDevContPlanfinceCheckService
    {
         public DevContPlanfinceCheckService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContPlanFinanceService : BaseService<DEV_CONT_PLAN_FINANCE>, IDevContPlanFinanceService
    {
         public DevContPlanFinanceService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContPlanFinanceHistService : BaseService<DEV_CONT_PLAN_FINANCE_HIST>, IDevContPlanFinanceHistService
    {
         public DevContPlanFinanceHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContStaticService : BaseService<DEV_CONT_STATIC>, IDevContStaticService
    {
         public DevContStaticService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContSubDescService : BaseService<DEV_CONT_SUB_DESC>, IDevContSubDescService
    {
         public DevContSubDescService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContSubMatterService : BaseService<DEV_CONT_SUB_MATTER>, IDevContSubMatterService
    {
         public DevContSubMatterService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevContSubMatterHistService : BaseService<DEV_CONT_SUB_MATTER_HIST>, IDevContSubMatterHistService
    {
         public DevContSubMatterHistService(ISqlSugarClient DbClient)
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
    
   
     
 public partial class DevFlowInstanceService : BaseService<DEV_FLOW_INSTANCE>, IDevFlowInstanceService
    {
         public DevFlowInstanceService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstanceHistService : BaseService<DEV_FLOW_INSTANCE_HIST>, IDevFlowInstanceHistService
    {
         public DevFlowInstanceHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstEdgeService : BaseService<DEV_FLOW_INST_EDGE>, IDevFlowInstEdgeService
    {
         public DevFlowInstEdgeService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstEdgeHistService : BaseService<DEV_FLOW_INST_EDGE_HIST>, IDevFlowInstEdgeHistService
    {
         public DevFlowInstEdgeHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstEndUserService : BaseService<DEV_FLOW_INST_END_USER>, IDevFlowInstEndUserService
    {
         public DevFlowInstEndUserService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstGroupuserService : BaseService<DEV_FLOW_INST_GROUPUSER>, IDevFlowInstGroupuserService
    {
         public DevFlowInstGroupuserService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstGroupuserHistService : BaseService<DEV_FLOW_INST_GROUPUSER_HIST>, IDevFlowInstGroupuserHistService
    {
         public DevFlowInstGroupuserHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstNodeService : BaseService<DEV_FLOW_INST_NODE>, IDevFlowInstNodeService
    {
         public DevFlowInstNodeService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstNodeHistService : BaseService<DEV_FLOW_INST_NODE_HIST>, IDevFlowInstNodeHistService
    {
         public DevFlowInstNodeHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstNodeInfoService : BaseService<DEV_FLOW_INST_NODE_INFO>, IDevFlowInstNodeInfoService
    {
         public DevFlowInstNodeInfoService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstNodeInfoHistService : BaseService<DEV_FLOW_INST_NODE_INFO_HIST>, IDevFlowInstNodeInfoHistService
    {
         public DevFlowInstNodeInfoHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstOptionService : BaseService<DEV_FLOW_INST_OPTION>, IDevFlowInstOptionService
    {
         public DevFlowInstOptionService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstOptionHistService : BaseService<DEV_FLOW_INST_OPTION_HIST>, IDevFlowInstOptionHistService
    {
         public DevFlowInstOptionHistService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
    
   
     
 public partial class DevFlowInstWaitUserService : BaseService<DEV_FLOW_INST_WAIT_USER>, IDevFlowInstWaitUserService
    {
         public DevFlowInstWaitUserService(ISqlSugarClient DbClient)
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

