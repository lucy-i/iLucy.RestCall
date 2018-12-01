using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iLucy.RestCall.Helpers;
using iLucy.RestCall.TestApiIntegration;
using Microsoft.AspNetCore.Mvc;

namespace iLucy.TestMVC.Controllers
{
    public class SampleController : Controller
    {
        SimpleApiIntegration _integration = new SimpleApiIntegration();
        public async Task<IActionResult> Index()
        {
            string[] stringList=await _integration.ExecuteFunc<SimpleApiIntegration, string[]>(t=>t.model);
            return View(stringList);
        }
    }
}