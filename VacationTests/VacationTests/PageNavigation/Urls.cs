namespace VacationTests.PageNavigation
{
    public static class Urls
    {
        private const string Host = "https://test-automation-course.gitlab-pages.kontur.host/for-course";

        // Использовать для локального запуска сервиса Отпуска
        // private const string Host = "http://localhost:8080";
        public const string LoginPage = Host + "/#/";
        public const string AdminVacationListPage = Host + "#/admin";

        public static string EmployeeVacationListPage(string employeeId)
        {
            return Host + $"/#/user/{employeeId}";
        }
    }
}