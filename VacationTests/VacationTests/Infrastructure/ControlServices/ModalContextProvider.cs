using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlServices
{
    public class ModalContextProvider
    {
        private readonly PortalContextProvider portalContextProvider;
        private readonly IWebDriver webDriver;

        public ModalContextProvider(IWrapsDriver webDriver, PortalContextProvider portalContextProvider)
        {
            this.portalContextProvider = portalContextProvider;
            this.webDriver = webDriver.WrappedDriver;
        }

        public IWebElement Get(string dataTid)
        {
            var webElement = webDriver.SearchElement(x => x.WithTid(dataTid));
            return portalContextProvider.Get(webElement);
        }

    }
}