using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class ClaimLightboxFooter : ControlBase
    {
        [InjectControls]
        public ClaimLightboxFooter(IContextBy contextBy) : base(contextBy)
        {
        }

        public Button AcceptButton { get; private set; }
        public Button RejectButton { get; private set; }
    }
}