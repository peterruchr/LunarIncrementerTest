using Xunit;

namespace WaffleSupply.AcceptanceTests.Setup
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
        
    }
}