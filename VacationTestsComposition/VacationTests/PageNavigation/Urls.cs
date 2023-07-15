namespace VacationTests.PageNavigation
{
    public static class Urls
    {
        private const string Host = "https://test-automation-course.gitlab-pages.kontur.host/for-course";

        // использовать для локального запуска сервиса Отпуска и для улерна
        //private const string Host = "http://localhost:8080";
        public const string LoginPage = Host + "/#/";
        public const string AdminVacationList = Host + "/#/admin"; //  Задание 2: должны добавить урл

        public static string EmployeeVacationList(string employeeId)
        {
            return Host + $"/#/user/{employeeId}";
        }
    }
}