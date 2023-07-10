using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class CurrencyLabel : Label
    {
        public CurrencyLabel(IContextBy contextBy) : base(contextBy)
        {
        }

        public IProp<decimal> Sum => Text.Currency();
    }
}