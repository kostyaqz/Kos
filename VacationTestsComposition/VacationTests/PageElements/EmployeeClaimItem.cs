using Kontur.Selone.Extensions;
using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;
using VacationTests.PageObjects;

namespace VacationTests.PageElements
{
    // класс элемента списка отпусков EmployeeClaimList
    // наследуем от ControlBase, поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    public class EmployeeClaimItem : ControlBase
    {
        public EmployeeClaimItem(IWebElement element)
            : base(element)
        {
        }

        // при обращении из теста к любому элементу списка отпусков будут доступны три свойства
        public Link<ClaimCreationPage> TitleLink { get; private set; }
        public Label PeriodLabel { get; private set; }
        public Label StatusLabel { get; private set; }

        // можно вот так реализовать метод для наведения мыши на конкретный элемент
        // такой метод может понадобиться, если только при наведении на элемент списка показывается какой-то контрол
        public void MouseOver()
        {
            container.Mouseover();
        }
    }
}