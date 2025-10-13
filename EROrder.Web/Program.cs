using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using EROrder.Web.Extensions;
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

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

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