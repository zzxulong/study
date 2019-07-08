using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1_api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ValuesController :Controller
	{
		/// <summary>
		/// 这里可能是私密相册什么的啦（如 QQ相册、腾讯微博内容。。。）
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>        
		public IActionResult Get(string str)
		{
			return new JsonResult(from c in User.Claims select new { c.Type,c.Value });
			//return str;
		}
	}
}
