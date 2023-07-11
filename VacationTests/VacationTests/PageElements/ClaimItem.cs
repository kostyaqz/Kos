using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class ClaimItem : ControlBase
    {
        public ClaimItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            Title = controlFactory.CreateControl<Link>(Container.Search(x =>
                x.WithTid("TitleLink")));
            PeriodLabel = controlFactory.CreateControl<Label>(Container.Search(x =>
                x.WithTid("PeriodLabel")));
            StatusLabel = controlFactory.CreateControl<Label>(Container.Search(x =>
                x.WithTid("StatusLabel")));
            AcceptButton = controlFactory.CreateControl<Button>(Container.Search(x =>
                x.WithTid("AcceptButton")));
            RejectButton = controlFactory.CreateControl<Button>(Container.Search(x =>
                x.WithTid("RejectButton")));
            User = controlFactory.CreateControl<Label>(Container.Search(x =>
                x.WithTid("UserFioLabel")));
        }

        public Label User { get; }
        public Button RejectButton { get; }
        public Button AcceptButton { get; }
        public Label StatusLabel { get; }
        public Label PeriodLabel { get; }
        public Link Title { get; }
    }
}