using Kontur.Selone.Properties;

namespace VacationTests.Infrastructure.ControlServices.Properties
{
    public class IntegerTransformation : IPropTransformation<int>
    {
        public int Deserialize(string value)
        {
            return int.Parse(value);
        }

        public string Serialize(int value)
        {
            return value.ToString();
        }
    }
}