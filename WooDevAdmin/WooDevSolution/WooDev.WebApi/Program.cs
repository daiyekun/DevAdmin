using Microsoft.OpenApi.Models;
using SqlSugar;
using WooDev.IServices;
using WooDev.Services;
using WooDev.WebCommon.ServiceExtend;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region ҵ�����
//���ݿ�����
var dbconnoptions = new DbConnOptions();
builder.Configuration.Bind("DbConn", dbconnoptions);
//���ݿ����Ӷ���
builder.Services.AddDevDbServices(dbconnoptions);
builder.Services.AddDevServices();
builder.Services.AddDevOtherServices();
builder.Services.AddDevLog4Net();
#endregion


//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "����ϵͳ��̨API", Version = "v1" });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.DevAppUse();


app.Run();

