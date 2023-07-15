using System;
using System.Linq;
using System.Reflection;
using Kontur.Selone.Extensions;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using VacationTests.Infrastructure.PageElements;

namespace VacationTests.Infrastructure.ControlFactories
{
    public class ControlFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IWrapsDriver wrapsDriver;

        public ControlFactory(IServiceProvider serviceProvider, IWrapsDriver wrapsDriver)
        {
            this.serviceProvider = serviceProvider;
            this.wrapsDriver = wrapsDriver;
        }

        public TControl Create<TControl>(By by) => Create<TControl>(wrapsDriver.WrappedDriver, @by);

        public TControl Create<TControl>(ISearchContext context, By by)
        {
            var element = context.SearchElement(by);
            return (TControl)Create(typeof(TControl), element);
        }

        private object Create(Type controlType, IWebElement element)
        {
            var control = ActivatorUtilities.CreateInstance(serviceProvider, controlType, element);
            InitializeFields(element, control);
            return control;
        }

        public void InitializeFields(ISearchContext searchContext, object control)
        {
            var props =
                from prop in control.GetType().GetProperties()
                let propertyType = prop.PropertyType
                where propertyType.IsAssignableTo(typeof(ControlBase))
                where prop.GetValue(control) == null
                let selector = prop
                                   .GetCustomAttributes<ByTidAttribute>()
                                   .Select(x => x.Tid)
                                   .FirstOrDefault()
                               ?? prop.Name
                let childWebElement = searchContext.SearchElement(x => x.WithTid(selector))
                select (prop, Create(propertyType, childWebElement));

            foreach (var (prop, childControl) in props)
            {
                prop.SetValue(control, childControl);
            }
        }
    }
}