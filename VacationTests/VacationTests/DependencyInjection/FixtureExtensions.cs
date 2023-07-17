using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal;

namespace VacationTests.DependencyInjection
{
    public static class FixtureExtensions
    {
        public static TService GetRequiredService<TService>(this IFixtureWithServiceProviderFramework _)
            where TService : notnull =>
            TestExecutionContext.CurrentContext.CurrentTest
                .GetServiceProvider()
                .GetRequiredService<TService>();
    }
}