using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorFioCombobox : Combobox
    {
        public DirectorFioCombobox(
            IWebElement element,
            IWrapsDriver wrapsDriver,
            ControlFactory controlFactory,
            ElementsCollectionFactory elementsCollectionFactory)
            : base(element, wrapsDriver, controlFactory, elementsCollectionFactory)
        {
            MenuItems = elementsCollectionFactory.Create<DirectorItem>(wrapsDriver.WrappedDriver,
                x => x.WithTid("ComboBoxMenu__item").FixedByIndex());
        }

        public new ElementsCollection<DirectorItem> MenuItems { get; private set; }
    }
}