using System;
using Autofac;
using FluentValidation;

namespace Ises.BackOffice.Api.Infrastructure.Validation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            this.context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (context.TryResolve(validatorType, out instance))
            {
                var validator = instance as IValidator;
                return validator;
            }

            return null;
        }
    }
}
