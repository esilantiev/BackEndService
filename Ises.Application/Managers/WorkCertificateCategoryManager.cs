using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.WorkCertificateCategoriesDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.WorkCertificateCategories;

namespace Ises.Application.Managers
{
    public interface IWorkCertificateCategoryManager : IManager
    {
        Task<ApiResult> GetWorkCertificateCategoriesAsync(WorkCertificateCategoryFilter workCertificateCategoryFilter);
        Task RemoveWorkCertificateCategoryAsync(long id);
        Task<long> CreateWorkCertificateCategoryAsync(WorkCertificateCategoryDto workCertificateCategoryDto);
        Task<long> UpdateWorkCertificateCategoryAsync(WorkCertificateCategoryDto workCertificateCategoryDto);
    }

    public class WorkCertificateCategoryManager : IWorkCertificateCategoryManager
    {
        readonly IWorkCertificateCategoryRepository workCertificateCategoryRepository;

        public WorkCertificateCategoryManager(IWorkCertificateCategoryRepository workCertificateCategoryRepository)
        {
            this.workCertificateCategoryRepository = workCertificateCategoryRepository;
        }

        public async Task<ApiResult> GetWorkCertificateCategoriesAsync(WorkCertificateCategoryFilter workCertificateCategoryFilter)
        {
            var workCertificateCategoriesPagedResult = await workCertificateCategoryRepository.GetWorkCertificateCategoriesAsync(workCertificateCategoryFilter);

            var workCertificateCategoriesDtoPagedResult = new PagedResult<WorkCertificateCategoryDto>();
            Mapper.Map(workCertificateCategoriesPagedResult, workCertificateCategoriesDtoPagedResult);
            return new ApiResult(MessageType.Success, workCertificateCategoriesDtoPagedResult);
        }

        public Task RemoveWorkCertificateCategoryAsync(long id)
        {
            return workCertificateCategoryRepository.RemoveWorkCertificateCategoryAsync(id);
        }

        public async Task<long> CreateWorkCertificateCategoryAsync(WorkCertificateCategoryDto workCertificateCategoryDto)
        {
            var workCertificateCategory = new WorkCertificateCategory();
            Mapper.Map(workCertificateCategoryDto, workCertificateCategory);
            var rowsUpdated = await workCertificateCategoryRepository.CreateWorkCertificateCategoryAsync(workCertificateCategory, workCertificateCategoryDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateWorkCertificateCategoryAsync(WorkCertificateCategoryDto workCertificateCategoryDto)
        {
            var workCertificateCategory = new WorkCertificateCategory();
            Mapper.Map(workCertificateCategoryDto, workCertificateCategory);
            var rowsUpdated = await workCertificateCategoryRepository.UpdateWorkCertificateCategoryAsync(workCertificateCategory, workCertificateCategoryDto.MappingScheme);
            return rowsUpdated;
        }

    }
}