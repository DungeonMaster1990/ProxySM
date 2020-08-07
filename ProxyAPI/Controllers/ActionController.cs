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
        private readonly Monitoring _monitoring;
        public ActionController(IWebRequestHelper helper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult SendDataToSMByGet(object data)
        {
            _monitoring.ProxyAPIMonitoringItem
            //_httpContextAccessor.HttpContext.Request.Cookies["mock"]
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult SendDataToSMByPost(object data)
        {
            throw new NotImplementedException();
        }
    }
}
