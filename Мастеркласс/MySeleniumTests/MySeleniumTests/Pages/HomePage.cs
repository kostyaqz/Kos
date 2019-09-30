using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests.Pages
{
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait; //alt+insert => constructor => появится publick HomePage

        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }
        
        //Вставили локаторы для этой страницы

        private string url = "https://www.labirint.ru";

        private By booksMenu = By.CssSelector("[data-toggle='header-genres']"); // Ссылка "Книги" в шапке 
        private By allBooks = By.CssSelector(".b-menu-second-container [href='/books/']"); //Ссылка "Все книги" в раскрывшемся меню
        private By addBookInCart = By.XPath("(//a[contains(@class,'btn')][contains(@class,'buy-link')])[1]"); //Кнопка "В корзину" или "Предзаказ" у первой книги
        private By issueOrder = By.XPath("(//a[contains(@class,'btn')][contains(@class,'buy-link')][contains(@class,'btn-primary')][contains(@class,'btn-more')])[1]"); //Кнопка "Оформить" у первой книги
        private By beginOrder = By.CssSelector("#basket-default-begin-order"); //Кнопка "Начать оформление"


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void addbookToCard()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(booksMenu)).Build().Perform();

            //Кликаем на подменю "Все книги"
            driver.FindElement(allBooks).Click();

            //Кликаем по добавить книгу
            driver.FindElement(addBookInCart).Click();

            driver.FindElement(issueOrder).Click();

            driver.FindElement(beginOrder).Click();
        }
    }
}
