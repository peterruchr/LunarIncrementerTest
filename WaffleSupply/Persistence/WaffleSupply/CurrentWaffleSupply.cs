namespace WaffleSupply.Persistence.WaffleSupply
{
    public class CurrentWaffleSupply
    {
        public CurrentWaffleSupply(int currentSupplyOfWaffles)
        {
            CurrentSupplyOfWaffles = currentSupplyOfWaffles;
        }

        public int CurrentSupplyOfWaffles { get; }
    }
}