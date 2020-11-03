namespace WaffleSupply.Persistence.WaffleSupply
{
    public class WaffleSupplyAdjustmentRequest
    {
        public WaffleSupplyAdjustmentRequest(int adjustmentAmount)
        {
            AdjustmentAmount = adjustmentAmount;
        }

        public int AdjustmentAmount { get; }
    }
}