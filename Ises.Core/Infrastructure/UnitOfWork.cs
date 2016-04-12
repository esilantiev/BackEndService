using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Common.Models;

namespace Ises.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext Context { get; set; }

        public UnitOfWork(IDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes) where T : class
        {
            Context.SetProxyCreationEnabled(false);
            var query = Context.DbSet<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null, IEnumerable<string> includes = null) where T : class
        {
            Context.SetProxyCreationEnabled(false);
            var query = Context.DbSet<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public IQueryable<AuditLog> QueryLogs<T>(Expression<Func<AuditLog, bool>> filter = null, IEnumerable<string> includes = null) where T : class
        {
            Context.SetProxyCreationEnabled(false);
            var query = Context.GetLogsFor<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public T Add<T>(T entity, string mappingScheme) where T : class
        {
            return Context.Add(entity, mappingScheme);
        }

        public T Update<T>(T entity, string mappingScheme) where T : class
        {
            return Context.Update(entity, mappingScheme);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Context.Delete(entity);
        }

        public Task<int> SaveAsync()
        {
            return Context.SaveAsync();
        }

        public Task<List<T>> SqlQueryAsync<T>(string sql, params KeyValuePair<string, object>[] parameters)
        {
            return Context.SqlQueryAsync<T>(sql, parameters);
        }
    }
}
