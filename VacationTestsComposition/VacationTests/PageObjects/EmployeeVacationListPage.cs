using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class EmployeeVacationListPage
    {
        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Link SalaryCalculatorTab { get; private set; }
        public Button<ClaimCreationPage> CreateButton { get; private set; }
        public EmployeeClaimList ClaimList { get; private set; }
        public PageFooter Footer { get; private set; }
    }
}