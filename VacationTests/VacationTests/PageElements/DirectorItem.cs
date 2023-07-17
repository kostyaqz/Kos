using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorItem : ControlBase
    {
        [InjectControls]
        public DirectorItem(IContextBy contextBy) : base(contextBy)
        {
        }

        public Label IdLabel { get; private set; }
        public Label FioLabel { get; private set; }
        public Label PositionLabel { get; private set; }
    }
}