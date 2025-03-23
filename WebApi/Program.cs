using Common.Configuration;
using DAL;
using Lib;
using Lib.Middleware;
using Microsoft.OpenApi.Models;
using Models;
using NLog.Web;
using System.Reflection;
using WebApi.Attributes;

var builder = WebApplication.CreateBuilder(args);


#region 砞﹚appSetting
var settingName = "appsettings";
builder.Configuration
    .AddJsonFile(path: $"{settingName}.json", optional: true)
    .AddJsonFile(path: $"{settingName}.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();
ConfigurationFactory.Initial(builder.Configuration);
#endregion

#region 砞﹚ NLog
// 砞﹚ NLog ら粁╰参
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();  // NLog 砞﹚
#endregion

#region 砞﹚办Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ResponseAttribute));
    options.Filters.Add(typeof(ExceptionAttribute));
});
#endregion

#region ref proj
builder.Services.AddDAL();
builder.Services.AddLib();
builder.Services.AddModels();
#endregion ref proj

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // API 磞瓃獺
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "瓣地.NET engineer絬穨",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "瑇花岸",
            Email = "starlet978@gmail.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ㄏノいざ硁砰
app.UseMiddleware<ApiCallLogMiddleware>();

app.MapControllers();

app.Run();
