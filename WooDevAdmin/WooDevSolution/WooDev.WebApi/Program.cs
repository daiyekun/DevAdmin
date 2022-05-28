using Microsoft.OpenApi.Models;
using SqlSugar;
using WooDev.IServices;
using WooDev.Services;
using WooDev.WebCommon.ServiceExtend;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region 业务服务
//数据库连接
var dbconnoptions = new DbConnOptions();
builder.Configuration.Bind("DbConn", dbconnoptions);
//数据库连接对象
builder.Services.AddDevDbServices(dbconnoptions);
builder.Services.AddDevServices();

#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "管理系统后台API", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();//鉴权：解析信息--就是读取token，解密token

#region 添加跨域中间件
app.UseCors();
#endregion

app.UseAuthorization();

app.MapControllers();


app.Run();

