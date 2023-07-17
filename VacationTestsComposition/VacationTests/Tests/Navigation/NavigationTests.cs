using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VacationTests.DependencyInjection;
using VacationTests.DependencyInjection.MethodInject;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : IFixtureWithServiceProviderFramework
    {
        public void ConfigureServices(IServiceCollection collection) => collection.AddWeb();

        [Test]
        public void LoginPage_LoginAsAdmin_AdminPageOpened([Inject] PageNavigation.Navigation navigation)
        {
            var enterPage = navigation.OpenLoginPage();
            enterPage.WaitLoaded();

            var adminPage = enterPage.LoginAsAdmin();

            adminPage.TitleLabel.Text.Should().Be("Список отпусков");
        }
    }
}