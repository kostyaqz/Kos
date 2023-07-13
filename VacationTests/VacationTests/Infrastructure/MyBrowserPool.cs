using System.Collections.Concurrent;
using Kontur.Selone.Extensions;
using Kontur.Selone.WebDrivers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public static class MyBrowserPool
    {
        static MyBrowserPool()
        {
            pool = new WebDriverPool(new ChromeDriverFactory(), cleaner);
        }

        static IWebDriverPool pool;


        private static ConcurrentDictionary<string, IWebDriver> webDriversMap = new();
        private static string key => TestContext.CurrentContext.Test.ID ?? "debug";

        public static IWebDriver Get()
        {
            var browser = webDriversMap.GetOrAdd(key, _=> pool.Acquire());
            return browser;
        }

        public static void Release()
        {

            if (webDriversMap.TryRemove(key, out var browser))
                pool.Release(browser);
        }

        public static void Dispose()
        {
            pool.Clear();
        }

        private static DelegateWebDriverCleaner cleaner = new(x => x.ResetWindows());
    }
}