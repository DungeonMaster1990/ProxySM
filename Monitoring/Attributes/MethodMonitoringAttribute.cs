using MethodBoundaryAspect.Fody.Attributes;
using Monitoring.Attributes.BaseAttribute;
using Monitoring.Models;
using System;

namespace Monitoring.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodMonitoringAttribute : BaseMethodMonitoringAttribute
    {
        MonitoringItemEntryCounter _monitoringItem;
        public MethodMonitoringAttribute( StatisticsItemsFullSet statisticsItemsFullSet, Type itemType) 
            : base(statisticsItemsFullSet, itemType)
        {
        }

        public override void AfterEntry(MethodExecutionArgs args)
        {
            
            _monitoringItem.Entries++;
            _monitoringItem.Watcher.Start();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _monitoringItem.Exits++;
            _monitoringItem.Watcher.Stop();
        }

        public override StatisticsMonitoringGroup<T> CreateGroup<T>()
        {
            throw new NotImplementedException();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _monitoringItem.Errors++;
            _monitoringItem.Watcher.Stop();
        }
    }
}
