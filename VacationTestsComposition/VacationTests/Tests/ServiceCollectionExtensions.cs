using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using OpenQA.Selenium;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.ControlServices.Properties;
using VacationTests.Infrastructure.WebDrivers;
using VacationTests.PageObjects;

namespace VacationTests.Tests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmployeeGenerator(this IServiceCollection collection)
            => collection
                .AddScoped<EmployeeGenerator>();

        public static IServiceCollection AddWeb(this IServiceCollection collection)
            => collection
                .AddSingleton<WebDriverPoolCache>()
                .AddScoped<IWrapsDriver, PooledWebDriver>()
                .AddScoped(sp => sp.GetRequiredService<IWrapsDriver>().WrappedDriver.Navigate())
                .AddScoped<PageNavigation.Navigation>()
                .AddScoped<LocalStorage>()
                .AddScoped<ClaimStorage>()
                .AddScoped(typeof(PageFactory<>))
                .AddScoped<ControlFactory>()
                .AddScoped<ElementsCollectionFactory>()
                .AddEmployeeGenerator()
                .AddScoped<AdminVacationListPage>()
                .AddScoped<ClaimCreationPage>()
                .AddScoped<ClaimLightbox>()
                .AddScoped<EmployeeVacationListPage>()
                .AddScoped<InfoSidePage>()
                .AddScoped<LoginPage>()
                .AddScoped<PortalContextProvider>()
                .AddScoped(typeof(ControlFactory<>))
                .AddScoped<ModalContextProvider>()
                .AddSingleton<ChromeDriverFactory>()
                .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
                .AddSingleton<IPooledObjectPolicy<WebDriver>, WebDriverPoolPolicy>()
                .AddScoped<Clicker>()
                .AddScoped<TextProvider>()
                .AddSingleton<CurrencyTransformation>()
                .AddScoped<CurrencyValueProvider>()
                .AddScoped<Waiter>();
    }
}