namespace WaffleSupply.AcceptanceTests.ApiHelpers
{
    public class ApiService
    {
        public WaffleSupplyApiHelper WaffleSupply { get; }

        public ApiService(WaffleSupplyApiHelper waffleSupplyApiHelper)
        {
            WaffleSupply = waffleSupplyApiHelper;
        }
    }
}