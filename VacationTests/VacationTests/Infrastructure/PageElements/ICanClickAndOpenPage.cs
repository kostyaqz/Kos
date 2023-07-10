namespace VacationTests.Infrastructure.PageElements
{
    // Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396

    /// <summary>
    ///     Маркерный интерфейс, чтобы возможность вызвать Extension-метод ClickAndOpen была только для тех контролов,
    ///     у которых этот интерфейс явно указан
    /// </summary>
    public interface ICanClickAndOpenPage : IHaveContainer
    {
        ControlFactory ControlFactory { get; }
        void Click();
    }
}