using Kontur.Selone.Selectors.Css;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.ControlFactories;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageElements;

namespace VacationTests.PageObjects
{
    public class AdminVacationListPage 
    {
        private ControlFactory controlFactory;
        public AdminVacationListPage(ControlFactory controlFactory)
        {
            this.controlFactory = controlFactory;
        }
        public Label TitleLabel { get; private set; }
        public Link ClaimsTab { get; private set; }
        public Button DownloadButton { get; private set; }
        public PageFooter Footer { get; private set; }
        
        public void AssertThatCalculatorTabNotExist()
        {
            var salaryCalculatorTab = controlFactory.Create<Link>(new CssBy().WithTid("SalaryCalculatorTab"));
            salaryCalculatorTab.Present.Wait().EqualTo(false);
        }
        
        public void AssertThatCreateButtonNotExist()
        {
            var createButton = controlFactory.Create<Button>(new CssBy().WithTid("CreateButton"));
            createButton.Present.Wait().EqualTo(false);
        }
    }
}