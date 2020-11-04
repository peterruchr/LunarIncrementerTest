using System.Threading;
using System.Threading.Tasks;

namespace WaffleSupplier.SupplyClient
{
    public interface IWaffleSupplyClient
    {
        Task AdjustWaffleSupply(int adjustSupplyAmount, CancellationToken cancellationToken = default);
        Task<WaffleSupplyResponse> GetWaffleSupply(CancellationToken cancellationToken = default);
    }
}