using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.SqlQueryable.GroupByQueryable
{
    /// <summary>
    /// 分组查询对象
    /// </summary>
    public interface INetSqlGrouping1<out TKey, TEntity> : INetSqlGrouping<TKey> where TEntity : IEntity
    {
        TResult Max<TResult> (Expression<Func<TEntity, TResult>> where);

        TResult Min<TResult> (Expression<Func<TEntity, TResult>> where);

        TResult Sum<TResult> (Expression<Func<TEntity, TResult>> where);

        TResult Avg<TResult> (Expression<Func<TEntity, TResult>> where);
    }
}
