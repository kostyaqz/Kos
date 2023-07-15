using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using VacationTests.Infrastructure;

namespace VacationTests.Claims
{
    public class ClaimStorage
    {
        private readonly LocalStorage localStorage;
        private const string ClaimsKeyName = "Vacation_App_Claims";

        public ClaimStorage(LocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public void Add(Claim[] claims)
        {
            // SetItem у localStorage перетерает значения по заданному ключу
            // поэтому мы сначала читаем текущее значение ключа
            var existingClaims = GetAll();
            if (existingClaims == null)
            {
                localStorage.SetItem(ClaimsKeyName, Serialize(claims));
            }
            else // для случая, если у нас в localStorage уже есть отпуска
            {
                // соединяем массив старых значений с новыми
                var newClaims = existingClaims.Concat(claims);
                // записываем все отпуска в localStorage
                localStorage.SetItem(ClaimsKeyName, Serialize(newClaims));
            }
        }

        public Claim[] GetAll()
        {
            var localStorageArray = localStorage.GetItem(ClaimsKeyName);
            return localStorageArray != null ? Deserialize<Claim[]>(localStorageArray) : null;
        }

        public void Add(Claim claim)
        {
            Add(new[] {claim});
        }

        public void ClearClaims()
        {
            localStorage.RemoveItem(ClaimsKeyName);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, GetJsonSettings());
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSettings());
        }

        private static JsonSerializerSettings GetJsonSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IsoDateTimeConverter {DateTimeFormat = "dd.MM.yyyy"});
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
        }
    }
}