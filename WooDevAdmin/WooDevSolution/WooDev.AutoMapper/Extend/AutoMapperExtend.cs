using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.WooNet.AutoMapper.Extend
{
   public static class AutoMapperExtend
    {
        ///// <summary>
        ///// 领域模型转化为Dto
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public static mdto ToModel<m, mdto>(this m entity)
        //    where m : class, new()

        //{

        //    return Mapper.Map<m, mdto>(entity);
        //}

        ///// <summary>
        ///// Dto转化为领域模型
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static m ToEntity<m, mdto>(this mdto model)
        //    where m : class, new()
        //{
        //    return Mapper.Map<mdto, m>(model);
        //}

        ///// <summary>
        ///// 重载 ToEntity, 在已有 Dto模型基础上使用领域模型转换成 Dto
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public static m ToEntity<m, mdto>(this mdto model, m entity)
        //  where m : class, new()
        //{
        //    return Mapper.Map(model, entity);
        //}
    }
}
