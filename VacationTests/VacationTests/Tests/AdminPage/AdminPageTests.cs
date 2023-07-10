using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using Kontur.Selone.Pages;
using Kontur.Selone.Properties;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.AdminPage
{
    public class AdminPageTests : VacationTestBase
    {
        [Test]
        public void Test1()
        {
            var pageFirst = Navigation.OpenEmployeeVacationListPage("1");
            CreateClaimFromUI.Create(DateTime.Now.AddDays(5).Date, DateTime.Today.AddDays(15).Date, pageFirst);

            var pageSecond = Navigation.OpenEmployeeVacationListPage("2");
            CreateClaimFromUI.Create(DateTime.Now.AddDays(15).Date, DateTime.Today.AddDays(30).Date, pageSecond);

            var adminPage = Navigation.OpenAdminVacationListPage();
            adminPage.AdminClaimList.Item.Select(x => x.Title.Text).Wait()
                .EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        [Test]
        public void Task2Test1()
        {
            var claim1 = Claim.CreateDefault() with { UserId = "1", Id = "1" };
            var claim2 = Claim.CreateDefault() with { UserId = "2", Id = "2" };
            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.ClearClaims();
            ClaimStorage.Add(new[]
            {
                claim1,
                claim2
            });

            adminPage.Refresh();


            var expected = new[]
            {
                ("Заявление 1", DateTimeToString.CreateStringPeriodFromDateTime(claim1.StartDate, claim1.EndDate),
                    ClaimStatus.NonHandled.GetDescription()),
                ("Заявление 2", DateTimeToString.CreateStringPeriodFromDateTime(claim2.StartDate, claim2.EndDate),
                    ClaimStatus.NonHandled.GetDescription())
            };

            adminPage.AdminClaimList.Item.Select(x =>
                Props.Create(x.Title.Text, x.PeriodLabel.Text, x.StatusLabel.Text)).Wait().EquivalentTo(expected);
        }

        [Test]
        public void Task2Test2()
        {
            var claim1 = Claim.CreateDefault() with { UserId = "1", Id = "1", Status = ClaimStatus.Accepted };
            var claim2 = Claim.CreateDefault() with { UserId = "2", Id = "2", Status = ClaimStatus.NonHandled };
            var claim3 = Claim.CreateDefault() with { UserId = "2", Id = "2", Status = ClaimStatus.Rejected };
            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.ClearClaims();
            ClaimStorage.Add(new[]
            {
                claim1,
                claim2,
                claim3
            });

            adminPage.Refresh();

            var expected = new[]
            {
                (false, false),
                (true, true),
                (false, false)
            };

            adminPage.AdminClaimList.Item.Select(x =>
                Props.Create(x.AcceptButton.Visible, x.RejectButton.Visible)).Wait().EquivalentTo(expected);
        }

        [Test]
        public void Task2Test3()
        {
            var claim = Claim.CreateDefault();
            var adminPage = Navigation.OpenAdminVacationListPage();

            adminPage.AdminClaimList.Item.Count.Wait().EqualTo(0);

            ClaimStorage.Add(new[] { claim });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Count.Wait().EqualTo(1);
            var lightbox = adminPage.AdminClaimList.Item.Single().Title.ClickAndOpen<ClaimLightbox>();
            lightbox.DirectorFioLabel.Text.Wait().EqualTo("Бублик Владимир Кузьмич");
            lightbox.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            lightbox.PeriodLabel.Text.Wait()
                .EqualTo(DateTimeToString.CreateStringPeriodFromDateTime(claim.StartDate.Date, claim.EndDate.Date));
        }

        [TestCaseSource(nameof(ButtonCases))]
        public void Task2Test4(Func<ClaimItem, Button> getButton, string expectedStatus)
        {
            var claim = Claim.CreateDefault();
            var adminPage = Navigation.OpenAdminVacationListPage();
            ClaimStorage.Add(new[] { claim });

            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Select(x => x.StatusLabel.Text).Wait()
                .EquivalentTo(new[] { ClaimStatus.NonHandled.GetDescription() });
            var claimItem = adminPage.AdminClaimList.Item.Single();
            getButton(claimItem).Click();
            adminPage.AdminClaimList.Item.Select(x => x.StatusLabel.Text).Wait().EquivalentTo(new[] { expectedStatus });
            ClaimStorage.ClearClaims();
        }

        [Test]
        public void Task2Test5()
        {
            var claim = Claim.CreateDefault();
            var adminPage = Navigation.OpenAdminVacationListPage();

            ClaimStorage.Add(new[] { claim });
            adminPage.Refresh();

            var lightbox = adminPage.AdminClaimList.Item.Single().Title.ClickAndOpen<ClaimLightbox>();
            lightbox.StatusLabel.Text.Wait().EqualTo(ClaimStatus.NonHandled.GetDescription());
            var page = lightbox.RejectedButton.ClickAndOpen<AdminVacationListPage>();

            page.AdminClaimList.Item.Select(x => x.StatusLabel.Text).Wait()
                .EquivalentTo(new[] { ClaimStatus.Rejected.GetDescription() });
        }

        [Test]
        public void Task2Test6()
        {
            var claim1 = Claim.CreateDefault() with { Id = "1" };
            var claim2 = Claim.CreateDefault() with { Id = "2" };
            var adminPage = Navigation.OpenAdminVacationListPage();

            ClaimStorage.Add(new[] { claim1, claim2 });
            adminPage.Refresh();

            adminPage.AdminClaimList.Item.Select(x => x.Title.Text).Wait()
                .EqualTo(new[] { "Заявление 1", "Заявление 2" });
        }

        private static IEnumerable<TestCaseData> ButtonCases()
        {
            yield return new TestCaseData(new Func<ClaimItem, Button>(item => item.RejectButton),
                ClaimStatus.Rejected.GetDescription());
            yield return new TestCaseData(new Func<ClaimItem, Button>(item => item.AcceptButton),
                ClaimStatus.Accepted.GetDescription());
        }
    }
}