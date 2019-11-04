﻿using Dapper.Web.Core.Extentions;
using Dapper.Web.Data.Entities;
using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Attributes;
using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Web.Data
{
    /// <summary>
    /// 实体描述
    /// </summary>
    public class EntityDescriptor : IEntityDescriptor
    {
        #region ==属性==

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; }

        /// <summary>
        /// 列
        /// </summary>
        public IColumnDescriptorCollection Columns { get; private set; }

        /// <summary>
        /// 主键
        /// </summary>
        public IPrimaryKeyDescriptor PrimaryKey { get; private set; }

        /// <summary>
        /// 是否包含软删除
        /// </summary>
        public bool SoftDelete { get; }

        public EntitySql Sql { get; }

        public ISqlAdapter SqlAdapter { get; }

        public bool IsEntityBase { get; }

        #endregion

        #region ==构造器==

        public EntityDescriptor (Type entityType, ISqlAdapter sqlAdapter, IEntitySqlBuilder sqlBuilder)
        {
            SqlAdapter = sqlAdapter;

            EntityType = entityType;

            Database = sqlAdapter.Database;

            PrimaryKey = new PrimaryKeyDescriptor();
            
            SetTableName();

            SetColumns();

            Sql = sqlBuilder.Build(this);

            //IsEntityBase = EntityType.IsSubclassOfGeneric(typeof(EntityBase<>)) || EntityType.IsSubclassOfGeneric(typeof(EntityBaseWithSoftDelete<,>));
        }

        #endregion

        #region ==私有方法==

        /// <summary>
        /// 设置表名
        /// </summary>
        private void SetTableName ()
        {
            var tableArr = EntityType.GetCustomAttribute<TableAttribute>(false);
            TableName = tableArr != null ? tableArr.Name : EntityType.Name;
        }

        /// <summary>
        /// 设置属性列表
        /// </summary>
        private void SetColumns ()
        {
            Columns = new ColumnDescriptorCollection();

            //加载属性列表
            var properties = new List<PropertyInfo>();
            foreach (var p in EntityType.GetProperties())
            {
                var type = p.PropertyType;
                if ((!type.IsGenericType || type.IsNullable()) && (type == typeof(Guid) || type.IsNullable() || Type.GetTypeCode(type) != TypeCode.Object)
                    && Attribute.GetCustomAttributes(p).All(attr => attr.GetType() != typeof(IgnoreAttribute)))
                {
                    properties.Add(p);
                }
            }

            foreach (var p in properties)
            {
                var column = new ColumnDescriptor(p);
                if (column.IsPrimaryKey)
                {
                    PrimaryKey = new PrimaryKeyDescriptor(p);
                    Columns.Insert(0, column);
                }
                else
                {
                    Columns.Add(column);
                }
            }
        }

        #endregion
    }
}
