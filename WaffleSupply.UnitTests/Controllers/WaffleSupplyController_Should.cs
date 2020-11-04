using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WaffleSupply.Controllers;
using WaffleSupply.Models;
using WaffleSupply.Persistence.WaffleSupply;
using Xunit;

namespace WaffleSupply.UnitTests.Controllers
{
    public class WaffleSupplyController_Should
    {
        private readonly WaffleSupplyController _sut;
        private readonly Mock<IWaffleSupplyRepository> _waffleSupplyRepository;
        
        public WaffleSupplyController_Should()
        {
            _waffleSupplyRepository = new Mock<IWaffleSupplyRepository>();
            _sut = new WaffleSupplyController(_waffleSupplyRepository.Object);
        }

        [Theory, AutoData]
        public async Task Call_AdjustWaffleSupply_On_Call_To_AdjustWarmWaffleSupply(
            AdjustWaffleSupplyRequest adjustWaffleSupplyRequest)
        {
            await _sut.AdjustWarmWaffleSupply(adjustWaffleSupplyRequest);
            
            _waffleSupplyRepository.Verify(x => 
                x.AdjustWaffleSupply(It.Is<WaffleSupplyAdjustmentRequest>(request => 
                    request.AdjustmentAmount == adjustWaffleSupplyRequest.WaffleAdjustment)));
        }

        [Theory, AutoData]
        public async Task Return_NoContent_Response_On_AdjustWarmWaffleSupply(
            AdjustWaffleSupplyRequest adjustWaffleSupplyRequest)
        {
            var response = await _sut.AdjustWarmWaffleSupply(adjustWaffleSupplyRequest);

            Assert.IsAssignableFrom<NoContentResult>(response);
        }

        [Theory, AutoData]
        public async Task Return_CurrentWaffleSupply_On_GetWaffleSupply(
            CurrentWaffleSupply expectedWaffleSupply)
        {
            _waffleSupplyRepository.Setup(x => x.GetCurrentWaffleSupply())
                .ReturnsAsync(expectedWaffleSupply);

            var response = await _sut.GetWaffleSupply();

            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var actualWaffleSupply = Assert.IsAssignableFrom<CurrentWaffleSupply>(okObjectResult.Value);
            
            Assert.Equal(expectedWaffleSupply.CurrentSupplyOfWaffles, actualWaffleSupply.CurrentSupplyOfWaffles);
        }
    }
}