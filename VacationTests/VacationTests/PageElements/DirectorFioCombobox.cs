using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using Kontur.Selone.Selectors.XPath;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorFioCombobox : Combobox
    {
        public DirectorFioCombobox(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy, controlFactory)
        {
            MenuItems = controlFactory.CreateElementsCollection<DirectorItem>(Container.Root(),
                x => x.WithTid("ComboBoxMenu__item").FixedByIndex());
        }

        public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
    }
}