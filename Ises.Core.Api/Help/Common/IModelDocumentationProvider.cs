using System;
using System.Reflection;

namespace Ises.Core.Api.Help.Common
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
