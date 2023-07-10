using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VacationTests.Data;

namespace VacationTests.Claims
{
    public record Claim(string Id, [property: JsonConverter(typeof(StringEnumConverter))]
        ClaimType Type, ClaimStatus Status, Director Director, DateTime StartDate, DateTime EndDate,
        int? ChildAgeInMonths, string UserId, bool PaidNow)
    {
        public static Claim CreateDefault()
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();
            var startDate = DateTime.Today.AddDays(7).Date;
            var endDate = DateTime.Today.AddDays(12).Date;

            return new Claim(randomClaimId, ClaimType.Paid, ClaimStatus.NonHandled, Directors.Default,
                startDate, endDate, null, "1", false);
        }

        // можно также добавить второй метод для создания типичного заявления по уходу за ребёнком
        public static Claim CreateChildType()
        {
            var random = new Random();
            var childAgeInMonths = random.Next(1, 101);
            return CreateDefault() with
            {
                Type = ClaimType.Child,
                ChildAgeInMonths = childAgeInMonths
            };
        }
    }
}