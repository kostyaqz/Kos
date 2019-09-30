using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests.Pages
{
    class BasketPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public BasketPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        private By courier = By.CssSelector("[data-gaid='cart_dlcourier']"); //Галочка для выбора курьерской доставки

        //Тут еще типо методы, которые относятся к этой странице


    }
}
