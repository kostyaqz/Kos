using System;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public abstract class BaseSearchByAttribute : Attribute
    {
        private readonly ByLambda byLambda;

        protected BaseSearchByAttribute(ByLambda byLambda)
        {
            this.byLambda = byLambda;
        }

        // return by.Css(...)
        public By SearchCriteria(ByDummy by) => byLambda(by);
    }
}