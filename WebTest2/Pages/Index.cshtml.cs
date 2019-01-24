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
        //private readonly IGuidTransientAppService _guidTransientAppService; //#构造函数注入
        public IGuidTransientAppService GuidTransientAppService { get; set; }
        public IGuidScopedAppService GuidScopedAppService { get; set; }
        public IGuidSingletonAppService GuidSingletonAppService { get; set; }
        public string GuidItem1 { get; set; }
        public string GuidItem2 { get; set; }
        public string GuidItem3 { get; set; }

        //private readonly IGuidScopedAppService _guidScopedAppService;
        //private readonly IGuidSingletonAppService _guidSingletonAppService;
        public void OnGet()
        {
            GuidItem1 = GuidTransientAppService.GuidItem().ToString();
            GuidItem2 = GuidScopedAppService.GuidItem().ToString();
            GuidItem3 = GuidSingletonAppService.GuidItem().ToString();
        }
    }
}
