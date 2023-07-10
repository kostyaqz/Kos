using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Portal : ControlBase
    {
        public Portal(IContextBy contextBy) : base(contextBy)
        {
        }

        public IWebElement GetPortalElement()
        {
            Present.Wait().EqualTo(true);
            var renderContainerId = Container.GetAttribute("data-render-container-id");
            try
            {
                return Container.Root()
                    .SearchElement(By.CssSelector($"[data-rendered-container-id='{renderContainerId}']"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}