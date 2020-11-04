using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WaffleSupplier.HostedServices;
using WaffleSupplier.SupplyClient;

namespace WaffleSupplier.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWaffleSupplier(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSerilog()
                .AddTransient<IWaffleSupplyClient, WaffleSupplyClient>();
            serviceCollection.AddHttpClient<IWaffleSupplyClient, WaffleSupplyClient>();


            serviceCollection
                .AddHostedService<EvilWaffleDecayService>()
                .AddHostedService<WaffleSupplierService>();

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