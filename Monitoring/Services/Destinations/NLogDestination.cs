using Monitoring.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Monitoring.Services
{
    class LogDestination : BaseDestination
    {
        private static ILogger _log = LogManager.GetLogger("Statistics");

        private const char _delimeterHoriontal = '-';
        private const char _delimeterVertical = '|';
        private IDictionary<string, List<int>> _dynamicGroupPropertiesIntends;
        private IDictionary<string, List<int>> _monitoringItemsPropertiesIntends;
        private int _baseNameIntend;


        public LogDestination(IEnumerable<MonitoringItemBase> monitoringItems, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups) : base(monitoringItems, dynamicGroups)
        {
            monitoringItems = _monitoringItems;
            dynamicGroups = _dynamicGroups;
        }

        private (int maxLengthGroupName, IDictionary<string, List<int>> dynamicGroupsIntends) CalculateIntendesDynamicGroups()
        {
            var intends = _dynamicGroups.ToDictionary(x => x.Name, x => x.MonitoringItems.First().Properties.Keys.Select(y => y.Length).ToList());

            var maxGroupNameLength = _dynamicGroups.Max(x => x.Name.Length);

            return (maxGroupNameLength, intends);
        }

        private (int maxLengthItemName, IDictionary<string, List<int>> monitoringItemsPropertiesIntends) CalculateBasicIntendes()
        {
            var intends = _monitoringItems.ToDictionary(x => x.Name, x => x.Properties.Keys.Select(y => y.Length).ToList());
            var maxNameLength = _monitoringItems.Max(x => x.Name.Length);
            return (maxNameLength, intends);
        }

        private void CalculateIntends()
        {
            var (maxLengthGroupName, dynamicGroupsIntends) = CalculateIntendesDynamicGroups();
            var (maxLengthItemName, monitoringItemsPropertiesIntends) = CalculateBasicIntendes();
            _baseNameIntend = Math.Max(maxLengthGroupName, maxLengthItemName);
            _dynamicGroupPropertiesIntends = dynamicGroupsIntends;
            _monitoringItemsPropertiesIntends = monitoringItemsPropertiesIntends;
        }

        public override void Send()
        {
            if (_dynamicGroups.Any(x => x.CheckAdded()))
                CalculateIntends();


            var body = BuildLogBody(items, dynamicGroups);
            _log.Info($"Statistics \n" + body);
        }

        public string BuildLogBody(IEnumerable<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
        {

            var sb = new StringBuilder();

            sb.AppendLine(_delimeter);

            foreach (var group in groups)
            {

                sb.Append($"{group.Key.Name, }")
            }
            foreach (var item in items)
            {
                item.Properties
            }
        }
    }
}
