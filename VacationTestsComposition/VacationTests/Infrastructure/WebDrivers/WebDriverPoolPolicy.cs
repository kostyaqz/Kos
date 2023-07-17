using Kontur.Selone.Extensions;
using Microsoft.Extensions.ObjectPool;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.WebDrivers
{
    public class WebDriverPoolPolicy : IPooledObjectPolicy<WebDriver>
    {
        private readonly ChromeDriverFactory chromeDriverFactory;

        public WebDriverPoolPolicy(ChromeDriverFactory chromeDriverFactory)
        {
            this.chromeDriverFactory = chromeDriverFactory;
        }

        public WebDriver Create() => chromeDriverFactory.Create();

        public bool Return(WebDriver obj)
        {
            obj.ResetWindows();
            return true;
        }
    }
}