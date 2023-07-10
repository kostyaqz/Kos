using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace VacationTests.Infrastructure.PageElements
{
    public class Label : ControlBase
    {
        public Label(IContextBy contextBy) : base(contextBy)
        {
        }

        public IProp<string> Text => Container.Text();
    }
}