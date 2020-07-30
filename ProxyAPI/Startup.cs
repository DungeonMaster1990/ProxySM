using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dh = Common.Helpers.DependencyHelper;
using Common.Models.ConfigModels;

namespace ProxyAPI
{
    public class Startup
    {
        private IWebHostEnvironment _hostingEnvironment;
        private IServiceProvider _serviceProvider;
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
            services.AddMvc(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });
            
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
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
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMProxy");
                c.RoutePrefix = string.Empty;
            });

            //app.UseAuthorization();
            app.UseMvc();
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
