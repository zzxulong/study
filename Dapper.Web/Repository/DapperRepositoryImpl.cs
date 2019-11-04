using Dapper.Web.Data.SqlServer;
using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Repository
{
    public class DapperRepositoryImpl<TEntity>: BaseRepositoryImpl<TEntity>,IDapperRepository<TEntity> where TEntity:IEntity,new()
    {
        public DapperRepositoryImpl (DapperContext dbContext)
        {
            base.DbContext = DbContext;
        }
    }
}
