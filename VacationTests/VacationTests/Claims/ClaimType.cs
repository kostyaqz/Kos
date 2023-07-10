using System.ComponentModel;
using System.Runtime.Serialization;

namespace VacationTests.Claims
{
    public enum ClaimType
    {
        // TODO pe: Это подозрительная фигня: мы храним в локал-сторадже какие-то русские слова, вместо идентификаторов?
        // TODO pe: Кажется это надо переделать в самом приложении.

        [Description("Дополнительный")] [EnumMember(Value = "Дополнительный")]
        Additional,

        [Description("По уходу за ребенком")] [EnumMember(Value = "По уходу за ребенком")]
        Child,

        [Description("Не оплачиваемый")] [EnumMember(Value = "Не оплачиваемый")]
        NotPaid,

        [Description("Основной")] [EnumMember(Value = "Основной")]
        Paid,

        [Description("На учебу")] [EnumMember(Value = "На учебу")]
        Study
    }
}