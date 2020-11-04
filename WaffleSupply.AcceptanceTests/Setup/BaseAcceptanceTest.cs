using Microsoft.Extensions.DependencyInjection;
using WaffleSupply.AcceptanceTests.ApiHelpers;
using Xunit;

namespace WaffleSupply.AcceptanceTests.Setup
{
    [Collection(nameof(TestCollection))]
    public class BaseAcceptanceTest
    {
        public ApiService Api { get; }
        
        public BaseAcceptanceTest(TestFixture testFixture)
        {
            Api = testFixture.ServiceProvider.GetRequiredService<ApiService>();
        }
    }
}