using System.Threading.Tasks;
using AutoMapper;
using Ises.Contracts.CertificatesUsersDto;
using Ises.Contracts.ClientFilters;
using Ises.Core.Common;
using Ises.Data.Repositories;
using Ises.Domain.CertificatesUsers;

namespace Ises.Application.Managers
{
    public interface ICertificateUserManager : IManager
    {
        Task<PagedResult<CertificateUserDto>> GetCertificateUsersAsync(CertificateUserFilter certificateCertificateUserFilter);
        Task RemoveCertificateUserAsync(long id);
        Task<long> CreateCertificateUserAsync(CertificateUserDto certificateCertificateUserDto);
        Task<long> UpdateCertificateUserAsync(CertificateUserDto certificateCertificateUserDto);
    }

    public class CertificateUserManager : ICertificateUserManager
    {
        readonly ICertificateUserRepository certificateUserRepository;

        public CertificateUserManager(ICertificateUserRepository certificateUserRepository)
        {
            this.certificateUserRepository = certificateUserRepository;
        }

        public async Task<PagedResult<CertificateUserDto>> GetCertificateUsersAsync(CertificateUserFilter certificateCertificateUserFilter)
        {
            var certificateUsersPagedResult = await certificateUserRepository.GetCertificateUsersAsync(certificateCertificateUserFilter);

            var certificateCertificateUsersDtoPagedResult = new PagedResult<CertificateUserDto>();
            Mapper.Map(certificateUsersPagedResult, certificateCertificateUsersDtoPagedResult);
            return certificateCertificateUsersDtoPagedResult;
        }

        public Task RemoveCertificateUserAsync(long id)
        {
            return certificateUserRepository.RemoveCertificateUserAsync(id);
        }

        public async Task<long> CreateCertificateUserAsync(CertificateUserDto certificateCertificateUserDto)
        {
            var certificateCertificateUser = new CertificateUser();
            Mapper.Map(certificateCertificateUserDto, certificateCertificateUser);
            var rowsUpdated = await certificateUserRepository.CreateCertificateUserAsync(certificateCertificateUser, certificateCertificateUserDto.MappingScheme);
            return rowsUpdated;
        }

        public async Task<long> UpdateCertificateUserAsync(CertificateUserDto certificateCertificateUserDto)
        {
            var certificateCertificateUser = new CertificateUser();
            Mapper.Map(certificateCertificateUserDto, certificateCertificateUser);
            var rowsUpdated = await certificateUserRepository.UpdateCertificateUserAsync(certificateCertificateUser, certificateCertificateUserDto.MappingScheme);
            return rowsUpdated;
        }

    }
}