using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ControlBase : IHaveContainer
    {
        protected IWebElement Container;

        protected ControlBase(IContextBy contextBy)
        {
            Container = contextBy.SearchContext.SearchElement(contextBy.By);
        }

        public IProp<bool> Present => Container.Present(); // Typo IsPreset. Expression reflection
        public IProp<bool> Visible => Container.Visible();
        public IProp<bool> Disabled => Container.Disabled();
        IWebElement IHaveContainer.Container => Container;

        public void Click()
        {
            Container.Click();
        }

        public override string ToString()
        {
            try
            {
                return $"{Container.TagName} {Container.Text}";
            }
            catch (StaleElementReferenceException)
            {
                return "StaleElement (not found in DOM)";
            }
        }
    }
}