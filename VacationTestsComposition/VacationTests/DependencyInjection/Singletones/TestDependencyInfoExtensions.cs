using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VacationTests.DependencyInjection.Singletones
{
    public static class TestDependencyInfoExtensions
    {
        private const BindingFlags BindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        
        public static void InitializeDependencies(this IEnumerable<TestDependencyInfo> testDependencies, Func<Type, object> createDependency)
        {
            foreach (var dependency in testDependencies)
            {
                var dependencyType = GetDependencyType(dependency);
                dependency.FieldInfo.SetValue(dependency.TestFixture, createDependency(dependencyType));
            }
        }

        public static IEnumerable<TestDependencyInfo> GetTestDependencies<TTestFixture>(this TTestFixture fixture)
            where TTestFixture : class
        {
            var type = fixture.GetType();
            return
                from classType in type.GetBaseTypes().Append(type)
                from fieldInfo in GetInstanceFields(classType)
                let attribute = fieldInfo.GetCustomAttribute<TestDependencyAttribute>()
                where attribute != null
                select new TestDependencyInfo(fieldInfo, fixture);
        }

        private static Type GetDependencyType(TestDependencyInfo dependency)
        {
            return dependency.FieldInfo.FieldType;
        }

        private static IEnumerable<FieldInfo> GetInstanceFields(Type type)
        {
            return type.GetFields(BindingAttr);
        }
        
        private static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            while (type.BaseType != null)
            {
                yield return type = type.BaseType;
            }
        }
    }
}