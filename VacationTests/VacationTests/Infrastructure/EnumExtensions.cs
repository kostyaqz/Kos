using System;
using System.ComponentModel;

namespace VacationTests.Infrastructure
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class EnumExtensions
    {
        /// <summary>Получить атрибут Description</summary>
        /// <param name="value">Значение enum</param>
        /// <returns>Значение атрибута Description</returns>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString())
                            ?? throw new Exception($"No field {value} in enum {value.GetType()}");

            var attributes =
                (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}