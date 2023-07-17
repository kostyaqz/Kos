using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VacationTests.DependencyInjection;
using VacationTests.DependencyInjection.MethodInject;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.Navigation
{
    public class NavigationExercise1Tests : IFixtureWithServiceProviderFramework
    {
        public void ConfigureServices(IServiceCollection collection) => collection.AddWeb();

        [Test]
        public void LoginPage_EmployeeButtonTest([Inject] PageNavigation.Navigation navigation)
        {
            var enterPage = navigation.OpenLoginPage();
            enterPage.LoginAsEmployeeButton.Present.Wait().EqualTo(true);
            enterPage.LoginAsEmployeeButton.Text.Wait().EqualTo("Я сотрудник");
        }
    }
}