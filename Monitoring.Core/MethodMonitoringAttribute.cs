using MethodBoundaryAspect.Fody.Attributes;
using Monitoring.Attributes.BaseAttribute;
using Monitoring.Models;
using System;
using System.Diagnostics;

namespace Monitoring.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodMonitoringAttribute : BaseMethodMonitoringAttribute
    {
        private MonitoringItemEntryCounter _monitoringItem;
        private Stopwatch _time;        
        public MethodMonitoringAttribute() 
            : base(typeof(MonitoringItemEntryCounter))
        {
            _time = new Stopwatch();
        }

        public override void AfterEntry(MethodExecutionArgs args)
        {
            _monitoringItem = _set.GetOrCreateGroupItem<MonitoringItemEntryCounter>(args.Method.Name, nameof(MethodMonitoringAttribute).Replace("Attribute", ""));
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
