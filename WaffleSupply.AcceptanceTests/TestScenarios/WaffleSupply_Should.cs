using System.Net;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using WaffleSupply.AcceptanceTests.Extensions;
using WaffleSupply.AcceptanceTests.Setup;
using Xunit;

namespace WaffleSupply.AcceptanceTests.TestScenarios
{
    public class WaffleSupply_Should : BaseAcceptanceTest
    {
        public WaffleSupply_Should(TestFixture testFixture) : base(testFixture)
        {
        }

        [Theory, AutoData]
        public async Task Return_Correct_Waffle_Amount_After_Adjustment(
            int expectedWaffleAdjustment)
        {
            var response = await Api.WaffleSupply.AdjustWaffleSupply(expectedWaffleAdjustment);
            
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            response = await Api.WaffleSupply.GetWaffleSupply();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var waffleAmount = await response.GetBodyAsync<WaffleAmount>();
            
            Assert.Equal(expectedWaffleAdjustment, waffleAmount.CurrentSupplyOfWaffles);
        }

        private class WaffleAmount
        {
            public int CurrentSupplyOfWaffles { get; set; }
        }
    }
}