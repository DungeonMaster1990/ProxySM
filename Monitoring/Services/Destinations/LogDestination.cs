using Monitoring.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Monitoring.Configurations;

namespace Monitoring.Services
{
    public class LogDestination : IDestination
    {
        private static ILogger _log = LogManager.GetLogger("Statistics");

        private const char _delimeterHoriontal = '-';
        private const char _delimeterVertical = '|';
        private IDictionary<string, List<int>> _dynamicGroupPropertiesIntends;
        private IDictionary<string, List<int>> _monitoringItemsPropertiesIntends;
        private int _baseNameIntend;
        private int _previousHashCode = 0;
        private MonitoringOptions _monitoringOptions;

        public LogDestination(IOptions<MonitoringOptions> options)
        {
            _monitoringOptions = options.Value;
        }

        private (int maxLengthGroupName, IDictionary<string, List<int>> dynamicGroupsIntends) CalculateIntendesForGroups(StatisticsItemsFullSet items)
        {
            var intends = items.GroupItems
                .ToDictionary(x => x.Key,
                    x => x.Value.Properties.Keys.Select(y => y.Length).ToList());

            var maxGroupNameLength = items.GroupItems.Count == 0 ? 0 : items.GroupItems.Max(x => x.Key.GroupName.Length);

            return (maxGroupNameLength, new Dictionary<string, List<int>>());
        }

        private (int maxLengthItemName, IDictionary<string, List<int>> monitoringItemsPropertiesIntends) CalculateBasicIntendes(StatisticsItemsFullSet items)
        {
            var intends = items.Items.ToDictionary(x => x.Key, 
                x => x.Value.Properties.Keys.Select(y=>y.Length).ToList());
            var maxNameLength = items.Items.Max(x => x.Key.Length);
            return (maxNameLength, intends);
        }

        private void CalculateIntends(StatisticsItemsFullSet items)
        {
            var (maxLengthGroupName, dynamicGroupsIntends) = CalculateIntendesForGroups(items);
            var (maxLengthItemName, monitoringItemsPropertiesIntends) = CalculateBasicIntendes(items);
            _baseNameIntend = Math.Max(maxLengthGroupName, maxLengthItemName);
            _dynamicGroupPropertiesIntends = dynamicGroupsIntends;
            _monitoringItemsPropertiesIntends = monitoringItemsPropertiesIntends;
        }
        private string BuildLogBody(StatisticsItemsFullSet items)
        {
            return null;
            //var sb = new StringBuilder();

            //sb.AppendLine(_delimeter);

            //foreach (var group in groups)
            //{

            //    sb.Append($"{group.Key.Name, }")
            //}
            //foreach (var item in items)
            //{
            //    item.Properties
            //}
        }

        public void SendStatistics(StatisticsItemsFullSet items)
        {
            var hashCode = items.GetHashCode();
            if (hashCode != _previousHashCode)
            {
                _previousHashCode = hashCode;
                CalculateIntends(items);
            }
            var logMessage = BuildLogBody(items);
            _log.Info(logMessage);
        }

        public void SendOneItem(ILogger log, IMonitoringItem monitoringItem)
        {
            log.Info(monitoringItem.GetJObject().ToString(_monitoringOptions.JsonFormatingInSimpleLog));
        }
    }
}
