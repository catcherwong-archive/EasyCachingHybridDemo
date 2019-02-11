namespace App1
{
    using EasyCaching.Bus.Redis;
    using EasyCaching.Core;
    using EasyCaching.Core.Configurations;
    using EasyCaching.HybridCache;
    using EasyCaching.InMemory;
    using EasyCaching.Redis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEasyCaching(option =>
            {
                // local
                option.UseInMemory("m1");
                // distributed
                option.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                    config.DBConfig.Database = 5;
                }, "myredis");

                // combine local and distributed
                option.UseHybrid(config =>
                {
                    config.TopicName = "test-topic";
                    config.EnableLogging = true;
                })
                // use redis bus
                .WithRedisBus(busConf =>
                {
                    busConf.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
