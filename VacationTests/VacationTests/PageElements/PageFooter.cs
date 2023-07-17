using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class PageFooter : ControlBase
    {
        [InjectControls]
        public PageFooter(IContextBy contextBy) : base(contextBy)
        {
        }

        public Link KnowEnvironmentLink { get; private set; }
        public Link OurFooterLink { get; private set; }
    }
}