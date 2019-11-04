using Dapper.Web.Core.Extentions;
using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Entities;
using Dapper.Web.DataAbstractions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Data
{

    public abstract class DbContextOptionsAbstract : IDbContextOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbOptions"></param>
        /// <param name="options"></param>
        /// <param name="sqlAdapter">数据库适配器</param>
        /// <param name="loggerFactory">日志工厂</param>
        /// <param name="loginInfo">登录信息</param>
        protected DbContextOptionsAbstract (DbOptions dbOptions, DbConnectionOptions options, ISqlAdapter sqlAdapter)
        {
            if (options.Name.IsNull())
                throw new ArgumentNullException(nameof(options.Name), "数据库连接名称未配置");

            if (options.ConnString.IsNull())
                throw new ArgumentNullException(nameof(options.ConnString), "数据库连接字符串未配置");

            DbOptions = dbOptions;
            Name = options.Name;
            ConnectionString = options.ConnString;
            SqlAdapter = sqlAdapter;

            if (options.EntityTypes != null && options.EntityTypes.Any())
            {
                foreach (var entityType in options.EntityTypes)
                {
                    EntityDescriptorCollection.Add(new EntityDescriptor(entityType, sqlAdapter, new EntitySqlBuilder()));
                }
            }
        }

        public string Name { get; }

        public ISqlAdapter SqlAdapter { get; }

        public string ConnectionString { get; }

        public abstract IDbConnection NewConnection ();
        public DbOptions DbOptions { get; }
    }
}
