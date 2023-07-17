using Kontur.Selone.Selectors.Css;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    public class LoginPage
    {
        public Label TitleLabel { get; private set; }
        public Button<EmployeeVacationListPage> LoginAsEmployeeButton { get; private set; }
        public Button<AdminVacationListPage> LoginAsAdminButton { get; private set; }
        public PageFooter Footer { get; private set; }

        public EmployeeVacationListPage LoginAsEmployee()
        {
            return LoginAsEmployeeButton.ClickAndOpen();
        }

        public AdminVacationListPage LoginAsAdmin()
        {
            return LoginAsAdminButton.ClickAndOpen();
        }

        public void WaitLoaded(int? timeout = null)
        { 
            LoginAsEmployeeButton.Present.Wait().EqualTo(true);
            LoginAsAdminButton.Present.Wait().EqualTo(true);
        }
    }
}