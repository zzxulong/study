using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebTest2.Models.Services;

namespace WebTest2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGuidTransientAppService _guidTransientAppService; //#构造函数注入
        private readonly IGuidScopedAppService _guidScopedAppService;
        private readonly IGuidSingletonAppService _guidSingletonAppService;

        public IndexModel(IGuidTransientAppService guidTransientAppService,
           IGuidScopedAppService guidScopedAppService, IGuidSingletonAppService guidSingletonAppService)
        {
            _guidTransientAppService = guidTransientAppService;
            _guidScopedAppService = guidScopedAppService;
            _guidSingletonAppService = guidSingletonAppService;
        }

        public string GuidItem1 { get; set; }
        public string GuidItem2 { get; set; }
        public string GuidItem3 { get; set; }

        //private readonly IGuidScopedAppService _guidScopedAppService;
        //private readonly IGuidSingletonAppService _guidSingletonAppService;
        public void OnGet()
        {
            GuidItem1 = _guidTransientAppService.GuidItem().ToString();
            GuidItem2 = _guidScopedAppService.GuidItem().ToString();
            GuidItem3 = _guidSingletonAppService.GuidItem().ToString();
        }
    }
}
