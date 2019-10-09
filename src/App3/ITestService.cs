namespace App3
{
    using EasyCaching.Core.Interceptor;
    using System;
    using System.Threading.Tasks;

    public interface ITestService
    {
        // this will use hybrid provider
        [EasyCachingAble(Expiration = 3600, IsHybridProvider = true)]
        Task<string> TestCacheV1();


        // this will use the provider whose name is m1
        [EasyCachingAble(Expiration = 3600, IsHybridProvider = false, CacheProviderName = "m1")]
        Task<string> TestCacheV2();

        // this will use default provider that specify in  ConfigureAspectCoreInterceptor
        [EasyCachingAble(Expiration = 3600, IsHybridProvider = false)]
        Task<string> TestCacheV3();
    }

    public class TestService : ITestService
    {
        public Task<string> TestCacheV1()
        {
            return Task.FromResult(DateTime.Now.ToString());
        }

        public Task<string> TestCacheV2()
        {
            return Task.FromResult(DateTime.Now.ToString());
        }

        public Task<string> TestCacheV3()
        {
            return Task.FromResult(DateTime.Now.ToString());
        }
    }
}
