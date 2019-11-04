using Dapper.Web.DataAbstractions.Attributes;
using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Web.Core.Extentions
{

    public class ColumnDescriptor : IColumnDescriptor
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; }

        public ColumnDescriptor (PropertyInfo property)
        {
            if (property == null)
                return;

            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
            Name = columnAttribute != null ? columnAttribute.Name : property.Name;
            PropertyInfo = property;

            IsPrimaryKey = Attribute.GetCustomAttributes(property).Any(attr => attr.GetType() == typeof(KeyAttribute));

            if (!IsPrimaryKey)
            {
                IsPrimaryKey = property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
