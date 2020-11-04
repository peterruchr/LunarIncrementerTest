using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WaffleSupplier.Extensions;

namespace WaffleSupplier
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => builder.ClearProviders())
                .ConfigureServices(collection => collection.AddWaffleSupplier())
                .Build()
                .RunAsync();
        }
    }
}