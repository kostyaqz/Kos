using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.PageObjects;

namespace VacationTests.PageNavigation
{
    public class Navigation
    {
        private readonly PageFactory<LoginPage> loginPageFactory;
        private readonly PageFactory<EmployeeVacationListPage> employeeVacationListPageFactory;
        private readonly PageFactory<AdminVacationListPage> adminVacationListPageFactory;
        private readonly INavigation navigation;

        public Navigation(
            PageFactory<LoginPage> loginPageFactory,
            PageFactory<EmployeeVacationListPage> employeeVacationListPageFactory,
            PageFactory<AdminVacationListPage> adminVacationListPageFactory,
            INavigation navigation)
        {
            this.loginPageFactory = loginPageFactory;
            this.employeeVacationListPageFactory = employeeVacationListPageFactory;
            this.adminVacationListPageFactory = adminVacationListPageFactory;
            this.navigation = navigation;
        }

        private TPage GoToUrl<TPage>(PageFactory<TPage> pageFactory, string url)
        {
            navigation.GoToUrl(url);
            return pageFactory.CreatePage();
        }

        public LoginPage OpenLoginPage() => GoToUrl(loginPageFactory, Urls.LoginPage);

        public void Refresh() => navigation.Refresh();

        public EmployeeVacationListPage OpenEmployeeVacationList(string employeeId = "1") =>
            GoToUrl(employeeVacationListPageFactory, Urls.EmployeeVacationList(employeeId));

        public AdminVacationListPage OpenAdminVacationList() =>
            GoToUrl(adminVacationListPageFactory, Urls.AdminVacationList);
    }
}