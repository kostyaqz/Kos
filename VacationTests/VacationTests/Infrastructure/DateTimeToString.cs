using System;

namespace VacationTests.Infrastructure
{
    public class DateTimeToString
    {
        public static string CreateStringPeriodFromDateTime(DateTime startDate, DateTime endDate)
        {
            var firstPart = startDate.ToString("dd.MM.yyyy");
            var secondPart = endDate.ToString("dd.MM.yyyy");
            return $"{firstPart} - {secondPart}";
        }
    }
}