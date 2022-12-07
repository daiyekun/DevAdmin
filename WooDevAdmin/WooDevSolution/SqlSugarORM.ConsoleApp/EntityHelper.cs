using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarORM.ConsoleApp
{
    public class EntityHelper
    {

            /// <summary>
            /// 自定义模板
            /// <para>屏蔽CS1591警告：#pragma warning disable 1591</para>
            /// <para>构造函数添加描述</para>
            /// <para>调整部分模板的空格数量</para>
            /// </summary>
            public class SugarCustom
            {
                /// <summary>
                /// 类模板
                /// </summary>
                public const string ClassTemplate = @"{using}
#pragma warning disable 1591
#pragma warning disable 8618
namespace {Namespace}
{
 {ClassDescription}{SugarTable}
    public partial class {ClassName}:IDevEntitiy
    {
        /// <summary>
        /// {ClassName}构造函数
        /// </summary>
        public {ClassName}()
        {
{Constructor}
        }
{PropertyName}
    }
}
";
                /// <summary>
                /// 构造函数参数赋值模板
                /// </summary>
                public const string ConstructorTemplate = @"            this.{PropertyName} = {DefaultValue};
";
                /// <summary>
                /// 引用模板
                /// </summary>
                public const string NamespaceTemplate = @"using System;
using System.Linq;
using System.Text;
";
                /// <summary>
                /// 类描述模板
                /// </summary>
                public const string ClassDescriptionTemplate = @"   /// <summary>
    /// {ClassDescription}    /// </summary>";
                /// <summary>
                /// 属性描述模板
                /// </summary>
                public const string PropertyDescriptionTemplate = @"        /// <summary>
        /// 描    述:{PropertyDescription}
        /// 默 认 值:{DefaultValue}
        /// 是否空值:{IsNullable}
        /// </summary>";

                /// <summary>
                /// 属性模板
                /// </summary>
                public const string PropertyTemplate = @"{SugarColumn}
        public {PropertyType} {PropertyName} { get; set; }
";
            }
            /// <summary>
            /// 默认模板
            /// </summary>
            public class SugarDefault
            {
                /// <summary>
                /// 类模板
                /// </summary>
                public const string ClassTemplate = @"{using}
namespace {Namespace}
{
{ClassDescription}{SugarTable}
    public partial class {ClassName}
    {
           public {ClassName}(){
{Constructor}
           }
{PropertyName}
    }
}
";
                /// <summary>
                /// 构造函数参数赋值模板
                /// </summary>
                public const string ConstructorTemplate = @"            this.{PropertyName} ={DefaultValue};
";
                /// <summary>
                /// 引用模板
                /// </summary>
                public const string NamespaceTemplate = @"using System;
using System.Linq;
using System.Text;
";
                /// <summary>
                /// 类描述模板
                /// </summary>
                public const string ClassDescriptionTemplate = @"    ///<summary>
    ///{ClassDescription}    ///</summary>
";
                /// <summary>
                /// 属性描述模板
                /// </summary>
                public const string PropertyDescriptionTemplate = @"           /// <summary>
           /// Desc:{PropertyDescription}
           /// Default:{DefaultValue}
           /// Nullable:{IsNullable}
           /// </summary>";

                /// <summary>
                /// 属性模板
                /// </summary>
                public const string PropertyTemplate = @"           {SugarColumn}
           public {PropertyType} {PropertyName} {get;set;}
";
            }


        }
    }


