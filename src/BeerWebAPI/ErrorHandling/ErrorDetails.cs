using System.Text.Json;

namespace BeerWebAPI.ErrorHandling
{
    /// <summary>
    /// Error model response class
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}