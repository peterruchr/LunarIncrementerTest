using System.Threading.Tasks;
using System.Transactions;
using AutoFixture.Xunit2;
using WaffleSupply.Models;
using WaffleSupply.Persistence.WaffleSupply;
using Xunit;

namespace WaffleSupply.UnitTests.Persistence.WaffleSupply
{
    public class InMemoryWaffleSupplyRepository_Should
    {
        [Theory, AutoData]
        public async Task Return_Zero_Warm_Waffles_As_Default(
            InMemoryWaffleSupplyRepository sut)
        {
            var currentWaffleSupply = await sut.GetCurrentWaffleSupply();
            
            Assert.Equal(0, currentWaffleSupply.CurrentSupplyOfWaffles);
        }
        
        [Theory, AutoData]
        public async Task Return_Correct_Waffle_Amount_When_Adjusted(
            WaffleSupplyAdjustmentRequest adjustmentRequest,
            InMemoryWaffleSupplyRepository sut)
        {
            await sut.AdjustWaffleSupply(adjustmentRequest);
            var currentWaffleSupply = await sut.GetCurrentWaffleSupply();
            
            Assert.Equal(adjustmentRequest.AdjustmentAmount, currentWaffleSupply.CurrentSupplyOfWaffles);
        }

        [Theory, AutoData]
        public async Task Bound_Waffle_Supply_To_Zero_When_Adjusted_To_Negative_Amount(
            InMemoryWaffleSupplyRepository sut)
        {
            var adjustmentRequest = new WaffleSupplyAdjustmentRequest(-500);
            await sut.AdjustWaffleSupply(adjustmentRequest);

            var currentWaffleSupply = await sut.GetCurrentWaffleSupply();
            
            Assert.Equal(0, currentWaffleSupply.CurrentSupplyOfWaffles);
        }

        [Theory, AutoData]
        public async Task Return_Correct_Waffle_Amount_When_Stressed_Concurrently(
            InMemoryWaffleSupplyRepository sut)
        {
            var taskOne = AdjustWaffleSupplyConcurrently(sut);
            var taskTwo = AdjustWaffleSupplyConcurrently(sut);
            var taskThree = AdjustWaffleSupplyConcurrently(sut);

            await Task.WhenAll(taskOne, taskTwo, taskThree);

            var expectedAmountOfWaffles = 3000;
            var actualAmountOfWaffles = await sut.GetCurrentWaffleSupply();

            Assert.Equal(expectedAmountOfWaffles, actualAmountOfWaffles.CurrentSupplyOfWaffles);
        }

        private Task AdjustWaffleSupplyConcurrently(InMemoryWaffleSupplyRepository sut)
        {
            return Task.Run(async () =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    await sut.AdjustWaffleSupply(new WaffleSupplyAdjustmentRequest(1));
                }
            });
        }
    }
}