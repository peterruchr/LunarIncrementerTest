using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WaffleSupply.HostedServices;
using WaffleSupply.Persistence.WaffleSupply;

namespace WaffleSupply.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApi(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSerilog()
                .AddSingleton<IWaffleSupplyRepository, InMemoryWaffleSupplyRepository>();

            serviceCollection.AddHostedService<EvilWaffleDecayService>();

            return serviceCollection;
        }

        private static IServiceCollection AddSerilog(this IServiceCollection serviceCollection)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            return serviceCollection.AddSingleton<ILogger>(log);
        }
    }
}