using System;
using System.Globalization;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class DatePicker : ControlBase
    {
        private readonly IWebElement dateInput;

        public DatePicker(IWebElement element)
            : base(element)
        {
            dateInput = container.SearchElement(By.XPath(".//*[contains(@data-comp-name, 'DateInput')]"));
        }

        public IProp<DateTime> Date => container.Date();
        public IProp<bool> HasError => container.HasError();

        public void SetValue(DateTime date)
        {
            SetValue(date.ToString("d", new CultureInfo("ru-RU")).Replace(".", string.Empty));
        }

        /// <summary>
        ///     Сейчас поведение контрола с датой такое, что каленарик остается открытым, пока в нём не кликнешь конкретную дату
        ///     Esc и Enter не закрывают календарик.
        ///     Каледарик может перекрывать нужный в тесте элемент, соответственно тест не сможет с ним взаимодейсвтовать
        ///     Возможные решения:
        ///     1) Послать Tab. Нюанс: следующий контрол может быть тоже календарик и снова перекроются элементы.
        ///     2) Кликнуть по другому элементу. Может выглядеть нелогично в тесте без комментариев. И надо думать в каждом тесте
        ///     отдельно.
        ///     3) Сделать универсальный клик в любое место. В каком-то из интерфейсов в этом месте может быть что-то.
        ///     4) Сделать на фронте так, чтобы в режиме для тестов календарик не раскрывался.
        ///     5) (используется в проекте) Посылать с помощью js blur() - убирание фокуса с элемента.
        ///     6) По-честному кликать в дату. Возможно непросто в реализации обвязки.
        /// </summary>
        public void SetValue(string text)
        {
            dateInput.SendKeys(text);
            Blur();
        }

        private void Blur()
        {
            dateInput.ExecuteJs("x.blur();");
        }
    }
}