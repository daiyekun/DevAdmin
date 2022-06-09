using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Common.Models
{
    /// <summary>
    /// 用于约束实体
    /// </summary>
     public interface IModelDTO{
        /// <summary>
        /// 约束ID
        /// </summary>
        int ID { get; set; }

    }
}
