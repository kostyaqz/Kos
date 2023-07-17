using System.Globalization;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyInput : Input
    {
        public CurrencyInput(IWebElement element)
            : base(element)
        {
        }

        public IProp<decimal> Sum => Value.Currency();

        public void ClearAndInputCurrency(decimal value)
        {
            ClearAndInputText(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}