using Dapper.Web.DataAbstractions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions
{

    /// <summary>
    /// 数据库配置项
    /// </summary>
    public interface IDbContextOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 数据库适配器
        /// </summary>
        ISqlAdapter SqlAdapter { get; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <returns></returns>
        IDbConnection NewConnection ();

        /// <summary>
        /// 所有数据库配置信息
        /// </summary>
        DbOptions DbOptions { get; }
    }
}
