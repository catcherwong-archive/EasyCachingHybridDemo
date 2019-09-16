namespace App3
{
    using EasyCaching.Bus.CSRedis;
    using EasyCaching.Core;
    using EasyCaching.CSRedis;
    using EasyCaching.HybridCache;
    using EasyCaching.InMemory;
    using EasyCaching.Interceptor.AspectCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<ITestService, TestService>();

            services.AddEasyCaching(option =>
            {
                // local
                option.UseInMemory("m1");
                // distributed
                option.UseCSRedis(config =>
                {
                    config.DBConfig = new CSRedisDBOptions
                    {
                        ConnectionStrings = new System.Collections.Generic.List<string>
                        {
                            "127.0.0.1:6388,defaultDatabase=12,poolsize=10"
                        }
                    };
                }, "myredis");

                // combine local and distributed
                option.UseHybrid(config =>
                {
                    config.TopicName = "test-topic";

                    config.EnableLogging = true;

                    config.LocalCacheProviderName = "m1";
                    config.DistributedCacheProviderName = "myredis";
                })
                .WithCSRedisBus(busConf =>
                {
                    busConf.ConnectionStrings = new System.Collections.Generic.List<string>
                    {
                        "127.0.0.1:6379,defaultDatabase=13,poolsize=10"
                    };
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.ConfigureAspectCoreInterceptor(options =>
            {
                // this is the default provider if you do not specify the provider name in the Attribute.
                options.CacheProviderName = "myredis";
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
