﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Entities
{
    /// <summary>
    /// 实体信息集合
    /// </summary>
    public class EntityDescriptorCollection
    {
        private static readonly List<IEntityDescriptor> List = new List<IEntityDescriptor>();

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entityDescriptor"></param>
        public static void Add (IEntityDescriptor entityDescriptor)
        {
            if (List.All(m => m.EntityType != entityDescriptor.EntityType))
                List.Add(entityDescriptor);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IEntityDescriptor Get<TEntity> () where TEntity : IEntity, new()
        {
            var entity = List.FirstOrDefault(m => m.EntityType == typeof(TEntity));
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(TEntity), "实体不存在");
            }

            return entity;
        }
    }
}
