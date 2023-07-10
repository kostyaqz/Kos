// using System;
// using System.Linq;
// using Kontur.Selone.Pages;
// using Kontur.Selone.Properties;
// using NUnit.Framework;
// using VacationTests.Claims;
// using VacationTests.Infrastructure;
// using VacationTests.Infrastructure.PageElements;
// using VacationTests.PageObjects;
//
// namespace VacationTests.Tests.EmployeePage
// {
//     public class EmployeeVacationsListTests : VacationTestBase
//     {
//         [Test]
//         public void CreateVacations_ShouldAddItemsToClaimsList()
//         {
//             var userId = TestContext.CurrentContext.Test.Name;
//
//             var page = Navigation.OpenEmployeeVacationListPage(userId);
//             ClaimStorage.Add(new[]
//             {
//                 Claim.CreateDefault() with { UserId = userId },
//                 Claim.CreateDefault() with { UserId = userId }
//             });
//
//             page.Refresh();
//
//             page.ClaimList.Items.Count.Wait().EqualTo(2);
//         }
//
//         [Test]
//         public void ClaimsList_ShouldDisplayRightTitles_InRightOrder()
//         {
//             var userId = TestContext.CurrentContext.Test.Name;
//
//             var page = Navigation.OpenEmployeeVacationListPage(userId);
//             ClaimStorage.Add(new[]
//             {
//                 Claim.CreateDefault() with { UserId = userId },
//                 Claim.CreateDefault() with { UserId = userId },
//                 Claim.CreateDefault() with { UserId = userId }
//             });
//
//             page.Refresh();
//
//             page.ClaimList.Items.Select(x => x.TitleLink.Text)
//                 .Wait().EqualTo(new[] { "Заявление 1", "Заявление 2", "Заявление 3" });
//         }
//
//         [Test]
//         public void ClaimsList_ShouldDisplayRightTitleAndStatus_IgnoringOrder()
//         {
//             var userId = TestContext.CurrentContext.Test.Name;
//             var firstClaim = Claim.CreateDefault() with { UserId = userId };
//             var secondClaimStartDate = DateTime.Today.AddDays(15);
//             var secondClaimEndDate = DateTime.Today.AddDays(30);
//             var expected = new[]
//             {
//                 ("Заявление 1", CreateStringPeriodFromDateTime(firstClaim.StartDate, firstClaim.EndDate),
//                     ClaimStatus.NonHandled.GetDescription()),
//                 ("Заявление 2", CreateStringPeriodFromDateTime(secondClaimStartDate, secondClaimEndDate),
//                     ClaimStatus.NonHandled.GetDescription())
//             };
//
//             var page = Navigation.OpenEmployeeVacationListPage(userId);
//             ClaimStorage.Add(new[] { firstClaim });
//             page.Refresh();
//             //CreateClaimFromUI(secondClaimStartDate, secondClaimEndDate, page);
//
//             page.ClaimList.Items
//                 .Select(claim => Props.Create(claim.TitleLink.Text, claim.PeriodLabel.Text, claim.StatusLabel.Text))
//                 .Wait().EquivalentTo(expected);
//         }
//
//         [Test]
//         public void ClaimsList_ShouldDisplayRightPeriodForItem()
//         {
//             var userId = TestContext.CurrentContext.Test.Name;
//             var customStartDate = DateTime.Today.AddDays(45);
//             var customEndDate = DateTime.Today.AddDays(60);
//
//             var page = Navigation.OpenEmployeeVacationListPage(userId);
//             ClaimStorage.Add(new[]
//             {
//                 Claim.CreateDefault() with { UserId = userId },
//                 Claim.CreateDefault() with { StartDate = customStartDate, EndDate = customEndDate, UserId = userId }
//             });
//
//             page.Refresh();
//
//             var secondClaimItem = page.ClaimList.Items.Wait().Single(
//                 x => x.TitleLink.Text, Is.EqualTo("Заявление 2"));
//
//             secondClaimItem.PeriodLabel.Text.Wait()
//                 .EqualTo(CreateStringPeriodFromDateTime(customStartDate, customEndDate));
//         }
//     }
// }