namespace VacationTests.Infrastructure
{
    public class ByTidAttribute : BaseSearchByAttribute
    {
        public ByTidAttribute(string tid) 
            : base(x => x.WithTid(tid))
        {
        }
    }
}