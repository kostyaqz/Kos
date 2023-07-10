using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace VacationTests.Infrastructure.PageElements
{
    public class Link : ControlBase, ICanClickAndOpenPage
    {
        private readonly ControlFactory controlFactory;

        public Link(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            this.controlFactory = controlFactory;
        }

        public IProp<string> Text => Container.Text();

        ControlFactory ICanClickAndOpenPage.ControlFactory => controlFactory;
    }
}