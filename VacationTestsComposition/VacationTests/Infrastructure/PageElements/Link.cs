using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Link : ControlBase
    {
        public Link(IWebElement element)
            : base(element)
        {
        }

        public IProp<string> Text => container.Text();
    }
}