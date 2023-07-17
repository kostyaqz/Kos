using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlServices
{
    public class Clicker
    {
        public void Click(IWebElement webElement) => webElement.Click();
    }
}