using SqlSugar;
using SqlSugarORM.ConsoleApp;

try
{
    Console.WriteLine("开始-------生成ORM实体");

    ConnectionConfig connectionConfig = new ConnectionConfig()
    {
        DbType = DbType.MySql,
        //ConnectionString="server=localhost;port=3306;database=woodevadmin;user id=root;password=Sasa123"
        ConnectionString ="server=81.70.196.21;port=13306;database=woodevadmin;user id=wooroot;password=Wooroot123"
    };
    
    var baserdir = AppContext.BaseDirectory;
    string rootPath = AppContext.BaseDirectory.Substring(0, baserdir.IndexOf("SqlSugarORM"));
    var createPath = $"{rootPath}\\WooDev.Model\\Models\\"; 
    Console.WriteLine($"生成文件夹路径:{createPath}");

    using (SqlSugarClient sqlSugarClient=new SqlSugarClient(connectionConfig)){
       
        foreach (var item in sqlSugarClient.DbMaintenance.GetTableInfoList())
        {
            string entityName = item.Name.ToUpper();/*实体名大写*/
            sqlSugarClient.MappingTables.Add(entityName, item.Name);
            foreach (var col in sqlSugarClient.DbMaintenance.GetColumnInfosByTableName(item.Name))
            {
                sqlSugarClient.MappingColumns.Add(col.DbColumnName.ToUpper() /*类的属性大写*/, col.DbColumnName, entityName);
            }

        }


            //这里还可以sqlSugarClient.DbFirst.Where条件
            //sqlSugarClient.DbFirst.CreateClassFile(createPath, nameSpace:"WooDev.Model");
            sqlSugarClient.DbFirst.SettingClassTemplate(o => { return EntityHelper.SugarCustom.ClassTemplate; })
                     .SettingNamespaceTemplate(o => { return EntityHelper.SugarCustom.NamespaceTemplate; })
                     .SettingClassDescriptionTemplate(o => { return EntityHelper.SugarCustom.ClassDescriptionTemplate; })
                     .SettingConstructorTemplate(o => { return EntityHelper.SugarCustom.ConstructorTemplate; })
                     .SettingPropertyDescriptionTemplate(o => { return EntityHelper.SugarCustom.PropertyDescriptionTemplate; })
                     .SettingPropertyTemplate(o => { return EntityHelper.SugarCustom.PropertyTemplate; })
                     .IsCreateAttribute(true)
                     .CreateClassFile(createPath, nameSpace: "WooDev.Model.Models");



    }
    
    Console.WriteLine("结束-------生成ORM实体");
}
catch (Exception ex)
{
    Console.WriteLine("执行异常");
    Console.WriteLine(ex.Message);
}
