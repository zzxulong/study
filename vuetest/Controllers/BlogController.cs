using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vuetest.Core.IService;
using vuetest.Core.Models;
using vuetest.Core.Service;
using vuetest.Core.ViewModel;

namespace vuetest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	//[Authorize(Policy = "Admin")]
	public class BlogController : ControllerBase
    {
		public IAdvertisementServices advertisementServices { get; set; }
		// GET: api/Blog
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//    return new string[] { "value1", "value2" };
		//}

		// GET: api/Blog/5
		[HttpGet("{id}",Name = "Get")]
		public async Task<AdvertisementViewModel> Get(int id)
        {
			return await advertisementServices.Get(id);
		}

        // POST: api/Blog
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
