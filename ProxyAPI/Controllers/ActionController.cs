using System;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Monitoring.Attributes;
using Monitoring.Models;
using ProxyAPI.Monitoring;

namespace ProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase, ISetProvider
    {
        private ILogger _log = LogManager.GetCurrentClassLogger();
        private readonly ProxyAPIMonitoring _monitoring;
        private readonly Random _rnd;
        public StatisticsItemsFullSet StatisticsItemsFullSet { get; }
        public ActionController(ProxyAPIMonitoring monitoring, StatisticsItemsFullSet statisticsItemsFullSet)
        {
            _rnd = new Random();
            _monitoring = monitoring;
            StatisticsItemsFullSet = statisticsItemsFullSet;
        }

        [MethodMonitoring]
        //[ServiceFilter(typeof(MonitoringSendRequestFilterAttribute))]
        [HttpGet]
        public IActionResult SendDataToSMByGet()
        {
            _monitoring.BasicMonitoring.CountOfRequests++;
            if (_rnd.NextDouble() > 0.5)
            {
                _monitoring.BasicMonitoring.CountOfFailedRequests++;
                _monitoring.ExceptionMonitoring.CountOfExceptions++;

                throw new NotImplementedException();
            }

            return Ok();
        }
        
        [HttpPost]
        public IActionResult SendDataToSMByPost()
        {
            _monitoring.BasicMonitoring.CountOfRequests++;
            if (_rnd.NextDouble() > 0.5)
            {
                _monitoring.BasicMonitoring.CountOfFailedRequests++;
                _monitoring.ExceptionMonitoring.CountOfExceptions++;

                throw new NotImplementedException();
            }

            return Ok();
        }
    }
}
