﻿using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Entities;
using Dapper.Web.DataAbstractions.SqlQueryable.GroupByQueryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Web.Data.SqlQueryable
{
    internal class GroupByQueryable1<TKey, TEntity> : GroupByQueryableAbstract, IGroupByQueryable1<TKey, TEntity> where TEntity : IEntity
    {
        public GroupByQueryable1 (IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression) : base(db, queryBody, queryBuilder, expression)
        {
        }

        public IGroupByQueryable1<TKey, TEntity> Having (Expression<Func<INetSqlGrouping1<TKey, TEntity>, bool>> expression)
        {
            SetHaving(expression);
            return this;
        }

        public IGroupByQueryable1<TKey, TEntity> OrderBy (string customOrderBy)
        {
            SetOrderBy(customOrderBy);
            return this;
        }

        public IGroupByQueryable1<TKey, TEntity> OrderByDescending (string customOrderBy)
        {
            SetOrderByDescending(customOrderBy);
            return this;
        }

        public IGroupByQueryable1<TKey, TEntity> OrderBy<TResult> (Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression)
        {
            SetOrderBy(expression);
            return this;
        }

        public IGroupByQueryable1<TKey, TEntity> OrderByDescending<TResult> (Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression)
        {
            SetOrderByDescending(expression);
            return this;
        }

        public IGroupByQueryable1<TKey, TEntity> Select<TResult> (Expression<Func<INetSqlGrouping1<TKey, TEntity>, TResult>> expression)
        {
            SetSelect(expression);
            return this;
        }
    }
}
