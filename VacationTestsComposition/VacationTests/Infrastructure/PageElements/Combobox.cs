using Kontur.Selone.Elements;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Css;
using NUnit.Framework;
using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.ControlServices.Properties;

namespace VacationTests.Infrastructure.PageElements
{
    public class Combobox : ControlBase
    {
        private readonly Input input;
        
        public Combobox(
            IWebElement element,
            IWrapsDriver wrapsDriver,
            ControlFactory controlFactory,
            ElementsCollectionFactory elementsCollectionFactory)
            : base(element)
        {
            input = controlFactory.Create<Input>(container, By.XPath(".//*[contains(@data-comp-name,'CommonWrapper Input')]"));
            MenuItems = elementsCollectionFactory.Create<Button>(wrapsDriver.WrappedDriver,
                x => x.WithTid("ComboBoxMenu__item").FixedByIndex());
        }

        public IProp<bool> HasError => container.HasError();
        public ElementsCollection<Button> MenuItems { get; }

        public void SelectValue(string value)
        {
            Open();
            MenuItems.Wait().Single(x => x.Text, Contains.Substring(value))
                .Click();
        }

        public void InputValue(string text)
        {
            container.Click();
            input.ClearAndInputText(text);
        }

        public void WaitValue(string text)
        {
            input.Value.Wait().EqualTo(text);
        }

        public void Open()
        {
            container.Click();
        }
    }
}