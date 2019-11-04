using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Entities
{
    /// <summary>
    /// 实体Sql生成器
    /// </summary>
    public interface IEntitySqlBuilder
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        EntitySql Build (IEntityDescriptor descriptor);
    }
}
