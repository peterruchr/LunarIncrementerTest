using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using WaffleSupplier.SupplyClient;

namespace WaffleSupplier.HostedServices
{
    public class WaffleSupplierService : BackgroundService
    {
        private readonly IWaffleSupplyClient _waffleSupplyClient;
        private readonly ILogger _logger;

        public WaffleSupplierService(
            IWaffleSupplyClient waffleSupplyClient,
            ILogger logger)
        {
            _waffleSupplyClient = waffleSupplyClient;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    var currentSupply = await _waffleSupplyClient.GetWaffleSupply(stoppingToken);
                    if (currentSupply.CurrentSupplyOfWaffles > 100)
                    {
                        _logger.Information("Our waffle supply is too large! No need to make more");
                        await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                        continue;
                    }

                    _logger.Information("Making waffles");
                    await _waffleSupplyClient.AdjustWaffleSupply(20, stoppingToken);
                }
                catch (WaffleSupplyException)
                {
                    _logger.Information("Oh no, we can't deliever our waffles to our supply");
                }
            }
        }
    }
}