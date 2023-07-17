using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.DependencyInjection;
using VacationTests.DependencyInjection.MethodInject;
using VacationTests.Infrastructure;

namespace VacationTests.Tests
{
    // теперь класс с тестами не наследует базовый класс, а имеет интерфейс IFixtureWithServiceProviderFramework
    // IFixtureWithServiceProviderFramework имеет атрибут DependencyInjection
    // в атрибуте DependencyInjection происходит разная магия с выполнением дейсвтий до и после тестов
    public class ExampleTests : IFixtureWithServiceProviderFramework
    {
        // обязательный метод с конфигурацией сервисов для контенейра
        // AddWeb() содержит все необходимые сервисы для тестов
        public void ConfigureServices(IServiceCollection collection) => collection.AddWeb();

        private readonly string employeeId = "newEmployeeId";
        // вот так можно достать сервис, который нужен большинству тестов, из контенейра
        private PageNavigation.Navigation Navigation => this.GetRequiredService<PageNavigation.Navigation>();
        
        [SetUp]
        public void SetUp()
        {
            Navigation.OpenEmployeeVacationList(employeeId);
        }

        [TearDown]
        public void TearDown()
        {
            // можно сделать какое-то общее действие для всех тестов
        }

        [Test]
        // с помощью [Inject] можно достать какой-то сервис конкретному тесту
        public void EnterWorker([Inject] ClaimStorage claimStorage)
        {
            var claim = Claim.CreateDefault() with
            {
                UserId = employeeId,
                Status = ClaimStatus.Accepted,
                EndDate = DateTime.Now.AddDays(3)
            };

            claimStorage.Add(new[] {claim});
            var employeePage = Navigation.OpenEmployeeVacationList(employeeId);
            employeePage.SalaryCalculatorTab.Present.Wait().EqualTo(true);
            employeePage.ClaimList.Items.Count.Wait().That(Is.AtLeast(1));
        }
    }
}