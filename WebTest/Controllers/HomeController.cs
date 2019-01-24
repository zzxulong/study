using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTest.Models;
using WebTest.Models.autofacTest;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGuidTransientAppService _guidTransientAppService; //#构造函数注入
        private readonly IGuidScopedAppService _guidScopedAppService;
        private readonly IGuidSingletonAppService _guidSingletonAppService;
        //接口注入
        public IGuidTransientAppService GuidTransientAppService { get; set; }
        public IGuidScopedAppService GuidScopedAppService { get; set; }
        public IGuidSingletonAppService GuidSingletonAppService { get; set; }

        public HomeController(IGuidTransientAppService guidTransientAppService,
            IGuidScopedAppService guidScopedAppService, IGuidSingletonAppService guidSingletonAppService)
        {
            _guidTransientAppService = guidTransientAppService;
            _guidScopedAppService = guidScopedAppService;
            _guidSingletonAppService = guidSingletonAppService;
        }

        public IActionResult Index()
        {
          
            ViewBag.TransientItem = GuidTransientAppService.GuidItem();
            ViewBag.ScopedItem = GuidScopedAppService.GuidItem();
            ViewBag.SingletonItem = GuidSingletonAppService.GuidItem();

            ViewBag.TransientItem1 = GuidTransientAppService.GuidItem();
            ViewBag.ScopedItem1 = GuidScopedAppService.GuidItem();
            ViewBag.SingletonItem1 = GuidSingletonAppService.GuidItem();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
