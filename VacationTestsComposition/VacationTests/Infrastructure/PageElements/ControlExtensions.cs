namespace VacationTests.Infrastructure.PageElements
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class ControlExtensions
    {
        public static void WaitPresence(this ControlBase control)
        {
            control.Present.Wait().EqualTo(true);
        }

        public static void WaitAbsence(this ControlBase control)
        {
            control.Present.Wait().EqualTo(false);
        }

        public static void WaitDisabled(this ControlBase control)
        {
            control.Disabled.Wait().EqualTo(true);
        }

        public static void WaitEnabled(this ControlBase control)
        {
            control.Disabled.Wait().EqualTo(false);
        }
    }
}