using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper.Web.Models;
using Dapper.Web.Repository;
using Dapper.Web.Service;

namespace Dapper.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ProductRepository productRepository;
        public ITestService service { get; set; }
        public HomeController()
		{
			productRepository = new ProductRepository();
		}
		public IActionResult Index()
		{
            var t=service.Query();
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
