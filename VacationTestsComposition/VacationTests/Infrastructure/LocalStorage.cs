using Kontur.Selone.Extensions;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public class LocalStorage
    {
        private readonly IWebDriver webDriver;

        public LocalStorage(IWrapsDriver webDriver)
        {
            this.webDriver = webDriver.WrappedDriver;
        }

        // получение количества элементов в хранилище
        public long Length => (long) webDriver.JavaScriptExecutor().ExecuteScript("return localStorage.length;");

        // очистка всего хранилища
        public void Clear()
        {
            webDriver.JavaScriptExecutor().ExecuteScript("localStorage.clear();");
        }

        // получение данных по ключу keyName
        public string GetItem(string keyName)
        {
            var script = $"return localStorage.getItem(\"{keyName}\");";
            var result = webDriver.JavaScriptExecutor().ExecuteScript(script);
            return result as string;
        }

        // получение ключа на заданной позиции
        public string Key(int keyNumber)
        {
            var keyName = webDriver.JavaScriptExecutor().ExecuteScript($"return localStorage.key({keyNumber})");
            return keyName.ToString();
        }

        // удаление данных с ключом keyName
        public void RemoveItem(string keyName)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.removeItem(\"{keyName}\");");
        }

        // сохранение пары ключ/значение
        public void SetItem(string keyName, string value)
        {
            webDriver.JavaScriptExecutor().ExecuteScript($"localStorage.setItem(\"{keyName}\", '{value}');");
        }
    }
}