using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.ApiHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace ProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private ILogger _log = LogManager.
        private IWebRequestHelper _helper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ActionController(IWebRequestHelper helper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult SendDataToSMByGet(object data)
        {
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
