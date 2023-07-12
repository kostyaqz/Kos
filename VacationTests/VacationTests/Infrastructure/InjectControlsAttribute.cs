using System;
using Kontur.Selone.Selectors;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure
{
    public class InjectControlsAttribute : Attribute
    {
        private readonly ByLambda byLambda;

        public InjectControlsAttribute(ByLambda byLambda)
        {
            this.byLambda = byLambda;
        }

        public By SearchCriteria(ByDummy by) => byLambda(by);

    }
}