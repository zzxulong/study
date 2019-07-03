using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vuetest.Models;

namespace vuetest.Controllers
{
	/// <summary>
	/// value
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "Admin")]
	public class ValuesController :ControllerBase
	{
		/// <summary>
		/// 获取全部
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1","value2" };
		}
		/// <summary>
		/// 获取
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(Love love)
		{
			return "value";
		}

		/// <summary>
		/// post
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] Love value)
		{
		}

		/// <summary>
		/// put
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id,[FromBody] string value)
		{
		}

		/// <summary>
		/// del
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
