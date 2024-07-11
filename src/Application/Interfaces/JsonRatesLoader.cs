using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces;

public class JsonRatesLoader : IRatesLoader
{
    public List<Rate> LoadRatesData()
    {
        string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "rates.json"));
        return JsonSerializer.Deserialize<List<Rate>>(json);
    }
}
