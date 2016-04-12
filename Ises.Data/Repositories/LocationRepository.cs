using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Core.Infrastructure;
using Ises.Data.MappingSchemes;
using Ises.Domain.Locations;

namespace Ises.Data.Repositories
{
    public interface ILocationRepository : IRepository
    {
        Task<PagedResult<Location>> GetLocationsAsync(LocationFilter locationFilter);
        Task<long> CreateLocationAsync(Location location, string mappingScheme);
        Task RemoveLocationAsync(long id);
        Task<Location> UpdateLocationAsync(Location location, string mappingScheme);

        IQueryable<Location> GetLocationsQuery(Expression<Func<Location, bool>> expression = null, params Expression<Func<Location, object>>[] includes);
    }

    public class LocationRepository : BaseRepository, ILocationRepository
    {
        readonly IUnitOfWork unitOfWork;
        private ILocationMappingSchemeRegistrator locationMappingSchemeRegistrator;

        public LocationRepository(IUnitOfWork unitOfWork, ILocationMappingSchemeRegistrator locationMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.locationMappingSchemeRegistrator = locationMappingSchemeRegistrator;
        }

        public async Task<PagedResult<Location>> GetLocationsAsync(LocationFilter filter)
        {
            filter = filter ?? new LocationFilter();

            var result = unitOfWork.Query(GetLocationExpression(filter), filter.PropertiesToInclude);

            List<Location> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<Location>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetLocationExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateLocationAsync(Location location, string mappingScheme)
        {
            locationMappingSchemeRegistrator.Register();
            var insertedLocation = unitOfWork.Add(location, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedLocation.Id;
        }

        public async Task RemoveLocationAsync(long id)
        {
            var location = await unitOfWork.Query<Location>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(location);
            await unitOfWork.SaveAsync();
        }

        public async Task<Location> UpdateLocationAsync(Location location, string mappingScheme)
        {
            locationMappingSchemeRegistrator.Register();
            var updatedLocation = unitOfWork.Add(location, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedLocation;
        }

        public IQueryable<Location> GetLocationsQuery(Expression<Func<Location, bool>> expression = null, params Expression<Func<Location, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<Location, bool>> GetLocationExpression(LocationFilter filter)
        {
            Expression<Func<Location, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }
}
