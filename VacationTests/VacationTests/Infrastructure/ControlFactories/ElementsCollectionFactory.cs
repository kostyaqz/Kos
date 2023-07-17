using Kontur.Selone.Elements;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.ControlFactories
{
    public class ElementsCollectionFactory
    {
        private readonly ControlFactory controlFactory;

        public ElementsCollectionFactory(ControlFactory controlFactory)
        {
            this.controlFactory = controlFactory;
        }

        internal ElementsCollection<TControl> Create<TControl>(
            ISearchContext context,
            ItemByLambda itemByLambda)
        {
            return new(
                context,
                itemByLambda,
                (s, b, e) => controlFactory.Create<TControl>(s, b));
        }
    }
}