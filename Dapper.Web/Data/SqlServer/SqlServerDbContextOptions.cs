using Dapper.Web.DataAbstractions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Data.SqlServer
{
    /// <summary>
    /// 数据库上下文配置项SqlServer实现
    /// </summary>
    public class SqlServerDbContextOptions : DbContextOptionsAbstract
    {
        public SqlServerDbContextOptions (DbOptions dbOptions, DbConnectionOptions options) : base(dbOptions, options, new SqlServerAdapter(options))
        {
        }

        public override IDbConnection NewConnection ()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}