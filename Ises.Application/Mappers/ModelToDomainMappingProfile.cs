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
    public class ModelToDomainMappingProfile : Profile
    {

        public override string ProfileName
        {
            get
            {
                return "ModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<ContactDetailsDto, ContactDetails>();

            Mapper.CreateMap<UserRoleDto, UserRole>();

            Mapper.CreateMap<InstallationDto, Installation>();

            Mapper.CreateMap<LocationDto, Location>();

            Mapper.CreateMap<HazardGroupDto, HazardGroup>();

            Mapper.CreateMap<HazardDto, Hazard>();

            Mapper.CreateMap<OrganizationDto, Organization>();

            Mapper.CreateMap<LeadDisciplineDto, LeadDiscipline>();

            Mapper.CreateMap<PositionDto, Position>();

            Mapper.CreateMap<RolePermissionDto, RolePermission>();

            Mapper.CreateMap<AreaDto, Area>();

            Mapper.CreateMap<AreaTypeDto, AreaType>();

            Mapper.CreateMap<CertificateUserDto, CertificateUser>();

            Mapper.CreateMap<ExternalDocumentDto, ExternalDocument>();

            Mapper.CreateMap<HazardControlDto, HazardControl>();

            Mapper.CreateMap<HazardRuleDto, HazardRule>();

            Mapper.CreateMap<HistoryChangeDto, HistoryChange>();

            Mapper.CreateMap<IsolationCertificateDto, IsolationCertificate>();

            Mapper.CreateMap<IsolationPointDto, IsolationPoint>();

            Mapper.CreateMap<WorkCertificateDto, WorkCertificate>();

            Mapper.CreateMap<IsolationPointDto, IsolationPoint>();

            Mapper.CreateMap<WorkCertificateCategoryDto, WorkCertificateCategory>();
        }
    }
}