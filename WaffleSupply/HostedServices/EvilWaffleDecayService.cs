using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using WaffleSupply.Persistence;
using WaffleSupply.Persistence.WaffleSupply;

namespace WaffleSupply.HostedServices
{
    public class EvilWaffleDecayService : BackgroundService
    {
        private static readonly TimeSpan DecayTime = TimeSpan.FromSeconds(10);
        
        private readonly IWaffleSupplyRepository _waffleSupplyRepository;
        private readonly ILogger _logger;

        public EvilWaffleDecayService(
            IWaffleSupplyRepository waffleSupplyRepository,
            ILogger logger)
        {
            _waffleSupplyRepository = waffleSupplyRepository;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                _logger.Information("Quickly eat your waffles! Before time claims them!");
                await _waffleSupplyRepository.AdjustWaffleSupply(new WaffleSupplyAdjustmentRequest(-1));

                await Task.Delay(DecayTime, stoppingToken);
            }
        }
    }
}