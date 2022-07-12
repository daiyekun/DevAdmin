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
           //.ForMember(a => a.UPDATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.UPDATE_TIME, opt => opt.Ignore())
           .ForMember(a => a.CREATE_TIME, opt => opt.Ignore())
           //.ForMember(a => a.CREATE_USERID, opt => opt.Ignore())
           .ForMember(a => a.C_STATE, opt => opt.Ignore())
           
           ;




        }
    }
}
