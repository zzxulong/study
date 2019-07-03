using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vuetest.AuthHelper;

namespace vuetest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
		[HttpGet]

		[Route("Token")]

		public JsonResult GetJWTStr(long id = 1,string sub = "Admin",int expiresSliding = 30,int expiresAbsoulute = 30)

		{

			TokenModel tokenModel = new TokenModel();

			tokenModel.Uid = id;

			tokenModel.Sub = sub;

			DateTime d1 = DateTime.Now;

			DateTime d2 = d1.AddMinutes(expiresSliding);

			DateTime d3 = d1.AddDays(expiresAbsoulute);

			TimeSpan sliding = d2 - d1;

			TimeSpan absoulute = d3 - d1;

			//string jwtStr = BlogCoreToken.IssueJWT(tokenModel,sliding,absoulute);
			string jwtStr = JwtHelper.IssueJWT(new TokenModelJWT() { Role = "Admin",Uid = 1 });
			return new JsonResult(jwtStr);

		}
	}
}