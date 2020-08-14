namespace ProxyAPI.Monitoring
{
    public class ProxyAPIMonitoring
    {
        public ProxyAPIExceptionMonitoringItem ExceptionMonitoring { get;}
        public ProxyAPIMonitoringItem BasicMonitoring { get; }

        public ProxyAPIMonitoring(ProxyAPIExceptionMonitoringItem exceptionMonitoring,
            ProxyAPIMonitoringItem basicMonitoring)
        {
            ExceptionMonitoring = exceptionMonitoring;
            BasicMonitoring = basicMonitoring;
        }
    }
}
