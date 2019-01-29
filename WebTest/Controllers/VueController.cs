using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTest.DAL;
using WebTest.Models.autofacTest;

namespace WebTest.Controllers
{
    public class VueController : Controller
    {
        private NopMallContext _context;
        public VueController(NopMallContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Data = _context.User.ToList();
            return View();
        }
    }
}