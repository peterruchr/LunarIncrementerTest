using System.ComponentModel.DataAnnotations;

namespace WaffleSupply.Models
{
    public class AdjustWarmWaffleSupplyRequest
    {
        public AdjustWarmWaffleSupplyRequest(int waffleAdjustment)
        {
            WaffleAdjustment = waffleAdjustment;
        }

        [Required]
        public int WaffleAdjustment { get; }
    }
}