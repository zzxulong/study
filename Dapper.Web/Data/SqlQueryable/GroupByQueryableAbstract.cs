﻿using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.Enums;
using Dapper.Web.DataAbstractions.SqlQueryable.GroupByQueryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Web.Data.SqlQueryable
{
    internal abstract class GroupByQueryableAbstract : IGroupByQueryable
    {
        protected readonly IDbSet Db;
        protected readonly QueryBody QueryBody;
        protected readonly QueryBuilder QueryBuilder;

        protected GroupByQueryableAbstract (IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression)
        {
            Db = db;
            QueryBody = queryBody;
            QueryBuilder = queryBuilder;
            QueryBody.SetGroupBy(expression);
        }

        protected void SetHaving (LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.Having.Add(expression);
        }

        public void SetOrderBy (string customOrderBy)
        {
            if (customOrderBy.NotNull())
                QueryBody.SetOrderBy(new Sort(customOrderBy));
        }

        public void SetOrderByDescending (string customOrderBy)
        {
            if (customOrderBy.NotNull())
                QueryBody.SetOrderBy(new Sort(customOrderBy, SortType.Desc));
        }

        public void SetOrderBy (LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.SetOrderBy(expression);
        }

        public void SetOrderByDescending (LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.SetOrderBy(expression, SortType.Desc);
        }

        public void SetSelect (LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.Select = expression;
        }

        public IList<dynamic> ToList ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.Query(sql, parameters.Parse()).ToList();
        }

        public IList<TResult> ToList<TResult> ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.Query<TResult>(sql, parameters.Parse()).ToList();
        }

        public async Task<IList<dynamic>> ToListAsync ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync(sql, parameters.Parse())).ToList();
        }

        public async Task<IList<TResult>> ToListAsync<TResult> ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync<TResult>(sql, parameters.Parse())).ToList();
        }

        public dynamic First ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault(sql, parameters.Parse());
        }

        public TResult First<TResult> ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault<TResult>(sql, parameters.Parse());
        }

        public Task<dynamic> FirstAsync ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync(sql, parameters.Parse());
        }

        public Task<TResult> FirstAsync<TResult> ()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync<TResult>(sql, parameters.Parse());
        }
    }
}
