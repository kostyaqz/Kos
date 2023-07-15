using System;
using Microsoft.Extensions.ObjectPool;
using OpenQA.Selenium;
using VacationTests.Tests;

namespace VacationTests.Infrastructure.WebDrivers
{
    internal class PooledWebDriver : IWrapsDriver, IDisposable
    {
        private readonly ObjectPool<WebDriver> pool;
        private readonly WebDriver driver;

        public PooledWebDriver(WebDriverPoolCache cache)
        {
            pool = cache.WebDriverPool;
            driver = pool.Get();
        }

        public IWebDriver WrappedDriver => driver;

        public void Dispose() => pool.Return(driver);
    }
}