using System.Threading.Tasks;

namespace WaffleSupply.Persistence.WaffleSupply
{
    public interface IWaffleSupplyRepository
    {
        Task AdjustWaffleSupply(WaffleSupplyAdjustmentRequest adjustmentRequest);
        Task<CurrentWaffleSupply> GetCurrentWaffleSupply();
    }
}