using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Model.Models
{

    /// <summary>
    /// 
    /// </summary>
    public partial class DEV_ROLE
    {
        /// <summary>
        /// 一对多， 一个角色拥有多个菜单
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(DEV_ROLE_FUNCTION.ROLE))]
        public List<DEV_ROLE_FUNCTION> Menus { get; set; }//注意禁止给books手动赋值
    }
}
