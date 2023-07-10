using VacationTests.Claims;

namespace VacationTests.Data
{
    public static class Directors
    {
        public static Director Default => new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
        public static Director SuperDirector => new Director(24320, "Кирпичников Алексей Николаевич", "Руководитель управления");
        public static DirectorBuilder New => new DirectorBuilder();
    }
}