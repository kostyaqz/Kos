using System;
using JetBrains.Annotations;

namespace VacationTests.DependencyInjection.Singletones
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field)]
    public class TestDependencyAttribute : Attribute
    {
    }
}