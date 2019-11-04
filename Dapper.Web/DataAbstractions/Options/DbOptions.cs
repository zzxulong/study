using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Options
{
    /// <summary>
    /// 数据库配置项
    /// </summary>
    public class DbOptions
    {
        /// <summary>
        /// 是否开启日志
        /// </summary>
        public bool Logging { get; set; }

        /// <summary>
        /// 数据库链接配置
        /// </summary>
        public List<DbConnectionOptions> Connections { get; set; }
    }
}
