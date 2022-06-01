### 使用说明

ViewModel.tt 是T4模板，目的主要是自动生成列表实体 ListView，提交数据库实体DTO,查看页面实体View

以用户为例生成代码如下：

```c#
//列表使用
public partial class DevUserList : DEV_USER
{ }
//提交数据库使用
public partial class DevUserDTO : DEV_USER
{ }
//暂定--查看页面使用
public partial class DevUserView : DEV_USER
{ }
```

比如查询列表时，我们需要建立一个DevUserList 类文件，然后在类里写入相应的增加字段代码即可。

**需要注意的是命名空间必须是：WooDev.ViewModel 以及class 前加入partial。否则无法使用T4自动生成的代码**



````c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooDev.Model.Models;

namespace WooDev.ViewModel
{

    /// <summary>
    /// 用户列表list
    /// </summary>
    public partial class DevUserList
    {
        /// <summary>
        /// 性别描述
        /// </summary>
      public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
      public string UstateDic { get; set; }

    }
}

````





