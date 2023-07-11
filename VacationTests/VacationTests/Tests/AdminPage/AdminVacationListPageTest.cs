﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Data;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.AdminPage
{
    public class AdminVacationListPageTest : VacationTestBase
    {
        [Test]
        public void AdminPage_CreateClaimFrom2Users_CorrectUser()
        {
            var pageFirst = Navigation.OpenEmployeeVacationListPage();

            ClaimHelper.CreateClaimFromUI(DateTime.Now.AddDays(5).Date, DateTime.Today.AddDays(15).Date, pageFirst);
            var pageSecond = Navigation.OpenEmployeeVacationListPage("2");
            ClaimHelper.CreateClaimFromUI(DateTime.Now.AddDays(15).Date, DateTime.Today.AddDays(30).Date, pageSecond);
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.AdminClaimList.Item.Select(x => x.Title.Text).Wait()
                .EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        [Test]
        public void AdminPage_CreateClaimFrom2Users_CorrectFilling()
        {
            var claim1 = Claim.CreateDefault() with { UserId = "1", Id = "1" };
            var claim2 = Claim.CreateDefault() with { UserId = "2", Id = "2" };
            var expected = new[]
            {
                ("Иванов Петр Семенович",
                    DateTimeToString.CreateStringPeriodFromDateTime(claim1.StartDate, claim1.EndDate),
                    ClaimStatus.NonHandled.GetDescription()),
                ("Пользователь 2", DateTimeToString.CreateStringPeriodFromDateTime(claim2.StartDate, claim2.EndDate),
                    ClaimStatus.NonHandled.GetDescription())
            };

            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.ClearClaims();
            ClaimStorage.Add(new[] { claim1, claim2 });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Select(x =>
                Props.Create(x.User.Text, x.PeriodLabel.Text, x.StatusLabel.Text)).Wait().EquivalentTo(expected);
        }

        [TestCase(ClaimStatus.Accepted, false)]
        [TestCase(ClaimStatus.NonHandled, true)]
        [TestCase(ClaimStatus.Rejected, false)]
        public void AdminPage_CreateClaimFrom3UsersWithDifferentStatuses_CheckButtonVisible(ClaimStatus status,
            bool needShowButtonInList)
        {
            var claim1 = Claim.CreateDefault() with { Status = status };
            var expected = new[] { (needShowButtonInList, needShowButtonInList) };

            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.ClearClaims();
            ClaimStorage.Add(new[] { claim1 });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Select(x =>
                Props.Create(x.AcceptButton.Visible, x.RejectButton.Visible)).Wait().EquivalentTo(expected);
        }

        [Test]
        public void AdminPage_CreateClaim_CorrectFillingIntoLightbox()
        {
            var claim = Claim.CreateDefault();

            var adminPage = Navigation.OpenAdminVacationListPage();
            adminPage.AdminClaimList.Item.Count.Wait().EqualTo(0);
            adminPage.NoClaimText.Text.Wait().EqualTo("Нет заявлений");
            ClaimStorage.Add(new[] { claim });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Count.Wait().EqualTo(1);

            var lightbox = adminPage.AdminClaimList.Item.Single().Title.ClickAndOpen<ClaimLightbox>();

            lightbox.DirectorFioLabel.Text.Wait().EqualTo(Directors.Default.Name);
            lightbox.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            lightbox.PeriodLabel.Text.Wait()
                .EqualTo(DateTimeToString.CreateStringPeriodFromDateTime(claim.StartDate.Date, claim.EndDate.Date));
            lightbox.ModalHeaderLabel.Text.Wait().EqualTo("Иванов Петр Семенович");
            lightbox.ClaimTypeLabel.Text.Wait().EqualTo(ClaimType.Paid.GetDescription());
        }

        [TestCaseSource(nameof(ButtonCasesForList))]
        public void AdminPage_ChangeClaimStatusFromList_StatusChanged(Func<ClaimItem, Button> getButton,
            string expectedStatus)
        {
            var claim = Claim.CreateDefault();

            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(new[] { claim });
            adminPage.Refresh();
            var statusLabel = adminPage.AdminClaimList.Item.Select(x => x.StatusLabel.Text);
            statusLabel.Wait().EquivalentTo(new[] { ClaimStatus.NonHandled.GetDescription() });
            var claimItem = adminPage.AdminClaimList.Item.Single();
            getButton(claimItem).Click();

            statusLabel.Wait().EquivalentTo(new[] { expectedStatus });
            ClaimStorage.ClearClaims();
        }


        [TestCaseSource(nameof(ButtonCasesForLightbox))]
        public void AdminPage_ChangeClaimStatusFromLightbox_StatusChanged(Func<ClaimLightboxFooter, Button> getButton,
            string expectedStatus)
        {
            var claim = Claim.CreateDefault();
            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(new[] { claim });
            adminPage.Refresh();

            var lightbox = adminPage.AdminClaimList.Item.Single().Title.ClickAndOpen<ClaimLightbox>();
            lightbox.WaitLoaded();
            lightbox.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            getButton(lightbox.Footer).Click();
            lightbox.ModalHeaderLabel.Present.Wait().EqualTo(false);

            adminPage.AdminClaimList.Item.Select(x => x.StatusLabel.Text).Wait()
                .EquivalentTo(new[] { expectedStatus });
            ClaimStorage.ClearClaims();
        }

        [Test]
        public void AdminPage_Create2Claim_SequenceIsCorrect()
        {
            var claim1 = Claim.CreateDefault() with { Id = "1" };
            var claim2 = Claim.CreateDefault() with { Id = "2" };
            var adminPage = Navigation.OpenAdminVacationListPage();

            ClaimStorage.Add(new[] { claim1, claim2 });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Select(x => x.Title.Text).Wait()
                .EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        private static IEnumerable<TestCaseData> ButtonCasesForList()
        {
            yield return new TestCaseData(new Func<ClaimItem, Button>(item => item.RejectButton),
                ClaimStatus.Rejected.GetDescription());
            yield return new TestCaseData(new Func<ClaimItem, Button>(item => item.AcceptButton),
                ClaimStatus.Accepted.GetDescription());
        }

        private static IEnumerable<TestCaseData> ButtonCasesForLightbox()
        {
            yield return new TestCaseData(new Func<ClaimLightboxFooter, Button>(item => item.RejectButton),
                ClaimStatus.Rejected.GetDescription());
            yield return new TestCaseData(new Func<ClaimLightboxFooter, Button>(item => item.AcceptButton),
                ClaimStatus.Accepted.GetDescription());
        }
    }
}