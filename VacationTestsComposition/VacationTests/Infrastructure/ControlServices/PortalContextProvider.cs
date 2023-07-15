using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlServices
{
    public class PortalContextProvider
    {
        private readonly IWebDriver webDriver;

        public PortalContextProvider(IWrapsDriver webDriver) =>
            this.webDriver = webDriver.WrappedDriver;

        public IWebElement Get(IWebElement webElement)
        {
            webElement.Present().Wait().EqualTo(true);
            var attribute = webElement.GetAttribute("data-render-container-id");
            return webDriver.SearchElement(x => x.Css().WithAttribute("data-rendered-container-id", attribute));
        }
    }
}