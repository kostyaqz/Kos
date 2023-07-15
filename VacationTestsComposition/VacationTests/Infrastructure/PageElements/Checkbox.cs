using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Checkbox : ControlBase
    {
        private readonly Clicker clicker;

        public Checkbox(IWebElement element, Clicker clicker)
            : base(element)
        {
            this.clicker = clicker;
        }

        public IProp<string> Text => container.Text();
        public IProp<bool> Checked => container.Checked();

        public void SetChecked()
        {
            Checked.Wait().EqualTo(false);
            clicker.Click(container);
            Checked.Wait().EqualTo(true);
        }

        public void SetUnchecked()
        {
            Checked.Wait().EqualTo(true);
            clicker.Click(container);
            Checked.Wait().EqualTo(false);
        }
    }
}