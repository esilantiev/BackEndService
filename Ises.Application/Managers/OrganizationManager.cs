using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.ClientFilters;
using Ises.Contracts.OrganizationsDto;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.Organizations;

namespace Ises.Application.Managers
{
    public interface IOrganizationManager : IManager
    {
        Task<PagedResult<OrganizationDto>> GetOrganizationsAsync(OrganizationFilter organizationFilter);
        Task RemoveOrganizationAsync(long id);
        Task<long> CreateOrganizationAsync(OrganizationDto organizationDto);
        Task<long> UpdateOrganizationAsync(OrganizationDto organizationDto);
    }

    public class OrganizationManager : IOrganizationManager
    {
        readonly IOrganizationRepository organizationRepository;

        public OrganizationManager(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<PagedResult<OrganizationDto>> GetOrganizationsAsync(OrganizationFilter organizationFilter)
        {
            var organizationsPagedResult = await organizationRepository.GetOrganizationsAsync(organizationFilter);

            var organizationsDtoPagedResult = new PagedResult<OrganizationDto>();
            Mapper.Map(organizationsPagedResult, organizationsDtoPagedResult);
            return organizationsDtoPagedResult;
        }

        public Task RemoveOrganizationAsync(long id)
        {
            return organizationRepository.RemoveOrganizationAsync(id);
        }

        public async Task<long> CreateOrganizationAsync(OrganizationDto organizationDto)
        {
            var organization = new Organization();
            Mapper.Map(organizationDto, organization);
            var rowsUpdated = await organizationRepository.CreateOrganizationAsync(organization, organizationDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateOrganizationAsync(OrganizationDto organizationDto)
        {
            var organization = new Organization();
            Mapper.Map(organizationDto, organization);
            var rowsUpdated = await organizationRepository.CreateOrganizationAsync(organization, organizationDto.MappingScheme);
            return rowsUpdated;
        }

    }
}