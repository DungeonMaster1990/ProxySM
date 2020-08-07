using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Monitoring.Models
{
    public class StatisticsItemsFullSet
    {
        public readonly IDictionary<string, StatisticsMonitoringItemBase> Items;
        public readonly IDictionary<string, StatisticsMonitoringDynamicGroup<StatisticsMonitoringItemBase>> DynamicGroups;

        public StatisticsItemsFullSet(IDictionary<string, StatisticsMonitoringItemBase> items,
            IDictionary<string, StatisticsMonitoringDynamicGroup<StatisticsMonitoringItemBase>> dynamicGroups)
        {
            Items = items;
            DynamicGroups = dynamicGroups;
        }

        public int GetHashCode()
        {
            return GenerateHash(Items.Select(x => x.Key))
                   + GenerateHash(DynamicGroups.Select(x => x.Key))
                   + GenerateHash(DynamicGroups.SelectMany(x => x.Value.MonitoringItems.Keys));
        }

        private static int GenerateHash(IEnumerable<string> values)
        {
            int hashcode = 0;
            foreach (string value in values)
            {
                if (value != null)
                    hashcode += value.GetHashCode();
            }
            return hashcode;
        }
    }
}
