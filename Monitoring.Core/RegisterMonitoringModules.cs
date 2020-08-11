using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.Models;
using Monitoring.Services;
using Monitoring.Services.Sender;

namespace Monitoring
{
    public static class RegisterMonitoringModules 
    {
        public static IServiceCollection RegisterMonitoring<Monitoring>(this IServiceCollection services, string environmentName)
        where Monitoring : class
        {
            services.AddSingleton<Monitoring>();
            services.AddSingleton<IMonitoringSender, MonitoringSender>();
            services.AddSingleton<IStatisticsSender, StatisticsSender>();
            services.AddSingleton<JsonNLogDestination>();
            services.AddSingleton<LogDestination>();
            
            var groups = new ConcurrentDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>>();
            services.AddSingleton<IDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>>>(groups);

            var statItems = typeof(Monitoring).GetProperties()
                .Where(p => typeof(StatisticsMonitoringItemBase).IsAssignableFrom(p.PropertyType))
                .Select(x =>
                    (StatisticsMonitoringItemBase) Activator.CreateInstance(x.PropertyType))
                .Select(x => new KeyValuePair<string, StatisticsMonitoringItemBase>(x.Name, x));

            var items = new ConcurrentDictionary<string, StatisticsMonitoringItemBase>(statItems);
            foreach (var item in items)
                services.AddSingleton(item.Value);
            services.AddSingleton<StatisticsItemsFullSet>();
            services.AddSingleton<IDictionary<string, StatisticsMonitoringItemBase>>(items);
            var set = new StatisticsItemsFullSet(items, groups);
            services.AddSingleton(set);

            var commonSet = new CommonMonitoringSet(environmentName);
            services.AddSingleton(commonSet);

            return services;
        }
    }
}
