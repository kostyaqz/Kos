using System.ComponentModel;

namespace VacationTests.Claims
{
    public enum ClaimStatus
    {
        [Description("Согласовано")] Accepted = 0,

        [Description("Отклонено")] Rejected = 1,

        [Description("На согласовании")] NonHandled = 2
    }
}