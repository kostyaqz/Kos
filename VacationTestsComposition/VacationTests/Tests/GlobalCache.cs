using System;
using System.Collections.Concurrent;
using NUnit.Framework;

namespace VacationTests.Tests
{
    [SetUpFixture]
    internal sealed class GlobalCache
    {
        private readonly ConcurrentDictionary<Type, Lazy<object>> cache = new();

        internal T GetOrCreate<T>(Func<T> factory)
            where T : notnull
        {
            Lazy<object> lazy = new(() => factory());
            return (T)cache.GetOrAdd(typeof(T), lazy).Value;
        }
    }
}