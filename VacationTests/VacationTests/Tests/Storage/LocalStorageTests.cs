using Kontur.Selone.Extensions;
using Kontur.Selone.Pages;
using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.PageObjects;

namespace VacationTests.Tests.Storage
{
    [NonParallelizable]
    public class LocalStorageTests : VacationTestBase
    {
        // Название ключа, который будем добавлять
        private const string ClaimsKeyName = "Vacation_App_Claims";

        // Значение, которое будем добавлять для ключа ClaimsKeyName
        private const string FirstVacation =
            @"[{""endDate"":""14.08.2020"",""id"":""1"",""paidNow"":true,""startDate"":""01.08.2020"",""status"":2,""type"":""Основной"",""userId"":""1"",""director"":{""id"":24939,""name"":""Захаров Максим Николаевич"",""position"":""Руководитель направления тестирования""}}]";

        private EmployeeVacationListPage employeePage;

          //todo для курсантов: после рализации методов LocalStorage расскоментировать и прогнать тесты
         [SetUp]
         public void SetUp()
         {
             // Открытие списка отпусков
             employeePage = Navigation.OpenEmployeeVacationListPage();

             // Перед каждым тестом этого класса делается очитска LocalStorage, поскольку
             // в данном случае все тесты идут последовательно
             // и данные с прошлого теста могут мешать новому тесту
             LocalStorage.Clear();
             employeePage.Refresh();

             // Проверка пустого списка
             employeePage.ClaimList.Items.Count.Wait().EqualTo(0);
         }

         [Test]
         public void AddClaims_WithSeloneInterface()
         {
             // Получение WebDriver и с помощью Selonе и метода JavaScriptExecutor() добавление отпуска в localStorage
             WebDriver.JavaScriptExecutor()
                 .ExecuteScript($"localStorage.setItem(\"{ClaimsKeyName}\", '{FirstVacation}');");

             // Чтобы отпуск появился в интерфейсе – обновляние страницы
             employeePage.Refresh();

             // Проверка появления записи
             employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
         }

         [Test]
         public void AddClaims_WithLocalStorageClassInterface()
         {
             // Добавление в хранилище отпуска
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

             employeePage.Refresh();
             employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
         }

         [Test]
         public void AddClaims_DeleteClaimsWithRemoveItemMethod()
         {
             // Добавление 1 отпуска в хранилище
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
             employeePage.Refresh();
             employeePage.ClaimList.Items.Count.Wait().EqualTo(1);
             // Добавление ещё одного ключа, который не должен удалиться
             LocalStorage.SetItem("TestKey", "TestName");

             // Проверка, что в LocalStorage два ключа
             Assert.That(LocalStorage.Length, Is.EqualTo(2));

             // Удаление ключа с отпуском
             LocalStorage.RemoveItem(ClaimsKeyName);
             employeePage.Refresh();
             // Проверка, что отпуск исчез из интерфейса
             employeePage.ClaimList.Items.Count.Wait().EqualTo(0);

             // Проверка, что в LocalStorage остался 1 ключ
             Assert.That(LocalStorage.Length, Is.EqualTo(1));
         }

         [Test]
         public void AddClaims_DeleteClaimsWithClearMethod()
         {
             // Добавление 1 отпуска
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
             employeePage.Refresh();
             employeePage.ClaimList.Items.Count.Wait().EqualTo(1);

             // Добавление ещё 1 ключа, который тоже будет удален
             LocalStorage.SetItem("TestKey", "TestName");

             // Очищение всего хранилища
             LocalStorage.Clear();

             employeePage.Refresh();
             // Проверка, что отпуск исчез из интерфейса
             employeePage.ClaimList.Items.Count.Wait().EqualTo(0);

             // Проверка, что в LocalStorage нет ключей
             Assert.That(LocalStorage.Length, Is.EqualTo(0));
         }

         [Test]
         public void AddClaims_GetItem()
         {
             // Добавление 1 отпуска
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

             // Получение значения для ключа ClaimsKeyName
             var result = LocalStorage.GetItem(ClaimsKeyName);

             // Проверка, что значение равно тому, что мы задавали
             Assert.That(result, Is.EqualTo(FirstVacation));
         }

         [Test]
         public void GetNonExistentItem()
         {
             var nonExistentKey = "nonExistentKey";
             // Получение значения для ключа ClaimsKeyName
             var result = LocalStorage.GetItem(nonExistentKey);

             // Проверка, что значение равно тому, что мы задавали
             Assert.That(result, Is.Null);
         }

         [Test]
         public void AddClaims_CheckLength()
         {
             // Добавление 1 отпуска
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);

             // Проверка, что теперь в хранилище есть всего 1 ключ
             Assert.That(LocalStorage.Length, Is.EqualTo(1));
         }

         [Test]
         public void AddClaims_GetKeyNameByIndex()
         {
             // Добавление 1 отпуска, первый ключ будет иметь индекс 0
             LocalStorage.SetItem(ClaimsKeyName, FirstVacation);
             // Добавление ещё 1 ключа, второй ключ будет иметь индекс 1
             LocalStorage.SetItem("TestKey", "TestName");

             // Проверка, что в LocalStorage ключ с индексом 0 == ClaimsKeyName
             Assert.That(LocalStorage.Key(0).Equals(ClaimsKeyName));
         }
    }
}