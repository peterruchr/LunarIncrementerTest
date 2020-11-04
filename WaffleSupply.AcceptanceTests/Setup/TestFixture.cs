using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WaffleSupply.AcceptanceTests.ApiHelpers;

namespace WaffleSupply.AcceptanceTests.Setup
{
    public class TestFixture
    {
        public IServiceProvider ServiceProvider { get; }
        
        public TestFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddTransient<WaffleSupplyApiHelper>();
            serviceCollection.AddHttpClient<WaffleSupplyApiHelper>();
            serviceCollection.AddTransient<ApiService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}