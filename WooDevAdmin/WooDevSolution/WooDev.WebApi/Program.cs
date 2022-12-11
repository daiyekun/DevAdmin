
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
#region ҵ�����
//���ݿ�����
var dbconnoptions = new DbConnOptions();
builder.Configuration.Bind("DbConn", dbconnoptions);
#region ��ʱ
//��־
QuartzUtility.QuarztLog(builder.Configuration["QuartzInfo:QuartzLogsCron"]);
#endregion
//���ݿ����Ӷ���
builder.Services.AddDevDbServices(dbconnoptions);
builder.Services.AddDevServices();
builder.Services.AddDevOtherServices();
builder.Services.AddDevLog4Net();
#endregion
//������룬��Ϊ����������������Ҫʹ����ͼ����pdf
builder.Services.AddMvc().AddNewtonsoftJson();
#region jwtУ��  HS



JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme-->JwtBearerDefaults.AuthenticationScheme
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
                    //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = false,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
    };
                //��Ȩʧ�ܵ�ʱ�򷵻ؽ��
                options.Events = new JwtBearerEvents
    {
        OnChallenge = context => {
            context.HandleResponse();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.WriteAsync(JsonConvert.SerializeObject(new DevResult()
            {
                 message= "ûȨ�޷��ʽӿ�",
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

