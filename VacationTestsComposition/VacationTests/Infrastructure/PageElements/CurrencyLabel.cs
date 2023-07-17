using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyLabel : ControlBase
    {
        private readonly CurrencyValueProvider currencyValueProvider;

        public CurrencyLabel(IWebElement element, CurrencyValueProvider currencyValueProvider)
            : base(element)
        {
            this.currencyValueProvider = currencyValueProvider;
        }

        public decimal Sum => currencyValueProvider.Get(container);
    }
}