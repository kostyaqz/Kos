using Kontur.Selone.Pages;
using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageNavigation
{
    public class Navigation
    {
        private readonly ControlFactory controlFactory;
        private readonly IWebDriver webDriver;

        public Navigation(IWebDriver webDriver, ControlFactory controlFactory)
        {
            this.webDriver = webDriver;
            this.controlFactory = controlFactory;
        }

        public LoginPage OpenLoginPage()
        {
            return OpenPage<LoginPage>(Urls.LoginPage);
        }

        public EmployeeVacationListPage OpenEmployeeVacationListPage(string employeeId = "1")
        {
            var isCurrentPageIsEmployeePage = webDriver.Url.Contains("user");
            var page = OpenPage<EmployeeVacationListPage>(Urls.EmployeeVacationListPage(employeeId));

            // на фронте баг со страницей сотрудника
            // если находишься на странице сотрудника и меняешь цифру с id сотрудником в урле, то страница не обновляется
            // пока баг не исправлен, вставляем принудительный рефреш страницы
            if (isCurrentPageIsEmployeePage)
                page.Refresh();
            return page;
        }

        public AdminVacationListPage OpenAdminVacationListPage()
        {
            return OpenPage<AdminVacationListPage>(Urls.AdminVacationListPage);
        }

        public TPageObject OpenPage<TPageObject>(string url)
            where TPageObject : PageBase
        {
            webDriver.Navigate().GoToUrl(url);
            webDriver.Navigate().Refresh();
            return controlFactory.CreatePage<TPageObject>(webDriver);
        }
    }
}