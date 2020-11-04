using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WaffleSupply.Models;
using WaffleSupply.Persistence.WaffleSupply;

namespace WaffleSupply.Controllers
{
    [ApiController]
    [Route("api/v1/waffle-supplies")]
    public class WaffleSupplyController : ControllerBase
    {
        private readonly IWaffleSupplyRepository _waffleSupplyRepository;

        public WaffleSupplyController(IWaffleSupplyRepository waffleSupplyRepository)
        {
            _waffleSupplyRepository = waffleSupplyRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> AdjustWarmWaffleSupply(
            [FromBody] AdjustWaffleSupplyRequest adjustWaffleSupplyRequest)
        {
            var persistentAdjustmentRequest = new WaffleSupplyAdjustmentRequest(adjustWaffleSupplyRequest.WaffleAdjustment);

            await _waffleSupplyRepository.AdjustWaffleSupply(persistentAdjustmentRequest);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetWaffleSupply()
        {
            var currentWaffleSupply = await _waffleSupplyRepository.GetCurrentWaffleSupply();

            return Ok(currentWaffleSupply);
        }
    }
}