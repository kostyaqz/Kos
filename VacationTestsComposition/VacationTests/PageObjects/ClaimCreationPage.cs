using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class ClaimCreationPage
    {
        public Label UserFioLabel { get; private set; }
        public Select ClaimTypeSelect { get; private set; }
        public Input ChildAgeInput { get; private set; }
        public DatePicker ClaimStartDatePicker { get; private set; }
        public DatePicker ClaimEndDatePicker { get; private set; }
        public Label AvailableDaysMessage { get; private set; }
        public Label AvailableDaysLabel { get; private set; }
        public Checkbox PayNowCheckbox { get; private set; }
        public DirectorFioCombobox DirectorFioCombobox { get; private set; }
        public Button<EmployeeVacationListPage> SendButton { get; private set; }
    }
}