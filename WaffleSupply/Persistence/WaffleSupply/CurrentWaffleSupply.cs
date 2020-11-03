namespace WaffleSupply.Persistence.WaffleSupply
{
    public class CurrentWaffleSupply
    {
        public CurrentWaffleSupply(int currentSupplyOfWarmWaffles)
        {
            CurrentSupplyOfWarmWaffles = currentSupplyOfWarmWaffles;
        }

        public int CurrentSupplyOfWarmWaffles { get; }
    }
}