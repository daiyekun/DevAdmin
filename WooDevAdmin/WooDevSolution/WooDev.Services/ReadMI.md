### 使用说明

操作数据基本方法已经写入到BaseService里面。并使用T4模板已经生成了基础的操作数据库服务

由于是采用T4模板自动生成，获取的基础是当前数据库实体类文件。所有操作方法一般如下：

1、使用 SqlSugarORM.ConsoleApp控制台程序生成实体

2、建立文件，并且下划线首字母大写的规则建立+Service

3、修改命名规则为 WooDev.Services

4、在class 前加 partial（表示分布文件）

以用户为例，您需要建立的文件如下：

```c#
/// <summary>
/// 系统用户
/// </summary>
public partial class DevUserService
{

}
```

1，如果你建立文件没有操作数据库连接上下文，请查看您建立的类文件与自动生成是否一致。如果不一致查看WooDevService.tt 下生成类文件是否一致

2，不需要集成集体接口，因为代码已经自动生成继承接口了，生成代码如下：

````c#
 public partial class DevUserService : BaseService<DEV_USER>, IDevUserService
    {
         public DevUserService(ISqlSugarClient DbClient)
           : base(DbClient)
        {
           
        }
		
		
    }
````





