using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Repository
{
    public interface IDapperRepository<TEntity> : IBaseRepository<TEntity> where TEntity: IEntity,new()
    {
    }
}
