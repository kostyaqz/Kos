using System;
using VacationTests.Claims;

namespace VacationTests.Data
{
    public static class Claims
    {
        public static ClaimBuilder New => new ClaimBuilder();
        public static Claim Default => new ClaimBuilder().Build();
        public static Claim AChildClaim => new ClaimBuilder()
            .WithType(ClaimType.Child)
            .WithChildAgeInMonths(new Random().Next(1, 101))
            .Build();
    }
}