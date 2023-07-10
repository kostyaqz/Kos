using System;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageNavigation;
using VacationTests.PageObjects;

namespace VacationTests.Tests.ControlTests
{
    public class ButtonTests : VacationTestBase
    {
        [Test]
        public void WaitExample()
        {
            var page = Navigation.OpenAdminVacationListPage();
            // todo для курсанта: после создания рекорда (Задание 6) заменить создание дефолтного отпуска на создание через рекорд
            ClaimStorage.Add(new[] {Claim.CreateDefault(), Claim.CreateDefault(), Claim.CreateDefault()});
            page.Refresh();

            page.DownloadButton.Text.Wait().EqualTo("Скачать их отпуска");
            page.DownloadButton.Text.Wait().That(Contains.Substring("Скачать"));

            page.DownloadButton.Present.Wait().EqualTo(true);
            page.DownloadButton.Disabled.Wait().EqualTo(true);

            // Сокращенный вариант
            page.DownloadButton.WaitPresence();
            page.DownloadButton.WaitDisabled();
        }

        [Test]
        public void OperationExample()
        {
            // Метод ClickAndOpen()
            var page = Navigation.OpenEmployeeVacationListPage();
            var claimPage = page.CreateButton.ClickAndOpen<ClaimCreationPage>();

            // Метод Click()
            claimPage.SendButton.Present.Wait().EqualTo(true);
            claimPage.SendButton.Click();
            claimPage.DirectorFioCombobox.HasError.Wait().EqualTo(true);
        }
    }
}