using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class AverageSalaryRow : ControlBase
    {
        public AverageSalaryRow(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            SalaryCurrencyInput = controlFactory.CreateControl<CurrencyInput>(Container.Search(x =>
                x.WithTid("SalaryCurrencyInput")));
            CountBaseCurrencyLabel =
                controlFactory.CreateControl<CurrencyLabel>(Container.Search(x
                    => x.WithTid("CountBaseCurrencyLabel")));
            YearSelect = controlFactory.CreateControl<Select>(Container.Search(x
                => x.WithTid("YearSelect")));
        }

        public Select YearSelect { get; }
        public CurrencyInput SalaryCurrencyInput { get; }
        public CurrencyLabel CountBaseCurrencyLabel { get; }
    }
}