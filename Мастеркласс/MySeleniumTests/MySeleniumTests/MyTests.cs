using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


//В тесте были предварительно добавлены нугет пакет NUnit, 2 пакета Selenium
//Также в папку проекта добавлен ChromeDriver.exe, он добавлен в проект (правой кнопкой по файлу в обозревателе решений)
//Также у хромдрайвера в свойствах копировать в выходной каталог изменено на "Копировать всегда"


namespace MySeleniumTests
{
	[TestFixture]
	public class MyTests
	{
		private IWebDriver driver;

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver();
		}

		[Test]
		public void MyFirstSeleniumTest()
		{
			driver.Navigate().GoToUrl("https://ru.wikipedia.org"); //Переход по урлу
			driver.FindElement(By.Name("search")).SendKeys("Selenium"); // Поиск элемента по имени и ввод в него селениум
			driver.FindElement(By.Name("go")).Click(); // Клик по кнопке поиска
			Assert.AreEqual("Seleniu — Википедия", driver.Title, "Не перешли на страницу"); // Проверка на заголовок страницы
		}

		[TearDown] // Закрытие браузера
		public void TearDown()
		{
			driver.Quit();
		}
	}
}
