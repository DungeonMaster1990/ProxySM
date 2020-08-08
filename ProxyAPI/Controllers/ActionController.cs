using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.ApiHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Services;
using NLog;
using ProxyAPI.Monitoring;

namespace ProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private ILogger _log = LogManager.GetCurrentClassLogger();
        private IWebRequestHelper _helper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProxyAPIMonitoring _monitoring;
        private readonly Random _rnd;
        public ActionController(IWebRequestHelper helper, ProxyAPIMonitoring monitoring)
        {
            _rnd = new Random();
            _monitoring = monitoring;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult SendDataToSMByGet(object data)
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
        public IActionResult SendDataToSMByPost(object data)
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
