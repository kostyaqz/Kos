using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.XPath;

namespace VacationTests.Infrastructure
{
    // О методах расширения
    // https://ulearn.me/course/basicprogramming/Metody_rasshireniya_01a1f9a5-c475-4af3-bef3-060f92e69a92
    public static class SelectorExtensions
    {
        public static XPathBy WithTid(this ByDummy dummy, string tid)
        {
            return dummy.XPath(".").ThenDescendant().AnyTag().WithAttribute("data-tid", tid);
        }        
        
        public static XPathBy WithTid(this XPathBy xPathBy, string tid) 
        {
            return xPathBy.ThenChild().XPath(".").ThenDescendant().AnyTag().WithAttribute("data-tid", tid);
        }
    }
}