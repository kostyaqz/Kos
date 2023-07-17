using System;

namespace VacationTests.Infrastructure
{
    public class ByTidAttribute : Attribute
    {
        public ByTidAttribute(string tid)
        {
            Tid = tid;
        }

        public string Tid { get; }
    }
}