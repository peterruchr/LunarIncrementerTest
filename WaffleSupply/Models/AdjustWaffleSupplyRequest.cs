using System.ComponentModel.DataAnnotations;

namespace WaffleSupply.Models
{
    public class AdjustWaffleSupplyRequest
    {
        [Required]
        public int WaffleAdjustment { get; set; }
    }
}