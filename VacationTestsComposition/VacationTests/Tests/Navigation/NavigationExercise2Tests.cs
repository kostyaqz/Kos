using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VacationTests.DependencyInjection;
using VacationTests.DependencyInjection.MethodInject;

namespace VacationTests.Tests.Navigation
{
    public class NavigationExercise2Tests : IFixtureWithServiceProviderFramework
    {
        public void ConfigureServices(IServiceCollection collection) => collection.AddWeb();

        [Test]
        public void LoginPage_TitleTest([Inject] PageNavigation.Navigation navigation)
        {
            var enterPage = navigation.OpenLoginPage();
            enterPage.TitleLabel.Text.Should().Be("Вход в сервис");
        }
    }
}