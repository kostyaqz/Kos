using System.Globalization;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyInput : Input
    {
        public CurrencyInput(IContextBy contextBy) : base(contextBy)
        {
        }

        public IProp<decimal> Sum => Value.Currency();

        public void ClearAndInputCurrency(decimal value)
        {
            ClearAndInputText(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}