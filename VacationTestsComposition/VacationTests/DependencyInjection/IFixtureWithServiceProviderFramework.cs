using Microsoft.Extensions.DependencyInjection;

namespace VacationTests.DependencyInjection
{
    [DependencyInjection]
    public interface IFixtureWithServiceProviderFramework
    {
        void ConfigureServices(IServiceCollection collection);
    }
}