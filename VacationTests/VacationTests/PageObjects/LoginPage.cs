using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using Kontur.Selone.Waiting;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

// О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
namespace VacationTests.PageObjects
{
    public class LoginPage : PageBase, ILoadable
    {
        public LoginPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            // Искать элемент по tid можно с помощью Css().WithTid("...")) - метод Selone
            TitleLabel = controlFactory.CreateControl<Label>(webDriver.Search(x => x.Css().WithTid("TitleLabel")));

            // Можно упростить написание для частых поисков, и создать свой метод WithTid(), чтобы опустить Css(),
            // этот метод будет вызывать Css().WithTid("..."))
            LoginAsEmployeeButton =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("LoginAsEmployeeButton")));
            Footer = controlFactory.CreateControl<PageFooter>(webDriver.Search(x => x.WithTid("Footer")));
            LoginAsAdminButton =
                controlFactory.CreateControl<Button>(webDriver.Search(x => x.WithTid("LoginAsAdminButton")));
        }

        public Label TitleLabel { get; }
        public Button LoginAsEmployeeButton { get; }
        public PageFooter Footer { get; }
        public Button LoginAsAdminButton { get; }

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