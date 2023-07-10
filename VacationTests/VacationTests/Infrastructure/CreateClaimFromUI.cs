using System;
using VacationTests.Claims;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Infrastructure
{
    public class CreateClaimFromUI
    {
        public static void Create(DateTime startDate, DateTime endDate, EmployeeVacationListPage page)
        {
            page.WaitLoaded();
            var itemCount = page.ClaimList.Items.Count.Get();
            var claimPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();
            claimPage.WaitLoaded();

            claimPage.ClaimTypeSelect.SelectValueByText(ClaimType.Child.GetDescription());
            claimPage.ChildAgeInput.InputText("2");
            claimPage.ClaimStartDatePicker.SetValue(startDate);
            claimPage.ClaimEndDatePicker.SetValue(endDate);
            claimPage.DirectorFioCombobox.SelectValue("Захаров");

            var pageWithVacation = claimPage.SendButton.ClickAndOpen<EmployeeVacationListPage>();

            pageWithVacation.ClaimList.Items.Count.Wait().EqualTo(itemCount + 1);
        }
    }
}