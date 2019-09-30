using NUnit.Framework;
using System;
using System.Linq.Expressions;
using MySeleniumTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests
{
	[TestFixture]
	class Labirint : SeleniumTestBase
    {
		
		[Test]
		public void Train()
		{
		    var homePage = new HomePage(driver, wait);
            homePage.OpenPage();

		    var basketPage = homePage.addbookToCard();
		    basketPage.ChoseCourierDelivery();

		    basketPage.CourierDeliveryLightbox.Entercity("sdsdsd");
			//Переходим на страницу лабиринта 
			OpenPage(); //ctrl+alt+m тут было изначально открытие страницы (этот метод ниже теперь)
            
			//Наводимся на Кнопку "Книги"
			addbookToCard(out var bookMenu, out var allBooks, out var addBookInCart, out var issueOrder, out var beginOrder);



			//Таким образом все действия выводятся в отдельные методы нажатием на ctrl+alt+m.
			//Сами действия будут ниже, тут останутся только методы
            //Так мы упрощаем визуально код для чтения


		    //Выбираем курьерскую доставку
			var chooseCourierDelivery = By.CssSelector("span.b-radio-e-bg");
			driver.FindElement(chooseCourierDelivery).Click();

			var city = By.CssSelector("form#b-dlform-m-272-clone input[placeholder=Екатеринбург]");

			driver.FindElement(city).SendKeys("sdsdsd");
			driver.FindElement(city).SendKeys(Keys.Enter);

			//Не получается поймать ошибку
			//var cityError = By.CssSelector("div.js-dlform-wrap span.b-form-error");
			//Assert.AreEqual("Неизвестный город", driver.FindElement(cityError), "Не упала ошибка");

			Actions doubleClick = new Actions(driver);
			doubleClick.DoubleClick(driver.FindElement(city)).Build().Perform();
			driver.FindElement(city).SendKeys("Екатеринбург");

			//Не получается кликнуть по выпадающему меню
			//var suggestedCity = By.CssSelector("form#b-dlform-m-272-clone ui#ui-id-4");
			//action.MoveToElement(driver.FindElement(suggestedCity)).Perform();
			//driver.FindElement(suggestedCity).Click();

			//Ввод улицы
			var street = By.CssSelector("form#b-dlform-m-272-clone div.b-form-e-row-m-street input.js-street-suggests");
			driver.FindElement(street).SendKeys("Блюхера");

			//Ввод номера дома
			var building = By.CssSelector("input.js-building");
			driver.FindElement(building).SendKeys("10");

			//Ввод № офиса
			var flat = By.CssSelector("input.b-form-input-m-tiny");
			driver.FindElement(flat).SendKeys("12");

			//Нажатие на готово
			var confirm = By.CssSelector("div.b-dlform-e-submit input[value*=Готово]");
			Confirm(confirm);

			//Не понятно что за элемент имеется ввиду
			//var courierDeliveryLightbox = By.CssSelector("");
			//driver.FindElement(courierDeliveryLightbox).Click();
			



			

		}

        private void Confirm(By confirm)
        {
            driver.FindElement(confirm).Click();
        }

        [Test]
		public void MyTest()
		{
			driver.Navigate().GoToUrl("https://www.labirint.ru/guestbook");
			driver.FindElement(By.Id("a_nested")).Click();

			var nameField = driver.FindElement(By.CssSelector("#a_writer [name ='name']"));
			nameField.SendKeys("Oy");

			var emailField = driver.FindElement(By.CssSelector("#a_writer [name ='email']"));
			emailField.SendKeys("sdsd@mail.ru");

			driver.FindElement(By.Id("hd")).Click();
			Assert.IsTrue(driver.FindElement(By.Id("notForGuestbook")).Displayed, "Не отобразился лайтбокс");

			var selectElement = driver.FindElement(By.Name("theme"));
			var select = new SelectElement(selectElement);
			select.SelectByText("Дизайн");
			Assert.AreEqual("Дизайн", select.SelectedOption.Text, "Неверный элемент выбран");

			var myLabirintLocator = By.ClassName("js-b-autofade-text");
			var enterLabirint =
				By.CssSelector(".popup-window.dropdown-BlockExpression-opened [data-sendto='registration']");
			var lightboxLocator = By.Id("registration");



			new Actions(driver)
				.MoveToElement(driver.FindElement(myLabirintLocator))
				.Click()
				.Build()
				.Perform();

			wait.Until(ExpectedConditions.ElementIsVisible(enterLabirint));

			new Actions(driver)
				.MoveToElement(driver.FindElement(enterLabirint))
				.Click()
				.Build()
				.Perform();

			Assert.IsTrue(driver.FindElement(lightboxLocator).Displayed, "Не отобразился лайтбокс");
		}
	}
}
