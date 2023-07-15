using System;
using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VacationTests.Infrastructure.WebDrivers
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    public class ChromeDriverFactory
    {
        public WebDriver Create()
        {
            var chromeDriverService =
                ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            // использовать для запуска тестов на ulearn
            // chromeDriverService.SuppressInitialDiagnosticInformation = true;
            // options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions", "--disable-dev-shm-usage", "--headless");
            var chromeDriver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromSeconds(180));
            chromeDriver.Manage().Window.SetSize(1280, 960);
            return chromeDriver;
        }
    }
}