using FluentAssertions;
using NUnit.Framework;

namespace VacationTests.Tests.Navigation
{
    public class NavigationTests : VacationTestBase
    {
        [Test]
        public void LoginPage_EmployeeButtonTest()
        {
            var enterPage = Navigation.OpenLoginPage();
            enterPage.WaitLoaded();

            var adminPage = enterPage.LoginAsAdmin();

            adminPage.IsAdminPage.Should().BeTrue();
        }
    }
}