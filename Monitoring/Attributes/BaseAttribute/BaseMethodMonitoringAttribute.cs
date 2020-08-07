using Monitoring.Models;
using System;
using System.Reflection;
using MethodBoundaryAspect.Fody.Attributes;
using System.Collections.Generic;

namespace Monitoring.Attributes.BaseAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseMethodMonitoringAttribute : OnMethodBoundaryAspect
    {
        protected readonly StatisticsItemsFullSet _statisticsItemsFullSet;
        protected MethodBase _method;
        protected StatisticsMonitoringGroup<StatisticsMonitoringItemBase> _group;
        protected Type _itemType;

        public BaseMethodMonitoringAttribute(StatisticsItemsFullSet statisticsItemsFullSet, Type itemType)
        {
            _statisticsItemsFullSet = statisticsItemsFullSet;
            _itemType = itemType;
        }

        public virtual void OnEntry(MethodExecutionArgs args)
        {
            if (_statisticsItemsFullSet.Groups.ContainsKey(this.GetType().FullName))
                _group = _statisticsItemsFullSet.Groups[this.GetType().FullName];
            else
                CreateGroup();
        }

        public virtual void OnException(MethodExecutionArgs args)
        { }

        public virtual void OnExit(MethodExecutionArgs args)
        { }

        public virtual void CreateGroup()
        {
            var item = Activator.CreateInstance(_itemType);
            Type generic = typeof(Dictionary<,>);           
            Type[] typeArgs = { typeof(string), _itemType };
            Type constructed = generic.MakeGenericType(typeArgs);
            _group = (StatisticsMonitoringGroup<StatisticsMonitoringItemBase>)Activator.CreateInstance(constructed, new object[] { this.GetType().Name, item });
        }
    }
}
