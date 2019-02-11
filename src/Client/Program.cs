using System;

namespace Client
{
    using System.Net.Http;

    class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string _App1Url = "http://localhost:8890";
        private const string _App2Url = "http://localhost:8891";

        static void Main(string[] args)
        {
            //1. each apps don't hava cached value

            var f_app1 = _client.GetStringAsync($"{_App1Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($" first time visit app1 get the following result : ");
            Console.WriteLine(f_app1);

            var f_app2 = _client.GetStringAsync($"{_App2Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine($" first time visit app2 get the following result : ");
            Console.WriteLine(f_app2);

            Console.WriteLine("-----");

            //2. call set in app1

            var set_app1 = _client.GetStringAsync($"{_App1Url}/api/values/set").ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($" set cache value in app1 get the following result : ");
            Console.WriteLine(set_app1);

            Console.WriteLine("-----");

            //3. app1 should read from local cache, app2 should read from distributed cache

            var s_app1 = _client.GetStringAsync($"{_App1Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($" second time visit app1 get the following result : ");
            Console.WriteLine(s_app1);

            var s_app2 = _client.GetStringAsync($"{_App2Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine($" second time visit app2 get the following result : ");
            Console.WriteLine(s_app2);

            Console.WriteLine("-----");

            //4. call set in app2

            var set_app2 = _client.GetStringAsync($"{_App2Url}/api/values/set").ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($" set cache value in app2 get the following result : ");
            Console.WriteLine(set_app2);

            Console.WriteLine("-----");

            //5. due to app2 modify the cached value , 
            // app1 should read from distributed cache, app2 should read from local cache

            var t_app1 = _client.GetStringAsync($"{_App1Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($" third time visit app1 get the following result : ");
            Console.WriteLine(t_app1);

            var t_app2 = _client.GetStringAsync($"{_App2Url}/api/values").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine($" third time visit app2 get the following result : ");
            Console.WriteLine(t_app2);

            Console.WriteLine("-----");

            Console.WriteLine("Hello World!");

            Console.Read();
        }
    }
}
