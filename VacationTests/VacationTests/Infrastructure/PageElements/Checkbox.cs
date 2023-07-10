using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Checkbox : ControlBase
    {
        public Checkbox(IContextBy contextBy) : base(contextBy)
        {
        }

        public IProp<string> Text => Container.Text();
        public IProp<bool> Checked => Container.Checked();

        public void SetChecked()
        {
            Checked.Wait().EqualTo(false);
            Click();
            Checked.Wait().EqualTo(true);
        }

        public void SetUnchecked()
        {
            Checked.Wait().EqualTo(true);
            Click();
            Checked.Wait().EqualTo(false);
        }
    }
}