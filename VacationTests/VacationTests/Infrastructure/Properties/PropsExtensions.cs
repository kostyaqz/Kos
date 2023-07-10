using System;
using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using OpenQA.Selenium;

// Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396

// О методах расширения
// https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92

namespace VacationTests.Infrastructure.Properties
{
    public static class PropsExtensions
    {
        public static IProp<bool> Disabled(this IWebElement webElement)
        {
            // Когда элемент сразу доступен, то data-prop-disabled не имеет значения,
            // когда элемент становится доступен после каких-то дейсвий, то data-prop-disabled = false
            var attribute = webElement.Attribute("data-prop-disabled");
            return attribute.Get() == null ? attribute.NullBoolean() : attribute.Boolean();
        }

        public static IProp<bool> Checked(this IWebElement webElement)
        {
            return webElement.Attribute("data-prop-checked").Boolean();
        }

        public static IProp<DateTime> Date(this IWebElement webElement)
        {
            return webElement.ReactValue().DateTime();
        }

        public static IProp<bool> Boolean(this IProp<string> property)
        {
            return property.Transform(new BooleanTransformation());
        }

        public static IProp<bool> NullBoolean(this IProp<string> property)
        {
            return property.Transform(new NullBooleanTransformation());
        }

        public static IProp<int> Integer(this IProp<string> property)
        {
            return property.Transform(new IntegerTransformation());
        }

        public static IProp<decimal> Currency(this IProp<string> property)
        {
            return property.Transform(new CurrencyTransformation());
        }

        public static IProp<DateTime> DateTime(this IProp<string> property)
        {
            return property.Transform(new DateTimeTransformation());
        }

        public static IProp<bool> HasError(this IWebElement webElement)
        {
            return webElement.Attribute("data-prop-error").Boolean();
        }

        public static IProp<string> ReactValue(this IWebElement webElement)
        {
            return webElement.Attribute("data-prop-value");
        }
    }
}