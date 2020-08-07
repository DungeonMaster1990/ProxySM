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
        public static IServiceCollection RegisterMonitoringBase(this IServiceCollection services, string environmentName, BaseStatisticsItemsSet baseStatisticsItems)
        {
            services.AddSingleton<IMonitoringSender, MonitoringSender>();
            services.AddSingleton<IStatisticsSender, StatisticsSender>();
            services.AddSingleton<JsonNLogDestination>();
            services.AddSingleton<LogDestination>();
            var groups = new ConcurrentDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>>();
            services.AddSingleton(groups);
            var statItems = baseStatisticsItems.GetAllStatisticsMonitoringItems();

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
