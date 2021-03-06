﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;
using WaffleSupplier.SupplyClient;

namespace WaffleSupplier.HostedServices
{
    public class EvilWaffleDecayService : BackgroundService
    {
        private static readonly TimeSpan DecayTime = TimeSpan.FromSeconds(2);
        
        private readonly IWaffleSupplyClient _waffleSupplyClient;
        private readonly ILogger _logger;

        public EvilWaffleDecayService(
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
                _logger.Information("Quickly eat your waffles, before they decay!");

                await AdjustWaffleSupply(-10, stoppingToken);

                await Task.Delay(DecayTime, stoppingToken);
            }
        }

        private async Task AdjustWaffleSupply(int adjustmentAmount, CancellationToken stoppingToken)
        {
            try
            {
                await _waffleSupplyClient.AdjustWaffleSupply(adjustmentAmount, stoppingToken);
            }
            catch (WaffleSupplyException)
            {
                _logger.Information("Score! Decay couldnt get to the waffles!");
            }
        }
    }
}