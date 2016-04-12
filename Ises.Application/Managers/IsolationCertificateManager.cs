using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.IsolationCertificatesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.IsolationCertificates;

namespace Ises.Application.Managers
{
    public interface IIsolationCertificateManager : IManager
    {
        Task<PagedResult<IsolationCertificateDto>> GetIsolationCertificatesAsync(IsolationCertificateFilter isolationCertificateFilter);
        Task RemoveIsolationCertificateAsync(long id);
        Task<long> CreateIsolationCertificateAsync(IsolationCertificateDto isolationCertificateDto);
        Task<long> UpdateIsolationCertificateAsync(IsolationCertificateDto isolationCertificateDto);
    }

    public class IsolationCertificateManager : IIsolationCertificateManager
    {
        readonly IIsolationCertificateRepository isolationCertificateRepository;

        public IsolationCertificateManager(IIsolationCertificateRepository isolationCertificateRepository)
        {
            this.isolationCertificateRepository = isolationCertificateRepository;
        }

        public async Task<PagedResult<IsolationCertificateDto>> GetIsolationCertificatesAsync(IsolationCertificateFilter isolationCertificateFilter)
        {
            var isolationCertificatesPagedResult = await isolationCertificateRepository.GetIsolationCertificatesAsync(isolationCertificateFilter);

            var isolationCertificatesModelPagedResult = new PagedResult<IsolationCertificateDto>();
            Mapper.Map(isolationCertificatesPagedResult, isolationCertificatesModelPagedResult);
            return isolationCertificatesModelPagedResult;
        }

        public Task RemoveIsolationCertificateAsync(long id)
        {
            return isolationCertificateRepository.RemoveIsolationCertificateAsync(id);
        }

        public async Task<long> CreateIsolationCertificateAsync(IsolationCertificateDto isolationCertificateDto)
        {
            var isolationCertificate = new IsolationCertificate();
            Mapper.Map(isolationCertificateDto, isolationCertificate);
            var rowsUpdated = await isolationCertificateRepository.CreateIsolationCertificateAsync(isolationCertificate, isolationCertificateDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateIsolationCertificateAsync(IsolationCertificateDto isolationCertificateDto)
        {
            var isolationCertificate = new IsolationCertificate();
            Mapper.Map(isolationCertificateDto, isolationCertificate);
            var rowsUpdated = await isolationCertificateRepository.UpdateIsolationCertificateAsync(isolationCertificate, isolationCertificateDto.MappingScheme);
            return rowsUpdated;
        }

    }
}