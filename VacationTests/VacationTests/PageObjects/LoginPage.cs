using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    [InjectControls]
    public class LoginPage : PageBase, ILoadable
    {
        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Label TitleLabel { get; private set; }
        public Button LoginAsEmployeeButton { get; private set; }
        public PageFooter Footer { get; private set; }
        public Button LoginAsAdminButton { get; private set; }



        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen<EmployeeVacationListPage>();
        }

        public AdminVacationListPage LoginAsAdmin()
        {
            return LoginAsAdminButton.ClickAndOpen<AdminVacationListPage>();
        }

        public void WaitLoaded(int? timeout = null)
        {
            LoginAsAdminButton.Present.Wait().EqualTo(true, timeout);
            LoginAsEmployeeButton.Present.Wait().EqualTo(true, timeout);
        }
    }
}