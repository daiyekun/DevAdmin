
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using SqlSugar;
using System.Text;
using WooDev.Auth;
using WooDev.Auth.Model;
using WooDev.Common.Models;
using WooDev.IServices;
using WooDev.Services;
using WooDev.WebCommon.FilterExtend;
using WooDev.WebCommon.ServiceExtend;
using WooDev.WebCommon.Utility;
using WooDev.WebCommon.Utiltiy.Quartz;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region JWt
builder.Services.AddTransient<ICustomJWTService, CustomHSJWTService>();
builder.Services.Configure<ConfigInformation>(builder.Configuration.GetSection("ConfigInformation"));
builder.Services.AddTransient<HttpHelperService>();
#endregion JWt
#region 业务服务
//数据库连接
var dbconnoptions = new DbConnOptions();
builder.Configuration.Bind("DbConn", dbconnoptions);
#region 定时
//日志
QuartzUtility.QuarztLog(builder.Configuration["QuartzInfo:QuartzLogsCron"]);
#endregion
//数据库连接对象
builder.Services.AddDevDbServices(dbconnoptions);
builder.Services.AddDevServices();
builder.Services.AddDevOtherServices();
builder.Services.AddDevLog4Net();
#endregion
//必须加入，因为我在生成审批单需要使用视图生成pdf
builder.Services.AddMvc().AddNewtonsoftJson();
#region jwt校验  HS



JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme-->JwtBearerDefaults.AuthenticationScheme
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
                    //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = false,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
    };
                //授权失败的时候返回结果
                options.Events = new JwtBearerEvents
    {
        OnChallenge = context => {
            context.HandleResponse();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(JsonConvert.SerializeObject(new DevResult()
            {
                 message= "没权限访问接口",
                 code= StatusCodes.Status401Unauthorized,
                
            }));
            return Task.FromResult(0);

        }

    };
});

#endregion

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "管理系统后台API", Version = "v1" });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.DevAppUse();


app.Run();

