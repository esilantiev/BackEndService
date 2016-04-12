using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.ExternalDocumentsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.ExternalDocuments;

namespace Ises.Application.Managers
{
    public interface IExternalDocumentManager : IManager
    {
        Task<PagedResult<ExternalDocumentDto>> GetExternalDocumentsAsync(ExternalDocumentFilter externalDocumentFilter);
        Task RemoveExternalDocumentAsync(long id);
        Task<long> CreateExternalDocumentAsync(ExternalDocumentDto externalDocumentDto);
        Task<long> UpdateExternalDocumentAsync(ExternalDocumentDto externalDocumentDto);
    }

    public class ExternalDocumentManager : IExternalDocumentManager
    {
        readonly IExternalDocumentRepository externalDocumentRepository;

        public ExternalDocumentManager(IExternalDocumentRepository externalDocumentRepository)
        {
            this.externalDocumentRepository = externalDocumentRepository;
        }

        public async Task<PagedResult<ExternalDocumentDto>> GetExternalDocumentsAsync(ExternalDocumentFilter externalDocumentFilter)
        {
            var externalDocumentsPagedResult = await externalDocumentRepository.GetExternalDocumentsAsync(externalDocumentFilter);

            var externalDocumentsModelPagedResult = new PagedResult<ExternalDocumentDto>();
            Mapper.Map(externalDocumentsPagedResult, externalDocumentsModelPagedResult);
            return externalDocumentsModelPagedResult;
        }

        public Task RemoveExternalDocumentAsync(long id)
        {
            return externalDocumentRepository.RemoveExternalDocumentAsync(id);
        }

        public async Task<long> CreateExternalDocumentAsync(ExternalDocumentDto externalDocumentDto)
        {
            var externalDocument = new ExternalDocument();
            Mapper.Map(externalDocumentDto, externalDocument);
            var rowsUpdated = await externalDocumentRepository.CreateExternalDocumentAsync(externalDocument, externalDocumentDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateExternalDocumentAsync(ExternalDocumentDto externalDocumentDto)
        {
            var externalDocument = new ExternalDocument();
            Mapper.Map(externalDocumentDto, externalDocument);
            var rowsUpdated = await externalDocumentRepository.UpdateExternalDocumentAsync(externalDocument, externalDocumentDto.MappingScheme);
            return rowsUpdated;
        }

    }
}