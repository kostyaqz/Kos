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
            Pool = pool;
        }

        //С конструктором непонятно. Для пользования им нужны свойства, но по заданию нужно сделать поле
        //В итоге сделал и то, и то
        static IWebDriverPool pool;
        public static IWebDriverPool Pool { get; set; }
        private static ConcurrentDictionary<string, IWebDriver> webDriversMap = new();
        private static string key => TestContext.CurrentContext.Test.ID ?? "debug";
        public static IWebDriver Get()
        {
            var browser = webDriversMap.GetOrAdd(key,
                _ => new WebDriverPool(new ChromeDriverFactory(),cleaner).Acquire());
            return browser;
        }

        public static void Release()
        {
            //Вот тут явно ошибка и я не понимаю что в метод требуется подставить
            webDriversMap.TryRemove(key, pool);
        }

        public static void Dispose()
        {
            pool.Clear();
        }

        private static DelegateWebDriverCleaner cleaner = new(x => x.ResetWindows());
    }
}