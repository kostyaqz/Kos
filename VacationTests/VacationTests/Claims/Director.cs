namespace VacationTests.Claims
{
    public record Director(int Id, string Name, string Position)
    {
        public static Director CreateDefault()
        {
            return new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
        }

        public static Director CreateSuperDirector()
        {
            return new Director(24320, "Кирпичников Алексей Николаевич", "Руководитель управления");
        }
    }
}