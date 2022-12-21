using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;
using WooDev.ViewModel;

namespace Dev.WooNet.AutoMapper.ProFiles
{

    /// <summary>
    /// 合同主业务映射文件
    /// </summary>
    public class DevProContractFile : Profile, IProfile
    {
        public DevProContractFile()
        {
            //合同对方
            CreateMap<DevCompanyDTO, DEV_COMPANY>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.C_STATE, opt => opt.Ignore())
           
           ;

            //合同
            CreateMap<DevContractDTO, DEV_CONTRACT>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.CONT_STATE, opt => opt.Ignore())

           ;
            //计划资金
            CreateMap<DevContPlanFinanceDTO, DEV_CONT_PLAN_FINANCE>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.F_STATE, opt => opt.Ignore())
           .ForMember(a => a.F_TYPE, opt => opt.Ignore())

           ;

            //合同->合同历史
            CreateMap<DEV_CONTRACT, DEV_CONTRACT_HIST>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.CONT_ID, opt => opt.Ignore())
           ;

            //计划资金->计划资金历史
            CreateMap<DEV_CONT_PLAN_FINANCE, DEV_CONT_PLAN_FINANCE_HIST>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.CONT_HIST_ID, opt => opt.Ignore())
           .ForMember(a => a.CONT_ID, opt => opt.Ignore())
           ;

            //合同文本->合同文本历史
            CreateMap<DEV_CONT_CONTTEXT, DEV_CONT_CONTTEXT_HIST>()
           .ForMember(a => a.IS_DELETE, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.CONT_HIST_ID, opt => opt.Ignore())
           .ForMember(a => a.CONT_ID, opt => opt.Ignore())
           ;

           


        }
    }
}
