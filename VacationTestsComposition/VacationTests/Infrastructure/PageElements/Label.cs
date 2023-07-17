using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;

namespace VacationTests.Infrastructure.PageElements
{
    public class Label : ControlBase
    {
        private readonly Clicker clicker;
        private readonly TextProvider textProvider;

        public Label(IWebElement element, Clicker clicker, TextProvider textProvider)
            : base(element)
        {
            this.clicker = clicker;
            this.textProvider = textProvider;
        }

        public void Click() => clicker.Click(container);

        public string Text => textProvider.Get(container);
    }
}