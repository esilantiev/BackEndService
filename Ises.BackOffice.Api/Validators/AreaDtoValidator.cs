using System.Linq;
using FluentValidation;
using Ises.Contracts.AreasDto;
using Ises.Contracts.ClientFilters;
using Ises.Data.Repositories;

namespace Ises.BackOffice.Api.Validators
{
    public class AreaDtoValidator : AbstractValidator<AreaDto>
    {
        private readonly IAreaRepository areaRepository;

        public AreaDtoValidator(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(areaDto => areaDto.Name)
                .NotEmpty().WithMessage("Should not be empty")
                .Must(IsUnique).WithMessage("This name is already in use");
        }

        private bool IsUnique(string name)
        {
            var existingAreas = areaRepository.GetAreasAsync(new AreaFilter { Name = name }).Result.Data;

            return !existingAreas.Any();
        }
    }
}
