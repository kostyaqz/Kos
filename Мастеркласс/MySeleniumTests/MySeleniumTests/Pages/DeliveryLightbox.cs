using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests.Pages
{
    class DeliveryLightbox
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        public DeliveryLightbox(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        private By city = By.CssSelector("input[data-suggeststype='district']"); //Поле ввода названия города
        private By cityError = By.CssSelector("span.b-form-error"); //Локатор ошибки о неизвестном городе
        private By street = By.CssSelector(".js-dlform-wrap input[data-suggeststype='streets']");//Название улицы
        private By building = By.CssSelector(".js-dlform-wrap [name^=building]"); //Номер дома
        private By flat = By.CssSelector(".js-dlform-wrap [name^=flat]"); //Номер квартиры
        private By confirm = By.CssSelector(".js-dlform-wrap [value=Готово]"); //Кнопка "Готово"
        private By courierDeliveryLightbox = By.CssSelector(".js-dlform-wrap"); //Локатор лайтбокса курьерской доставки
        private By suggestedCity = By.CssSelector(".suggests-item-txt"); //Локатор подсказки названия города

        //Далее идут методы, которых у меня нет :( Они снова должны относиться только к этой странице, методы должны
        //быть публичными
    }
}
