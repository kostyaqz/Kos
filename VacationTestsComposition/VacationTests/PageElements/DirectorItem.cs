using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlServices;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    public class DirectorItem : ControlBase
    {
        private readonly Clicker clicker;

        public DirectorItem(IWebElement element, Clicker clicker)
            : base(element)
        {
            this.clicker = clicker;
        }

        public void Click() => clicker.Click(container);

        public Label IdLabel { get; private set; }
        public Label FioLabel { get; private set; }
        public Label PositionLabel { get; private set; }
    }
}