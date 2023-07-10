namespace VacationTests.Claims
{
    public class DirectorBuilder
    {
        private int directorId = 14;
        private string directorName = "Бублик Владимир Кузьмич";
        private string position = "Директор департамента";

        public DirectorBuilder WithId(int newDirectorId)
        {
            directorId = newDirectorId;
            return this;
        }

        public DirectorBuilder WithName(string newDirectorName)
        {
            directorName = newDirectorName;
            return this;
        }

        public DirectorBuilder WithPosition(string newPosition)
        {
            position = newPosition;
            return this;
        }

        public Director Build() => new (directorId, directorName, position);
    }
}