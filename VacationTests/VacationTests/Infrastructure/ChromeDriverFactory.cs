using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VacationTests.Infrastructure
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ChromeDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            var chromeDriver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromSeconds(180));
            chromeDriver.Manage().Window.SetSize(1280, 960);
            return chromeDriver;
        }
    }
}