using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    [InjectControls]
    public class AverageDailyEarningsCalculatorPage : PageBase
    {
        public AverageDailyEarningsCalculatorPage(IWebDriver webDriver, ControlFactory controlFactory) : base(webDriver)
        {
            AverageDailyEarningsCurrencyLabel = controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                x.WithTid("AverageDailyEarningsCurrencyLabel")));
            CountOfExcludeDaysInput =
                controlFactory.CreateControl<Input>(webDriver.Search(x =>
                    x.WithTid("CountOfExcludeDaysInput")));
            TotalEarningsCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(webDriver.Search(x =>
                    x.WithTid("TotalEarningsCurrencyLabel")));
            AverageSalaryRowFirst =
                controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("first")));
            AverageSalaryRowSecond =
                controlFactory.CreateControl<AverageSalaryRow>(webDriver.Search(x => x.WithTid("second")));
        }

        public CurrencyLabel AverageDailyEarningsCurrencyLabel { get; }
        public Input CountOfExcludeDaysInput { get; }
        public AverageSalaryRow AverageSalaryRowFirst { get; }
        public AverageSalaryRow AverageSalaryRowSecond { get; }
        public CurrencyLabel TotalEarningsCurrencyLabel { get; }
    }
}