using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
            services.AddSingleton(groups);
            var statItems = typeof(Monitoring).GetProperties()
                .Where(p => p.GetType().IsAssignableFrom(typeof(StatisticsMonitoringItemBase)))
                .Select(x => x.GetValue(x) as StatisticsMonitoringItemBase).ToList();

            foreach (var item in statItems)
                services.AddSingleton(item);

            var items = new ConcurrentDictionary<string, StatisticsMonitoringItemBase>(
                statItems.Select(x => new KeyValuePair<string, StatisticsMonitoringItemBase>(x.Name, x)));

            services.AddSingleton(items);
            var set = new StatisticsItemsFullSet(items, groups);
            services.AddSingleton(set);

            var commonSet = new CommonMonitoringSet(environmentName);
            services.AddSingleton(commonSet);

            return services;
        }
    }
}
