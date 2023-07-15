using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Select : ControlBase
    {
        private readonly PortalContextProvider portalContextProvider;
        private readonly ControlFactory controlFactory;

        public Select(IWebElement element, PortalContextProvider portalContextProvider, ControlFactory controlFactory)
            : base(element)
        {
            this.portalContextProvider = portalContextProvider;
            this.controlFactory = controlFactory;
        }

        public IProp<string> Value => container.ReactValue();

        // todo в параллели бывает нестабильность
        public void SelectValueByText(string text)
        {
            container.Click();

            var portalContext = portalContextProvider.Get(container.SearchElement(x => x.Css().Tag("noscript")));
            var itemSelector = $".//*[contains(@data-comp-name,'MenuItem CommonWrapper')][contains(.,'{text}')]";

            var item = controlFactory.Create<Button>(portalContext, By.XPath(itemSelector));
            item.Visible.Wait().EqualTo(true);
            item.Click();
        }
    }
}