#nullable enable
using System;

namespace VacationTests.DependencyInjection.MethodInject
{
    public class InjectedService<TService> : IInjectedService
    {
        public override string ToString() => $"Service<{typeof(TService).Name}>";

        public Type GetServiceType() => typeof(TService);
    }
}
