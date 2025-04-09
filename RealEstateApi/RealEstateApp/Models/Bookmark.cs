using Newtonsoft.Json;

namespace RealEstateApp.Models;

public class Bookmark
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("status")]
    public bool Status { get; set; }

    [JsonProperty("user_Id")]
    public int UserId { get; set; }

    [JsonProperty("propertyId")]
    public int PropertyId { get; set; }
}
