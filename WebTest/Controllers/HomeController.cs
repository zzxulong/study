using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebTest.Models;
using WebTest.Models.autofacTest;
using WebTest.Services.Users;

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
        public ILogger _logger { get; set; }
        public IUserService UserService { get; set; }
        public IGuidSingletonAppService GuidSingletonAppService { get; set; }

        public HomeController(IGuidTransientAppService guidTransientAppService,
            IGuidScopedAppService guidScopedAppService,
            IGuidSingletonAppService guidSingletonAppService,
            ILogger<HomeController> logger
            )
        {
            _guidTransientAppService = guidTransientAppService;
            _guidScopedAppService = guidScopedAppService;
            _guidSingletonAppService = guidSingletonAppService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogError("asdfasdfasdf");

            var a = UserService.Add(new DAL.User() {
                Active = true,
                AffiliateId = 1,
                CannotLoginUntilDateUtc = DateTime.Now,
                CreatedOnUtc = DateTime.Now,
                Deleted = false,
                Email = "123123",
                FailedLoginAttempts = 1,
                IsSystemAccount = true,
                LastActivityDateUtc = DateTime.Now,
                LastIpAddress = "aasdf",
                HasShoppingCartItems = false,
                IsTaxExempt = false,
                LastLoginDateUtc = DateTime.Now,
                RegisteredInStoreId = 1,
                RequireReLogin = false,
                SystemName = "long",
                UserGuid = new Guid(),
                UserName = "long",
                VendorId = 1,
            });

            var b = UserService.Get(1);

            var t = UserService.GetAll();


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
