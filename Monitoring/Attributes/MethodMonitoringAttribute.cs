using MethodBoundaryAspect.Fody.Attributes;
using Monitoring.Attributes.BaseAttribute;
using Monitoring.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Monitoring.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodMonitoringAttribute : BaseMethodMonitoringAttribute
    {
        MonitoringItemEntryCounter _monitoringItem;
        Stopwatch _time;
        public MethodMonitoringAttribute(StatisticsItemsFullSet statisticsItemsFullSet) 
            : base(statisticsItemsFullSet, typeof(MonitoringItemEntryCounter))
        {
        }

        public override void AfterEntry(MethodExecutionArgs args)
        {
            _monitoringItem = (MonitoringItemEntryCounter)_group.GetOrAddItem(args.Method.Name);
            _monitoringItem.Entries++;
            _time.Start();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _monitoringItem.Exits++;
            _time.Stop();
            _monitoringItem.AverageExecutionTime.Add(_time.Elapsed);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _monitoringItem.Errors++;
        }
    }
}
