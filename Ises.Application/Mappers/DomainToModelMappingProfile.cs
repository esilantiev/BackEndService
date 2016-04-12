using AutoMapper;
using Ises.Contracts.AreasDto;
using Ises.Contracts.AreaTypesDto;
using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.ExternalDocumentsDto;
using Ises.Contracts.HazardControlsDto;
using Ises.Contracts.HazardGroupsDto;
using Ises.Contracts.HazardRulesDto;
using Ises.Contracts.HazardsDto;
using Ises.Contracts.HistoryChangesDto;
using Ises.Contracts.InstallationsDto;
using Ises.Contracts.IsolationCertificatesDto;
using Ises.Contracts.IsolationPointsDto;
using Ises.Contracts.LeadDisciplinesDto;
using Ises.Contracts.LocationsDto;
using Ises.Contracts.OrganizationsDto;
using Ises.Contracts.PositionsDto;
using Ises.Contracts.RolePermissionsDto;
using Ises.Contracts.UserRolesDto;
using Ises.Contracts.UsersDto;
using Ises.Contracts.WorkCertificateCategoriesDto;
using Ises.Contracts.WorkCertificatesDto;
using Ises.Core.Common;
using Ises.Domain.Areas;
using Ises.Domain.AreaTypes;
using Ises.Domain.CertificatesUsers;
using Ises.Domain.ExternalDocuments;
using Ises.Domain.HazardControls;
using Ises.Domain.HazardGroups;
using Ises.Domain.HazardRules;
using Ises.Domain.Hazards;
using Ises.Domain.HistoryChanges;
using Ises.Domain.Installations;
using Ises.Domain.IsolationCertificates;
using Ises.Domain.IsolationPoints;
using Ises.Domain.LeadDisciplines;
using Ises.Domain.Locations;
using Ises.Domain.Organizations;
using Ises.Domain.Positions;
using Ises.Domain.RolePermissions;
using Ises.Domain.UserRoles;
using Ises.Domain.Users;
using Ises.Domain.WorkCertificateCategories;
using Ises.Domain.WorkCertificates;

namespace Ises.Application.Mappers
{
    public class DomainToModelMappingProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "DomainToModelMappingProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<ContactDetails, ContactDetailsDto>();
            Mapper.CreateMap<PagedResult<User>, PagedResult<UserDto>>();

            Mapper.CreateMap<UserRole, UserRoleDto>();
            Mapper.CreateMap<PagedResult<UserRole>, PagedResult<UserRoleDto>>();

            Mapper.CreateMap<Installation, InstallationDto>();
            Mapper.CreateMap<PagedResult<Installation>, PagedResult<InstallationDto>>();

            Mapper.CreateMap<Location, LocationDto>();
            Mapper.CreateMap<PagedResult<Location>, PagedResult<LocationDto>>();

            Mapper.CreateMap<HazardGroup, HazardGroupDto>();
            Mapper.CreateMap<PagedResult<HazardGroup>, PagedResult<HazardGroupDto>>();

            Mapper.CreateMap<Hazard, HazardDto>();
            Mapper.CreateMap<PagedResult<Hazard>, PagedResult<HazardDto>>();

            Mapper.CreateMap<Organization, OrganizationDto>();
            Mapper.CreateMap<PagedResult<Organization>, PagedResult<OrganizationDto>>();

            Mapper.CreateMap<LeadDiscipline, LeadDisciplineDto>();
            Mapper.CreateMap<PagedResult<LeadDiscipline>, PagedResult<LeadDisciplineDto>>();

            Mapper.CreateMap<Position, PositionDto>();
            Mapper.CreateMap<PagedResult<Position>, PagedResult<PositionDto>>();

            Mapper.CreateMap<RolePermission, RolePermissionDto>();
            Mapper.CreateMap<PagedResult<RolePermission>, PagedResult<RolePermissionDto>>();

            Mapper.CreateMap<Area, AreaDto>();
            Mapper.CreateMap<PagedResult<Area>, PagedResult<AreaDto>>();

            Mapper.CreateMap<AreaType, AreaTypeDto>();
            Mapper.CreateMap<PagedResult<AreaType>, PagedResult<AreaTypeDto>>();

            Mapper.CreateMap<CertificateUser, CertificateUserDto>();
            Mapper.CreateMap<PagedResult<CertificateUser>, PagedResult<CertificateUserDto>>();

            Mapper.CreateMap<ExternalDocument, ExternalDocumentDto>();
            Mapper.CreateMap<PagedResult<ExternalDocument>, PagedResult<ExternalDocumentDto>>();

            Mapper.CreateMap<HazardControl, HazardControlDto>();
            Mapper.CreateMap<PagedResult<HazardControl>, PagedResult<HazardControlDto>>();

            Mapper.CreateMap<HazardRule, HazardRuleDto>();
            Mapper.CreateMap<PagedResult<HazardRule>, PagedResult<HazardRuleDto>>();

            Mapper.CreateMap<HistoryChange, HistoryChangeDto>();
            Mapper.CreateMap<PagedResult<HistoryChange>, PagedResult<HistoryChangeDto>>();

            Mapper.CreateMap<IsolationCertificate, IsolationCertificateDto>();
            Mapper.CreateMap<PagedResult<IsolationCertificate>, PagedResult<IsolationCertificateDto>>();

            Mapper.CreateMap<IsolationPoint, IsolationPointDto>();
            Mapper.CreateMap<PagedResult<IsolationPoint>, PagedResult<IsolationPointDto>>();

            Mapper.CreateMap<WorkCertificate, WorkCertificateDto>();
            Mapper.CreateMap<PagedResult<WorkCertificate>, PagedResult<WorkCertificateDto>>();

            Mapper.CreateMap<WorkCertificateCategory, WorkCertificateCategoryDto>();
            Mapper.CreateMap<PagedResult<WorkCertificateCategory>, PagedResult<WorkCertificateCategoryDto>>();
        }
    }
}