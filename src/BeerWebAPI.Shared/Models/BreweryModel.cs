using System.ComponentModel.DataAnnotations;

namespace BeerWebAPI.Shared.Models
{
    public class BreweryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
