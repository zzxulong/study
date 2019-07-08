using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace formauthroize
{
	public class MyAuthorizeAttribute :AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if(filterContext.HttpContext.User.Identity.Name != "xulong")
			{
				filterContext.HttpContext.Response.Write("您不是vip用户，不能访问机密数据");
				filterContext.HttpContext.Response.End();
				return;
			}
			base.OnAuthorization(filterContext);
		}
	}
}