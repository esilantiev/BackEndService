using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.InstallationsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Installations;

namespace Ises.Application.Managers
{
    public interface IInstallationManager : IManager
    {
        Task<ApiResult> GetInstallationsAsync(InstallationFilter installationFilter);
        Task<ApiResult> RemoveInstallationAsync(long id);
        Task<ApiResult> CreateInstallationAsync(InstallationDto installationDto);
        Task<ApiResult> UpdateInstallationAsync(InstallationDto userDto);
    }

    public class InstallationManager : IInstallationManager
    {
        readonly IInstallationRepository installationRepository;

        public InstallationManager(IInstallationRepository installationRepository)
        {
            this.installationRepository = installationRepository;
        }

        public async Task<ApiResult> GetInstallationsAsync(InstallationFilter installationFilter)
        {
            var installationsPagedResult = await installationRepository.GetInstallationsAsync(installationFilter);

            var installationsDtoPagedResult = new PagedResult<InstallationDto>();
            Mapper.Map(installationsPagedResult, installationsDtoPagedResult);
            return new ApiResult(MessageType.Success, installationsDtoPagedResult);
        }

        public async Task<ApiResult> RemoveInstallationAsync(long id)
        {
            await installationRepository.RemoveInstallationAsync(id);
            return new ApiResult(MessageType.Success);
        }

        public async Task<ApiResult> CreateInstallationAsync(InstallationDto installationDto)
        {
            var installation = new Installation();
            Mapper.Map(installationDto, installation);
            var insertedId = await installationRepository.CreateInstallationAsync(installation, installationDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            apiResult.AdditionalDetails.Add("insertedId", insertedId);

            return apiResult;
        }

        public async Task<ApiResult> UpdateInstallationAsync(InstallationDto installationDto)
        {
            var installation = new Installation();
            Mapper.Map(installationDto, installation);
            var updatedInstallation = await installationRepository.UpdateInstallationAsync(installation, installationDto.MappingScheme);

            var apiResult = new ApiResult(MessageType.Success);
            return apiResult;
        }

    }
}
