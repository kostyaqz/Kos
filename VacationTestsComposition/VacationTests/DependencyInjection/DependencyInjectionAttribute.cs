using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using VacationTests.DependencyInjection.MethodInject;
using VacationTests.DependencyInjection.Singletones;

namespace VacationTests.DependencyInjection
{
    internal class DependencyInjectionAttribute : NUnitAttribute, ITestAction
    {
        private FixtureServiceProviderManager manager;
        
        public void BeforeTest(ITest test)
        {
            if (test.IsSuite)
            {
                BeforeTestSuite(test);
            }
            else
            {
                BeforeTestCase(test);
            }
        }

        public void AfterTest(ITest test)
        {
            if (test.IsSuite)
            {
                AfterTestSuite();
            }
            else
            {
                AfterTestCase(test);
            }
        }

        private void BeforeTestSuite(ITest test)
        {
            var testFixture = test.Fixture;
            if (testFixture is not IFixtureWithServiceProviderFramework fixture)
            {
                throw new InvalidOperationException(
                    $"Test {test.FullName} with fixture {testFixture} do not implement {nameof(IFixtureWithServiceProviderFramework)}");
            }

            manager = new(serviceCollection =>
            {
                serviceCollection.AddSingleton(fixture)
                    .AddSingleton(test)
                    .AddScoped<TestAccessor>()
                    .AddScoped(sp => sp.GetRequiredService<TestAccessor>().Test)
                    .RegisterFixtures(test);

                fixture.ConfigureServices(serviceCollection);
            });

            var dependencies = testFixture.GetTestDependencies();
            dependencies.InitializeDependencies(manager.GetRequiredService);
        }
        
        private void BeforeTestCase(ITest test)
        {
            var scopeServiceProvider = manager.CreateScope(test);

            var originalMethodInfo = test.Method;
            var testMethod = (TestMethod)test;
            scopeServiceProvider.GetRequiredService<TestAccessor>().Test = testMethod;

            var map = test.GetFixtureServiceProviderMap();
            map.AddScope(scopeServiceProvider);

            testMethod.Method = new UseContainerMethodInfo(originalMethodInfo, scopeServiceProvider);
        }

        private void AfterTestCase(ITest test) => manager.DisposeScopeAsync(test).GetAwaiter().GetResult();

        private void AfterTestSuite() => manager.DisposeAsync().GetAwaiter().GetResult();

        public ActionTargets Targets => ActionTargets.Suite | ActionTargets.Test;
    }
}