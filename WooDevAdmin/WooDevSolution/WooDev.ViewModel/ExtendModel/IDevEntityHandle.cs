
using System;
using System.Collections.Generic;
using System.Text;

namespace WooDev.ViewModel.ExtendModel
{
    /// <summary>
    /// DTO操作接口
    /// </summary>
   public interface IDevEntityHandle
    {
       
        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <returns>属性值</returns>
        FieldInfo GetPropValue(string propName);
    }
    /// <summary>
    /// 实体DTO约束
    /// </summary>
    public interface IEntityDTO
    {
        /// <summary>
        /// 约束必须有ID
        /// </summary>
       int Id { get; set; }
       
    }
   
}
