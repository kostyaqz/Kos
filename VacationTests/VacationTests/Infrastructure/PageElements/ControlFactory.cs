using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class ControlFactory
    {
        private readonly object[] dependencies;

        public ControlFactory(params object[] dependencies)
        {
            this.dependencies = dependencies;
        }

        /// <summary>Создать контрол типа TPageElement</summary>
        /// <typeparam name="TPageElement">Должен содержать конструктор, принимающий IWebDriver</typeparam>
        public TPageElement CreateControl<TPageElement>(IContextBy contextBy)
        {
            return (TPageElement) CreateInstance(typeof(TPageElement), contextBy, dependencies.Prepend(this).ToArray());
        }

        /// <summary>Создать страницу типа TPageObject</summary>
        public TPageObject CreatePage<TPageObject>(IWebDriver webDriver)
        {
            var allDependencies = dependencies.Prepend(this).Prepend(webDriver).ToArray();
            return (TPageObject) CreateInstance(typeof(TPageObject), null, allDependencies);
        }

        /// <summary>Создать коллекцию контролов типа TItem</summary>
        public ElementsCollection<TItem> CreateElementsCollection<TItem>(ISearchContext itemsSearchContext,
            ItemByLambda findItem)
        {
            return new ElementsCollection<TItem>(itemsSearchContext,
                findItem,
                (s, b, _) => CreateControl<TItem>(new ContextBy(s, b)));
        }

        private static object CreateInstance(Type controlType, IContextBy contextBy, object[] dependencies)
        {
            // У объекта, который хотим создать, проверяем, что конструктор есть и он один
            var constructors = controlType.GetConstructors();
            if (constructors.Length != 1)
                throw new NotSupportedException($"Контрол {controlType} должен иметь только один конструктор");
            var constructor = constructors.Single();
            // У конструктора получаеям все его входные парметры, которые ему нужны
            var parameters = constructor.GetParameters();
            var args = new List<object>();
            // Провеярем, что среди наших зависимостей есть все необходимые для создания объекта
            foreach (var parameterInfo in parameters)
            {
                var arg =
                    dependencies.Prepend(contextBy).FirstOrDefault(dep =>
                        dep != null && dep.GetType().IsAssignableTo(parameterInfo.ParameterType)) ??
                    throw new NotSupportedException(
                        $"Не поддерживаемый тип {parameterInfo.ParameterType} параметра конструктора контрола {controlType}");
                args.Add(arg);
            }

            // Вызываем конструктор и передаём ему все входные параметры
            var value = constructor.Invoke(args.ToArray());
            
            // Получаем контекст, по которому будем искать все контролы, входящие в состав нашего объекта
            var searchContext = contextBy?.SearchContext.SearchElement(contextBy.By) ??
                                dependencies.OfType<ISearchContext>().SingleOrDefault();
            if (searchContext == null)
                throw new NotSupportedException(
                    "Для автоматической инициализации полей контрола должен быть известен ISearchContext. " +
                    "Либо укажите IContextBy, либо передайте в зависимости WebDriver.");
            // Инициализируем контролы объекта
            InitializePropertiesWithControls(value, searchContext, dependencies);

            // Возвращаем экземпляр объекта
            return value;
        }

        private static void InitializePropertiesWithControls(object control, ISearchContext searchContext,
            params object[] dependencies)
        {
            // У переданного объекта ищем все свойства, наследющиеся от ControlBase
            var controlProps = control.GetType().GetProperties()
                .Where(p => typeof(ControlBase).IsAssignableFrom(p.PropertyType)).ToList();

            // Для каждого найденного свойства:
            foreach (var prop in controlProps)
            {
                // проверяем, что доступен метод set;
                if (prop.SetMethod is null) continue;
                
                // находим атрибут BaseSearchByAttribute или его наследника ByTidAttribute
                var attribute = prop.GetCustomAttribute<BaseSearchByAttribute>(true);
                // если атрибут не найден, то берём название самого свойства,
                // а если атрибут найден, берём его значение
                var contextBy = attribute == null
                    ? searchContext.Search(x => x.WithTid(prop.Name))
                    : searchContext.Search(attribute.SearchCriteria);
                
                // создаём экземпляр свойства через CreateInstance,
                // чтобы иницаилизировать у сложных контролов ещё и их свойства
                var value = CreateInstance(prop.PropertyType, contextBy, dependencies);
                // присваиваем свойству объекта полученный экземпляр
                prop.SetValue(control, value);
            }
        }
    }
}