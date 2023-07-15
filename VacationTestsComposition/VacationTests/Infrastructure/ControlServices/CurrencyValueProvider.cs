using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.ControlServices
{
    public class CurrencyValueProvider
    {
        private readonly TextProvider textProvider;
        private readonly CurrencyTransformation currencyTransformation;

        public CurrencyValueProvider(TextProvider textProvider, CurrencyTransformation currencyTransformation)
        {
            this.textProvider = textProvider;
            this.currencyTransformation = currencyTransformation;
        }

        public decimal Get(IWebElement webElement)
        {
            var text = textProvider.Get(webElement);
            return currencyTransformation.Deserialize(text);
        }
    }
}