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
    /// <summary>
    /// Метод расширения для регистрации зависимосте на коллекции services
    /// </summary>
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
            var section = configuration.GetSection("MonitoringOptions");
            var monitoringOptions = new MonitoringOptions();
            section.Bind(monitoringOptions);
            var monitoringIOptions = Options.Create(monitoringOptions);
            services.Configure<MonitoringOptions>(section);

            var commonSet = new CommonMonitoringSet(environmentName);

            services.AddSingleton(commonSet);
            services.AddSingleton<Monitoring>();
            services.AddSingleton<IMonitoringSender, MonitoringSender>();

            var destinations = new List<IDestination>(newDestinations);
            var jsonNLogDestination = new JsonNLogDestination(commonSet, monitoringIOptions);

            destinations.Add(jsonNLogDestination);

            services.AddSingleton<IEnumerable<IDestination>>(destinations);

            var groups = new ConcurrentDictionary<(string, string), IStatisticsMonitoringItem>();
            services.AddSingleton<IDictionary<(string, string), IStatisticsMonitoringItem>>(groups);

            var statItems = typeof(Monitoring).GetProperties()
                .Where(p => typeof(IStatisticsMonitoringItem).IsAssignableFrom(p.PropertyType))
                .Select(x =>
                    {
                        var item = (IStatisticsMonitoringItem)Activator.CreateInstance(x.PropertyType);
                        item.Name = x.PropertyType.Name;
                        item.SetProperties();
                        return item;
                    }
                )
                .Select(x => new KeyValuePair<string, IStatisticsMonitoringItem>(x.Name, x));

            var itemsDictionary = new ConcurrentDictionary<string, IStatisticsMonitoringItem>(statItems);

            foreach (var item in itemsDictionary)
                services.AddSingleton(item.Value.GetType(), item.Value);

            services.AddSingleton<IDictionary<string, IStatisticsMonitoringItem>>(itemsDictionary);

            var set = new StatisticsItemsFullSet(itemsDictionary, groups);

            services.AddSingleton(set);

            var statisticsSender = new StatisticsSender(monitoringIOptions, destinations, set);
            services.AddSingleton<IStatisticsSender>(statisticsSender);

            return services;
        }
    }
}
