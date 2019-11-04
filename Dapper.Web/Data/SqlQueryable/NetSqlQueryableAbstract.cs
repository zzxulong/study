﻿using Dapper.Web.DataAbstractions;
using Dapper.Web.DataAbstractions.SqlQueryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dapper.Web.Data.SqlQueryable
{
    internal abstract class NetSqlQueryableAbstract : INetSqlQueryable
    {
        protected readonly IDbSet Db;
        protected readonly ISqlAdapter SqlAdapter;
        protected QueryBody QueryBody;
        protected readonly QueryBuilder QueryBuilder;

        protected NetSqlQueryableAbstract (IDbSet dbSet, QueryBody queryBody)
        {
            Db = dbSet;
            SqlAdapter = dbSet.DbContext.Options.SqlAdapter;

            QueryBody = queryBody;

            QueryBuilder = new QueryBuilder(QueryBody, SqlAdapter,  Db.DbContext);
        }

        #region ==ToList==

        public IList<dynamic> ToList ()
        {
            var sql = QueryBuilder.QuerySqlBuild(out IQueryParameters parameters);
            return Db.Query(sql, parameters.Parse(), QueryBody.Transaction).ToList();
        }

        public IList<TResult> ToList<TResult> ()
        {
            var sql = QueryBuilder.QuerySqlBuild(out IQueryParameters parameters);
            return Db.Query<TResult>(sql, parameters.Parse(), QueryBody.Transaction).ToList();
        }

        public async Task<IList<dynamic>> ToListAsync ()
        {
            var sql = QueryBuilder.QuerySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync(sql, parameters.Parse(), QueryBody.Transaction)).ToList();
        }

        public async Task<IList<TResult>> ToListAsync<TResult> ()
        {
            var sql = QueryBuilder.QuerySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction)).ToList();
        }

        #endregion

        #region ==Count==

        public long Count ()
        {
            var sql = QueryBuilder.CountSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<long>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        public Task<long> CountAsync ()
        {
            var sql = QueryBuilder.CountSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<long>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        #region ==First==

        public dynamic First ()
        {
            var sql = QueryBuilder.FirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault(sql, parameters.Parse(), QueryBody.Transaction);
        }

        public TResult First<TResult> ()
        {
            var sql = QueryBuilder.FirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        public Task<dynamic> FirstAsync ()
        {
            var sql = QueryBuilder.FirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync(sql, parameters.Parse(), QueryBody.Transaction);
        }

        public Task<TResult> FirstAsync<TResult> ()
        {
            var sql = QueryBuilder.FirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        #region ==Pagination==

        public IList<dynamic> Pagination (Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = Count();
            return ToList();
        }

        public IList<TResult> Pagination<TResult> (Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = Count();
            return ToList<TResult>();
        }

        public async Task<IList<dynamic>> PaginationAsync (Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = await CountAsync();
            return await ToListAsync();
        }

        public async Task<IList<TResult>> PaginationAsync<TResult> (Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = await CountAsync();
            return await ToListAsync<TResult>();
        }

        #endregion

        #region ==Exists==

        public bool Exists ()
        {
            var sql = QueryBuilder.ExistsSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<int>(sql, parameters.Parse(), QueryBody.Transaction) > 0;
        }

        public async Task<bool> ExistsAsync ()
        {
            var sql = QueryBuilder.ExistsSqlBuild(out IQueryParameters parameters);
            return await Db.ExecuteScalarAsync<int>(sql, parameters.Parse(), QueryBody.Transaction) > 0;
        }

        #endregion

        #region ==Max==

        protected TResult Max<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MaxSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        protected Task<TResult> MaxAsync<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MaxSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        #region ==Min==

        protected TResult Min<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MinSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        protected Task<TResult> MinAsync<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MinSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        #region ==Sum==

        protected TResult Sum<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.SumSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        protected Task<TResult> SumAsync<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.SumSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        #region ==Avg==

        protected TResult Avg<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.AvgSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        protected Task<TResult> AvgAsync<TResult> (LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.AvgSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse(), QueryBody.Transaction);
        }

        #endregion

        public string ToSql ()
        {
            return QueryBuilder.QuerySqlBuild(out _);
        }

        public string ToSql (out IQueryParameters parameters)
        {
            return QueryBuilder.QuerySqlBuild(out parameters);
        }
    }
}
