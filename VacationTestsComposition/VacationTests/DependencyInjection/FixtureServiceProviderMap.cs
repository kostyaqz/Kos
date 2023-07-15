using System;

namespace VacationTests.DependencyInjection
{
    internal class FixtureServiceProviderMap
    {
        private IServiceProvider serviceScope;

        internal IServiceProvider GetScope() => serviceScope;

        internal void AddScope(IServiceProvider sp) => serviceScope = sp;
    }
}
