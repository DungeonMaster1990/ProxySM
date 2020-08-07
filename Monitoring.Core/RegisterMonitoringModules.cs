using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Extensions;
using Monitoring.Models;
using Monitoring.Services;
using Monitoring.Services.Sender;

namespace Monitoring
{
    public static class RegisterMonitoringModules
    {
        public static IServiceCollection RegisterMonitoringBase(IServiceCollection services, string environmentName, StatisticsItemsFullSet set)
        {
            set.ForEach(x => services.AddSingleton(x.GetType()));
            
            services.AddSingleton<IMonitoringSender, MonitoringSender>();
            services.AddSingleton<IStatisticsSender, StatisticsSender>();
            services.AddSingleton<JsonNLogDestination>();
            services.AddSingleton<LogDestination>();
            services.AddSingleton(set);
            var commonSet = new CommonMonitoringSet(environmentName);
            services.AddSingleton(commonSet);

            return services;

        }

        public static IServiceCollection RegisterStatisticsItemsFullSet(IServiceCollection services,
            StatisticsItemsFullSet set)
        {
            
        }
    }
}
