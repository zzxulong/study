using Dapper.Web.DataAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Data.SqlServer
{
    public class DapperContext : DbContext
    {
        public DapperContext (IDbContextOptions options) 
        {
            base.Options = options;
        }
    }
}
