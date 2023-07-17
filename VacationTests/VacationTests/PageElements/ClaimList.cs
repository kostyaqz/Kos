using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    [InjectControls]
    public class ClaimList : ControlBase
    {
        public ClaimList(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            Item = controlFactory.CreateElementsCollection<ClaimItem>(Container,
                x => x.WithTid("ClaimItem").FixedByIndex());
        }

        public ElementsCollection<ClaimItem> Item { get; set; }
    }
}