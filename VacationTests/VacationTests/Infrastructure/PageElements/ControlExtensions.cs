using Kontur.Selone.Extensions;
using VacationTests.PageObjects;

namespace VacationTests.Infrastructure.PageElements
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class ControlExtensions
    {
        /// <typeparam name="TPageObject">Должен содержать конструктор, принимающий IWebDriver</typeparam>
        /// Метод кликает по контролу и возвращает экземпляр новой страницы TPageObject
        public static TPageObject ClickAndOpen<TPageObject>(this ICanClickAndOpenPage control)
            where TPageObject : PageBase
        {
            control.Click();
            return control.ControlFactory.CreatePage<TPageObject>(control.Container.WebDriver());
        }

        public static void WaitPresence(this ControlBase control, int? timeout = null)
        {
            control.Present.Wait().EqualTo(true, timeout);
        }

        public static void WaitAbsence(this ControlBase control, int? timeout = null)
        {
            control.Present.Wait().EqualTo(false, timeout);
        }

        public static void WaitDisabled(this ControlBase control, int? timeout = null)
        {
            control.Disabled.Wait().EqualTo(true, timeout);
        }

        public static void WaitEnabled(this ControlBase control, int? timeout = null)
        {
            control.Disabled.Wait().EqualTo(false, timeout);
        }
    }
}