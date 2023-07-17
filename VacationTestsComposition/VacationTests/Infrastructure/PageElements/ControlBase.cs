using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ControlBase
    {
        protected readonly IWebElement container;

        protected ControlBase(IWebElement element)
        {
            container = element;
        }

        public IProp<bool> Present => container.Present(); // Typo IsPreset. Expression reflection
        public IProp<bool> Visible => container.Visible();
        public IProp<bool> Disabled => container.Disabled();
        public IWebElement Container => container;

        public override string ToString()
        {
            try
            {
                return $"{container.TagName} {container.Text}";
            }
            catch (StaleElementReferenceException)
            {
                return "StaleElement (not found in DOM)";
            }
        }
    }
}