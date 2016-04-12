using AutoMapper;

namespace Ises.Application.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.AddProfile<ModelToDomainMappingProfile>();
            Mapper.AddProfile<DomainToModelMappingProfile>();
        }
    }
}
