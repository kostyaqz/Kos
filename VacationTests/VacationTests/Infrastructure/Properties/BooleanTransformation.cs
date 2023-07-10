using Kontur.Selone.Properties;

namespace VacationTests.Infrastructure.Properties
{
    public class BooleanTransformation : IPropTransformation<bool>
    {
        public bool Deserialize(string value)
        {
            return bool.Parse(value.ToLower());
        }

        public string Serialize(bool value)
        {
            return value.ToString().ToLower();
        }
    }
}