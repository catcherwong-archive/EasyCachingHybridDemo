using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace App1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8890")
                .UseStartup<Startup>();
    }
}
