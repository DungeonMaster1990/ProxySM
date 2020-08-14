using Monitoring.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using Newtonsoft.Json;

namespace Monitoring.Services
{
    public class LogDestination : IDestination
    {
        private static ILogger _log = LogManager.GetLogger("Statistics");

        private const char _delimeterHoriontal = '-';
        private const char _delimeterVertical = '|';
        private const int _addCharsInCell = 6;
        private (List<int> DynamicGroupIntends, List<int> ItemsGroupIntends) _intends;
        private int _previousHashCode = 0;
        private MonitoringOptions _monitoringOptions;
        private (int GroupNameIntend, int ItemsNameIntend) _baseIntends = (0, 0);
        private int _itemsNameIntend;

        public LogDestination(IOptions<MonitoringOptions> options)
        {
            _monitoringOptions = options.Value;
        }

        private (int maxLengthGroupName, List<int> dynamicGroupsIntends) CalculateIntendesForGroups(StatisticsItemsFullSet items)
        {
            var propertiesNames = items.GroupItems.Values.Select(x => x.Properties.Keys.ToList()).ToList();

            var columnNumber = propertiesNames.Max(x => x.Count);
            var intends = new List<int>(columnNumber);

            for (int i = 0; i < propertiesNames.Count; i++)
                for (int j = 0; j < propertiesNames[i].Count; j++)
                {
                    var maxIntend = Math.Max(intends[j], propertiesNames[i][j].Length);
                    intends[j] = maxIntend;
                }

            for (int i = 0; i < propertiesNames.Count; i++)
            {
                intends[i] += _addCharsInCell;
            }

            var firstColumnIntend = items.GroupItems.Count == 0 ? 0 : items.GroupItems.Max(x => Math.Max(x.Key.ItemName.Length, x.Key.GroupName.Length));

            return (firstColumnIntend, intends);
        }

        private (int maxLengthItemName, List<int> monitoringItemsPropertiesIntends) CalculateBasicIntendes(StatisticsItemsFullSet items)
        {
            var propertiesNames = items.Items.Values.Select(x => x.Properties.Keys.ToList()).ToList();

            var columnNumber = propertiesNames.Max(x => x.Count);
            var intends = new List<int>(columnNumber);

            for (int i = 0; i < propertiesNames.Count; i++)
                for (int j = 0; j < propertiesNames[i].Count; j++)
                {
                    var maxIntend = Math.Max(intends[j], propertiesNames[i][j].Length);
                    intends[j] = maxIntend;
                }

            for (int i = 0; i < propertiesNames.Count; i++)
            {
                intends[i] += _addCharsInCell;
            }

            var maxNameLength = items.Items.Max(x => x.Key.Length);
            return (maxNameLength, intends);
        }

        private void CalculateIntends(StatisticsItemsFullSet items)
        {
            (_baseIntends.GroupNameIntend, _intends.DynamicGroupIntends) = CalculateIntendesForGroups(items);
            (_baseIntends.ItemsNameIntend, _intends.ItemsGroupIntends) = CalculateBasicIntendes(items);
        }
        private string BuildLogBody(StatisticsItemsFullSet items)
        {
            var sb = new StringBuilder();
            sb.Append("Monitoring Groups");

            var delimeterLineLength = _intends.DynamicGroupIntends.Sum(x => x) + 2;
            var delimeterString = new string(_delimeterHoriontal, delimeterLineLength);

            var groups = items.GroupItems.Values
                .GroupBy(x => x.GroupName)
                .ToDictionary(x => x.Key, x => x.ToList());

            sb.AppendLine(delimeterString);
            return null;
        }

        private string PrintGroup(string groupName, IList<IStatisticsMonitoringItem> monitoringItems)
        {
            var sb = new StringBuilder();
            sb.Append(_delimeterVertical);
            var groupIntend = _baseIntends.GroupNameIntend;
            sb.Append($"{groupName}");
            sb.Append(new string(' ', groupIntend - groupName.Length));
            sb.Append(_delimeterVertical);

            return null;
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
            log.Info(monitoringItem.GetJObject().ToString(Formatting.None));
        }
    }
}
