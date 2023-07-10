using System;
using VacationTests.Data;

namespace VacationTests.Claims
{
    // Builder – это тоже класс
    // Название должно быть обязательно <имя класса, для которого создаем Builder>Builder
    public class ClaimBuilder
    {
        private int? childAgeInMonths;
        private Director director = Directors.Default;

        private DateTime endDate = DateTime.Today.AddDays(12);

        // Инициализируем приватные поля со значениями по умолчанию для каждого свойства класса Claim
        private string id = new Random().Next(101).ToString();
        private bool paidNow;
        private DateTime startDate = DateTime.Today.AddDays(7);
        private ClaimStatus status = ClaimStatus.NonHandled;
        private ClaimType type = ClaimType.Paid;
        private string userId = "1";

        // Для каждого поля создаем метод With<название свойства>, возвращающий экземпляр этого DirectorBuilder
        // Метод принимает значение и записывает в соответствующее приватное поле
        // С помощью таких методов можно будет задать необходимые поляr
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

        public ClaimBuilder WithPaidNow(bool newPaidNow)
        {
            paidNow = newPaidNow;
            return this;
        }

        public ClaimBuilder WithPeriod(DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate >= newEndDate)
                throw new Exception("Дата начала отпуска должна быть раньше даты конца отпуска");
            if (newEndDate < newStartDate.AddDays(3))
                throw new Exception("Минимальный период отпуска должен быть 3 дня");

            startDate = newStartDate;
            endDate = newEndDate;

            return this;
        }

        // Основной метод, который возвращает экземпляр класса Claim
        public Claim Build()
        {
            return new Claim(
                id,
                type,
                status,
                director,
                startDate,
                endDate,
                childAgeInMonths,
                userId,
                paidNow
            );
        }
    }
}