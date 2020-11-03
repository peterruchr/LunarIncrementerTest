using System.Threading.Tasks;

namespace WaffleSupply.Persistence.WaffleSupply
{
    public class InMemoryWaffleSupplyRepository : IWaffleSupplyRepository
    {
        private int WarmWaffleSupply = 0;
        private readonly object _lockingObject = new object();
        
        public Task AdjustWaffleSupply(WaffleSupplyAdjustmentRequest adjustmentRequest)
        {
            lock (_lockingObject)
            {
                WarmWaffleSupply += adjustmentRequest.AdjustmentAmount;
                if (WarmWaffleSupply < 0)
                    WarmWaffleSupply = 0;
            }

            return Task.CompletedTask;
        }

        public Task<CurrentWaffleSupply> GetCurrentWaffleSupply()
        {
            return Task.FromResult(new CurrentWaffleSupply(WarmWaffleSupply));
        }
    }
}