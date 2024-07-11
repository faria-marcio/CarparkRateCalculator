using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Price
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("price")]
    public double Value { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;
}