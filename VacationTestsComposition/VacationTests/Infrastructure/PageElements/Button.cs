using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    // О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
    public class Button : ControlBase
    {
        private readonly Clicker clicker;

        public Button(IWebElement element, Clicker clicker)
            : base(element)
        {
            this.clicker = clicker;
        }

        public IProp<string> Text => container.Text();

        public void Click() => clicker.Click(container);
    }
}