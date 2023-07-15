using NUnit.Framework.Internal;

namespace VacationTests.Claims
{
    public class EmployeeGenerator
    {
        private readonly TestMethod test;

        public EmployeeGenerator(TestMethod test)
        {
            this.test = test;
        }

        public string GenerateEmployeeId() => test.Name;
    }
}