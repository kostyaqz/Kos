#nullable enable
using System;
using NUnit.Framework.Internal;

namespace VacationTests.DependencyInjection
{
    internal sealed class TestAccessor
    {
        private TestMethod? test;

        public TestMethod Test
        {
            get => test ?? throw new InvalidOperationException("Test is not yet set");
            internal set => test = value;
        }
    }
}