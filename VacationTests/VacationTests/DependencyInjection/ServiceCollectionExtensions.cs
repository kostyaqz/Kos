#nullable enable
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace VacationTests.DependencyInjection
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection RegisterFixtures(
            this IServiceCollection serviceCollection,
            ITest? currentTest)
        {
            while (currentTest != null)
            {
                if (currentTest.Fixture is { } fixture)
                {
                    serviceCollection.AddSingleton(fixture.GetType(), fixture);
                }

                currentTest = currentTest.Parent;
            }

            return serviceCollection;
        }
    }
}
