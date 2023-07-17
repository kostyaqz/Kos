using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    // Об enum https://ulearn.me/course/basicprogramming/Konstanty_i_enum_y_f1740706-b8e2-4bd4-ab87-3cc710a52449
    public enum ClaimType
    {
        // TODO pe: Это подозрительная фигня: мы храним в локал-сторадже какие-то русские слова, вместо идентификаторов?
        // TODO pe: Кажется это надо переделать в самом приложении.

        [System.ComponentModel.Description("Дополнительный")] [EnumMember(Value = "Дополнительный")]
        Additional,

        [System.ComponentModel.Description("По уходу за ребенком")] [EnumMember(Value = "По уходу за ребенком")]
        Child,

        [System.ComponentModel.Description("Не оплачиваемый")] [EnumMember(Value = "Не оплачиваемый")]
        NotPaid,

        [System.ComponentModel.Description("Основной")] [EnumMember(Value = "Основной")]
        Paid,

        [System.ComponentModel.Description("На учебу")] [EnumMember(Value = "На учебу")]
        Study
    }

    public enum ClaimStatus
    {
        [System.ComponentModel.Description("Согласовано")]
        Accepted = 0,

        [System.ComponentModel.Description("Отклонено")]
        Rejected = 1,

        [System.ComponentModel.Description("На согласовании")]
        NonHandled = 2
    }

    public record Claim(
        string Id,
        [property: JsonConverter(typeof(StringEnumConverter))]
        ClaimType Type,
        ClaimStatus Status,
        Director Director,
        DateTime StartDate,
        DateTime EndDate,
        int? ChildAgeInMonths,
        string UserId,
        bool PaidNow)
    {
        public static Claim CreateDefault()
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();
            var defaultUser = Guid.NewGuid().ToString();

            return new(
                randomClaimId,
                ClaimType.Paid,
                ClaimStatus.NonHandled,
                Director.CreateDefault(),
                DateTime.Now.Date.AddDays(7),
                DateTime.Now.Date.AddDays(7 + 5),
                null,
                defaultUser, // для студентов "1"
                false
            );
        }

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

    public record Director(int Id, string Name, string Position)
    {
        public static Director CreateDefault()
        {
            return new(Id: 14, Name: "Бублик Владимир Кузьмич", Position: "Директор департамента");
        }

        public static Director CreateSuperDirector()
        {
            return new(Id: 24320, Name: "Кирпичников Алексей Николаевич", Position: "Руководитель управления");
        }
    }
}