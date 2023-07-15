using System;
using Microsoft.Extensions.ObjectPool;
using OpenQA.Selenium;

namespace VacationTests.Tests
{
    internal class WebDriverPoolCache
    {
        private readonly GlobalCache cache;
        private readonly GlobalLifetimeManager lifetimeManager;
        private readonly ObjectPoolProvider objectPoolProvider;
        private readonly IPooledObjectPolicy<WebDriver> policy;

        public WebDriverPoolCache(
            GlobalCache cache,
            GlobalLifetimeManager lifetimeManager,
            ObjectPoolProvider objectPoolProvider,
            IPooledObjectPolicy<WebDriver> policy)
        {
            this.cache = cache;
            this.lifetimeManager = lifetimeManager;
            this.objectPoolProvider = objectPoolProvider;
            this.policy = policy;
        }

        public ObjectPool<WebDriver> WebDriverPool => cache.GetOrCreate(CreatePool);

        private ObjectPool<WebDriver> CreatePool()
        {
            var pool = objectPoolProvider.Create(policy);
            lifetimeManager.RegisterForDispose((IDisposable)pool);
            return pool;
        }
    }
}