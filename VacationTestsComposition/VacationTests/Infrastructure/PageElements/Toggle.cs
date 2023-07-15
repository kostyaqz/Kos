using Kontur.Selone.Properties;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Toggle : ControlBase
    {
        public Toggle(IWebElement element)
            : base(element)
        {
        }

        public IProp<bool> Checked => container.Checked();
    }
}