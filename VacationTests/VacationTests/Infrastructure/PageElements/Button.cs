using Kontur.Selone.Extensions;
using Kontur.Selone.Properties;
using Kontur.Selone.Selectors.Context;

namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
    // О наследовании https://ulearn.me/course/basicprogramming/Nasledovanie_ac2b8cb6-8d63-4b81-9083-eaa77ab0c89c
    public class Button : ControlBase, ICanClickAndOpenPage
    {
        private readonly ControlFactory controlFactory;

        public Button(IContextBy contextBy, ControlFactory controlFactory) : base(contextBy)
        {
            this.controlFactory = controlFactory;
        }

        public IProp<string> Text => Container.Text();
        
        // Явная реализация интерфейса − способ скрыть ControlFactory из автокомплита,
        // но оставить доступ к нему из классов инфраструктуры.
        ControlFactory ICanClickAndOpenPage.ControlFactory => controlFactory;
    }
}