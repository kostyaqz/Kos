using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageElements
{
    public class ClaimLightboxFooter : ControlBase
    {
        public ClaimLightboxFooter(IWebElement element)
            : base(element)
        {
        }

        public Button<AdminVacationListPage> AcceptButton { get; private set; }
        public Button RejectButton { get; private set; }
    }
}