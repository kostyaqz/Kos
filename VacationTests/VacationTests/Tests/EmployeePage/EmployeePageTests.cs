using NUnit.Framework;
using VacationTests.Infrastructure;

namespace VacationTests.Tests.EmployeePage
{
    public class EmployeePageTests : VacationTestBase
    {
        [Test]
        public void ControlVisibilityTest()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            page.TitleLabel.Text.Wait().EqualTo("Список отпусков");
            page.ClaimsTab.Visible.Wait().EqualTo(true);
            page.ClaimsTab.Text.Wait().EqualTo("🌴 Заявления на отпуск");
            page.CreateButton.Visible.Wait().EqualTo(true);
            page.CreateButton.Disabled.Wait().EqualTo(false);
            page.CreateButton.Text.Wait().EqualTo("Создать");

            page.Footer.KnowEnvironmentLink.Visible.Wait().EqualTo(true);
            page.Footer.KnowEnvironmentLink.Text.Wait().EqualTo("Узнать окружение");
            page.Footer.OurFooterLink.Visible.Wait().EqualTo(true);
            page.Footer.OurFooterLink.Text.Wait().EqualTo("Наш футер");
        }
    }
}