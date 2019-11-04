using Dapper.Web.Models;
using Dapper.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Service
{
    public class TestServiceImpl:ITestService
    {
        public IDapperRepository<Product> repository { get; set; }

        //public TestServiceImpl (IDapperRepository<Product> productRepository)
        //{
        //    repository = productRepository;
        //}

        public Task<IList<Product>> Query ()
        {
            return repository.QueryAsync();
        }
    }
}
