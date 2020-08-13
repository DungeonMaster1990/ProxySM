using Monitoring.Models;
using System;
using System.Linq;
using System.Reflection;
using MethodBoundaryAspect.Fody.Attributes;

namespace Monitoring.Attributes.BaseAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseMethodMonitoringAttribute : OnMethodBoundaryAspect
    {
        protected readonly StatisticsItemsFullSet _set;
        protected MethodBase _method;
        protected StatisticsMonitoringGroup<IStatisticsMonitoringItem> _group;
        /// <summary>
        /// тип мониторингового объекта
        /// </summary>
        protected Type _itemType;

        public BaseMethodMonitoringAttribute(StatisticsItemsFullSet set, Type itemType)
        {
            _itemType = itemType;
            _set = set;
        }

        internal new void OnEntry(MethodExecutionArgs args)
        {
            var method = _set.GetType().GetMethods().First(x => x.Name == nameof(_set.GetOrCreateMonitoringGroup));
            var genericMethod = method.MakeGenericMethod(_itemType);
            var groupWrapped = genericMethod.Invoke(_set, new[] {this.GetType().Name});
            _group = (StatisticsMonitoringGroup<IStatisticsMonitoringItem>)groupWrapped;

            AfterEntry(args);
        }

        public abstract void AfterEntry(MethodExecutionArgs args);

        public abstract override void OnException(MethodExecutionArgs args);

        public abstract override void OnExit(MethodExecutionArgs args);
    }
}
