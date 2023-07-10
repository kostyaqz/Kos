using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Toggle : ControlBase
    {
        public Toggle(IContextBy contextBy) : base(contextBy)
        {
        }

        public IProp<string> Text => Container.Text();
        public IProp<bool> Checked => Container.Checked();
    }
}