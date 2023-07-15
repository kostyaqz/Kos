using System;

namespace VacationTests.Claims
{
    // Builder – это тоже класс
    // название должно быть обязательно <имя класса, для которого создаем Builder>Builder
    public class ClaimBuilder
    {
        private const string DefaultUserId = "1";
        private string id = new Random().Next(101).ToString();
        private ClaimType type = ClaimType.Paid;
        private ClaimStatus status = ClaimStatus.NonHandled;
        private string userId = DefaultUserId;
        private int? childAgeInMonths;
        private Director director = DirectorBuilder.ADefaultDirector();
        private DateTime startDate = DateTime.Now.Date.AddDays(7);
        private DateTime endDate = DateTime.Now.Date.AddDays(7 + 5);
        private bool paidNow = false;

        public ClaimBuilder()
        {
        }

        // для каждого свойства создаем метод With<название свойства>, возвращающий ClaimBuilder
        // метод принимает новое значение свойства, записывает это значение в переменную, созданную выше
        // с помощью таких методов можно будет задать необходимые поля
        public ClaimBuilder WithId(string newId)
        {
            id = newId;
            return this;
        }

        public ClaimBuilder WithType(ClaimType newClaimType)
        {
            type = newClaimType;
            return this;
        }

        public ClaimBuilder WithStatus(ClaimStatus newStatus)
        {
            status = newStatus;
            return this;
        }

        public ClaimBuilder WithUserId(string newUserId)
        {
            userId = newUserId;
            return this;
        }

        // можно создать перегрузку метода для удобных входных данных
        public ClaimBuilder WithUserId(int newUserId)
        {
            userId = newUserId.ToString();
            return this;
        }

        public ClaimBuilder WithChildAgeInMonths(int newChildAgeInMonths)
        {
            childAgeInMonths = newChildAgeInMonths;
            return this;
        }

        public ClaimBuilder WithDirector(Director newDirector)
        {
            director = newDirector;
            return this;
        }

        public ClaimBuilder WithStartDate(DateTime newStartDate)
        {
            startDate = newStartDate.Date;
            return this;
        }

        public ClaimBuilder WithEndDate(DateTime newEndDate)
        {
            endDate = newEndDate.Date;
            return this;
        }

        public ClaimBuilder WithPeriod(DateTime newStartDate, DateTime newEndDate)
        {
            if (newEndDate < newStartDate)
                throw new("Дата начала отпуска должна быть раньше даты конца отпуска");
            if (newEndDate < newStartDate.AddDays(3))
                throw new("Минимальный период отпуска должен быть 3 дня");
            startDate = newStartDate;
            endDate = newEndDate;
            return this;
        }

        public ClaimBuilder WithPaidNow(bool newPaidNow)
        {
            paidNow = newPaidNow;
            return this;
        }

        // основной метод, который возвращает экземпляр класса Claim
        public Claim Build() => new(id, type, status, director, startDate, endDate, childAgeInMonths, userId, paidNow);

        // статический метод, который возвращает экземпляр класса ClaimBuilder вместе со значениями по умолчанию
        public static ClaimBuilder AClaim()
        {
            return new();
        }

        // статический метод, который возвращает экземпляр класса Claim вместе со значениями по умолчанию
        // для случая, если нам никакие данные не нужны
        public static Claim ADefaultClaim() => AClaim().Build();

        // статический метод, который возвращает экземпляр класса Claim
        // для случая, если нам важен отпуск по уходу за ребёнком
        public static ClaimBuilder AChildClaim() => AClaim()
            .WithType(ClaimType.Child)
            .WithChildAgeInMonths(new Random().Next(1, 101));
    }

    public class DirectorBuilder
    {
        private int id = 14;
        private string name = "Бублик Владимир Кузьмич";
        private string position = "Директор департамента";

        public DirectorBuilder WithId(int newId)
        {
            id = newId;
            return this;
        }

        public DirectorBuilder WithName(string newName)
        {
            name = newName;
            return this;
        }

        public DirectorBuilder WithPosition(string newPosition)
        {
            position = newPosition;
            return this;
        }

        public Director Build() => new(id, name, position);

        public static DirectorBuilder ADirector()
        {
            return new();
        }

        public static Director ADefaultDirector() => ADirector().Build();

        public static Director TheSuperDirector()
        {
            return ADirector().WithId(24320).WithName("Кирпичников Алексей Николаевич")
                .WithPosition("Руководитель управления").Build();
        }
    }
}