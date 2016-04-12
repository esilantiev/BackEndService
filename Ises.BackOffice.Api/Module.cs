using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation;
using Ises.BackOffice.Api.Infrastructure.Validation;
using Ises.BackOffice.Api.Validators;
using Ises.Core.Api.Common;

namespace Ises.BackOffice.Api
{
    public class Module : ApiModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = GetType().Assembly;
            builder.RegisterApiControllers(assembly);

            AssemblyScanner
                .FindValidatorsInAssemblyContaining<AreaDtoValidator>()
                .ForEach(x => builder.RegisterType(x.ValidatorType)
                .As(x.InterfaceType)
                .SingleInstance());

            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
        }
    }
}
