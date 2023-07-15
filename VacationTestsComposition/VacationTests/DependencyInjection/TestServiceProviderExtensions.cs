using System;
using NUnit.Framework.Interfaces;

namespace VacationTests.DependencyInjection
{
    public static class TestServiceProviderExtensions
    {
        private const string Key = "FixtureServiceProviderMap";

        public static IServiceProvider GetServiceProvider(this ITest test) => GetFixtureServiceProviderMap(test).GetScope();

        internal static FixtureServiceProviderMap GetFixtureServiceProviderMap(this ITest test)
        {
            var properties = test.Properties;
            if (properties.Get(Key) is FixtureServiceProviderMap alreadyCreatedValue)
            {
                return alreadyCreatedValue;
            }

            FixtureServiceProviderMap map = new();
            properties.Set(Key, map);
            return map;
        }
    }
}