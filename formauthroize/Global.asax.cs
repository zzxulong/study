using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace formauthroize
{
	public class MvcApplication :System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		protected void Application_AuthenticateRequest()
		{
			GetUserInfo();
		}

		public void GetUserInfo()
		{
			// 1. ����¼Cookie
			HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

			try
			{
				UserInfo userData = null;
				// 2. ����Cookieֵ����ȡFormsAuthenticationTicket����
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

				if(ticket != null && string.IsNullOrEmpty(ticket.UserData) == false)
					// 3. ��ԭ�û�����
					userData = JsonConvert.DeserializeObject<UserInfo>(ticket.UserData);

				if(ticket != null && userData != null)
					// 4. �������ǵ�MyFormsPrincipalʵ�������¸�context.User��ֵ��
					HttpContext.Current.User = new MyFormsPrincipal<UserInfo>(ticket,userData);
			}
			catch { /* ���쳣Ҳ��Ҫ�׳�����ֹ��������̽�� */ }
		}
	}
}
