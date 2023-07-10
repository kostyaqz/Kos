using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.Css;
using OpenQA.Selenium;

namespace VacationTests.Infrastructure.PageElements
{
    public class Input : ControlBase
    {
        private readonly IWebElement input;

        public Input(IContextBy contextBy) : base(contextBy)
        {
            input = Container.SearchElement(x => x.Css("input"));
        }

        public IProp<string> Value => input.Value();

        public void ClearAndInputText(string value)
        {
            ClearText();
            InputText(value);
        }

        public void InputText(string value)
        {
            input.SendKeys(value);
        }

        public void ClearText()
        {
            input.Click();
            input.SendKeys(Keys.End);
            var length = input.Value().Get().Length;
            while (length > 0)
            {
                input.SendKeys(Keys.Backspace);
                length--;
            }

            WaitEmpty();
        }

        public void WaitEmpty()
        {
            Value.Wait().EqualTo(string.Empty);
        }
    }
}