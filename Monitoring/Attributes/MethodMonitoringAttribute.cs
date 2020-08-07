using MethodBoundaryAspect.Fody.Attributes;
using Monitoring.Attributes.BaseAttribute;
using Monitoring.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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

        public void OnEntry(MethodExecutionArgs args)
        {
            _method = args.Method;
            _monitoringItem.Entries++;
            _monitoringItem.Watcher.Start();
        }
        public override void OnExit(MethodExecutionArgs args)
        {
            _monitoringItem.Exits++;
            _monitoringItem.Watcher.Stop();
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _monitoringItem.Errors++;
            _monitoringItem.Watcher.Stop();
        }

        public override void CreateGroup()
        {
        }
    }
}
