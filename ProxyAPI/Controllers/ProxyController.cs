using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPI.Controllers
{
    public class ProxyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
