#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace VacationTests.DependencyInjection.MethodInject
{
    internal class UseContainerMethodInfo : MethodWrapper, IMethodInfo
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMethodInfo methodInfo;

        public UseContainerMethodInfo(IMethodInfo methodInfo, IServiceProvider serviceProvider)
            : base(methodInfo.TypeInfo.Type, methodInfo.MethodInfo)
        {
            this.methodInfo = methodInfo;
            this.serviceProvider = serviceProvider;
        }

        object? IMethodInfo.Invoke(object? fixture, params object?[]? args)
        {
            var resolvedArguments = ResolveArgs(args ?? Enumerable.Empty<object?>()).ToArray();
            return methodInfo.Invoke(fixture, resolvedArguments);
        }

        private IEnumerable<object?> ResolveArgs(IEnumerable<object?> args)
        {
            return
                from arg in args
                let serviceType = (arg as IInjectedService)?.GetServiceType()
                select serviceType is null ? arg : serviceProvider.GetRequiredService(serviceType);
        }
    }
}