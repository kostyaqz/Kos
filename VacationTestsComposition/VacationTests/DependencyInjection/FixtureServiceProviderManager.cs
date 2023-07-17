using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace VacationTests.DependencyInjection
{
    internal sealed class FixtureServiceProviderManager : IServiceProvider, IAsyncDisposable
    {
        private static readonly ServiceProviderOptions Options = new()
        {
            ValidateOnBuild = true,
            ValidateScopes = true,
        };

        private readonly ConcurrentDictionary<ITest, IAsyncDisposable> scopes = new();
        private readonly ServiceProvider serviceProvider;

        internal FixtureServiceProviderManager(Action<IServiceCollection> configureServices)
        {
            ServiceCollection serviceCollection = new();
            configureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider(Options);
        }

        public async ValueTask DisposeAsync() => await serviceProvider.DisposeAsync().ConfigureAwait(false);

        public object GetService(Type serviceType) => serviceProvider.GetService(serviceType);

        internal IServiceProvider CreateScope(ITest test)
        {
            var serviceScope = serviceProvider.CreateAsyncScope();
            return scopes.TryAdd(test, serviceScope)
                ? serviceScope.ServiceProvider
                : throw new InvalidOperationException($"Failed to store service scope for {test.FullName}");
        }

        internal async Task DisposeScopeAsync(ITest test)
        {
            if (!scopes.TryRemove(test, out var scope))
            {
                throw new InvalidOperationException($"Failed to receive stored scope for {test.FullName}");
            }

            await scope.DisposeAsync().ConfigureAwait(false);
        }
    }
}
