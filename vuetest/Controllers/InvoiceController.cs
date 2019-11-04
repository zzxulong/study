using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vuetest.Controllers
{
	public class InvoiceController :Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
