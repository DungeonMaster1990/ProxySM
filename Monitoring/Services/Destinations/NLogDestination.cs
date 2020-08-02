using Monitoring.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitoring.Services
{
    class NLogDestination : IDestination
    {
        private static ILogger _log = LogManager.GetLogger("Statistics");
        
        private const string _delimeter = "---------------------------------------------------------------------------------";
        private IDictionary<string, List<int>> _dynamicGroupPropertiesIntends;
        private IDictionary<string, int> _monitoringItemsPropertiesIntends;
        private int _typeIndent;

        public NLogDestination()
        {
            _dynamicGroupPropertiesIntends = new Dictionary<string, List<int>>();

        }

        private void CalculateIntendes(IEnumerable<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
        {
            var groups = dynamicGroups.GroupBy(x => x.MonitoringGroup.Values.GetType().Name, x=>x.MonitoringGroup);

            var maxTypeName = Math.Max(groups.Max(x => x.Key.Length), items.Max(x => x.GetType().Name.Length));
            var typeIndent = maxTypeName + 5;

            _dynamicGroupPropertiesIntends = groups.ToDictionary(x=>x.Key, x => x.First().MonitoringGroup.Values.First().Properties.Select(y => y.Key.Length).ToList());


            var propertiesIntents = new List<int>();
        }

        public void Send(IEnumerable<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
        {

        }

        

        public string BuildLogBody(IList<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
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
