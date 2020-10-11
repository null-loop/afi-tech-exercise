using System;
using System.Net.Http;
using AFIExercise.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace AFIExercise.Tests.Integration
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            _server = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile("testsettings.json");
                })
                .UseStartup<Startup>());
            Client = _server.CreateClient();
        }


        public void Dispose()
        {
            _server.Dispose();
        }
    }
}