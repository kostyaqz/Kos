using NUnit.Framework;

namespace VacationTests.Infrastructure.ControlServices.Properties
{
    public class TransformationTests
    {
        private static object[] currencySerializeTransformationCases =
        {
            new object[] {123m, "123,00"},
            new object[] {123.55m, "123,55"},
            new object[] {12345.6m, $"12{(char) 8201}345,60"},
            new object[] {12345.6m, $"12{(char) 8201}345,60"},
            new object[] {12345.600m, $"12{(char) 8201}345,60"},
            new object[] {1234567.89m, $"1{(char) 8201}234{(char) 8201}567,89"}
        };

        private static object[] currencyDeserializeTransformationCases =
        {
            new object[] {"123,00", 123m},
            new object[] {"123,00₽", 123m},
            new object[] {"123,50", 123.5m},
            new object[] {"123.55", 123.55m},
            new object[] {$"12{(char) 8201}345,60", 12345.6m},
            new object[] {"bug", 0m}
        };

        [TestCaseSource(nameof(currencySerializeTransformationCases))]
        public void CurrencyTransformation_Serialize(decimal valueForTransform, string expectedResult)
        {
            var currencyTransformation = new CurrencyTransformation();
            Assert.That(currencyTransformation.Serialize(valueForTransform), Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(currencyDeserializeTransformationCases))]
        public void CurrencyTransformation_Deserialize(string valueForTransform, decimal expectedResult)
        {
            var currencyTransformation = new CurrencyTransformation();
            Assert.That(currencyTransformation.Deserialize(valueForTransform), Is.EqualTo(expectedResult));
        }
    }
}