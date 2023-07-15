#nullable enable
using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace VacationTests.DependencyInjection.MethodInject
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class InjectAttribute : NUnitAttribute, IParameterDataSource
    {
        public IEnumerable GetData(IParameterInfo parameter)
        {
            var markerType = typeof(InjectedService<>).MakeGenericType(parameter.ParameterType);
            yield return Activator.CreateInstance(markerType);
        }
    }
}
