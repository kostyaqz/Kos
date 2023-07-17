using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlServices
{
    public class Waiter
    {
        public void WaitPresent(IWebElement webElement)
        {
            webElement.Present().Wait().EqualTo(true);
        }
    }
}