using System;
using System.Linq;
using System.Text;
using SqlSugar;

#pragma warning disable 1591
#pragma warning disable 8618
namespace WooDev.Model.Models
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [SugarTable("dev_option_log")]
    public partial class DEV_OPTION_LOG:IDevEntitiy
    {
        /// <summary>
        /// DEV_OPTION_LOG构造函数
        /// </summary>
        public DEV_OPTION_LOG()
        {

        }
        /// <summary>
        /// 描    述:ID
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
        public int ID { get; set; }

        /// <summary>
        /// 描    述:操作名称
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 描    述:请求路径
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public string REQ_URL { get; set; }

        /// <summary>
        /// 描    述:请求参数
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REQ_PARAMETER { get; set; }

        /// <summary>
        /// 描    述:请求结果
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REQ_RESULT { get; set; }

        /// <summary>
        /// 描    述:ID
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 描    述:是否删除
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int IS_DELETE { get; set; }

        /// <summary>
        /// 描    述:创建人
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int CREATE_USERID { get; set; }

        /// <summary>
        /// 描    述:创建时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime CREATE_TIME { get; set; }

        /// <summary>
        /// 描    述:更新人
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public int UPDATE_USERID { get; set; }

        /// <summary>
        /// 描    述:更新时间
        /// 默 认 值:
        /// 是否空值:False
        /// </summary>
        public DateTime UPDATE_TIME { get; set; }

        /// <summary>
        /// 描    述:操作标题
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ACTION_TITLE { get; set; }

        /// <summary>
        /// 描    述:方法
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string ACTION_NAME { get; set; }

        /// <summary>
        /// 描    述:控制器
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string CONTROLLER_NAME { get; set; }

        /// <summary>
        /// 描    述:请求类型get post等
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? REQ_METHOD { get; set; }

        /// <summary>
        /// 描    述:增加 删除 修改 查询等
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public int? OPTION_TYPE { get; set; }

        /// <summary>
        /// 描    述:描述
        /// 默 认 值:
        /// 是否空值:True
        /// </summary>
        public string REMARK { get; set; }

    }
}
