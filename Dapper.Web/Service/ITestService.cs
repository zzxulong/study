using Dapper.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Service
{
    public interface ITestService
    {
        Task<IList<Product>> Query ();
    }
}
