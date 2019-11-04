using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Entities;
using Dapper.Web.DataAbstractions.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Web.Data
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract class DbContext : IDbContext
    {
        #region ==属性==

        /// <summary>
        /// 数据库上下文配置项
        /// </summary>
        public IDbContextOptions Options { get; set; }

        #endregion

        #region ==构造函数==

        //protected DbContext (IDbContextOptions options)
        //{
        //    Options = options;
        //}

        #endregion

        #region ==方法==

        public IDbConnection NewConnection (IDbTransaction transaction = null)
        {
            if (transaction != null)
                return transaction.Connection;

            var conn = Options.NewConnection();

            //SQLite跨数据库访问需要附加
            if (Options.SqlAdapter.SqlDialect ==  SqlDialect.SQLite)
            {
                conn.Open();

                var sql = new StringBuilder();
                foreach (var c in Options.DbOptions.Connections)
                {
                    var connString = "";
                    foreach (var param in c.ConnString.Split(';'))
                    {
                        var temp = param.Split('=');
                        var key = temp[0];
                        if (key.Equals("Data Source", StringComparison.OrdinalIgnoreCase) ||
                            key.Equals("DataSource", StringComparison.OrdinalIgnoreCase))
                        {
                            connString = temp[1];
                            break;
                        }
                    }

                    sql.AppendFormat("ATTACH DATABASE '{0}' as '{1}';", connString, conn.Database);
                }

                conn.ExecuteAsync(sql.ToString());
            }

            return conn;
        }

        public IDbTransaction BeginTransaction ()
        {
            if (Options.SqlAdapter.SqlDialect ==  SqlDialect.SQLite)
                return null;

            var conn = NewConnection();
            conn.Open();
            return conn.BeginTransaction();
        }

        public IDbTransaction BeginTransaction (IsolationLevel isolationLevel)
        {
            if (Options.SqlAdapter.SqlDialect ==  SqlDialect.SQLite)
                return null;

            var conn = NewConnection();
            conn.Open();
            return conn.BeginTransaction(isolationLevel);
        }

        public IDbSet<TEntity> Set<TEntity> () where TEntity : IEntity, new()
        {
            return new DbSet<TEntity>(this);
        }

        #endregion
    }
}