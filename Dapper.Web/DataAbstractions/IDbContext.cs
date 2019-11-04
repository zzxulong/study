using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        IDbContextOptions Options { get; }

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection NewConnection (IDbTransaction transaction = null);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction ();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">隔离级别</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction (IsolationLevel isolationLevel);

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> Set<TEntity> () where TEntity : IEntity, new();
    }

    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContext<TDbContext> : IDbContext where TDbContext : IDbContext
    {

    }
}
