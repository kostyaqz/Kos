using System;
using System.Globalization;
using Kontur.Selone.Properties;

namespace VacationTests.Infrastructure.ControlServices.Properties
{
    public class DateTimeTransformation : IPropTransformation<DateTime>
    {
        public DateTime Deserialize(string value)
        {
            return DateTime.Parse(value, new CultureInfo("ru-Ru"));
        }

        public string Serialize(DateTime value)
        {
            return value.ToString("d", new CultureInfo("ru-RU"));
        }
    }
}