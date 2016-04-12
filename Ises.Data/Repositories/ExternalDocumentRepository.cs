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
using Ises.Domain.ExternalDocuments;

namespace Ises.Data.Repositories
{
    public interface IExternalDocumentRepository : IRepository
    {
        Task<PagedResult<ExternalDocument>> GetExternalDocumentsAsync(ExternalDocumentFilter filter);
        Task<long> CreateExternalDocumentAsync(ExternalDocument externalDocument, string mappingScheme);
        Task RemoveExternalDocumentAsync(long id);
        Task<long> UpdateExternalDocumentAsync(ExternalDocument externalDocument, string mappingScheme);

        IQueryable<ExternalDocument> GetExternalDocumentsQuery(Expression<Func<ExternalDocument, bool>> expression = null, params Expression<Func<ExternalDocument, object>>[] includes);
    }

    public class ExternalDocumentRepository : BaseRepository, IExternalDocumentRepository
    {
        readonly IUnitOfWork unitOfWork;
        private IExternalDocumentMappingSchemeRegistrator externalDocumentMappingSchemeRegistrator;

        public ExternalDocumentRepository(IUnitOfWork unitOfWork, IExternalDocumentMappingSchemeRegistrator externalDocumentMappingSchemeRegistrator)
            : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.externalDocumentMappingSchemeRegistrator = externalDocumentMappingSchemeRegistrator;
        }

        public async Task<PagedResult<ExternalDocument>> GetExternalDocumentsAsync(ExternalDocumentFilter filter)
        {
            filter = filter ?? new ExternalDocumentFilter();

            var result = unitOfWork.Query(GetExternalDocumentExpression(filter), filter.PropertiesToInclude);
      
            List<ExternalDocument> list = await result.OrderBy(filter.OrderBy)
               .Skip((filter.Page - 1) * filter.Skip).Take(filter.Take)
               .ToListAsync();
            var pagedResult = new PagedResult<ExternalDocument>
            {
                Data = list,
                PageInfo = await GetPageInfo(x => x.Id, filter, GetExternalDocumentExpression(filter))
            };
            return pagedResult;
        }

        public async Task<long> CreateExternalDocumentAsync(ExternalDocument externalDocument, string mappingScheme)
        {
            externalDocumentMappingSchemeRegistrator.Register();
            var insertedExternalDocument = unitOfWork.Add(externalDocument, mappingScheme);

            await unitOfWork.SaveAsync();
            return insertedExternalDocument.Id;
        }

        public async Task RemoveExternalDocumentAsync(long id)
        {
            var externalDocument = await unitOfWork.Query<ExternalDocument>(x => x.Id == id).SingleOrDefaultAsync();
            unitOfWork.Delete(externalDocument);
            await unitOfWork.SaveAsync();
        }

        public async Task<long> UpdateExternalDocumentAsync(ExternalDocument externalDocument, string mappingScheme)
        {
            externalDocumentMappingSchemeRegistrator.Register();
            var updatedExternalDocument = unitOfWork.Add(externalDocument, mappingScheme);

            await unitOfWork.SaveAsync();
            return updatedExternalDocument.Id;
        }

        public IQueryable<ExternalDocument> GetExternalDocumentsQuery(Expression<Func<ExternalDocument, bool>> expression = null, params Expression<Func<ExternalDocument, object>>[] includes)
        {
            return unitOfWork.Query(expression, includes);
        }

        private Expression<Func<ExternalDocument, bool>> GetExternalDocumentExpression(ExternalDocumentFilter filter)
        {
            Expression<Func<ExternalDocument, bool>> expression = null;
            if (filter.Id > 0)
            {
                expression = x => x.Id == filter.Id;
            }
            return expression;
        }
    }


}
