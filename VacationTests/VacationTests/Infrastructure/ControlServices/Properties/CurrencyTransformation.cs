using System.Globalization;
using System.Linq;
using Kontur.Selone.Properties;

namespace VacationTests.Infrastructure.ControlServices.Properties
{
    public class CurrencyTransformation : IPropTransformation<decimal>
    {
        public decimal Deserialize(string value)
        {
            var prepared = string.Join("", value
                .Where(x => x != (char) 8201 && x != ' ' && x != '₽')
                .Select(x => x == (char) 8722 ? '-' : x == ',' ? '.' : x));
            return decimal.TryParse(prepared, NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture, out var sum)
                ? sum
                : decimal.Zero;
        }

        public string Serialize(decimal value)
        {
            var culture = CultureInfo.CreateSpecificCulture("fr-CA"); //формат 111111,000
            var specifier = "N2"; //пробел   между порядками, 2 знака после запятой -> 111 111,00
            return value.ToString(specifier, culture).Replace(" ", ((char) 8201).ToString());
        }
    }
}