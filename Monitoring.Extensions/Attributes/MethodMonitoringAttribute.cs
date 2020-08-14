using MethodBoundaryAspect.Fody.Attributes;
using Monitoring.Attributes.BaseAttribute;
using Monitoring.Models;
using Monitoring.Services;
using System;
using System.Diagnostics;

namespace Monitoring.Attributes
{
    /// <summary>
    /// Мониторинговый атрибут для методов 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodMonitoringAttribute : BaseMethodMonitoringAttribute
    {
        /// <summary>
        /// Мониторинговый item - счетчик
        /// </summary>
        private MonitoringItemEntryCounter _monitoringItem;        
        
        /// <summary>
        /// Таймер времени выполнения метода
        /// </summary>
        private Stopwatch _time;        
        public MethodMonitoringAttribute() 
            : base(typeof(MonitoringItemEntryCounter))
        {
            _time = new Stopwatch();
        }

        /// <summary>
        /// Метод, выполняемый внутри метода OnEntry(вход в атрибут перед исполнением метода) после инициализации
        /// </summary>
        /// <param name="args"></param>
        public override void AfterEntry(MethodExecutionArgs args)
        {
            _monitoringItem = _set.GetOrCreateGroupItem<MonitoringItemEntryCounter>(args.Method.Name, nameof(MethodMonitoringAttribute).Replace("Attribute", ""));
            _monitoringItem.Entries++;
            _time.Start();
        }

        /// <summary>
        /// Метод, выполняемый после выхода из метода
        /// </summary>
        /// <param name="args"></param>
        public override void OnExit(MethodExecutionArgs args)
        {
            _monitoringItem.Exits++;
            _time.Stop();
            _monitoringItem.AverageExecutionTime.Add(_time.Elapsed);
        }

        /// <summary>
        /// В случае ошибки
        /// </summary>
        /// <param name="args"></param>
        public override void OnException(MethodExecutionArgs args)
        {
            _monitoringItem.Errors++;
        }
    }
}
