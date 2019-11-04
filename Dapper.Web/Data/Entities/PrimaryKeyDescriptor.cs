﻿using Dapper.Web.DataAbstractions.Entities;
using Dapper.Web.DataAbstractions.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Web.Data.Entities
{
    public class PrimaryKeyDescriptor : IPrimaryKeyDescriptor
    {
        public string Name { get; }
        public PrimaryKeyType Type { get; }
        public PropertyInfo PropertyInfo { get; }

        public PrimaryKeyDescriptor (PropertyInfo p)
        {
            PropertyInfo = p;
            var columnAttribute = p.GetCustomAttribute<ColumnAttribute>();
            Name = columnAttribute != null ? columnAttribute.Name : p.Name;

            if (p.PropertyType == typeof(int))
            {
                Type = PrimaryKeyType.Int;
            }
            else if (p.PropertyType == typeof(long))
            {
                Type = PrimaryKeyType.Long;
            }
            else if (p.PropertyType == typeof(Guid))
            {
                Type = PrimaryKeyType.Guid;
            }
            else if (p.PropertyType == typeof(string))
            {
                Type = PrimaryKeyType.String;
            }
            else
            {
                throw new ArgumentException("无效的主键类型", nameof(p.PropertyType));
            }
        }

        public PrimaryKeyDescriptor ()
        {
            Type = PrimaryKeyType.NoPrimaryKey;
        }

        public bool Is (PrimaryKeyType type)
        {
            return Type == type;
        }

        public bool IsNo ()
        {
            return Type == PrimaryKeyType.NoPrimaryKey;
        }

        public bool IsInt ()
        {
            return Type == PrimaryKeyType.Int;
        }

        public bool IsLong ()
        {
            return Type == PrimaryKeyType.Long;
        }

        public bool IsGuid ()
        {
            return Type == PrimaryKeyType.Guid;
        }
    }
}
