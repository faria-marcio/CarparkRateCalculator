using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Rate
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("typeId")]
    public int TypeId { get; set; }

    [JsonPropertyName("prices")]
    public List<Price> Prices { get; set; } = [];

    [JsonPropertyName("entryCondition")]
    public string EntryCondition { get; set; } = string.Empty;

    [JsonPropertyName("exitCondition")]
    public string ExitCondition { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;
}