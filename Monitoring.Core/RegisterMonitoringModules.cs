using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using Monitoring.Models;
using Monitoring.Services;
using Monitoring.Services.Sender;

namespace Monitoring
{
    public static class RegisterMonitoringModules 
    {
        public static IServiceCollection RegisterMonitoring<Monitoring>(this IServiceCollection services,
            IConfiguration configuration, string environmentName)
            where Monitoring : class
        {
            return services.RegisterMonitoring<Monitoring>(configuration, new List<IDestination>(), environmentName);
        }

        public static IServiceCollection RegisterMonitoring<Monitoring>(this IServiceCollection services, IConfiguration configuration, IEnumerable<IDestination> newDestinations, string environmentName)
        where Monitoring : class
        {
            var monitoringOptions = new MonitoringOptions();
            var section = configuration.GetSection("MonitoringOptions");
            section.Bind(monitoringOptions);
            var monitoringIOptions = Options.Create(monitoringOptions);

            var commonSet = new CommonMonitoringSet(environmentName);
            services.AddSingleton(commonSet);
            section.Bind(monitoringOptions);
            services.Configure<MonitoringOptions>(section);
            services.AddSingleton<Monitoring>();
            services.AddSingleton<IMonitoringSender, MonitoringSender>();

            var destinations = new List<IDestination>(newDestinations);
            var jsonNLogDestination = new JsonNLogDestination(commonSet, monitoringIOptions);
            var logDestination = new LogDestination(monitoringIOptions);
            destinations.Add(jsonNLogDestination);
            destinations.Add(logDestination);

            services.AddSingleton<IEnumerable<IDestination>>(destinations);

            var groups = new ConcurrentDictionary<string, StatisticsMonitoringGroup<IStatisticsMonitoringItem>>();
            services.AddSingleton<IDictionary<string, StatisticsMonitoringGroup<IStatisticsMonitoringItem>>>(groups);

            var statItems = typeof(Monitoring).GetProperties()
                .Where(p => typeof(StatisticsMonitoringItemBase).IsAssignableFrom(p.PropertyType))
                .Select(x =>
                    (StatisticsMonitoringItemBase) Activator.CreateInstance(x.PropertyType))
                .Select(x => new KeyValuePair<string, StatisticsMonitoringItemBase>(x.Name, x));

            var items = new ConcurrentDictionary<string, StatisticsMonitoringItemBase>(statItems);
            foreach (var item in items)
                services.AddSingleton(item.Value.GetType(), item.Value);
            services.AddSingleton<StatisticsItemsFullSet>();
            services.AddSingleton<IDictionary<string, StatisticsMonitoringItemBase>>(items);
            var set = new StatisticsItemsFullSet(items, groups);
            services.AddSingleton(set);

            var statisticsSender = new StatisticsSender(monitoringIOptions, destinations, set);
            services.AddSingleton<IStatisticsSender>(statisticsSender);
            return services;
        }
    }
}
