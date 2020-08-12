using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.ApiHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Services;
using Monitoring.Services.Sender;
using NLog;
using ProxyAPI.Monitoring;

namespace ProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private ILogger _log = LogManager.GetCurrentClassLogger();
        private readonly ProxyAPIMonitoring _monitoring;
        private readonly Random _rnd;
        private readonly IStatisticsSender _statSender;
        public ActionController(ProxyAPIMonitoring monitoring, StatisticsSender statSender)
        {
            _statSender = statSender;
            _rnd = new Random();
            _monitoring = monitoring;
        }

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
