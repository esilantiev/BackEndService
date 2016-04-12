using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.WorkCertificatesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.WorkCertificates;

namespace Ises.Application.Managers
{
    public interface IWorkCertificateManager : IManager
    {
        Task<PagedResult<WorkCertificateDto>> GetWorkCertificatesAsync(WorkCertificateFilter workCertificateFilter);
        Task RemoveWorkCertificateAsync(long id);
        Task<long> CreateWorkCertificateAsync(WorkCertificateDto workCertificateDto);
        Task<long> UpdateWorkCertificateAsync(WorkCertificateDto workCertificateDto);
    }

    public class WorkCertificateManager : IWorkCertificateManager
    {
        readonly IWorkCertificateRepository workCertificateRepository;

        public WorkCertificateManager(IWorkCertificateRepository workCertificateRepository)
        {
            this.workCertificateRepository = workCertificateRepository;
        }

        public async Task<PagedResult<WorkCertificateDto>> GetWorkCertificatesAsync(WorkCertificateFilter workCertificateFilter)
        {
            var workCertificatesPagedResult = await workCertificateRepository.GetWorkCertificatesAsync(workCertificateFilter);

            var workCertificatesModelPagedResult = new PagedResult<WorkCertificateDto>();
            Mapper.Map(workCertificatesPagedResult, workCertificatesModelPagedResult);
            return workCertificatesModelPagedResult;
        }

        public Task RemoveWorkCertificateAsync(long id)
        {
            return workCertificateRepository.RemoveWorkCertificateAsync(id);
        }

        public async Task<long> CreateWorkCertificateAsync(WorkCertificateDto workCertificateDto)
        {
            var workCertificate = new WorkCertificate();
            Mapper.Map(workCertificateDto, workCertificate);
            var rowsUpdated = await workCertificateRepository.CreateWorkCertificateAsync(workCertificate, workCertificateDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateWorkCertificateAsync(WorkCertificateDto workCertificateDto)
        {
            var workCertificate = new WorkCertificate();
            Mapper.Map(workCertificateDto, workCertificate);
            var rowsUpdated = await workCertificateRepository.UpdateWorkCertificateAsync(workCertificate, workCertificateDto.MappingScheme);
            return rowsUpdated;
        }

    }
}