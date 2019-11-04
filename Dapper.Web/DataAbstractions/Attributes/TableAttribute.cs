using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Attributes
{
    /// <summary>
    /// 表名称，指定实体类在数据库中对应的表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指定实体类在数据库中对应的表名称
        /// </summary>
        /// <param name="tableName">表名</param>
        public TableAttribute (string tableName)
        {
            Name = tableName;
        }
    }
    /// <summary>
    /// 列名，指定实体属性在表中对应的列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">列名</param>
        public ColumnAttribute (string columnName)
        {
            Name = columnName;
        }
    }

    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }
}
