using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests
{
	[TestFixture]
	public class Training
	{
		private IWebDriver driver;
		private WebDriverWait wait;



		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
		}

		[Test]
		public void Train()
		{
			driver.Navigate().GoToUrl("https://www.labirint.ru");
			var search = By.Id("search-field");
			var coupon = By.ClassName("getemail-btn");
			var text = (By.CssSelector("#product-about"));
			var searchForm = By.Name("searchformadvanced");
			var allGoods = By.XPath("//span[text()='Все товары']");
			var options = By.CssSelector("select[nameof=year_begin] option:not([selected])");
			var links = By.CssSelector("a.single-block-left-title");
			//var linksT = driver.FindElements(By.CssSelector("a[class^=single-block]"));
			var foter = By.LinkText("Как сделать заказ");

			//driver.Navigate().GoToUrl("https://www.labirint.ru");
			//var search = driver.FindElement(By.Id("search-field"));
			//var coupon = driver.FindElement(By.ClassName("getemail-btn"));
			//var text = driver.FindElement((By.CssSelector("#product-about")));
			//var searchForm = driver.FindElement(By.Name("searchformadvanced"));
			//var allGoods = driver.FindElement(By.XPath("//span[text()='Все товары']"));
			//var options = driver.FindElements(By.CssSelector("select[nameof=year_begin] option:not([selected])"));
			//var links = driver.FindElements(By.CssSelector("a.single-block-left-title"));
			////var linksT = driver.FindElements(By.CssSelector("a[class^=single-block]"));
			//var foter = driver.FindElement(By.LinkText("Как сделать заказ"));
		}

		[TearDown] // Закрытие браузера
		public void TearDown()
		{
			driver.Quit();
		}

	}
}
