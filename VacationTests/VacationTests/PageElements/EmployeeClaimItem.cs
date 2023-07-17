using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    // Класс элемента списка отпусков EmployeeClaimList наследуем от ControlBase,
    // поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    public class EmployeeClaimItem : ControlBase
    {
        [InjectControls]
        public EmployeeClaimItem(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            TitleLink = controlFactory.CreateControl<Link>(Container.Search(x => x.WithTid("TitleLink")));
            PeriodLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("PeriodLabel")));
            StatusLabel = controlFactory.CreateControl<Label>(Container.Search(x => x.WithTid("StatusLabel")));
        }

        // При обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Link TitleLink { get; }
        public Label PeriodLabel { get; }
        public Label StatusLabel { get; }

        // Можно вот так реализовать метод для наведения мыши на конкретный элемент,
        // такой метод может понадобиться, если только при наведении на элемент списка показывается какой-то контрол
        public void MouseOver()
        {
            Container.Mouseover();
        }
    }
}