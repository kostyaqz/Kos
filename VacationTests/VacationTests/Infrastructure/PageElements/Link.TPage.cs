using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;

namespace VacationTests.Infrastructure.PageElements
{
    public class Link<TPage> : ControlBase
    {
        private readonly PageFactory<TPage> pageFactory;
        private readonly Clicker clicker;
        private readonly TextProvider textProvider;

        public Link(
            IWebElement element,
            PageFactory<TPage> pageFactory,
            Clicker clicker,
            TextProvider textProvider)
            : base(element)
        {
            this.pageFactory = pageFactory;
            this.clicker = clicker;
            this.textProvider = textProvider;
        }

        public string Text => textProvider.Get(container);

        public void Click() => clicker.Click(container);

        public TPage ClickAndOpen()
        {
            Click();
            return pageFactory.CreatePage();
        }
    }
}