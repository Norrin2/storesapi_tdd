using System.Text.Json.Serialization;

namespace StoresApi.Models
{
    public class Store
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
    }
}
