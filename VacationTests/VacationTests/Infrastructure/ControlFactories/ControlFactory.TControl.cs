using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlFactories
{
    public class ControlFactory<TControl>
    {
        private readonly ControlFactory controlFactory;

        public ControlFactory(ControlFactory controlFactory)
        {
            this.controlFactory = controlFactory;
        }

        public TControl Create(ISearchContext context, By by) => controlFactory.Create<TControl>(context, by);
    }
}