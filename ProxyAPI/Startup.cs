using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dh = Common.Helpers.DependencyHelper;
using Common.Models.ConfigModels;
using Monitoring;
using ProxyAPI.Monitoring;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.RegisterMonitoring("dev", new ProxyAPIMonitoring(),)
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
            services.Configure<SMApiConfigurationModel>(Configuration.GetSection("SMApiConfig"));
            AddCommonServices(services);

            //_serviceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

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
