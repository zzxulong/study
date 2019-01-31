using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreDbAutoFacTest.Models;
using EFCoreDbAutoFacTest.Service.Users;
using EFCoreDbAutoFacTest.Models.Users;

namespace EFCoreDbAutoFacTest.Controllers
{
    public class HomeController : Controller
    {
        public IUserService UserService { get; set; }
        public IActionResult Index()
        {
            var a = UserService.Add(new User()
            {
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

            var b = UserService.Get(5);

            var t = UserService.GetAll();

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
