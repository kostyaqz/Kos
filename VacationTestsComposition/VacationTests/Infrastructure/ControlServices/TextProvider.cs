using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlServices
{
    public class TextProvider
    {
        private readonly Waiter waiter;

        public TextProvider(Waiter waiter)
        {
            this.waiter = waiter;
        }

        public string Get(IWebElement webElement)
        {
            waiter.WaitPresent(webElement);
            return webElement.Text;
        }
    }
}