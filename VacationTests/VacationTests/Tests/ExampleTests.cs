using System;
using NUnit.Framework;
using VacationTests.Claims;
using VacationTests.Infrastructure;

namespace VacationTests.Tests
{
    // [Parallelizable] // тут можно указать, что тесты класса должны / не должны идти в параллель
    // [TestFixture] // простой класс с тестами без параметризаций можно не помечать таким атрибутом
    // Класс с тестами (сборка тестов) с именем ExampleTests,
    // наследуется от базового класса, в котором могут быть SetUp и TearDown
    public class ExampleTests : VacationTestBase
    {
        // Можно хранить константы, доступные для всех тестов
        private readonly string employeeId = "newEmployeeId";

        // Действия, выполняемые перед каждым тестом
        [SetUp]
        public void SetUp()
        {
            // Можно сделать какое-то общее действие для всех тестов
            Navigation.OpenEmployeeVacationListPage(employeeId);
        }

        // Действия, выполняемые после каждого теста
        [TearDown]
        public new void TearDown()
        {
            // Можно сделать какое-то общее действие для всех тестов, например
            // ClaimStorage.ClearClaims(); 
        }

        // Тест
        [Test]
        public void EnterWorker()
        {
            // Arrange
            var defaultDirector = new Director(14, "Бублик Владимир Кузьмич", "Директор департамента");
            var claim = Claim.CreateDefault() with
            {
                UserId = employeeId,
                Status = ClaimStatus.Accepted,
                EndDate = DateTime.Now.AddDays(3)
            };
            ClaimStorage.Add(new[] {claim});

            // Act
            var employeePage = Navigation.OpenEmployeeVacationListPage(employeeId);

            // Assert
            employeePage.SalaryCalculatorTab.Present.Wait().EqualTo(true);
            employeePage.ClaimList.Items.Count.Wait().That(Is.AtLeast(1));
        }
    }
}