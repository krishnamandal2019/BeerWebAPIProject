using System.ComponentModel.DataAnnotations;

namespace BeerWebAPI.Shared.Models
{
    public class BeerModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal PercentageAlcoholByVolume { get; set; }

    }
}
