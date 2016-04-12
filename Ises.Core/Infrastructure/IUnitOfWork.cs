using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Common.Models;

namespace Ises.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes) where T : class;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null, IEnumerable<string> includes = null) where T : class;
        IQueryable<AuditLog> QueryLogs<T>(Expression<Func<AuditLog, bool>> filter = null, IEnumerable<string> includes = null) where T : class;
        T Add<T>(T entity, string mappingScheme) where T : class;
        T Update<T>(T entity, string mappingScheme) where T : class;
        T Delete<T>(T entity) where T : class;
        Task<List<T>> SqlQueryAsync<T>(string sql, params KeyValuePair<string, object>[] parameters);
        Task<int> SaveAsync();
       
    }
}
