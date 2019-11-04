using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Web.Models;
using Dapper.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
		//private readonly ProductRepository productRepository;
        public ITestService service { get; set; }
		public TestController(ITestService testService)
		{
            service = testService;
			//productRepository = new ProductRepository();
		}
		// GET: api/Test
		[HttpGet]
        public Task<IList<Product>> Get()
        {
            return service.Query();
			//return productRepository.GetAll();
		}

  //      // GET: api/Test/5
  //      [HttpGet("{id}", Name = "Get")]
  //      public Product Get(int id)
  //      {
		//	return productRepository.GetByID(id);
		//}

  //      // POST: api/Test
  //      [HttpPost]
  //      public void Post([FromBody] Product value)
  //      {
		//	if (ModelState.IsValid)
		//		          productRepository.Add(value);
		//}

  //      // PUT: api/Test/5
  //      [HttpPut("{id}")]
  //      public void Put(int id, [FromBody] string value)
  //      {
  //      }

  //      // DELETE: api/ApiWithActions/5
  //      [HttpDelete("{id}")]
  //      public void Delete(int id)
  //      {
		//	productRepository.Delete(id);
		//}
    }
}
