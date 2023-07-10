using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.ControlTests
{
    public class CheckboxTests : VacationTestBase
    {
        [Test]
        public void Example()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            var claimPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimPage.PayNowCheckbox.Checked.Wait().That(Is.False);
            claimPage.PayNowCheckbox.SetChecked();
            claimPage.PayNowCheckbox.Checked.Wait().EqualTo(true);
        }
    }
}