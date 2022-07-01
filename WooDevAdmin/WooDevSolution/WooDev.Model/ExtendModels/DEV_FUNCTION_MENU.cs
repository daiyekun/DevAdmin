using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooDev.Model.Models
{

    /// <summary>
    /// 菜单
    /// </summary>
    public partial class DEV_FUNCTION_MENU
    {
        [Navigate(NavigateType.OneToOne, nameof(META_ID))]//一对一
        public DEV_ROUTEMETA RouteMate { get; set; }
    }
}
