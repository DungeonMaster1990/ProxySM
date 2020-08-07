using Monitoring.Models;
using System;
using System.Reflection;
using MethodBoundaryAspect.Fody.Attributes;

namespace Monitoring.Attributes.BaseAttribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseMethodMonitoringAttribute : OnMethodBoundaryAspect
    {
        protected readonly StatisticsItemsFullSet _statisticsItemsFullSet;
        protected MethodBase _method;
        /// <summary>
        /// тип мониторингового объекта
        /// </summary>
        protected Type _itemType;

        public BaseMethodMonitoringAttribute(StatisticsItemsFullSet statisticsItemsFullSet)
        {
            _statisticsItemsFullSet = statisticsItemsFullSet;
        }

        internal new void OnEntry(MethodExecutionArgs args)
        {
            _method = args.Method;
            StatisticsMonitoringGroup<StatisticsMonitoringItemBase> group;
            var name = this.GetType().Name.Replace("Attribute", "");

            if (_statisticsItemsFullSet.Groups.ContainsKey(name))
            {
                group = _statisticsItemsFullSet.Groups[name];
                if (!group.MonitoringItems.ContainsKey(args.Method.Name))
                {
                    group.MonitoringItems[args.Method.Name] = CreateMonitoringItem();
                }
            }
            else
            {
                group = CreateGroup();
                _statisticsItemsFullSet.Groups[name] = group;
                group.MonitoringItems[args.Method.Name] = CreateMonitoringItem();
            }

            AfterEntry(args);
        }

        public abstract void AfterEntry(MethodExecutionArgs args);

        public abstract override void OnException(MethodExecutionArgs args);

        public abstract override void OnExit(MethodExecutionArgs args);

        public abstract StatisticsMonitoringGroup<T> CreateGroup<T>()
            where T : StatisticsMonitoringItemBase;
        //{
        //    if (!_itemType.IsAssignableFrom(typeof(StatisticsMonitoringItemBase)))
        //        throw new Exception("Type of monitoringItem should be inherited from base");

        //    Type dictType = typeof(ConcurrentDictionary<,>);
            
        //    Type[] genericTypesOfDictType = { typeof(string), _itemType };

        //    Type concurrentDictType = dictType.MakeGenericType(genericTypesOfDictType);
        //    var x = (ConcurrentDictionary<StatisticsMonitoringGroup, >)Activator.CreateInstance(concurrentDictType, new object[] { this.GetType().Name });

        //    return (StatisticsMonitoringGroup<StatisticsMonitoringItemBase>)
        //}

        private StatisticsMonitoringItemBase CreateMonitoringItem()
        {
            return (StatisticsMonitoringItemBase)Activator.CreateInstance(_itemType);
        }
    }
}
