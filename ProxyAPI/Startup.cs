using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dh = Common.Helpers.DependencyHelper;
using Common.Models.ConfigModels;
using Monitoring;
using Monitoring.Configurations;
using ProxyAPI.Monitoring;
using Monitoring.Models;
using Monitoring.Attributes;

namespace ProxyAPI
{
    public class Startup
    {
        private IWebHostEnvironment _hostingEnvironment;
        public IServiceProvider _serviceProvider;
        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.RegisterMonitoring<ProxyAPIMonitoring>(Configuration, "dev");
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
            services.Configure<SMApiConfigurationModel>(Configuration.GetSection("SMApiConfig"));

            services.Configure<MonitoringOptions>(Configuration.GetSection("MonitoringOptions"));
            //services.AddScoped<RequestMonitoringItem>();
            //services.AddScoped<MonitoringSendRequestFilterAttribute>();
            
            AddCommonServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddCommonServices(IServiceCollection services)
        {
            foreach (var dependency in dh.DependencyHelper.GetCommonDependencies())
            {
                services.AddTransient(dependency.Key, dependency.Value);
            }
        }
    }
}
