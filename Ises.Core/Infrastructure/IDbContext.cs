using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackerEnabledDbContext.Common.Models;

namespace Ises.Core.Infrastructure
{
    public interface IDbContext
    {
        void SetProxyCreationEnabled(bool flag);
        IQueryable<T> DbSet<T>() where T : class;
        T Add<T>(T entity, string mappingScheme) where T : class;
        T Update<T>(T entity, string mappingScheme) where T : class;
        T Delete<T>(T entity) where T : class;
        IQueryable<AuditLog> GetLogsFor<T>() where T : class;
        Task<int> SaveAsync();
        Task<List<T>> SqlQueryAsync<T>(string sql, params KeyValuePair<string, object>[] parameters);
    }
}
