using System;
using System.Collections.Concurrent;
using System.Linq;
using NUnit.Framework;

namespace VacationTests.Tests
{
    [SetUpFixture]
    internal sealed class GlobalLifetimeManager : IDisposable
    {
        private readonly ConcurrentBag<IDisposable> disposables = new();

        public void Dispose()
        {
            foreach (var disposable in disposables.Reverse())
            {
                disposable.Dispose();
            }
        }

        internal void RegisterForDispose(IDisposable disposable) => disposables.Add(disposable);
    }
}