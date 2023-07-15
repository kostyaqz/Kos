using System.Reflection;

namespace VacationTests.DependencyInjection.Singletones
{
    public class TestDependencyInfo
    {
        internal TestDependencyInfo(FieldInfo fieldInfo, object testFixture)
        {
            TestFixture = testFixture;
            FieldInfo = fieldInfo;
        }

        internal object TestFixture { get; }

        internal FieldInfo FieldInfo { get; }
    }
}