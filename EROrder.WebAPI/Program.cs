using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;
using EROrder.Shared.Helpers;
using EROrder.WebAPI.Extensions;
using EROrder.Core.Models;

try
{
    var builder = WebApplication.CreateBuilder(args);

    #region 加入 Serilog
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        .WriteTo.Console()
        .WriteTo.Logger(lg => lg
            .WriteTo.File(@$"Logs/All-.log",
                rollingInterval: RollingInterval.Day))
        .WriteTo.Logger(lg => lg
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error || e.Level == LogEventLevel.Fatal)
            .WriteTo.File(@$"Logs/Error-.log",
                rollingInterval: RollingInterval.Day))
        .WriteTo.Logger(lg => lg
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
            .WriteTo.File(@$"Logs/Warning-.log",
                rollingInterval: RollingInterval.Day))
        .CreateLogger();

    builder.Host.UseSerilog();
    #endregion

    #region 加入 DbContext
    builder.Services.AddDbContext<EROrderDbContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));    
    #endregion

    #region 加入 Newtonsoft.Json 支援
    builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = null
            };
        });
    #endregion

    #region 加入 Swagger 服務
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        foreach (var version in MagicHelper.ApiVersions.SupportedVersions)
        {
            options.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = MagicHelper.SwaggerInfo.Title,
                Version = version,
                Description = MagicHelper.SwaggerInfo.Description
            });
        }
    });

    builder.Services.AddSwaggerGenNewtonsoftSupport();
    #endregion

    builder.Services.AddServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        #region 開發環境啟用 Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in MagicHelper.ApiVersions.SupportedVersions)
            {
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", MagicHelper.SwaggerInfo.GetVersionTitle(version));
            }

            options.RoutePrefix = "swagger";
            options.DocExpansion(DocExpansion.None);
            options.DefaultModelsExpandDepth(0);
            options.DisplayRequestDuration();
            options.EnableTryItOutByDefault();
        });
        #endregion
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException && ex.Source != "Microsoft.EntityFrameworkCore.Design")
{
    Log.Error(ex, ex.Message);
}
finally
{
    Log.CloseAndFlush();
}