using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageElements
{
    public class PageFooter : ControlBase
    {
        public PageFooter(IWebElement element)
            : base(element)
        {
        }

        public Link KnowEnvironmentLink { get; private set; }
        public Link<InfoSidePage> OurFooterLink { get; private set; }
    }
}