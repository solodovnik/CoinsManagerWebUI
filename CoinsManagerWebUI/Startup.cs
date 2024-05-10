using CoinsManagerWebUI.Authentication;
using CoinsManagerWebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using System;

namespace CoinsManagerWebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<LoginHandler>();
            services.AddHttpClient<ICoinCatalogService, CoinCatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["Api:Uri"]))
                .AddHttpMessageHandler<LoginHandler>();
            services.AddScoped<IAuthenticator, AzureJwtAuthenticator>();
            //Add logging to file system
            services.AddLogging(loggerBuilder => { loggerBuilder.AddAzureWebAppDiagnostics(); });
            //Add logging to AppInsights
            services.AddLogging(loggerBuilder => { loggerBuilder.AddApplicationInsights(
                configureTelemetryConfiguration: (config) => 
                config.ConnectionString = Configuration.GetConnectionString("AppInsights"), configureApplicationInsightsLoggerOptions: options => { }); });
            services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "logs-";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 5;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=CoinCatalog}/{action=Index}/{id?}");
            });
        }
    }
}
