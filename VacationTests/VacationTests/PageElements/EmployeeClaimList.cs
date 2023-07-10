using Kontur.Selone.Elements;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using Kontur.Selone.Selectors.XPath;
using VacationTests.Infrastructure;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.PageElements
{
    // Класс списка наследуем от ControlBase, поскольку это тоже контрол и могут понадобиться базовые методы и пропсы
    public class EmployeeClaimList : ControlBase
    {
        public EmployeeClaimList(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            // EmployeeClaimItem - тип каждого элемента, который также создаем ниже
            // WithTid("СlaimItem") - одинаковый селектор для каждого элемента 
            // FixedByAttribute() - используется для указания уникального атрибута, чтобы элемент сделать уникальным,
            // это позволяет, например, проверить невидимость этого элемента или его порядок в списке
            // FixedByIndex() - используется, когда нет уникального признака, индексируются элементы по-порядку
            Items = controlFactory.CreateElementsCollection<EmployeeClaimItem>(Container,
                x => x.WithTid("ClaimItem").FixedByIndex());
        }

        // ElementsCollection<T> – сам список, к которому из тестов надо обращаться
        // ElementsCollection<T> – специальный класс Selone для коллекции элементов
        // Имеет интерфейсы : IElementsCollection<T>, IEnumerable<T>, IEnumerable
        // Имеет свойство Count для получения количества элементов в коллекции
        public ElementsCollection<EmployeeClaimItem> Items { get; private set; }
    }
}