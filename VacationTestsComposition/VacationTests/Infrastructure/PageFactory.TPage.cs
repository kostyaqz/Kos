using OpenQA.Selenium;
using VacationTests.Infrastructure.ControlFactories;

namespace VacationTests.Infrastructure
{
    public class PageFactory<TPage>
    {
        private readonly TPage pageObject;
        private readonly ControlFactory controlFactory;
        private readonly ISearchContext rootContext;

        public PageFactory(
            TPage pageObject,
            ControlFactory controlFactory,
            IWrapsDriver webDriver)
        {
            this.pageObject = pageObject;
            this.controlFactory = controlFactory;
            rootContext = webDriver.WrappedDriver;
        }

        public TPage CreatePage()
        {
            controlFactory.InitializeFields(rootContext, pageObject);
            return pageObject;
        }
    }
}