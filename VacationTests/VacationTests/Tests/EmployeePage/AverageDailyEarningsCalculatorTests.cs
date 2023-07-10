using NUnit.Framework;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.Tests.EmployeePage
{
    public class AverageDailyEarningsCalculatorTests : VacationTestBase
    {
        // [Test]
        // public void SmokyTest()
        // {
        //     var page = Navigation.OpenEmployeeVacationListPage();
        //
        //     Thread.Sleep(2000);
        //
        //     var calc = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
        //
        //
        //     calc.YearSelectFirst.Visible.Wait().EqualTo(true);
        //     calc.SalaryCurrencyInputFirst.Visible.Wait().EqualTo(true);
        //     calc.YearSelectSecond.Visible.Wait().EqualTo(true);
        //     calc.SalaryCurrencyInputSecond.Visible.Wait().EqualTo(true);
        //     calc.AverageDailyEarningsCurrencyLabel.Visible.Wait().EqualTo(true);
        //
        //     calc.AverageDailyEarningsCurrencyLabel.Text.Wait().EqualTo("370,85");
        // }

        [Test]
        public void AverageDailyEarningsCalculator_FillingAllFields_AverageDailyEarningsCurrencyIsCorrect()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            page.TitleLabel.WaitPresence();

            var calc = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            calc.AverageSalaryRowFirst.YearSelect.SelectValueByText("2020");
            calc.AverageSalaryRowSecond.YearSelect.SelectValueByText("2021");
            calc.AverageSalaryRowFirst.SalaryCurrencyInput.ClearAndInputCurrency(100000);
            calc.AverageSalaryRowSecond.SalaryCurrencyInput.ClearAndInputCurrency(200000);
            calc.CountOfExcludeDaysInput.ClearAndInputText("100");

            calc.AverageDailyEarningsCurrencyLabel.Sum.Wait().EqualTo(475.44m);
        }

        [Test]
        public void AverageDailyEarningsCalculator_SalaryCurrencyMoreBase_TakeDefaultSum()
        {
            var page = Navigation.OpenEmployeeVacationListPage();
            page.TitleLabel.WaitPresence();

            var calc = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            calc.AverageSalaryRowFirst.YearSelect.SelectValueByText("2020");
            calc.AverageSalaryRowFirst.SalaryCurrencyInput.ClearAndInputCurrency(2000000.00m);

            calc.AverageSalaryRowFirst.CountBaseCurrencyLabel.Sum.Wait().EqualTo(912000m);

            calc.AverageSalaryRowFirst.YearSelect.SelectValueByText("2021");

            calc.AverageSalaryRowFirst.CountBaseCurrencyLabel.Sum.Wait().EqualTo(966000m);
        }

        [Test]
        public void AverageDailyEarningsCalculator_SalaryCurrencyLessBase_TakeSalaryCurrency()
        {
            var page = Navigation.OpenEmployeeVacationListPage();

            page.TitleLabel.Present.Wait().EqualTo(true);

            var calc = page.SalaryCalculatorTab.ClickAndOpen<AverageDailyEarningsCalculatorPage>();
            calc.AverageSalaryRowFirst.SalaryCurrencyInput.ClearAndInputCurrency(100000.10m);
            calc.AverageSalaryRowSecond.SalaryCurrencyInput.ClearAndInputCurrency(200000.20m);

            calc.AverageSalaryRowFirst.CountBaseCurrencyLabel.Sum.Wait().EqualTo(100000.10m);
            calc.AverageSalaryRowSecond.CountBaseCurrencyLabel.Sum.Wait().EqualTo(200000.20m);
            calc.TotalEarningsCurrencyLabel.Sum.Wait().EqualTo(300000.30m);
        }
    }
}