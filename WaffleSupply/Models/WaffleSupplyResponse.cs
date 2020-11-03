namespace WaffleSupply.Models
{
    public class WaffleSupplyResponse
    {
        public WaffleSupplyResponse(int currentSupplyOfWarmWaffles)
        {
            CurrentSupplyOfWarmWaffles = currentSupplyOfWarmWaffles;
        }

        public int CurrentSupplyOfWarmWaffles { get; }
    }
}