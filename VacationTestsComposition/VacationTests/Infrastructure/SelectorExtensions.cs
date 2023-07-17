using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Css;

namespace VacationTests.Infrastructure
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class SelectorExtensions
    {
        public static CssBy WithTid(this ByDummy dummy, string tid)
        {
            return dummy.Css().WithTid(tid);
        }
    }
}