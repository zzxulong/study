﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ssoclent2.Controllers
{
	public class HomeController :Controller
	{
		public static List<string> Tokens = new List<string>();
		public async Task<ActionResult> Index()
		{
			var tokenId = Request.QueryString["tokenId"];
			//如果tokenId不为空，则是由Service302过来的。
			if(tokenId != null)
			{
				using(HttpClient http = new HttpClient())
				{
					//验证Tokend是否有效
					var isValid = await http.GetStringAsync("http://localhost:16542/Home/TokenIdIsValid?tokenId=" + tokenId);
					if(bool.Parse(isValid.ToString()))
					{
						if(!Tokens.Contains(tokenId))
						{
							//记录登录过的Client (主要是为了可以统一登出)
							Tokens.Add(tokenId);
						}
						Session["token"] = tokenId;
					}
				}
			}
			//判断是否是登录状态
			if(Session["token"] == null || !Tokens.Contains(Session["token"].ToString()))
			{
				return Redirect("http://localhost:16542/Home/Verification?backUrl=http://localhost:30929/Home");
			}
			else
			{
				if(Session["token"] != null)
					Session["token"] = null;
			}
			return View();
		}

		public void ClearToken(string tokenId)
		{
			Tokens.Remove(tokenId);
		}
	}
}