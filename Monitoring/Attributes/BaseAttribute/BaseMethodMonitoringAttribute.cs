using Monitoring.Models;
using System;
using System.Reflection;
using MethodBoundaryAspect.Fody.Attributes;

namespace Monitoring.Attributes.BaseAttribute
{
    /// <summary>
    /// Базовый абстрактный атрибут мониторинга методов
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseMethodMonitoringAttribute : OnMethodBoundaryAspect
    {
        protected StatisticsItemsFullSet _set;
        protected MethodBase _method;
        /// <summary>
        /// тип мониторингового объекта
        /// </summary>
        protected Type _itemType;

        public BaseMethodMonitoringAttribute(Type itemType)
        {
            _itemType = itemType;
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            _set = ((ISetProvider)args.Instance).StatisticsItemsFullSet;
            AfterEntry(args);
        }

        public abstract void AfterEntry(MethodExecutionArgs args);
    }
}
