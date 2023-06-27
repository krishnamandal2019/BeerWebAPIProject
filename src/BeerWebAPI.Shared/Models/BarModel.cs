using System.ComponentModel.DataAnnotations;

namespace BeerWebAPI.Shared.Models
{
    public class BarModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
    }
}
